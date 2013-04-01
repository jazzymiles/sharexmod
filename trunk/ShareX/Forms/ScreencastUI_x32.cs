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

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                case EScreencastFileType.gif:
                case EScreencastFileType.custom:
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
        }
 
        private void Encoder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Program.ScreencastCancellationPending = false;

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                case EScreencastFileType.gif:
                    Encoder_RunWorkerCompleted_Img();
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

            Encoder_RunWorkerCompleted_Publish();
        }
    }
}