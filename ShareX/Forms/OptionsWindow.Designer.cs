using HelpersLib.UserControls;
namespace ShareX.Forms
{
    partial class OptionsWindow
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Clipboard");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Shapes");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Capture", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("File Naming");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Quality");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Resize");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Image Processing", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Upload");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Paths");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Proxy");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Shapes");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("User config");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Sync");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Advanced", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14});
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.tcBase = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.gbNotifications = new System.Windows.Forms.GroupBox();
            this.chkShowBalloonAfterUpload = new System.Windows.Forms.CheckBox();
            this.chkPlaySoundAfterUpload = new System.Windows.Forms.CheckBox();
            this.cbPlaySoundAfterCapture = new System.Windows.Forms.CheckBox();
            this.lblGeneralInfo = new System.Windows.Forms.Label();
            this.cbCheckUpdates = new System.Windows.Forms.CheckBox();
            this.cbStartWithWindows = new System.Windows.Forms.CheckBox();
            this.cbShowTray = new System.Windows.Forms.CheckBox();
            this.cbShellContextMenu = new System.Windows.Forms.CheckBox();
            this.tpAdvanced = new System.Windows.Forms.TabPage();
            this.panelAdvanced = new System.Windows.Forms.Panel();
            this.pgSettings = new System.Windows.Forms.PropertyGrid();
            this.tpCapture = new System.Windows.Forms.TabPage();
            this.panelCapture = new System.Windows.Forms.Panel();
            this.gbCaptureAfter = new System.Windows.Forms.GroupBox();
            this.ucAfterCaptureTasks = new HelpersLib.UserControls.AfterCaptureTasksUI();
            this.cbCaptureShadow = new System.Windows.Forms.CheckBox();
            this.cbShowCursor = new System.Windows.Forms.CheckBox();
            this.cbCaptureTransparent = new System.Windows.Forms.CheckBox();
            this.tpShapes = new System.Windows.Forms.TabPage();
            this.panelShapes = new System.Windows.Forms.Panel();
            this.cbShapeForceWindowCapture = new System.Windows.Forms.CheckBox();
            this.cbShapeIncludeControls = new System.Windows.Forms.CheckBox();
            this.lblFixedShapeSizeHeight = new System.Windows.Forms.Label();
            this.cbDrawBorder = new System.Windows.Forms.CheckBox();
            this.lblFixedShapeSizeWidth = new System.Windows.Forms.Label();
            this.cbQuickCrop = new System.Windows.Forms.CheckBox();
            this.nudFixedShapeSizeHeight = new System.Windows.Forms.NumericUpDown();
            this.cbDrawCheckerboard = new System.Windows.Forms.CheckBox();
            this.nudFixedShapeSizeWidth = new System.Windows.Forms.NumericUpDown();
            this.cbFixedShapeSize = new System.Windows.Forms.CheckBox();
            this.tpProxy = new System.Windows.Forms.TabPage();
            this.panelProxy = new System.Windows.Forms.Panel();
            this.btnAutofillProxy = new System.Windows.Forms.Button();
            this.pgProxy = new System.Windows.Forms.PropertyGrid();
            this.tpClipboardUpload = new System.Windows.Forms.TabPage();
            this.panelClipboardUpload = new System.Windows.Forms.Panel();
            this.cbClipboardUploadAutoDetectURL = new System.Windows.Forms.CheckBox();
            this.lblClipboardUploadInfo = new System.Windows.Forms.Label();
            this.tpUpload = new System.Windows.Forms.TabPage();
            this.panelUpload = new System.Windows.Forms.Panel();
            this.gbAfterUpload = new System.Windows.Forms.GroupBox();
            this.ucAfterUploadTasks = new HelpersLib.UserControls.AfterUploadTasksUI();
            this.lblUploadLimitHint = new System.Windows.Forms.Label();
            this.nudUploadLimit = new System.Windows.Forms.NumericUpDown();
            this.lblUploadLimit = new System.Windows.Forms.Label();
            this.lblBufferSize = new System.Windows.Forms.Label();
            this.lblBufferSizeInfo = new System.Windows.Forms.Label();
            this.cbBufferSize = new System.Windows.Forms.ComboBox();
            this.tpImageResize = new System.Windows.Forms.TabPage();
            this.panelImageResize = new System.Windows.Forms.Panel();
            this.ucImageResizeUI = new ShareX.ImageResizeUI();
            this.tpImageQuality2 = new System.Windows.Forms.TabPage();
            this.panelImageQuality = new System.Windows.Forms.Panel();
            this.ucImageQualityUI = new ShareX.ImageQualityUI();
            this.tpPaths = new System.Windows.Forms.TabPage();
            this.panelPaths = new System.Windows.Forms.Panel();
            this.gbPathRoot = new System.Windows.Forms.GroupBox();
            this.lblOpenZUploaderPath = new System.Windows.Forms.Label();
            this.btnOpenZUploaderPath = new System.Windows.Forms.Button();
            this.gbScreenshots = new System.Windows.Forms.GroupBox();
            this.btnImagesOrganise = new System.Windows.Forms.Button();
            this.txtSaveImageSubFolderPatternPreview = new System.Windows.Forms.TextBox();
            this.lblSaveImageSubFolderPattern = new System.Windows.Forms.Label();
            this.txtSaveImageSubFolderPattern = new System.Windows.Forms.TextBox();
            this.btnBrowseScreenshotsDir = new System.Windows.Forms.Button();
            this.txtScreenshotsPath = new System.Windows.Forms.TextBox();
            this.gbUploadersConfig = new System.Windows.Forms.GroupBox();
            this.btnLoadUploadersConfig = new System.Windows.Forms.Button();
            this.btnBrowseCustomUploadersConfigPath = new System.Windows.Forms.Button();
            this.txtCustomUploadersConfigPath = new System.Windows.Forms.TextBox();
            this.cbUseCustomUploadersConfigPath = new System.Windows.Forms.CheckBox();
            this.gbHistory = new System.Windows.Forms.GroupBox();
            this.nudHistoryMaxItemCount = new System.Windows.Forms.NumericUpDown();
            this.lblHistoryMaxItemCount = new System.Windows.Forms.Label();
            this.btnBrowseCustomHistoryPath = new System.Windows.Forms.Button();
            this.txtCustomHistoryPath = new System.Windows.Forms.TextBox();
            this.cbUseCustomHistoryPath = new System.Windows.Forms.CheckBox();
            this.cbHistorySave = new System.Windows.Forms.CheckBox();
            this.tpSync = new System.Windows.Forms.TabPage();
            this.panelSync = new System.Windows.Forms.Panel();
            this.btnDropboxSyncExport = new System.Windows.Forms.Button();
            this.btnDropboxSyncImport = new System.Windows.Forms.Button();
            this.tpFileNaming = new System.Windows.Forms.TabPage();
            this.panelFileNaming = new System.Windows.Forms.Panel();
            this.gbFilenamingPatternOthers = new System.Windows.Forms.GroupBox();
            this.txtNameFormatPatternOther = new System.Windows.Forms.TextBox();
            this.lblNameFormatPatternPreviewOther = new System.Windows.Forms.Label();
            this.btnNameFormatPatternHelpOther = new System.Windows.Forms.Button();
            this.gbFilenamingPatternImages = new System.Windows.Forms.GroupBox();
            this.lblNameFormatPatternPreviewImages = new System.Windows.Forms.Label();
            this.txtNameFormatPatternImages = new System.Windows.Forms.TextBox();
            this.btnNameFormatPatternHelpImages = new System.Windows.Forms.Button();
            this.tpShapes2 = new System.Windows.Forms.TabPage();
            this.panelShapes2 = new System.Windows.Forms.Panel();
            this.pgShapes = new System.Windows.Forms.PropertyGrid();
            this.tpUserConfig = new System.Windows.Forms.TabPage();
            this.panelUserConfig = new System.Windows.Forms.Panel();
            this.pgUserConfig = new System.Windows.Forms.PropertyGrid();
            this.tpImageProcessing = new System.Windows.Forms.TabPage();
            this.panelImageProcessing = new System.Windows.Forms.Panel();
            this.chkFileUploadImageProcess = new System.Windows.Forms.CheckBox();
            this.tlpMain.SuspendLayout();
            this.tcBase.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.panelGeneral.SuspendLayout();
            this.gbNotifications.SuspendLayout();
            this.tpAdvanced.SuspendLayout();
            this.panelAdvanced.SuspendLayout();
            this.tpCapture.SuspendLayout();
            this.panelCapture.SuspendLayout();
            this.gbCaptureAfter.SuspendLayout();
            this.tpShapes.SuspendLayout();
            this.panelShapes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeWidth)).BeginInit();
            this.tpProxy.SuspendLayout();
            this.panelProxy.SuspendLayout();
            this.tpClipboardUpload.SuspendLayout();
            this.panelClipboardUpload.SuspendLayout();
            this.tpUpload.SuspendLayout();
            this.panelUpload.SuspendLayout();
            this.gbAfterUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).BeginInit();
            this.tpImageResize.SuspendLayout();
            this.panelImageResize.SuspendLayout();
            this.tpImageQuality2.SuspendLayout();
            this.panelImageQuality.SuspendLayout();
            this.tpPaths.SuspendLayout();
            this.panelPaths.SuspendLayout();
            this.gbPathRoot.SuspendLayout();
            this.gbScreenshots.SuspendLayout();
            this.gbUploadersConfig.SuspendLayout();
            this.gbHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItemCount)).BeginInit();
            this.tpSync.SuspendLayout();
            this.panelSync.SuspendLayout();
            this.tpFileNaming.SuspendLayout();
            this.panelFileNaming.SuspendLayout();
            this.gbFilenamingPatternOthers.SuspendLayout();
            this.gbFilenamingPatternImages.SuspendLayout();
            this.tpShapes2.SuspendLayout();
            this.panelShapes2.SuspendLayout();
            this.tpUserConfig.SuspendLayout();
            this.panelUserConfig.SuspendLayout();
            this.tpImageProcessing.SuspendLayout();
            this.panelImageProcessing.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpMain.Controls.Add(this.tvMain, 0, 0);
            this.tlpMain.Controls.Add(this.tcBase, 1, 0);
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(4);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(928, 630);
            this.tlpMain.TabIndex = 0;
            // 
            // tvMain
            // 
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.Location = new System.Drawing.Point(4, 4);
            this.tvMain.Margin = new System.Windows.Forms.Padding(4);
            this.tvMain.Name = "tvMain";
            treeNode1.Name = "tnGeneral";
            treeNode1.Tag = "panelGeneral";
            treeNode1.Text = "General";
            treeNode2.Name = "tnClipboardUpload";
            treeNode2.Text = "Clipboard";
            treeNode3.Name = "tnShapes";
            treeNode3.Tag = "panelShapes";
            treeNode3.Text = "Shapes";
            treeNode4.Name = "tnCapture";
            treeNode4.Tag = "panelCapture";
            treeNode4.Text = "Capture";
            treeNode5.Name = "tnFileNaming";
            treeNode5.Text = "File Naming";
            treeNode6.Name = "tnImageQuality";
            treeNode6.Text = "Quality";
            treeNode7.Name = "tnImageResize";
            treeNode7.Text = "Resize";
            treeNode8.Name = "tnImageProcessing";
            treeNode8.Text = "Image Processing";
            treeNode9.Name = "tnUpload";
            treeNode9.Text = "Upload";
            treeNode10.Name = "tnPaths";
            treeNode10.Text = "Paths";
            treeNode11.Name = "tnProxy";
            treeNode11.Text = "Proxy";
            treeNode12.Name = "tnShapes2";
            treeNode12.Text = "Shapes";
            treeNode13.Name = "tnUserConfig";
            treeNode13.Text = "User config";
            treeNode14.Name = "tnSync";
            treeNode14.Text = "Sync";
            treeNode15.Name = "tnAdvanced";
            treeNode15.Text = "Advanced";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode5,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode15});
            this.tvMain.Size = new System.Drawing.Size(177, 622);
            this.tvMain.TabIndex = 0;
            this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
            // 
            // tcBase
            // 
            this.tcBase.Controls.Add(this.tpGeneral);
            this.tcBase.Controls.Add(this.tpAdvanced);
            this.tcBase.Controls.Add(this.tpCapture);
            this.tcBase.Controls.Add(this.tpShapes);
            this.tcBase.Controls.Add(this.tpProxy);
            this.tcBase.Controls.Add(this.tpClipboardUpload);
            this.tcBase.Controls.Add(this.tpUpload);
            this.tcBase.Controls.Add(this.tpImageResize);
            this.tcBase.Controls.Add(this.tpImageQuality2);
            this.tcBase.Controls.Add(this.tpPaths);
            this.tcBase.Controls.Add(this.tpSync);
            this.tcBase.Controls.Add(this.tpFileNaming);
            this.tcBase.Controls.Add(this.tpShapes2);
            this.tcBase.Controls.Add(this.tpUserConfig);
            this.tcBase.Controls.Add(this.tpImageProcessing);
            this.tcBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBase.Location = new System.Drawing.Point(189, 4);
            this.tcBase.Margin = new System.Windows.Forms.Padding(4);
            this.tcBase.Name = "tcBase";
            this.tcBase.SelectedIndex = 0;
            this.tcBase.Size = new System.Drawing.Size(735, 622);
            this.tcBase.TabIndex = 1;
            this.tcBase.Visible = false;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.panelGeneral);
            this.tpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tpGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(727, 593);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // panelGeneral
            // 
            this.panelGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.panelGeneral.Controls.Add(this.gbNotifications);
            this.panelGeneral.Controls.Add(this.lblGeneralInfo);
            this.panelGeneral.Controls.Add(this.cbCheckUpdates);
            this.panelGeneral.Controls.Add(this.cbStartWithWindows);
            this.panelGeneral.Controls.Add(this.cbShowTray);
            this.panelGeneral.Controls.Add(this.cbShellContextMenu);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGeneral.Location = new System.Drawing.Point(0, 0);
            this.panelGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(727, 593);
            this.panelGeneral.TabIndex = 0;
            // 
            // gbNotifications
            // 
            this.gbNotifications.Controls.Add(this.chkShowBalloonAfterUpload);
            this.gbNotifications.Controls.Add(this.chkPlaySoundAfterUpload);
            this.gbNotifications.Controls.Add(this.cbPlaySoundAfterCapture);
            this.gbNotifications.Location = new System.Drawing.Point(21, 167);
            this.gbNotifications.Margin = new System.Windows.Forms.Padding(4);
            this.gbNotifications.Name = "gbNotifications";
            this.gbNotifications.Padding = new System.Windows.Forms.Padding(4);
            this.gbNotifications.Size = new System.Drawing.Size(683, 129);
            this.gbNotifications.TabIndex = 4;
            this.gbNotifications.TabStop = false;
            this.gbNotifications.Text = "Notifications";
            // 
            // chkShowBalloonAfterUpload
            // 
            this.chkShowBalloonAfterUpload.AutoSize = true;
            this.chkShowBalloonAfterUpload.Location = new System.Drawing.Point(21, 87);
            this.chkShowBalloonAfterUpload.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowBalloonAfterUpload.Name = "chkShowBalloonAfterUpload";
            this.chkShowBalloonAfterUpload.Size = new System.Drawing.Size(277, 21);
            this.chkShowBalloonAfterUpload.TabIndex = 2;
            this.chkShowBalloonAfterUpload.Text = "Show balloon after upload is completed";
            this.chkShowBalloonAfterUpload.UseVisualStyleBackColor = true;
            this.chkShowBalloonAfterUpload.CheckedChanged += new System.EventHandler(this.chkShowBalloon_CheckedChanged);
            // 
            // chkPlaySoundAfterUpload
            // 
            this.chkPlaySoundAfterUpload.AutoSize = true;
            this.chkPlaySoundAfterUpload.Location = new System.Drawing.Point(21, 59);
            this.chkPlaySoundAfterUpload.Margin = new System.Windows.Forms.Padding(4);
            this.chkPlaySoundAfterUpload.Name = "chkPlaySoundAfterUpload";
            this.chkPlaySoundAfterUpload.Size = new System.Drawing.Size(263, 21);
            this.chkPlaySoundAfterUpload.TabIndex = 1;
            this.chkPlaySoundAfterUpload.Text = "Play sound after upload is completed";
            this.chkPlaySoundAfterUpload.UseVisualStyleBackColor = true;
            this.chkPlaySoundAfterUpload.CheckedChanged += new System.EventHandler(this.chkPlaySoundAfterUpload_CheckedChanged);
            // 
            // cbPlaySoundAfterCapture
            // 
            this.cbPlaySoundAfterCapture.AutoSize = true;
            this.cbPlaySoundAfterCapture.Location = new System.Drawing.Point(21, 30);
            this.cbPlaySoundAfterCapture.Margin = new System.Windows.Forms.Padding(4);
            this.cbPlaySoundAfterCapture.Name = "cbPlaySoundAfterCapture";
            this.cbPlaySoundAfterCapture.Size = new System.Drawing.Size(238, 21);
            this.cbPlaySoundAfterCapture.TabIndex = 0;
            this.cbPlaySoundAfterCapture.Text = "Play sound after capture is made";
            this.cbPlaySoundAfterCapture.UseVisualStyleBackColor = true;
            this.cbPlaySoundAfterCapture.CheckedChanged += new System.EventHandler(this.cbPlaySoundAfterCapture_CheckedChanged);
            // 
            // lblGeneralInfo
            // 
            this.lblGeneralInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblGeneralInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGeneralInfo.ForeColor = System.Drawing.Color.White;
            this.lblGeneralInfo.Location = new System.Drawing.Point(0, 557);
            this.lblGeneralInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(727, 36);
            this.lblGeneralInfo.TabIndex = 5;
            this.lblGeneralInfo.Text = "Shell context menu is Windows Explorer right click menu for files and folders.";
            this.lblGeneralInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbCheckUpdates
            // 
            this.cbCheckUpdates.AutoSize = true;
            this.cbCheckUpdates.Location = new System.Drawing.Point(21, 108);
            this.cbCheckUpdates.Margin = new System.Windows.Forms.Padding(4);
            this.cbCheckUpdates.Name = "cbCheckUpdates";
            this.cbCheckUpdates.Size = new System.Drawing.Size(273, 21);
            this.cbCheckUpdates.TabIndex = 3;
            this.cbCheckUpdates.Text = "Automatically check updates at startup";
            this.cbCheckUpdates.UseVisualStyleBackColor = true;
            this.cbCheckUpdates.CheckedChanged += new System.EventHandler(this.cbCheckUpdates_CheckedChanged);
            // 
            // cbStartWithWindows
            // 
            this.cbStartWithWindows.AutoSize = true;
            this.cbStartWithWindows.Location = new System.Drawing.Point(21, 49);
            this.cbStartWithWindows.Margin = new System.Windows.Forms.Padding(4);
            this.cbStartWithWindows.Name = "cbStartWithWindows";
            this.cbStartWithWindows.Size = new System.Drawing.Size(199, 21);
            this.cbStartWithWindows.TabIndex = 1;
            this.cbStartWithWindows.Text = "Start ShareX with Windows";
            this.cbStartWithWindows.UseVisualStyleBackColor = true;
            this.cbStartWithWindows.CheckedChanged += new System.EventHandler(this.cbStartWithWindows_CheckedChanged);
            // 
            // cbShowTray
            // 
            this.cbShowTray.AutoSize = true;
            this.cbShowTray.Location = new System.Drawing.Point(21, 20);
            this.cbShowTray.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowTray.Name = "cbShowTray";
            this.cbShowTray.Size = new System.Drawing.Size(173, 21);
            this.cbShowTray.TabIndex = 0;
            this.cbShowTray.Text = "Show ShareX tray icon";
            this.cbShowTray.UseVisualStyleBackColor = true;
            this.cbShowTray.CheckedChanged += new System.EventHandler(this.cbShowTray_CheckedChanged);
            // 
            // cbShellContextMenu
            // 
            this.cbShellContextMenu.AutoSize = true;
            this.cbShellContextMenu.Location = new System.Drawing.Point(21, 79);
            this.cbShellContextMenu.Margin = new System.Windows.Forms.Padding(4);
            this.cbShellContextMenu.Name = "cbShellContextMenu";
            this.cbShellContextMenu.Size = new System.Drawing.Size(270, 21);
            this.cbShellContextMenu.TabIndex = 2;
            this.cbShellContextMenu.Text = "Show in \"Send To\" Shell context menu";
            this.cbShellContextMenu.UseVisualStyleBackColor = true;
            this.cbShellContextMenu.CheckedChanged += new System.EventHandler(this.cbShellContextMenu_CheckedChanged);
            // 
            // tpAdvanced
            // 
            this.tpAdvanced.Controls.Add(this.panelAdvanced);
            this.tpAdvanced.Location = new System.Drawing.Point(4, 25);
            this.tpAdvanced.Margin = new System.Windows.Forms.Padding(4);
            this.tpAdvanced.Name = "tpAdvanced";
            this.tpAdvanced.Size = new System.Drawing.Size(727, 593);
            this.tpAdvanced.TabIndex = 1;
            this.tpAdvanced.Text = "Advanced";
            this.tpAdvanced.UseVisualStyleBackColor = true;
            // 
            // panelAdvanced
            // 
            this.panelAdvanced.BackColor = System.Drawing.SystemColors.Control;
            this.panelAdvanced.Controls.Add(this.pgSettings);
            this.panelAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdvanced.Location = new System.Drawing.Point(0, 0);
            this.panelAdvanced.Margin = new System.Windows.Forms.Padding(4);
            this.panelAdvanced.Name = "panelAdvanced";
            this.panelAdvanced.Size = new System.Drawing.Size(727, 593);
            this.panelAdvanced.TabIndex = 0;
            // 
            // pgSettings
            // 
            this.pgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSettings.Location = new System.Drawing.Point(0, 0);
            this.pgSettings.Margin = new System.Windows.Forms.Padding(4);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.Size = new System.Drawing.Size(727, 593);
            this.pgSettings.TabIndex = 0;
            // 
            // tpCapture
            // 
            this.tpCapture.Controls.Add(this.panelCapture);
            this.tpCapture.Location = new System.Drawing.Point(4, 25);
            this.tpCapture.Margin = new System.Windows.Forms.Padding(4);
            this.tpCapture.Name = "tpCapture";
            this.tpCapture.Padding = new System.Windows.Forms.Padding(4);
            this.tpCapture.Size = new System.Drawing.Size(727, 593);
            this.tpCapture.TabIndex = 2;
            this.tpCapture.Text = "Capture";
            this.tpCapture.UseVisualStyleBackColor = true;
            // 
            // panelCapture
            // 
            this.panelCapture.BackColor = System.Drawing.SystemColors.Control;
            this.panelCapture.Controls.Add(this.gbCaptureAfter);
            this.panelCapture.Controls.Add(this.cbCaptureShadow);
            this.panelCapture.Controls.Add(this.cbShowCursor);
            this.panelCapture.Controls.Add(this.cbCaptureTransparent);
            this.panelCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCapture.Location = new System.Drawing.Point(4, 4);
            this.panelCapture.Margin = new System.Windows.Forms.Padding(4);
            this.panelCapture.Name = "panelCapture";
            this.panelCapture.Size = new System.Drawing.Size(719, 585);
            this.panelCapture.TabIndex = 0;
            // 
            // gbCaptureAfter
            // 
            this.gbCaptureAfter.Controls.Add(this.ucAfterCaptureTasks);
            this.gbCaptureAfter.Location = new System.Drawing.Point(15, 118);
            this.gbCaptureAfter.Margin = new System.Windows.Forms.Padding(4);
            this.gbCaptureAfter.Name = "gbCaptureAfter";
            this.gbCaptureAfter.Padding = new System.Windows.Forms.Padding(4);
            this.gbCaptureAfter.Size = new System.Drawing.Size(668, 364);
            this.gbCaptureAfter.TabIndex = 3;
            this.gbCaptureAfter.TabStop = false;
            this.gbCaptureAfter.Text = "After capture tasks (only applicable for workflows that contain this activity)";
            this.gbCaptureAfter.Visible = false;
            // 
            // ucAfterCaptureTasks
            // 
            this.ucAfterCaptureTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterCaptureTasks.Location = new System.Drawing.Point(4, 19);
            this.ucAfterCaptureTasks.Margin = new System.Windows.Forms.Padding(5);
            this.ucAfterCaptureTasks.Name = "ucAfterCaptureTasks";
            this.ucAfterCaptureTasks.Size = new System.Drawing.Size(660, 341);
            this.ucAfterCaptureTasks.TabIndex = 0;
            // 
            // cbCaptureShadow
            // 
            this.cbCaptureShadow.AutoSize = true;
            this.cbCaptureShadow.Location = new System.Drawing.Point(21, 79);
            this.cbCaptureShadow.Margin = new System.Windows.Forms.Padding(4);
            this.cbCaptureShadow.Name = "cbCaptureShadow";
            this.cbCaptureShadow.Size = new System.Drawing.Size(362, 21);
            this.cbCaptureShadow.TabIndex = 2;
            this.cbCaptureShadow.Text = "Capture window with shadow (requires transparency)";
            this.cbCaptureShadow.UseVisualStyleBackColor = true;
            this.cbCaptureShadow.CheckedChanged += new System.EventHandler(this.cbCaptureShadow_CheckedChanged);
            // 
            // cbShowCursor
            // 
            this.cbShowCursor.AutoSize = true;
            this.cbShowCursor.Location = new System.Drawing.Point(21, 20);
            this.cbShowCursor.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowCursor.Name = "cbShowCursor";
            this.cbShowCursor.Size = new System.Drawing.Size(204, 21);
            this.cbShowCursor.TabIndex = 0;
            this.cbShowCursor.Text = "Show cursor in screenshots";
            this.cbShowCursor.UseVisualStyleBackColor = true;
            this.cbShowCursor.CheckedChanged += new System.EventHandler(this.cbShowCursor_CheckedChanged);
            // 
            // cbCaptureTransparent
            // 
            this.cbCaptureTransparent.AutoSize = true;
            this.cbCaptureTransparent.Location = new System.Drawing.Point(21, 49);
            this.cbCaptureTransparent.Margin = new System.Windows.Forms.Padding(4);
            this.cbCaptureTransparent.Name = "cbCaptureTransparent";
            this.cbCaptureTransparent.Size = new System.Drawing.Size(244, 21);
            this.cbCaptureTransparent.TabIndex = 1;
            this.cbCaptureTransparent.Text = "Capture window with transparency";
            this.cbCaptureTransparent.UseVisualStyleBackColor = true;
            this.cbCaptureTransparent.CheckedChanged += new System.EventHandler(this.cbCaptureTransparent_CheckedChanged);
            // 
            // tpShapes
            // 
            this.tpShapes.Controls.Add(this.panelShapes);
            this.tpShapes.Location = new System.Drawing.Point(4, 25);
            this.tpShapes.Margin = new System.Windows.Forms.Padding(4);
            this.tpShapes.Name = "tpShapes";
            this.tpShapes.Padding = new System.Windows.Forms.Padding(4);
            this.tpShapes.Size = new System.Drawing.Size(727, 593);
            this.tpShapes.TabIndex = 3;
            this.tpShapes.Text = "Shapes";
            this.tpShapes.UseVisualStyleBackColor = true;
            // 
            // panelShapes
            // 
            this.panelShapes.BackColor = System.Drawing.SystemColors.Control;
            this.panelShapes.Controls.Add(this.cbShapeForceWindowCapture);
            this.panelShapes.Controls.Add(this.cbShapeIncludeControls);
            this.panelShapes.Controls.Add(this.lblFixedShapeSizeHeight);
            this.panelShapes.Controls.Add(this.cbDrawBorder);
            this.panelShapes.Controls.Add(this.lblFixedShapeSizeWidth);
            this.panelShapes.Controls.Add(this.cbQuickCrop);
            this.panelShapes.Controls.Add(this.nudFixedShapeSizeHeight);
            this.panelShapes.Controls.Add(this.cbDrawCheckerboard);
            this.panelShapes.Controls.Add(this.nudFixedShapeSizeWidth);
            this.panelShapes.Controls.Add(this.cbFixedShapeSize);
            this.panelShapes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShapes.Location = new System.Drawing.Point(4, 4);
            this.panelShapes.Margin = new System.Windows.Forms.Padding(4);
            this.panelShapes.Name = "panelShapes";
            this.panelShapes.Size = new System.Drawing.Size(719, 585);
            this.panelShapes.TabIndex = 0;
            // 
            // cbShapeForceWindowCapture
            // 
            this.cbShapeForceWindowCapture.AutoSize = true;
            this.cbShapeForceWindowCapture.Location = new System.Drawing.Point(21, 217);
            this.cbShapeForceWindowCapture.Margin = new System.Windows.Forms.Padding(4);
            this.cbShapeForceWindowCapture.Name = "cbShapeForceWindowCapture";
            this.cbShapeForceWindowCapture.Size = new System.Drawing.Size(378, 21);
            this.cbShapeForceWindowCapture.TabIndex = 9;
            this.cbShapeForceWindowCapture.Text = "Use window capture mode for all rectangle type shapes";
            this.cbShapeForceWindowCapture.UseVisualStyleBackColor = true;
            this.cbShapeForceWindowCapture.CheckedChanged += new System.EventHandler(this.cbShapeForceWindowCapture_CheckedChanged);
            // 
            // cbShapeIncludeControls
            // 
            this.cbShapeIncludeControls.AutoSize = true;
            this.cbShapeIncludeControls.Location = new System.Drawing.Point(21, 187);
            this.cbShapeIncludeControls.Margin = new System.Windows.Forms.Padding(4);
            this.cbShapeIncludeControls.Name = "cbShapeIncludeControls";
            this.cbShapeIncludeControls.Size = new System.Drawing.Size(433, 21);
            this.cbShapeIncludeControls.TabIndex = 8;
            this.cbShapeIncludeControls.Text = "Allow capturing controls in window capture (buttons, panels etc.)";
            this.cbShapeIncludeControls.UseVisualStyleBackColor = true;
            this.cbShapeIncludeControls.CheckedChanged += new System.EventHandler(this.cbShapeIncludeControls_CheckedChanged);
            // 
            // lblFixedShapeSizeHeight
            // 
            this.lblFixedShapeSizeHeight.AutoSize = true;
            this.lblFixedShapeSizeHeight.Location = new System.Drawing.Point(213, 143);
            this.lblFixedShapeSizeHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFixedShapeSizeHeight.Name = "lblFixedShapeSizeHeight";
            this.lblFixedShapeSizeHeight.Size = new System.Drawing.Size(53, 17);
            this.lblFixedShapeSizeHeight.TabIndex = 6;
            this.lblFixedShapeSizeHeight.Text = "Height:";
            // 
            // cbDrawBorder
            // 
            this.cbDrawBorder.AutoSize = true;
            this.cbDrawBorder.Location = new System.Drawing.Point(21, 20);
            this.cbDrawBorder.Margin = new System.Windows.Forms.Padding(4);
            this.cbDrawBorder.Name = "cbDrawBorder";
            this.cbDrawBorder.Size = new System.Drawing.Size(224, 21);
            this.cbDrawBorder.TabIndex = 0;
            this.cbDrawBorder.Text = "Draw border around the shape";
            this.cbDrawBorder.UseVisualStyleBackColor = true;
            this.cbDrawBorder.CheckedChanged += new System.EventHandler(this.cbDrawBorder_CheckedChanged);
            // 
            // lblFixedShapeSizeWidth
            // 
            this.lblFixedShapeSizeWidth.AutoSize = true;
            this.lblFixedShapeSizeWidth.Location = new System.Drawing.Point(59, 143);
            this.lblFixedShapeSizeWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFixedShapeSizeWidth.Name = "lblFixedShapeSizeWidth";
            this.lblFixedShapeSizeWidth.Size = new System.Drawing.Size(48, 17);
            this.lblFixedShapeSizeWidth.TabIndex = 4;
            this.lblFixedShapeSizeWidth.Text = "Width:";
            // 
            // cbQuickCrop
            // 
            this.cbQuickCrop.AutoSize = true;
            this.cbQuickCrop.Location = new System.Drawing.Point(21, 79);
            this.cbQuickCrop.Margin = new System.Windows.Forms.Padding(4);
            this.cbQuickCrop.Name = "cbQuickCrop";
            this.cbQuickCrop.Size = new System.Drawing.Size(604, 21);
            this.cbQuickCrop.TabIndex = 2;
            this.cbQuickCrop.Text = "Complete capture as soon as the mouse button is released, except when capturing p" +
    "olygon";
            this.cbQuickCrop.UseVisualStyleBackColor = true;
            this.cbQuickCrop.CheckedChanged += new System.EventHandler(this.cbQuickCrop_CheckedChanged);
            // 
            // nudFixedShapeSizeHeight
            // 
            this.nudFixedShapeSizeHeight.Location = new System.Drawing.Point(277, 138);
            this.nudFixedShapeSizeHeight.Margin = new System.Windows.Forms.Padding(4);
            this.nudFixedShapeSizeHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudFixedShapeSizeHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeHeight.Name = "nudFixedShapeSizeHeight";
            this.nudFixedShapeSizeHeight.Size = new System.Drawing.Size(75, 22);
            this.nudFixedShapeSizeHeight.TabIndex = 7;
            this.nudFixedShapeSizeHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeHeight.ValueChanged += new System.EventHandler(this.nudFixedShapeSizeHeight_ValueChanged);
            // 
            // cbDrawCheckerboard
            // 
            this.cbDrawCheckerboard.AutoSize = true;
            this.cbDrawCheckerboard.Location = new System.Drawing.Point(21, 49);
            this.cbDrawCheckerboard.Margin = new System.Windows.Forms.Padding(4);
            this.cbDrawCheckerboard.Name = "cbDrawCheckerboard";
            this.cbDrawCheckerboard.Size = new System.Drawing.Size(381, 21);
            this.cbDrawCheckerboard.TabIndex = 1;
            this.cbDrawCheckerboard.Text = "Draw checkerboard pattern replacing transparent areas";
            this.cbDrawCheckerboard.UseVisualStyleBackColor = true;
            this.cbDrawCheckerboard.CheckedChanged += new System.EventHandler(this.cbDrawCheckerboard_CheckedChanged);
            // 
            // nudFixedShapeSizeWidth
            // 
            this.nudFixedShapeSizeWidth.Location = new System.Drawing.Point(117, 138);
            this.nudFixedShapeSizeWidth.Margin = new System.Windows.Forms.Padding(4);
            this.nudFixedShapeSizeWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudFixedShapeSizeWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeWidth.Name = "nudFixedShapeSizeWidth";
            this.nudFixedShapeSizeWidth.Size = new System.Drawing.Size(75, 22);
            this.nudFixedShapeSizeWidth.TabIndex = 5;
            this.nudFixedShapeSizeWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFixedShapeSizeWidth.ValueChanged += new System.EventHandler(this.nudFixedShapeSizeWidth_ValueChanged);
            // 
            // cbFixedShapeSize
            // 
            this.cbFixedShapeSize.AutoSize = true;
            this.cbFixedShapeSize.Location = new System.Drawing.Point(21, 108);
            this.cbFixedShapeSize.Margin = new System.Windows.Forms.Padding(4);
            this.cbFixedShapeSize.Name = "cbFixedShapeSize";
            this.cbFixedShapeSize.Size = new System.Drawing.Size(139, 21);
            this.cbFixedShapeSize.TabIndex = 3;
            this.cbFixedShapeSize.Text = "Fixed shape size:";
            this.cbFixedShapeSize.UseVisualStyleBackColor = true;
            this.cbFixedShapeSize.CheckedChanged += new System.EventHandler(this.cbFixedShapeSize_CheckedChanged);
            // 
            // tpProxy
            // 
            this.tpProxy.Controls.Add(this.panelProxy);
            this.tpProxy.Location = new System.Drawing.Point(4, 25);
            this.tpProxy.Margin = new System.Windows.Forms.Padding(4);
            this.tpProxy.Name = "tpProxy";
            this.tpProxy.Padding = new System.Windows.Forms.Padding(4);
            this.tpProxy.Size = new System.Drawing.Size(727, 593);
            this.tpProxy.TabIndex = 4;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // panelProxy
            // 
            this.panelProxy.Controls.Add(this.btnAutofillProxy);
            this.panelProxy.Controls.Add(this.pgProxy);
            this.panelProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProxy.Location = new System.Drawing.Point(4, 4);
            this.panelProxy.Margin = new System.Windows.Forms.Padding(4);
            this.panelProxy.Name = "panelProxy";
            this.panelProxy.Size = new System.Drawing.Size(719, 585);
            this.panelProxy.TabIndex = 0;
            // 
            // btnAutofillProxy
            // 
            this.btnAutofillProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutofillProxy.Location = new System.Drawing.Point(606, 541);
            this.btnAutofillProxy.Margin = new System.Windows.Forms.Padding(4);
            this.btnAutofillProxy.Name = "btnAutofillProxy";
            this.btnAutofillProxy.Size = new System.Drawing.Size(100, 28);
            this.btnAutofillProxy.TabIndex = 1;
            this.btnAutofillProxy.Text = "Autofill";
            this.btnAutofillProxy.UseVisualStyleBackColor = true;
            this.btnAutofillProxy.Click += new System.EventHandler(this.btnAutofillProxy_Click);
            // 
            // pgProxy
            // 
            this.pgProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProxy.Location = new System.Drawing.Point(0, 0);
            this.pgProxy.Margin = new System.Windows.Forms.Padding(4);
            this.pgProxy.Name = "pgProxy";
            this.pgProxy.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgProxy.Size = new System.Drawing.Size(719, 585);
            this.pgProxy.TabIndex = 0;
            this.pgProxy.ToolbarVisible = false;
            // 
            // tpClipboardUpload
            // 
            this.tpClipboardUpload.Controls.Add(this.panelClipboardUpload);
            this.tpClipboardUpload.Location = new System.Drawing.Point(4, 25);
            this.tpClipboardUpload.Margin = new System.Windows.Forms.Padding(4);
            this.tpClipboardUpload.Name = "tpClipboardUpload";
            this.tpClipboardUpload.Padding = new System.Windows.Forms.Padding(4);
            this.tpClipboardUpload.Size = new System.Drawing.Size(727, 593);
            this.tpClipboardUpload.TabIndex = 5;
            this.tpClipboardUpload.Text = "Clipboard Upload";
            this.tpClipboardUpload.UseVisualStyleBackColor = true;
            // 
            // panelClipboardUpload
            // 
            this.panelClipboardUpload.Controls.Add(this.cbClipboardUploadAutoDetectURL);
            this.panelClipboardUpload.Controls.Add(this.lblClipboardUploadInfo);
            this.panelClipboardUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClipboardUpload.Location = new System.Drawing.Point(4, 4);
            this.panelClipboardUpload.Margin = new System.Windows.Forms.Padding(4);
            this.panelClipboardUpload.Name = "panelClipboardUpload";
            this.panelClipboardUpload.Size = new System.Drawing.Size(719, 585);
            this.panelClipboardUpload.TabIndex = 0;
            // 
            // cbClipboardUploadAutoDetectURL
            // 
            this.cbClipboardUploadAutoDetectURL.AutoSize = true;
            this.cbClipboardUploadAutoDetectURL.Location = new System.Drawing.Point(21, 20);
            this.cbClipboardUploadAutoDetectURL.Margin = new System.Windows.Forms.Padding(4);
            this.cbClipboardUploadAutoDetectURL.Name = "cbClipboardUploadAutoDetectURL";
            this.cbClipboardUploadAutoDetectURL.Size = new System.Drawing.Size(529, 21);
            this.cbClipboardUploadAutoDetectURL.TabIndex = 0;
            this.cbClipboardUploadAutoDetectURL.Text = "Automatically detect URL when performing Text Upload and use URL shortener";
            this.cbClipboardUploadAutoDetectURL.UseVisualStyleBackColor = true;
            this.cbClipboardUploadAutoDetectURL.CheckedChanged += new System.EventHandler(this.cbClipboardUploadAutoDetectURL_CheckedChanged);
            // 
            // lblClipboardUploadInfo
            // 
            this.lblClipboardUploadInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblClipboardUploadInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClipboardUploadInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblClipboardUploadInfo.ForeColor = System.Drawing.Color.White;
            this.lblClipboardUploadInfo.Location = new System.Drawing.Point(0, 550);
            this.lblClipboardUploadInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClipboardUploadInfo.Name = "lblClipboardUploadInfo";
            this.lblClipboardUploadInfo.Size = new System.Drawing.Size(719, 35);
            this.lblClipboardUploadInfo.TabIndex = 1;
            this.lblClipboardUploadInfo.Text = "Clipboard upload automatically detects the data type and selects the upload servi" +
    "ce accordingly.";
            this.lblClipboardUploadInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpUpload
            // 
            this.tpUpload.Controls.Add(this.panelUpload);
            this.tpUpload.Location = new System.Drawing.Point(4, 25);
            this.tpUpload.Margin = new System.Windows.Forms.Padding(4);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Padding = new System.Windows.Forms.Padding(4);
            this.tpUpload.Size = new System.Drawing.Size(727, 593);
            this.tpUpload.TabIndex = 6;
            this.tpUpload.Text = "Upload";
            this.tpUpload.UseVisualStyleBackColor = true;
            // 
            // panelUpload
            // 
            this.panelUpload.Controls.Add(this.gbAfterUpload);
            this.panelUpload.Controls.Add(this.lblUploadLimitHint);
            this.panelUpload.Controls.Add(this.nudUploadLimit);
            this.panelUpload.Controls.Add(this.lblUploadLimit);
            this.panelUpload.Controls.Add(this.lblBufferSize);
            this.panelUpload.Controls.Add(this.lblBufferSizeInfo);
            this.panelUpload.Controls.Add(this.cbBufferSize);
            this.panelUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUpload.Location = new System.Drawing.Point(4, 4);
            this.panelUpload.Margin = new System.Windows.Forms.Padding(4);
            this.panelUpload.Name = "panelUpload";
            this.panelUpload.Size = new System.Drawing.Size(719, 585);
            this.panelUpload.TabIndex = 0;
            // 
            // gbAfterUpload
            // 
            this.gbAfterUpload.Controls.Add(this.ucAfterUploadTasks);
            this.gbAfterUpload.Location = new System.Drawing.Point(21, 108);
            this.gbAfterUpload.Margin = new System.Windows.Forms.Padding(4);
            this.gbAfterUpload.Name = "gbAfterUpload";
            this.gbAfterUpload.Padding = new System.Windows.Forms.Padding(4);
            this.gbAfterUpload.Size = new System.Drawing.Size(644, 138);
            this.gbAfterUpload.TabIndex = 6;
            this.gbAfterUpload.TabStop = false;
            this.gbAfterUpload.Text = "After Upload";
            this.gbAfterUpload.Visible = false;
            // 
            // ucAfterUploadTasks
            // 
            this.ucAfterUploadTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterUploadTasks.Location = new System.Drawing.Point(4, 19);
            this.ucAfterUploadTasks.Margin = new System.Windows.Forms.Padding(5);
            this.ucAfterUploadTasks.Name = "ucAfterUploadTasks";
            this.ucAfterUploadTasks.Size = new System.Drawing.Size(636, 115);
            this.ucAfterUploadTasks.TabIndex = 0;
            // 
            // lblUploadLimitHint
            // 
            this.lblUploadLimitHint.AutoSize = true;
            this.lblUploadLimitHint.Location = new System.Drawing.Point(288, 30);
            this.lblUploadLimitHint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUploadLimitHint.Name = "lblUploadLimitHint";
            this.lblUploadLimitHint.Size = new System.Drawing.Size(123, 17);
            this.lblUploadLimitHint.TabIndex = 2;
            this.lblUploadLimitHint.Text = "0 - 25 (0 disables)";
            // 
            // nudUploadLimit
            // 
            this.nudUploadLimit.Location = new System.Drawing.Point(203, 25);
            this.nudUploadLimit.Margin = new System.Windows.Forms.Padding(4);
            this.nudUploadLimit.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudUploadLimit.Name = "nudUploadLimit";
            this.nudUploadLimit.Size = new System.Drawing.Size(75, 22);
            this.nudUploadLimit.TabIndex = 1;
            this.nudUploadLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudUploadLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudUploadLimit.ValueChanged += new System.EventHandler(this.nudUploadLimit_ValueChanged);
            // 
            // lblUploadLimit
            // 
            this.lblUploadLimit.AutoSize = true;
            this.lblUploadLimit.Location = new System.Drawing.Point(21, 30);
            this.lblUploadLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUploadLimit.Name = "lblUploadLimit";
            this.lblUploadLimit.Size = new System.Drawing.Size(172, 17);
            this.lblUploadLimit.TabIndex = 0;
            this.lblUploadLimit.Text = "Simultaneous upload limit:";
            // 
            // lblBufferSize
            // 
            this.lblBufferSize.AutoSize = true;
            this.lblBufferSize.Location = new System.Drawing.Point(21, 69);
            this.lblBufferSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBufferSize.Name = "lblBufferSize";
            this.lblBufferSize.Size = new System.Drawing.Size(79, 17);
            this.lblBufferSize.TabIndex = 3;
            this.lblBufferSize.Text = "Buffer size:";
            // 
            // lblBufferSizeInfo
            // 
            this.lblBufferSizeInfo.AutoSize = true;
            this.lblBufferSizeInfo.Location = new System.Drawing.Point(203, 69);
            this.lblBufferSizeInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBufferSizeInfo.Name = "lblBufferSizeInfo";
            this.lblBufferSizeInfo.Size = new System.Drawing.Size(29, 17);
            this.lblBufferSizeInfo.TabIndex = 5;
            this.lblBufferSizeInfo.Text = "KiB";
            // 
            // cbBufferSize
            // 
            this.cbBufferSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBufferSize.FormattingEnabled = true;
            this.cbBufferSize.Location = new System.Drawing.Point(107, 64);
            this.cbBufferSize.Margin = new System.Windows.Forms.Padding(4);
            this.cbBufferSize.Name = "cbBufferSize";
            this.cbBufferSize.Size = new System.Drawing.Size(84, 24);
            this.cbBufferSize.TabIndex = 4;
            this.cbBufferSize.SelectedIndexChanged += new System.EventHandler(this.cbBufferSize_SelectedIndexChanged);
            // 
            // tpImageResize
            // 
            this.tpImageResize.Controls.Add(this.panelImageResize);
            this.tpImageResize.Location = new System.Drawing.Point(4, 25);
            this.tpImageResize.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageResize.Name = "tpImageResize";
            this.tpImageResize.Padding = new System.Windows.Forms.Padding(4);
            this.tpImageResize.Size = new System.Drawing.Size(727, 593);
            this.tpImageResize.TabIndex = 7;
            this.tpImageResize.Text = "Resize";
            this.tpImageResize.UseVisualStyleBackColor = true;
            // 
            // panelImageResize
            // 
            this.panelImageResize.Controls.Add(this.ucImageResizeUI);
            this.panelImageResize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImageResize.Location = new System.Drawing.Point(4, 4);
            this.panelImageResize.Margin = new System.Windows.Forms.Padding(4);
            this.panelImageResize.Name = "panelImageResize";
            this.panelImageResize.Size = new System.Drawing.Size(719, 585);
            this.panelImageResize.TabIndex = 0;
            // 
            // ucImageResizeUI
            // 
            this.ucImageResizeUI.Location = new System.Drawing.Point(11, 10);
            this.ucImageResizeUI.Margin = new System.Windows.Forms.Padding(5);
            this.ucImageResizeUI.Name = "ucImageResizeUI";
            this.ucImageResizeUI.Size = new System.Drawing.Size(619, 381);
            this.ucImageResizeUI.TabIndex = 0;
            // 
            // tpImageQuality2
            // 
            this.tpImageQuality2.Controls.Add(this.panelImageQuality);
            this.tpImageQuality2.Location = new System.Drawing.Point(4, 25);
            this.tpImageQuality2.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageQuality2.Name = "tpImageQuality2";
            this.tpImageQuality2.Padding = new System.Windows.Forms.Padding(4);
            this.tpImageQuality2.Size = new System.Drawing.Size(727, 593);
            this.tpImageQuality2.TabIndex = 8;
            this.tpImageQuality2.Text = "Quality";
            this.tpImageQuality2.UseVisualStyleBackColor = true;
            // 
            // panelImageQuality
            // 
            this.panelImageQuality.Controls.Add(this.ucImageQualityUI);
            this.panelImageQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImageQuality.Location = new System.Drawing.Point(4, 4);
            this.panelImageQuality.Margin = new System.Windows.Forms.Padding(4);
            this.panelImageQuality.Name = "panelImageQuality";
            this.panelImageQuality.Size = new System.Drawing.Size(719, 585);
            this.panelImageQuality.TabIndex = 0;
            // 
            // ucImageQualityUI
            // 
            this.ucImageQualityUI.Location = new System.Drawing.Point(21, 20);
            this.ucImageQualityUI.Margin = new System.Windows.Forms.Padding(5);
            this.ucImageQualityUI.Name = "ucImageQualityUI";
            this.ucImageQualityUI.Size = new System.Drawing.Size(509, 273);
            this.ucImageQualityUI.TabIndex = 0;
            // 
            // tpPaths
            // 
            this.tpPaths.Controls.Add(this.panelPaths);
            this.tpPaths.Location = new System.Drawing.Point(4, 25);
            this.tpPaths.Margin = new System.Windows.Forms.Padding(4);
            this.tpPaths.Name = "tpPaths";
            this.tpPaths.Padding = new System.Windows.Forms.Padding(4);
            this.tpPaths.Size = new System.Drawing.Size(727, 593);
            this.tpPaths.TabIndex = 9;
            this.tpPaths.Text = "Paths";
            this.tpPaths.UseVisualStyleBackColor = true;
            // 
            // panelPaths
            // 
            this.panelPaths.AutoScroll = true;
            this.panelPaths.Controls.Add(this.gbPathRoot);
            this.panelPaths.Controls.Add(this.gbScreenshots);
            this.panelPaths.Controls.Add(this.gbUploadersConfig);
            this.panelPaths.Controls.Add(this.gbHistory);
            this.panelPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPaths.Location = new System.Drawing.Point(4, 4);
            this.panelPaths.Margin = new System.Windows.Forms.Padding(4);
            this.panelPaths.Name = "panelPaths";
            this.panelPaths.Size = new System.Drawing.Size(719, 585);
            this.panelPaths.TabIndex = 0;
            // 
            // gbPathRoot
            // 
            this.gbPathRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPathRoot.Controls.Add(this.lblOpenZUploaderPath);
            this.gbPathRoot.Controls.Add(this.btnOpenZUploaderPath);
            this.gbPathRoot.Location = new System.Drawing.Point(11, 443);
            this.gbPathRoot.Margin = new System.Windows.Forms.Padding(4);
            this.gbPathRoot.Name = "gbPathRoot";
            this.gbPathRoot.Padding = new System.Windows.Forms.Padding(4);
            this.gbPathRoot.Size = new System.Drawing.Size(564, 108);
            this.gbPathRoot.TabIndex = 3;
            this.gbPathRoot.TabStop = false;
            this.gbPathRoot.Text = "Root";
            // 
            // lblOpenZUploaderPath
            // 
            this.lblOpenZUploaderPath.AutoSize = true;
            this.lblOpenZUploaderPath.Location = new System.Drawing.Point(21, 69);
            this.lblOpenZUploaderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOpenZUploaderPath.Name = "lblOpenZUploaderPath";
            this.lblOpenZUploaderPath.Size = new System.Drawing.Size(348, 17);
            this.lblOpenZUploaderPath.TabIndex = 1;
            this.lblOpenZUploaderPath.Text = "This folder has settings, history database and log files";
            // 
            // btnOpenZUploaderPath
            // 
            this.btnOpenZUploaderPath.Location = new System.Drawing.Point(21, 30);
            this.btnOpenZUploaderPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenZUploaderPath.Name = "btnOpenZUploaderPath";
            this.btnOpenZUploaderPath.Size = new System.Drawing.Size(235, 28);
            this.btnOpenZUploaderPath.TabIndex = 0;
            this.btnOpenZUploaderPath.Text = "Open ShareX personal folder";
            this.btnOpenZUploaderPath.UseVisualStyleBackColor = true;
            this.btnOpenZUploaderPath.Click += new System.EventHandler(this.btnOpenPersonalPath_Click);
            // 
            // gbScreenshots
            // 
            this.gbScreenshots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbScreenshots.Controls.Add(this.btnImagesOrganise);
            this.gbScreenshots.Controls.Add(this.txtSaveImageSubFolderPatternPreview);
            this.gbScreenshots.Controls.Add(this.lblSaveImageSubFolderPattern);
            this.gbScreenshots.Controls.Add(this.txtSaveImageSubFolderPattern);
            this.gbScreenshots.Controls.Add(this.btnBrowseScreenshotsDir);
            this.gbScreenshots.Controls.Add(this.txtScreenshotsPath);
            this.gbScreenshots.Location = new System.Drawing.Point(11, 10);
            this.gbScreenshots.Margin = new System.Windows.Forms.Padding(4);
            this.gbScreenshots.Name = "gbScreenshots";
            this.gbScreenshots.Padding = new System.Windows.Forms.Padding(4);
            this.gbScreenshots.Size = new System.Drawing.Size(619, 108);
            this.gbScreenshots.TabIndex = 0;
            this.gbScreenshots.TabStop = false;
            this.gbScreenshots.Text = "Screenshots";
            // 
            // btnImagesOrganise
            // 
            this.btnImagesOrganise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImagesOrganise.AutoSize = true;
            this.btnImagesOrganise.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImagesOrganise.Location = new System.Drawing.Point(504, 63);
            this.btnImagesOrganise.Margin = new System.Windows.Forms.Padding(4);
            this.btnImagesOrganise.Name = "btnImagesOrganise";
            this.btnImagesOrganise.Size = new System.Drawing.Size(88, 27);
            this.btnImagesOrganise.TabIndex = 5;
            this.btnImagesOrganise.Text = "&Organise...";
            this.btnImagesOrganise.UseVisualStyleBackColor = true;
            this.btnImagesOrganise.Click += new System.EventHandler(this.btnImagesOrganise_Click);
            // 
            // txtSaveImageSubFolderPatternPreview
            // 
            this.txtSaveImageSubFolderPatternPreview.Location = new System.Drawing.Point(331, 65);
            this.txtSaveImageSubFolderPatternPreview.Margin = new System.Windows.Forms.Padding(4);
            this.txtSaveImageSubFolderPatternPreview.Name = "txtSaveImageSubFolderPatternPreview";
            this.txtSaveImageSubFolderPatternPreview.ReadOnly = true;
            this.txtSaveImageSubFolderPatternPreview.Size = new System.Drawing.Size(159, 22);
            this.txtSaveImageSubFolderPatternPreview.TabIndex = 4;
            // 
            // lblSaveImageSubFolderPattern
            // 
            this.lblSaveImageSubFolderPattern.AutoSize = true;
            this.lblSaveImageSubFolderPattern.Location = new System.Drawing.Point(21, 69);
            this.lblSaveImageSubFolderPattern.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSaveImageSubFolderPattern.Name = "lblSaveImageSubFolderPattern";
            this.lblSaveImageSubFolderPattern.Size = new System.Drawing.Size(126, 17);
            this.lblSaveImageSubFolderPattern.TabIndex = 2;
            this.lblSaveImageSubFolderPattern.Text = "Sub folder pattern:";
            // 
            // txtSaveImageSubFolderPattern
            // 
            this.txtSaveImageSubFolderPattern.Location = new System.Drawing.Point(160, 65);
            this.txtSaveImageSubFolderPattern.Margin = new System.Windows.Forms.Padding(4);
            this.txtSaveImageSubFolderPattern.Name = "txtSaveImageSubFolderPattern";
            this.txtSaveImageSubFolderPattern.Size = new System.Drawing.Size(159, 22);
            this.txtSaveImageSubFolderPattern.TabIndex = 3;
            this.txtSaveImageSubFolderPattern.TextChanged += new System.EventHandler(this.txtSaveImageSubFolderPattern_TextChanged);
            // 
            // btnBrowseScreenshotsDir
            // 
            this.btnBrowseScreenshotsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseScreenshotsDir.AutoSize = true;
            this.btnBrowseScreenshotsDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseScreenshotsDir.Location = new System.Drawing.Point(516, 27);
            this.btnBrowseScreenshotsDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseScreenshotsDir.Name = "btnBrowseScreenshotsDir";
            this.btnBrowseScreenshotsDir.Size = new System.Drawing.Size(76, 27);
            this.btnBrowseScreenshotsDir.TabIndex = 1;
            this.btnBrowseScreenshotsDir.Text = "&Browse...";
            this.btnBrowseScreenshotsDir.UseVisualStyleBackColor = true;
            this.btnBrowseScreenshotsDir.Click += new System.EventHandler(this.btnBrowseScreenshotsDir_Click);
            // 
            // txtScreenshotsPath
            // 
            this.txtScreenshotsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScreenshotsPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtScreenshotsPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtScreenshotsPath.Location = new System.Drawing.Point(28, 30);
            this.txtScreenshotsPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtScreenshotsPath.Name = "txtScreenshotsPath";
            this.txtScreenshotsPath.Size = new System.Drawing.Size(451, 22);
            this.txtScreenshotsPath.TabIndex = 0;
            this.txtScreenshotsPath.Leave += new System.EventHandler(this.txtScreenshotsPath_Leave);
            // 
            // gbUploadersConfig
            // 
            this.gbUploadersConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUploadersConfig.Controls.Add(this.btnLoadUploadersConfig);
            this.gbUploadersConfig.Controls.Add(this.btnBrowseCustomUploadersConfigPath);
            this.gbUploadersConfig.Controls.Add(this.txtCustomUploadersConfigPath);
            this.gbUploadersConfig.Controls.Add(this.cbUseCustomUploadersConfigPath);
            this.gbUploadersConfig.Location = new System.Drawing.Point(11, 138);
            this.gbUploadersConfig.Margin = new System.Windows.Forms.Padding(4);
            this.gbUploadersConfig.Name = "gbUploadersConfig";
            this.gbUploadersConfig.Padding = new System.Windows.Forms.Padding(4);
            this.gbUploadersConfig.Size = new System.Drawing.Size(619, 98);
            this.gbUploadersConfig.TabIndex = 1;
            this.gbUploadersConfig.TabStop = false;
            this.gbUploadersConfig.Text = "Uploaders Config";
            // 
            // btnLoadUploadersConfig
            // 
            this.btnLoadUploadersConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadUploadersConfig.AutoSize = true;
            this.btnLoadUploadersConfig.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadUploadersConfig.Location = new System.Drawing.Point(539, 21);
            this.btnLoadUploadersConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadUploadersConfig.Name = "btnLoadUploadersConfig";
            this.btnLoadUploadersConfig.Size = new System.Drawing.Size(50, 27);
            this.btnLoadUploadersConfig.TabIndex = 0;
            this.btnLoadUploadersConfig.Text = "Load";
            this.btnLoadUploadersConfig.UseVisualStyleBackColor = true;
            this.btnLoadUploadersConfig.Click += new System.EventHandler(this.btnLoadUploadersConfig_Click);
            // 
            // btnBrowseCustomUploadersConfigPath
            // 
            this.btnBrowseCustomUploadersConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseCustomUploadersConfigPath.AutoSize = true;
            this.btnBrowseCustomUploadersConfigPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseCustomUploadersConfigPath.Location = new System.Drawing.Point(515, 56);
            this.btnBrowseCustomUploadersConfigPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseCustomUploadersConfigPath.Name = "btnBrowseCustomUploadersConfigPath";
            this.btnBrowseCustomUploadersConfigPath.Size = new System.Drawing.Size(76, 27);
            this.btnBrowseCustomUploadersConfigPath.TabIndex = 3;
            this.btnBrowseCustomUploadersConfigPath.Text = "Browse...";
            this.btnBrowseCustomUploadersConfigPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomUploadersConfigPath.Click += new System.EventHandler(this.btnBrowseCustomUploadersConfigPath_Click);
            // 
            // txtCustomUploadersConfigPath
            // 
            this.txtCustomUploadersConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomUploadersConfigPath.Location = new System.Drawing.Point(21, 59);
            this.txtCustomUploadersConfigPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustomUploadersConfigPath.Name = "txtCustomUploadersConfigPath";
            this.txtCustomUploadersConfigPath.Size = new System.Drawing.Size(458, 22);
            this.txtCustomUploadersConfigPath.TabIndex = 2;
            this.txtCustomUploadersConfigPath.TextChanged += new System.EventHandler(this.txtCustomUploadersConfigPath_TextChanged);
            // 
            // cbUseCustomUploadersConfigPath
            // 
            this.cbUseCustomUploadersConfigPath.AutoSize = true;
            this.cbUseCustomUploadersConfigPath.Location = new System.Drawing.Point(21, 30);
            this.cbUseCustomUploadersConfigPath.Margin = new System.Windows.Forms.Padding(4);
            this.cbUseCustomUploadersConfigPath.Name = "cbUseCustomUploadersConfigPath";
            this.cbUseCustomUploadersConfigPath.Size = new System.Drawing.Size(271, 21);
            this.cbUseCustomUploadersConfigPath.TabIndex = 1;
            this.cbUseCustomUploadersConfigPath.Text = "Use custom uploaders config file path:";
            this.cbUseCustomUploadersConfigPath.UseVisualStyleBackColor = true;
            this.cbUseCustomUploadersConfigPath.CheckedChanged += new System.EventHandler(this.cbUseCustomUploadersConfigPath_CheckedChanged);
            // 
            // gbHistory
            // 
            this.gbHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHistory.Controls.Add(this.nudHistoryMaxItemCount);
            this.gbHistory.Controls.Add(this.lblHistoryMaxItemCount);
            this.gbHistory.Controls.Add(this.btnBrowseCustomHistoryPath);
            this.gbHistory.Controls.Add(this.txtCustomHistoryPath);
            this.gbHistory.Controls.Add(this.cbUseCustomHistoryPath);
            this.gbHistory.Controls.Add(this.cbHistorySave);
            this.gbHistory.Location = new System.Drawing.Point(11, 266);
            this.gbHistory.Margin = new System.Windows.Forms.Padding(4);
            this.gbHistory.Name = "gbHistory";
            this.gbHistory.Padding = new System.Windows.Forms.Padding(4);
            this.gbHistory.Size = new System.Drawing.Size(619, 167);
            this.gbHistory.TabIndex = 2;
            this.gbHistory.TabStop = false;
            this.gbHistory.Text = "History";
            // 
            // nudHistoryMaxItemCount
            // 
            this.nudHistoryMaxItemCount.Location = new System.Drawing.Point(288, 123);
            this.nudHistoryMaxItemCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudHistoryMaxItemCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudHistoryMaxItemCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudHistoryMaxItemCount.Name = "nudHistoryMaxItemCount";
            this.nudHistoryMaxItemCount.Size = new System.Drawing.Size(107, 22);
            this.nudHistoryMaxItemCount.TabIndex = 5;
            this.nudHistoryMaxItemCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHistoryMaxItemCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudHistoryMaxItemCount.ValueChanged += new System.EventHandler(this.nudHistoryMaxItemCount_ValueChanged);
            // 
            // lblHistoryMaxItemCount
            // 
            this.lblHistoryMaxItemCount.AutoSize = true;
            this.lblHistoryMaxItemCount.Location = new System.Drawing.Point(21, 128);
            this.lblHistoryMaxItemCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryMaxItemCount.Name = "lblHistoryMaxItemCount";
            this.lblHistoryMaxItemCount.Size = new System.Drawing.Size(260, 17);
            this.lblHistoryMaxItemCount.TabIndex = 4;
            this.lblHistoryMaxItemCount.Text = "Max item count for filtering (-1 disables):";
            // 
            // btnBrowseCustomHistoryPath
            // 
            this.btnBrowseCustomHistoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseCustomHistoryPath.AutoSize = true;
            this.btnBrowseCustomHistoryPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseCustomHistoryPath.Location = new System.Drawing.Point(517, 89);
            this.btnBrowseCustomHistoryPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseCustomHistoryPath.Name = "btnBrowseCustomHistoryPath";
            this.btnBrowseCustomHistoryPath.Size = new System.Drawing.Size(76, 27);
            this.btnBrowseCustomHistoryPath.TabIndex = 3;
            this.btnBrowseCustomHistoryPath.Text = "Browse...";
            this.btnBrowseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomHistoryPath.Click += new System.EventHandler(this.btnBrowseCustomHistoryPath_Click);
            // 
            // txtCustomHistoryPath
            // 
            this.txtCustomHistoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomHistoryPath.Location = new System.Drawing.Point(21, 89);
            this.txtCustomHistoryPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustomHistoryPath.Name = "txtCustomHistoryPath";
            this.txtCustomHistoryPath.Size = new System.Drawing.Size(458, 22);
            this.txtCustomHistoryPath.TabIndex = 2;
            this.txtCustomHistoryPath.TextChanged += new System.EventHandler(this.txtCustomHistoryPath_TextChanged);
            // 
            // cbUseCustomHistoryPath
            // 
            this.cbUseCustomHistoryPath.AutoSize = true;
            this.cbUseCustomHistoryPath.Location = new System.Drawing.Point(21, 59);
            this.cbUseCustomHistoryPath.Margin = new System.Windows.Forms.Padding(4);
            this.cbUseCustomHistoryPath.Name = "cbUseCustomHistoryPath";
            this.cbUseCustomHistoryPath.Size = new System.Drawing.Size(208, 21);
            this.cbUseCustomHistoryPath.TabIndex = 1;
            this.cbUseCustomHistoryPath.Text = "Use custom history file path:";
            this.cbUseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.cbUseCustomHistoryPath.CheckedChanged += new System.EventHandler(this.cbUseCustomHistoryPath_CheckedChanged);
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(21, 30);
            this.cbHistorySave.Margin = new System.Windows.Forms.Padding(4);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(154, 21);
            this.cbHistorySave.TabIndex = 0;
            this.cbHistorySave.Text = "Enable history save";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // tpSync
            // 
            this.tpSync.Controls.Add(this.panelSync);
            this.tpSync.Location = new System.Drawing.Point(4, 25);
            this.tpSync.Margin = new System.Windows.Forms.Padding(4);
            this.tpSync.Name = "tpSync";
            this.tpSync.Padding = new System.Windows.Forms.Padding(4);
            this.tpSync.Size = new System.Drawing.Size(727, 593);
            this.tpSync.TabIndex = 10;
            this.tpSync.Text = "Sync";
            this.tpSync.UseVisualStyleBackColor = true;
            // 
            // panelSync
            // 
            this.panelSync.Controls.Add(this.btnDropboxSyncExport);
            this.panelSync.Controls.Add(this.btnDropboxSyncImport);
            this.panelSync.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSync.Location = new System.Drawing.Point(4, 4);
            this.panelSync.Margin = new System.Windows.Forms.Padding(4);
            this.panelSync.Name = "panelSync";
            this.panelSync.Size = new System.Drawing.Size(719, 585);
            this.panelSync.TabIndex = 0;
            // 
            // btnDropboxSyncExport
            // 
            this.btnDropboxSyncExport.Location = new System.Drawing.Point(11, 49);
            this.btnDropboxSyncExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnDropboxSyncExport.Name = "btnDropboxSyncExport";
            this.btnDropboxSyncExport.Size = new System.Drawing.Size(245, 27);
            this.btnDropboxSyncExport.TabIndex = 1;
            this.btnDropboxSyncExport.Text = "&Export Settings to Dropbox";
            this.btnDropboxSyncExport.UseVisualStyleBackColor = true;
            this.btnDropboxSyncExport.Click += new System.EventHandler(this.btnDropboxSyncExport_Click);
            // 
            // btnDropboxSyncImport
            // 
            this.btnDropboxSyncImport.Location = new System.Drawing.Point(11, 10);
            this.btnDropboxSyncImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnDropboxSyncImport.Name = "btnDropboxSyncImport";
            this.btnDropboxSyncImport.Size = new System.Drawing.Size(245, 27);
            this.btnDropboxSyncImport.TabIndex = 0;
            this.btnDropboxSyncImport.Text = "&Import Settings from Dropbox";
            this.btnDropboxSyncImport.UseVisualStyleBackColor = true;
            this.btnDropboxSyncImport.Click += new System.EventHandler(this.btnDropboxSyncImport_Click);
            // 
            // tpFileNaming
            // 
            this.tpFileNaming.Controls.Add(this.panelFileNaming);
            this.tpFileNaming.Location = new System.Drawing.Point(4, 25);
            this.tpFileNaming.Margin = new System.Windows.Forms.Padding(4);
            this.tpFileNaming.Name = "tpFileNaming";
            this.tpFileNaming.Padding = new System.Windows.Forms.Padding(4);
            this.tpFileNaming.Size = new System.Drawing.Size(727, 593);
            this.tpFileNaming.TabIndex = 11;
            this.tpFileNaming.Text = "File Naming";
            this.tpFileNaming.UseVisualStyleBackColor = true;
            // 
            // panelFileNaming
            // 
            this.panelFileNaming.Controls.Add(this.gbFilenamingPatternOthers);
            this.panelFileNaming.Controls.Add(this.gbFilenamingPatternImages);
            this.panelFileNaming.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFileNaming.Location = new System.Drawing.Point(4, 4);
            this.panelFileNaming.Margin = new System.Windows.Forms.Padding(4);
            this.panelFileNaming.Name = "panelFileNaming";
            this.panelFileNaming.Size = new System.Drawing.Size(719, 585);
            this.panelFileNaming.TabIndex = 0;
            // 
            // gbFilenamingPatternOthers
            // 
            this.gbFilenamingPatternOthers.Controls.Add(this.txtNameFormatPatternOther);
            this.gbFilenamingPatternOthers.Controls.Add(this.lblNameFormatPatternPreviewOther);
            this.gbFilenamingPatternOthers.Controls.Add(this.btnNameFormatPatternHelpOther);
            this.gbFilenamingPatternOthers.Location = new System.Drawing.Point(11, 128);
            this.gbFilenamingPatternOthers.Margin = new System.Windows.Forms.Padding(4);
            this.gbFilenamingPatternOthers.Name = "gbFilenamingPatternOthers";
            this.gbFilenamingPatternOthers.Padding = new System.Windows.Forms.Padding(4);
            this.gbFilenamingPatternOthers.Size = new System.Drawing.Size(679, 98);
            this.gbFilenamingPatternOthers.TabIndex = 1;
            this.gbFilenamingPatternOthers.TabStop = false;
            this.gbFilenamingPatternOthers.Text = "File naming pattern for other files that do not already exist in the computer:";
            // 
            // txtNameFormatPatternOther
            // 
            this.txtNameFormatPatternOther.Location = new System.Drawing.Point(21, 30);
            this.txtNameFormatPatternOther.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameFormatPatternOther.Name = "txtNameFormatPatternOther";
            this.txtNameFormatPatternOther.Size = new System.Drawing.Size(553, 22);
            this.txtNameFormatPatternOther.TabIndex = 0;
            this.txtNameFormatPatternOther.TextChanged += new System.EventHandler(this.txtNameFormatPatternOther_TextChanged);
            // 
            // lblNameFormatPatternPreviewOther
            // 
            this.lblNameFormatPatternPreviewOther.AutoSize = true;
            this.lblNameFormatPatternPreviewOther.Location = new System.Drawing.Point(21, 69);
            this.lblNameFormatPatternPreviewOther.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNameFormatPatternPreviewOther.Name = "lblNameFormatPatternPreviewOther";
            this.lblNameFormatPatternPreviewOther.Size = new System.Drawing.Size(61, 17);
            this.lblNameFormatPatternPreviewOther.TabIndex = 2;
            this.lblNameFormatPatternPreviewOther.Text = "Preview:";
            // 
            // btnNameFormatPatternHelpOther
            // 
            this.btnNameFormatPatternHelpOther.Location = new System.Drawing.Point(587, 30);
            this.btnNameFormatPatternHelpOther.Margin = new System.Windows.Forms.Padding(4);
            this.btnNameFormatPatternHelpOther.Name = "btnNameFormatPatternHelpOther";
            this.btnNameFormatPatternHelpOther.Size = new System.Drawing.Size(32, 28);
            this.btnNameFormatPatternHelpOther.TabIndex = 1;
            this.btnNameFormatPatternHelpOther.Text = "?";
            this.btnNameFormatPatternHelpOther.UseVisualStyleBackColor = true;
            this.btnNameFormatPatternHelpOther.Click += new System.EventHandler(this.btnNameFormatPatternHelpOther_Click);
            // 
            // gbFilenamingPatternImages
            // 
            this.gbFilenamingPatternImages.Controls.Add(this.lblNameFormatPatternPreviewImages);
            this.gbFilenamingPatternImages.Controls.Add(this.txtNameFormatPatternImages);
            this.gbFilenamingPatternImages.Controls.Add(this.btnNameFormatPatternHelpImages);
            this.gbFilenamingPatternImages.Location = new System.Drawing.Point(11, 10);
            this.gbFilenamingPatternImages.Margin = new System.Windows.Forms.Padding(4);
            this.gbFilenamingPatternImages.Name = "gbFilenamingPatternImages";
            this.gbFilenamingPatternImages.Padding = new System.Windows.Forms.Padding(4);
            this.gbFilenamingPatternImages.Size = new System.Drawing.Size(679, 98);
            this.gbFilenamingPatternImages.TabIndex = 0;
            this.gbFilenamingPatternImages.TabStop = false;
            this.gbFilenamingPatternImages.Text = "File naming pattern for image files that do not already exist in the computer:";
            // 
            // lblNameFormatPatternPreviewImages
            // 
            this.lblNameFormatPatternPreviewImages.AutoSize = true;
            this.lblNameFormatPatternPreviewImages.Location = new System.Drawing.Point(21, 69);
            this.lblNameFormatPatternPreviewImages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNameFormatPatternPreviewImages.Name = "lblNameFormatPatternPreviewImages";
            this.lblNameFormatPatternPreviewImages.Size = new System.Drawing.Size(61, 17);
            this.lblNameFormatPatternPreviewImages.TabIndex = 2;
            this.lblNameFormatPatternPreviewImages.Text = "Preview:";
            // 
            // txtNameFormatPatternImages
            // 
            this.txtNameFormatPatternImages.Location = new System.Drawing.Point(21, 30);
            this.txtNameFormatPatternImages.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameFormatPatternImages.Name = "txtNameFormatPatternImages";
            this.txtNameFormatPatternImages.Size = new System.Drawing.Size(553, 22);
            this.txtNameFormatPatternImages.TabIndex = 0;
            this.txtNameFormatPatternImages.TextChanged += new System.EventHandler(this.txtNameFormatPattern_TextChanged);
            // 
            // btnNameFormatPatternHelpImages
            // 
            this.btnNameFormatPatternHelpImages.Location = new System.Drawing.Point(587, 30);
            this.btnNameFormatPatternHelpImages.Margin = new System.Windows.Forms.Padding(4);
            this.btnNameFormatPatternHelpImages.Name = "btnNameFormatPatternHelpImages";
            this.btnNameFormatPatternHelpImages.Size = new System.Drawing.Size(32, 28);
            this.btnNameFormatPatternHelpImages.TabIndex = 1;
            this.btnNameFormatPatternHelpImages.Text = "?";
            this.btnNameFormatPatternHelpImages.UseVisualStyleBackColor = true;
            this.btnNameFormatPatternHelpImages.Click += new System.EventHandler(this.btnNameFormatPatternHelp_Click);
            // 
            // tpShapes2
            // 
            this.tpShapes2.Controls.Add(this.panelShapes2);
            this.tpShapes2.Location = new System.Drawing.Point(4, 25);
            this.tpShapes2.Margin = new System.Windows.Forms.Padding(4);
            this.tpShapes2.Name = "tpShapes2";
            this.tpShapes2.Padding = new System.Windows.Forms.Padding(4);
            this.tpShapes2.Size = new System.Drawing.Size(727, 593);
            this.tpShapes2.TabIndex = 13;
            this.tpShapes2.Text = "Shapes";
            this.tpShapes2.UseVisualStyleBackColor = true;
            // 
            // panelShapes2
            // 
            this.panelShapes2.Controls.Add(this.pgShapes);
            this.panelShapes2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShapes2.Location = new System.Drawing.Point(4, 4);
            this.panelShapes2.Margin = new System.Windows.Forms.Padding(4);
            this.panelShapes2.Name = "panelShapes2";
            this.panelShapes2.Size = new System.Drawing.Size(719, 585);
            this.panelShapes2.TabIndex = 0;
            // 
            // pgShapes
            // 
            this.pgShapes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgShapes.Location = new System.Drawing.Point(0, 0);
            this.pgShapes.Margin = new System.Windows.Forms.Padding(4);
            this.pgShapes.Name = "pgShapes";
            this.pgShapes.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgShapes.Size = new System.Drawing.Size(719, 585);
            this.pgShapes.TabIndex = 0;
            this.pgShapes.ToolbarVisible = false;
            // 
            // tpUserConfig
            // 
            this.tpUserConfig.Controls.Add(this.panelUserConfig);
            this.tpUserConfig.Location = new System.Drawing.Point(4, 25);
            this.tpUserConfig.Margin = new System.Windows.Forms.Padding(4);
            this.tpUserConfig.Name = "tpUserConfig";
            this.tpUserConfig.Padding = new System.Windows.Forms.Padding(4);
            this.tpUserConfig.Size = new System.Drawing.Size(727, 593);
            this.tpUserConfig.TabIndex = 14;
            this.tpUserConfig.Text = "User Config";
            this.tpUserConfig.UseVisualStyleBackColor = true;
            // 
            // panelUserConfig
            // 
            this.panelUserConfig.Controls.Add(this.pgUserConfig);
            this.panelUserConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserConfig.Location = new System.Drawing.Point(4, 4);
            this.panelUserConfig.Margin = new System.Windows.Forms.Padding(4);
            this.panelUserConfig.Name = "panelUserConfig";
            this.panelUserConfig.Size = new System.Drawing.Size(719, 585);
            this.panelUserConfig.TabIndex = 0;
            // 
            // pgUserConfig
            // 
            this.pgUserConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgUserConfig.Location = new System.Drawing.Point(0, 0);
            this.pgUserConfig.Margin = new System.Windows.Forms.Padding(4);
            this.pgUserConfig.Name = "pgUserConfig";
            this.pgUserConfig.Size = new System.Drawing.Size(719, 585);
            this.pgUserConfig.TabIndex = 0;
            this.pgUserConfig.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgUserConfig_PropertyValueChanged);
            // 
            // tpImageProcessing
            // 
            this.tpImageProcessing.Controls.Add(this.panelImageProcessing);
            this.tpImageProcessing.Location = new System.Drawing.Point(4, 25);
            this.tpImageProcessing.Name = "tpImageProcessing";
            this.tpImageProcessing.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageProcessing.Size = new System.Drawing.Size(727, 593);
            this.tpImageProcessing.TabIndex = 15;
            this.tpImageProcessing.Text = "Image Processing";
            this.tpImageProcessing.UseVisualStyleBackColor = true;
            // 
            // panelImageProcessing
            // 
            this.panelImageProcessing.Controls.Add(this.chkFileUploadImageProcess);
            this.panelImageProcessing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImageProcessing.Location = new System.Drawing.Point(3, 3);
            this.panelImageProcessing.Name = "panelImageProcessing";
            this.panelImageProcessing.Size = new System.Drawing.Size(721, 587);
            this.panelImageProcessing.TabIndex = 1;
            // 
            // chkFileUploadImageProcess
            // 
            this.chkFileUploadImageProcess.AutoSize = true;
            this.chkFileUploadImageProcess.Location = new System.Drawing.Point(8, 8);
            this.chkFileUploadImageProcess.Name = "chkFileUploadImageProcess";
            this.chkFileUploadImageProcess.Size = new System.Drawing.Size(458, 21);
            this.chkFileUploadImageProcess.TabIndex = 0;
            this.chkFileUploadImageProcess.Text = "Process image files during File Upload or Drag n Drop from Explorer";
            this.chkFileUploadImageProcess.UseVisualStyleBackColor = true;
            this.chkFileUploadImageProcess.CheckedChanged += new System.EventHandler(this.chkFileUploadImageProcess_CheckedChanged);
            // 
            // OptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 697);
            this.Controls.Add(this.tlpMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(954, 728);
            this.Name = "OptionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OptionsWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsWindow_FormClosed);
            this.Shown += new System.EventHandler(this.OptionsWindow_Shown);
            this.Resize += new System.EventHandler(this.OptionsWindow_Resize);
            this.tlpMain.ResumeLayout(false);
            this.tcBase.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.panelGeneral.ResumeLayout(false);
            this.panelGeneral.PerformLayout();
            this.gbNotifications.ResumeLayout(false);
            this.gbNotifications.PerformLayout();
            this.tpAdvanced.ResumeLayout(false);
            this.panelAdvanced.ResumeLayout(false);
            this.tpCapture.ResumeLayout(false);
            this.panelCapture.ResumeLayout(false);
            this.panelCapture.PerformLayout();
            this.gbCaptureAfter.ResumeLayout(false);
            this.tpShapes.ResumeLayout(false);
            this.panelShapes.ResumeLayout(false);
            this.panelShapes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFixedShapeSizeWidth)).EndInit();
            this.tpProxy.ResumeLayout(false);
            this.panelProxy.ResumeLayout(false);
            this.tpClipboardUpload.ResumeLayout(false);
            this.panelClipboardUpload.ResumeLayout(false);
            this.panelClipboardUpload.PerformLayout();
            this.tpUpload.ResumeLayout(false);
            this.panelUpload.ResumeLayout(false);
            this.panelUpload.PerformLayout();
            this.gbAfterUpload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).EndInit();
            this.tpImageResize.ResumeLayout(false);
            this.panelImageResize.ResumeLayout(false);
            this.tpImageQuality2.ResumeLayout(false);
            this.panelImageQuality.ResumeLayout(false);
            this.tpPaths.ResumeLayout(false);
            this.panelPaths.ResumeLayout(false);
            this.gbPathRoot.ResumeLayout(false);
            this.gbPathRoot.PerformLayout();
            this.gbScreenshots.ResumeLayout(false);
            this.gbScreenshots.PerformLayout();
            this.gbUploadersConfig.ResumeLayout(false);
            this.gbUploadersConfig.PerformLayout();
            this.gbHistory.ResumeLayout(false);
            this.gbHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItemCount)).EndInit();
            this.tpSync.ResumeLayout(false);
            this.panelSync.ResumeLayout(false);
            this.tpFileNaming.ResumeLayout(false);
            this.panelFileNaming.ResumeLayout(false);
            this.gbFilenamingPatternOthers.ResumeLayout(false);
            this.gbFilenamingPatternOthers.PerformLayout();
            this.gbFilenamingPatternImages.ResumeLayout(false);
            this.gbFilenamingPatternImages.PerformLayout();
            this.tpShapes2.ResumeLayout(false);
            this.panelShapes2.ResumeLayout(false);
            this.tpUserConfig.ResumeLayout(false);
            this.panelUserConfig.ResumeLayout(false);
            this.tpImageProcessing.ResumeLayout(false);
            this.panelImageProcessing.ResumeLayout(false);
            this.panelImageProcessing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Panel panelCapture;
        private System.Windows.Forms.Panel panelShapes;
        private System.Windows.Forms.Panel panelAdvanced;
        private System.Windows.Forms.Panel panelProxy;
        private System.Windows.Forms.Panel panelClipboardUpload;
        private System.Windows.Forms.CheckBox cbCheckUpdates;
        private System.Windows.Forms.CheckBox cbStartWithWindows;
        private System.Windows.Forms.CheckBox cbShowTray;
        private System.Windows.Forms.CheckBox cbShellContextMenu;
        private System.Windows.Forms.CheckBox cbCaptureShadow;
        private System.Windows.Forms.CheckBox cbShowCursor;
        private System.Windows.Forms.CheckBox cbCaptureTransparent;
        private System.Windows.Forms.Label lblGeneralInfo;
        private System.Windows.Forms.PropertyGrid pgSettings;
        private System.Windows.Forms.TabControl tcBase;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpAdvanced;
        private System.Windows.Forms.TabPage tpCapture;
        private System.Windows.Forms.TabPage tpShapes;
        private System.Windows.Forms.TabPage tpProxy;
        private System.Windows.Forms.TabPage tpClipboardUpload;
        private System.Windows.Forms.TabPage tpUpload;
        private System.Windows.Forms.Panel panelUpload;
        private System.Windows.Forms.Button btnAutofillProxy;
        private System.Windows.Forms.PropertyGrid pgProxy;
        private System.Windows.Forms.GroupBox gbCaptureAfter;
        private System.Windows.Forms.TextBox txtSaveImageSubFolderPatternPreview;
        private System.Windows.Forms.Button btnBrowseScreenshotsDir;
        private System.Windows.Forms.TextBox txtScreenshotsPath;
        private System.Windows.Forms.TextBox txtSaveImageSubFolderPattern;
        private System.Windows.Forms.Label lblSaveImageSubFolderPattern;
        private System.Windows.Forms.CheckBox cbShapeForceWindowCapture;
        private System.Windows.Forms.CheckBox cbShapeIncludeControls;
        private System.Windows.Forms.Label lblFixedShapeSizeHeight;
        private System.Windows.Forms.CheckBox cbDrawBorder;
        private System.Windows.Forms.Label lblFixedShapeSizeWidth;
        private System.Windows.Forms.CheckBox cbQuickCrop;
        private System.Windows.Forms.NumericUpDown nudFixedShapeSizeHeight;
        private System.Windows.Forms.CheckBox cbDrawCheckerboard;
        private System.Windows.Forms.NumericUpDown nudFixedShapeSizeWidth;
        private System.Windows.Forms.CheckBox cbFixedShapeSize;
        private System.Windows.Forms.CheckBox cbClipboardUploadAutoDetectURL;
        private System.Windows.Forms.Label lblClipboardUploadInfo;
        private System.Windows.Forms.Label lblNameFormatPatternPreviewOther;
        private System.Windows.Forms.Button btnNameFormatPatternHelpImages;
        private System.Windows.Forms.TextBox txtNameFormatPatternImages;
        private System.Windows.Forms.TabPage tpImageResize;
        private System.Windows.Forms.Panel panelImageResize;
        private System.Windows.Forms.TabPage tpImageQuality2;
        private System.Windows.Forms.Panel panelImageQuality;
        private System.Windows.Forms.TabPage tpPaths;
        private System.Windows.Forms.Panel panelPaths;
        private System.Windows.Forms.GroupBox gbHistory;
        private System.Windows.Forms.NumericUpDown nudHistoryMaxItemCount;
        private System.Windows.Forms.Label lblHistoryMaxItemCount;
        private System.Windows.Forms.Button btnBrowseCustomHistoryPath;
        private System.Windows.Forms.TextBox txtCustomHistoryPath;
        private System.Windows.Forms.CheckBox cbUseCustomHistoryPath;
        private System.Windows.Forms.CheckBox cbHistorySave;
        private System.Windows.Forms.Label lblUploadLimitHint;
        private System.Windows.Forms.NumericUpDown nudUploadLimit;
        private System.Windows.Forms.Label lblUploadLimit;
        private System.Windows.Forms.Label lblBufferSize;
        private System.Windows.Forms.Label lblBufferSizeInfo;
        private System.Windows.Forms.ComboBox cbBufferSize;
        private System.Windows.Forms.GroupBox gbUploadersConfig;
        private System.Windows.Forms.Button btnLoadUploadersConfig;
        private System.Windows.Forms.Button btnBrowseCustomUploadersConfigPath;
        private System.Windows.Forms.TextBox txtCustomUploadersConfigPath;
        private System.Windows.Forms.CheckBox cbUseCustomUploadersConfigPath;
        private System.Windows.Forms.Label lblOpenZUploaderPath;
        private System.Windows.Forms.Button btnOpenZUploaderPath;
        private System.Windows.Forms.CheckBox cbPlaySoundAfterCapture;
        private System.Windows.Forms.TabPage tpSync;
        private System.Windows.Forms.Panel panelSync;
        private System.Windows.Forms.GroupBox gbNotifications;
        private System.Windows.Forms.GroupBox gbPathRoot;
        private System.Windows.Forms.GroupBox gbScreenshots;
        private System.Windows.Forms.CheckBox chkPlaySoundAfterUpload;
        private System.Windows.Forms.GroupBox gbAfterUpload;
        private System.Windows.Forms.Button btnDropboxSyncExport;
        private System.Windows.Forms.Button btnDropboxSyncImport;
        private System.Windows.Forms.GroupBox gbFilenamingPatternImages;
        private System.Windows.Forms.TabPage tpFileNaming;
        private System.Windows.Forms.Panel panelFileNaming;
        private System.Windows.Forms.GroupBox gbFilenamingPatternOthers;
        private System.Windows.Forms.TextBox txtNameFormatPatternOther;
        private System.Windows.Forms.Button btnNameFormatPatternHelpOther;
        private System.Windows.Forms.Label lblNameFormatPatternPreviewImages;
        private System.Windows.Forms.Button btnImagesOrganise;
        private System.Windows.Forms.CheckBox chkShowBalloonAfterUpload;
        private System.Windows.Forms.TabPage tpShapes2;
        private System.Windows.Forms.Panel panelShapes2;
        private System.Windows.Forms.PropertyGrid pgShapes;
        private System.Windows.Forms.TabPage tpUserConfig;
        private System.Windows.Forms.Panel panelUserConfig;
        private System.Windows.Forms.PropertyGrid pgUserConfig;
        private AfterCaptureTasksUI ucAfterCaptureTasks;
        private AfterUploadTasksUI ucAfterUploadTasks;
        private ImageResizeUI ucImageResizeUI;
        private ImageQualityUI ucImageQualityUI;
        private System.Windows.Forms.TabPage tpImageProcessing;
        private System.Windows.Forms.CheckBox chkFileUploadImageProcess;
        private System.Windows.Forms.Panel panelImageProcessing;
    }
}