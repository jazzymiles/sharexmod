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

            if (SettingsManager.ConfigCore.IndexFolderWhenPossible && Directory.Exists(text))
            {
                bool html = destination == EDataType.File;
                task.Info.FileName = new NameParser().Convert(SettingsManager.ConfigCore.NameFormatPatternOther) + (html ? ".html" : ".log");
                task.tempText = IndexersLib.QuickIndexer.Index(text, html, SettingsManager.ConfigCore.ConfigIndexer);
            }
            else
            {
                task.Info.FileName = new NameParser().Convert(SettingsManager.ConfigCore.NameFormatPatternOther) + ".txt";
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
                if (SettingsManager.ConfigUploaders == null)
                {
                    SettingsManager.UploaderSettingsResetEvent.WaitOne();
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
                else if (SettingsManager.ConfigCore.URLShortenAfterUpload || Info.Job == TaskJob.ShortenURL || Info.DestConfig.LinkUploaders.Count > 0)
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
            new PrintForm(imageData.ImageExported, SettingsManager.ConfigCore.PrintSettings).Show();
        }

        private void PrintText()
        {
            new PrintHelper(tempText) { Settings = SettingsManager.ConfigCore.PrintSettings }.Print();
        }

        private void DoBeforeImagePreparedJobs()
        {
            if (Info.Jobs.HasFlag(Subtask.AnnotateImageAddTornEffect))
            {
                if (!Greenshot.IniFile.IniConfig.IsInited)
                    Greenshot.IniFile.IniConfig.Init();

                imageData.Image = GreenshotPlugin.Core.ImageHelper.CreateTornEdge(new Bitmap(imageData.Image));
            }

            if (Info.Jobs.HasFlag(Subtask.AnnotateImageAddShadowBorder))
            {
                if (!Greenshot.IniFile.IniConfig.IsInited)
                    Greenshot.IniFile.IniConfig.Init();

                imageData.Image = GreenshotPlugin.Core.ImageHelper.CreateShadow(imageData.Image, 1f, 7, new Point(7, 7), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }

            if (Info.Jobs.HasFlag(Subtask.AddWatermark))
            {
                imageData.Image = new HelpersLibWatermark.WatermarkEffects(SettingsManager.ConfigCore.ConfigWatermark).ApplyWatermark(imageData.Image);
            }

            if (Info.Jobs.HasFlag(Subtask.ShowImageEffectsStudio))
            {
                ImageEffectsGUI dlg = new ImageEffectsGUI(imageData.Image);
                dlg.ShowDialog();
                imageData.Image = dlg.GetImageForExport();
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
            uploader.BufferSize = (int)Math.Pow(2, SettingsManager.ConfigCore.BufferSizePower) * 1024;
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
                    imageUploader = new ImageShackUploader(ApiKeys.ImageShackKey, SettingsManager.ConfigUploaders.ImageShackAccountType,
                        SettingsManager.ConfigUploaders.ImageShackRegistrationCode)
                    {
                        IsPublic = SettingsManager.ConfigUploaders.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageDestination.TinyPic:
                    imageUploader = new TinyPicUploader(ApiKeys.TinyPicID, ApiKeys.TinyPicKey, SettingsManager.ConfigUploaders.TinyPicAccountType,
                        SettingsManager.ConfigUploaders.TinyPicRegistrationCode);
                    break;
                case ImageDestination.Imgur:
                    imageUploader = new Imgur(SettingsManager.ConfigUploaders.ImgurAccountType, ApiKeys.ImgurAnonymousKey, SettingsManager.ConfigUploaders.ImgurOAuthInfo)
                    {
                        ThumbnailType = SettingsManager.ConfigUploaders.ImgurThumbnailType
                    };
                    break;
                case ImageDestination.Flickr:
                    imageUploader = new FlickrUploader(ApiKeys.FlickrKey, ApiKeys.FlickrSecret, SettingsManager.ConfigUploaders.FlickrAuthInfo, SettingsManager.ConfigUploaders.FlickrSettings);
                    break;
                case ImageDestination.Photobucket:
                    imageUploader = new Photobucket(SettingsManager.ConfigUploaders.PhotobucketOAuthInfo, SettingsManager.ConfigUploaders.PhotobucketAccountInfo);
                    break;
                case ImageDestination.UploadScreenshot:
                    imageUploader = new UploadScreenshot(ApiKeys.UploadScreenshotKey);
                    break;
                case ImageDestination.Twitpic:
                    int indexTwitpic = SettingsManager.ConfigUploaders.TwitterSelectedAccount;

                    if (SettingsManager.ConfigUploaders.TwitterOAuthInfoList.IsValidIndex(indexTwitpic))
                    {
                        imageUploader = new TwitPicUploader(ApiKeys.TwitPicKey, SettingsManager.ConfigUploaders.TwitterOAuthInfoList[indexTwitpic])
                        {
                            TwitPicThumbnailMode = SettingsManager.ConfigUploaders.TwitPicThumbnailMode,
                            ShowFull = SettingsManager.ConfigUploaders.TwitPicShowFull
                        };
                    }
                    break;
                case ImageDestination.Twitsnaps:
                    int indexTwitsnaps = SettingsManager.ConfigUploaders.TwitterSelectedAccount;

                    if (SettingsManager.ConfigUploaders.TwitterOAuthInfoList.IsValidIndex(indexTwitsnaps))
                    {
                        imageUploader = new TwitSnapsUploader(ApiKeys.TwitsnapsKey, SettingsManager.ConfigUploaders.TwitterOAuthInfoList[indexTwitsnaps]);
                    }
                    break;
                case ImageDestination.yFrog:
                    YfrogOptions yFrogOptions = new YfrogOptions(ApiKeys.ImageShackKey);
                    yFrogOptions.Username = SettingsManager.ConfigUploaders.YFrogUsername;
                    yFrogOptions.Password = SettingsManager.ConfigUploaders.YFrogPassword;
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
                    PastebinSettings pastebinSettings = SettingsManager.ConfigUploaders.PastebinSettings;
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
                    string uploadPath = parser.Convert(Dropbox.TidyUploadPath(SettingsManager.ConfigUploaders.DropboxUploadPath));
                    fileUploader = new Dropbox(SettingsManager.ConfigUploaders.DropboxOAuthInfo, uploadPath, SettingsManager.ConfigUploaders.DropboxAccountInfo)
                    {
                        AutoCreateShareableLink = SettingsManager.ConfigUploaders.DropboxAutoCreateShareableLink
                    };
                    break;
                case FileDestination.RapidShare:
                    fileUploader = new RapidShare(SettingsManager.ConfigUploaders.RapidShareUsername, SettingsManager.ConfigUploaders.RapidSharePassword,
                        SettingsManager.ConfigUploaders.RapidShareFolderID);
                    break;
                case FileDestination.SendSpace:
                    fileUploader = new SendSpace(ApiKeys.SendSpaceKey);
                    switch (SettingsManager.ConfigUploaders.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(ApiKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(ApiKeys.SendSpaceKey, SettingsManager.ConfigUploaders.SendSpaceUsername, SettingsManager.ConfigUploaders.SendSpacePassword);
                            break;
                    }
                    break;
                case FileDestination.Minus:
                    fileUploader = new Minus(SettingsManager.ConfigUploaders.MinusConfig, new OAuthInfo(ApiKeys.MinusConsumerKey, ApiKeys.MinusConsumerSecret));
                    break;
                case FileDestination.Box:
                    fileUploader = new Box(ApiKeys.BoxKey)
                    {
                        AuthToken = SettingsManager.ConfigUploaders.BoxAuthToken,
                        FolderID = SettingsManager.ConfigUploaders.BoxFolderID,
                        Share = SettingsManager.ConfigUploaders.BoxShare
                    };
                    break;
                case FileDestination.CustomUploader:
                    if (SettingsManager.ConfigUploaders.CustomUploadersList.IsValidIndex(SettingsManager.ConfigUploaders.CustomUploaderSelected))
                    {
                        fileUploader = new CustomUploader(SettingsManager.ConfigUploaders.CustomUploadersList[SettingsManager.ConfigUploaders.CustomUploaderSelected]);
                    }
                    break;
                case FileDestination.FTP:
                    int idFtp = SettingsManager.ConfigUploaders.GetFtpIndex(Info.DataType);

                    if (SettingsManager.ConfigUploaders.FTPAccountList.IsValidIndex(idFtp))
                    {
                        FTPAccount account = SettingsManager.ConfigUploaders.FTPAccountList[idFtp];
                        if (account.Protocol == FTPProtocol.SFTP)
                            fileUploader = new SFTP(account);
                        else
                            fileUploader = new FTPUploader(account);
                    }
                    break;
                case FileDestination.SharedFolder:
                    int idLocalhost = SettingsManager.ConfigUploaders.GetLocalhostIndex(Info.DataType);
                    if (SettingsManager.ConfigUploaders.LocalhostAccountList.IsValidIndex(idLocalhost))
                    {
                        fileUploader = new SharedFolderUploader(SettingsManager.ConfigUploaders.LocalhostAccountList[idLocalhost]);
                    }
                    break;
                case FileDestination.Email:
                    using (EmailForm emailForm = new EmailForm(SettingsManager.ConfigUploaders.EmailRememberLastTo ? SettingsManager.ConfigUploaders.EmailLastTo : string.Empty,
                        SettingsManager.ConfigUploaders.EmailDefaultSubject, SettingsManager.ConfigUploaders.EmailDefaultBody))
                    {
                        if (emailForm.ShowDialog() == DialogResult.OK)
                        {
                            if (SettingsManager.ConfigUploaders.EmailRememberLastTo)
                            {
                                SettingsManager.ConfigUploaders.EmailLastTo = emailForm.ToEmail;
                            }

                            fileUploader = new Email
                            {
                                SmtpServer = SettingsManager.ConfigUploaders.EmailSmtpServer,
                                SmtpPort = SettingsManager.ConfigUploaders.EmailSmtpPort,
                                FromEmail = SettingsManager.ConfigUploaders.EmailFrom,
                                Password = SettingsManager.ConfigUploaders.EmailPassword,
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
                    urlShortener = new GoogleURLShortener(SettingsManager.ConfigUploaders.GoogleURLShortenerAccountType, ApiKeys.GoogleApiKey,
                        SettingsManager.ConfigUploaders.GoogleURLShortenerOAuthInfo);
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