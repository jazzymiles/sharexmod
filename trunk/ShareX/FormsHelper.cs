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
        private static UploadersConfigForm _UploadersConfigWindow;

        public static MainForm Main
        {
            get
            {
                if (_MainForm == null || _MainForm.IsDisposed)
                    _MainForm = new MainForm() { Icon = Resources.ShareXSmallIcon };

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
                    _OptionsWindow = new OptionsWindow() { Icon = Resources.ShareXSmallIcon };

                return _OptionsWindow;
            }
            set
            {
                _OptionsWindow = value;
            }
        }

        public static UploadersConfigForm UploadersConfigWindow
        {
            get
            {
                if (_UploadersConfigWindow == null || _UploadersConfigWindow.IsDisposed)
                    _UploadersConfigWindow = new UploadersConfigForm(Program.UploadersConfig, new UploadersAPIKeys()) { Icon = Resources.ShareXSmallIcon };

                return _UploadersConfigWindow;
            }
            set
            {
                _UploadersConfigWindow = value;
            }
        }

        public static void ShowOptions()
        {
            Options.Show();
            Options.Activate();
        }

        public static void ShowUploadersConfig()
        {
            if (Program.UploadersConfig == null)
            {
                Program.UploaderSettingsResetEvent.WaitOne();
            }

            UploadersConfigWindow.ShowDialog();
            UploadersConfigWindow.Activate();
            UploadersConfigWindow.Config.SaveAsync(Program.UploadersConfigFilePath);
        }
    }
}