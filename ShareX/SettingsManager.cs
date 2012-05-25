using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ShareX.SettingsHelpers;
using UploadersLib;

namespace ShareX
{
    public static class SettingsManager
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ManualResetEvent SettingsResetEvent;
        public static ManualResetEvent UploaderSettingsResetEvent;

        private static readonly string ApplicationName = Application.ProductName;

        public static Settings ConfigCore { get; internal set; }
        internal static readonly string ConfigCoreFileName = ApplicationName + "Settings.json";
        public static string ConfigCoreFilePath
        {
            get
            {
                return Path.Combine(Program.PersonalPath, ConfigCoreFileName);
            }
        }

        public static UserConfig ConfigUser { get; internal set; }
        private static readonly string ConfigUserFileName = ApplicationName + "UserConfig.json";
        public static string ConfigUserFilePath
        {
            get
            {
                return Path.Combine(Program.PersonalPath, ConfigUserFileName);
            }
        }

        public static UploadersConfig ConfigUploaders { get; internal set; }
        internal static readonly string ConfigUploadersFileName = "UploadersConfig.json";
        public static string ConfigUploadersFilePath
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
        private static readonly string ConfigWorkflowsFileName = ApplicationName + "WorkflowsConfig.json";
        public static string ConfigWorkflowsFilePath
        {
            get
            {
                return Path.Combine(Program.PersonalPath, ConfigWorkflowsFileName);
            }
        }

        public static void SaveAsync()
        {
            ConfigUploaders.SaveAsync(ConfigUploadersFilePath);
            ConfigCore.SaveAsync(ConfigCoreFilePath);
            ConfigCore.BackupAsync(ConfigCoreFilePath);
        }

        public static void Save()
        {
            // This code should be removed after 2012-06-15
            if (ConfigCore.Workflows1.Count > 0)
            {
                ConfigWorkflows = new WorkflowsConfig();
                ConfigWorkflows.Workflows.AddRange(ConfigCore.Workflows1);
                ConfigCore.Workflows1.Clear();
                ConfigWorkflows.Save(ConfigWorkflowsFilePath);
            }
            ConfigUploaders.Save(ConfigUploadersFilePath);
            ConfigCore.Save(ConfigCoreFilePath);
            ConfigCore.Backup(ConfigCoreFilePath);
        }

        public static void Load()
        {
            log.Info("Loading workflows.");
            ConfigWorkflows = WorkflowsConfig.Load(ConfigWorkflowsFilePath);

            log.Info("Loading Settings");
            SettingsManager.ConfigCore = Settings.Load(ConfigCoreFilePath);
            SettingsResetEvent.Set();

            log.Info("Loading Uploaders Config");
            LoadUploadersConfig();
            UploaderSettingsResetEvent.Set();
        }

        public static void LoadUploadersConfig()
        {
            SettingsManager.ConfigUploaders = UploadersConfig.Load(ConfigUploadersFilePath);
        }
    }
}