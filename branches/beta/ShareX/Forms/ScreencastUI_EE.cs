using Microsoft.Expression.Encoder;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Profiles;
using Microsoft.Expression.Encoder.ScreenCapture;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX.Forms
{
    public partial class ScreencastUI
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ScreenCaptureJob XescScreenCaptureJob;
        private Timer XescTimer = new Timer() { Enabled = true };

        private void ExpressionEncoderStart()
        {
            XescScreenCaptureJob = new Microsoft.Expression.Encoder.ScreenCapture.ScreenCaptureJob();
            XescScreenCaptureJob.CaptureFollowCursor = SettingsManager.ConfigUser.ScreencastFollowMouseCursor;
            XescScreenCaptureJob.CaptureRectangle = CaptureRectangle;
            XescScreenCaptureJob.OutputPath = Program.ScreenshotsPath;

            if (SettingsManager.ConfigUser.ScreencastEnableAudio)
            {
                Collection<EncoderDevice> audioDevices = EncoderDevices.FindDevices(EncoderDeviceType.Audio);
                try
                {
                    EncoderDevice foundDevice = audioDevices.First(delegate(EncoderDevice item)
                    {
                        return item.Name.Contains(@"Microphone");
                    });
                    XescScreenCaptureJob.AddAudioDeviceSource(foundDevice);
                }
                catch (Exception ex)
                {
                    log.Error("Error while finding audio devices.", ex);
                    XescScreenCaptureJob.AddAudioDeviceSource(audioDevices[0]);
                }
            }

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

        private void ExpressionEncoderScreenCaptureStop()
        {
            this.XescScreenCaptureJob.Stop();
            if (!Encoder.IsBusy) // XescTimer_Tick can fire this twice
                Encoder.RunWorkerAsync();
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

                try
                {
                    mediaItem.OutputFormat.AudioProfile.Bitrate = new ConstantBitrate(SettingsManager.ConfigUser.ScreencastAudioBitrate);

                    switch (SettingsManager.ConfigUser.ScreencastBitrateType)
                    {
                        case EBitrateType.ConstantBitrate:
                            mediaItem.OutputFormat.VideoProfile.Bitrate =
                                new ConstantBitrate(SettingsManager.ConfigUser.ScreencastVideoBitrate);
                            break;

                        case EBitrateType.VariableConstrainedBitrate:
                            mediaItem.OutputFormat.VideoProfile.Bitrate =
                                new VariableConstrainedBitrate(SettingsManager.ConfigUser.ScreencastVideoBitrate, SettingsManager.ConfigUser.ScreencastVideoBitrate * 2);
                            break;

                        case EBitrateType.VariableQualityBitrate:
                            mediaItem.OutputFormat.VideoProfile.Bitrate =
                                new VariableQualityBitrate(SettingsManager.ConfigUser.ScreencastVBRQuality);
                            break;

                        case EBitrateType.VariableUnconstrainedBitrate:
                            mediaItem.OutputFormat.VideoProfile.Bitrate =
                                new VariableUnconstrainedBitrate(SettingsManager.ConfigUser.ScreencastVideoBitrate);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error while customising audio/video profile.", ex);
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

        private void Encoder_RunWorkerCompleted_WMV()
        {
            Screencast.FilePath = Path.ChangeExtension(XescScreenCaptureJob.ScreenCaptureFileName, "wmv");

            if (File.Exists(Screencast.FilePath))
                File.Delete(XescScreenCaptureJob.ScreenCaptureFileName); // if wmv exists then delete xesc

        }

        private void Encoder_RunWorkerCompleted_XESC()
        {
            Screencast.FilePath = XescScreenCaptureJob.ScreenCaptureFileName;
        }
    }
}