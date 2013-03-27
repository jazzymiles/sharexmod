using HelpersLib;
using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Expression.Encoder.ScreenCapture;
using ScreenCapture;
using ShareX.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareX.Forms
{
    public partial class ScreencastUI : Form
    {
        public ImageData Screencast { get; set; }

        private AfterCaptureActivity _act;

        private Rectangle CaptureRectangle;

        private ScreenCaptureJob _screenCaptureJob = new ScreenCaptureJob();
        private BackgroundWorker _wmEncoder = new BackgroundWorker();

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

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                default:
                    ScreencastExpressionEncoderStart();
                    break;
            }
        }

        private void GifEncoderStart()
        {
            // TODO: record GIF until user hits okay
            ScreenRecorder screenRecorder = null;
            string path = "";
        }

        private void ScreencastExpressionEncoderStart()
        {
            ScreenCaptureVideoProfile video = new ScreenCaptureVideoProfile();

            _screenCaptureJob.CaptureFollowCursor = SettingsManager.ConfigUser.FollowMouseCursor;
            _screenCaptureJob.CaptureRectangle = CaptureRectangle;
            _screenCaptureJob.OutputPath = Program.ScreenshotsPath;
            _screenCaptureJob.Start();
        }

        private void ScreencastExpressionEncoderStop()
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
            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.wmv:
                    Screencast.FilePath = Path.ChangeExtension(_screenCaptureJob.ScreenCaptureFileName, "wmv");

                    if (File.Exists(Screencast.FilePath))
                        File.Delete(_screenCaptureJob.ScreenCaptureFileName);
                    break;

                case EScreencastFileType.xesc:
                    Screencast.FilePath = _screenCaptureJob.ScreenCaptureFileName;
                    break;
            }

            UploadTask task = UploadTask.CreateFileUploaderTask(Screencast.FilePath, EDataType.File);
            task.SetWorkflow(_act.Workflow);
            TaskManager.Start(task);

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
                ScreencastExpressionEncoderStop();
            }
        }

        private void ScreencastUI_MouseClick(object sender, MouseEventArgs e)
        {
            ScreencastExpressionEncoderStop();
        }

        private void ScreencastUI_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ScreencastExpressionEncoderStop();
        }
    }
}