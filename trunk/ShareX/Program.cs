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

using HelpersLib;
using HelpersLib.Hotkeys2;
using ShareX.Forms;
using SingleInstanceApplication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UploadersLib;

namespace ShareX
{
    internal static class Program
    {
        private static readonly string ApplicationName = Application.ProductName;

        #region Links

        public const string URL_WEBSITE = "http://code.google.com/p/sharexmod";
        public const string URL_ISSUES = "http://code.google.com/p/sharexmod/issues/entry";
        public const string URL_UPDATE = "http://sharexmod.googlecode.com/svn/trunk/Update.xml";
        public const string URL_DONATE = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=mcored%40gmail%2ecom&lc=US&item_name=ShareXmod&no_note=0&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHostedGuest";

        #endregion Links

        #region Paths

        private static readonly string DefaultScreenshotsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ApplicationName);
        private static readonly string DefaultPersonalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ApplicationName);
        private static readonly string PortablePersonalPath = Path.Combine(Application.StartupPath, ApplicationName);

        private static readonly string LogFileName = ApplicationName + "Log-{0}.log";

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

        public static string LogFilePath
        {
            get
            {
                DateTime now = FastDateTime.Now;
                return Path.Combine(PersonalPath, string.Format(LogFileName, now.ToString("yyyy-MM")));
            }
        }

        public static string ScreenshotsRootPath
        {
            get
            {
                if (SettingsManager.ConfigCore != null && Directory.Exists(SettingsManager.ConfigCore.ScreenshotsPath))
                {
                    return SettingsManager.ConfigCore.ScreenshotsPath;
                }
                else
                {
                    return DefaultScreenshotsPath;
                }
            }
        }

        public static string ScreenshotsPath
        {
            get
            {
                string subFolderName = new NameParser(NameParserType.FolderPath).Parse(SettingsManager.ConfigCore.SaveImageSubFolderPattern);
                return Path.Combine(ScreenshotsRootPath, subFolderName);
            }
        }

        #endregion Paths

        #region Hotkeys / Workflows

        public static HotkeySetting HotkeyClipboardUpload = new HotkeySetting(Keys.Control | Keys.F12);
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

        public static bool IsMultiInstance { get; private set; }

        public static bool IsPortable { get; private set; }

        public static bool IsSilentRun { get; private set; }

        public static bool IsHotkeysAllowed { get; private set; }

        public static bool IsDebug { get; private set; }

        public static Stopwatch StartTimer { get; private set; }

        private static log4net.ILog log = null;

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

                IsPortable = CLIHelper.CheckArgs(args, "p", "portable");

                if (IsPortable && !Directory.Exists(PortablePersonalPath))
                {
                    Directory.CreateDirectory(PortablePersonalPath);
                }

                IsDebug = CLIHelper.CheckArgs(args, "d", "debug");
                IsHotkeysAllowed = !CLIHelper.CheckArgs(args, "nohotkeys");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                Log4netHelper.Init_log4net(LogFilePath);

                log.InfoFormat("{0} {1} r{2} started", Application.ProductName, Application.ProductVersion, AppRevision);
                log.InfoFormat("Operating system: " + Environment.OSVersion.VersionString);
                log.InfoFormat("CommandLine: " + Environment.CommandLine);
                log.InfoFormat("IsMultiInstance: " + IsMultiInstance);
                log.InfoFormat("IsSilentRun: " + IsSilentRun);
                log.InfoFormat("IsPortable: " + IsPortable);
                log.InfoFormat("IsDebug: " + IsDebug);
                log.InfoFormat("IsHotkeysEnabled: " + IsHotkeysAllowed);

                SettingsManager.LoadAsync();
#if DEBUG
                IsDebug = true;
#endif
                log.InfoFormat("new FormsHelper.mainForm() started");
                FormsHelper.Main = new MainForm();
                log.InfoFormat("new FormsHelper.mainForm() finished");

                if (SettingsManager.ConfigCore == null)
                    SettingsManager.CoreResetEvent.WaitOne();
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                if (IsDebug)
                    FormsHelper.ShowLog();

                Application.Run(FormsHelper.Main);

                SettingsManager.Save();

                log.Info("ShareX closing\n");
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
            LibNames.Add(string.Format("{0} - {1}", args.LoadedAssembly.FullName, args.LoadedAssembly.Location));
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
            new ErrorForm(Application.ProductName, e, new Logger(), LogFilePath, Program.URL_ISSUES).ShowDialog();
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (WaitFormLoad(5000))
            {
                Action d = () =>
                {
                    if (FormsHelper.Main.Visible)
                    {
                        FormsHelper.Main.ShowActivate();
                    }

                    FormsHelper.Main.UseCommandLineArgs(args.CommandLineArgs);
                };

                FormsHelper.Main.Invoke(d);
            }
        }

        private static bool WaitFormLoad(int wait)
        {
            Stopwatch timer = Stopwatch.StartNew();

            while (timer.ElapsedMilliseconds < wait)
            {
                if (FormsHelper.Main != null && FormsHelper.Main.IsReady) return true;

                Thread.Sleep(10);
            }

            return false;
        }
    }
}