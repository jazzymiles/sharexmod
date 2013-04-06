using Greenshot.Forms;
using Greenshot.Plugin;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using GreenshotPlugin.UnmanagedHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Greenshot
{
    public partial class ImageEditorForm : BaseForm, IImageEditor
    {
        private SettingsForm settingsForm = null;

        // Thumbnail preview
        private FormWithoutActivation thumbnailForm = null;

        private IntPtr thumbnailHandle = IntPtr.Zero;
        private Rectangle parentMenuBounds = Rectangle.Empty;

        public void ShowSetting()
        {
            if (settingsForm == null)
            {
                settingsForm = new SettingsForm();
            }
            if (settingsForm != null)
            {
                WindowDetails.ToForeground(settingsForm.Handle);
                settingsForm.ShowDialog();
            }
        }

        public void AddCaptureWindowMenuItems(ToolStripMenuItem menuItem, EventHandler eventHandler)
        {
            menuItem.DropDownItems.Clear();

            // check if thumbnailPreview is enabled and DWM is enabled
            bool thumbnailPreview = coreConfiguration.ThumnailPreview && DWM.isDWMEnabled();

            List<WindowDetails> windows = WindowDetails.GetTopLevelWindows();
            foreach (WindowDetails window in windows)
            {
                string title = window.Text;
                if (title != null)
                {
                    if (title.Length > coreConfiguration.MaxMenuItemLength)
                    {
                        title = title.Substring(0, Math.Min(title.Length, coreConfiguration.MaxMenuItemLength));
                    }
                    ToolStripItem captureWindowItem = menuItem.DropDownItems.Add(title);
                    captureWindowItem.Tag = window;
                    captureWindowItem.Image = window.DisplayIcon;
                    captureWindowItem.Click += new System.EventHandler(eventHandler);

                    // Only show preview when enabled
                    if (thumbnailPreview)
                    {
                        captureWindowItem.MouseEnter += new System.EventHandler(ShowThumbnailOnEnter);
                        captureWindowItem.MouseLeave += new System.EventHandler(HideThumbnailOnLeave);
                    }
                }
            }
        }

        private void ShowThumbnailOnEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem captureWindowItem = sender as ToolStripMenuItem;
            WindowDetails window = captureWindowItem.Tag as WindowDetails;
            parentMenuBounds = captureWindowItem.GetCurrentParent().TopLevelControl.Bounds;
            if (thumbnailForm == null)
            {
                thumbnailForm = new FormWithoutActivation();
                thumbnailForm.ShowInTaskbar = false;
                thumbnailForm.FormBorderStyle = FormBorderStyle.None;
                thumbnailForm.TopMost = false;
                thumbnailForm.Enabled = false;
                if (coreConfiguration.WindowCaptureMode == WindowCaptureMode.Auto || coreConfiguration.WindowCaptureMode == WindowCaptureMode.Aero)
                {
                    thumbnailForm.BackColor = Color.FromArgb(255, coreConfiguration.DWMBackgroundColor.R, coreConfiguration.DWMBackgroundColor.G, coreConfiguration.DWMBackgroundColor.B);
                }
                else
                {
                    thumbnailForm.BackColor = Color.White;
                }
            }
            if (thumbnailHandle != IntPtr.Zero)
            {
                DWM.DwmUnregisterThumbnail(thumbnailHandle);
                thumbnailHandle = IntPtr.Zero;
            }
            DWM.DwmRegisterThumbnail(thumbnailForm.Handle, window.Handle, out thumbnailHandle);
            if (thumbnailHandle != IntPtr.Zero)
            {
                Rectangle windowRectangle = window.WindowRectangle;
                int thumbnailHeight = 200;
                int thumbnailWidth = (int)(thumbnailHeight * ((float)windowRectangle.Width / (float)windowRectangle.Height));
                if (thumbnailWidth > parentMenuBounds.Width)
                {
                    thumbnailWidth = parentMenuBounds.Width;
                    thumbnailHeight = (int)(thumbnailWidth * ((float)windowRectangle.Height / (float)windowRectangle.Width));
                }
                thumbnailForm.Width = thumbnailWidth;
                thumbnailForm.Height = thumbnailHeight;

                // Prepare the displaying of the Thumbnail
                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();
                props.Opacity = (byte)255;
                props.Visible = true;
                props.SourceClientAreaOnly = false;
                props.Destination = new RECT(0, 0, thumbnailWidth, thumbnailHeight);
                DWM.DwmUpdateThumbnailProperties(thumbnailHandle, ref props);
                if (!thumbnailForm.Visible)
                {
                    thumbnailForm.Show();
                }

                // Make sure it's on "top"!
                User32.SetWindowPos(thumbnailForm.Handle, captureWindowItem.GetCurrentParent().TopLevelControl.Handle, 0, 0, 0, 0, WindowPos.SWP_NOMOVE | WindowPos.SWP_NOSIZE | WindowPos.SWP_NOACTIVATE);

                // Align to menu
                Rectangle screenBounds = WindowCapture.GetScreenBounds();
                if (screenBounds.Contains(parentMenuBounds.Left, parentMenuBounds.Top - thumbnailHeight))
                {
                    thumbnailForm.Location = new Point(parentMenuBounds.Left + (parentMenuBounds.Width / 2) - (thumbnailWidth / 2), parentMenuBounds.Top - thumbnailHeight);
                }
                else
                {
                    thumbnailForm.Location = new Point(parentMenuBounds.Left + (parentMenuBounds.Width / 2) - (thumbnailWidth / 2), parentMenuBounds.Bottom);
                }
            }
        }

        private void hideThumbnail()
        {
            if (thumbnailHandle != IntPtr.Zero)
            {
                DWM.DwmUnregisterThumbnail(thumbnailHandle);
                thumbnailHandle = IntPtr.Zero;
                thumbnailForm.Hide();
            }
        }

        private void HideThumbnailOnLeave(object sender, EventArgs e)
        {
            hideThumbnail();
        }
    }
}