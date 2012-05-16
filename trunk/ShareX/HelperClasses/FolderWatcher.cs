using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using HelpersLib;

namespace ShareX
{
    public class FolderWatcher
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public string FolderPath { get; set; }

        public FolderWatcher(ISynchronizeInvoke synchronizingObject)
        {
            this.watcher.SynchronizingObject = synchronizingObject;
            this.watcher.Created += new FileSystemEventHandler(Watcher_Created);
        }

        public FolderWatcher(string p, ISynchronizeInvoke synchronizingObject)
            : this(synchronizingObject)
        {
            this.FolderPath = p;
            this.watcher = new FileSystemWatcher(p);
        }

        public void StartWatching()
        {
            if (Directory.Exists(FolderPath))
            {
                log.InfoFormat("FolderWatch is monitoring {0}", FolderPath);
                watcher.Path = this.FolderPath;
                this.watcher.EnableRaisingEvents = true;
            }
        }

        public void StopWatching()
        {
            this.watcher.EnableRaisingEvents = false;
        }

        [STAThread]
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string filepathWatchFolder = e.FullPath;
            int retry = 86400;
            while (retry > 0)
            {
                try
                {
                    if (File.Exists(filepathWatchFolder))
                    {
                        using (FileStream fs = new FileStream(filepathWatchFolder, FileMode.Open, FileAccess.Read))
                        {
                            break;
                        }
                    }
                }
                catch (System.IO.IOException ex)
                {
                    if (--retry == 0)
                    {
                        log.WarnFormat("Waiting for file {0}", filepathWatchFolder, ex.ToString());
                    }
                    Thread.Sleep(1000);
                }
            }

            log.InfoFormat("Created {0}", filepathWatchFolder);
            UploadManager.UploadFile(filepathWatchFolder);
        }
    }
}