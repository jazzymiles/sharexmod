using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Expression.Encoder.ScreenCapture;
using ShareX.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX.Forms
{
    public partial class ScreencastUI : Form
    {
        public ImageData Screencast { get; set; }

        private ScreenCaptureJob _screenCaptureJob = new ScreenCaptureJob();
        private BackgroundWorker _wmEncoder = new BackgroundWorker();

        public ScreencastUI(ImageData imagedata)
        {
            InitializeComponent();

            this.Text = Application.ProductName + " - Screencast";
            this.Location = new Point(0, 0);

            Screencast = imagedata;

            ScreenCaptureVideoProfile video = new ScreenCaptureVideoProfile();

            Rectangle capRect = imagedata.CaptureRectangle;

            capRect.Width = Math.Max(RoundOff(capRect.Width, 4.0), 4);
            capRect.Height = Math.Max(RoundOff(capRect.Height, 4.0), 4);

            _screenCaptureJob.CaptureRectangle = capRect;
            _screenCaptureJob.OutputPath = Program.ScreenshotsPath;
            _screenCaptureJob.Start();
        }

        private void ScreencastStop()
        {
            _screenCaptureJob.Stop();

            progress.Visible = true;

            _wmEncoder.WorkerReportsProgress = true;
            _wmEncoder.DoWork += WMEncoder_DoWork;
            _wmEncoder.ProgressChanged += WMEncoder_ProgressChanged;
            _wmEncoder.RunWorkerCompleted += WMEncoder_RunWorkerCompleted;
            _wmEncoder.RunWorkerAsync();
        }

        private void WMEncoder_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create the media item and validates it.
            MediaItem mediaItem;
            try
            {
                mediaItem = new MediaItem(_screenCaptureJob.ScreenCaptureFileName);
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

        private void WMEncoder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }

        private void WMEncoder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Screencast.FilePath = Path.ChangeExtension(_screenCaptureJob.ScreenCaptureFileName, "wmv");

            if (File.Exists(Screencast.FilePath))
                File.Delete(_screenCaptureJob.ScreenCaptureFileName);

            this.Close();
        }

        /// <summary>
        /// Called when encoding progress occurs.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnProgress(object sender, EncodeProgressEventArgs e)
        {
            _wmEncoder.ReportProgress((int)e.Progress);
        }

        public int RoundOff(int round, double roundOffTo)
        {
            return ((int)Math.Round(round / roundOffTo)) * (int)roundOffTo;
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