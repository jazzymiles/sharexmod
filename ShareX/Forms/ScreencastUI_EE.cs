using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Expression.Encoder.ScreenCapture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX.Forms
{
    public partial class ScreencastUI
    {
        private ScreenCaptureJob XescScreenCaptureJob;
        private Timer XescTimer = new Timer() { Enabled = true };

        private void ExpressionEncoderStart()
        {
            XescScreenCaptureJob = new Microsoft.Expression.Encoder.ScreenCapture.ScreenCaptureJob();
            XescScreenCaptureJob.CaptureFollowCursor = SettingsManager.ConfigUser.FollowMouseCursor;
            XescScreenCaptureJob.CaptureRectangle = CaptureRectangle;
            XescScreenCaptureJob.OutputPath = Program.ScreenshotsPath;
            XescScreenCaptureJob.Start();

            XescTimer.Tick += XescTimer_Tick;
        }

        void XescTimer_Tick(object sender, EventArgs e)
        {
            if (Program.ScreencastCancellationPending)
            {
                XescTimer.Stop();
                Stop();
            }
        }

        private void WMEncode()
        {
            // Create the media item and validates it.
            Microsoft.Expression.Encoder.MediaItem mediaItem;
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
            using (Microsoft.Expression.Encoder.Job job = new Microsoft.Expression.Encoder.Job())
            {
                job.MediaItems.Add(mediaItem);

                mediaItem.OutputFormat.VideoProfile = new AdvancedVC1VideoProfile()
                {
                    Size = mediaItem.MainMediaFile.VideoStreams[0].VideoSize,
                };

                switch (SettingsManager.ConfigUser.ScreencastBitrateType)
                {
                    case EBitrateType.ConstantBitrate:
                        mediaItem.OutputFormat.VideoProfile.Bitrate = new ConstantBitrate(SettingsManager.ConfigUser.ScreencastBitrate);
                        break;

                    case EBitrateType.VariableConstrainedBitrate:
                        mediaItem.OutputFormat.VideoProfile.Bitrate = new VariableConstrainedBitrate(SettingsManager.ConfigUser.ScreencastBitrate, SettingsManager.ConfigUser.ScreencastBitrate * 2);
                        break;

                    case EBitrateType.VariableQualityBitrate:
                        mediaItem.OutputFormat.VideoProfile.Bitrate = new VariableQualityBitrate(SettingsManager.ConfigUser.ScreencastVBRQuality);
                        break;

                    case EBitrateType.VariableUnconstrainedBitrate:
                        mediaItem.OutputFormat.VideoProfile.Bitrate = new VariableUnconstrainedBitrate(SettingsManager.ConfigUser.ScreencastBitrate);
                        break;
                }

                job.CreateSubfolder = false;
                job.OutputDirectory = Program.ScreenshotsPath;

                job.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(WMEncoderOnProgress);

                job.Encode();
            }
        }

        private void WMEncoderOnProgress(object sender, Microsoft.Expression.Encoder.EncodeProgressEventArgs e)
        {
            Encoder.ReportProgress((int)e.Progress);
        }
    }
}