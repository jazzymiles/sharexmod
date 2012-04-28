using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using ScreenCapture;
using ShareX.HelperClasses;

namespace ShareX
{
    public partial class MainForm
    {
        private void InitHotkeys()
        {
            HotkeyManager = new HotkeyManager(this);

            Workflow wfClipboardUpload = new Workflow(EHotkey.ClipboardUpload, Program.HotkeyClipboardUpload);
            Workflow wfFileUpload = new Workflow(EHotkey.FileUpload, Program.HotkeyFileUpload);

            Workflow wfPrintScreen = new Workflow(EHotkey.FullScreen, Program.HotkeyPrintScreen);
            Workflow wfActiveMonitor = new Workflow(EHotkey.ActiveMonitor, Program.HotkeyActiveMonitor);
            Workflow wfActiveWindow = new Workflow(EHotkey.ActiveWindow, Program.HotkeyActiveWindow);
            Workflow wfWindowRectangle = new Workflow(EHotkey.WindowRectangle, Program.HotkeyWindowRectangle);

            Workflow wfRectangleRegion = new Workflow(EHotkey.RectangleRegion, Program.HotkeyRectangleRegion);
            Workflow wfRoundedRectangleRegion = new Workflow(EHotkey.RoundedRectangleRegion, new HotkeySetting());
            Workflow wfEllipseRegion = new Workflow(EHotkey.EllipseRegion, new HotkeySetting());
            Workflow wfTriangleRegion = new Workflow(EHotkey.TriangleRegion, new HotkeySetting());
            Workflow wfDiamondRegion = new Workflow(EHotkey.DiamondRegion, new HotkeySetting());
            Workflow wfPolygonRegion = new Workflow(EHotkey.PolygonRegion, new HotkeySetting());
            Workflow wfFreeHandRegion = new Workflow(EHotkey.FreeHandRegion, new HotkeySetting());

            wfClipboardUpload.Activities.Add(EActivity.UploadClipboard);
            wfFileUpload.Activities.Add(EActivity.UploadFile);
            wfPrintScreen.Activities.Add(EActivity.CaptureScreen);
            wfActiveWindow.Activities.Add(EActivity.CaptureActiveWindow);
            wfActiveMonitor.Activities.Add(EActivity.CaptureActiveMonitor);
            wfWindowRectangle.Activities.Add(EActivity.CaptureWindowRectangle);
            wfRectangleRegion.Activities.Add(EActivity.CaptureRectangleRegion);
            wfRoundedRectangleRegion.Activities.Add(EActivity.CaptureRoundedRectangleRegion);
            wfEllipseRegion.Activities.Add(EActivity.CaptureEllipseRegion);
            wfTriangleRegion.Activities.Add(EActivity.CaptureTriangleRegion);
            wfDiamondRegion.Activities.Add(EActivity.CaptureDiamondRegion);
            wfPolygonRegion.Activities.Add(EActivity.CapturePolygonRegion);
            wfFreeHandRegion.Activities.Add(EActivity.CaptureFreeHandRegion);

            if (Program.Settings.Workflows11.Count == 0)
            {
                Program.Settings.Workflows11.Add(wfClipboardUpload);
                Program.Settings.Workflows11.Add(wfFileUpload);
                Program.Settings.Workflows11.Add(wfPrintScreen);
                Program.Settings.Workflows11.Add(wfActiveWindow);
                Program.Settings.Workflows11.Add(wfActiveMonitor);
                Program.Settings.Workflows11.Add(wfWindowRectangle);
                Program.Settings.Workflows11.Add(wfRectangleRegion);
                Program.Settings.Workflows11.Add(wfRoundedRectangleRegion);
                Program.Settings.Workflows11.Add(wfEllipseRegion);
                Program.Settings.Workflows11.Add(wfTriangleRegion);
                Program.Settings.Workflows11.Add(wfDiamondRegion);
                Program.Settings.Workflows11.Add(wfPolygonRegion);
                Program.Settings.Workflows11.Add(wfFreeHandRegion);

                foreach (Workflow wf in Program.Settings.Workflows11)
                {
                    wf.Activities.Add(EActivity.ClipboardCopyLink);
                    wf.Activities.Add(EActivity.UploadToRemoteHost);
                }
            } // if Workflows.Count == 0

            foreach (Workflow wf in Program.Settings.Workflows11)
            {
                string tag = wf.HotkeyConfig.Tag;
                HotkeyManager.AddHotkey(wf, () => DoWork(tag));
            }

            string failedHotkeys;

            if (HotkeyManager.IsHotkeyRegisterFailed(out failedHotkeys))
            {
                MessageBox.Show("Unable to register hotkey(s):\r\n\r\n" + failedHotkeys +
                    "\r\n\r\nPlease select a different hotkey or quit the conflicting application and reopen ShareX.",
                    "Hotkey register failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void DoWork(string tag)
        {
            Workflow wf = Program.Settings.Workflows11.FirstOrDefault(x => x.HotkeyConfig.Tag == tag);
            Image img = null;

            if (wf == null)
                throw new Exception("Workflow cannot be found!");

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
                    case EActivity.ClipboardCopyImage:
                        CopyMultiFormatBitmapToClipboard((Image)img.Clone());
                        break;
                    case EActivity.ClipboardCopyLink:

                        break;
                    case EActivity.ImageAnnotate:
                        EditImage(ref img);
                        break;
                    case EActivity.SaveToFile:
                        SaveImageToFile(img);
                        break;
                    case EActivity.SaveToFileWithDialog:
                        SaveImageToFileWithDialog((Image)img.Clone());
                        break;
                    case EActivity.UploadToRemoteHost:
                        AfterCapture(img);
                        break;
                    default:
                        throw new Exception(string.Format("{0} is not yet implemented.", act));
                }
            }
        }

        private void EditImage(ref Image img)
        {
            Greenshot.IniFile.IniConfig.Init();
            GreenshotPlugin.Core.CoreConfiguration conf = Greenshot.IniFile.IniConfig.GetIniSection<GreenshotPlugin.Core.CoreConfiguration>(); ;
            conf.OutputFileFilenamePattern = "${title}";
            conf.OutputFilePath = Program.ScreenshotsPath;

            Greenshot.Plugin.ICapture capture = new GreenshotPlugin.Core.Capture();
            capture.Image = img;
            ImageData imageData = TaskHelper.PrepareImageAndFilename(img);
            capture.CaptureDetails.Filename = Path.Combine(Program.ScreenshotsPath, imageData.Filename);
            capture.CaptureDetails.Title =
                Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
            capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
            capture.CaptureDetails.AddMetaData("source", "file");

            var surface = new Greenshot.Drawing.Surface(capture);
            var editor = new Greenshot.ImageEditorForm(surface, Program.Settings.CaptureSaveImage) { Icon = this.Icon };

            editor.SetImagePath(capture.CaptureDetails.Filename);
            editor.Visible = false;
            editor.ShowDialog();
            img = editor.GetImageForExport();
        }

        private void SaveImageToFile(Image img)
        {
            using (ImageData imageData = TaskHelper.PrepareImageAndFilename(img))
            {
                imageData.WriteToFile(Program.ScreenshotsPath);
            }
        }

        private void SaveImageToFileWithDialog(Image img)
        {
            using (ImageData imageData = TaskHelper.PrepareImageAndFilename(img))
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                dlg.ShowNewFolderButton = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageData.WriteToFile(dlg.SelectedPath);
                }
            }
        }

        private static void CopyMultiFormatBitmapToClipboard(Image img)
        {
            using (img)
            {
                MemoryStream ms = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();
                Bitmap bmp = new Bitmap(img);
                bmp.Save(ms, ImageFormat.Bmp);
                byte[] b = ms.GetBuffer();
                ms2.Write(b, 14, (int)ms.Length - 14);
                ms.Position = 0;
                DataObject dataObject = new DataObject();
                dataObject.SetData(DataFormats.Bitmap, bmp);
                dataObject.SetData(DataFormats.Dib, ms2);
                Clipboard.SetDataObject(dataObject, true, 3, 1000);
            }
        }

        private string FindTagByHotkey(EHotkey hotkey)
        {
            return FindAppWorkflowByHotkey(hotkey).HotkeyConfig.Tag;
        }

        private Workflow FindAppWorkflowByHotkey(EHotkey hotkey)
        {
            return Program.Settings.Workflows11.FirstOrDefault(x => x.Hotkey == hotkey);
        }
    }
}