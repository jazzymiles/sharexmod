using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ShareX.Properties;

namespace ShareX
{
    public static class TrayIconManager
    {
        private static BackgroundWorker bwTray = new BackgroundWorker();

        static TrayIconManager()
        {
            bwTray.DoWork += new DoWorkEventHandler(bwTray_DoWork);
            bwTray.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwTray_RunWorkerCompleted);
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
            IEnumerable<Task> workingTasks = UploadManager.Tasks.Where(x => x != null && x.IsWorking);
            while (workingTasks.Count() > 0)
            {
                FormsHelper.Main.niTray.Icon = Resources.ShareXSmallBusyIcon2;
                System.Threading.Thread.Sleep(ms);
                FormsHelper.Main.niTray.Icon = Resources.ShareXSmallBusyIcon3;
                System.Threading.Thread.Sleep(ms);
                FormsHelper.Main.niTray.Icon = Resources.ShareXSmallBusyIcon4;
                System.Threading.Thread.Sleep(ms);
                FormsHelper.Main.niTray.Icon = Resources.ShareXSmallBusyIcon1;
                System.Threading.Thread.Sleep(ms);
            }
        }

        private static void bwTray_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormsHelper.Main.niTray.Icon = Resources.ShareXSmallBusyIcon1;
        }

        private static void bwTray_DoWork(object sender, DoWorkEventArgs e)
        {
            RotateTrayIcon(250);
        }
    }
}