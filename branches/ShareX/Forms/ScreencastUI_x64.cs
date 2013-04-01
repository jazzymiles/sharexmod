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
        private void ScreencastUI_Load(object sender, EventArgs e)
        {
            if (SettingsManager.ConfigUser.ScreencastFileType == EScreencastFileType.wmv ||
                SettingsManager.ConfigUser.ScreencastFileType == EScreencastFileType.xesc)
                SettingsManager.ConfigUser.ScreencastFileType = EScreencastFileType.gif;
        }

        private void ExpressionEncoderStart()
        {
            // nothing done here - added for compatibilty
        }

        private void WMEncode()
        {
            // nothing done here - added for compatibilty
        }

        private void Encoder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Program.ScreencastCancellationPending = false; // to prepare for another screencast

            switch (SettingsManager.ConfigUser.ScreencastFileType)
            {
                case EScreencastFileType.avi:
                case EScreencastFileType.gif:
                case EScreencastFileType.custom:
                    Encoder_RunWorkerCompleted_Img();
                    break;
                case EScreencastFileType.wmv:
                case EScreencastFileType.xesc:
                    break;
            }

            Encoder_RunWorkerCompleted_Publish();
        }

        public void Stop()
        {
            ScreencastStop_Common();
        }

    }
}