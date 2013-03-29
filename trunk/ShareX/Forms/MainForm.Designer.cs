#region License Information (GPL v3)

/*
    ShareX - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2008-2011 ShareX Developers

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

namespace ShareX
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmsUploads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyImageToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyShortenedURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyThumbnailURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDeletionURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStopUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.showInWindowsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInFullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiContextMenuShare = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsddbOutputs = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbAfterCaptureTasks = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbAfterUploadTasks = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbClipboardUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbFileUpload = new System.Windows.Forms.ToolStripButton();
            this.tsddbCapture = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindowRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLastRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRoundedRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTriangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDiamond = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFreeHand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.screencastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDebug = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiTestImageUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestTextUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestShapeCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbDestinations = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiImageUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTextUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiURLShorteners = new System.Windows.Forms.ToolStripMenuItem();
            this.tssDestinations1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUploadersConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddbShare = new System.Windows.Forms.ToolStripDropDownButton();
            this.tssMain1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.tsddbSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWorkflows = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWatermark = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDebugOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbDonate = new System.Windows.Forms.ToolStripButton();
            this.tscMain = new System.Windows.Forms.ToolStripContainer();
            this.lvUploads = new HelpersLib.MyListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chElapsed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRemaining = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUploaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTrayClipboardUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFileUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFullscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayWindowRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayLastRegion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrayRoundedRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayTriangle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayDiamond = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayPolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFreeHand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrayScreencast = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDestinations = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayImageUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayTextUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayFileUploaders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayURLShorteners = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.tssTray1 = new System.Windows.Forms.ToolStripSeparator();
            this.screenshotsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTraySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTrayDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.tssTray2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUploads.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tscMain.ContentPanel.SuspendLayout();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.cmsTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsUploads
            // 
            this.cmsUploads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openURLToolStripMenuItem,
            this.copyURLToolStripMenuItem,
            this.copyImageToClipboardToolStripMenuItem,
            this.copyShortenedURLToolStripMenuItem,
            this.copyThumbnailURLToolStripMenuItem,
            this.copyDeletionURLToolStripMenuItem,
            this.showErrorsToolStripMenuItem,
            this.copyErrorsToolStripMenuItem,
            this.showResponseToolStripMenuItem,
            this.uploadFileToolStripMenuItem,
            this.tsmiStopUpload,
            this.showInWindowsExplorerToolStripMenuItem,
            this.viewInFullscreenToolStripMenuItem,
            this.tsmiUpload,
            this.tsmiContextMenuShare});
            this.cmsUploads.Name = "cmsUploads";
            this.cmsUploads.ShowItemToolTips = false;
            this.cmsUploads.Size = new System.Drawing.Size(264, 386);
            // 
            // openURLToolStripMenuItem
            // 
            this.openURLToolStripMenuItem.Name = "openURLToolStripMenuItem";
            this.openURLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openURLToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.openURLToolStripMenuItem.Text = "Open URL";
            this.openURLToolStripMenuItem.Visible = false;
            this.openURLToolStripMenuItem.Click += new System.EventHandler(this.openURLToolStripMenuItem_Click);
            // 
            // copyURLToolStripMenuItem
            // 
            this.copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            this.copyURLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyURLToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.copyURLToolStripMenuItem.Text = "Copy URL";
            this.copyURLToolStripMenuItem.Visible = false;
            this.copyURLToolStripMenuItem.Click += new System.EventHandler(this.copyURLToolStripMenuItem_Click);
            // 
            // copyImageToClipboardToolStripMenuItem
            // 
            this.copyImageToClipboardToolStripMenuItem.Image = global::ShareX.Properties.Resources.image;
            this.copyImageToClipboardToolStripMenuItem.Name = "copyImageToClipboardToolStripMenuItem";
            this.copyImageToClipboardToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.copyImageToClipboardToolStripMenuItem.Text = "Copy image to clipboard";
            this.copyImageToClipboardToolStripMenuItem.Visible = false;
            this.copyImageToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyImageToClipboardToolStripMenuItem_Click);
            // 
            // copyShortenedURLToolStripMenuItem
            // 
            this.copyShortenedURLToolStripMenuItem.Name = "copyShortenedURLToolStripMenuItem";
            this.copyShortenedURLToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.copyShortenedURLToolStripMenuItem.Text = "Copy Shortened URL";
            this.copyShortenedURLToolStripMenuItem.Visible = false;
            this.copyShortenedURLToolStripMenuItem.Click += new System.EventHandler(this.copyShortenedURLToolStripMenuItem_Click);
            // 
            // copyThumbnailURLToolStripMenuItem
            // 
            this.copyThumbnailURLToolStripMenuItem.Name = "copyThumbnailURLToolStripMenuItem";
            this.copyThumbnailURLToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.copyThumbnailURLToolStripMenuItem.Text = "Copy URL";
            this.copyThumbnailURLToolStripMenuItem.Visible = false;
            this.copyThumbnailURLToolStripMenuItem.Click += new System.EventHandler(this.copyURLToolStripMenuItem_Click);
            // 
            // copyDeletionURLToolStripMenuItem
            // 
            this.copyDeletionURLToolStripMenuItem.Name = "copyDeletionURLToolStripMenuItem";
            this.copyDeletionURLToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.copyDeletionURLToolStripMenuItem.Text = "Copy Deletion URL";
            this.copyDeletionURLToolStripMenuItem.Visible = false;
            this.copyDeletionURLToolStripMenuItem.Click += new System.EventHandler(this.copyDeletionURLToolStripMenuItem_Click);
            // 
            // showErrorsToolStripMenuItem
            // 
            this.showErrorsToolStripMenuItem.Name = "showErrorsToolStripMenuItem";
            this.showErrorsToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.showErrorsToolStripMenuItem.Text = "Show Errors";
            this.showErrorsToolStripMenuItem.Visible = false;
            this.showErrorsToolStripMenuItem.Click += new System.EventHandler(this.showErrorsToolStripMenuItem_Click);
            // 
            // copyErrorsToolStripMenuItem
            // 
            this.copyErrorsToolStripMenuItem.Name = "copyErrorsToolStripMenuItem";
            this.copyErrorsToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.copyErrorsToolStripMenuItem.Text = "Copy Errors";
            this.copyErrorsToolStripMenuItem.Visible = false;
            this.copyErrorsToolStripMenuItem.Click += new System.EventHandler(this.copyErrorsToolStripMenuItem_Click);
            // 
            // showResponseToolStripMenuItem
            // 
            this.showResponseToolStripMenuItem.Name = "showResponseToolStripMenuItem";
            this.showResponseToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.showResponseToolStripMenuItem.Text = "Show Response";
            this.showResponseToolStripMenuItem.Visible = false;
            this.showResponseToolStripMenuItem.Click += new System.EventHandler(this.showResponseToolStripMenuItem_Click);
            // 
            // uploadFileToolStripMenuItem
            // 
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.uploadFileToolStripMenuItem.Text = "Upload file...";
            this.uploadFileToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            // 
            // tsmiStopUpload
            // 
            this.tsmiStopUpload.Name = "tsmiStopUpload";
            this.tsmiStopUpload.Size = new System.Drawing.Size(263, 24);
            this.tsmiStopUpload.Text = "Stop upload";
            this.tsmiStopUpload.Visible = false;
            this.tsmiStopUpload.Click += new System.EventHandler(this.stopUploadToolStripMenuItem_Click);
            // 
            // showInWindowsExplorerToolStripMenuItem
            // 
            this.showInWindowsExplorerToolStripMenuItem.Image = global::ShareX.Properties.Resources.folder;
            this.showInWindowsExplorerToolStripMenuItem.Name = "showInWindowsExplorerToolStripMenuItem";
            this.showInWindowsExplorerToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.showInWindowsExplorerToolStripMenuItem.Text = "Show in Windows Explorer...";
            this.showInWindowsExplorerToolStripMenuItem.Visible = false;
            this.showInWindowsExplorerToolStripMenuItem.Click += new System.EventHandler(this.showInWindowsExplorerToolStripMenuItem_Click);
            // 
            // viewInFullscreenToolStripMenuItem
            // 
            this.viewInFullscreenToolStripMenuItem.Image = global::ShareX.Properties.Resources.Fullscreen;
            this.viewInFullscreenToolStripMenuItem.Name = "viewInFullscreenToolStripMenuItem";
            this.viewInFullscreenToolStripMenuItem.Size = new System.Drawing.Size(263, 24);
            this.viewInFullscreenToolStripMenuItem.Text = "View in Fullscreen";
            this.viewInFullscreenToolStripMenuItem.Visible = false;
            this.viewInFullscreenToolStripMenuItem.Click += new System.EventHandler(this.viewInFullscreenToolStripMenuItem_Click);
            // 
            // tsmiUpload
            // 
            this.tsmiUpload.Image = global::ShareX.Properties.Resources.server;
            this.tsmiUpload.Name = "tsmiUpload";
            this.tsmiUpload.Size = new System.Drawing.Size(263, 24);
            this.tsmiUpload.Text = "Upload";
            this.tsmiUpload.Visible = false;
            this.tsmiUpload.Click += new System.EventHandler(this.tsmiContextMenuUpload_Click);
            // 
            // tsmiContextMenuShare
            // 
            this.tsmiContextMenuShare.Image = global::ShareX.Properties.Resources.share;
            this.tsmiContextMenuShare.Name = "tsmiContextMenuShare";
            this.tsmiContextMenuShare.Size = new System.Drawing.Size(263, 24);
            this.tsmiContextMenuShare.Text = "Share using";
            this.tsmiContextMenuShare.Visible = false;
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbOutputs,
            this.tsddbAfterCaptureTasks,
            this.tsddbAfterUploadTasks,
            this.tsbClipboardUpload,
            this.tsbFileUpload,
            this.tsddbCapture,
            this.tsbDebug,
            this.tsddbDestinations,
            this.tsddbShare,
            this.tssMain1,
            this.tsbHistory,
            this.tsddbSettings,
            this.tsbAbout,
            this.tsbDonate});
            this.tsMain.Location = new System.Drawing.Point(3, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(4, 6, 4, 4);
            this.tsMain.ShowItemToolTips = false;
            this.tsMain.Size = new System.Drawing.Size(865, 37);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsddbOutputs
            // 
            this.tsddbOutputs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbOutputs.Image = global::ShareX.Properties.Resources.target;
            this.tsddbOutputs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbOutputs.Name = "tsddbOutputs";
            this.tsddbOutputs.Size = new System.Drawing.Size(29, 24);
            this.tsddbOutputs.Text = "Outputs";
            // 
            // tsddbAfterCaptureTasks
            // 
            this.tsddbAfterCaptureTasks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbAfterCaptureTasks.Image = global::ShareX.Properties.Resources.camera_add;
            this.tsddbAfterCaptureTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbAfterCaptureTasks.Name = "tsddbAfterCaptureTasks";
            this.tsddbAfterCaptureTasks.Size = new System.Drawing.Size(29, 24);
            this.tsddbAfterCaptureTasks.Text = "After Capture Tasks";
            this.tsddbAfterCaptureTasks.ToolTipText = "Default After Capture Tasks";
            // 
            // tsddbAfterUploadTasks
            // 
            this.tsddbAfterUploadTasks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbAfterUploadTasks.Image = global::ShareX.Properties.Resources.world_link;
            this.tsddbAfterUploadTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbAfterUploadTasks.Name = "tsddbAfterUploadTasks";
            this.tsddbAfterUploadTasks.Size = new System.Drawing.Size(29, 24);
            this.tsddbAfterUploadTasks.Text = "After upload tasks";
            // 
            // tsbClipboardUpload
            // 
            this.tsbClipboardUpload.Image = global::ShareX.Properties.Resources.clipboard__plus;
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(197, 24);
            this.tsbClipboardUpload.Text = "Share clipboard content...";
            this.tsbClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            // 
            // tsbFileUpload
            // 
            this.tsbFileUpload.Image = global::ShareX.Properties.Resources.folder__plus;
            this.tsbFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileUpload.Name = "tsbFileUpload";
            this.tsbFileUpload.Size = new System.Drawing.Size(106, 24);
            this.tsbFileUpload.Text = "Share files...";
            this.tsbFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            // 
            // tsddbCapture
            // 
            this.tsddbCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFullscreen,
            this.tsmiWindow,
            this.tsmiWindowRectangle,
            this.tsmiRectangle,
            this.tsmiLastRegion,
            this.toolStripSeparator1,
            this.tsmiRoundedRectangle,
            this.tsmiEllipse,
            this.tsmiTriangle,
            this.tsmiDiamond,
            this.tsmiPolygon,
            this.tsmiFreeHand,
            this.toolStripSeparator4,
            this.screencastToolStripMenuItem});
            this.tsddbCapture.Image = global::ShareX.Properties.Resources.camera;
            this.tsddbCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbCapture.Name = "tsddbCapture";
            this.tsddbCapture.Size = new System.Drawing.Size(90, 24);
            this.tsddbCapture.Text = "Capture";
            this.tsddbCapture.DropDownOpening += new System.EventHandler(this.tsddbCapture_DropDownOpening);
            // 
            // tsmiFullscreen
            // 
            this.tsmiFullscreen.Image = global::ShareX.Properties.Resources.Fullscreen;
            this.tsmiFullscreen.Name = "tsmiFullscreen";
            this.tsmiFullscreen.Size = new System.Drawing.Size(219, 24);
            this.tsmiFullscreen.Text = "Fullscreen";
            this.tsmiFullscreen.Click += new System.EventHandler(this.tsmiFullscreen_Click);
            // 
            // tsmiWindow
            // 
            this.tsmiWindow.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiWindow.Name = "tsmiWindow";
            this.tsmiWindow.Size = new System.Drawing.Size(219, 24);
            this.tsmiWindow.Text = "Window";
            // 
            // tsmiWindowRectangle
            // 
            this.tsmiWindowRectangle.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiWindowRectangle.Name = "tsmiWindowRectangle";
            this.tsmiWindowRectangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiWindowRectangle.Text = "Window && Rectangle";
            this.tsmiWindowRectangle.Click += new System.EventHandler(this.tsmiWindowRectangle_Click);
            // 
            // tsmiRectangle
            // 
            this.tsmiRectangle.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiRectangle.Name = "tsmiRectangle";
            this.tsmiRectangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiRectangle.Text = "Rectangle";
            this.tsmiRectangle.Click += new System.EventHandler(this.tsmiRectangle_Click);
            // 
            // tsmiLastRegion
            // 
            this.tsmiLastRegion.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiLastRegion.Name = "tsmiLastRegion";
            this.tsmiLastRegion.Size = new System.Drawing.Size(219, 24);
            this.tsmiLastRegion.Text = "Last Region";
            this.tsmiLastRegion.Click += new System.EventHandler(this.tsmiLastRegion_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // tsmiRoundedRectangle
            // 
            this.tsmiRoundedRectangle.Image = global::ShareX.Properties.Resources.RoundedRectangle;
            this.tsmiRoundedRectangle.Name = "tsmiRoundedRectangle";
            this.tsmiRoundedRectangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiRoundedRectangle.Text = "Rounded Rectangle";
            this.tsmiRoundedRectangle.Click += new System.EventHandler(this.tsmiRoundedRectangle_Click);
            // 
            // tsmiEllipse
            // 
            this.tsmiEllipse.Image = global::ShareX.Properties.Resources.Ellipse;
            this.tsmiEllipse.Name = "tsmiEllipse";
            this.tsmiEllipse.Size = new System.Drawing.Size(219, 24);
            this.tsmiEllipse.Text = "Ellipse";
            this.tsmiEllipse.Click += new System.EventHandler(this.tsmiEllipse_Click);
            // 
            // tsmiTriangle
            // 
            this.tsmiTriangle.Image = global::ShareX.Properties.Resources.Triangle;
            this.tsmiTriangle.Name = "tsmiTriangle";
            this.tsmiTriangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiTriangle.Text = "Triangle";
            this.tsmiTriangle.Click += new System.EventHandler(this.tsmiTriangle_Click);
            // 
            // tsmiDiamond
            // 
            this.tsmiDiamond.Image = global::ShareX.Properties.Resources.Diamond;
            this.tsmiDiamond.Name = "tsmiDiamond";
            this.tsmiDiamond.Size = new System.Drawing.Size(219, 24);
            this.tsmiDiamond.Text = "Diamond";
            this.tsmiDiamond.Click += new System.EventHandler(this.tsmiDiamond_Click);
            // 
            // tsmiPolygon
            // 
            this.tsmiPolygon.Image = global::ShareX.Properties.Resources.Polygon;
            this.tsmiPolygon.Name = "tsmiPolygon";
            this.tsmiPolygon.Size = new System.Drawing.Size(219, 24);
            this.tsmiPolygon.Text = "Polygon";
            this.tsmiPolygon.Click += new System.EventHandler(this.tsmiPolygon_Click);
            // 
            // tsmiFreeHand
            // 
            this.tsmiFreeHand.Image = global::ShareX.Properties.Resources.FreeHand;
            this.tsmiFreeHand.Name = "tsmiFreeHand";
            this.tsmiFreeHand.Size = new System.Drawing.Size(219, 24);
            this.tsmiFreeHand.Text = "Free Hand";
            this.tsmiFreeHand.Click += new System.EventHandler(this.tsmiFreeHand_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(216, 6);
            // 
            // screencastToolStripMenuItem
            // 
            this.screencastToolStripMenuItem.Image = global::ShareX.Properties.Resources.camcorder_image;
            this.screencastToolStripMenuItem.Name = "screencastToolStripMenuItem";
            this.screencastToolStripMenuItem.Size = new System.Drawing.Size(219, 24);
            this.screencastToolStripMenuItem.Text = "Screencast";
            this.screencastToolStripMenuItem.Click += new System.EventHandler(this.screencastToolStripMenuItem_Click);
            // 
            // tsbDebug
            // 
            this.tsbDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestImageUpload,
            this.tsmiTestTextUpload,
            this.tsmiTestShapeCapture});
            this.tsbDebug.Image = global::ShareX.Properties.Resources.gear;
            this.tsbDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDebug.Name = "tsbDebug";
            this.tsbDebug.Size = new System.Drawing.Size(65, 24);
            this.tsbDebug.Text = "Test";
            this.tsbDebug.Visible = false;
            // 
            // tsmiTestImageUpload
            // 
            this.tsmiTestImageUpload.Name = "tsmiTestImageUpload";
            this.tsmiTestImageUpload.Size = new System.Drawing.Size(202, 24);
            this.tsmiTestImageUpload.Text = "Test image upload";
            this.tsmiTestImageUpload.Click += new System.EventHandler(this.tsmiTestImageUpload_Click);
            // 
            // tsmiTestTextUpload
            // 
            this.tsmiTestTextUpload.Name = "tsmiTestTextUpload";
            this.tsmiTestTextUpload.Size = new System.Drawing.Size(202, 24);
            this.tsmiTestTextUpload.Text = "Test text upload";
            this.tsmiTestTextUpload.Click += new System.EventHandler(this.tsmiTestTextUpload_Click);
            // 
            // tsmiTestShapeCapture
            // 
            this.tsmiTestShapeCapture.Name = "tsmiTestShapeCapture";
            this.tsmiTestShapeCapture.Size = new System.Drawing.Size(202, 24);
            this.tsmiTestShapeCapture.Text = "Test shape capture";
            this.tsmiTestShapeCapture.Click += new System.EventHandler(this.tsmiTestShapeCapture_Click);
            // 
            // tsddbDestinations
            // 
            this.tsddbDestinations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiImageUploaders,
            this.tsmiTextUploaders,
            this.tsmiFileUploaders,
            this.tsmiURLShorteners,
            this.tssDestinations1,
            this.tsmiUploadersConfig});
            this.tsddbDestinations.Image = global::ShareX.Properties.Resources.server;
            this.tsddbDestinations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestinations.Name = "tsddbDestinations";
            this.tsddbDestinations.Size = new System.Drawing.Size(120, 24);
            this.tsddbDestinations.Text = "Destinations";
            this.tsddbDestinations.DropDownOpening += new System.EventHandler(this.tsddbDestinations_DropDownOpening);
            // 
            // tsmiImageUploaders
            // 
            this.tsmiImageUploaders.Image = global::ShareX.Properties.Resources.image;
            this.tsmiImageUploaders.Name = "tsmiImageUploaders";
            this.tsmiImageUploaders.Size = new System.Drawing.Size(202, 24);
            this.tsmiImageUploaders.Text = "Image uploaders";
            // 
            // tsmiTextUploaders
            // 
            this.tsmiTextUploaders.Image = global::ShareX.Properties.Resources.notebook;
            this.tsmiTextUploaders.Name = "tsmiTextUploaders";
            this.tsmiTextUploaders.Size = new System.Drawing.Size(202, 24);
            this.tsmiTextUploaders.Text = "Text uploaders";
            // 
            // tsmiFileUploaders
            // 
            this.tsmiFileUploaders.Image = global::ShareX.Properties.Resources.application_block;
            this.tsmiFileUploaders.Name = "tsmiFileUploaders";
            this.tsmiFileUploaders.Size = new System.Drawing.Size(202, 24);
            this.tsmiFileUploaders.Text = "File uploaders";
            // 
            // tsmiURLShorteners
            // 
            this.tsmiURLShorteners.Image = global::ShareX.Properties.Resources.edit_scale;
            this.tsmiURLShorteners.Name = "tsmiURLShorteners";
            this.tsmiURLShorteners.Size = new System.Drawing.Size(202, 24);
            this.tsmiURLShorteners.Text = "URL shorteners";
            // 
            // tssDestinations1
            // 
            this.tssDestinations1.Name = "tssDestinations1";
            this.tssDestinations1.Size = new System.Drawing.Size(199, 6);
            // 
            // tsmiUploadersConfig
            // 
            this.tsmiUploadersConfig.Image = global::ShareX.Properties.Resources.gear;
            this.tsmiUploadersConfig.Name = "tsmiUploadersConfig";
            this.tsmiUploadersConfig.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.tsmiUploadersConfig.Size = new System.Drawing.Size(202, 24);
            this.tsmiUploadersConfig.Text = "Configuration...";
            this.tsmiUploadersConfig.Click += new System.EventHandler(this.tsddbUploadersConfig_Click);
            // 
            // tsddbShare
            // 
            this.tsddbShare.Image = global::ShareX.Properties.Resources.share;
            this.tsddbShare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbShare.Name = "tsddbShare";
            this.tsddbShare.Size = new System.Drawing.Size(107, 24);
            this.tsddbShare.Text = "Share with";
            this.tsddbShare.Visible = false;
            // 
            // tssMain1
            // 
            this.tssMain1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tssMain1.Name = "tssMain1";
            this.tssMain1.Size = new System.Drawing.Size(6, 27);
            this.tssMain1.Visible = false;
            // 
            // tsbHistory
            // 
            this.tsbHistory.Image = global::ShareX.Properties.Resources.address_book_blue;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(85, 24);
            this.tsbHistory.Text = "History...";
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // tsddbSettings
            // 
            this.tsddbSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettings,
            this.tsmiWorkflows,
            this.tsmiWatermark,
            this.toolStripSeparator3,
            this.tsmiDebugOpen});
            this.tsddbSettings.Image = global::ShareX.Properties.Resources.application_form;
            this.tsddbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbSettings.Name = "tsddbSettings";
            this.tsddbSettings.Size = new System.Drawing.Size(91, 24);
            this.tsddbSettings.Text = "&Settings";
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Image = global::ShareX.Properties.Resources.gear;
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.tsmiSettings.Size = new System.Drawing.Size(213, 24);
            this.tsmiSettings.Text = "Configuration...";
            this.tsmiSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
            // 
            // tsmiWorkflows
            // 
            this.tsmiWorkflows.Name = "tsmiWorkflows";
            this.tsmiWorkflows.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tsmiWorkflows.Size = new System.Drawing.Size(213, 24);
            this.tsmiWorkflows.Text = "Workflows...";
            this.tsmiWorkflows.Click += new System.EventHandler(this.tsmiWorkflows_Click);
            // 
            // tsmiWatermark
            // 
            this.tsmiWatermark.Name = "tsmiWatermark";
            this.tsmiWatermark.Size = new System.Drawing.Size(213, 24);
            this.tsmiWatermark.Text = "Watermark...";
            this.tsmiWatermark.Click += new System.EventHandler(this.tsmiWatermark_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(210, 6);
            // 
            // tsmiDebugOpen
            // 
            this.tsmiDebugOpen.Name = "tsmiDebugOpen";
            this.tsmiDebugOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.tsmiDebugOpen.Size = new System.Drawing.Size(213, 24);
            this.tsmiDebugOpen.Text = "Debug...";
            this.tsmiDebugOpen.Click += new System.EventHandler(this.tsmiDebugOpen_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Image = global::ShareX.Properties.Resources.application_browser;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(79, 24);
            this.tsbAbout.Text = "About...";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tsbDonate
            // 
            this.tsbDonate.Image = global::ShareX.Properties.Resources.present;
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(87, 24);
            this.tsbDonate.Text = "Donate...";
            this.tsbDonate.Visible = false;
            this.tsbDonate.Click += new System.EventHandler(this.tsbDonate_Click);
            // 
            // tscMain
            // 
            this.tscMain.BottomToolStripPanelVisible = false;
            // 
            // tscMain.ContentPanel
            // 
            this.tscMain.ContentPanel.Controls.Add(this.lvUploads);
            this.tscMain.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.tscMain.ContentPanel.Padding = new System.Windows.Forms.Padding(4);
            this.tscMain.ContentPanel.Size = new System.Drawing.Size(1253, 438);
            this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscMain.LeftToolStripPanelVisible = false;
            this.tscMain.Location = new System.Drawing.Point(0, 0);
            this.tscMain.Margin = new System.Windows.Forms.Padding(4);
            this.tscMain.Name = "tscMain";
            this.tscMain.RightToolStripPanelVisible = false;
            this.tscMain.Size = new System.Drawing.Size(1253, 475);
            this.tscMain.TabIndex = 0;
            this.tscMain.Text = "toolStripContainer1";
            // 
            // tscMain.TopToolStripPanel
            // 
            this.tscMain.TopToolStripPanel.Controls.Add(this.tsMain);
            // 
            // lvUploads
            // 
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chStatus,
            this.chProgress,
            this.chSpeed,
            this.chElapsed,
            this.chRemaining,
            this.chUploaderType,
            this.chHost,
            this.chURL});
            this.lvUploads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUploads.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(4, 4);
            this.lvUploads.Margin = new System.Windows.Forms.Padding(4);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.ShowItemToolTips = true;
            this.lvUploads.Size = new System.Drawing.Size(1245, 430);
            this.lvUploads.TabIndex = 0;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
            this.lvUploads.SelectedIndexChanged += new System.EventHandler(this.lvUploads_SelectedIndexChanged);
            this.lvUploads.DoubleClick += new System.EventHandler(this.lvUploads_DoubleClick);
            this.lvUploads.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvUploads_MouseUp);
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 150;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 75;
            // 
            // chProgress
            // 
            this.chProgress.Text = "Progress";
            this.chProgress.Width = 47;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Speed";
            this.chSpeed.Width = 65;
            // 
            // chElapsed
            // 
            this.chElapsed.Text = "Elapsed";
            this.chElapsed.Width = 63;
            // 
            // chRemaining
            // 
            this.chRemaining.Text = "Remaining";
            this.chRemaining.Width = 70;
            // 
            // chUploaderType
            // 
            this.chUploaderType.Text = "Type";
            this.chUploaderType.Width = 50;
            // 
            // chHost
            // 
            this.chHost.Text = "Destination";
            this.chHost.Width = 115;
            // 
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 239;
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmsTray;
            this.niTray.Text = "ShareX";
            this.niTray.BalloonTipClicked += new System.EventHandler(this.niTray_BalloonTipClicked);
            this.niTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseDoubleClick);
            // 
            // cmsTray
            // 
            this.cmsTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayClipboardUpload,
            this.tsmiTrayFileUpload,
            this.tsmiCapture,
            this.tsmiDestinations,
            this.tssTray1,
            this.screenshotsFolderToolStripMenuItem,
            this.tsmiHistory,
            this.tsmiTraySettings,
            this.tsmiTrayAbout,
            this.tsmiTrayDonate,
            this.tssTray2,
            this.tsmiTrayExit});
            this.cmsTray.Name = "cmsTray";
            this.cmsTray.Size = new System.Drawing.Size(210, 256);
            // 
            // tsmiTrayClipboardUpload
            // 
            this.tsmiTrayClipboardUpload.Image = global::ShareX.Properties.Resources.clipboard__plus;
            this.tsmiTrayClipboardUpload.Name = "tsmiTrayClipboardUpload";
            this.tsmiTrayClipboardUpload.Size = new System.Drawing.Size(209, 24);
            this.tsmiTrayClipboardUpload.Text = "Clipboard upload...";
            this.tsmiTrayClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            // 
            // tsmiTrayFileUpload
            // 
            this.tsmiTrayFileUpload.Image = global::ShareX.Properties.Resources.folder__plus;
            this.tsmiTrayFileUpload.Name = "tsmiTrayFileUpload";
            this.tsmiTrayFileUpload.Size = new System.Drawing.Size(209, 24);
            this.tsmiTrayFileUpload.Text = "File upload...";
            this.tsmiTrayFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            // 
            // tsmiCapture
            // 
            this.tsmiCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayFullscreen,
            this.tsmiTrayWindow,
            this.tsmiTrayWindowRectangle,
            this.tsmiTrayRectangle,
            this.tsmiTrayLastRegion,
            this.toolStripSeparator5,
            this.tsmiTrayRoundedRectangle,
            this.tsmiTrayEllipse,
            this.tsmiTrayTriangle,
            this.tsmiTrayDiamond,
            this.tsmiTrayPolygon,
            this.tsmiTrayFreeHand,
            this.toolStripSeparator6,
            this.tsmiTrayScreencast});
            this.tsmiCapture.Image = global::ShareX.Properties.Resources.camera;
            this.tsmiCapture.Name = "tsmiCapture";
            this.tsmiCapture.Size = new System.Drawing.Size(209, 24);
            this.tsmiCapture.Text = "Capture";
            this.tsmiCapture.DropDownOpening += new System.EventHandler(this.tsmiCapture_DropDownOpening);
            // 
            // tsmiTrayFullscreen
            // 
            this.tsmiTrayFullscreen.Image = global::ShareX.Properties.Resources.Fullscreen;
            this.tsmiTrayFullscreen.Name = "tsmiTrayFullscreen";
            this.tsmiTrayFullscreen.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayFullscreen.Text = "Fullscreen";
            this.tsmiTrayFullscreen.Click += new System.EventHandler(this.tsmiTrayFullscreen_Click);
            // 
            // tsmiTrayWindow
            // 
            this.tsmiTrayWindow.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiTrayWindow.Name = "tsmiTrayWindow";
            this.tsmiTrayWindow.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayWindow.Text = "Window";
            // 
            // tsmiTrayWindowRectangle
            // 
            this.tsmiTrayWindowRectangle.Image = global::ShareX.Properties.Resources.Window;
            this.tsmiTrayWindowRectangle.Name = "tsmiTrayWindowRectangle";
            this.tsmiTrayWindowRectangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayWindowRectangle.Text = "Window && Rectangle";
            this.tsmiTrayWindowRectangle.Click += new System.EventHandler(this.tsmiTrayWindowRectangle_Click);
            // 
            // tsmiTrayRectangle
            // 
            this.tsmiTrayRectangle.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiTrayRectangle.Name = "tsmiTrayRectangle";
            this.tsmiTrayRectangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayRectangle.Text = "Rectangle";
            this.tsmiTrayRectangle.Click += new System.EventHandler(this.tsmiTrayRectangle_Click);
            // 
            // tsmiTrayLastRegion
            // 
            this.tsmiTrayLastRegion.Image = global::ShareX.Properties.Resources.Rectangle;
            this.tsmiTrayLastRegion.Name = "tsmiTrayLastRegion";
            this.tsmiTrayLastRegion.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayLastRegion.Text = "Last Region";
            this.tsmiTrayLastRegion.Click += new System.EventHandler(this.tsmiTrayLastRegion_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(216, 6);
            // 
            // tsmiTrayRoundedRectangle
            // 
            this.tsmiTrayRoundedRectangle.Image = global::ShareX.Properties.Resources.RoundedRectangle;
            this.tsmiTrayRoundedRectangle.Name = "tsmiTrayRoundedRectangle";
            this.tsmiTrayRoundedRectangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayRoundedRectangle.Text = "Rounded Rectangle";
            this.tsmiTrayRoundedRectangle.Click += new System.EventHandler(this.tsmiTrayRoundedRectangle_Click);
            // 
            // tsmiTrayEllipse
            // 
            this.tsmiTrayEllipse.Image = global::ShareX.Properties.Resources.Ellipse;
            this.tsmiTrayEllipse.Name = "tsmiTrayEllipse";
            this.tsmiTrayEllipse.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayEllipse.Text = "Ellipse";
            this.tsmiTrayEllipse.Click += new System.EventHandler(this.tsmiTrayEllipse_Click);
            // 
            // tsmiTrayTriangle
            // 
            this.tsmiTrayTriangle.Image = global::ShareX.Properties.Resources.Triangle;
            this.tsmiTrayTriangle.Name = "tsmiTrayTriangle";
            this.tsmiTrayTriangle.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayTriangle.Text = "Triangle";
            this.tsmiTrayTriangle.Click += new System.EventHandler(this.tsmiTrayTriangle_Click);
            // 
            // tsmiTrayDiamond
            // 
            this.tsmiTrayDiamond.Image = global::ShareX.Properties.Resources.Diamond;
            this.tsmiTrayDiamond.Name = "tsmiTrayDiamond";
            this.tsmiTrayDiamond.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayDiamond.Text = "Diamond";
            this.tsmiTrayDiamond.Click += new System.EventHandler(this.tsmiTrayDiamond_Click);
            // 
            // tsmiTrayPolygon
            // 
            this.tsmiTrayPolygon.Image = global::ShareX.Properties.Resources.Polygon;
            this.tsmiTrayPolygon.Name = "tsmiTrayPolygon";
            this.tsmiTrayPolygon.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayPolygon.Text = "Polygon";
            this.tsmiTrayPolygon.Click += new System.EventHandler(this.tsmiTrayPolygon_Click);
            // 
            // tsmiTrayFreeHand
            // 
            this.tsmiTrayFreeHand.Image = global::ShareX.Properties.Resources.FreeHand;
            this.tsmiTrayFreeHand.Name = "tsmiTrayFreeHand";
            this.tsmiTrayFreeHand.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayFreeHand.Text = "Free Hand";
            this.tsmiTrayFreeHand.Click += new System.EventHandler(this.tsmiTrayFreeHand_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(216, 6);
            // 
            // tsmiTrayScreencast
            // 
            this.tsmiTrayScreencast.Image = global::ShareX.Properties.Resources.camcorder_image;
            this.tsmiTrayScreencast.Name = "tsmiTrayScreencast";
            this.tsmiTrayScreencast.Size = new System.Drawing.Size(219, 24);
            this.tsmiTrayScreencast.Text = "Screencast";
            this.tsmiTrayScreencast.Click += new System.EventHandler(this.tsmiTrayScreencast_Click);
            // 
            // tsmiDestinations
            // 
            this.tsmiDestinations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTrayImageUploaders,
            this.tsmiTrayTextUploaders,
            this.tsmiTrayFileUploaders,
            this.tsmiTrayURLShorteners,
            this.toolStripSeparator2,
            this.tsmiConfiguration});
            this.tsmiDestinations.Image = global::ShareX.Properties.Resources.server;
            this.tsmiDestinations.Name = "tsmiDestinations";
            this.tsmiDestinations.Size = new System.Drawing.Size(209, 24);
            this.tsmiDestinations.Text = "Destinations";
            // 
            // tsmiTrayImageUploaders
            // 
            this.tsmiTrayImageUploaders.Image = global::ShareX.Properties.Resources.image;
            this.tsmiTrayImageUploaders.Name = "tsmiTrayImageUploaders";
            this.tsmiTrayImageUploaders.Size = new System.Drawing.Size(201, 24);
            this.tsmiTrayImageUploaders.Text = "Image uploaders";
            // 
            // tsmiTrayTextUploaders
            // 
            this.tsmiTrayTextUploaders.Image = global::ShareX.Properties.Resources.notebook;
            this.tsmiTrayTextUploaders.Name = "tsmiTrayTextUploaders";
            this.tsmiTrayTextUploaders.Size = new System.Drawing.Size(201, 24);
            this.tsmiTrayTextUploaders.Text = "Text uploaders";
            // 
            // tsmiTrayFileUploaders
            // 
            this.tsmiTrayFileUploaders.Image = global::ShareX.Properties.Resources.application_block;
            this.tsmiTrayFileUploaders.Name = "tsmiTrayFileUploaders";
            this.tsmiTrayFileUploaders.Size = new System.Drawing.Size(201, 24);
            this.tsmiTrayFileUploaders.Text = "File uploaders";
            // 
            // tsmiTrayURLShorteners
            // 
            this.tsmiTrayURLShorteners.Image = global::ShareX.Properties.Resources.edit_scale;
            this.tsmiTrayURLShorteners.Name = "tsmiTrayURLShorteners";
            this.tsmiTrayURLShorteners.Size = new System.Drawing.Size(201, 24);
            this.tsmiTrayURLShorteners.Text = "URL shorteners";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // tsmiConfiguration
            // 
            this.tsmiConfiguration.Image = global::ShareX.Properties.Resources.gear;
            this.tsmiConfiguration.Name = "tsmiConfiguration";
            this.tsmiConfiguration.Size = new System.Drawing.Size(201, 24);
            this.tsmiConfiguration.Text = "Uploaders config...";
            this.tsmiConfiguration.Click += new System.EventHandler(this.tsddbUploadersConfig_Click);
            // 
            // tssTray1
            // 
            this.tssTray1.Name = "tssTray1";
            this.tssTray1.Size = new System.Drawing.Size(206, 6);
            // 
            // screenshotsFolderToolStripMenuItem
            // 
            this.screenshotsFolderToolStripMenuItem.Image = global::ShareX.Properties.Resources.folder_open_image;
            this.screenshotsFolderToolStripMenuItem.Name = "screenshotsFolderToolStripMenuItem";
            this.screenshotsFolderToolStripMenuItem.Size = new System.Drawing.Size(209, 24);
            this.screenshotsFolderToolStripMenuItem.Text = "Screenshots folder...";
            this.screenshotsFolderToolStripMenuItem.Click += new System.EventHandler(this.screenshotsFolderToolStripMenuItem_Click);
            // 
            // tsmiHistory
            // 
            this.tsmiHistory.Image = global::ShareX.Properties.Resources.address_book_blue;
            this.tsmiHistory.Name = "tsmiHistory";
            this.tsmiHistory.Size = new System.Drawing.Size(209, 24);
            this.tsmiHistory.Text = "History...";
            this.tsmiHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // tsmiTraySettings
            // 
            this.tsmiTraySettings.Image = global::ShareX.Properties.Resources.application_form;
            this.tsmiTraySettings.Name = "tsmiTraySettings";
            this.tsmiTraySettings.Size = new System.Drawing.Size(209, 24);
            this.tsmiTraySettings.Text = "Settings...";
            this.tsmiTraySettings.Click += new System.EventHandler(this.tsmiTraySettings_Click);
            // 
            // tsmiTrayAbout
            // 
            this.tsmiTrayAbout.Image = global::ShareX.Properties.Resources.application_browser;
            this.tsmiTrayAbout.Name = "tsmiTrayAbout";
            this.tsmiTrayAbout.Size = new System.Drawing.Size(209, 24);
            this.tsmiTrayAbout.Text = "About...";
            this.tsmiTrayAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tsmiTrayDonate
            // 
            this.tsmiTrayDonate.Image = global::ShareX.Properties.Resources.present;
            this.tsmiTrayDonate.Name = "tsmiTrayDonate";
            this.tsmiTrayDonate.Size = new System.Drawing.Size(209, 24);
            this.tsmiTrayDonate.Text = "Donate...";
            this.tsmiTrayDonate.Visible = false;
            this.tsmiTrayDonate.Click += new System.EventHandler(this.tsbDonate_Click);
            // 
            // tssTray2
            // 
            this.tssTray2.Name = "tssTray2";
            this.tssTray2.Size = new System.Drawing.Size(206, 6);
            // 
            // tsmiTrayExit
            // 
            this.tsmiTrayExit.Image = global::ShareX.Properties.Resources.cross_button;
            this.tsmiTrayExit.Name = "tsmiTrayExit";
            this.tsmiTrayExit.Size = new System.Drawing.Size(209, 24);
            this.tsmiTrayExit.Text = "Exit";
            this.tsmiTrayExit.Click += new System.EventHandler(this.tsmiTrayExit_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 475);
            this.Controls.Add(this.tscMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1258, 506);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShareX";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.cmsUploads.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tscMain.ContentPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.PerformLayout();
            this.tscMain.ResumeLayout(false);
            this.tscMain.PerformLayout();
            this.cmsTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code

        private HelpersLib.MyListView lvUploads;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.ContextMenuStrip cmsUploads;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyThumbnailURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDeletionURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chProgress;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chUploaderType;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbClipboardUpload;
        private System.Windows.Forms.ToolStripButton tsbFileUpload;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripSeparator tssMain1;
        private System.Windows.Forms.ToolStripContainer tscMain;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chRemaining;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopUpload;
        private System.Windows.Forms.ColumnHeader chElapsed;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripMenuItem showErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showResponseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiImageUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTextUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiURLShorteners;
        private System.Windows.Forms.ToolStripDropDownButton tsddbDestinations;
        private System.Windows.Forms.ToolStripMenuItem copyShortenedURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tssDestinations1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUploadersConfig;
        private System.Windows.Forms.ToolStripButton tsbDonate;
        private System.Windows.Forms.ContextMenuStrip cmsTray;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayClipboardUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFileUpload;
        private System.Windows.Forms.ToolStripSeparator tssTray1;
        public System.Windows.Forms.NotifyIcon niTray;
        private System.Windows.Forms.ToolStripDropDownButton tsddbCapture;
        private System.Windows.Forms.ToolStripMenuItem tsmiFullscreen;
        private System.Windows.Forms.ToolStripMenuItem tsmiRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiRoundedRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiEllipse;
        private System.Windows.Forms.ToolStripMenuItem tsmiTriangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiDiamond;
        private System.Windows.Forms.ToolStripMenuItem tsmiPolygon;
        private System.Windows.Forms.ToolStripMenuItem tsmiFreeHand;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindowRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiTraySettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayDonate;
        private System.Windows.Forms.ToolStripSeparator tssTray2;
        private System.Windows.Forms.ToolStripMenuItem tsmiCapture;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFullscreen;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayWindowRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayRoundedRectangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayTriangle;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayDiamond;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayPolygon;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFreeHand;
        private System.Windows.Forms.ToolStripMenuItem tsmiConfiguration;
        private System.Windows.Forms.ToolStripDropDownButton tsddbSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiDebugOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayEllipse;
        private System.Windows.Forms.ToolStripDropDownButton tsddbOutputs;
        private System.Windows.Forms.ToolStripMenuItem showInWindowsExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmiWatermark;
        private System.Windows.Forms.ToolStripDropDownButton tsbDebug;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestImageUpload;    
        private System.Windows.Forms.ToolStripMenuItem tsmiTestTextUpload;	  
        private System.Windows.Forms.ToolStripMenuItem tsmiTestShapeCapture;
        private System.Windows.Forms.ToolStripMenuItem tsmiDestinations;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayImageUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayTextUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayFileUploaders;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayURLShorteners;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tsddbShare;
        private System.Windows.Forms.ToolStripMenuItem tsmiContextMenuShare;
        private System.Windows.Forms.ToolStripMenuItem viewInFullscreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton tsddbAfterCaptureTasks;
        private System.Windows.Forms.ToolStripDropDownButton tsddbAfterUploadTasks;
        private System.Windows.Forms.ToolStripMenuItem copyImageToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiLastRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayLastRegion;
        private System.Windows.Forms.ToolStripMenuItem tsmiWorkflows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem screencastToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiTrayScreencast;
        private System.Windows.Forms.ToolStripMenuItem screenshotsFolderToolStripMenuItem;
    }
}