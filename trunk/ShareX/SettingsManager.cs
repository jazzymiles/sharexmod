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

        public static void SaveAsync()
        {
            ConfigWorkflows.SaveAsync(ConfigWorkflowsFilePath);
            SaveCoreConfigAsync();
            SaveUploadersConfigAsync();
            ConfigUser.SaveAsync(ConfigUserFilePath);
        }

        public static void Save()
        {
            // This code should be removed after 2012-06-15
            if (ConfigCore.Workflows1.Count > 0)
            {
                ConfigWorkflows = new WorkflowsConfig();
                ConfigWorkflows.Workflows.AddRange(ConfigCore.Workflows1);
                ConfigCore.Workflows1.Clear();
            }

            ConfigWorkflows.Save(ConfigWorkflowsFilePath);
            ConfigUploaders.Save(ConfigUploadersFilePath);
            SaveCoreConfig();
            ConfigUser.Save(ConfigUserFilePath);
        }

        public static void SaveCoreConfig()
        {
            ConfigCore.Save(ConfigCoreFilePath);
            ConfigCore.Backup(ConfigCoreFilePath);
        }

        public static void LoadWorkflows()
        {
            log.Info("Loading workflows config");
            ConfigWorkflows = WorkflowsConfig.Load(ConfigWorkflowsFilePath);
        }

        public static void LoadCoreConfig()
        {
            log.Info("Loading core config");
            SettingsManager.ConfigCore = Settings.Load(ConfigCoreFilePath);
            SettingsResetEvent.Set();
        }

        public static void LoadUserConfig()
        {
            log.Info("Loading user config");
            ConfigUser = UserConfig.Load(ConfigUserFilePath);
        }

        public static void LoadUploadersConfig()
        {
            log.Info("Loading uploaders config");
            ConfigUploaders = UploadersConfig.Load(ConfigUploadersFilePath);
            if (ConfigUploaders.PasswordsSecureUsingEncryption)
                ConfigUploaders.CryptPasswords(false);
            //   UploaderSettingsResetEvent.Set();
        }

        public static void SaveCoreConfigAsync()
        {
            ConfigCore.SaveAsync(ConfigCoreFilePath);
            ConfigCore.BackupAsync(ConfigCoreFilePath);
        }

        public static void SaveUploadersConfigAsync()
        {
            ConfigUploaders.SaveAsync(SettingsManager.ConfigUploadersFilePath);
        }
    }
}