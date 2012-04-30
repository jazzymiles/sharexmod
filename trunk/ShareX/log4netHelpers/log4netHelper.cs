using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;

namespace ShareX
{
    public static class log4netHelper
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static RichTextBoxAppender rba;

        public static void Init_log4net()
        {
            if (!log.Logger.Repository.Configured)
            {
                RollingFileAppender fa = new RollingFileAppender();
                fa.AppendToFile = true;
                fa.Threshold = log4net.Core.Level.All;
                fa.RollingStyle = RollingFileAppender.RollingMode.Size;
                fa.MaxFileSize = 100000;
                fa.MaxSizeRollBackups = 3;
                fa.File = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName), Application.ProductName + ".log");
                fa.Layout = new log4net.Layout.PatternLayout("%date{ISO8601} [%thread] %-5level - [%logger] %m%n%exception");
                log4net.Config.BasicConfigurator.Configure(fa);
                fa.ActivateOptions();
            }
        }

        public static void ConfigureRichTextBox(RichTextBox myRichTextBox)
        {
            rba = new RichTextBoxAppender(myRichTextBox);
            rba.Threshold = Level.All;
            rba.Layout = new PatternLayout("%date{ISO8601} [%thread] %-5level - [%logger] %m%n%exception");
            LevelTextStyle ilts = new LevelTextStyle();

            ilts.Level = Level.Info;
            ilts.TextColor = Color.DarkGreen;
            ilts.PointSize = 10.0f;
            rba.AddMapping(ilts);

            LevelTextStyle dlts = new LevelTextStyle();
            dlts.Level = Level.Debug;
            dlts.TextColor = Color.Blue;
            dlts.PointSize = 10.0f;
            rba.AddMapping(dlts);

            LevelTextStyle wlts = new LevelTextStyle();
            wlts.Level = Level.Warn;
            wlts.TextColor = Color.Orange;
            wlts.PointSize = 10.0f;
            rba.AddMapping(wlts);

            LevelTextStyle elts = new LevelTextStyle();
            elts.Level = Level.Error;
            elts.TextColor = Color.DarkRed;
            elts.PointSize = 10.0f;
            rba.AddMapping(elts);

            BasicConfigurator.Configure(rba);
            rba.ActivateOptions();
        }
    }
}