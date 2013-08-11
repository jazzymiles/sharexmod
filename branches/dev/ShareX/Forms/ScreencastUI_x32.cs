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
        public void Stop()
        {
            ScreencastStop_Common();

            switch (SettingsManager.ConfigUser.ScreencastEncoderType)
            {
                case EScreencastEncoderType.CommandLineEncoder:
                case EScreencastEncoderType.GraphicsInterchangeFormat:
                case EScreencastEncoderType.PromptUser:
                    break;

                case EScreencastEncoderType.WindowsMediaVideo:
                case EScreencastEncoderType.ExpressionEncoderScreenCaptureCodec:
                    ExpressionEncoderScreenCaptureStop();
                    break;

                default:
                    throw new Exception("Unsupported screencast filetype: " + SettingsManager.ConfigUser.ScreencastEncoderType.GetDescription());
            }
        }

        private void Encoder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Program.ScreencastCancellationPending = false;

            switch (SettingsManager.ConfigUser.ScreencastEncoderType)
            {
                case EScreencastEncoderType.PromptUser:
                case EScreencastEncoderType.GraphicsInterchangeFormat:
                case EScreencastEncoderType.CommandLineEncoder:
                    Encoder_RunWorkerCompleted_Img();
                    break;

                case EScreencastEncoderType.WindowsMediaVideo:
                    Encoder_RunWorkerCompleted_WMV();
                    break;

                case EScreencastEncoderType.ExpressionEncoderScreenCaptureCodec:
                    Encoder_RunWorkerCompleted_XESC();
                    break;

                default:
                    throw new Exception("Unhandled screencast filetype: " + SettingsManager.ConfigUser.ScreencastEncoderType.GetDescription());

            }

            Encoder_RunWorkerCompleted_Publish();
        }

        private void ScreencastUI_Shown(object sender, EventArgs e)
        {
            Program.IsRecordingScreencast = true;
       
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
        }

        private void tmrShowStopButton_Tick(object sender, EventArgs e)
        {
            this.BackgroundImage = Resources.stop;
            tmrShowStopButton.Stop();
        }
    }
}