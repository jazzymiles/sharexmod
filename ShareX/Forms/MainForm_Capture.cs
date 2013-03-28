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
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using HelpersLibWatermark;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Taskbar;
using ScreenCapture;
using ShareX.HelperClasses;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareX
{
    public partial class MainForm
    {
        private delegate Image ScreenCaptureDelegate();

        private ImageData DoCapture(ScreenCaptureDelegate capture, bool autoHideForm = true)
        {
            if (autoHideForm)
            {
                Hide();
                Thread.Sleep(250);
            }

            ImageData imageData = null;

            try
            {
                Screenshot.CaptureCursor = SettingsManager.ConfigCore.ShowCursor;
                Screenshot.CaptureShadow = SettingsManager.ConfigCore.CaptureShadow;

                Image img = capture();

                if (img != null)
                {
                    imageData = new ImageData(img, screenCapture: true);

                    if (SettingsManager.ConfigCore.PlaySoundAfterCapture)
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
                AfterCaptureActivity.Prepare(ref act);
                DialogResult result = System.Windows.Forms.DialogResult.OK;

                // ignore WindowAfterCapture for screencasting

                if (act.Workflow.Hotkey != HelpersLib.Hotkeys2.EHotkey.Screencast &&
                    SettingsManager.ConfigCore.ShowAfterCaptureWizard)
                {
                    TaskbarHelper.TaskbarSetProgressState(TaskbarProgressBarState.Indeterminate);

                    WindowAfterCapture dlg = new WindowAfterCapture(imageData, act.Workflow.Subtasks) { Icon = Resources.ShareX };
                    result = dlg.ShowDialog();

                    switch (dlg.DialogResult)
                    {
                        case System.Windows.Forms.DialogResult.OK:
                            act.Workflow.Subtasks = dlg.ConfigSubtasks;
                            imageData.ConfigUser = dlg.ConfigUser;
                            break;

                        case System.Windows.Forms.DialogResult.Cancel:
                            Clipboard.SetImage(imageData.Image);
                            break;

                        case System.Windows.Forms.DialogResult.Abort:
                            break;
                    }
                }

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    if (act.Workflow.Hotkey == HelpersLib.Hotkeys2.EHotkey.Screencast)
                        UploadManager.DoScreencast(imageData, act);
                    else
                        UploadManager.DoImageWork(imageData, act);
                }
                else
                {
                    TaskbarHelper.TaskbarSetProgressState(TaskbarProgressBarState.NoProgress);
                }
            }
        }

        private ImageData CaptureScreen(bool autoHideForm = true)
        {
            return DoCapture(Screenshot.CaptureFullscreen, autoHideForm);
        }

        private ImageData CaptureActiveWindow(bool autoHideForm = true)
        {
            if (SettingsManager.ConfigCore.CaptureTransparent)
            {
                return DoCapture(Screenshot.CaptureActiveWindowTransparent, autoHideForm);
            }
            else
            {
                return DoCapture(Screenshot.CaptureActiveWindow, autoHideForm);
            }
        }

        private ImageData CaptureActiveMonitor(bool autoHideForm = true)
        {
            return DoCapture(Screenshot.CaptureActiveMonitor, autoHideForm);
        }

        private ImageData CaptureWindow(IntPtr handle, bool autoHideForm = true)
        {
            autoHideForm = autoHideForm && handle != this.Handle;

            return DoCapture(() =>
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
            return DoCapture(() =>
            {
                Image img = null;
                Image screenshot = Screenshot.CaptureFullscreen();

                surface.Config = SettingsManager.ConfigCore.SurfaceOptions;
                surface.SurfaceImage = screenshot;
                surface.Prepare();
                surface.ShowDialog();

                if (surface.Result == SurfaceResult.Region)
                {
                    img = surface.GetRegionImage();
                    screenshot.Dispose();
                }
                else if (surface.Result == SurfaceResult.Fullscreen)
                {
                    img = screenshot;
                }

                surface.Dispose();

                return img;
            }, autoHideForm);
        }

        private ImageData CaptureLastRegion(bool autoHideForm = true)
        {
            if (Surface.LastRegionFillPath != null)
            {
                return DoCapture(() =>
                    {
                        using (Image screenshot = Screenshot.CaptureFullscreen())
                        {
                            return ShapeCaptureHelpers.GetRegionImage(screenshot, Surface.LastRegionFillPath, Surface.LastRegionDrawPath, SettingsManager.ConfigCore.SurfaceOptions);
                        }
                    }, autoHideForm);
            }
            else
            {
                return CaptureRegion(new RectangleRegion(), autoHideForm);
            }
        }

        private ImageData CaptureWindowRectangle(bool autoHideForm = true)
        {
            RectangleRegion rectangleRegion = new RectangleRegion();
            rectangleRegion.AreaManager.WindowCaptureMode = true;
            return CaptureRegion(rectangleRegion, autoHideForm);
        }

        private ImageData CaptureScreencast(bool autoHideForm = true)
        {
            ImageData id_screencast = null;

            if (autoHideForm)
            {
                Hide();
                Thread.Sleep(250);
            }

            using (RectangleRegion surface = new RectangleRegion())
            {
                surface.AreaManager.WindowCaptureMode = true;
                surface.Config = SettingsManager.ConfigCore.SurfaceOptions;
                surface.Config.QuickCrop = true;
                surface.Prepare();
                surface.ShowDialog();

                if (surface.Result != SurfaceResult.Close && surface.AreaManager.IsCurrentAreaValid)
                {
                    id_screencast = new ImageData(null, screenCapture: true);
                    id_screencast.CaptureRectangle = CaptureHelpers.ClientToScreen(surface.AreaManager.CurrentArea);
                }
            }

            return id_screencast;
        }

        private void PrepareWindowsMenu(ToolStripMenuItem tsmi, EventHandler handler)
        {
            tsmi.DropDownItems.Clear();

            WindowsList windowsList = new WindowsList();
            List<WindowInfo> windows = windowsList.GetVisibleWindowsList();

            if (windows != null)
            {
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
                        log.Error(e);
                    }

                    tsi.Tag = window;
                }

                tsmi.Invalidate();
            }
        }
    }
}