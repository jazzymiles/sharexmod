using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShareX.Forms;
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

        public static OptionsWindow Options
        {
            get
            {
                if (_OptionsWindow == null || _OptionsWindow.IsDisposed)
                    _OptionsWindow = new OptionsWindow() { Icon = Resources.ShareX };

                return _OptionsWindow;
            }
            set
            {
                _OptionsWindow = value;
            }
        }

        public static void ShowOptions()
        {
            if (_OptionsWindow != null && !_OptionsWindow.IsDisposed)
            {
                _OptionsWindow.Show();
                _OptionsWindow.Focus();
            }
            else
                Options.ShowDialog();

            DropboxSyncHelper.SaveAsync();

            Main.ReloadConfig();
            Main.ReloadOutputsMenu();
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