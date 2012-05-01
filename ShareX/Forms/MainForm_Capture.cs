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
using ScreenCapture;
using ShareX.HelperClasses;

namespace ShareX
{
    public partial class MainForm
    {
        private delegate Image ScreenCaptureDelegate();

        private new Image Capture(ScreenCaptureDelegate capture, bool autoHideForm = true)
        {
            if (autoHideForm)
            {
                Hide();
                Thread.Sleep(250);
            }

            Image img = null;

            try
            {
                Screenshot.DrawCursor = Program.Settings.ShowCursor;
                Screenshot.CaptureShadow = Program.Settings.CaptureShadow;
                img = capture();

                if (img != null && Program.Settings.PlaySoundAfterCapture)
                {
                    string soundPath = Path.Combine(Application.StartupPath, "Camera.wav");

                    if (File.Exists(soundPath))
                    {
                        new SoundPlayer(soundPath).Play();
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex);
            }
            finally
            {
                if (autoHideForm)
                {
                    ShowActivate();
                }
            }

            return img;
        }

        private void AfterCapture(Image img)
        {
            if (img != null)
            {
                WizardAfterCaptureConfig configAfterCapture = new WizardAfterCaptureConfig
                {
                    AnnotateImage = Program.Settings.CaptureAnnotateImage,
                    CopyImageToClipboard = Program.Settings.CaptureCopyImage,
                    SaveImageToFile = Program.Settings.CaptureSaveImage,
                    UploadImageToHost = Program.Settings.UploadImageToHost
                };

                if (Program.Settings.ShowAfterCaptureWizard)
                {
                    WindowAfterCapture dlg = new WindowAfterCapture(configAfterCapture);
                    dlg.ShowDialog();
                    configAfterCapture = dlg.Config;
                }

                if (configAfterCapture.AnnotateImage)
                {
                    EditImage(ref img);
                }

                if (configAfterCapture.CopyImageToClipboard)
                {
                    Clipboard.SetImage(img);
                }

                if (configAfterCapture.SaveImageToFile)
                {
                    ImageData imageData = TaskHelper.PrepareImageAndFilename(img);
                    string filePath = imageData.WriteToFile(Program.ScreenshotsPath);

                    if (configAfterCapture.UploadImageToHost)
                    {
                        UploadManager.UploadImageStream(imageData.ImageStream, filePath);
                    }
                    else
                    {
                        imageData.Dispose();
                    }
                }
                else if (configAfterCapture.UploadImageToHost)
                {
                    UploadManager.UploadImage(img);
                }
            }
        }

        private Image CaptureScreen(bool autoHideForm = true)
        {
            return Capture(Screenshot.CaptureFullscreen, autoHideForm);
        }

        private Image CaptureActiveWindow(bool autoHideForm = true)
        {
            if (Program.Settings.CaptureTransparent)
            {
                return Capture(Screenshot.CaptureActiveWindowTransparent, autoHideForm);
            }
            else
            {
                return Capture(Screenshot.CaptureActiveWindow, autoHideForm);
            }
        }

        private Image CaptureActiveMonitor(bool autoHideForm = true)
        {
            return Capture(Screenshot.CaptureActiveMonitor, autoHideForm);
        }

        private Image CaptureWindow(IntPtr handle, bool autoHideForm = true)
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

                 if (Program.Settings.CaptureTransparent)
                 {
                     return Screenshot.CaptureWindowTransparent(handle);
                 }

                 return Screenshot.CaptureWindow(handle);
             }, autoHideForm);
        }

        private Image CaptureRegion(Surface surface, bool autoHideForm = true)
        {
            return Capture(() =>
            {
                Image img = null;
                Image screenshot = Screenshot.CaptureFullscreen();

                surface.Config = Program.Settings.SurfaceOptions;
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

        private Image WindowRectangleCapture(bool autoHideForm = true)
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
                    DebugHelper.WriteException(e);
                }

                tsi.Tag = window;
            }
        }

        #region Menu events

        private void tsmiFullscreen_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.FullScreen));
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
            DoWork(FindTagByHotkey(EHotkey.WindowRectangle));
        }

        private void tsmiRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.RectangleRegion));
        }

        private void tsmiRoundedRectangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.RoundedRectangleRegion));
        }

        private void tsmiEllipse_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.EllipseRegion));
        }

        private void tsmiTriangle_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.TriangleRegion));
        }

        private void tsmiDiamond_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.DiamondRegion));
        }

        private void tsmiPolygon_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.PolygonRegion));
        }

        private void tsmiFreeHand_Click(object sender, EventArgs e)
        {
            DoWork(FindTagByHotkey(EHotkey.FreeHandRegion));
        }

        #endregion Menu events

        #region Tray events

        private void tsmiTrayFullscreen_Click(object sender, EventArgs e)
        {
            CaptureScreen(false);
        }

        private void tsmiCapture_DropDownOpening(object sender, EventArgs e)
        {
            PrepareWindowsMenu(tsmiTrayWindow, tsmiTrayWindowItems_Click);
        }

        private void tsmiTrayWindowItems_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            WindowInfo wi = tsi.Tag as WindowInfo;
            if (wi != null) CaptureWindow(wi.Handle, false);
        }

        private void tsmiTrayWindowRectangle_Click(object sender, EventArgs e)
        {
            WindowRectangleCapture(false);
        }

        private void tsmiTrayRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RectangleRegion(), false);
        }

        private void tsmiTrayRoundedRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RoundedRectangleRegion(), false);
        }

        private void tsmiTrayTriangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new TriangleRegion(), false);
        }

        private void tsmiTrayDiamond_Click(object sender, EventArgs e)
        {
            CaptureRegion(new DiamondRegion(), false);
        }

        private void tsmiTrayPolygon_Click(object sender, EventArgs e)
        {
            CaptureRegion(new PolygonRegion(), false);
        }

        private void tsmiTrayFreeHand_Click(object sender, EventArgs e)
        {
            CaptureRegion(new FreeHandRegion(), false);
        }

        #endregion Tray events
    }
}