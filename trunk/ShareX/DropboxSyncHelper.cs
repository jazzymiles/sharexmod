using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using Newtonsoft.Json;
using UploadersLib;
using UploadersLib.FileUploaders;

namespace ShareX
{
    public class DropboxSyncHelper
    {
        Dropbox fileUploader = null;

        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string pathDropboxSettings = Helpers.CombineURL(Application.ProductName, Program.SettingsFileName);
        string pathDropboxUploadersConfig = Helpers.CombineURL(Application.ProductName, Program.UploadersConfigFileName);

        public DropboxSyncHelper()
        {
            fileUploader = new Dropbox(Program.UploadersConfig.DropboxOAuthInfo, Application.ProductName, Program.UploadersConfig.DropboxAccountInfo);
        }

        public static MemoryStream GetMemoryStream(object obj)
        {
            try
            {
                lock (obj)
                {
                    MemoryStream ms = new MemoryStream();
                    StreamWriter streamWriter = new StreamWriter(ms);
                    JsonWriter jsonWriter = new JsonTextWriter(streamWriter);
                    jsonWriter.Formatting = Formatting.Indented;
                    new JsonSerializer().Serialize(jsonWriter, obj);
                    jsonWriter.Flush();
                    return ms;
                }
            }
            catch (Exception e)
            {
                log.Error("Error", e);
            }
            return null;
        }

        private void bwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            Settings settings = fileUploader.DownloadFile<Settings>(pathDropboxSettings);
            if (settings != null)
            {
                // override these settings from local copy
                settings.ProxySettings = Program.Settings.ProxySettings;
                settings.ScreenshotsPath = Program.Settings.ScreenshotsPath;
                settings.CustomHistoryPath = Program.Settings.CustomHistoryPath;
                settings.CustomUploadersConfigPath = Program.Settings.CustomUploadersConfigPath;
                settings.FolderMonitorPath = Program.Settings.FolderMonitorPath;

                Program.Settings = settings;
                log.InfoFormat("Updated Settings using {0}", pathDropboxSettings);
                e.Result = settings;
            }
            UploadersConfig config = fileUploader.DownloadFile<UploadersConfig>(pathDropboxUploadersConfig);
            if (config != null)
            {
                Program.UploadersConfig = config;
                log.InfoFormat("Updated Uploaders Config using {0}", pathDropboxUploadersConfig);
            }
        }

        private void bwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RebuildWorkflowTags(e.Result as Settings);
        }

        /// <summary>
        /// This method overwrites the tags read from Dropbox with the tags already registered on the system.
        /// We do this because it is more efficient that unregistering and re-registering hotkeys.
        /// </summary>
        private void RebuildWorkflowTags(Settings settingsDropbox)
        {
            if (settingsDropbox != null)
            {
                int min = Math.Min(settingsDropbox.Workflows1.Count, Program.Settings.Workflows1.Count);
                for (int i = 0; i < min; i++)
                {
                    Program.Settings.Workflows1[i].HotkeyConfig.Tag = settingsDropbox.Workflows1[i].HotkeyConfig.Tag;
                    log.DebugFormat("Updated Workflow: {0}, ID: {1}", Program.Settings.Workflows1[i].HotkeyConfig.Description, Program.Settings.Workflows1[i].HotkeyConfig.Tag);
                }
            }
        }

        private void bwSave_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a copy of Settins
            IClone cm = new CloneManager();

            Settings settings = cm.Clone(Program.Settings);
            fileUploader.Upload(GetMemoryStream(settings), pathDropboxSettings);
            log.InfoFormat("{0} updated.", pathDropboxSettings);

            UploadersConfig config = cm.Clone(Program.UploadersConfig);
            fileUploader.Upload(GetMemoryStream(config), pathDropboxUploadersConfig);
            log.InfoFormat("{0} updated.", pathDropboxUploadersConfig);
        }

        public void Load()
        {
            if (fileUploader != null)
            {
                BackgroundWorker bwLoad = new BackgroundWorker();
                bwLoad.DoWork += new DoWorkEventHandler(bwLoad_DoWork);
                bwLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoad_RunWorkerCompleted);
                bwLoad.RunWorkerAsync();
            }
        }

        public void Save()
        {
            if (fileUploader != null)
            {
                BackgroundWorker bwSave = new BackgroundWorker();
                bwSave.DoWork += new DoWorkEventHandler(bwSave_DoWork);
                bwSave.RunWorkerAsync();
            }
        }
    }
}