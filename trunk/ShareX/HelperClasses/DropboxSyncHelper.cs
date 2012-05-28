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
using ShareX.SettingsHelpers;
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
        string pathDropboxWorkflowsConfig = Helpers.CombineURL(Application.ProductName, SettingsManager.ConfigWorkflowsFileName);
        string pathDropboxUserConfig = Helpers.CombineURL(Application.ProductName, SettingsManager.ConfigUserFileName);

        public DropboxSyncHelper()
        {
            if (SettingsManager.ConfigUploaders == null)
                SettingsManager.LoadUploadersConfig();

            dropbox = new Dropbox(SettingsManager.ConfigUploaders.DropboxOAuthInfo, Application.ProductName, SettingsManager.ConfigUploaders.DropboxAccountInfo);
        }

        public void Sync()
        {
            if (dropbox != null)
            {
                BackgroundWorker bwLoad = new BackgroundWorker();
                bwLoad.DoWork += new DoWorkEventHandler(bwLoad_DoWork);
                bwLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLoad_RunWorkerCompleted);
                bwLoad.RunWorkerAsync();
            }
        }

        private void bwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WorkflowsConfig dbWorkflows = Load<WorkflowsConfig>(pathDropboxWorkflowsConfig);
                if (dbWorkflows != null)
                    SettingsManager.ConfigWorkflows = dbWorkflows;

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

                UserConfig dbConfigUser = Load<UserConfig>(pathDropboxUserConfig);
                if (dbConfigUser != null)
                    SettingsManager.ConfigUser = dbConfigUser;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        private void bwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormsHelper.Main.LoadSettings();
            if (Program.IsHotkeysAllowed)
                FormsHelper.Main.InitHotkeys();
            FormsHelper.Options.LoadSettings();
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
                        log.InfoFormat("Loaded {0}", path);
                        return (T)tmp;
                    }
                }
            }

            return default(T);
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

        public static void SaveAsync()
        {
            if (SettingsManager.ConfigCore.DropboxSync)
            {
                new DropboxSyncHelper().Save();
            }
        }

        private void bwSave_DoWork(object sender, DoWorkEventArgs e)
        {
            // Create a copy of Settings
            if (dropbox.Upload(GetMemoryStream(SettingsManager.ConfigWorkflows), pathDropboxWorkflowsConfig) != null)
                log.InfoFormat("Updated {0}", pathDropboxWorkflowsConfig);

            if (dropbox.Upload(GetMemoryStream(SettingsManager.ConfigCore), pathDropboxSettings) != null)
                log.InfoFormat("Updated {0}", pathDropboxSettings);

            if (dropbox.Upload(GetMemoryStream(SettingsManager.ConfigUploaders), pathDropboxUploadersConfig) != null)
                log.InfoFormat("Updated {0}", pathDropboxUploadersConfig);

            if (dropbox.Upload(GetMemoryStream(SettingsManager.ConfigUser), pathDropboxUserConfig) != null)
                log.InfoFormat("Updated {0}", pathDropboxUserConfig);
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
    }
}