using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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