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
                    this.XescScreenCaptureJob.Stop();
                    if (!Encoder.IsBusy) // XescTimer_Tick can fire this twice
                        Encoder.RunWorkerAsync();
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
                    Encoder_RunWorkerCompleted_Img();
                    break;

                case EScreencastEncoderType.WindowsMediaVideo:
                    Screencast.FilePath = Path.ChangeExtension(XescScreenCaptureJob.ScreenCaptureFileName, "wmv");

                    if (File.Exists(Screencast.FilePath))
                        File.Delete(XescScreenCaptureJob.ScreenCaptureFileName); // if wmv exists then delete xesc
                    break;

                case EScreencastEncoderType.ExpressionEncoderScreenCaptureCodec:
                    Screencast.FilePath = XescScreenCaptureJob.ScreenCaptureFileName;
                    break;
            }

            Encoder_RunWorkerCompleted_Publish();
        }
    }
}