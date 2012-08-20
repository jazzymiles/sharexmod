using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HistoryLib;
using ShareX.SettingsHelpers;
using UploadersLib;

namespace ShareX
{
    public static class SettingsManager
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ManualResetEvent WorkflowsResetEvent;
        public static ManualResetEvent CoreResetEvent;
        public static ManualResetEvent UploaderSettingsResetEvent;
        public static ManualResetEvent UserConfigResetEvent;

        private static readonly string ApplicationName = Application.ProductName;

        public static Settings ConfigCore { get; internal set; }

        internal static readonly string ConfigCoreFileName = ApplicationName + "Settings.json";

        private static string ConfigCoreFilePath
        {
            get
            {
                return Path.Combine(Program.PersonalPath, ConfigCoreFileName);
            }
        }

        public static UserConfig ConfigUser { get; internal set; }

        internal static readonly string ConfigUserFileName = ApplicationName + "UserConfig.json";

        private static string ConfigUserFilePath
        {
            get
            {
                return Path.Combine(Program.PersonalPath, ConfigUserFileName);
            }
        }

        public static UploadersConfig ConfigUploaders { get; internal set; }

        internal static readonly string ConfigUploadersFileName = "UploadersConfig.json";

        private static string ConfigUploadersFilePath
        {
            get
            {
                if (SettingsManager.ConfigCore != null && SettingsManager.ConfigCore.UseCustomUploadersConfigPath &&
                    !string.IsNullOrEmpty(SettingsManager.ConfigCore.CustomUploadersConfigPath))
                {
                    return SettingsManager.ConfigCore.CustomUploadersConfigPath;
                }

                return Path.Combine(Program.PersonalPath, ConfigUploadersFileName);
            }
        }

        public static WorkflowsConfig ConfigWorkflows { get; internal set; }

        internal static readonly string ConfigWorkflowsFileName = ApplicationName + "WorkflowsConfig.json";

        private static string ConfigWorkflowsFilePath
        {
            get
            {
                return Path.Combine(Program.PersonalPath, ConfigWorkflowsFileName);
            }
        }

        internal static HistoryManager ConfigHistory { get; set; }

        internal static readonly string HistoryFileName = "History.xml";

        public static string OldHistoryFilePath
        {
            get
            {
                if (SettingsManager.ConfigCore != null && SettingsManager.ConfigCore.UseCustomHistoryPath &&
                    !string.IsNullOrEmpty(SettingsManager.ConfigCore.CustomHistoryPath))
                {
                    return SettingsManager.ConfigCore.CustomHistoryPath;
                }

                return Path.Combine(Program.PersonalPath, "UploadersHistory.xml");
            }
        }

        public static string HistoryFilePath
        {
            get
            {
                if (SettingsManager.ConfigCore != null && SettingsManager.ConfigCore.UseCustomHistoryPath &&
                    !string.IsNullOrEmpty(SettingsManager.ConfigCore.CustomHistoryPath))
                {
                    return Path.Combine(SettingsManager.ConfigCore.CustomHistoryPath, HistoryFileName);
                }

                return Path.Combine(Program.PersonalPath, HistoryFileName);
            }
        }

        public static void SaveAsync()
        {
            ConfigWorkflows.SaveAsync(ConfigWorkflowsFilePath);
            SaveCoreConfigAsync();
            SaveUploadersConfigAsync();
            ConfigUser.SaveAsync(ConfigUserFilePath);
        }

        public static void Save()
        {
            ConfigWorkflows.Save(ConfigWorkflowsFilePath);
            ConfigUploaders.Save(ConfigUploadersFilePath);
            SaveCoreConfig();
            ConfigUser.Save(ConfigUserFilePath);
        }

        public static void SaveCoreConfig()
        {
            ConfigCore.Save(ConfigCoreFilePath);
            // ConfigCore.Backup(ConfigCoreFilePath);
        }

        public static void LoadAsync()
        {
            SettingsManager.WorkflowsResetEvent = new ManualResetEvent(false);
            SettingsManager.CoreResetEvent = new ManualResetEvent(false);
            SettingsManager.UploaderSettingsResetEvent = new ManualResetEvent(false);
            SettingsManager.UserConfigResetEvent = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem(state => LoadWorkflows());
            ThreadPool.QueueUserWorkItem(state => LoadCoreConfig());
            ThreadPool.QueueUserWorkItem(state => LoadUploadersConfig());
            ThreadPool.QueueUserWorkItem(state => LoadUserConfig());

            ConfigHistory = new HistoryManager(HistoryFilePath);
        }

        public static void LoadWorkflows()
        {
            log.Info("Loading workflows config");
            ConfigWorkflows = WorkflowsConfig.Load(ConfigWorkflowsFilePath);
            UpgradeHelper.Upgrade_r175();
            WorkflowsResetEvent.Set();
        }

        public static void LoadCoreConfig()
        {
            log.Info("Loading core config");
            SettingsManager.ConfigCore = Settings.Load(ConfigCoreFilePath);
            UpgradeHelper.Upgrade_r125();
            CoreResetEvent.Set();
        }

        public static void LoadUserConfig()
        {
            log.Info("Loading user config");
            ConfigUser = UserConfig.Load(ConfigUserFilePath);
            UserConfigResetEvent.Set();
        }

        public static void LoadUploadersConfig()
        {
            log.Info("Loading uploaders config");
            ConfigUploaders = UploadersConfig.Load(ConfigUploadersFilePath);
            UploaderSettingsResetEvent.Set();
        }

        public static void SaveCoreConfigAsync()
        {
            ConfigCore.SaveAsync(ConfigCoreFilePath);
            // ConfigCore.BackupAsync(ConfigCoreFilePath);
        }

        public static void SaveUploadersConfigAsync()
        {
            ConfigUploaders.SaveAsync(SettingsManager.ConfigUploadersFilePath);
        }
    }
}