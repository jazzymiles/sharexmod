using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using Newtonsoft.Json;
using UploadersLib;
using UploadersLib.FileUploaders;

namespace ShareX
{
    public class DropboxSyncHelper
    {
        string dropBoxSettingsPath = Helpers.CombineURL(Application.ProductName, Program.SettingsFileName);
        string dropBoxUploadersConfigPath = Helpers.CombineURL(Application.ProductName, Program.UploadersConfigFileName);
        FileUploader fileUploader = null;
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        public static Settings GetSettings(object obj)
        {
            Settings settings = null;
            return settings;
        }

        private void bwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            Dropbox dropBox = fileUploader as Dropbox;

            Settings settings = dropBox.DownloadFile<Settings>(dropBoxSettingsPath);
            if (settings != null)
            {
                settings.Paths = Program.Settings.Paths;
                Program.Settings = settings;
                log.InfoFormat("Updated Settings using {0}", dropBoxSettingsPath);
            }
            UploadersConfig config = dropBox.DownloadFile<UploadersConfig>(dropBoxUploadersConfigPath);
            if (config != null)
            {
                Program.UploadersConfig = config;
                log.InfoFormat("Updated Uploaders Config using {0}", dropBoxUploadersConfigPath);
            }
        }

        private void bwSave_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a copy of Settins
            IClone cm = new CloneManager();
            Settings settings = cm.Clone(Program.Settings);
            // Empty the Paths
            settings.Paths = new SettingsPaths();
            fileUploader.Upload(GetMemoryStream(settings), dropBoxSettingsPath);
            UploadersConfig config = cm.Clone(Program.UploadersConfig);
            fileUploader.Upload(GetMemoryStream(config), dropBoxUploadersConfigPath);
        }

        public void Load()
        {
            if (fileUploader != null)
            {
                BackgroundWorker bwLoad = new BackgroundWorker();
                bwLoad.DoWork += new DoWorkEventHandler(bwLoad_DoWork);
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

        public void Sync()
        {
            if (fileUploader != null)
            {
            }
        }
    }
}