using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;

namespace HelpersLib
{
    public static class Log4netHelper
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static RichTextBoxAppender rba;
        private static ListViewAppender lva;

        public static void Init_log4net(string fpLog)
        {
            if (!log.Logger.Repository.Configured)
            {
                RollingFileAppender fa = new RollingFileAppender();
                fa.AppendToFile = true;
                fa.Threshold = log4net.Core.Level.All;
                fa.RollingStyle = RollingFileAppender.RollingMode.Date;
                fa.DatePattern = "yyyy-MM.log";
                fa.File = fpLog;
                fa.Layout = new log4net.Layout.PatternLayout("%date{ISO8601} [%thread] %-5level - [%logger] %m%n%exception");
                log4net.Config.BasicConfigurator.Configure(fa);
                fa.ActivateOptions();
            }
        }

        public static void ConfigureListView(MyListView myListView)
        {
            lva = new ListViewAppender(myListView);
            lva.Threshold = Level.All;
            lva.Layout = new PatternLayout("%date{ISO8601} [%thread] %-5level - [%logger] %m%n%exception");

            BasicConfigurator.Configure(lva);
            lva.ActivateOptions();
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