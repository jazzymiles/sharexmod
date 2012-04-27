using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using ScreenCapture;
using ShareX.HelperClasses;

namespace ShareX
{
    public partial class MainForm
    {
        public void DoWork(string tag)
        {
            Workflow wf = Program.Settings.Workflows7.FirstOrDefault(x => x.HotkeyConfig.Tag == tag);
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
                    case EActivity.ClipboardCopyImage:
                        CopyMultiFormatBitmapToClipboard((Image)img.Clone());
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
            if (Greenshot.MainForm.instance == null)
                Greenshot.MainForm.Start(new string[0]);

            GreenshotPlugin.Core.CoreConfiguration coreConfiguration = Greenshot.IniFile.IniConfig.GetIniSection<GreenshotPlugin.Core.CoreConfiguration>();
            coreConfiguration.OutputFileFilenamePattern = "${title}";
            coreConfiguration.OutputFilePath = Program.ScreenshotsPath;

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
    }
}