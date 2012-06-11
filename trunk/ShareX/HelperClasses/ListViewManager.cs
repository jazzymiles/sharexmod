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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;

namespace ShareX.HelperClasses
{
    public static class ListViewManager
    {
        public static ImageList ListViewControlImages { get; set; }

        public static void AddThumbnail(MyListView listView, UploadInfo info)
        {
            if (SettingsManager.ConfigCore.ListViewMode != View.Details)
            {
                if (File.Exists(info.FilePath))
                {
                    ListViewControlImages.Images.Add(info.FileName, Image.FromFile(info.FilePath));
                }

                for (int i = 1; i < ListViewControlImages.Images.Count; i++)
                {
                    listView.Items[listView.Items.Count - i].ImageIndex = ListViewControlImages.Images.Count - i;
                }
            }
            else
            {
                if (ListViewControlImages != null)
                    ListViewControlImages.Dispose();
            }
        }

        internal static void Initialize(MyListView listView)
        {
            if (ListViewControlImages == null)
                ListViewControlImages = new ImageList();

            if (listView.View == View.LargeIcon)
            {
                listView.LargeImageList = ListViewControlImages;
                listView.LargeImageList.ImageSize = new System.Drawing.Size(128, 128);
            }

            // reset ImageIndex to prevent showing wrong images
            if (listView.View == View.Details)
            {
                foreach (ListViewItem lvi in listView.Items)
                {
                    lvi.ImageIndex = 3;
                }
            }
            else
            {
                for (int i = 1; i < ListViewControlImages.Images.Count; i++)
                {
                    listView.Items[listView.Items.Count - i].ImageIndex = ListViewControlImages.Images.Count - i;
                }
            }
        }

        internal static void SetIconError(ListViewItem lvi)
        {
            if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 1;
        }

        internal static void SetIconCompleted(ListViewItem lvi)
        {
            if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 2;
        }

        internal static void SetIconUploadStarted(ListViewItem lvi)
        {
            if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 0;
        }

        internal static void SetIconCreated(ListViewItem lvi)
        {
            if (SettingsManager.ConfigCore.ListViewMode == View.Details) lvi.ImageIndex = 3;
        }
    }
}