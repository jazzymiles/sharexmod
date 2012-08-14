using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib.Hotkeys2;
using HistoryLib;
using ShareX.Forms;
using ShareX.HelperClasses;
using ShareX.Properties;
using UploadersLib;

namespace ShareX
{
    public static class FormsHelper
    {
        private static MainForm _MainForm;
        private static OptionsWindow _OptionsWindow;
        private static WorkflowManager _WorkflowManager;

        public static MainForm Main
        {
            get
            {
                if (_MainForm == null || _MainForm.IsDisposed)
                    _MainForm = new MainForm() { Icon = Resources.ShareX };

                return _MainForm;
            }
            set
            {
                _MainForm = value;
            }
        }

        /// <summary>
        /// Options Window must use ShowDialog() to ensure latest settings are shown in the UI
        /// Do not modify it to support Show()
        /// </summary>
        public static void ShowOptions()
        {
            _OptionsWindow = new OptionsWindow() { Icon = Resources.ShareX };
            _OptionsWindow.ShowDialog();
            _OptionsWindow.Activate();
        }

        /// <summary>
        /// Workflow Manager must use ShowDialog() to ensure latest settings are shown in the UI
        /// Do not modify it to support Show()
        /// </summary>
        public static void ShowWorkflowManager()
        {
            _WorkflowManager = new WorkflowManager(FormsHelper.Main.HotkeyManager) { Icon = Resources.ShareX };
            _WorkflowManager.ShowDialog();
            _WorkflowManager.Activate();

            #region Workflows

            if (FormsHelper.Main.HotkeyManager != null)
            {
                List<Workflow> workflowsNew = new List<Workflow>();

                foreach (Workflow wf in FormsHelper.Main.HotkeyManager.Workflows)
                {
                    Workflow wf2 = SettingsManager.ConfigWorkflows.Workflows.FirstOrDefault(x => x.HotkeyConfig.Tag == wf.HotkeyConfig.Tag);
                    if (wf2 == null)
                        workflowsNew.Add(wf);
                }

                foreach (Workflow wf in workflowsNew)
                {
                    string tag = wf.HotkeyConfig.Tag;
                    FormsHelper.Main.UnregisterHotkey(wf.HotkeyConfig.Hotkey);
                    FormsHelper.Main.HotkeyManager.AddHotkey(wf, () => FormsHelper.Main.DoWork(tag, false));
                }

                List<Workflow> workflowOld = new List<Workflow>();
                foreach (Workflow wf in SettingsManager.ConfigWorkflows.Workflows)
                {
                    Workflow wf2 = FormsHelper.Main.HotkeyManager.Workflows.FirstOrDefault(x => x.HotkeyConfig.Tag == wf.HotkeyConfig.Tag);
                    if (wf2 == null)
                        workflowOld.Add(wf);
                }

                foreach (Workflow wf in workflowOld)
                {
                    FormsHelper.Main.UnregisterHotkey(wf.HotkeyConfig.Hotkey);
                }

                SettingsManager.ConfigWorkflows.Workflows.Clear();
                SettingsManager.ConfigWorkflows.Workflows.AddRange(FormsHelper.Main.HotkeyManager.Workflows);
            }

            #endregion Workflows
        }

        public static void OptionsSettingsLoad()
        {
            if (_OptionsWindow != null)
                _OptionsWindow.LoadSettings();
        }

        public static void ShowUploadersConfig()
        {
            if (SettingsManager.ConfigUploaders == null)
            {
                SettingsManager.UploaderSettingsResetEvent.WaitOne();
            }

            UploadersConfigForm uploaderConfig = new UploadersConfigForm(SettingsManager.ConfigUploaders, new UploadersAPIKeys()) { Icon = Resources.ShareX };
            uploaderConfig.ShowDialog();
            uploaderConfig.Activate();

            SettingsManager.SaveUploadersConfigAsync();
            Main.AfterUploadersConfigClosed();
            DropboxSyncHelper.SaveAsync();
        }

        public static void ShowLog()
        {
            HelpersLib.log4netHelpers.log4netViewer_ListView viewer = new HelpersLib.log4netHelpers.log4netViewer_ListView();
            viewer.Icon = Resources.ShareX;
            viewer.Show();
        }
    }
}