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
using UploadersLib;

namespace ShareX
{
    public partial class MainForm
    {
        internal void InitHotkeys()
        {
            HotkeyManager = new HotkeyManager(this);

            if (SettingsManager.ConfigWorkflows.Workflows.Count == 0)
            {
                Workflow wfClipboardUpload = new Workflow(HelpersLib.Hotkeys2.EHotkey.ClipboardUpload, Program.HotkeyClipboardUpload);
                Workflow wfFileUpload = new Workflow(HelpersLib.Hotkeys2.EHotkey.FileUpload, Program.HotkeyFileUpload);

                Workflow wfPrintScreen = new Workflow(HelpersLib.Hotkeys2.EHotkey.FullScreen, Program.HotkeyPrintScreen);
                Workflow wfActiveMonitor = new Workflow(HelpersLib.Hotkeys2.EHotkey.ActiveMonitor, Program.HotkeyActiveMonitor);
                Workflow wfActiveWindow = new Workflow(HelpersLib.Hotkeys2.EHotkey.ActiveWindow, Program.HotkeyActiveWindow);
                Workflow wfWindowRectangle = new Workflow(HelpersLib.Hotkeys2.EHotkey.WindowRectangle, Program.HotkeyWindowRectangle);

                Workflow wfRectangleRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.RectangleRegion, Program.HotkeyRectangleRegion);
                Workflow wfRoundedRectangleRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.RoundedRectangleRegion, new HotkeySetting());
                Workflow wfEllipseRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.EllipseRegion, new HotkeySetting());
                Workflow wfTriangleRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.TriangleRegion, new HotkeySetting());
                Workflow wfDiamondRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.DiamondRegion, new HotkeySetting());
                Workflow wfPolygonRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.PolygonRegion, new HotkeySetting());
                Workflow wfFreeHandRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.FreeHandRegion, new HotkeySetting());

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

                SettingsManager.ConfigWorkflows.Workflows.Add(wfClipboardUpload);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfFileUpload);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfPrintScreen);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfActiveWindow);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfActiveMonitor);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfWindowRectangle);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfRectangleRegion);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfRoundedRectangleRegion);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfEllipseRegion);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfTriangleRegion);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfDiamondRegion);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfPolygonRegion);
                SettingsManager.ConfigWorkflows.Workflows.Add(wfFreeHandRegion);

                foreach (Workflow wf in SettingsManager.ConfigWorkflows.Workflows)
                {
                    if (wf.Hotkey == HelpersLib.Hotkeys2.EHotkey.ClipboardUpload || wf.Hotkey == HelpersLib.Hotkeys2.EHotkey.FileUpload)
                        wf.Activities.Add(EActivity.UploadToRemoteHost);
                    else
                        wf.Activities.Add(EActivity.AfterCaptureTasks);
                }
            } // if Workflows.Count == 0

            foreach (Workflow wf in SettingsManager.ConfigWorkflows.Workflows)
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
            Workflow wf = SettingsManager.ConfigWorkflows.Workflows.FirstOrDefault(x => x.HotkeyConfig.Tag == tag);
            ImageData imagedata_wf = null;
            string fpImg = string.Empty;

            if (wf == null)
            {
                log.Error("Workflow cannot be found!");
                return;
            }

            AfterCaptureActivity jobs_wf = new AfterCaptureActivity();
            jobs_wf.Workflow = new Workflow();
            jobs_wf.Workflow.Activities = wf.Activities;
            jobs_wf.Workflow.DestConfig = Helpers.Clone(wf.DestConfig) as DestConfig;

            foreach (EActivity act in jobs_wf.Workflow.Activities)
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
                        jobs_wf.Subtasks |= SettingsManager.ConfigCore.AfterCaptureSubtasks;
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

                    default:
                        FormsHelper.ShowLog();
                        log.ErrorFormat("{0} is not  yet implemented.", act.ToString());
                        break;
                }
            }

            AfterHotkeyPressed(imagedata_wf, jobs_wf);
        }

        private void AfterHotkeyPressed(ImageData imageData, AfterCaptureActivity act = null)
        {
            if (act.Workflow.DestConfig.FileUploaders.Count > 0)
            {
                if (act.Workflow.DestConfig.ImageUploaders.Count == 0)
                    act.Workflow.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.FileUploader);
                if (act.Workflow.DestConfig.TextUploaders.Count == 0)
                    act.Workflow.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.FileUploader);
            }

            if (imageData != null)
            {
                if (act.Subtasks == Subtask.None)
                    act.Subtasks |= SettingsManager.ConfigCore.AfterCaptureSubtasks;
                if (act.Workflow.DestConfig.ImageUploaders.Count > 0)
                    act.Subtasks |= Subtask.UploadImageToHost;
                log.Debug("After Capture initiated.");
                AfterCapture(imageData, act);
            }
            else if (act.InputType == EInputType.Clipboard)
            {
                if (act.Subtasks == Subtask.None)
                    act.Subtasks |= SettingsManager.ConfigCore.AfterCaptureSubtasks;

                log.Debug("ClipboardUpload initiated.");
                UploadManager.ClipboardUpload(act);
            }
            else if (act.InputType == EInputType.FileSystem)
            {
                log.Debug("UploadFile initiated.");
                UploadManager.UploadFile(act);
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

        private string FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey hotkey)
        {
            return FindAppWorkflowByHotkey(hotkey).HotkeyConfig.Tag;
        }

        private Workflow FindAppWorkflowByHotkey(HelpersLib.Hotkeys2.EHotkey hotkey)
        {
            return SettingsManager.ConfigWorkflows.Workflows.FirstOrDefault(x => x.Hotkey == hotkey);
        }
    }
}