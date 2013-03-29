using HelpersLib;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Expression.Encoder.ScreenCapture;
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

        private AfterCaptureActivity _act;

        private Rectangle CaptureRectangle;

        private int delay = 200;
        private ScreenRecorderCache GifCache;

        private BackgroundWorker GifRecorder = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        private ScreenCaptureJob XescScreenCaptureJob = new ScreenCaptureJob();

        private BackgroundWorker Encoder = new BackgroundWorker() { WorkerReportsProgress = true };

        public ScreencastUI(ImageData imagedata, AfterCaptureActivity act)
        {
            InitializeComponent();
            _act = act;

            this.Text = Application.ProductName + " - Screencast";
            this.Location = new Point(0, 0);

            Screencast = imagedata;
            CaptureRectangle = Screencast.CaptureRectangle;
            CaptureRectangle.Width = Math.Max(RoundOff(CaptureRectangle.Width, 4.0), 4);
            CaptureRectangle.Height = Math.Max(RoundOff(CaptureRectangle.Height, 4.0), 4);

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
            this.BackgroundImage = Resources.stop;

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.gif:
                    ScreencastGifEncoderStart();
                    break;

                default:
                    ScreencastExpressionEncoderStart();
                    break;
            }

            timerScreencast.Stop();
        }

        #region Gif

        private void ScreencastGifEncoderStart()
        {
            Screencast.FilePath = Path.Combine(Program.ScreenshotsPath, Screencast.Filename + ".gif");

            GifRecorder.DoWork += GifRecorder_DoWork;
            GifRecorder.RunWorkerCompleted += GifRecorder_RunWorkerCompleted;

            GifRecorder.RunWorkerAsync();
        }

        private void GifScreenCaptureJobStop()
        {
            GifRecorder.CancelAsync();
        }

        /// <summary>
        /// Gif recording has to be in a thread to have response in ScreencastUI for stopping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GifRecorder_DoWork(object sender, DoWorkEventArgs e)
        {
            GifRecord();
        }

        private ScreenRecorderCache GifRecord()
        {
            using (GifCache = new ScreenRecorderCache(SettingsManager.ScreenRecorderCacheFilePath))
            {
                while (!GifRecorder.CancellationPending)
                {
                    Stopwatch timer = Stopwatch.StartNew();

                    Screenshot.CaptureCursor = SettingsManager.ConfigCore.ShowCursor;
                    Image img = Screenshot.CaptureRectangle(CaptureRectangle);

                    GifCache.AddImageAsync(img);

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
                GifCache.Finish();
            }

            return GifCache;
        }

        private void GifRecorder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Encoder.RunWorkerAsync();
        }

        #endregion Gif

        private void GifEncode()
        {
            using (GifCreator gifEncoder = new GifCreator(200))
            {
                int total = GifCache.GetImageEnumerator().Count();
                int count = 0;
                int progress = 0;
                foreach (Image img in GifCache.GetImageEnumerator())
                {
                    using (img)
                    {
                        gifEncoder.AddFrame(img, SettingsManager.ConfigUser.ImageGIFQuality);
                        count++;
                        progress = (int)((double)count / (double)total * 100);

                        Encoder.ReportProgress(progress);
                    }
                }

                gifEncoder.Finish();
                gifEncoder.Save(Screencast.FilePath);
            }
        }

        #region Expression Encoder

        private void ScreencastExpressionEncoderStart()
        {
            ScreenCaptureVideoProfile video = new ScreenCaptureVideoProfile();

            XescScreenCaptureJob.CaptureFollowCursor = SettingsManager.ConfigUser.FollowMouseCursor;
            XescScreenCaptureJob.CaptureRectangle = CaptureRectangle;
            XescScreenCaptureJob.OutputPath = Program.ScreenshotsPath;
            XescScreenCaptureJob.Start();
        }

        private void WMEncode()
        {
            // Create the media item and validates it.
            MediaItem mediaItem;
            try
            {
                mediaItem = new MediaItem(XescScreenCaptureJob.ScreenCaptureFileName);
            }
            catch (InvalidMediaFileException exp)
            {
                Console.WriteLine(exp.Message);
                return;
            }

            // Create the job, add the media item and encode.
            using (Job job = new Job())
            {
                job.MediaItems.Add(mediaItem);

                mediaItem.OutputFormat.VideoProfile = new AdvancedVC1VideoProfile()
                {
                    Size = mediaItem.MainMediaFile.VideoStreams[0].VideoSize,
                    Bitrate = new ConstantBitrate(SettingsManager.ConfigUser.ScreencastBitrate)
                };

                job.CreateSubfolder = false;
                job.OutputDirectory = Program.ScreenshotsPath;

                job.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(OnProgress);

                job.Encode();
            }
        }

        #endregion Expression Encoder

        private void ScreencastStop()
        {
            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.gif:
                    this.GifScreenCaptureJobStop();
                    break;

                default:
                    this.XescScreenCaptureJob.Stop();
                    Encoder.RunWorkerAsync();
                    break;
            }

            progress.Visible = true;
        }

        private void Encoder_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.gif:
                    GifEncode();
                    break;

                default:
                    WMEncode();
                    break;
            }
        }

        private void OnProgress(object sender, EncodeProgressEventArgs e)
        {
            Encoder.ReportProgress((int)e.Progress);
        }

        private void Encoder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }

        private void Encoder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EDataType dataType = EDataType.File;

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.gif:
                    dataType = EDataType.Image;
                    if (GifCache != null)
                        GifCache.Dispose();

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

            UploadTask task = UploadTask.CreateFileUploaderTask(Screencast.FilePath, dataType);
            task.SetWorkflow(_act.Workflow);
            TaskManager.Start(task);

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