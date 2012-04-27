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
using HelpersLib.Hotkey;
using ScreenCapture;
using ShareX.HelperClasses;

namespace ShareX
{
    public partial class MainForm
    {
        private delegate Image ScreenCaptureDelegate();

        private void InitHotkeys()
        {
            HotkeyManager = new HotkeyManager(this);

            Workflow wfClipboardUpload = new Workflow(ZUploaderHotkey.ClipboardUpload.GetDescription(), Program.Settings.HotkeyClipboardUpload, true);
            Workflow wfFileUpload = new Workflow(ZUploaderHotkey.FileUpload.GetDescription(), Program.Settings.HotkeyFileUpload, true);
            Workflow wfPrintScreen = new Workflow(ZUploaderHotkey.PrintScreen.GetDescription(), Program.Settings.HotkeyPrintScreen, true);
            Workflow wfActiveWindow = new Workflow(ZUploaderHotkey.ActiveWindow.GetDescription(), Program.Settings.HotkeyActiveWindow, true);
            Workflow wfActiveMonitor = new Workflow(ZUploaderHotkey.ActiveMonitor.GetDescription(), Program.Settings.HotkeyActiveMonitor, true);
            Workflow WindowRectangle = new Workflow(ZUploaderHotkey.WindowRectangle.GetDescription(), Program.Settings.HotkeyWindowRectangle, true);
            Workflow wfRectangleRegion = new Workflow(ZUploaderHotkey.RectangleRegion.GetDescription(), Program.Settings.HotkeyRectangleRegion, true);
            Workflow wfRoundedRectangleRegion = new Workflow(ZUploaderHotkey.RoundedRectangleRegion.GetDescription(), Program.Settings.HotkeyRoundedRectangleRegion, true);
            Workflow wfEllipseRegion = new Workflow(ZUploaderHotkey.EllipseRegion.GetDescription(), Program.Settings.HotkeyEllipseRegion, true);
            Workflow wfTriangleRegion = new Workflow(ZUploaderHotkey.TriangleRegion.GetDescription(), Program.Settings.HotkeyTriangleRegion, true);
            Workflow wfDiamondRegion = new Workflow(ZUploaderHotkey.DiamondRegion.GetDescription(), Program.Settings.HotkeyDiamondRegion, true);
            Workflow wfPolygonRegion = new Workflow(ZUploaderHotkey.PolygonRegion.GetDescription(), Program.Settings.HotkeyPolygonRegion, true);
            Workflow wfFreeHandRegion = new Workflow(ZUploaderHotkey.FreeHandRegion.GetDescription(), Program.Settings.HotkeyFreeHandRegion, true);

            HotkeyManager.AddHotkeyApp(wfClipboardUpload, UploadManager.ClipboardUpload);
            HotkeyManager.AddHotkeyApp(wfFileUpload, UploadManager.UploadFile);
            HotkeyManager.AddHotkeyApp(wfPrintScreen, () => CaptureScreen(false), tsmiFullscreen);
            HotkeyManager.AddHotkeyApp(wfActiveWindow, () => CaptureActiveWindow(false));
            HotkeyManager.AddHotkeyApp(wfActiveMonitor, () => CaptureActiveMonitor(false));
            HotkeyManager.AddHotkeyApp(WindowRectangle, () => WindowRectangleCapture(false), tsmiWindowRectangle);
            HotkeyManager.AddHotkeyApp(wfRectangleRegion, () => CaptureRegion(new RectangleRegion(), false), tsmiRectangle);
            HotkeyManager.AddHotkeyApp(wfRoundedRectangleRegion, () => CaptureRegion(new RoundedRectangleRegion(), false), tsmiRoundedRectangle);
            HotkeyManager.AddHotkeyApp(wfEllipseRegion, () => CaptureRegion(new EllipseRegion(), false), tsmiEllipse);
            HotkeyManager.AddHotkeyApp(wfTriangleRegion, () => CaptureRegion(new TriangleRegion(), false), tsmiTriangle);
            HotkeyManager.AddHotkeyApp(wfDiamondRegion, () => CaptureRegion(new DiamondRegion(), false), tsmiDiamond);
            HotkeyManager.AddHotkeyApp(wfPolygonRegion, () => CaptureRegion(new PolygonRegion(), false), tsmiPolygon);
            HotkeyManager.AddHotkeyApp(wfFreeHandRegion, () => CaptureRegion(new FreeHandRegion(), false), tsmiFreeHand);

            /*
            if (Program.Settings.Workflows97.Count == 0)
            {
                Workflow wfUser1 = new Workflow("Capture Active Window, Annotate and Upload", new HotkeySetting(Keys.Control | Keys.Shift | Keys.A));
                wfUser1.Activities.Add(EActivity.CaptureActiveWindow);
                wfUser1.Activities.Add(EActivity.ImageAnnotate);
                wfUser1.Activities.Add(EActivity.UploadToRemoteHost);
                Program.Settings.Workflows97.Add(wfUser1);

                Workflow wfUser2 = new Workflow("Crop, Annotate and Upload", new HotkeySetting(Keys.Control | Keys.Shift | Keys.X));
                wfUser2.Activities.Add(EActivity.CaptureRectangleRegion);
                wfUser2.Activities.Add(EActivity.ImageAnnotate);
                wfUser2.Activities.Add(EActivity.UploadToRemoteHost);
                Program.Settings.Workflows97.Add(wfUser2);
            }
            */
            foreach (Workflow wf in Program.Settings.Workflows97)
            {
                HotkeyManager.AddHotkeyUser(wf, () => this.DoWork(wf.HotkeyConfig.Tag));
            }

            string failedHotkeys;

            if (HotkeyManager.IsHotkeyRegisterFailed(out failedHotkeys))
            {
                MessageBox.Show("Unable to register hotkey(s):\r\n\r\n" + failedHotkeys +
                    "\r\n\r\nPlease select a different hotkey or quit the conflicting application and reopen ShareX.",
                    "Hotkey register failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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

                if (img != null && Program.Settings.AutoPlaySound)
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

        private void EditImage(ref Image img)
        {
            if (Greenshot.MainForm.instance == null)
                Greenshot.MainForm.Start(new string[0]);

            GreenshotPlugin.Core.CoreConfiguration coreConfiguration = Greenshot.IniFile.IniConfig.GetIniSection<GreenshotPlugin.Core.CoreConfiguration>();
            coreConfiguration.OutputFileFilenamePattern = "${title}";
            coreConfiguration.OutputFilePath = Program.ScreenshotsPath;

            Greenshot.Plugin.ICapture capture = new GreenshotPlugin.Core.Capture();
            capture.Image = img;
            ImageData imageData = TaskHelper.PrepareImageAndFilename(img);
            capture.CaptureDetails.Filename = Path.Combine(Program.ScreenshotsPath, imageData.Filename);
            capture.CaptureDetails.Title =
                Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
            capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
            capture.CaptureDetails.AddMetaData("source", "file");

            var surface = new Greenshot.Drawing.Surface(capture);
            var editor = new Greenshot.ImageEditorForm(surface, Program.Settings.CaptureSaveImage) { Icon = this.Icon };

            editor.SetImagePath(capture.CaptureDetails.Filename);
            editor.Visible = false;
            editor.ShowDialog();
            img = editor.GetImageForExport();
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
                    UploadImageToHost = Program.Settings.CaptureUploadImage
                };

                if (Program.Settings.ShowAfterCaptureWizard)
                {
                    WindowAfterCapture dlg = new WindowAfterCapture(configAfterCapture);
                    dlg.ShowDialog();
                    configAfterCapture = dlg.Config;
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

        private void CaptureScreen(bool autoHideForm = true)
        {
            Capture(Screenshot.CaptureFullscreen, autoHideForm);
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

        private void CaptureActiveMonitor(bool autoHideForm = true)
        {
            Capture(Screenshot.CaptureActiveMonitor, autoHideForm);
        }

        private void CaptureWindow(IntPtr handle, bool autoHideForm = true)
        {
            autoHideForm = autoHideForm && handle != this.Handle;

            Capture(() =>
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

        private void WindowRectangleCapture(bool autoHideForm = true)
        {
            RectangleRegion rectangleRegion = new RectangleRegion();
            rectangleRegion.AreaManager.WindowCaptureMode = true;
            CaptureRegion(rectangleRegion, autoHideForm);
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
            CaptureScreen();
        }

        private void tsddbCapture_DropDownOpening(object sender, EventArgs e)
        {
            PrepareWindowsMenu(tsmiWindow, tsmiWindowItems_Click);
        }

        private void tsmiWindowItems_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;
            WindowInfo wi = tsi.Tag as WindowInfo;
            if (wi != null) CaptureWindow(wi.Handle);
        }

        private void tsmiWindowRectangle_Click(object sender, EventArgs e)
        {
            WindowRectangleCapture();
        }

        private void tsmiRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RectangleRegion());
        }

        private void tsmiRoundedRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RoundedRectangleRegion());
        }

        private void tsmiEllipse_Click(object sender, EventArgs e)
        {
            CaptureRegion(new EllipseRegion());
        }

        private void tsmiTriangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new TriangleRegion());
        }

        private void tsmiDiamond_Click(object sender, EventArgs e)
        {
            CaptureRegion(new DiamondRegion());
        }

        private void tsmiPolygon_Click(object sender, EventArgs e)
        {
            CaptureRegion(new PolygonRegion());
        }

        private void tsmiFreeHand_Click(object sender, EventArgs e)
        {
            CaptureRegion(new FreeHandRegion());
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