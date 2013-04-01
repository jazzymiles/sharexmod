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

        private void timerScreencast_Tick(object sender, EventArgs e)
        {
            Program.IsRecordingScreencast = true;
            this.BackgroundImage = Resources.stop;

            switch (SettingsManager.ConfigUser.ScreencastEncoderType)
            {
                case EScreencastEncoderType.PromptUser:
                case EScreencastEncoderType.GraphicsInterchangeFormat:
                case EScreencastEncoderType.CommandLineEncoder:
                    ImgEncoderStart();
                    break;

                case EScreencastEncoderType.WindowsMediaVideo:
                case EScreencastEncoderType.ExpressionEncoderScreenCaptureCodec:
                    ExpressionEncoderStart();
                    break;

                default:
                    throw new Exception("Unsupported screencast filetype: " + SettingsManager.ConfigUser.ScreencastEncoderType.GetDescription());
            }

            timerScreencastDelay.Stop();
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

            ImgRecorder.DoWork += ImgRecorder_DoWork;
            ImgRecorder.RunWorkerCompleted += ImgRecorder_RunWorkerCompleted;

            ImgRecorder.RunWorkerAsync();
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

        private void AviEncode(bool writeCompressed)
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
                            img2 = CaptureHelpers.ResizeImage(img2, FixRes(width), heightLimit);
                        }

                        aviManager.AddFrame(img2, writeCompressed);
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

        private void CommandlineEncode()
        {
            AviEncode(false); // create uncompressed RAW video

            if (File.Exists(Screencast.FilePath) && File.Exists(SettingsManager.ConfigUser.ScreencastCmdEncoderPath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(SettingsManager.ConfigUser.ScreencastCmdEncoderPath);

                string fpCompressed = Path.ChangeExtension(Screencast.FilePath, SettingsManager.ConfigUser.ScreencastEncoderTargetFileExtension);
                string args = SettingsManager.ConfigUser.ScreencastEncoderArgs;
                args = Regex.Replace(SettingsManager.ConfigUser.ScreencastEncoderArgs, "%source%", "\"" + Screencast.FilePath + "\"");
                args = Regex.Replace(args, "%target%", "\"" + fpCompressed + "\"");
                psi.Arguments = args;
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
                    AviEncode(true);
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
            if (ImgCache != null)
                ImgCache.Dispose();

            try
            {
                if (File.Exists(SettingsManager.ScreenRecorderCacheFilePath))
                    File.Delete(SettingsManager.ScreenRecorderCacheFilePath);
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
