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
            Workflow wf = Program.Settings.Workflows2.FirstOrDefault(x => x.HotkeyConfig.Tag == tag);
            Image img = null;

            if (wf == null)
                return;

            foreach (EActivity act in wf.Activities)
            {
                switch (act)
                {
                    case EActivity.UploadClipboard:
                        UploadManager.ClipboardUpload();
                        break;
                    case EActivity.UploadFile:
                        UploadManager.UploadFile();
                        break;
                    case EActivity.CaptureScreen:
                        img = CaptureScreen(false);
                        break;
                    case EActivity.CaptureActiveWindow:
                        img = CaptureActiveWindow(false);
                        break;
                    case EActivity.CaptureRectangleRegion:
                        img = CaptureRegion(new RectangleRegion(), false);
                        break;
                    case EActivity.CaptureActiveMonitor:
                        img = CaptureActiveMonitor(false);
                        break;
                    case EActivity.CaptureWindowRectangle:
                        img = WindowRectangleCapture(false);
                        break;
                    case EActivity.CaptureRoundedRectangleRegion:
                        img = CaptureRegion(new RoundedRectangleRegion(), false);
                        break;
                    case EActivity.CaptureEllipseRegion:
                        img = CaptureRegion(new EllipseRegion(), false);
                        break;
                    case EActivity.CaptureTriangleRegion:
                        img = CaptureRegion(new TriangleRegion(), false);
                        break;
                    case EActivity.CaptureDiamondRegion:
                        img = CaptureRegion(new DiamondRegion(), false);
                        break;
                    case EActivity.CapturePolygonRegion:
                        img = CaptureRegion(new PolygonRegion(), false);
                        break;
                    case EActivity.CaptureFreeHandRegion:
                        img = CaptureRegion(new FreeHandRegion(), false);
                        break;
                    case EActivity.ImageAnnotate:
                        EditImage(ref img);
                        break;
                    case EActivity.UploadToRemoteHost:
                        AfterCapture(img);
                        break;
                }
            }
        }
    }
}