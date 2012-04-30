using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX
{
    public static class log4netHelper
    {
        public static void SetTextBox(TextBox tb)
        {
            foreach (log4net.Appender.IAppender appender in GetAppenders())
            {
                if (appender is TextBoxAppender)
                {
                    ((TextBoxAppender)appender)._textBox = tb;
                }
            }
        }

        public static log4net.Appender.IAppender[] GetAppenders()
        {
            ArrayList appenders = new ArrayList();

            appenders.AddRange(((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root.Appenders);
            foreach (log4net.ILog log in log4net.LogManager.GetCurrentLoggers())
            {
                appenders.AddRange(((log4net.Repository.Hierarchy.Logger)log.Logger).Appenders);
            }

            return (log4net.Appender.IAppender[])appenders.ToArray(typeof(log4net.Appender.IAppender));
        }
    }
}