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
using ScreenCapture;

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
            Program.Settings.PlaySoundAfterCapture = cbPlaySoundAfterCapture.Checked;
        }

        private void chkPlaySoundAfterUpload_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.PlaySoundAfterUpload = chkPlaySoundAfterUpload.Checked;
        }

        #endregion General / Notifications

        #region Helper Methods

        #region Configure Panels

        private void ConfigurePanels()
        {
            this.tlpMain.Dock = DockStyle.Fill;

            hmHotkeys.PrepareHotkeys(FormsHelper.Main.HotkeyManager);

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

            for (int i = 0; i < tvMain.Nodes.Count; i++)
            {
                tvMain.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvMain_NodeMouseClick);
            }
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

        private void tvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tvNode = e.Node;
            Panel myPanel = Panels.ContainsKey(tvNode.Tag.ToString()) ? Panels[tvNode.Tag.ToString()] : new Panel();
            if (tlpMain.Controls[1].Name != myPanel.Name)
            {
                tlpMain.Controls.RemoveAt(1);
                tlpMain.Controls.Add(myPanel, 1, 0);
            }
        }

        #endregion Configure Panels

        private void LoadSettings()
        {
            // General
            cbShowTray.Checked = Program.Settings.ShowTray;
            cbStartWithWindows.Checked = ShortcutHelper.CheckShortcut(Environment.SpecialFolder.Startup);
            cbShellContextMenu.Checked = ShortcutHelper.CheckShortcut(Environment.SpecialFolder.SendTo);
            cbCheckUpdates.Checked = Program.Settings.AutoCheckUpdate;
            cbClipboardAutoCopy.Checked = Program.Settings.ClipboardAutoCopy;
            cbURLShortenAfterUpload.Checked = Program.Settings.URLShortenAfterUpload;
            cbPlaySoundAfterCapture.Checked = Program.Settings.PlaySoundAfterCapture;
            chkPlaySoundAfterUpload.Checked = Program.Settings.PlaySoundAfterUpload;

            // Upload
            cbUseCustomUploadersConfigPath.Checked = Program.Settings.UseCustomUploadersConfigPath;
            txtCustomUploadersConfigPath.Text = Program.Settings.CustomUploadersConfigPath;
            nudUploadLimit.Value = Program.Settings.UploadLimit;

            for (int i = 0; i < MaxBufferSizePower; i++)
            {
                cbBufferSize.Items.Add(Math.Pow(2, i).ToString("N0"));
            }

            cbBufferSize.SelectedIndex = Program.Settings.BufferSizePower.Between(0, MaxBufferSizePower);

            // Capture
            cbShowCursor.Checked = Program.Settings.ShowCursor;
            cbCaptureTransparent.Checked = Program.Settings.CaptureTransparent;
            cbCaptureShadow.Enabled = Program.Settings.CaptureTransparent;
            cbCaptureShadow.Checked = Program.Settings.CaptureShadow;
            chkCaptureAnnotateImage.Checked = Program.Settings.CaptureAnnotateImage;
            cbCaptureCopyImage.Checked = Program.Settings.CaptureCopyImage;
            cbCaptureSaveImage.Checked = Program.Settings.CaptureSaveImage;
            txtScreenshotsPath.Text = Program.ScreenshotsRootPath;
            txtSaveImageSubFolderPattern.Text = Program.Settings.SaveImageSubFolderPattern;
            cbCaptureUploadImage.Checked = Program.Settings.UploadImageToHost;

            if (Program.Settings.SurfaceOptions == null) Program.Settings.SurfaceOptions = new SurfaceOptions();
            cbDrawBorder.Checked = Program.Settings.SurfaceOptions.DrawBorder;
            cbDrawCheckerboard.Checked = Program.Settings.SurfaceOptions.DrawChecker;
            cbQuickCrop.Checked = Program.Settings.SurfaceOptions.QuickCrop;
            cbFixedShapeSize.Checked = Program.Settings.SurfaceOptions.IsFixedSize;
            nudFixedShapeSizeWidth.Value = Program.Settings.SurfaceOptions.FixedSize.Width;
            nudFixedShapeSizeHeight.Value = Program.Settings.SurfaceOptions.FixedSize.Height;
            cbShapeIncludeControls.Checked = Program.Settings.SurfaceOptions.IncludeControls;
            cbShapeForceWindowCapture.Checked = Program.Settings.SurfaceOptions.ForceWindowCapture;

            // Clipboard upload
            cbClipboardUploadAutoDetectURL.Checked = Program.Settings.ClipboardUploadAutoDetectURL;
            txtNameFormatPattern.Text = Program.Settings.NameFormatPattern;
            CreateCodesMenu();

            // Image - Quality
            cbImageFormat.SelectedIndex = (int)Program.Settings.ImageFormat;
            nudImageJPEGQuality.Value = Program.Settings.ImageJPEGQuality;
            cbImageGIFQuality.SelectedIndex = (int)Program.Settings.ImageGIFQuality;
            nudUseImageFormat2After.Value = Program.Settings.ImageSizeLimit;
            cbImageFormat2.SelectedIndex = (int)Program.Settings.ImageFormat2;

            // Image - Resize
            cbImageAutoResize.Checked = Program.Settings.ImageAutoResize;
            cbImageKeepAspectRatio.Checked = Program.Settings.ImageKeepAspectRatio;
            cbImageUseSmoothScaling.Checked = Program.Settings.ImageUseSmoothScaling;

            switch (Program.Settings.ImageScaleType)
            {
                case ImageScaleType.Percentage:
                    rbImageScaleTypePercentage.Checked = true;
                    break;
                case ImageScaleType.Width:
                    rbImageScaleTypeToWidth.Checked = true;
                    break;
                case ImageScaleType.Height:
                    rbImageScaleTypeToHeight.Checked = true;
                    break;
                case ImageScaleType.Specific:
                    rbImageScaleTypeSpecific.Checked = true;
                    break;
            }

            nudImageScalePercentageWidth.Value = Program.Settings.ImageScalePercentageWidth;
            nudImageScalePercentageHeight.Value = Program.Settings.ImageScalePercentageHeight;
            nudImageScaleToWidth.Value = Program.Settings.ImageScaleToWidth;
            nudImageScaleToHeight.Value = Program.Settings.ImageScaleToHeight;
            nudImageScaleSpecificWidth.Value = Program.Settings.ImageScaleSpecificWidth;
            nudImageScaleSpecificHeight.Value = Program.Settings.ImageScaleSpecificHeight;

            // History
            cbHistorySave.Checked = Program.Settings.SaveHistory;
            cbUseCustomHistoryPath.Checked = Program.Settings.UseCustomHistoryPath;
            txtCustomHistoryPath.Text = Program.Settings.CustomHistoryPath;
            nudHistoryMaxItemCount.Value = Program.Settings.HistoryMaxItemCount;

            // Proxy
            pgProxy.SelectedObject = Program.Settings.ProxySettings;

            // Advanced
            pgSettings.SelectedObject = Program.Settings;
        }

        private void BeforeClose()
        {
            string dir = txtScreenshotsPath.Text;

            if (Directory.Exists(dir))
                Program.Settings.ScreenshotsPath = dir;

            #region Workflows

            List<Workflow> workflowsNew = new List<Workflow>();
            foreach (Workflow wf in FormsHelper.Main.HotkeyManager.Workflows)
            {
                Workflow wf2 = Program.Settings.Workflows1.FirstOrDefault(x => x.HotkeyConfig.Tag == wf.HotkeyConfig.Tag);
                if (wf2 == null)
                    workflowsNew.Add(wf);
            }

            foreach (Workflow wf in workflowsNew)
            {
                string tag = wf.HotkeyConfig.Tag;
                FormsHelper.Main.UnregisterHotkey(wf.HotkeyConfig.Hotkey);
                FormsHelper.Main.HotkeyManager.AddHotkey(wf, () => FormsHelper.Main.DoWork(tag));
            }

            List<Workflow> workflowOld = new List<Workflow>();
            foreach (Workflow wf in Program.Settings.Workflows1)
            {
                Workflow wf2 = FormsHelper.Main.HotkeyManager.Workflows.FirstOrDefault(x => x.HotkeyConfig.Tag == wf.HotkeyConfig.Tag);
                if (wf2 == null)
                    workflowOld.Add(wf);
            }

            foreach (Workflow wf in workflowOld)
            {
                FormsHelper.Main.UnregisterHotkey(wf.HotkeyConfig.Hotkey);
            }

            Program.Settings.Workflows1.Clear();
            Program.Settings.Workflows1.AddRange(FormsHelper.Main.HotkeyManager.Workflows);

            #endregion Workflows
        }

        private void CheckImageScaleType()
        {
            bool aspectRatioEnabled = true;

            if (rbImageScaleTypePercentage.Checked)
            {
                Program.Settings.ImageScaleType = ImageScaleType.Percentage;
            }
            else if (rbImageScaleTypeToWidth.Checked)
            {
                Program.Settings.ImageScaleType = ImageScaleType.Width;
            }
            else if (rbImageScaleTypeToHeight.Checked)
            {
                Program.Settings.ImageScaleType = ImageScaleType.Height;
            }
            else if (rbImageScaleTypeSpecific.Checked)
            {
                Program.Settings.ImageScaleType = ImageScaleType.Specific;
                aspectRatioEnabled = false;
            }

            cbImageKeepAspectRatio.Enabled = aspectRatioEnabled;
        }

        private void CreateCodesMenu()
        {
            codesMenu = new ContextMenuStrip
            {
                Font = new XFont("Lucida Console", 8),
                Opacity = 0.8,
                ShowImageMargin = false
            };

            var variables = Enum.GetValues(typeof(ReplacementVariables)).Cast<ReplacementVariables>().
                Select(x => new
                {
                    Name = ReplacementExtension.Prefix + Enum.GetName(typeof(ReplacementVariables), x),
                    Description = x.GetDescription(),
                    Enum = x
                });

            foreach (var variable in variables)
            {
                switch (variable.Enum)
                {
                    case ReplacementVariables.t:
                    case ReplacementVariables.i:
                    case ReplacementVariables.n:
                    case ReplacementVariables.link:
                    case ReplacementVariables.name:
                    case ReplacementVariables.size:
                        continue;
                }

                ToolStripMenuItem tsi = new ToolStripMenuItem { Text = string.Format("{0} - {1}", variable.Name, variable.Description), Tag = variable.Name };
                tsi.Click += (sender, e) => txtNameFormatPattern.AppendText(((ToolStripMenuItem)sender).Tag.ToString());
                codesMenu.Items.Add(tsi);
            }
        }

        private bool ChooseFolder(string title, TextBox tb)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = title;

                try
                {
                    string path = tb.Text;

                    if (!string.IsNullOrEmpty(path))
                    {
                        path = Path.GetDirectoryName(path);

                        if (Directory.Exists(path))
                        {
                            ofd.InitialDirectory = path;
                        }
                    }
                }
                finally
                {
                    if (string.IsNullOrEmpty(ofd.InitialDirectory))
                    {
                        ofd.InitialDirectory = Program.PersonalPath;
                    }
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tb.Text = ofd.FileName;
                    return true;
                }
            }

            return false;
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
            Program.Settings.ShowTray = cbShowTray.Checked;

            if (loaded)
            {
                FormsHelper.Main.niTray.Visible = Program.Settings.ShowTray;
            }
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutoCheckUpdate = cbCheckUpdates.Checked;
        }

        #endregion General

        #region Capture

        private void cbCaptureShadow_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.CaptureShadow = cbCaptureShadow.Checked;
        }

        private void cbCaptureTransparent_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.CaptureTransparent = cbCaptureTransparent.Checked;

            cbCaptureShadow.Enabled = Program.Settings.CaptureTransparent;
        }

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ShowCursor = cbShowCursor.Checked;
        }

        #region After Capture

        private void cbCaptureSaveImage_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.CaptureSaveImage = cbCaptureSaveImage.Checked;
        }

        private void chkCaptureAnnotateImage_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.CaptureAnnotateImage = chkCaptureAnnotateImage.Checked;
        }

        private void cbCaptureCopyImage_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.CaptureCopyImage = cbCaptureCopyImage.Checked;
        }

        #endregion After Capture

        #region Capture / Shapes

        private void cbDrawCheckerboard_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.DrawChecker = cbDrawCheckerboard.Checked;
        }

        private void cbDrawBorder_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.DrawBorder = cbDrawBorder.Checked;
        }

        private void nudFixedShapeSizeHeight_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.FixedSize = new Size(Program.Settings.SurfaceOptions.FixedSize.Width, (int)nudFixedShapeSizeHeight.Value);
        }

        private void cbQuickCrop_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.QuickCrop = cbQuickCrop.Checked;
        }

        private void cbFixedShapeSize_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.IsFixedSize = cbFixedShapeSize.Checked;
        }

        private void cbShapeForceWindowCapture_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.ForceWindowCapture = cbShapeForceWindowCapture.Checked;
        }

        private void cbShapeIncludeControls_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.IncludeControls = cbShapeIncludeControls.Checked;
        }

        private void nudFixedShapeSizeWidth_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.SurfaceOptions.FixedSize = new Size((int)nudFixedShapeSizeWidth.Value, Program.Settings.SurfaceOptions.FixedSize.Height);
        }

        #endregion Capture / Shapes

        #region Capture / Clipboard

        private void txtNameFormatPattern_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.NameFormatPattern = txtNameFormatPattern.Text;
            lblNameFormatPatternPreview.Text = "Preview: " + new NameParser().Convert(Program.Settings.NameFormatPattern);
        }

        private void cbClipboardUploadAutoDetectURL_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ClipboardUploadAutoDetectURL = cbClipboardUploadAutoDetectURL.Checked;
        }

        #endregion Capture / Clipboard

        #endregion Capture

        #region Image Processing

        private void btnNameFormatPatternHelp_Click(object sender, EventArgs e)
        {
            codesMenu.Show(btnNameFormatPatternHelp, new Point(btnNameFormatPatternHelp.Width + 1, 0));
        }

        private void rbImageScaleTypePercentage_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void nudImageScalePercentageWidth_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageScalePercentageWidth = (int)nudImageScalePercentageWidth.Value;

            if (Program.Settings.ImageKeepAspectRatio)
            {
                nudImageScalePercentageHeight.Value = Program.Settings.ImageScalePercentageWidth;
            }
        }

        private void nudImageScaleSpecificHeight_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageScaleSpecificHeight = (int)nudImageScaleSpecificHeight.Value;
        }

        private void rbImageScaleTypeToHeight_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void nudImageScaleSpecificWidth_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageScaleSpecificWidth = (int)nudImageScaleSpecificWidth.Value;
        }

        private void cbImageUseSmoothScaling_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageUseSmoothScaling = cbImageUseSmoothScaling.Checked;
        }

        private void rbImageScaleTypeToWidth_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void cbImageKeepAspectRatio_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageKeepAspectRatio = cbImageKeepAspectRatio.Checked;

            if (Program.Settings.ImageKeepAspectRatio)
            {
                nudImageScalePercentageHeight.Value = nudImageScalePercentageWidth.Value;
            }
        }

        private void rbImageScaleTypeSpecific_CheckedChanged(object sender, EventArgs e)
        {
            CheckImageScaleType();
        }

        private void nudImageScaleToHeight_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageScaleToHeight = (int)nudImageScaleToHeight.Value;
        }

        private void nudImageScalePercentageHeight_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageScalePercentageHeight = (int)nudImageScalePercentageHeight.Value;

            if (Program.Settings.ImageKeepAspectRatio)
            {
                nudImageScalePercentageWidth.Value = Program.Settings.ImageScalePercentageHeight;
            }
        }

        private void nudImageScaleToWidth_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageScaleToWidth = (int)nudImageScaleToWidth.Value;
        }

        private void cbImageAutoResize_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageAutoResize = cbImageAutoResize.Checked;
        }

        private void cbImageFormat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageFormat2 = (EImageFormat)cbImageFormat2.SelectedIndex;
        }

        private void nudImageJPEGQuality_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageJPEGQuality = (int)nudImageJPEGQuality.Value;
        }

        private void cbImageGIFQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageGIFQuality = (GIFQuality)cbImageGIFQuality.SelectedIndex;
        }

        private void cbImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageFormat = (EImageFormat)cbImageFormat.SelectedIndex;
        }

        private void nudUseImageFormat2After_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageSizeLimit = (int)nudUseImageFormat2After.Value;
        }

        #endregion Image Processing

        #region Upload

        private void cbCaptureUploadImage_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UploadImageToHost = cbCaptureUploadImage.Checked;
        }

        private void cbBufferSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.BufferSizePower = cbBufferSize.SelectedIndex;
            string bufferSize = (Math.Pow(2, Program.Settings.BufferSizePower) * 1024 / 1000).ToString("#,0.###");
            lblBufferSizeInfo.Text = string.Format("x {0} KiB = {1} KiB", 1.024, bufferSize);
        }

        private void nudUploadLimit_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.UploadLimit = (int)nudUploadLimit.Value;
        }

        private void txtCustomUploadersConfigPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomUploadersConfigPath = txtCustomUploadersConfigPath.Text;
        }

        private void cbUseCustomUploadersConfigPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomUploadersConfigPath = cbUseCustomUploadersConfigPath.Checked;
        }

        private void btnBrowseCustomUploadersConfigPath_Click(object sender, EventArgs e)
        {
            ChooseFolder("ShareX - Choose uploaders config file path", txtCustomUploadersConfigPath);
            Program.Settings.CustomUploadersConfigPath = txtCustomUploadersConfigPath.Text;
            Program.LoadUploadersConfig();
        }

        private void btnLoadUploadersConfig_Click(object sender, EventArgs e)
        {
            Program.LoadUploadersConfig();
        }

        private void cbURLShortenAfterUpload_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.URLShortenAfterUpload = cbURLShortenAfterUpload.Checked;
        }

        private void cbClipboardAutoCopy_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ClipboardAutoCopy = cbClipboardAutoCopy.Checked;
        }

        #endregion Upload

        #region Paths

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
            Program.Settings.SaveImageSubFolderPattern = txtSaveImageSubFolderPattern.Text;
            string subFolderName = new NameParser(NameParserType.SaveFolder).Convert(txtSaveImageSubFolderPattern.Text);
            txtSaveImageSubFolderPatternPreview.Text = subFolderName;
        }

        #region History

        private void btnBrowseCustomHistoryPath_Click(object sender, EventArgs e)
        {
            ChooseFolder("ShareX - Choose history file path", txtCustomHistoryPath);
        }

        private void nudHistoryMaxItemCount_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.HistoryMaxItemCount = (int)nudHistoryMaxItemCount.Value;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SaveHistory = cbHistorySave.Checked;
        }

        private void txtCustomHistoryPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomHistoryPath = txtCustomHistoryPath.Text;
        }

        private void cbUseCustomHistoryPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomHistoryPath = cbUseCustomHistoryPath.Checked;
        }

        #endregion History

        #endregion Paths

        #region Proxy

        private void btnAutofillProxy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Program.Settings.ProxySettings.UserName))
            {
                Program.Settings.ProxySettings.UserName = Environment.UserName;
            }

            WebProxy proxy = Helpers.GetDefaultWebProxy();
            if (proxy != null && proxy.Address != null)
            {
                if (string.IsNullOrEmpty(Program.Settings.ProxySettings.Host))
                {
                    Program.Settings.ProxySettings.Host = proxy.Address.Host;
                }
                if (Program.Settings.ProxySettings.Port == 0)
                {
                    Program.Settings.ProxySettings.Port = proxy.Address.Port;
                }
            }

            pgProxy.SelectedObject = Program.Settings.ProxySettings;
        }

        #endregion Proxy

        #region Form Events

        public OptionsWindow()
        {
            InitializeComponent();
            this.Text = Application.ProductName + " Settings - " + Program.Settings.FilePath;

            ConfigurePanels();
            LoadSettings();
            loaded = true;
        }

        private void OptionsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            BeforeClose();
            UploadManager.UpdateProxySettings();
            Program.Settings.SaveAsync();
            FormsHelper.Main.ReloadConfig();
        }

        private void OptionsWindow_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }

        private void OptionsWindow_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #endregion Form Events

        private void btnDropboxSyncImport_Click(object sender, EventArgs e)
        {
            new DropboxSyncHelper().Load();
        }

        private void btnDropboxSyncExport_Click(object sender, EventArgs e)
        {
            new DropboxSyncHelper().Save();
        }
    }
}