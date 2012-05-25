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
        string pathDropboxSettings = Helpers.CombineURL(Application.ProductName, SettingsManager.ConfigCoreFileName);
        string pathDropboxUploadersConfig = Helpers.CombineURL(Application.ProductName, SettingsManager.ConfigUploadersFileName);

        public DropboxSyncHelper()
        {
            if (SettingsManager.ConfigUploaders == null)
                SettingsManager.UploaderSettingsResetEvent.WaitOne();

            dropbox = new Dropbox(SettingsManager.ConfigUploaders.DropboxOAuthInfo, Application.ProductName, SettingsManager.ConfigUploaders.DropboxAccountInfo);
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
            try
            {
                Settings dbSettings = Load<Settings>(pathDropboxSettings);
                if (dbSettings != null)
                {
                    // override these settings from local copy
                    dbSettings.ProxySettings = SettingsManager.ConfigCore.ProxySettings;
                    dbSettings.ScreenshotsPath = SettingsManager.ConfigCore.ScreenshotsPath;
                    dbSettings.CustomHistoryPath = SettingsManager.ConfigCore.CustomHistoryPath;
                    dbSettings.CustomUploadersConfigPath = SettingsManager.ConfigCore.CustomUploadersConfigPath;
                    dbSettings.FolderMonitorPath = SettingsManager.ConfigCore.FolderMonitorPath;
                    SettingsManager.ConfigCore = dbSettings;
                }

                UploadersConfig dbConfigUploaders = Load<UploadersConfig>(pathDropboxUploadersConfig);
                if (dbConfigUploaders != null)
                    SettingsManager.ConfigUploaders = dbConfigUploaders;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public T Load<T>(string path)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                if (dropbox.DownloadFile(path, ms))
                {
                    ms.Position = 0;
                    Object tmp = SettingsHelper.Load<T>(ms, SerializationType.Json);
                    if (tmp != null)
                    {
                        log.InfoFormat("Loaded {0}", pathDropboxUploadersConfig);
                        return (T)tmp;
                    }
                }
            }

            return default(T);
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

            Settings settings = cm.Clone(SettingsManager.ConfigCore);
            dropbox.Upload(GetMemoryStream(settings), pathDropboxSettings);
            log.InfoFormat("{0} updated.", pathDropboxSettings);

            UploadersConfig config = cm.Clone(SettingsManager.ConfigUploaders);
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

        public static void SaveAsync()
        {
            if (SettingsManager.ConfigCore.DropboxSync)
            {
                new DropboxSyncHelper().Save();
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