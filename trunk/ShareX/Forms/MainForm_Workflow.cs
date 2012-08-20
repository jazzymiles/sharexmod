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
using HelpersLibMod;
using ScreenCapture;
using ShareX.HelperClasses;
using UploadersLib;

namespace ShareX
{
    public partial class MainForm
    {
        internal void InitHotkeys()
        {
            if (SettingsManager.ConfigWorkflows == null)
                SettingsManager.WorkflowsResetEvent.WaitOne();

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
                Workflow wfLastRegion = new Workflow(HelpersLib.Hotkeys2.EHotkey.LastRegion, new HotkeySetting());

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
                SettingsManager.ConfigWorkflows.Workflows.Add(wfLastRegion);

                foreach (Workflow wf in SettingsManager.ConfigWorkflows.Workflows)
                {
                    if (wf.Hotkey == HelpersLib.Hotkeys2.EHotkey.ClipboardUpload || wf.Hotkey == HelpersLib.Hotkeys2.EHotkey.FileUpload)
                    {
                        wf.Subtasks |= Subtask.UploadToRemoteHost;
                        wf.AfterUploadTasks |= AfterUploadTasks.CopyURLToClipboard;
                    }
                    else
                        wf.Settings.PerformGlobalAfterCaptureTasks = true;
                }
            } // if Workflows.Count == 0

            Workflow wfLastRegion_r200 = FindAppWorkflowByHotkey(HelpersLib.Hotkeys2.EHotkey.LastRegion);
            if (wfLastRegion_r200 == null)
            {
                wfLastRegion_r200 = new Workflow(HelpersLib.Hotkeys2.EHotkey.LastRegion, new HotkeySetting());
                wfLastRegion_r200.Settings.PerformGlobalAfterCaptureTasks = true;
                SettingsManager.ConfigWorkflows.Workflows.Insert(13, wfLastRegion_r200);
            }

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
            jobs_wf.Workflow = wf.Clone();

            switch (jobs_wf.Workflow.Hotkey)
            {
                case HelpersLib.Hotkeys2.EHotkey.ActiveMonitor:
                    imagedata_wf = CaptureActiveMonitor(autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.ActiveWindow:
                    imagedata_wf = CaptureActiveWindow(autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.ClipboardUpload:
                    jobs_wf.InputType = EInputType.Clipboard;
                    break;

                case HelpersLib.Hotkeys2.EHotkey.DiamondRegion:
                    break;

                case HelpersLib.Hotkeys2.EHotkey.EllipseRegion:
                    imagedata_wf = CaptureRegion(new EllipseRegion(), autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.FileUpload:
                    jobs_wf.InputType = EInputType.FileSystem;
                    break;

                case HelpersLib.Hotkeys2.EHotkey.FreeHandRegion:
                    imagedata_wf = CaptureRegion(new FreeHandRegion(), autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.FullScreen:
                    imagedata_wf = CaptureScreen(autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.LastRegion:
                    imagedata_wf = CaptureLastRegion(autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.PolygonRegion:
                    imagedata_wf = CaptureRegion(new PolygonRegion(), autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.RectangleRegion:
                    imagedata_wf = CaptureRegion(new RectangleRegion(), autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.RoundedRectangleRegion:
                    imagedata_wf = CaptureRegion(new RoundedRectangleRegion(), autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.TriangleRegion:
                    imagedata_wf = CaptureRegion(new TriangleRegion(), autoHideForm);
                    break;

                case HelpersLib.Hotkeys2.EHotkey.WindowRectangle:
                    imagedata_wf = WindowRectangleCapture(autoHideForm);
                    break;
            }

            if (jobs_wf.Workflow.Settings.PerformGlobalAfterCaptureTasks)
            {
                jobs_wf.Workflow.Subtasks |= SettingsManager.ConfigCore.AfterCaptureTasks;
                jobs_wf.Workflow.AfterUploadTasks |= SettingsManager.ConfigCore.AfterUploadTasks;
            }

            AfterHotkeyPressed(imagedata_wf, jobs_wf);
        }

        private void AfterHotkeyPressed(ImageData imageData, AfterCaptureActivity act = null)
        {
            if (act.Workflow.Settings.DestConfig.FileUploaders.Count > 0)
            {
                if (act.Workflow.Settings.DestConfig.ImageUploaders.Count == 0)
                    act.Workflow.Settings.DestConfig.ImageUploaders.Add(UploadersLib.ImageDestination.FileUploader);
                if (act.Workflow.Settings.DestConfig.TextUploaders.Count == 0)
                    act.Workflow.Settings.DestConfig.TextUploaders.Add(UploadersLib.TextDestination.FileUploader);
            }

            if (imageData != null)
            {
                if (act.Workflow.Subtasks == Subtask.None)
                    act.Workflow.Subtasks |= SettingsManager.ConfigCore.AfterCaptureTasks;
                if (act.Workflow.Settings.DestConfig.ImageUploaders.Count > 0)
                    act.Workflow.Subtasks |= Subtask.UploadToRemoteHost;
                log.Debug("After Capture initiated.");
                AfterCapture(imageData, act);
            }
            else if (act.InputType == EInputType.Clipboard)
            {
                if (act.Workflow.Subtasks == Subtask.None)
                    act.Workflow.Subtasks |= SettingsManager.ConfigCore.AfterCaptureTasks;

                log.Debug("ClipboardUpload initiated.");
                UploadManager.ClipboardUpload(act);
            }
            else if (act.InputType == EInputType.FileSystem)
            {
                log.Debug("UploadFile initiated.");
                UploadManager.UploadFile(act);
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