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

using Greenshot.Configuration;
using Greenshot.Core;
using Greenshot.IniFile;
using GreenshotPlugin.Core;
using HelpersLib;
using HelpersLib.GraphicsHelper;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using ShareX.HelperClasses;
using ShareX.Properties;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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
    public class UploadTask : IDisposable
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void TaskEventHandler(UploadTask task);

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

        private Stream GetDataCopy(Stream data)
        {
            long dataPos = 0;
            if (data != null && data.CanSeek)
            {
                dataPos = data.Position;
                data.Position = 0;
            }

            Stream dataCopy = new MemoryStream();
            data.CopyStreamTo(dataCopy);

            if (data != null && data.CanSeek)
                data.Position = dataPos; // restore data position for the src data

            if (dataCopy != null && data.CanSeek)
                dataCopy.Position = 0;

            return dataCopy;
        }

        #region Constructors

        private UploadTask(EDataType dataType, TaskJob job)
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
            this.Info.Subtasks = wf.Subtasks;
            this.Info.SetDestination(wf.Settings.DestConfig);
        }

        public static UploadTask CreateDataUploaderTask(EDataType dataType, Stream stream, string filePath, EDataType destination = EDataType.Default)
        {
            UploadTask task = new UploadTask(dataType, TaskJob.DataUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FileName = Path.GetFileName(filePath);
            task.Info.FilePath = filePath;
            task.data = stream;
            return task;
        }

        // string filePath -> FileStream data
        public static UploadTask CreateFileUploaderTask(string filePath, EDataType destination = EDataType.Default)
        {
            EDataType dataType = Helpers.FindDataType(filePath);

            TaskJob taskJob = TaskJob.FileUpload;

            if (SettingsManager.ConfigCore.FileUploadImageProcess)
            {
                switch (dataType)
                {
                    case EDataType.Image:
                        taskJob = TaskJob.ImageUpload;
                        break;

                    case EDataType.Text:
                        taskJob = TaskJob.TextUpload;
                        break;
                }
            }

            UploadTask task = new UploadTask(dataType, taskJob);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FilePath = filePath;
            if (taskJob == TaskJob.ImageUpload)
                task.imageData = ImageData.GetNew(filePath);

            try
            {
                task.data = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                log.Error("Error while creating FileUploader task", ex);
                return null;
            }

            return task;
        }

        // Image image -> MemoryStream data (in thread)
        public static UploadTask CreateImageUploaderTask(ImageData imageData, EDataType destination = EDataType.Default)
        {
            UploadTask task = new UploadTask(EDataType.Image, TaskJob.ImageUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;
            task.Info.FileName = imageData.Filename;
            task.imageData = imageData;
            return task;
        }

        // string text -> MemoryStream data (in thread)
        public static UploadTask CreateTextUploaderTask(string text, EDataType destination = EDataType.Default)
        {
            UploadTask task = new UploadTask(EDataType.Text, TaskJob.TextUpload);
            if (destination != EDataType.Default) task.Info.UploadDestination = destination;

            if (SettingsManager.ConfigCore.IndexFolderWhenPossible && Directory.Exists(text))
            {
                bool html = destination == EDataType.File;
                task.Info.FileName = new NameParser(NameParserType.FileName).Parse(SettingsManager.ConfigCore.NameFormatPatternOther) + (html ? ".html" : ".log");
                task.tempText = IndexersLib.QuickIndexer.Index(text, html, SettingsManager.ConfigUser.ConfigIndexer);
            }
            else
            {
                task.Info.FileName = new NameParser(NameParserType.FileName).Parse(SettingsManager.ConfigCore.NameFormatPatternOther) + ".txt";
                task.tempText = text;
            }
            return task;
        }

        public static UploadTask CreateURLShortenerTask(string url)
        {
            UploadTask task = new UploadTask(EDataType.URL, TaskJob.ShortenURL);
            task.Info.FileName = url;
            task.Info.Result.URL = url;
            return task;
        }

        public static UploadTask CreatePostToSocialNetworkingServiceTask(UploadResult result)
        {
            UploadTask task = new UploadTask(EDataType.URL, TaskJob.ShareURL);
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
                new Thread(() => UploadFile_Email(GetDataCopy(data))).Start();
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.SharedFolder))
            {
                threadWorker.InvokeAsync(() => UploadFile_SharedFolder(GetDataCopy(data)));
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.Printer))
            {
                threadWorker.InvokeAsync(UploadFile_Print);
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.RemoteHost) && Info.Subtasks.HasFlag(Subtask.UploadToRemoteHost))
            {
                if (SettingsManager.ConfigUploaders == null)
                    SettingsManager.UploaderSettingsResetEvent.WaitOne();

                Status = TaskStatus.Working;
                Info.Status = "Uploading";
                Info.StartTime = DateTime.UtcNow;
                threadWorker.InvokeAsync(OnUploadStarted);

                try
                {
                    switch (Info.UploadDestination)
                    {
                        case EDataType.Image:
                            Info.Result = UploadImage(data);
                            break;

                        case EDataType.File:
                            Info.Result = UploadFile(data);
                            break;

                        case EDataType.Text:
                            Info.Result = UploadText(data, Info.FileName);
                            break;
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

            if (!IsStopped && Info.Result != null && !Info.Result.IsError)
            {
                if (Info.Result.IsURLExpected && string.IsNullOrEmpty(Info.Result.URL))
                {
                    Info.Result.Errors.Add("URL is empty. Press F2 to review destinations configuration.");
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

                if (Info.Subtasks.HasFlagAny(Subtask.UploadToRemoteHost, Subtask.SaveToFile,
                    Subtask.SaveImageToFileWithDialog))
                {
                    imageData.PrepareImageAndFilename();

                    Info.FileName = imageData.Filename;

                    if (Info.Subtasks.HasFlag(Subtask.SaveToFile))
                    {
                        Info.FilePath = imageData.WriteToFile(Program.ScreenshotsPath);
                    }

                    if (Info.Subtasks.HasFlag(Subtask.SaveImageToFileWithDialog))
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

                    if (data == null)
                        data = imageData.ImageStream;
                }
            }
            else if (Info.Job == TaskJob.TextUpload && !string.IsNullOrEmpty(tempText))
            {
                if (Info.Subtasks.HasFlag(Subtask.Print))
                {
                    threadWorker.InvokeAsync(UploadFile_Print);
                }

                if (Info.Subtasks.HasFlag(Subtask.SaveImageToFileWithDialog))
                {
                    threadWorker.InvokeAsync(SaveTextToFileWithDialog);
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(tempText);
                data = new MemoryStream(byteArray);
            }

            if (data != null && data.CanSeek)
            {
                data.Position = 0;
            }

            if (Info.Job != TaskJob.ShareURL)
            {
                threadWorker.InvokeAsync(ListViewManager.AddThumbnail);
            }
        }

        private void DoPostUploadTasks()
        {
            Info.Result.LocalFilePath = Info.FilePath;

            // Shorten URL

            if (Info.Result.IsURLExpected &&
                (Workflow.AfterUploadTasks.HasFlag(AfterUploadTasks.UseURLShortener) &&
                !string.IsNullOrEmpty(Info.Result.URL) &&
                Info.Result.URL.Length >= SettingsManager.ConfigCore.MaximumURLLength) ||
                Info.Job == TaskJob.ShortenURL ||
                Info.Job == TaskJob.ShareURL && string.IsNullOrEmpty(Info.Result.ShortenedURL))
            {
                UploadResult result = ShortenURL(Info.Result.URL);

                if (result != null)
                {
                    Info.Result.ShortenedURL = result.ShortenedURL;
                }
            }

            // Send an email
            new Thread(() => UploadFile_Email(Info.Result)).Start();

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
            if (Info.Subtasks.HasFlag(Subtask.AnnotateImageAddTornEffect))
            {
                InitGreenshot();

                int toothHeight = 12, horizontalToothRange = 20, verticalToothRange = 20;

                imageData.Image = GreenshotPlugin.Core.ImageHelper.CreateTornEdge(new Bitmap(imageData.Image),
                    toothHeight, horizontalToothRange, verticalToothRange);
            }

            if (Info.Subtasks.HasFlag(Subtask.AnnotateImageAddShadowBorder))
            {
                InitGreenshot();
                Point shadowOffset = new Point();
                imageData.Image = ImageHelper.ApplyEffect(imageData.Image, new DropShadowEffect()
                {
                    Darkness = SettingsManager.ConfigUser.ShadowDarkness,
                    ShadowSize = SettingsManager.ConfigUser.ShadowSize,
                    ShadowOffset = SettingsManager.ConfigUser.ShadowOffset
                }, out shadowOffset);
            }

            if (Info.Subtasks.HasFlag(Subtask.AddWatermark))
            {
                imageData.Image = new HelpersLibWatermark.WatermarkEffects(SettingsManager.ConfigUser.ConfigWatermark).ApplyWatermark(imageData.Image);
            }

            if (Info.Subtasks.HasFlag(Subtask.AnnotateImage) || ImageEditOnKeyPress)
            {
                EditImage(ref imageData);
            }

            if (Info.Subtasks.HasFlag(Subtask.ShowImageEffectsStudio))
            {
                ImageEffectsGUI dlg = new ImageEffectsGUI(imageData.Image);
                dlg.ShowDialog();
                imageData.Image = dlg.GetImageForExport();
            }

            if (SettingsManager.ConfigCore.Outputs.HasFlag(HelpersLibMod.OutputEnum.Clipboard) &&
                Info.Job == TaskJob.ImageUpload && imageData != null && Info.Subtasks.HasFlag(Subtask.CopyImageToClipboard))
            {
                Clipboard.SetImage(imageData.Image);
            }

            if (Info.Subtasks.HasFlag(Subtask.Print))
            {
                threadWorker.InvokeAsync(UploadFile_Print);
            }

            if (Info.Subtasks.HasFlag(Subtask.SaveImageToFileWithDialog))
            {
                threadWorker.Invoke(SaveImageToFileWithDialog);
            }
        }

        private void InitGreenshot()
        {
            if (!Greenshot.IniFile.IniConfig.isInitialized)
            {
                Greenshot.IniFile.IniConfig.Init(Program.PersonalPath);
                Greenshot.IniFile.IniConfig.AllowSave = true;
            }
        }

        private void EditImage(ref ImageData imageData_gse)
        {
            if (imageData_gse != null)
            {
                InitGreenshot();
                GreenshotPlugin.Core.CoreConfiguration conf = Greenshot.IniFile.IniConfig.GetIniSection<GreenshotPlugin.Core.CoreConfiguration>(); ;
                conf.OutputFileFilenamePattern = "${title}";
                conf.OutputFilePath = Program.ScreenshotsPath;

                EditorConfiguration editorConfiguration = IniConfig.GetIniSection<EditorConfiguration>();
                editorConfiguration.SuppressSaveDialogAtClose = true;

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
            try
            {
                return UploadImage(stream, Workflow.Settings.DestConfig.ImageUploaders[0]);
            }
            catch (Exception)
            {
                if (Workflow.Settings.DestConfig.ImageUploaders2.Count > 0)
                    return UploadImage(stream, Workflow.Settings.DestConfig.ImageUploaders2[0]);
            }

            return null;
        }

        private UploadResult UploadImage(Stream stream, ImageDestination destination)
        {
            ImageUploader imageUploader = null;

            switch (destination)
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
                    imageUploader = new Imgur_v3(SettingsManager.ConfigUploaders.ImgurOAuth2Info)
                    {
                        UploadMethod = SettingsManager.ConfigUploaders.ImgurAccountType,
                        ThumbnailType = SettingsManager.ConfigUploaders.ImgurThumbnailType
                    };
                    break;

                case ImageDestination.Picasa:
                    imageUploader = new Picasa(SettingsManager.ConfigUploaders.PicasaOAuth2Info);
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

                case ImageDestination.CustomImageUploader:
                    if (SettingsManager.ConfigUploaders.CustomUploadersList.IsValidIndex(SettingsManager.ConfigUploaders.CustomImageUploaderSelected))
                    {
                        imageUploader = new CustomImageUploader(SettingsManager.ConfigUploaders.CustomUploadersList[SettingsManager.ConfigUploaders.CustomImageUploaderSelected]);
                    }
                    break;

                default:
                    throw new Exception("Unsupported image uploader: " + destination.GetDescription());
            }

            if (imageUploader != null)
            {
                PrepareUploader(imageUploader);
                return imageUploader.Upload(stream, Info.FileName);
            }

            return null;
        }

        #endregion Upload Image

        #region Upload Text

        public UploadResult UploadText(Stream stream, string fileName)
        {
            try
            {
                return UploadText(stream, fileName, Workflow.Settings.DestConfig.TextUploaders[0]);
            }
            catch (Exception)
            {
                if (Workflow.Settings.DestConfig.TextUploaders2.Count > 0)
                    return UploadText(stream, fileName, Workflow.Settings.DestConfig.TextUploaders2[0]);
            }

            return null;
        }

        private UploadResult UploadText(Stream stream, string fileName, TextDestination destination)
        {
            TextUploader textUploader = null;

            switch (destination)
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

                case TextDestination.Paste_ee:
                    textUploader = new Paste_ee(SettingsManager.ConfigUploaders.Paste_eeUserAPIKey);
                    break;

                case TextDestination.CustomTextUploader:
                    if (SettingsManager.ConfigUploaders.CustomUploadersList.IsValidIndex(SettingsManager.ConfigUploaders.CustomTextUploaderSelected))
                    {
                        textUploader = new CustomTextUploader(SettingsManager.ConfigUploaders.CustomUploadersList[SettingsManager.ConfigUploaders.CustomTextUploaderSelected]);
                    }
                    break;

                default:
                    throw new Exception("Unsupported text uploader: " + destination.GetDescription());
            }

            if (textUploader != null)
            {
                PrepareUploader(textUploader);
                return textUploader.UploadText(stream, fileName);
            }

            return null;
        }

        #endregion Upload Text

        #region Upload File

        public UploadResult UploadFile(Stream stream)
        {
            try
            {
                return UploadFile(stream, Workflow.Settings.DestConfig.FileUploaders[0]);
            }
            catch (Exception)
            {
                if (Workflow.Settings.DestConfig.FileUploaders2.Count > 0)
                    return UploadFile(stream, Workflow.Settings.DestConfig.FileUploaders2[0]);
            }

            return null;
        }

        private UploadResult UploadFile(Stream stream, FileDestination destination)
        {
            FileUploader fileUploader = null;

            switch (destination)
            {
                case FileDestination.Dropbox:
                    NameParser parser = new NameParser(NameParserType.FolderPath);
                    string uploadPath = parser.Parse(Dropbox.TidyUploadPath(SettingsManager.ConfigUploaders.DropboxUploadPath));
                    fileUploader = new Dropbox(SettingsManager.ConfigUploaders.DropboxOAuthInfo, uploadPath, SettingsManager.ConfigUploaders.DropboxAccountInfo)
                    {
                        AutoCreateShareableLink = SettingsManager.ConfigUploaders.DropboxAutoCreateShareableLink
                    };
                    break;

                case FileDestination.GoogleDrive:
                    fileUploader = new GoogleDrive(SettingsManager.ConfigUploaders.GoogleDriveOAuth2Info);
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

                case FileDestination.Localhostr:
                    fileUploader = new Localhostr(SettingsManager.ConfigUploaders.LocalhostrEmail, SettingsManager.ConfigUploaders.LocalhostrPassword)
                    {
                        DirectURL = SettingsManager.ConfigUploaders.LocalhostrDirectURL
                    };
                    break;

                case FileDestination.CustomFileUploader:
                    if (SettingsManager.ConfigUploaders.CustomUploadersList.IsValidIndex(SettingsManager.ConfigUploaders.CustomFileUploaderSelected))
                    {
                        fileUploader = new CustomFileUploader(SettingsManager.ConfigUploaders.CustomUploadersList[SettingsManager.ConfigUploaders.CustomFileUploaderSelected]);
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

                default:
                    throw new Exception("Unsupported file uploader: " + destination.GetDescription());
            }

            if (fileUploader != null)
            {
                PrepareUploader(fileUploader);
                return fileUploader.Upload(stream, Info.FileName);
            }

            return null;
        }

        /// <summary>
        /// Looks for exe and compresses to 7z
        /// </summary>
        private string PrepareEmailAttachment(ref Stream attachment, string fileName)
        {
            string[] badExtArray = new string[] { ".ade", ".adp", ".bat", ".chm", ".cmd", 
                                             ".com", ".cpl", ".exe", ".hta", ".ins", 
                                             ".isp", ".jse", ".lib", ".mde", ".msc", 
                                             ".msp", ".mst", ".pif", ".scr", ".sct", 
                                             ".shb", ".sys", ".vb", ".vbe", ".vbs", 
                                             ".vxd", ".wsc", ".wsf", ".wsh" 
                                           };

            string ext = Path.GetExtension(fileName).ToLower();
            string result = badExtArray.SingleOrDefault(badExt => badExt.Contains(ext));

            if (!string.IsNullOrEmpty(result))
            {
                fileName = Path.ChangeExtension(fileName, ".7z");
                attachment = ZipHelper.CompressFileLZMA(attachment);
            }

            return fileName;
        }

        private void UploadFile_Email(UploadResult result)
        {
            if (result != null && !string.IsNullOrEmpty(AddressBookHelper.CurrentRecipient))
            {
                Email email = new Email
                {
                    SmtpServer = SettingsManager.ConfigUploaders.EmailSmtpServer,
                    SmtpPort = SettingsManager.ConfigUploaders.EmailSmtpPort,
                    FromEmail = SettingsManager.ConfigUploaders.EmailFrom,
                    Password = SettingsManager.ConfigUploaders.EmailPassword,
                    ToEmail = AddressBookHelper.CurrentRecipient,
                    Subject = SettingsManager.ConfigUploaders.EmailDefaultSubject,
                    Body = result.ToSummaryString()
                };

                PrepareUploader(email);
                email.Send(email.ToEmail, email.Subject, email.Body);
            }
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
                        AddressBookHelper.AddEmail(emailForm.ToEmail);
                    }

                    Email emailUploader = new Email
                      {
                          SmtpServer = SettingsManager.ConfigUploaders.EmailSmtpServer,
                          SmtpPort = SettingsManager.ConfigUploaders.EmailSmtpPort,
                          FromEmail = SettingsManager.ConfigUploaders.EmailFrom,
                          Password = SettingsManager.ConfigUploaders.EmailPassword,
                          ToEmail = emailForm.ToEmail,
                          Subject = emailForm.Subject,
                          Body = emailForm.Body
                      };

                    string fileName = PrepareEmailAttachment(ref stream, Info.FileName);
                    PrepareUploader(emailUploader);
                    return emailUploader.Upload(stream, fileName);
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

        public UploadResult ShortenURL(string url)
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
                    urlShortener = new GoogleURLShortener(SettingsManager.ConfigUploaders.GoogleURLShortenerAccountType, ApiKeys.GoogleAPIKey,
                      SettingsManager.ConfigUploaders.GoogleURLShortenerOAuth2Info);
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

                case UrlShortenerType.CustomURLShortener:
                    if (SettingsManager.ConfigUploaders.CustomUploadersList.IsValidIndex(SettingsManager.ConfigUploaders.CustomURLShortenerSelected))
                    {
                        urlShortener = new CustomURLShortener(SettingsManager.ConfigUploaders.CustomUploadersList[SettingsManager.ConfigUploaders.CustomURLShortenerSelected]);
                    }
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
            if (imageData != null)
                return imageData.ImageExported;

            return null;
        }

        public void Dispose()
        {
            if (data != null) data.Dispose();
            if (imageData != null) imageData.Dispose();
        }
    }
}