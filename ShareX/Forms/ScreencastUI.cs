using HelpersLib;
using HelpersLibMod;
using ScreenCapture;
using ShareX.HelperClasses;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ShareX.Forms
{
    public partial class ScreencastUI
    {
        public ImageData Screencast { get; set; }

        private AfterCaptureActivity act;

        private Rectangle CaptureRectangle;
        int pixelRound = 8;

        AVICache aviCache;
        private HardDiskCache hdCache;
        private int delay = 200;

        private BackgroundWorker ImgRecorder = new BackgroundWorker() { WorkerReportsProgress = true };
        private BackgroundWorker Encoder = new BackgroundWorker() { WorkerReportsProgress = true };

        public string ScreenRecorderCacheFilePath
        {
            get
            {
                return Path.Combine(Program.PersonalPath, "ScreenRecorder" + Screencast.Filename + ".cache");
            }
        }

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

            CaptureRectangle.Width = FixRes(CaptureRectangle.Width);
            CaptureRectangle.Height = FixRes(CaptureRectangle.Height);

            Encoder.DoWork += Encoder_DoWork;
            Encoder.ProgressChanged += Encoder_ProgressChanged;
            Encoder.RunWorkerCompleted += Encoder_RunWorkerCompleted;
        }

        private int FixRes(int round)
        {
            return ((int)Math.Ceiling(round / (double)pixelRound)) * pixelRound;
        }

        #region ImgRecord

        private void ImgRecord()
        {
            while (!Program.ScreencastCancellationPending)
            {
                Stopwatch timer = Stopwatch.StartNew();

                Screenshot.CaptureCursor = SettingsManager.ConfigCore.ShowCursor;
                Image img = Screenshot.CaptureRectangle(CaptureRectangle);

                switch (SettingsManager.ConfigUser.ScreencastEncoderType)
                {
                    case EScreencastEncoderType.PromptUser:
                    case EScreencastEncoderType.CommandLineEncoder:
                        aviCache.AddImageAsync(img);
                        break;
                    case EScreencastEncoderType.GraphicsInterchangeFormat:
                        hdCache.AddImageAsync(img);
                        break;
                }

                int sleepTime = delay - (int)timer.ElapsedMilliseconds;

                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
            }

            switch (SettingsManager.ConfigUser.ScreencastEncoderType)
            {
                case EScreencastEncoderType.PromptUser:
                case EScreencastEncoderType.CommandLineEncoder:
                    aviCache.Finish();
                    break;
                case EScreencastEncoderType.GraphicsInterchangeFormat:
                    hdCache.Finish();
                    break;
            }
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
            progress.Style = ProgressBarStyle.Continuous;
            Encoder.RunWorkerAsync();
        }

        private void ImgEncoderStart()
        {
            string fileExt = ".avi";
            switch (SettingsManager.ConfigUser.ScreencastEncoderType)
            {
                case EScreencastEncoderType.GraphicsInterchangeFormat:
                    fileExt = ".gif";
                    break;
            }

            Screencast.FilePath = Path.Combine(Program.ScreenshotsPath, Screencast.Filename + fileExt);

            switch (SettingsManager.ConfigUser.ScreencastEncoderType)
            {
                case EScreencastEncoderType.PromptUser:
                    aviCache = new AVICache(Screencast.FilePath, SettingsManager.ConfigUser.ScreencastFPS, this.CaptureRectangle.Size, true);
                    break;
                case EScreencastEncoderType.CommandLineEncoder:
                    aviCache = new AVICache(Screencast.FilePath, SettingsManager.ConfigUser.ScreencastFPS, this.CaptureRectangle.Size, false);
                    break;
                case EScreencastEncoderType.GraphicsInterchangeFormat:
                    hdCache = new HardDiskCache(ScreenRecorderCacheFilePath);
                    break;
            }

            ImgRecorder.DoWork += ImgRecorder_DoWork;
            ImgRecorder.RunWorkerCompleted += ImgRecorder_RunWorkerCompleted;

            ImgRecorder.RunWorkerAsync();
        }

        #endregion

        private void GifEncode()
        {
            using (GifCreator gifEncoder = new GifCreator(delay))
            {
                int total = hdCache.GetImageEnumerator().Count();
                int count = 0;
                foreach (Image img in hdCache.GetImageEnumerator())
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


        private void CommandlineEncode()
        {
            if (File.Exists(Screencast.FilePath) && File.Exists(SettingsManager.ConfigUser.ScreencastCmdEncoderPath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(SettingsManager.ConfigUser.ScreencastCmdEncoderPath);

                string fpCompressed = Path.ChangeExtension(Screencast.FilePath, SettingsManager.ConfigUser.ScreencastEncoderTargetFileExtension);
                string args = SettingsManager.ConfigUser.ScreencastEncoderArgs;
                args = Regex.Replace(SettingsManager.ConfigUser.ScreencastEncoderArgs, "%source%", "\"" + Screencast.FilePath + "\"");
                args = Regex.Replace(args, "%target%", "\"" + fpCompressed + "\"");
                psi.Arguments = args;
                psi.WindowStyle = ProcessWindowStyle.Minimized;
                p.StartInfo = psi;
                p.Start();
                p.WaitForExit();

                if (File.Exists(fpCompressed))
                {
                    try
                    {
                        File.Delete(Screencast.FilePath);
                    }
                    finally
                    {
                        Screencast.FilePath = fpCompressed;
                    }
                }
            }
        }

        private void Encoder_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (SettingsManager.ConfigUser.ScreencastEncoderType)
            {
                case EScreencastEncoderType.PromptUser:
                    break;

                case EScreencastEncoderType.GraphicsInterchangeFormat:
                    GifEncode();
                    break;

                case EScreencastEncoderType.CommandLineEncoder:
                    CommandlineEncode();
                    break;

                case EScreencastEncoderType.WindowsMediaVideo:
                case EScreencastEncoderType.ExpressionEncoderScreenCaptureCodec:
                    WMEncode();
                    break;

                default:
                    throw new Exception("Unsupported screencast filetype: " + SettingsManager.ConfigUser.ScreencastEncoderType.GetDescription());
            }
        }

        private void Encoder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }

        private void Encoder_RunWorkerCompleted_Img()
        {
            if (hdCache != null)
                hdCache.Dispose();

            try
            {
                if (File.Exists(ScreenRecorderCacheFilePath))
                    File.Delete(ScreenRecorderCacheFilePath);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteLine(ex.Message);
            }
        }

        private void Encoder_RunWorkerCompleted_Publish()
        {
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

        private void ScreencastStop_Common()
        {
            Program.IsRecordingScreencast = false;
            Program.ScreencastCancellationPending = true;
            progress.Visible = true;
            progress.Style = ProgressBarStyle.Marquee;
        }

        private void ScreencastUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Stop();
            }
        }

        private void ScreencastUI_MouseClick(object sender, MouseEventArgs e)
        {
            Stop();
        }

        private void ScreencastUI_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Stop();
        }
    }
}
