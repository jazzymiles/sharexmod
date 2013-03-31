using HelpersLib;
using HelpersLibMod;
using Microsoft.Expression.Encoder.Profiles;
using ScreenCapture;
using ShareX.HelperClasses;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareX.Forms
{
    public partial class ScreencastUI : Form
    {
        public ImageData Screencast { get; set; }

        private AfterCaptureActivity act;

        private Rectangle CaptureRectangle;

        private ScreenRecorderCache ImgCache;
        private int delay = 200;

        // Avi settings
        private int heightLimit = 720;

        private BackgroundWorker ImgRecorder = new BackgroundWorker() { WorkerReportsProgress = true };
        private BackgroundWorker Encoder = new BackgroundWorker() { WorkerReportsProgress = true };

        public ScreencastUI(ImageData imagedata, AfterCaptureActivity act)
        {
            InitializeComponent();
            this.Text = Application.ProductName + " - Screencast";
            this.Location = new Point(0, 0);

            this.act = act;

            if (SettingsManager.ConfigUser.ScreencastFPS == 0)
                SettingsManager.ConfigUser.ScreencastFPS = 5;
            this.delay = 1000 / SettingsManager.ConfigUser.ScreencastFPS;

            Screencast = imagedata;
            CaptureRectangle = Screencast.CaptureRectangle;

            int pixelRound = 16;
            CaptureRectangle.Width = Math.Max(RoundOff(CaptureRectangle.Width, pixelRound), pixelRound);
            CaptureRectangle.Height = Math.Max(RoundOff(CaptureRectangle.Height, pixelRound), pixelRound);

            Encoder.DoWork += Encoder_DoWork;
            Encoder.ProgressChanged += Encoder_ProgressChanged;
            Encoder.RunWorkerCompleted += Encoder_RunWorkerCompleted;
        }

        private int RoundOff(int round, double roundOffTo)
        {
            return ((int)Math.Round(round / roundOffTo)) * (int)roundOffTo;
        }

        private void timerScreencast_Tick(object sender, EventArgs e)
        {
            Program.IsRecordingScreencast = true;
            this.BackgroundImage = Resources.stop;

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                case EScreencastFileType.gif:
                    ImgEncoderStart();
                    break;

                case EScreencastFileType.wmv:
                case EScreencastFileType.xesc:
                    ExpressionEncoderStart();
                    break;

                default:
                    throw new Exception("Unsupported screencast filetype: " + SettingsManager.ConfigUser.ScreencastFileType.GetDescription());
            }

            timerScreencast.Stop();
        }

        private void ImgEncoderStart()
        {
            string fileExt = ".gif";
            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                    fileExt = ".avi";
                    break;

                case EScreencastFileType.gif:
                    fileExt = ".gif";
                    break;
            }

            Screencast.FilePath = Path.Combine(Program.ScreenshotsPath, Screencast.Filename + fileExt);

            ImgRecorder.DoWork += ImgRecorder_DoWork;
            ImgRecorder.RunWorkerCompleted += ImgRecorder_RunWorkerCompleted;

            ImgRecorder.RunWorkerAsync();
        }

        /// <summary>
        /// Gif recording has to be in a thread to have response in ScreencastUI for stopping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgRecorder_DoWork(object sender, DoWorkEventArgs e)
        {
            ImgRecord();
        }

        private void ImgRecorder_ReportProgress(int count, int total)
        {
            int progress = (int)((double)count / (double)total * 100);
            Encoder.ReportProgress(progress);
        }

        private void ImgRecorder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Encoder.RunWorkerAsync();
        }
        
        private ScreenRecorderCache ImgRecord()
        {
            using (ImgCache = new ScreenRecorderCache(SettingsManager.ScreenRecorderCacheFilePath))
            {
                while (!Program.ScreencastCancellationPending)
                {
                    Stopwatch timer = Stopwatch.StartNew();

                    Screenshot.CaptureCursor = SettingsManager.ConfigCore.ShowCursor;
                    Image img = Screenshot.CaptureRectangle(CaptureRectangle);

                    ImgCache.AddImageAsync(img);

                    int sleepTime = delay - (int)timer.ElapsedMilliseconds;

                    if (sleepTime > 0)
                    {
                        Thread.Sleep(sleepTime);
                    }
                    else
                    {
                        Debug.WriteLine("FPS drop: " + sleepTime);
                    }
                }
                ImgCache.Finish();
            }

            return ImgCache;
        }

        private void GifEncode()
        {
            using (GifCreator gifEncoder = new GifCreator(delay))
            {
                int total = ImgCache.GetImageEnumerator().Count();
                int count = 0;
                foreach (Image img in ImgCache.GetImageEnumerator())
                {
                    using (img)
                    {
                        gifEncoder.AddFrame(img, SettingsManager.ConfigUser.ImageGIFQuality);
                        count++;
                        ImgRecorder_ReportProgress(count, total);
                    }
                }

                gifEncoder.Finish();
                gifEncoder.Save(Screencast.FilePath);
            }
        }

        private void AviEncode()
        {
            using (AVIManager aviManager = new AVIManager(Screencast.FilePath, SettingsManager.ConfigUser.ScreencastFPS))
            {
                int total = ImgCache.GetImageEnumerator().Count();
                int count = 0;
                foreach (Image img in ImgCache.GetImageEnumerator())
                {
                    Image img2 = img;

                    try
                    {
                        if (heightLimit > 0 && CaptureRectangle.Height > heightLimit)
                        {
                            int width = (int)((float)heightLimit / CaptureRectangle.Height * CaptureRectangle.Width);
                            img2 = CaptureHelpers.ResizeImage(img2, width, heightLimit);
                        }

                        aviManager.AddFrame(img2);
                        count++;
                        ImgRecorder_ReportProgress(count, total);
                    }
                    finally
                    {
                        if (img2 != null) img2.Dispose();
                    }
                }
            }
        }

        private void ScreencastStop()
        {
            Program.ScreencastCancellationPending = true;

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                case EScreencastFileType.gif:
                    break;

                case EScreencastFileType.wmv:
                case EScreencastFileType.xesc:
                    this.XescScreenCaptureJob.Stop();
                    if (!Encoder.IsBusy) // XescTimer_Tick can fire this twice
                        Encoder.RunWorkerAsync();
                    break;

                default:
                    throw new Exception("Unsupported screencast filetype: " + SettingsManager.ConfigUser.ScreencastFileType.GetDescription());
            }

            progress.Visible = true;

            Program.IsRecordingScreencast = false;
        }

        private void Encoder_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                    AviEncode();
                    break;

                case EScreencastFileType.gif:
                    GifEncode();
                    break;

                case EScreencastFileType.wmv:
                case EScreencastFileType.xesc:
                    WMEncode();
                    break;

                default:
                    throw new Exception("Unsupported screencast filetype: " + SettingsManager.ConfigUser.ScreencastFileType.GetDescription());
            }
        }

        private void Encoder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }

        private void Encoder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Program.ScreencastCancellationPending = false;

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                case EScreencastFileType.gif:
                    if (ImgCache != null)
                        ImgCache.Dispose();

                    if (File.Exists(SettingsManager.ScreenRecorderCacheFilePath))
                        File.Delete(SettingsManager.ScreenRecorderCacheFilePath);
                    break;

                case EScreencastFileType.wmv:
                    Screencast.FilePath = Path.ChangeExtension(XescScreenCaptureJob.ScreenCaptureFileName, "wmv");

                    if (File.Exists(Screencast.FilePath))
                        File.Delete(XescScreenCaptureJob.ScreenCaptureFileName); // if wmv exists then delete xesc
                    break;

                case EScreencastFileType.xesc:
                    Screencast.FilePath = XescScreenCaptureJob.ScreenCaptureFileName;
                    break;
            }

            if (act.Workflow.Subtasks.HasFlag(Subtask.UploadToRemoteHost))
            {
                UploadTask task = UploadTask.CreateFileUploaderTask(Screencast.FilePath, EDataType.File);
                if (task != null)
                {
                    task.SetWorkflow(act.Workflow);
                    TaskManager.Start(task);
                }
            }
            this.Close();
        }

        private void ScreencastUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ScreencastStop();
            }
        }

        private void ScreencastUI_MouseClick(object sender, MouseEventArgs e)
        {
            ScreencastStop();
        }

        private void ScreencastUI_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ScreencastStop();
        }
    }
}