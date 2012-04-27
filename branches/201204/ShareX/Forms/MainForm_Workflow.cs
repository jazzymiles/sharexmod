using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using HelpersLib;
using ScreenCapture;

namespace ShareX
{
    public partial class MainForm
    {
        public void DoWork(string tag)
        {
            Workflow wf = Program.Settings.Workflows97.FirstOrDefault(x => x.HotkeyConfig.Tag == tag);
            Image img = null;

            foreach (EActivity act in wf.Activities)
            {
                if (act == EActivity.CaptureActiveWindow)
                {
                    img = CaptureActiveWindow(false);
                }
                else if (act == EActivity.CaptureRectangleRegion)
                {
                    img = CaptureRegion(new RectangleRegion(), false);
                }

                if (act == EActivity.ImageAnnotate)
                {
                    EditImage(ref img);
                }

                if (act == EActivity.UploadToRemoteHost)
                {
                    AfterCapture(img);
                }

                if (act == EActivity.UploadClipboard)
                {
                    UploadManager.ClipboardUpload();
                }
                else if (act == EActivity.UploadFile)
                {
                    UploadManager.UploadFile();
                }
            }
        }
    }
}