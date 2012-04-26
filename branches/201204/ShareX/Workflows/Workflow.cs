using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib.Hotkey;

namespace ShareX
{
    public class Workflow
    {
        public HotkeySetting Hotkey = new HotkeySetting();
        public List<EActivity> Activities = new List<EActivity>();

        public Workflow()
        {
        }

        public void DoWork()
        {
            foreach (EActivity act in Activities)
            {
                if (act == EActivity.CaptureActiveWindow)
                {
                    Console.WriteLine("Capture Active Window");
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