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
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.GraphicsHelper;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using ShareX.HelperClasses;
using ShareX.Properties;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.GUI;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using UploadersLibMod;

namespace ShareX
{
    public class Task : IDisposable
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void TaskEventHandler(Task task);

        public event TaskEventHandler UploadStarted;

        public event TaskEventHandler UploadPreparing;

        public event TaskEventHandler UploadProgressChanged;

        public event TaskEventHandler UploadCompleted;

        private Workflow Workflow = new Workflow();

        public UploadInfo Info { get; private set; }

        public TaskStatus Status { get; private set; }

        public bool IsWorking
        {
            get
            {
                return Status == TaskStatus.Preparing || Status == TaskStatus.Working;
            }
        }

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

        public void SetWorkflow(Workflow wf)
        {
            this.Workflow = wf;
            this.Info.Jobs = wf.Subtasks;
            this.Info.SetDestination(wf.Settings.DestConfig);
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
            TaskJob taskJob = TaskJob.FileUpload;
            switch (dataType)
            {
                case EDataType.Image:
                    taskJob = TaskJob.ImageUpload;
                    break;

                case EDataType.Text:
                    taskJob = TaskJob.TextUpload;
                    break;
            }
            Task task = new Task(dataType, taskJob);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FilePath = filePath;
            if (taskJob == TaskJob.ImageUpload)
                task.imageData = ImageData.GetNew(filePath);
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
                task.Info.FileName = new NameParser(NameParserType.FileName).Convert(SettingsManager.ConfigCore.NameFormatPatternOther) + (html ? ".html" : ".log");
                task.tempText = IndexersLib.QuickIndexer.Index(text, html, SettingsManager.ConfigUser.ConfigIndexer);
            }
            else
            {
                task.Info.FileName = new NameParser(NameParserType.FileName).Convert(SettingsManager.ConfigCore.NameFormatPatternOther) + ".txt";
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

        public static Task CreatePostToSocialNetworkingServiceTask(UploadResult result)
        {
            Task task = new Task(EDataType.URL, TaskJob.ShareURL);
            task.Info.UploadDestination = EDataType.Default;
            task.Info.Result = result;
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
                threadWorker.Start(ApartmentState.STA);
            }
        }

        public void Stop()
        {
            IsStopped = true;

            if (Status == TaskStatus.InQueue)
            {
                OnUploadCompleted();
            }
            else if (Status == TaskStatus.Working && uploader != null)
            {
                uploader.StopUpload();
            }
        }

        private void ThreadDoWork()
        {
            DoThreadJob();

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.Email))
            {
                UploadFile_Email(data);
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.SharedFolder))
            {
                UploadFile_SharedFolder(data);
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.Printer))
            {
                threadWorker.InvokeAsync(UploadFile_Print);
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.RemoteHost) && Info.Jobs.HasFlag(Subtask.UploadToRemoteHost))
            {
                if (SettingsManager.ConfigUploaders == null)
                    SettingsManager.UploaderSettingsResetEvent.WaitOne();

                Status = TaskStatus.Working;
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

                DoPostUploadTasks();
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

                if (Info.Jobs.HasFlagAny(Subtask.UploadToRemoteHost, Subtask.SaveToFile,
                    Subtask.SaveImageToFileWithDialog))
                {
                    imageData.PrepareImageAndFilename();

                    Info.FileName = imageData.Filename;

                    if (Info.Jobs.HasFlag(Subtask.SaveToFile))
                    {
                        Info.FilePath = imageData.WriteToFile(Program.ScreenshotsPath);
                    }

                    if (Info.Jobs.HasFlag(Subtask.SaveImageToFileWithDialog))
                    {
                        string fp = imageData.WriteToFile(Info.FolderPath);
                        if (string.IsNullOrEmpty(Info.FilePath))
                            Info.FilePath = fp;
                    }

                    if (Workflow.Subtasks.HasFlag(Subtask.RunExternalProgram))
                    {
                        var actions = Workflow.Settings.ExternalPrograms.Where(x => x.IsActive);
                        if (actions.Count() > 0)
                        {
                            if (data != null)
                                data.Dispose();

                            if (string.IsNullOrEmpty(Info.FilePath))
                                Info.FilePath = imageData.WriteToFile(Program.ScreenshotsPath);

                            foreach (ExternalProgram fileAction in actions)
                                fileAction.Run(Info.FilePath);

                            data = new FileStream(Info.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        }
                    }

                    threadWorker.InvokeAsync(ListViewManager.AddThumbnail);

                    if (data == null)
                        data = imageData.ImageStream;
                }
            }
            else if (Info.Job == TaskJob.TextUpload && !string.IsNullOrEmpty(tempText))
            {
                if (Info.Jobs.HasFlag(Subtask.Print))
                {
                    threadWorker.InvokeAsync(UploadFile_Print);
                }

                if (Info.Jobs.HasFlag(Subtask.SaveImageToFileWithDialog))
                {
                    threadWorker.InvokeAsync(SaveTextToFileWithDialog);
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(tempText);
                data = new MemoryStream(byteArray);
            }
        }

        private void DoPostUploadTasks()
        {
            // Shorten URL

            if ((Workflow.AfterUploadTasks.HasFlag(AfterUploadTasks.UseURLShortener) || Info.Job == TaskJob.ShortenURL) && Info.Result.URL.Length >= SettingsManager.ConfigCore.MaximumURLLength)
            {
                Info.Result.ShortenedURL = ShortenURL(Info.Result.URL);
            }

            // Share using Social Networking Services

            if (Workflow.AfterUploadTasks.HasFlag(AfterUploadTasks.ShareUsingSocialNetworkingService) && !string.IsNullOrEmpty(Info.Result.URL))
            {
                foreach (SocialNetworkingService sns in Workflow.Settings.DestConfig.SocialNetworkingServices)
                {
                    switch (sns)
                    {
                        case UploadersLib.SocialNetworkingService.Twitter:
                            OAuthInfo twitterOAuth = SettingsManager.ConfigUploaders.TwitterOAuthInfoList.ReturnIfValidIndex(SettingsManager.ConfigUploaders.TwitterSelectedAccount);

                            if (twitterOAuth != null)
                            {
                                using (TwitterMsg twitter = new TwitterMsg(twitterOAuth))
                                {
                                    twitter.Message = Info.Result.ToString();
                                    twitter.Config = SettingsManager.ConfigUploaders.TwitterClientConfig;
                                    twitter.ShowDialog();
                                }
                            }
                            break;
                    }
                }
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

        #region Upload Image

        private bool ImageEditOnKeyPress
        {
            get
            {
                return Control.IsKeyLocked(Keys.CapsLock) && imageData.ConfigUser.ImageEditorOnKeyPress == EImageEditorOnKeyLock.CapsLock ||
                 Control.IsKeyLocked(Keys.NumLock) && imageData.ConfigUser.ImageEditorOnKeyPress == EImageEditorOnKeyLock.NumLock ||
                 Control.IsKeyLocked(Keys.Scroll) && imageData.ConfigUser.ImageEditorOnKeyPress == EImageEditorOnKeyLock.ScrollLock;
            }
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
                imageData.Image = new HelpersLibWatermark.WatermarkEffects(SettingsManager.ConfigUser.ConfigWatermark).ApplyWatermark(imageData.Image);
            }

            if (Info.Jobs.HasFlag(Subtask.AnnotateImage) || ImageEditOnKeyPress)
            {
                EditImage(ref imageData);
            }

            if (Info.Jobs.HasFlag(Subtask.ShowImageEffectsStudio))
            {
                ImageEffectsGUI dlg = new ImageEffectsGUI(imageData.Image);
                dlg.ShowDialog();
                imageData.Image = dlg.GetImageForExport();
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.Clipboard) &&
                Info.Job == TaskJob.ImageUpload && imageData != null && Info.Jobs.HasFlag(Subtask.CopyImageToClipboard))
            {
                Clipboard.SetImage(imageData.Image);
            }

            if (Info.Jobs.HasFlag(Subtask.Print))
            {
                threadWorker.InvokeAsync(UploadFile_Print);
            }

            if (Info.Jobs.HasFlag(Subtask.SaveImageToFileWithDialog))
            {
                threadWorker.Invoke(SaveImageToFileWithDialog);
            }
        }

        private void EditImage(ref ImageData imageData_gse)
        {
            if (imageData_gse != null)
            {
                if (!Greenshot.IniFile.IniConfig.IsInited)
                    Greenshot.IniFile.IniConfig.Init();

                GreenshotPlugin.Core.CoreConfiguration conf = Greenshot.IniFile.IniConfig.GetIniSection<GreenshotPlugin.Core.CoreConfiguration>(); ;
                conf.OutputFileFilenamePattern = "${title}";
                conf.OutputFilePath = Program.ScreenshotsPath;

                Greenshot.Plugin.ICapture capture = new GreenshotPlugin.Core.Capture();
                capture.Image = imageData_gse.Image;
                capture.CaptureDetails.Filename = Path.Combine(Program.ScreenshotsPath, imageData_gse.Filename);
                capture.CaptureDetails.Title =
                    Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
                capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
                capture.CaptureDetails.AddMetaData("source", "file");

                var surface = new Greenshot.Drawing.Surface(capture);
                var editor = new Greenshot.ImageEditorForm(surface, true) { Icon = Resources.ShareX };

                editor.SetImagePath(capture.CaptureDetails.Filename);
                editor.Visible = false; // required before ShowDialog
                editor.ShowDialog();

                imageData_gse.Image = editor.GetImageForExport();
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

            ImageDestination imageDestination = Workflow.Settings.DestConfig.ImageUploaders[0];

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

                case ImageDestination.Picasa:
                    imageUploader = new Picasa(SettingsManager.ConfigUploaders.PicasaOAuthInfo);
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

        #endregion Upload Image

        public UploadResult UploadText(Stream stream)
        {
            TextUploader textUploader = null;
            TextDestination textDestination = Workflow.Settings.DestConfig.TextUploaders[0];

            switch (textDestination)
            {
                case TextDestination.Pastebin:
                    PastebinSettings pastebinSettings = SettingsManager.ConfigUploaders.PastebinSettings;
                    pastebinSettings.TextFormat = Workflow.Settings.DestConfig.TextFormat;
                    textUploader = new Pastebin(ApiKeys.PastebinKey, pastebinSettings);
                    break;

                case TextDestination.PastebinCA:
                    textUploader = new Pastebin_ca(ApiKeys.PastebinCaKey, new PastebinCaSettings()
                    {
                        TextFormat = Workflow.Settings.DestConfig.TextFormat
                    });
                    break;

                case TextDestination.Paste2:
                    textUploader = new Paste2(new Paste2Settings()
                    {
                        TextFormat = Workflow.Settings.DestConfig.TextFormat
                    });
                    break;

                case TextDestination.Slexy:
                    textUploader = new Slexy(new SlexySettings()
                    {
                        TextFormat = Workflow.Settings.DestConfig.TextFormat
                    });
                    break;

                case TextDestination.Pastee:
                    textUploader = new Pastee() { Lexer = Workflow.Settings.DestConfig.TextFormat };
                    break;
            }

            if (textUploader != null)
            {
                PrepareUploader(textUploader);
                return textUploader.UploadText(stream);
            }

            return null;
        }

        #region Upload File

        public UploadResult UploadFile(Stream stream)
        {
            FileUploader fileUploader = null;

            switch (Workflow.Settings.DestConfig.FileUploaders[0])
            {
                case FileDestination.Dropbox:
                    NameParser parser = new NameParser(NameParserType.FolderPath);
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

                case FileDestination.Ge_tt:
                    if (SettingsManager.ConfigUploaders.IsActive(FileDestination.Ge_tt))
                    {
                        fileUploader = new Ge_tt(ApiKeys.Ge_ttKey)
                        {
                            AccessToken = SettingsManager.ConfigUploaders.Ge_ttLogin.AccessToken
                        };
                    }
                    break;

                case FileDestination.CustomUploader:
                    if (SettingsManager.ConfigUploaders.CustomUploadersList.IsValidIndex(SettingsManager.ConfigUploaders.CustomUploaderSelected))
                    {
                        fileUploader = new CustomUploader(SettingsManager.ConfigUploaders.CustomUploadersList[SettingsManager.ConfigUploaders.CustomUploaderSelected]);
                    }
                    break;

                case FileDestination.FTP:

                    // fileUploader = get_FtpAccountByFileExtensionSupport();
                    if (fileUploader == null)
                    {
                        int idFtp = new UploadersConfigHelper(SettingsManager.ConfigUploaders).GetFtpIndex(Info.DataType);
                        if (SettingsManager.ConfigUploaders.FTPAccountList.IsValidIndex(idFtp))
                        {
                            FTPAccount account = SettingsManager.ConfigUploaders.FTPAccountList[idFtp];
                            if (account.Protocol == FTPProtocol.SFTP)
                                fileUploader = new SFTP(account);
                            else
                                fileUploader = new FTPUploader(account);
                        }
                    }
                    break;

                case FileDestination.SharedFolder:
                    UploadFile_SharedFolder(stream);
                    break;

                case FileDestination.Email:
                    UploadFile_Email(stream);
                    break;
            }

            if (fileUploader != null)
            {
                PrepareUploader(fileUploader);
                return fileUploader.Upload(stream, Info.FileName);
            }

            return null;
        }

        private UploadResult UploadFile_Email(Stream stream)
        {
            using (EmailForm emailForm = new EmailForm(SettingsManager.ConfigUploaders.EmailRememberLastTo ? SettingsManager.ConfigUploaders.EmailLastTo : string.Empty,
    SettingsManager.ConfigUploaders.EmailDefaultSubject, SettingsManager.ConfigUploaders.EmailDefaultBody))
            {
                if (emailForm.ShowDialog() == DialogResult.OK)
                {
                    if (SettingsManager.ConfigUploaders.EmailRememberLastTo)
                    {
                        SettingsManager.ConfigUploaders.EmailLastTo = emailForm.ToEmail;
                    }

                    FileUploader fileUploader = new Email
                      {
                          SmtpServer = SettingsManager.ConfigUploaders.EmailSmtpServer,
                          SmtpPort = SettingsManager.ConfigUploaders.EmailSmtpPort,
                          FromEmail = SettingsManager.ConfigUploaders.EmailFrom,
                          Password = SettingsManager.ConfigUploaders.EmailPassword,
                          ToEmail = emailForm.ToEmail,
                          Subject = emailForm.Subject,
                          Body = emailForm.Body
                      };
                    PrepareUploader(fileUploader);
                    return fileUploader.Upload(stream, Info.FileName);
                }
                else
                {
                    IsStopped = true;
                }
            }
            return null;
        }

        private void UploadFile_Print()
        {
            if (imageData != null && imageData.Image != null)
            {
                new PrintForm(imageData.ImageExported, SettingsManager.ConfigUser.PrintSettings).Show();
            }
            else if (Info.Job == TaskJob.TextUpload && !string.IsNullOrEmpty(tempText))
            {
                new PrintHelper(tempText) { Settings = SettingsManager.ConfigUser.PrintSettings }.Print();
            }
        }

        private UploadResult UploadFile_SharedFolder(Stream stream)
        {
            int idLocalhost = SettingsManager.ConfigUploaders.GetLocalhostIndex(Info.DataType);
            if (SettingsManager.ConfigUploaders.LocalhostAccountList.IsValidIndex(idLocalhost))
            {
                FileUploader fileUploader = new SharedFolderUploader(SettingsManager.ConfigUploaders.LocalhostAccountList[idLocalhost]);
                PrepareUploader(fileUploader);
                return fileUploader.Upload(stream, Info.FileName);
            }

            return null;
        }

        /*

        /// <summary>
        /// Returns FTP/SFTP Uploader based on file extensions it supports
        /// </summary>
        /// <returns></returns>
        private FileUploader get_FtpAccountByFileExtensionSupport()
        {
            foreach (FTPAccount account in SettingsManager.ConfigUploaders.FTPAccountList)
            {
                if (!string.IsNullOrEmpty(account.ExtensionsForTrigger))
                {
                    string[] extensions = account.ExtensionsForTrigger.Split(',');
                    foreach (string ext in extensions)
                    {
                        if ("." + ext.ToLower() == Path.GetExtension(Info.FileName).ToLower())
                        {
                            if (account.Protocol == FTPProtocol.SFTP)
                                return new SFTP(account);
                            else
                                return new FTPUploader(account);
                        }
                    }
                }
            }

            return null;
        }
        */

        #endregion Upload File

        public string ShortenURL(string url)
        {
            URLShortener urlShortener = null;

            if ((Workflow.Settings.DestConfig.LinkUploaders.Count == 0))
            {
                Workflow.Settings.DestConfig.LinkUploaders.Add(UploadManager.URLShortener);
                this.Info.SetDestination(Workflow.Settings.DestConfig); // update destination
            }

            switch (Workflow.Settings.DestConfig.LinkUploaders[0])
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
                UploadPreparing(this);
            }
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(this);
            }
        }

        private void OnUploadProgressChanged()
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(this);
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
                UploadCompleted(this);
            }

            Dispose();
        }

        public Image GetImageForExport()
        {
            return imageData.ImageExported;
        }

        public void Dispose()
        {
            if (data != null) data.Dispose();
            if (imageData != null) imageData.Dispose();
        }
    }
}