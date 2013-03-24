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
using HelpersLibMod;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX.HelperClasses
{
    public static class ListViewManager
    {
        private static ImageList Thumbnails { get; set; }

        private static ImageList DetailViewImageList { get; set; }

        public static void RefreshThumbnails()
        {
            if (SettingsManager.ConfigCore.ListViewMode != View.Details)
            {
                ThreadWorker bwRefreshThumbnails = new ThreadWorker();
                bwRefreshThumbnails.DoWork += new Action(bwRefreshThumbnails_DoWork);
                bwRefreshThumbnails.Completed += new Action(bwRefreshThumbnails_Completed);
                bwRefreshThumbnails.Start();
            }
        }

        private static void bwRefreshThumbnails_Completed()
        {
            if (SettingsManager.ConfigCore.ListViewMode != View.Details)
            {
                foreach (Image img in Thumbnails.Images)
                {
                    TaskManager.ListViewControl.LargeImageList.Images.Add(img);
                }

                for (int i = 1; i <= Thumbnails.Images.Count; i++)
                {
                    TaskManager.ListViewControl.Items[TaskManager.ListViewControl.Items.Count - i].ImageIndex = Thumbnails.Images.Count - i;
                }
            }
        }

        private static void bwRefreshThumbnails_DoWork()
        {
            if (Thumbnails != null)
                Thumbnails.Dispose();

            Thumbnails = get_NewLargeImageList();

            foreach (UploadTask task in TaskManager.Tasks)
            {
                if (task.Info != null && File.Exists(task.Info.FilePath) && Helpers.IsImageFile(task.Info.FilePath))
                {
                    Thumbnails.Images.Add(task.Info.FileName, HelpersMod.ImageFromFile(task.Info.FilePath));
                }
            }
        }

        public static void AddThumbnail()
        {
            ThreadWorker bwAddThumbnail = new ThreadWorker();
            bwAddThumbnail.DoWork += new Action(bwAddThumbnail_DoWork);
            bwAddThumbnail.Completed += new Action(bwAddThumbnail_Completed);
            bwAddThumbnail.Start();
        }

        private static void bwAddThumbnail_Completed()
        {
            if (Thumbnails.Images.Count > 0)
            {
                TaskManager.ListViewControl.LargeImageList.Images.Add(Thumbnails.Images[Thumbnails.Images.Count - 1]);

                if (SettingsManager.ConfigCore.ListViewMode != View.Details && TaskManager.ListViewControl.Items.Count >= Thumbnails.Images.Count)
                {
                    TaskManager.ListViewControl.Items[TaskManager.ListViewControl.Items.Count - 1].ImageIndex = Thumbnails.Images.Count - 1;
                }
            }
        }

        private static void bwAddThumbnail_DoWork()
        {
            UploadTask task = TaskManager.Tasks.Last();
            UploadInfo info = task.Info;

            if (File.Exists(info.FilePath) && Helpers.IsImageFile(info.FilePath))
            {
                Thumbnails.Images.Add(info.FileName, HelpersMod.ImageFromFile(info.FilePath));
            }
            else
            {
                Image img = task.GetImageForExport();
                if (img == null)
                    img = Resources.folder;
                Thumbnails.Images.Add(task.Info.FileName, img);
            }
        }

        private static ImageList get_NewLargeImageList()
        {
            return new ImageList()
            {
                ImageSize = new System.Drawing.Size(128, 128),
                ColorDepth = ColorDepth.Depth32Bit
            };
        }

        internal static void Initialize()
        {
            if (DetailViewImageList == null)
            {
                DetailViewImageList = new ImageList();
                DetailViewImageList.ColorDepth = ColorDepth.Depth32Bit;
                DetailViewImageList.Images.Add(Properties.Resources.navigation_090_button);
                DetailViewImageList.Images.Add(Properties.Resources.cross_button);
                DetailViewImageList.Images.Add(Properties.Resources.tick_button);
                DetailViewImageList.Images.Add(Properties.Resources.navigation_000_button);
            }

            if (Thumbnails == null)
                Thumbnails = get_NewLargeImageList();

            TaskManager.ListViewControl.LargeImageList = get_NewLargeImageList(); // not Thumbnails because cross thread errors could occur

            // reset ImageIndex to prevent showing wrong images
            if (TaskManager.ListViewControl.View == View.Details)
            {
                // UploadManager.ListViewControl.SmallImageList = DetailViewImageList;
                foreach (ListViewItem lvi in TaskManager.ListViewControl.Items)
                {
                    set_IconCompleted(lvi);
                }
            }
            else
            {
                for (int i = 1; i < Thumbnails.Images.Count; i++)
                {
                    if (SettingsManager.ConfigCore.ListViewMode != View.Details && TaskManager.ListViewControl.Items.Count > i)
                    {
                        TaskManager.ListViewControl.Items[TaskManager.ListViewControl.Items.Count - i].ImageIndex = Thumbnails.Images.Count - i;
                    }
                }
            }
        }

        internal static void set_IconUploadStarted(ListViewItem lvi)
        {
            //  if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 0;
        }

        internal static void set_IconError(ListViewItem lvi)
        {
            //  if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 1;
        }

        internal static void set_IconCompleted(ListViewItem lvi)
        {
            // if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 2;
        }

        internal static void set_IconCreated(ListViewItem lvi)
        {
            // if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 3;
        }
    }
}