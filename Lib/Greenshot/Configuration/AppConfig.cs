/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2012  Thomas Braun, Jens Klingen, Robin Krom
 *
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Core;
using GreenshotPlugin.UnmanagedHelpers;

namespace Greenshot.Configuration
{
    public enum ScreenshotDestinations { Editor = 1, FileDefault = 2, FileWithDialog = 4, Clipboard = 8, Printer = 16, EMail = 32 }

    /// <summary>
    /// AppConfig is used for loading and saving the configuration. All public fields
    /// in this class are serialized with the BinaryFormatter and then saved to the
    /// config file. After loading the values from file, SetDefaults iterates over
    /// all public fields an sets fields set to null to the default value.
    /// </summary>
    [Serializable]
    public class AppConfig
    {
        private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(AppConfig));
        private static readonly Regex FIXOLD_REGEXP = new Regex(@"%(?<variable>[\w]+)%", RegexOptions.Compiled);
        private const string VAR_PREFIX = "${";
        private const string VAR_POSTFIX = "}";

        //private static string loc = Assembly.GetExecutingAssembly().Location;
        //private static string oldFilename = Path.Combine(loc.Substring(0,loc.LastIndexOf(@"\")),"config.dat");
        private const string CONFIG_FILE_NAME = "config.dat";
        private static string configfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Greenshot\");

        // the configuration part - all public vars are stored in the config file
        // don't use "null" and "0" as default value!

        #region general application config

        public bool? General_IsFirstLaunch = true;

        #endregion general application config

        #region capture config

        public bool? Capture_Mousepointer = true;
        public bool? Capture_Windows_Interactive = false;
        public int Capture_Wait_Time = 101;
        public bool? fixedWaitTime = false;

        #endregion capture config

        #region user interface config

        public string Ui_Language = "";
        public bool? Ui_Effects_CameraSound = true;

        #endregion user interface config

        #region output config

        public ScreenshotDestinations Output_Destinations = ScreenshotDestinations.Editor;

        public string Output_File_Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public string Output_File_FilenamePattern = "${capturetime}_${title}";
        public string Output_File_Format = ImageFormat.Png.ToString();
        public bool? Output_File_CopyPathToClipboard = false;
        public int Output_File_JpegQuality = 80;
        public bool? Output_File_PromptJpegQuality = false;
        public int Output_File_IncrementingNumber = 1;

        public string Output_FileAs_Fullpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "dummy.png");

        public bool? Output_Print_PromptOptions = true;
        public bool? Output_Print_AllowRotate = true;
        public bool? Output_Print_AllowEnlarge = true;
        public bool? Output_Print_AllowShrink = true;
        public bool? Output_Print_Center = true;
        public bool? Output_Print_Timestamp = true;

        #endregion output config

        #region editor config

        public WindowPlacement Editor_Placement;
        public Color[] Editor_RecentColors = new Color[12];
        public Font Editor_Font = null;

        #endregion editor config

        /// <summary>
        /// a private constructor because this is a singleton
        /// </summary>
        private AppConfig()
        {
        }

        /// <summary>
        /// Remove the old %VAR% syntax
        /// </summary>
        /// <param name="oldPattern">String with old syntax %VAR%</param>
        /// <returns>The fixed pattern</returns>
        private static string FixFallback(string oldPattern)
        {
            return FIXOLD_REGEXP.Replace(oldPattern, new MatchEvaluator(delegate(Match m) { return VAR_PREFIX + m.Groups["variable"].Value + VAR_POSTFIX; }));
        }

        /// <summary>
        /// loads the configuration from the config file
        /// </summary>
        /// <returns>an instance of AppConfig with all values set from the config file</returns>
        private static AppConfig Load()
        {
            AppConfig conf = null;
            if (File.Exists(Path.Combine(Application.StartupPath, CONFIG_FILE_NAME)))
            {
                configfilepath = Application.StartupPath;
            }
            string configfilename = Path.Combine(configfilepath, CONFIG_FILE_NAME);
            try
            {
                LOG.DebugFormat("Loading configuration from: {0}", configfilename);
                using (FileStream fileStream = File.Open(configfilename, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    conf = (AppConfig)binaryFormatter.Deserialize(fileStream);
                }
                conf.Output_File_FilenamePattern = FixFallback(conf.Output_File_FilenamePattern);
                conf.Output_File_Path = FixFallback(conf.Output_File_Path);
            }
            catch (Exception e)
            {
                LOG.Warn("(ignoring) Problem loading configuration from: " + configfilename, e);
            }
            return conf;
        }
    }
}