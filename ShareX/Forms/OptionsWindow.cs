using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkeys2;
using HelpersLibMod;
using ScreenCapture;
using ShareX.HelperClasses;

namespace ShareX.Forms
{
    public partial class OptionsWindow : Form
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool loaded;
        private const int MaxBufferSizePower = 12;
        private ContextMenuStrip codesMenu;
        private Dictionary<string, Panel> Panels = new Dictionary<string, Panel>();

        #region General / Notifications

        private void cbPlaySoundAfterCapture_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.PlaySoundAfterCapture = cbPlaySoundAfterCapture.Checked;
        }

        private void chkPlaySoundAfterUpload_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.PlaySoundAfterUpload = chkPlaySoundAfterUpload.Checked;
        }

        private void chkShowBalloon_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.ShowBalloonAfterUpload = chkShowBalloonAfterUpload.Checked;
        }

        #endregion General / Notifications

        #region Helper Methods

        #region Configure Panels

        private void ConfigurePanels()
        {
            this.tlpMain.Dock = DockStyle.Fill;

            // TreeView node.Tag property will have corresponding panel.Name
            FillTagsUsingName(tvMain.Nodes);

            // Load Panels to a dictionary
            foreach (TabPage tp in tcBase.TabPages)
            {
                foreach (Control ctl in tp.Controls)
                {
                    if (ctl.GetType() == typeof(Panel))
                        Panels.Add(ctl.Name, ctl as Panel);
                    break;
                }
            }

            tvMain.ExpandAll();
            tvMain.SelectedNode = tvMain.Nodes[0];

            // Make General tab visible and nothing else
            tlpMain.Controls.RemoveAt(1);
            tlpMain.Controls.Add(panelGeneral, 1, 0);
        }

        private void FillTagsUsingName(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                tn.Tag = tn.Name.Replace("tn", "panel");
                if (tn.Nodes.Count > 0)
                {
                    FillTagsUsingName(tn.Nodes);
                }
            }
        }

        private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowPanel(e.Node);
        }

        private void ShowPanel(TreeNode tn)
        {
            Panel myPanel = Panels.ContainsKey(tn.Tag.ToString()) ? Panels[tn.Tag.ToString()] : new Panel();
            if (tlpMain.Controls[1].Name != myPanel.Name)
            {
                tlpMain.Controls.RemoveAt(1);
                tlpMain.Controls.Add(myPanel, 1, 0);
            }
        }

        #endregion Configure Panels

        public void LoadSettings()
        {
            string path = string.IsNullOrEmpty(SettingsManager.ConfigCore.FilePath) ? "via Dropbox Sync" : SettingsManager.ConfigCore.FilePath;
            this.Text = Application.ProductName + " Settings - " + path;

            // General
            cbShowTray.Checked = SettingsManager.ConfigCore.ShowTray;
            cbStartWithWindows.Checked = ShortcutHelper.CheckShortcut(Environment.SpecialFolder.Startup);
            cbShellContextMenu.Checked = ShortcutHelper.CheckShortcut(Environment.SpecialFolder.SendTo);
            cbCheckUpdates.Checked = SettingsManager.ConfigCore.AutoCheckUpdate;
            cbPlaySoundAfterCapture.Checked = SettingsManager.ConfigCore.PlaySoundAfterCapture;
            chkPlaySoundAfterUpload.Checked = SettingsManager.ConfigCore.PlaySoundAfterUpload;
            chkShowBalloonAfterUpload.Checked = SettingsManager.ConfigCore.ShowBalloonAfterUpload;

            // Upload
            cbUseCustomUploadersConfigPath.Checked = SettingsManager.ConfigCore.UseCustomUploadersConfigPath;
            txtCustomUploadersConfigPath.Text = SettingsManager.ConfigCore.CustomUploadersConfigPath;
            nudUploadLimit.Value = SettingsManager.ConfigCore.UploadLimit;

            for (int i = 0; i < MaxBufferSizePower; i++)
            {
                cbBufferSize.Items.Add(Math.Pow(2, i).ToString("N0"));
            }

            cbBufferSize.SelectedIndex = SettingsManager.ConfigCore.BufferSizePower.Between(0, MaxBufferSizePower);

            // Capture
            ucAfterCaptureTasks.ConfigUI(SettingsManager.ConfigCore.AfterCaptureTasks, chkAfterCaptureTask_CheckedChanged);
            ucAfterUploadTasks.ConfigUI(SettingsManager.ConfigCore.AfterUploadTasks, chkAfterUploadTask_CheckedChanged);

            cbShowCursor.Checked = SettingsManager.ConfigCore.ShowCursor;
            cbCaptureTransparent.Checked = SettingsManager.ConfigCore.CaptureTransparent;
            cbCaptureShadow.Enabled = SettingsManager.ConfigCore.CaptureTransparent;
            cbCaptureShadow.Checked = SettingsManager.ConfigCore.CaptureShadow;

            txtScreenshotsPath.Text = Program.ScreenshotsRootPath;
            txtSaveImageSubFolderPattern.Text = SettingsManager.ConfigCore.SaveImageSubFolderPattern;

            if (SettingsManager.ConfigCore.SurfaceOptions == null) SettingsManager.ConfigCore.SurfaceOptions = new SurfaceOptions();
            cbDrawBorder.Checked = SettingsManager.ConfigCore.SurfaceOptions.DrawBorder;
            cbDrawCheckerboard.Checked = SettingsManager.ConfigCore.SurfaceOptions.DrawChecker;
            cbQuickCrop.Checked = SettingsManager.ConfigCore.SurfaceOptions.QuickCrop;
            cbFixedShapeSize.Checked = SettingsManager.ConfigCore.SurfaceOptions.IsFixedSize;
            nudFixedShapeSizeWidth.Value = SettingsManager.ConfigCore.SurfaceOptions.FixedSize.Width;
            nudFixedShapeSizeHeight.Value = SettingsManager.ConfigCore.SurfaceOptions.FixedSize.Height;
            cbShapeIncludeControls.Checked = SettingsManager.ConfigCore.SurfaceOptions.IncludeControls;
            cbShapeForceWindowCapture.Checked = SettingsManager.ConfigCore.SurfaceOptions.ForceWindowCapture;

            // Clipboard upload
            cbClipboardUploadAutoDetectURL.Checked = SettingsManager.ConfigCore.ClipboardUploadAutoDetectURL;
            txtNameFormatPatternImages.Text = SettingsManager.ConfigCore.NameFormatPattern;
            txtNameFormatPatternOther.Text = SettingsManager.ConfigCore.NameFormatPatternOther;

            // Image - Quality
            ucImageQualityUI.ConfigUI(SettingsManager.ConfigUser);

            // Image - Resize
            ucImageResizeUI.ConfigUI(SettingsManager.ConfigUser);

            // History
            cbHistorySave.Checked = SettingsManager.ConfigCore.SaveHistory;
            cbUseCustomHistoryPath.Checked = SettingsManager.ConfigCore.UseCustomHistoryPath;
            txtCustomHistoryPath.Text = SettingsManager.ConfigCore.CustomHistoryPath;
            nudHistoryMaxItemCount.Value = SettingsManager.ConfigCore.HistoryMaxItemCount;

            // Proxy
            pgProxy.SelectedObject = SettingsManager.ConfigCore.ProxySettings;

            // Advanced
            pgSettings.SelectedObject = SettingsManager.ConfigCore;
            pgUploaderConfig.SelectedObject = SettingsManager.ConfigUploaders;
            pgShapes.SelectedObject = SettingsManager.ConfigCore.SurfaceOptions;
            pgUserConfig.SelectedObject = SettingsManager.ConfigUser;

            loaded = true;
        }

        private void chkAfterCaptureTask_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAfterCaptureTask = sender as CheckBox;
            if (chkAfterCaptureTask.Checked)
                SettingsManager.ConfigCore.AfterCaptureTasks |= (Subtask)chkAfterCaptureTask.Tag;
            else
                SettingsManager.ConfigCore.AfterCaptureTasks &= ~(Subtask)chkAfterCaptureTask.Tag;
        }

        private void chkAfterUploadTask_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAfterUploadTask = sender as CheckBox;
            AfterUploadTasks task = (AfterUploadTasks)chkAfterUploadTask.Tag;

            if (chkAfterUploadTask.Checked)
                SettingsManager.ConfigCore.AfterUploadTasks |= (AfterUploadTasks)chkAfterUploadTask.Tag;
            else
                SettingsManager.ConfigCore.AfterUploadTasks &= ~(AfterUploadTasks)chkAfterUploadTask.Tag;
        }

        private void BeforeClose()
        {
            string dir = txtScreenshotsPath.Text;

            if (Directory.Exists(dir))
                SettingsManager.ConfigCore.ScreenshotsPath = dir;

            FormsHelper.Main.ReloadConfig();
            FormsHelper.Main.ReloadOutputsMenu();

            ListViewManager.RefreshThumbnails();
            DropboxSyncHelper.SaveAsync();
        }

        /// <summary>
        /// Creates a menu with replacement variables
        /// </summary>
        /// <param name="textBox">TextBox where the replacement variables should be appended to</param>
        /// <param name="ignoreList">List of replacement variables to be ignored</param>
        private void CreateCodesMenu(TextBox textBox, List<ReplacementVariables> ignoreList = null)
        {
            codesMenu = new ContextMenuStrip
            {
                Font = new XFont("Lucida Console", 8),
                Opacity = 0.8,
                ShowImageMargin = false
            };

            if (ignoreList == null)
                ignoreList = new List<ReplacementVariables>();

            var variables = Enum.GetValues(typeof(ReplacementVariables)).Cast<ReplacementVariables>().
                Where(x => !ignoreList.Contains(x)).
                Select(x => new
                {
                    Name = ReplacementExtension.Prefix + Enum.GetName(typeof(ReplacementVariables), x),
                    Description = x.GetDescription(),
                    Enum = x,
                });

            foreach (var variable in variables)
            {
                switch (variable.Enum)
                {
                    case ReplacementVariables.i:
                    case ReplacementVariables.n:
                        continue;
                }

                ToolStripMenuItem tsi = new ToolStripMenuItem { Text = string.Format("{0} - {1}", variable.Name, variable.Description), Tag = variable.Name };
                tsi.Click += (sender, e) => textBox.AppendText(((ToolStripMenuItem)sender).Tag.ToString());
                codesMenu.Items.Add(tsi);
            }
        }

        #endregion Helper Methods

        #region General

        private void cbStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ShortcutHelper.SetShortcut(cbStartWithWindows.Checked, Environment.SpecialFolder.Startup, "-silent");
            }
        }

        private void cbShellContextMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ShortcutHelper.SetShortcut(cbShellContextMenu.Checked, Environment.SpecialFolder.SendTo);
            }
        }

        private void cbShowTray_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.ShowTray = cbShowTray.Checked;

            if (loaded)
            {
                FormsHelper.Main.niTray.Visible = SettingsManager.ConfigCore.ShowTray;
            }
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.AutoCheckUpdate = cbCheckUpdates.Checked;
        }

        #endregion General

        #region Capture

        private void cbCaptureShadow_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.CaptureShadow = cbCaptureShadow.Checked;
        }

        private void cbCaptureTransparent_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.CaptureTransparent = cbCaptureTransparent.Checked;

            cbCaptureShadow.Enabled = SettingsManager.ConfigCore.CaptureTransparent;
        }

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.ShowCursor = cbShowCursor.Checked;
        }

        #region Capture / Shapes

        private void cbDrawCheckerboard_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.DrawChecker = cbDrawCheckerboard.Checked;
        }

        private void cbDrawBorder_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.DrawBorder = cbDrawBorder.Checked;
        }

        private void nudFixedShapeSizeHeight_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.FixedSize = new Size(SettingsManager.ConfigCore.SurfaceOptions.FixedSize.Width, (int)nudFixedShapeSizeHeight.Value);
        }

        private void cbQuickCrop_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.QuickCrop = cbQuickCrop.Checked;
        }

        private void cbFixedShapeSize_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.IsFixedSize = cbFixedShapeSize.Checked;
        }

        private void cbShapeForceWindowCapture_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.ForceWindowCapture = cbShapeForceWindowCapture.Checked;
        }

        private void cbShapeIncludeControls_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.IncludeControls = cbShapeIncludeControls.Checked;
        }

        private void nudFixedShapeSizeWidth_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SurfaceOptions.FixedSize = new Size((int)nudFixedShapeSizeWidth.Value, SettingsManager.ConfigCore.SurfaceOptions.FixedSize.Height);
        }

        #endregion Capture / Shapes

        #region File Naming

        private void btnNameFormatPatternHelp_Click(object sender, EventArgs e)
        {
            CreateCodesMenu(txtNameFormatPatternImages);
            codesMenu.Show(btnNameFormatPatternHelpImages, new Point(btnNameFormatPatternHelpImages.Width + 1, 0));
        }

        private void btnNameFormatPatternHelpOther_Click(object sender, EventArgs e)
        {
            CreateCodesMenu(txtNameFormatPatternOther, new List<ReplacementVariables>() { ReplacementVariables.t });
            codesMenu.Show(btnNameFormatPatternHelpImages, new Point(btnNameFormatPatternHelpOther.Width + 1, gbFilenamingPatternOthers.Location.Y - 8));
        }

        private void txtNameFormatPattern_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.NameFormatPattern = txtNameFormatPatternImages.Text;
            lblNameFormatPatternPreviewImages.Text = new NameParser(NameParserType.FileName) { WindowText = NativeMethods.GetForegroundWindowText() }.Convert(SettingsManager.ConfigCore.NameFormatPattern);
        }

        private void cbClipboardUploadAutoDetectURL_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.ClipboardUploadAutoDetectURL = cbClipboardUploadAutoDetectURL.Checked;
        }

        private void txtNameFormatPatternOther_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.NameFormatPatternOther = txtNameFormatPatternOther.Text;
            lblNameFormatPatternPreviewOther.Text = new NameParser(NameParserType.FileName).Convert(SettingsManager.ConfigCore.NameFormatPatternOther);
        }

        #endregion File Naming

        #endregion Capture

        #region Upload

        private void cbBufferSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.BufferSizePower = cbBufferSize.SelectedIndex;
            string bufferSize = (Math.Pow(2, SettingsManager.ConfigCore.BufferSizePower) * 1024 / 1000).ToString("#,0.###");
            lblBufferSizeInfo.Text = string.Format("x {0} KiB = {1} KiB", 1.024, bufferSize);
        }

        private void nudUploadLimit_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.UploadLimit = (int)nudUploadLimit.Value;
        }

        private void txtCustomUploadersConfigPath_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.CustomUploadersConfigPath = txtCustomUploadersConfigPath.Text;
        }

        private void cbUseCustomUploadersConfigPath_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.UseCustomUploadersConfigPath = cbUseCustomUploadersConfigPath.Checked;
        }

        private void btnBrowseCustomUploadersConfigPath_Click(object sender, EventArgs e)
        {
            Helpers.BrowseFile("ShareX - Choose uploaders config file path", txtCustomUploadersConfigPath);
            SettingsManager.ConfigCore.CustomUploadersConfigPath = txtCustomUploadersConfigPath.Text;
            SettingsManager.LoadUploadersConfig();
        }

        private void btnLoadUploadersConfig_Click(object sender, EventArgs e)
        {
            SettingsManager.LoadUploadersConfig();
        }

        #endregion Upload

        #region Paths

        private void txtScreenshotsPath_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtScreenshotsPath.Text))
                txtScreenshotsPath.Text = Program.ScreenshotsRootPath;
        }

        private void btnImagesOrganise_Click(object sender, EventArgs e)
        {
            ManageImageFolders(txtScreenshotsPath.Text);
        }

        public static bool ManageImageFolders(string rootDir)
        {
            if (!string.IsNullOrEmpty(rootDir) && Directory.Exists(rootDir))
            {
                string[] images = Directory.GetFiles(rootDir);

                List<string> imagesList = new List<string>();

                List<string> listExt = new List<string>();
                foreach (ImageFileExtensions ext in Enum.GetValues(typeof(ImageFileExtensions)))
                {
                    listExt.Add(ext.ToString());
                }
                foreach (VideoFileExtensions ext in Enum.GetValues(typeof(VideoFileExtensions)))
                {
                    listExt.Add(ext.ToString());
                }

                foreach (string image in images)
                {
                    foreach (string s in listExt)
                    {
                        if (Path.HasExtension(image) && Path.GetExtension(image.ToLower()) == "." + s)
                        {
                            imagesList.Add(image);
                            break;
                        }
                    }
                }

                DebugHelper.WriteLine(string.Format("Found {0} images to move to sub-folders", imagesList.Count));

                if (imagesList.Count > 0)
                {
                    if (MessageBox.Show(string.Format("{0} files found in {1}\n\nPlease wait until all the files are moved.",
                        imagesList.Count, rootDir), Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        return false;
                    }

                    DateTime time;
                    string movePath;

                    foreach (string image in imagesList)
                    {
                        if (File.Exists(image))
                        {
                            time = File.GetLastWriteTime(image);
                            string subDirName = new NameParser(NameParserType.FolderPath).Convert(SettingsManager.ConfigCore.SaveImageSubFolderPattern);
                            string subDirPath = Path.Combine(rootDir, subDirName);

                            if (!Directory.Exists(subDirPath))
                                Directory.CreateDirectory(subDirPath);

                            movePath = Helpers.GetUniqueFilePath(subDirPath, Path.GetFileName(image));
                            File.Move(image, movePath);
                        }
                    }
                }

                return true;
            }

            return false;
        }

        private void btnBrowseScreenshotsDir_Click(object sender, EventArgs e)
        {
            string dir = Path.Combine(txtScreenshotsPath.Text, txtSaveImageSubFolderPatternPreview.Text);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            Process.Start(dir);
        }

        private void btnOpenPersonalPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Program.PersonalPath) && Directory.Exists(Program.PersonalPath))
            {
                Process.Start(Program.PersonalPath);
            }
        }

        private void txtSaveImageSubFolderPattern_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SaveImageSubFolderPattern = txtSaveImageSubFolderPattern.Text;
            string subFolderName = new NameParser(NameParserType.FolderPath).Convert(txtSaveImageSubFolderPattern.Text);
            txtSaveImageSubFolderPatternPreview.Text = subFolderName;
        }

        #region History

        private void btnBrowseCustomHistoryPath_Click(object sender, EventArgs e)
        {
            Helpers.BrowseFolder("ShareX - Choose history file path", txtCustomHistoryPath);
        }

        private void nudHistoryMaxItemCount_ValueChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.HistoryMaxItemCount = (int)nudHistoryMaxItemCount.Value;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.SaveHistory = cbHistorySave.Checked;
        }

        private void txtCustomHistoryPath_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.CustomHistoryPath = txtCustomHistoryPath.Text;
        }

        private void cbUseCustomHistoryPath_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ConfigCore.UseCustomHistoryPath = cbUseCustomHistoryPath.Checked;
        }

        #endregion History

        #endregion Paths

        #region Proxy

        private void btnAutofillProxy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SettingsManager.ConfigCore.ProxySettings.UserName))
            {
                SettingsManager.ConfigCore.ProxySettings.UserName = Environment.UserName;
            }

            WebProxy proxy = Helpers.GetDefaultWebProxy();
            if (proxy != null && proxy.Address != null)
            {
                if (string.IsNullOrEmpty(SettingsManager.ConfigCore.ProxySettings.Host))
                {
                    SettingsManager.ConfigCore.ProxySettings.Host = proxy.Address.Host;
                }
                if (SettingsManager.ConfigCore.ProxySettings.Port == 0)
                {
                    SettingsManager.ConfigCore.ProxySettings.Port = proxy.Address.Port;
                }
            }

            pgProxy.SelectedObject = SettingsManager.ConfigCore.ProxySettings;
        }

        #endregion Proxy

        #region Form Events

        public OptionsWindow()
        {
            InitializeComponent();
            ConfigurePanels();
        }

        private void OptionsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            BeforeClose();

            UploadManager.UpdateProxySettings();

            SettingsManager.SaveAsync();
        }

        private void OptionsWindow_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
            LoadSettings();
        }

        private void OptionsWindow_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #endregion Form Events

        private void btnDropboxSyncImport_Click(object sender, EventArgs e)
        {
            new DropboxSyncHelper().Sync();
        }

        private void btnDropboxSyncExport_Click(object sender, EventArgs e)
        {
            new DropboxSyncHelper().Save();
        }
    }
}