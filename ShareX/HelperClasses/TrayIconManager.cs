using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShareX.Properties;

namespace ShareX
{
    public static class TrayIconManager
    {
        private static BackgroundWorker bwTray = new BackgroundWorker();
        private static List<Icon> iconList = new List<Icon>();

        static TrayIconManager()
        {
            if (iconList.Count == 0)
                Load();

            bwTray.DoWork += new DoWorkEventHandler(bwTray_DoWork);
            bwTray.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwTray_RunWorkerCompleted);
        }

        public static void Load()
        {
            iconList.Add(Resources.sharex_16px_0);
            iconList.Add(Resources.sharex_16px_1);
            iconList.Add(Resources.sharex_16px_2);
            iconList.Add(Resources.sharex_16px_3);
            iconList.Add(Resources.sharex_16px_4);
            iconList.Add(Resources.sharex_16px_5);
            iconList.Add(Resources.sharex_16px_6);
        }

        public static void UpdateTrayIcon()
        {
            if (!bwTray.IsBusy)
            {
                bwTray.RunWorkerAsync();
            }
        }

        private static void RotateTrayIcon(int ms)
        {
            IEnumerable<Task> workingTasks = TaskManager.Tasks.Where(x => x != null && x.IsWorking);
            while (workingTasks.Count() > 0)
            {
                for (int i = 0; i < iconList.Count; i++)
                {
                    FormsHelper.Main.niTray.Icon = iconList[i];
                    System.Threading.Thread.Sleep(ms);
                }
            }
        }

        private static void bwTray_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormsHelper.Main.niTray.Icon = Resources.ShareXSmallIcon;
        }

        private static void bwTray_DoWork(object sender, DoWorkEventArgs e)
        {
            RotateTrayIcon(200);
        }
    }
}