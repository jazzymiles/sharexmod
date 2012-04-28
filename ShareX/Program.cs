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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using SingleInstanceApplication;
using UploadersLib;

namespace ShareX
{
    internal static class Program
    {
        private static readonly string ApplicationName = Application.ProductName;

        #region Paths

        private static readonly string DefaultPersonalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ApplicationName);
        private static readonly string PortablePersonalPath = Path.Combine(Application.StartupPath, ApplicationName);

        private static readonly string SettingsFileName = ApplicationName + "Settings.json";
        private static readonly string HistoryFileName = "UploadersHistory.xml";
        private static readonly string UploadersConfigFileName = "UploadersConfig.json";
        private static readonly string LogFileName = ApplicationName + "Log-{0}-{1}.txt";

        public static string PersonalPath
        {
            get
            {
                if (IsPortable)
                {
                    return PortablePersonalPath;
                }

                return DefaultPersonalPath;
            }
        }

        public static string SettingsFilePath
        {
            get
            {
                return Path.Combine(PersonalPath, SettingsFileName);
            }
        }

        public static string HistoryFilePath
        {
            get
            {
                if (Settings != null && Settings.UseCustomHistoryPath && !string.IsNullOrEmpty(Settings.CustomHistoryPath))
                {
                    return Settings.CustomHistoryPath;
                }

                return Path.Combine(PersonalPath, HistoryFileName);
            }
        }

        public static string UploadersConfigFilePath
        {
            get
            {
                if (Settings != null && Settings.UseCustomUploadersConfigPath && !string.IsNullOrEmpty(Settings.CustomUploadersConfigPath))
                {
                    return Settings.CustomUploadersConfigPath;
                }

                return Path.Combine(PersonalPath, UploadersConfigFileName);
            }
        }

        public static string LogFilePath
        {
            get
            {
                DateTime now = FastDateTime.Now;
                return Path.Combine(PersonalPath, string.Format(LogFileName, now.Year, now.Month));
            }
        }

        public static string ScreenshotsRootPath
        {
            get
            {
                if (Settings != null && Directory.Exists(Settings.ScreenshotsPath2))
                {
                    return Settings.ScreenshotsPath2;
                }
                else
                {
                    return Path.Combine(PersonalPath, "Screenshots");
                }
            }
        }

        public static string ScreenshotsPath
        {
            get
            {
                string subFolderName = new NameParser(NameParserType.SaveFolder).Convert(Settings.SaveImageSubFolderPattern);
                return Path.Combine(ScreenshotsRootPath, subFolderName);
            }
        }

        #endregion Paths

        #region Hotkeys / Workflows

        public static HotkeySetting HotkeyClipboardUpload = new HotkeySetting(Keys.Control | Keys.PageUp);
        public static HotkeySetting HotkeyFileUpload = new HotkeySetting(Keys.Shift | Keys.PageUp);
        public static HotkeySetting HotkeyPrintScreen = new HotkeySetting(Keys.PrintScreen);
        public static HotkeySetting HotkeyActiveWindow = new HotkeySetting(Keys.Alt | Keys.PrintScreen);
        public static HotkeySetting HotkeyActiveMonitor = new HotkeySetting(Keys.Control | Keys.Alt | Keys.PrintScreen);
        public static HotkeySetting HotkeyWindowRectangle = new HotkeySetting(Keys.Shift | Keys.PrintScreen);
        public static HotkeySetting HotkeyRectangleRegion = new HotkeySetting(Keys.Control | Keys.PrintScreen);
        public static HotkeySetting HotkeyRoundedRectangleRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.R);
        public static HotkeySetting HotkeyEllipseRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.E);
        public static HotkeySetting HotkeyTriangleRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.T);
        public static HotkeySetting HotkeyDiamondRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.D);
        public static HotkeySetting HotkeyPolygonRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.P);
        public static HotkeySetting HotkeyFreeHandRegion = new HotkeySetting(Keys.Control | Keys.Shift | Keys.F);

        #endregion Hotkeys / Workflows

        public static Settings Settings { get; private set; }
        public static UploadersConfig UploadersConfig { get; private set; }
        public static bool IsMultiInstance { get; private set; }
        public static bool IsPortable { get; private set; }
        public static bool IsSilentRun { get; private set; }
        public static Stopwatch StartTimer { get; private set; }
        public static Logger MyLogger { get; private set; }

        public static string Title
        {
            get
            {
                string title = string.Format("{0} {1} r{2}", ApplicationName, Application.ProductVersion, AppRevision);
                if (IsPortable) title += " Portable";
                return title;
            }
        }

        public static string AppRevision
        {
            get
            {
                return AssemblyVersion.Split('.')[3];
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static MainForm mainForm;
        public static ManualResetEvent SettingsResetEvent;
        public static ManualResetEvent UploaderSettingsResetEvent;

        public static List<string> LibNames = new List<string>();

        [STAThread]
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            StartTimer = Stopwatch.StartNew();

            IsMultiInstance = CLIHelper.CheckArgs(args, "m", "multi");

            if (!IsMultiInstance && !ApplicationInstanceManager.CreateSingleInstance(SingleInstanceCallback))
            {
                return;
            }

            Mutex mutex = null;

            try
            {
                mutex = new Mutex(false, @"Global\82E6AC09-0FEF-4390-AD9F-0DD3F5561EFC"); // Required for installer

                IsSilentRun = CLIHelper.CheckArgs(args, "s", "silent");

                if (CLIHelper.CheckArgs(args, "p", "portable") && !Directory.Exists(PortablePersonalPath))
                {
                    Directory.CreateDirectory(PortablePersonalPath);
                }

                IsPortable = Directory.Exists(PortablePersonalPath);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                MyLogger = new Logger();
                DebugHelper.MyLogger = MyLogger;
                MyLogger.WriteLine("{0} {1} r{2} started", Application.ProductName, Application.ProductVersion, AppRevision);
                MyLogger.WriteLine("Operating system: " + Environment.OSVersion.VersionString);
                MyLogger.WriteLine("CommandLine: " + Environment.CommandLine);
                MyLogger.WriteLine("IsMultiInstance: " + IsMultiInstance);
                MyLogger.WriteLine("IsSilentRun: " + IsSilentRun);
                MyLogger.WriteLine("IsPortable: " + IsPortable);

                SettingsResetEvent = new ManualResetEvent(false);
                UploaderSettingsResetEvent = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(state => LoadSettings());

                MyLogger.WriteLine("new MainForm() started");
                mainForm = new MainForm();
                MyLogger.WriteLine("new MainForm() finished");

                if (Settings == null)
                {
                    SettingsResetEvent.WaitOne();
                }

                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.Run(mainForm);

                Settings.Save();

                MyLogger.WriteLine("ShareX closing");
                MyLogger.SaveLog(LogFilePath);
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.Close();
                }
            }
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            LibNames.Add(args.LoadedAssembly.Location);
        }

        public static void LoadSettings()
        {
            // import from ZUploader - remove this code after 2013-04-28
            string zuSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ZUploader\Settings.xml");
            if (!File.Exists(SettingsFilePath) && File.Exists(zuSettings))
            {
                Settings = Settings.Load(zuSettings, SerializationType.Xml);
                try
                {
                    File.Delete(zuSettings);
                }
                catch (Exception ex)
                {
                    MyLogger.WriteException(ex, "while deleting old ZUploader settings");
                }
            }
            else
            {
                Settings = Settings.Load(SettingsFilePath);
            }

            SettingsResetEvent.Set();

            string zuOutputsConfig = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ZUploader\UploadersConfig.xml");
            if (!File.Exists(UploadersConfigFilePath) && File.Exists(zuOutputsConfig))
            {
                UploadersConfig = UploadersConfig.Load(zuOutputsConfig, SerializationType.Xml);
                try
                {
                    File.Delete(zuOutputsConfig);
                }
                catch (Exception ex)
                {
                    MyLogger.WriteException(ex, "while importing ZUploader outputs config");
                }
            }
            else
            {
                LoadUploadersConfig();
            }

            UploaderSettingsResetEvent.Set();
        }

        public static void LoadUploadersConfig()
        {
            UploadersConfig = UploadersConfig.Load(UploadersConfigFilePath);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            OnError(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnError((Exception)e.ExceptionObject);
        }

        private static void OnError(Exception e)
        {
            new ErrorForm(Application.ProductName, e, MyLogger, LogFilePath, Links.URL_ISSUES).ShowDialog();
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (WaitFormLoad(5000))
            {
                Action d = () =>
                {
                    if (mainForm.Visible)
                    {
                        mainForm.ShowActivate();
                    }

                    mainForm.UseCommandLineArgs(args.CommandLineArgs);
                };

                mainForm.Invoke(d);
            }
        }

        private static bool WaitFormLoad(int wait)
        {
            Stopwatch timer = Stopwatch.StartNew();

            while (timer.ElapsedMilliseconds < wait)
            {
                if (mainForm != null && mainForm.IsReady) return true;

                Thread.Sleep(10);
            }

            return false;
        }
    }
}