#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2012 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.GraphicsHelper;
using HelpersLib.Hotkeys2;
using ShareX.HelperClasses;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.GUI;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;

namespace ShareX
{
    public class Task : IDisposable
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void TaskEventHandler(UploadInfo info);

        public event TaskEventHandler UploadStarted;
        public event TaskEventHandler UploadPreparing;
        public event TaskEventHandler UploadProgressChanged;
        public event TaskEventHandler UploadCompleted;

        public Workflow Workflow = new Workflow();
        public UploadInfo Info { get; private set; }
        public TaskStatus Status { get; private set; }
        public bool IsWorking { get { return Status == TaskStatus.Preparing || Status == TaskStatus.Uploading; } }
        public bool IsStopped { get; private set; }

        private Stream data;
        private ImageData imageData;
        private string tempText;
        private ThreadWorker threadWorker;
        private Uploader uploader;

        #region Constructors

        private Task(EDataType dataType, TaskJob job)
        {
            Status = TaskStatus.InQueue;
            Info = new UploadInfo();
            Info.Job = job;
            Info.DataType = dataType;
            Info.UploadDestination = dataType;
        }

        public static Task CreateDataUploaderTask(EDataType dataType, Stream stream, string filePath, EDataType destination = EDataType.Default)
        {
            Task task = new Task(dataType, TaskJob.DataUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FileName = Path.GetFileName(filePath);
            task.Info.FilePath = filePath;
            task.data = stream;
            return task;
        }

        // string filePath -> FileStream data
        public static Task CreateFileUploaderTask(EDataType dataType, string filePath, EDataType destination = EDataType.Default)
        {
            Task task = new Task(dataType, TaskJob.FileUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FilePath = filePath;
            task.data = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return task;
        }

        // Image image -> MemoryStream data (in thread)
        public static Task CreateImageUploaderTask(ImageData imageData, EDataType destination = EDataType.Default)
        {
            Task task = new Task(EDataType.Image, TaskJob.ImageUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FileName = imageData.Filename;
            task.imageData = imageData;
            return task;
        }

        // string text -> MemoryStream data (in thread)
        public static Task CreateTextUploaderTask(string text, EDataType destination = EDataType.Default)
        {
            Task task = new Task(EDataType.Text, TaskJob.TextUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;

            if (Program.Settings.IndexFolderWhenPossible && Directory.Exists(text))
            {
                bool html = destination == EDataType.File;
                task.Info.FileName = new NameParser().Convert(Program.Settings.NameFormatPatternOther) + (html ? ".html" : ".log");
                task.tempText = IndexersLib.QuickIndexer.Index(text, html, Program.Settings.IndexerConfig);
            }
            else
            {
                task.Info.FileName = new NameParser().Convert(Program.Settings.NameFormatPatternOther) + ".txt";
                task.tempText = text;
            }
            return task;
        }

        public static Task CreateURLShortenerTask(string url)
        {
            Task task = new Task(EDataType.URL, TaskJob.ShortenURL);
            task.Info.FileName = url;
            task.Info.Result.URL = url;
            return task;
        }

        #endregion Constructors

        public void Start()
        {
            if (Status == TaskStatus.InQueue && !IsStopped)
            {
                OnUploadPreparing();

                threadWorker = new ThreadWorker();
                threadWorker.DoWork += ThreadDoWork;
                threadWorker.Completed += ThreadCompleted;
                threadWorker.Start();
            }
        }

        public void Stop()
        {
            IsStopped = true;

            if (Status == TaskStatus.InQueue)
            {
                OnUploadCompleted();
            }
            else if (Status == TaskStatus.Uploading && uploader != null)
            {
                uploader.StopUpload();
            }
        }

        private void ThreadDoWork()
        {
            DoThreadJob();

            if (Info.Jobs.HasFlag(Subtask.UploadImageToHost))
            {
                if (Program.UploadersConfig == null)
                {
                    Program.UploaderSettingsResetEvent.WaitOne();
                }

                Status = TaskStatus.Uploading;
                Info.Status = "Uploading";
                Info.StartTime = DateTime.UtcNow;
                threadWorker.InvokeAsync(OnUploadStarted);

                try
                {
                    if (data != null && data.CanSeek)
                    {
                        data.Position = 0;

                        switch (Info.UploadDestination)
                        {
                            case EDataType.Image:
                                Info.Result = UploadImage(data);
                                break;
                            case EDataType.File:
                                Info.Result = UploadFile(data);
                                break;
                            case EDataType.Text:
                                Info.Result = UploadText(data);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!IsStopped)
                    {
                        log.Error("Stopped.", ex);
                        uploader.Errors.Add(ex.ToString());
                    }
                    uploader.Errors.Add(ex.ToString());
                }
                finally
                {
                    if (Info.Result == null) Info.Result = new UploadResult();
                    if (uploader != null) Info.Result.Errors = uploader.Errors;
                }
            }
            else
            {
                Info.Result.IsURLExpected = false;
            }

            if (!IsStopped && Info.Result != null && Info.Result.IsURLExpected && !Info.Result.IsError)
            {
                if (string.IsNullOrEmpty(Info.Result.URL))
                {
                    Info.Result.Errors.Add("URL is empty.");
                }
                else if (Program.Settings.URLShortenAfterUpload || Info.Job == TaskJob.ShortenURL || Info.DestConfig.LinkUploaders.Count > 0)
                {
                    Info.Result.ShortenedURL = ShortenURL(Info.Result.URL);
                }
            }

            Info.UploadTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Mod 01: Info.FilePath = imageData.WriteToFile(Program.ScreenshotsPath);
        /// Mod 02: imageData disposes when Task disposes
        /// </summary>
        private void DoThreadJob()
        {
            if (Info.Job == TaskJob.ImageUpload && imageData != null)
            {
                DoBeforeImagePreparedJobs();

                if (Info.Jobs.HasFlagAny(Subtask.UploadImageToHost, Subtask.SaveImageToFile,
                    Subtask.SaveImageToFileWithDialog))
                {
                    imageData.PrepareImageAndFilename();

                    data = imageData.ImageStream;
                    Info.FileName = imageData.Filename;

                    if (Info.Jobs.HasFlag(Subtask.SaveImageToFile))
                    {
                        Info.FilePath = imageData.WriteToFile(Program.ScreenshotsPath);
                    }
                    if (Info.Jobs.HasFlag(Subtask.SaveImageToFileWithDialog) && Directory.Exists(Info.FolderPath))
                    {
                        string fp = imageData.WriteToFile(Info.FolderPath);
                        if (string.IsNullOrEmpty(Info.FilePath))
                            Info.FilePath = fp;
                    }
                }
            }
            else if (Info.Job == TaskJob.TextUpload && !string.IsNullOrEmpty(tempText))
            {
                if (Info.Jobs.HasFlag(Subtask.Print))
                {
                    threadWorker.InvokeAsync(PrintText);
                }

                if (Info.Jobs.HasFlag(Subtask.SaveImageToFileWithDialog))
                {
                    threadWorker.InvokeAsync(SaveTextToFileWithDialog);
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(tempText);
                data = new MemoryStream(byteArray);
            }
        }

        private void SaveTextToFileWithDialog()
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = string.Format("Choose a folder to save {0}", Info.FileName);
                dlg.ShowNewFolderButton = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(Path.Combine(dlg.SelectedPath, Info.FileName)))
                    {
                        sw.WriteLine(tempText);
                    }
                }
            }
        }

        private void SaveImageToFileWithDialog()
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = string.Format("Choose a folder to save {0}", Info.FileName);
                dlg.ShowNewFolderButton = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Info.FolderPath = dlg.SelectedPath;
                }
            }
        }

        private void PrintImage()
        {
            new PrintForm(imageData.ImageExported, Program.Settings.PrintSettings).Show();
        }

        private void PrintText()
        {
            new PrintHelper(tempText) { Settings = Program.Settings.PrintSettings }.Print();
        }

        private void DoBeforeImagePreparedJobs()
        {
            if (Info.Jobs.HasFlag(Subtask.AnnotateImageAddShadowBorder))
            {
                if (!Greenshot.IniFile.IniConfig.IsInited)
                    Greenshot.IniFile.IniConfig.Init();

                imageData.Image = GreenshotPlugin.Core.ImageHelper.CreateShadow(imageData.Image, 1f, 7, new Point(7, 7), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }

            if (Info.Jobs.HasFlag(Subtask.ShowImageEffectsStudio))
            {
                ImageEffectsGUI dlg = new ImageEffectsGUI(imageData.Image);
                dlg.ShowDialog();
                imageData.Image = dlg.GetImageForExport();
            }

            if (Info.Jobs.HasFlag(Subtask.AnnotateImageAddTornEffect))
            {
                if (!Greenshot.IniFile.IniConfig.IsInited)
                    Greenshot.IniFile.IniConfig.Init();

                imageData.Image = GreenshotPlugin.Core.ImageHelper.CreateTornEdge(new Bitmap(imageData.Image));
            }

            if (Info.Job == TaskJob.ImageUpload && imageData != null && Info.Jobs.HasFlag(Subtask.CopyImageToClipboard))
            {
                Clipboard.SetImage(imageData.Image);
            }

            if (Info.Jobs.HasFlag(Subtask.Print))
            {
                threadWorker.InvokeAsync(PrintImage);
            }

            if (Info.Jobs.HasFlag(Subtask.SaveImageToFileWithDialog))
            {
                threadWorker.InvokeAsync(SaveImageToFileWithDialog);
            }
        }

        private void ThreadCompleted()
        {
            OnUploadCompleted();
        }

        private void PrepareUploader(Uploader currentUploader)
        {
            uploader = currentUploader;
            uploader.BufferSize = (int)Math.Pow(2, Program.Settings.BufferSizePower) * 1024;
            uploader.ProgressChanged += new Uploader.ProgressEventHandler(uploader_ProgressChanged);
        }

        private void uploader_ProgressChanged(ProgressManager progress)
        {
            if (progress != null)
            {
                Info.Progress = progress;
                threadWorker.InvokeAsync(OnUploadProgressChanged);
            }
        }

        /// <summary>
        /// Uploads an image using a stream and UploadInfo
        /// </summary>
        /// <param name="stream">Data stream</param>
        /// <param name="info">UploadInfo object of the Task</param>
        /// <returns>Returns an UploadResult object with URLs</returns>
        public UploadResult UploadImage(Stream stream)
        {
            ImageUploader imageUploader = null;
            ImageDestination imageDestination = Info.DestConfig.ImageUploaders[0];

            switch (imageDestination)
            {
                case ImageDestination.ImageShack:
                    imageUploader = new ImageShackUploader(ApiKeys.ImageShackKey, Program.UploadersConfig.ImageShackAccountType,
                        Program.UploadersConfig.ImageShackRegistrationCode)
                    {
                        IsPublic = Program.UploadersConfig.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageDestination.TinyPic:
                    imageUploader = new TinyPicUploader(ApiKeys.TinyPicID, ApiKeys.TinyPicKey, Program.UploadersConfig.TinyPicAccountType,
                        Program.UploadersConfig.TinyPicRegistrationCode);
                    break;
                case ImageDestination.Imgur:
                    imageUploader = new Imgur(Program.UploadersConfig.ImgurAccountType, ApiKeys.ImgurAnonymousKey, Program.UploadersConfig.ImgurOAuthInfo)
                    {
                        ThumbnailType = Program.UploadersConfig.ImgurThumbnailType
                    };
                    break;
                case ImageDestination.Flickr:
                    imageUploader = new FlickrUploader(ApiKeys.FlickrKey, ApiKeys.FlickrSecret, Program.UploadersConfig.FlickrAuthInfo, Program.UploadersConfig.FlickrSettings);
                    break;
                case ImageDestination.Photobucket:
                    imageUploader = new Photobucket(Program.UploadersConfig.PhotobucketOAuthInfo, Program.UploadersConfig.PhotobucketAccountInfo);
                    break;
                case ImageDestination.UploadScreenshot:
                    imageUploader = new UploadScreenshot(ApiKeys.UploadScreenshotKey);
                    break;
                case ImageDestination.Twitpic:
                    int indexTwitpic = Program.UploadersConfig.TwitterSelectedAccount;

                    if (Program.UploadersConfig.TwitterOAuthInfoList.IsValidIndex(indexTwitpic))
                    {
                        imageUploader = new TwitPicUploader(ApiKeys.TwitPicKey, Program.UploadersConfig.TwitterOAuthInfoList[indexTwitpic])
                        {
                            TwitPicThumbnailMode = Program.UploadersConfig.TwitPicThumbnailMode,
                            ShowFull = Program.UploadersConfig.TwitPicShowFull
                        };
                    }
                    break;
                case ImageDestination.Twitsnaps:
                    int indexTwitsnaps = Program.UploadersConfig.TwitterSelectedAccount;

                    if (Program.UploadersConfig.TwitterOAuthInfoList.IsValidIndex(indexTwitsnaps))
                    {
                        imageUploader = new TwitSnapsUploader(ApiKeys.TwitsnapsKey, Program.UploadersConfig.TwitterOAuthInfoList[indexTwitsnaps]);
                    }
                    break;
                case ImageDestination.yFrog:
                    YfrogOptions yFrogOptions = new YfrogOptions(ApiKeys.ImageShackKey);
                    yFrogOptions.Username = Program.UploadersConfig.YFrogUsername;
                    yFrogOptions.Password = Program.UploadersConfig.YFrogPassword;
                    yFrogOptions.Source = Application.ProductName;
                    imageUploader = new YfrogUploader(yFrogOptions);
                    break;
                case ImageDestination.Immio:
                    imageUploader = new ImmioUploader();
                    break;
            }

            if (imageUploader != null)
            {
                PrepareUploader(imageUploader);
                return imageUploader.Upload(stream, Info.FileName);
            }

            return null;
        }

        public UploadResult UploadText(Stream stream)
        {
            TextUploader textUploader = null;
            TextDestination textDestination = Info.DestConfig.TextUploaders[0];

            switch (textDestination)
            {
                case TextDestination.Pastebin:
                    PastebinSettings pastebinSettings = Program.UploadersConfig.PastebinSettings;
                    pastebinSettings.TextFormat = Workflow.TextFormat;
                    textUploader = new PastebinUploader(ApiKeys.PastebinKey, pastebinSettings);
                    break;
                case TextDestination.PastebinCA:
                    textUploader = new PastebinCaUploader(ApiKeys.PastebinCaKey, new PastebinCaSettings()
                    {
                        TextFormat = Workflow.TextFormat
                    });
                    break;
                case TextDestination.Paste2:
                    textUploader = new Paste2Uploader(new Paste2Settings()
                    {
                        TextFormat = Workflow.TextFormat
                    });
                    break;
                case TextDestination.Slexy:
                    textUploader = new SlexyUploader(new SlexySettings()
                    {
                        TextFormat = Workflow.TextFormat
                    });
                    break;
            }

            if (textUploader != null)
            {
                PrepareUploader(textUploader);
                string url = textUploader.UploadText(stream);
                return new UploadResult(null, url);
            }

            return null;
        }

        public UploadResult UploadFile(Stream stream)
        {
            FileUploader fileUploader = null;

            switch (Info.DestConfig.FileUploaders[0])
            {
                case FileDestination.Dropbox:
                    NameParser parser = new NameParser { IsFolderPath = true };
                    string uploadPath = parser.Convert(Dropbox.TidyUploadPath(Program.UploadersConfig.DropboxUploadPath));
                    fileUploader = new Dropbox(Program.UploadersConfig.DropboxOAuthInfo, uploadPath, Program.UploadersConfig.DropboxAccountInfo)
                    {
                        AutoCreateShareableLink = Program.UploadersConfig.DropboxAutoCreateShareableLink
                    };
                    break;
                case FileDestination.RapidShare:
                    fileUploader = new RapidShare(Program.UploadersConfig.RapidShareUsername, Program.UploadersConfig.RapidSharePassword,
                        Program.UploadersConfig.RapidShareFolderID);
                    break;
                case FileDestination.SendSpace:
                    fileUploader = new SendSpace(ApiKeys.SendSpaceKey);
                    switch (Program.UploadersConfig.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(ApiKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(ApiKeys.SendSpaceKey, Program.UploadersConfig.SendSpaceUsername, Program.UploadersConfig.SendSpacePassword);
                            break;
                    }
                    break;
                case FileDestination.Minus:
                    fileUploader = new Minus(Program.UploadersConfig.MinusConfig, new OAuthInfo(ApiKeys.MinusConsumerKey, ApiKeys.MinusConsumerSecret));
                    break;
                case FileDestination.Box:
                    fileUploader = new Box(ApiKeys.BoxKey)
                    {
                        AuthToken = Program.UploadersConfig.BoxAuthToken,
                        FolderID = Program.UploadersConfig.BoxFolderID,
                        Share = Program.UploadersConfig.BoxShare
                    };
                    break;
                case FileDestination.CustomUploader:
                    if (Program.UploadersConfig.CustomUploadersList.IsValidIndex(Program.UploadersConfig.CustomUploaderSelected))
                    {
                        fileUploader = new CustomUploader(Program.UploadersConfig.CustomUploadersList[Program.UploadersConfig.CustomUploaderSelected]);
                    }
                    break;
                case FileDestination.FTP:
                    int idFtp = Program.UploadersConfig.GetFtpIndex(Info.DataType);

                    if (Program.UploadersConfig.FTPAccountList.IsValidIndex(idFtp))
                    {
                        FTPAccount account = Program.UploadersConfig.FTPAccountList[idFtp];
                        if (account.Protocol == FTPProtocol.SFTP)
                            fileUploader = new SFTP(account);
                        else
                            fileUploader = new FTPUploader(account);
                    }
                    break;
                case FileDestination.SharedFolder:
                    int idLocalhost = Program.UploadersConfig.GetLocalhostIndex(Info.DataType);
                    if (Program.UploadersConfig.LocalhostAccountList.IsValidIndex(idLocalhost))
                    {
                        fileUploader = new SharedFolderUploader(Program.UploadersConfig.LocalhostAccountList[idLocalhost]);
                    }
                    break;
                case FileDestination.Email:
                    using (EmailForm emailForm = new EmailForm(Program.UploadersConfig.EmailRememberLastTo ? Program.UploadersConfig.EmailLastTo : string.Empty,
                        Program.UploadersConfig.EmailDefaultSubject, Program.UploadersConfig.EmailDefaultBody))
                    {
                        if (emailForm.ShowDialog() == DialogResult.OK)
                        {
                            if (Program.UploadersConfig.EmailRememberLastTo)
                            {
                                Program.UploadersConfig.EmailLastTo = emailForm.ToEmail;
                            }

                            fileUploader = new Email
                            {
                                SmtpServer = Program.UploadersConfig.EmailSmtpServer,
                                SmtpPort = Program.UploadersConfig.EmailSmtpPort,
                                FromEmail = Program.UploadersConfig.EmailFrom,
                                Password = Program.UploadersConfig.EmailPassword,
                                ToEmail = emailForm.ToEmail,
                                Subject = emailForm.Subject,
                                Body = emailForm.Body
                            };
                        }
                        else
                        {
                            IsStopped = true;
                        }
                    }
                    break;
            }

            if (fileUploader != null)
            {
                PrepareUploader(fileUploader);
                return fileUploader.Upload(stream, Info.FileName);
            }

            return null;
        }

        public string ShortenURL(string url)
        {
            URLShortener urlShortener = null;

            if ((Info.DestConfig.LinkUploaders.Count == 0))
                Info.DestConfig.LinkUploaders.Add(UploadManager.URLShortener);

            switch (Info.DestConfig.LinkUploaders[0])
            {
                case UrlShortenerType.BITLY:
                    urlShortener = new BitlyURLShortener(ApiKeys.BitlyLogin, ApiKeys.BitlyKey);
                    break;
                case UrlShortenerType.Google:
                    urlShortener = new GoogleURLShortener(Program.UploadersConfig.GoogleURLShortenerAccountType, ApiKeys.GoogleApiKey,
                        Program.UploadersConfig.GoogleURLShortenerOAuthInfo);
                    break;
                case UrlShortenerType.ISGD:
                    urlShortener = new IsgdURLShortener();
                    break;
                case UrlShortenerType.Jmp:
                    urlShortener = new JmpURLShortener(ApiKeys.BitlyLogin, ApiKeys.BitlyKey);
                    break;
                case UrlShortenerType.TINYURL:
                    urlShortener = new TinyURLShortener();
                    break;
                case UrlShortenerType.TURL:
                    urlShortener = new TurlURLShortener();
                    break;
            }

            if (urlShortener != null)
            {
                Status = TaskStatus.URLShortening;
                return urlShortener.ShortenURL(url);
            }

            return null;
        }

        private void OnUploadPreparing()
        {
            Status = TaskStatus.Preparing;

            switch (Info.Job)
            {
                case TaskJob.ImageUpload:
                case TaskJob.TextUpload:
                    Info.Status = "Preparing";
                    break;
                default:
                    Info.Status = "Starting";
                    break;
            }

            if (UploadPreparing != null)
            {
                UploadPreparing(Info);
            }
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(Info);
            }
        }

        private void OnUploadProgressChanged()
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(Info);
            }
        }

        private void OnUploadCompleted()
        {
            Status = TaskStatus.Completed;

            if (!IsStopped)
            {
                Info.Status = "Done";
            }
            else
            {
                Info.Status = "Stopped";
            }

            if (UploadCompleted != null)
            {
                UploadCompleted(Info);
            }

            Dispose();
        }

        public void Dispose()
        {
            if (data != null) data.Dispose();
            if (imageData != null) imageData.Dispose();
        }
    }
}