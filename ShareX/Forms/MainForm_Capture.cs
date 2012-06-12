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
using System.Media;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using HelpersLibWatermark;
using ScreenCapture;
using ShareX.HelperClasses;
using ShareX.Properties;

namespace ShareX
{
    public partial class MainForm
    {
        private delegate Image ScreenCaptureDelegate();

        private new ImageData Capture(ScreenCaptureDelegate capture, bool autoHideForm = true)
        {
            if (autoHideForm)
            {
                Hide();
                Thread.Sleep(250);
            }

            ImageData imageData = null;

            try
            {
                Screenshot.DrawCursor = SettingsManager.ConfigCore.ShowCursor;
                Screenshot.CaptureShadow = SettingsManager.ConfigCore.CaptureShadow;

                Image img = capture();

                if (img != null && SettingsManager.ConfigCore.PlaySoundAfterCapture)
                {
                    imageData = new ImageData(img, screenshot: true);
                    Helpers.PlaySoundAsync(Resources.CameraSound);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            finally
            {
                if (autoHideForm)
                {
                    ShowActivate();
                }
            }

            return imageData;
        }

        /// <summary>
        /// Method to run after capturing screneshot.
        /// </summary>
        /// <param name="imageData">ImageData object contains Image and WindowText.</param>
        /// <param name="act">AfterCaptureActivity object is null when default hotkeys are run.</param>
        private void AfterCapture(ImageData imageData, AfterCaptureActivity act = null)
        {
            if (imageData != null)
            {
                AfterCaptureActivity.Prepare(act);

                if (SettingsManager.ConfigCore.ShowAfterCaptureWizard)
                {
                    WindowAfterCapture dlg = new WindowAfterCapture(act.Subtasks);
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        act.Subtasks = dlg.Config;
                    }
                    else
                    {
                        return;
                    }
                }

                if (act.Subtasks.HasFlag(Subtask.AnnotateImage))
                {
                    EditImage(ref imageData);
                }

                UploadManager.DoImageWork(imageData, act);
            }
        }

        private ImageData CaptureScreen(bool autoHideForm = true)
        {
            return Capture(Screenshot.CaptureFullscreen, autoHideForm);
        }

        private ImageData CaptureActiveWindow(bool autoHideForm = true)
        {
            if (SettingsManager.ConfigCore.CaptureTransparent)
            {
                return Capture(Screenshot.CaptureActiveWindowTransparent, autoHideForm);
            }
            else
            {
                return Capture(Screenshot.CaptureActiveWindow, autoHideForm);
            }
        }

        private ImageData CaptureActiveMonitor(bool autoHideForm = true)
        {
            return Capture(Screenshot.CaptureActiveMonitor, autoHideForm);
        }

        private ImageData CaptureWindow(IntPtr handle, bool autoHideForm = true)
        {
            autoHideForm = autoHideForm && handle != this.Handle;

            return Capture(() =>
             {
                 if (NativeMethods.IsIconic(handle))
                 {
                     NativeMethods.RestoreWindow(handle);
                 }

                 NativeMethods.SetForegroundWindow(handle);
                 Thread.Sleep(250);

                 if (SettingsManager.ConfigCore.CaptureTransparent)
                 {
                     return Screenshot.CaptureWindowTransparent(handle);
                 }

                 return Screenshot.CaptureWindow(handle);
             }, autoHideForm);
        }

        private ImageData CaptureRegion(Surface surface, bool autoHideForm = true)
        {
            return Capture(() =>
            {
                Image img = null;
                Image screenshot = Screenshot.CaptureFullscreen();

                surface.Config = SettingsManager.ConfigCore.SurfaceOptions;
                surface.SurfaceImage = screenshot;
                surface.Prepare();

                if (surface.ShowDialog() == DialogResult.OK)
                {
                    img = surface.GetRegionImage();
                }

                surface.Dispose();

                return img;
            }, autoHideForm);
        }

        private ImageData WindowRectangleCapture(bool autoHideForm = true)
        {
            RectangleRegion rectangleRegion = new RectangleRegion();
            rectangleRegion.AreaManager.WindowCaptureMode = true;
            return CaptureRegion(rectangleRegion, autoHideForm);
        }

        private void PrepareWindowsMenu(ToolStripMenuItem tsmi, EventHandler handler)
        {
            tsmi.DropDownItems.Clear();

            WindowsList windowsList = new WindowsList();
            List<WindowInfo> windows = windowsList.GetVisibleWindowsList();

            foreach (WindowInfo window in windows)
            {
                string title = window.Text.Truncate(50);
                ToolStripItem tsi = tsmi.DropDownItems.Add(title);
                tsi.Click += handler;

                try
                {
                    using (Icon icon = window.Icon)
                    {
                        if (icon != null)
                        {
                            tsi.Image = icon.ToBitmap();
                        }
                    }
                }
                catch (Exception e)
                {
                    log.Error(e.ToString());
                }

                tsi.Tag = window;
            }
        }

        #region Menu events

        private void tsmiFullscreen_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.FullScreen));
        }

        private void tsddbCapture_DropDownOpening(object sender, EventArgs e)
        {
            PrepareWindowsMenu(tsmiWindow, tsmiWindowItems_Click);
        }

        private void tsmiWindowItems_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            WindowInfo wi = tsi.Tag as WindowInfo;
            if (wi != null) AfterCapture(CaptureWindow(wi.Handle));
        }

        private void tsmiWindowRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.WindowRectangle));
        }

        private void tsmiRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.RectangleRegion));
        }

        private void tsmiRoundedRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.RoundedRectangleRegion));
        }

        private void tsmiEllipse_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.EllipseRegion));
        }

        private void tsmiTriangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.TriangleRegion));
        }

        private void tsmiDiamond_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.DiamondRegion));
        }

        private void tsmiPolygon_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.PolygonRegion));
        }

        private void tsmiFreeHand_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(HelpersLib.Hotkeys2.EHotkey.FreeHandRegion));
        }

        #endregion Menu events

        #region Tray events

        private void tsmiTrayFullscreen_Click(object sender, EventArgs e)
        {
            tsmiFullscreen_Click(sender, e);
        }

        private void tsmiCapture_DropDownOpening(object sender, EventArgs e)
        {
            PrepareWindowsMenu(tsmiTrayWindow, tsmiTrayWindowItems_Click);
        }

        private void tsmiTrayWindowItems_Click(object sender, EventArgs e)
        {
            tsmiWindowItems_Click(sender, e);
        }

        private void tsmiTrayWindowRectangle_Click(object sender, EventArgs e)
        {
            tsmiWindowRectangle_Click(sender, e);
        }

        private void tsmiTrayRectangle_Click(object sender, EventArgs e)
        {
            tsmiRectangle_Click(sender, e);
        }

        private void tsmiTrayRoundedRectangle_Click(object sender, EventArgs e)
        {
            tsmiRoundedRectangle_Click(sender, e);
        }

        private void tsmiTrayEllipse_Click(object sender, EventArgs e)
        {
            tsmiEllipse_Click(sender, e);
        }

        private void tsmiTrayTriangle_Click(object sender, EventArgs e)
        {
            tsmiTriangle_Click(sender, e);
        }

        private void tsmiTrayDiamond_Click(object sender, EventArgs e)
        {
            tsmiDiamond_Click(sender, e);
        }

        private void tsmiTrayPolygon_Click(object sender, EventArgs e)
        {
            tsmiPolygon_Click(sender, e);
        }

        private void tsmiTrayFreeHand_Click(object sender, EventArgs e)
        {
            tsmiFreeHand_Click(sender, e);
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            tsmiTraySettings_Click(sender, e);
        }

        private void tsmiWatermark_Click(object sender, EventArgs e)
        {
            WatermarkUI ui = new WatermarkUI(SettingsManager.ConfigUser.ConfigWatermark)
            {
                Icon = this.Icon
            };
            ui.Show();
        }

        #endregion Tray events
    }
}