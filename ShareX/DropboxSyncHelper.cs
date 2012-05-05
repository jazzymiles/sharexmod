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
        Dropbox dropbox = null;
        string pathDropboxSettings = Helpers.CombineURL(Application.ProductName, Program.SettingsFileName);
        string pathDropboxUploadersConfig = Helpers.CombineURL(Application.ProductName, Program.UploadersConfigFileName);

        public DropboxSyncHelper()
        {
            dropbox = new Dropbox(Program.UploadersConfig.DropboxOAuthInfo, Application.ProductName, Program.UploadersConfig.DropboxAccountInfo);
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
                log4netHelper.Log.Error("Error", e);
            }
            return null;
        }

        private void bwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (dropbox.DownloadFile(pathDropboxSettings, ms))
                {
                    ms.Position = 0;
                    Settings settings = Settings.Load(ms);
                    if (settings != null)
                    {
                        // override these settings from local copy
                        settings.ProxySettings = Program.Settings.ProxySettings;
                        settings.ScreenshotsPath = Program.Settings.ScreenshotsPath;
                        settings.CustomHistoryPath = Program.Settings.CustomHistoryPath;
                        settings.CustomUploadersConfigPath = Program.Settings.CustomUploadersConfigPath;
                        settings.FolderMonitorPath = Program.Settings.FolderMonitorPath;

                        Program.Settings = settings;
                        log4netHelper.Log.InfoFormat("Updated Settings using {0}", pathDropboxSettings);
                        e.Result = settings;
                    }
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                if (dropbox.DownloadFile(pathDropboxUploadersConfig, ms))
                {
                    ms.Position = 0;
                    UploadersConfig config = UploadersConfig.Load(ms);
                    if (config != null)
                    {
                        Program.UploadersConfig = config;
                        log4netHelper.Log.InfoFormat("Updated Uploaders Config using {0}", pathDropboxUploadersConfig);
                    }
                }
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
                int min = Math.Min(settingsDropbox.Workflows1.Count, FormsHelper.Main.HotkeyList.Count);
                for (int i = 0; i < min; i++)
                {
                    FormsHelper.Main.HotkeyList[i].Tag = settingsDropbox.Workflows1[i].HotkeyConfig.Tag;
                    log4netHelper.Log.DebugFormat("Updated Workflow: {0}, ID: {1}", Program.Settings.Workflows1[i].HotkeyConfig.Description, Program.Settings.Workflows1[i].HotkeyConfig.Tag);
                }
            }
        }

        private void bwSave_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a copy of Settins
            IClone cm = new CloneManager();

            Settings settings = cm.Clone(Program.Settings);
            dropbox.Upload(GetMemoryStream(settings), pathDropboxSettings);
            log4netHelper.Log.InfoFormat("{0} updated.", pathDropboxSettings);

            UploadersConfig config = cm.Clone(Program.UploadersConfig);
            dropbox.Upload(GetMemoryStream(config), pathDropboxUploadersConfig);
            log4netHelper.Log.InfoFormat("{0} updated.", pathDropboxUploadersConfig);
        }

        public void Load()
        {
            if (dropbox != null)
            {
                BackgroundWorker bwLoad = new BackgroundWorker();
                bwLoad.DoWork += new DoWorkEventHandler(bwLoad_DoWork);
                bwLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoad_RunWorkerCompleted);
                bwLoad.RunWorkerAsync();
            }
        }

        public void Save()
        {
            if (dropbox != null)
            {
                BackgroundWorker bwSave = new BackgroundWorker();
                bwSave.DoWork += new DoWorkEventHandler(bwSave_DoWork);
                bwSave.RunWorkerAsync();
            }
        }
    }
}