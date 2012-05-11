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
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Dropbox dropbox = null;
        string pathDropboxSettings = Helpers.CombineURL(Application.ProductName, Program.SettingsFileName);
        string pathDropboxUploadersConfig = Helpers.CombineURL(Application.ProductName, Program.UploadersConfigFileName);

        public DropboxSyncHelper()
        {
            if (Program.UploadersConfig == null)
                Program.UploaderSettingsResetEvent.WaitOne();

            dropbox = new Dropbox(Program.UploadersConfig.DropboxOAuthInfo, Application.ProductName, Program.UploadersConfig.DropboxAccountInfo);
        }

        private static MemoryStream GetMemoryStream(object obj)
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
                        log.InfoFormat("Updated Settings using {0}", pathDropboxSettings);
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
                        log.InfoFormat("Updated Uploaders Config using {0}", pathDropboxUploadersConfig);
                    }
                }
            }
        }

        private void bwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormsHelper.Main.LoadSettings();
            FormsHelper.Main.InitHotkeys();
            FormsHelper.Options.LoadSettings();
        }

        private void bwSave_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a copy of Settings
            IClone cm = new CloneManager();

            Settings settings = cm.Clone(Program.Settings);
            dropbox.Upload(GetMemoryStream(settings), pathDropboxSettings);
            log.InfoFormat("{0} updated.", pathDropboxSettings);

            UploadersConfig config = cm.Clone(Program.UploadersConfig);
            dropbox.Upload(GetMemoryStream(config), pathDropboxUploadersConfig);
            log.InfoFormat("{0} updated.", pathDropboxUploadersConfig);
        }

        public void InitHotkeys()
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