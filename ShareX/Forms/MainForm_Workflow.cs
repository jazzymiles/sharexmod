using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using ScreenCapture;
using ShareX.HelperClasses;

namespace ShareX
{
    public partial class MainForm
    {
        internal void InitHotkeys()
        {
            HotkeyManager = new HotkeyManager(this);

            if (Program.Settings.Workflows1.Count == 0)
            {
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

                Program.Settings.Workflows1.Add(wfClipboardUpload);
                Program.Settings.Workflows1.Add(wfFileUpload);
                Program.Settings.Workflows1.Add(wfPrintScreen);
                Program.Settings.Workflows1.Add(wfActiveWindow);
                Program.Settings.Workflows1.Add(wfActiveMonitor);
                Program.Settings.Workflows1.Add(wfWindowRectangle);
                Program.Settings.Workflows1.Add(wfRectangleRegion);
                Program.Settings.Workflows1.Add(wfRoundedRectangleRegion);
                Program.Settings.Workflows1.Add(wfEllipseRegion);
                Program.Settings.Workflows1.Add(wfTriangleRegion);
                Program.Settings.Workflows1.Add(wfDiamondRegion);
                Program.Settings.Workflows1.Add(wfPolygonRegion);
                Program.Settings.Workflows1.Add(wfFreeHandRegion);

                foreach (Workflow wf in Program.Settings.Workflows1)
                {
                    if (wf.Hotkey == EHotkey.ClipboardUpload || wf.Hotkey == EHotkey.FileUpload)
                        wf.Activities.Add(EActivity.UploadToRemoteHost);
                    else
                        wf.Activities.Add(EActivity.AfterCaptureTasks);
                }
            } // if Workflows.Count == 0

            foreach (Workflow wf in Program.Settings.Workflows1)
            {
                string tag = wf.HotkeyConfig.Tag;
                HotkeyManager.AddHotkey(wf, () => DoWork(tag, false));
            }

            string failedHotkeys;

            if (HotkeyManager.IsHotkeyRegisterFailed(out failedHotkeys))
            {
                MessageBox.Show("Unable to register hotkey(s):\r\n\r\n" + failedHotkeys +
                    "\r\n\r\nPlease select a different hotkey or quit the conflicting application and reopen ShareX.",
                    "Hotkey register failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            log.Info("Initialized hotkeys.");
        }

        public void DoWork(string tag, bool autoHideForm = true)
        {
            Workflow wf = Program.Settings.Workflows1.FirstOrDefault(x => x.HotkeyConfig.Tag == tag);
            ImageData imagedata_wf = null;
            string fpImg = string.Empty;

            if (wf == null)
            {
                log.Error("Workflow cannot be found!");
                return;
            }

            AfterCaptureActivity jobs_wf = new AfterCaptureActivity();
            jobs_wf.Workflow = wf;

            foreach (EActivity act in wf.Activities)
            {
                switch (act)
                {
                    case EActivity.UploadClipboard:
                        jobs_wf.InputType = EInputType.Clipboard;
                        break;
                    case EActivity.UploadFile:
                        jobs_wf.InputType = EInputType.FileSystem;
                        break;
                    case EActivity.CaptureScreen:
                        imagedata_wf = CaptureScreen(autoHideForm);
                        break;
                    case EActivity.CaptureActiveWindow:
                        imagedata_wf = CaptureActiveWindow(autoHideForm);
                        break;
                    case EActivity.CaptureRectangleRegion:
                        imagedata_wf = CaptureRegion(new RectangleRegion(), autoHideForm);
                        break;
                    case EActivity.CaptureActiveMonitor:
                        imagedata_wf = CaptureActiveMonitor(autoHideForm);
                        break;
                    case EActivity.CaptureWindowRectangle:
                        imagedata_wf = WindowRectangleCapture(autoHideForm);
                        break;
                    case EActivity.CaptureRoundedRectangleRegion:
                        imagedata_wf = CaptureRegion(new RoundedRectangleRegion(), autoHideForm);
                        break;
                    case EActivity.CaptureEllipseRegion:
                        imagedata_wf = CaptureRegion(new EllipseRegion(), autoHideForm);
                        break;
                    case EActivity.CaptureTriangleRegion:
                        imagedata_wf = CaptureRegion(new TriangleRegion(), autoHideForm);
                        break;
                    case EActivity.CaptureDiamondRegion:
                        imagedata_wf = CaptureRegion(new DiamondRegion(), autoHideForm);
                        break;
                    case EActivity.CapturePolygonRegion:
                        imagedata_wf = CaptureRegion(new PolygonRegion(), autoHideForm);
                        break;
                    case EActivity.CaptureFreeHandRegion:
                        imagedata_wf = CaptureRegion(new FreeHandRegion(), autoHideForm);
                        break;
                    case EActivity.ClipboardCopyImage:
                        jobs_wf.Subtasks |= Subtask.CopyImageToClipboard;
                        break;
                    case EActivity.ImageAnnotate:
                        jobs_wf.Subtasks |= Subtask.AnnotateImage;
                        break;
                    case EActivity.SaveToFile:
                        jobs_wf.Subtasks |= Subtask.SaveImageToFile;
                        break;
                    case EActivity.SaveToFileWithDialog:
                        jobs_wf.Subtasks |= Subtask.SaveImageToFileWithDialog;
                        break;
                    case EActivity.Printer:
                        jobs_wf.Subtasks |= Subtask.Print;
                        break;
                    case EActivity.AfterCaptureTasks:
                        jobs_wf.Subtasks |= Program.Settings.AfterCaptureTasks;
                        break;
                    case EActivity.UploadToRemoteHost:
                        jobs_wf.Subtasks |= Subtask.UploadImageToHost;
                        break;

                    case EActivity.ShowImageEffectsStudio:
                        jobs_wf.Subtasks |= Subtask.ShowImageEffectsStudio;
                        break;
                    case EActivity.ImageAnnotateAddTornEffect:
                        jobs_wf.Subtasks |= Subtask.AnnotateImageAddTornEffect;
                        break;
                    case EActivity.ImageAnnotateAddShadowBorder:
                        jobs_wf.Subtasks |= Subtask.AnnotateImageAddShadowBorder;
                        break;

                    case EActivity.UploadToImageShack:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.ImageShack);
                        break;
                    case EActivity.UploadToTinyPic:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.TinyPic);
                        break;
                    case EActivity.UploadToImgur:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Imgur);
                        break;
                    case EActivity.UploadToFlickr:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Flickr);
                        break;
                    case EActivity.UploadToPhotobucket:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Photobucket);
                        break;
                    case EActivity.UploadToTwitPic:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Twitpic);
                        break;
                    case EActivity.UploadToTwitSnaps:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Twitsnaps);
                        break;
                    case EActivity.UploadToYfrog:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.yFrog);
                        break;
                    case EActivity.UploadToImmio:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.Immio);
                        break;

                    case EActivity.UploadToPastebin:
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.Pastebin);
                        break;
                    case EActivity.UploadToPastebinCa:
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.PastebinCA);
                        break;
                    case EActivity.UploadToPaste2:
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.Paste2);
                        break;
                    case EActivity.UploadToSlexy:
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.Slexy);
                        break;

                    case EActivity.UploadToDropbox:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.FileUploader);
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.FileUploader);
                        jobs_wf.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.Dropbox);
                        break;
                    case EActivity.UploadToRapidShare:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.FileUploader);
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.FileUploader);
                        jobs_wf.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.RapidShare);
                        break;
                    case EActivity.UploadToSendSpace:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.FileUploader);
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.FileUploader);
                        jobs_wf.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.SendSpace);
                        break;
                    case EActivity.UploadToMinus:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.FileUploader);
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.FileUploader);
                        jobs_wf.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.Minus);
                        break;
                    case EActivity.UploadToFTP:
                        jobs_wf.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.FileUploader);
                        jobs_wf.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.FileUploader);
                        jobs_wf.DestConfig.FileUploaders.Add(UploadersLib.FileDestination.FTP);
                        break;

                    case EActivity.ShortenLinkUsingGoogle:
                        jobs_wf.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.Google);
                        break;
                    case EActivity.ShortenLinkUsingBitly:
                        jobs_wf.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.BITLY);
                        break;
                    case EActivity.ShortenLinkUsingJmp:
                        jobs_wf.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.Jmp);
                        break;
                    case EActivity.ShortenLinkUsingIsgd:
                        jobs_wf.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.ISGD);
                        break;
                    case EActivity.ShortenLinkUsingTinyUrl:
                        jobs_wf.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.TINYURL);
                        break;
                    case EActivity.ShortenLinkUsingTurl:
                        jobs_wf.DestConfig.LinkUploaders.Add(UploadersLib.UrlShortenerType.TURL);
                        break;

                    default:
                        FormsHelper.ShowLog();
                        log.ErrorFormat("{0} is not  yet implemented.", act.GetDescription());
                        break;
                }
            }

            AfterHotkeyPressed(imagedata_wf, jobs_wf);
        }

        private void AfterHotkeyPressed(ImageData imageData, AfterCaptureActivity jobs = null)
        {
            if (imageData != null)
            {
                if (jobs.Subtasks == Subtask.None)
                    jobs.Subtasks |= Program.Settings.AfterCaptureTasks;
                if (jobs.DestConfig.ImageUploaders.Count > 0)
                    jobs.Subtasks |= Subtask.UploadImageToHost;
                log.Debug("After Capture initiated.");
                AfterCapture(imageData, jobs);
            }
            else if (jobs.InputType == EInputType.Clipboard)
            {
                if (jobs.Subtasks == Subtask.None)
                    jobs.Subtasks |= Program.Settings.AfterCaptureTasks;

                log.Debug("ClipboardUpload initiated.");
                UploadManager.ClipboardUpload(jobs);
            }
            else if (jobs.InputType == EInputType.FileSystem)
            {
                log.Debug("UploadFile initiated.");
                UploadManager.UploadFile(jobs);
            }
        }

        private void EditImage(ref ImageData imageData_gse)
        {
            if (imageData_gse != null)
            {
                if (!Greenshot.IniFile.IniConfig.IsInited)
                    Greenshot.IniFile.IniConfig.Init();

                GreenshotPlugin.Core.CoreConfiguration conf = Greenshot.IniFile.IniConfig.GetIniSection<GreenshotPlugin.Core.CoreConfiguration>(); ;
                conf.OutputFileFilenamePattern = "${title}";
                conf.OutputFilePath = Program.ScreenshotsPath;

                Greenshot.Plugin.ICapture capture = new GreenshotPlugin.Core.Capture();
                capture.Image = imageData_gse.Image;
                capture.CaptureDetails.Filename = Path.Combine(Program.ScreenshotsPath, imageData_gse.Filename);
                capture.CaptureDetails.Title =
                    Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
                capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
                capture.CaptureDetails.AddMetaData("source", "file");

                var surface = new Greenshot.Drawing.Surface(capture);
                var editor = new Greenshot.ImageEditorForm(surface, true) { Icon = this.Icon };

                editor.SetImagePath(capture.CaptureDetails.Filename);
                editor.Visible = false; // required before ShowDialog
                editor.ShowDialog();

                imageData_gse.Image = editor.GetImageForExport();
            }
        }

        private string FindTagByHotkey(EHotkey hotkey)
        {
            return FindAppWorkflowByHotkey(hotkey).HotkeyConfig.Tag;
        }

        private Workflow FindAppWorkflowByHotkey(EHotkey hotkey)
        {
            return Program.Settings.Workflows1.FirstOrDefault(x => x.Hotkey == hotkey);
        }
    }
}