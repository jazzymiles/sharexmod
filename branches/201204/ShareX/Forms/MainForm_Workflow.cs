using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;

namespace ShareX
{
    public partial class MainForm
    {
        public void DoWork(Workflow wf)
        {
            foreach (EActivity act in wf.Activities)
            {
                if (act == EActivity.CaptureActiveWindow)
                {
                    CaptureActiveWindow(false);
                }

                if (act == EActivity.ImageAnnotate)
                {
                    Console.WriteLine("Annotate Image");
                }

                if (act == EActivity.UploadToRemoteHost)
                {
                    Console.WriteLine("Annotate Image");
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