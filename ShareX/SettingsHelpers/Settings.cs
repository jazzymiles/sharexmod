#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2012 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using HelpersLib;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using ScreenCapture;
using UploadersLib;
using UploadersLib.HelperClasses;

namespace ShareX
{
    /// <summary>
    /// Core settings that are required at the initial loading of the application
    /// </summary>
    public class Settings : SettingsBase<Settings>
    {
        #region Main Form

        public ImageDestination ImageUploaderDestination = ImageDestination.Imgur;
        public TextDestination TextUploaderDestination = TextDestination.Pastebin;
        public FileDestination FileUploaderDestination = FileDestination.Dropbox;
        public UrlShortenerType URLShortenerDestination = UrlShortenerType.Google;
        public SocialNetworkingService SocialNetworkingServiceDestination = SocialNetworkingService.Twitter;

        public bool ShowClipboardContentViewer = true;

        #endregion Main Form

        #region Settings Form

        // General

        public bool ShowTray = true;
        public bool AutoCheckUpdate = true;
        public bool PlaySoundAfterCapture = true;
        public bool PlaySoundAfterUpload = true;
        public bool ShowBalloonAfterUpload = true;

        /// <summary>
        /// Added for compatibility from r125
        /// </summary>
        public List<Workflow> Workflows1 = new List<Workflow>();

        // Upload

        public bool UseCustomUploadersConfigPath = false;
        public string CustomUploadersConfigPath = string.Empty;
        public int UploadLimit = 5;
        public int BufferSizePower = 3;

        // Image - Location

        public string ScreenshotsPath = Program.ScreenshotsRootPath;

        // Clipboard upload

        public bool ClipboardUploadAutoDetectURL = true;

        // Test: %y %mo %mon %mon2 %d %h %mi %s %ms %w %w2 %pm %rn %ra %width %height %app %ver

        public string NameFormatPattern = "%y-%mo-%d_%h-%mi-%s";
        public string NameFormatPatternOther = "%y-%mo-%d_%h-%mi-%s";

        // Capture

        public bool ShowCursor = false;
        public bool CaptureTransparent = true;
        public bool CaptureShadow = true;
        public OutputEnum Outputs = OutputEnum.Clipboard | OutputEnum.LocalDisk | OutputEnum.RemoteHost;
        public Subtask AfterCaptureTasks = Subtask.CopyImageToClipboard | Subtask.SaveToFile | Subtask.UploadToRemoteHost;
        public AfterUploadTasks AfterUploadTasks = AfterUploadTasks.CopyURLToClipboard;
        public string SaveImageSubFolderPattern = "%y-%mo";
        public SurfaceOptions SurfaceOptions = new SurfaceOptions() { QuickCrop = true, IncludeControls = true };

        // History

        public bool SaveHistory = true;
        public string CustomHistoryPath = string.Empty;
        public bool UseCustomHistoryPath = false;
        public int HistoryMaxItemCount = -1;

        // Proxy

        public ProxyInfo ProxySettings = new ProxyInfo();

        // Column Headers

        public int[] ColumnWidths = { 150, 75, 47, 65, 63, 70, 50, 102, 239 };

        // Advanced

        [Category(ComponentModelStrings.App), DefaultValue(false), Description("If you have configured Dropbox, then this setting will synchronize uploaders configuration and application settings except for paths.")]
        public bool DropboxSync { get; set; }

        [Category(ComponentModelStrings.App), DefaultValue(View.Tile), Description("You can choose LargeIcon to enable Thumbnail mode")]
        public View ListViewMode { get; set; }

        [Category(ComponentModelStrings.SettingsInteraction), DefaultValue(true), Description("Show after capture wizard. Dynamically choose actions after capture")]
        public bool ShowAfterCaptureWizard { get; set; }

        [Category(ComponentModelStrings.SettingsInteraction), DefaultValue(true), Description("Show clipboard options after host upload is completed. Dynamically choose which link format to be copied to the clipboad.")]
        public bool ShowClipboardOptionsWizard { get; set; }

        [Category(ComponentModelStrings.InputsClipboard), DefaultValue(true), Description("When a folder path is in the clipboard, upload the folder index instead of the folder path as part of Clipboard Upload.")]
        public bool IndexFolderWhenPossible { get; set; }

        [Category(ComponentModelStrings.InputsWatchFolder), DefaultValue(false), Description("Automatically upload files saved in to this folder.")]
        public bool FolderMonitoring { get; set; }

        [Category(ComponentModelStrings.InputsWatchFolder), Description("Folder monitor path where files automatically get uploaded.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string FolderMonitorPath { get; set; }

        [Category(ComponentModelStrings.FileNaming), DefaultValue(100), Description("Maximum file name length")]
        public int MaxFilenameLength { get; set; }

        [Category(ComponentModelStrings.URLShortener), DefaultValue(80), Description("Maximum character length at which the URL will be shortened.")]
        public int MaximumURLLength { get; set; }

        [Category(ComponentModelStrings.AppPasswords), DefaultValue(true), Description("Encrypt passwords using AES")]
        public bool PasswordsSecureUsingEncryption { get; set; }

        [Browsable(false), Category(ComponentModelStrings.AppPasswords), DefaultValue(EncryptionStrength.High), Description("Strength can be Low = 12,8 Medium = 192, or High = 256")]
        public EncryptionStrength PasswordsEncryptionStrength { get; set; }

        [Browsable(false), Category(ComponentModelStrings.AppPasswords), DefaultValue("password"), Description("If this SamplePassword displayed as 'password' then configuration is not encrypted.")]
        public string TestPassword { get; set; }

        #endregion Settings Form

        #region Methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public Settings()
        {
            ApplyDefaultValues(this);
        }

        public void CryptPasswords(bool doEncrypt)
        {
            bool isEncrypted = TestPassword != "password";

            if (doEncrypt && isEncrypted || !doEncrypt && !isEncrypted)
            {
                // ensure encrupted passwords are not encrypted again or decrypted passwords are not decrypted again
                return;
            }

            DebugHelper.WriteLine((doEncrypt ? "Encrypting " : "Decrypting") + " passwords.");

            CryptKeys crypt = new CryptKeys() { KeySize = this.PasswordsEncryptionStrength };

            this.TestPassword = doEncrypt ? crypt.Encrypt(TestPassword) : crypt.Decrypt(TestPassword);

            this.ProxySettings.Password = doEncrypt ? crypt.Encrypt(this.ProxySettings.Password) : crypt.Decrypt(this.ProxySettings.Password);
        }

        #endregion Methods

        #region I/O Methods

        public static new Settings Load(string filePath)
        {
            Settings config = SettingsBase<Settings>.Load(filePath);
            if (config.PasswordsSecureUsingEncryption) config.CryptPasswords(false);
            return config;
        }

        public override bool Save(string filePath)
        {
            bool result;
            if (PasswordsSecureUsingEncryption) CryptPasswords(true);
            result = base.Save(filePath);
            if (PasswordsSecureUsingEncryption) CryptPasswords(false);
            return result;
        }

        #endregion I/O Methods
    }
}