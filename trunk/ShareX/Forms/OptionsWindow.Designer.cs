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
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Uploader config");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("User config");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Sync");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Advanced", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15});
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
            this.tpUploaderConfig = new System.Windows.Forms.TabPage();
            this.panelUploaderConfig = new System.Windows.Forms.Panel();
            this.pgUploaderConfig = new System.Windows.Forms.PropertyGrid();
            this.tpShapes2 = new System.Windows.Forms.TabPage();
            this.panelShapes2 = new System.Windows.Forms.Panel();
            this.pgShapes = new System.Windows.Forms.PropertyGrid();
            this.tpUserConfig = new System.Windows.Forms.TabPage();
            this.panelUserConfig = new System.Windows.Forms.Panel();
            this.pgUserConfig = new System.Windows.Forms.PropertyGrid();
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
            this.tpUploaderConfig.SuspendLayout();
            this.panelUploaderConfig.SuspendLayout();
            this.tpShapes2.SuspendLayout();
            this.panelShapes2.SuspendLayout();
            this.tpUserConfig.SuspendLayout();
            this.panelUserConfig.SuspendLayout();
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
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(696, 512);
            this.tlpMain.TabIndex = 0;
            // 
            // tvMain
            // 
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.Location = new System.Drawing.Point(3, 3);
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
            treeNode8.Name = "tnImage";
            treeNode8.Text = "Image Processing";
            treeNode9.Name = "tnUpload";
            treeNode9.Text = "Upload";
            treeNode10.Name = "tnPaths";
            treeNode10.Text = "Paths";
            treeNode11.Name = "tnProxy";
            treeNode11.Text = "Proxy";
            treeNode12.Name = "tnShapes2";
            treeNode12.Text = "Shapes";
            treeNode13.Name = "tnUploaderConfig";
            treeNode13.Text = "Uploader config";
            treeNode14.Name = "tnUserConfig";
            treeNode14.Text = "User config";
            treeNode15.Name = "tnSync";
            treeNode15.Text = "Sync";
            treeNode16.Name = "tnAdvanced";
            treeNode16.Text = "Advanced";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode4,
            treeNode5,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode16});
            this.tvMain.Size = new System.Drawing.Size(133, 506);
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
            this.tcBase.Controls.Add(this.tpUploaderConfig);
            this.tcBase.Controls.Add(this.tpShapes2);
            this.tcBase.Controls.Add(this.tpUserConfig);
            this.tcBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcBase.Location = new System.Drawing.Point(142, 3);
            this.tcBase.Name = "tcBase";
            this.tcBase.SelectedIndex = 0;
            this.tcBase.Size = new System.Drawing.Size(551, 506);
            this.tcBase.TabIndex = 1;
            this.tcBase.Visible = false;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.panelGeneral);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Size = new System.Drawing.Size(543, 480);
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
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(543, 480);
            this.panelGeneral.TabIndex = 0;
            // 
            // gbNotifications
            // 
            this.gbNotifications.Controls.Add(this.chkShowBalloonAfterUpload);
            this.gbNotifications.Controls.Add(this.chkPlaySoundAfterUpload);
            this.gbNotifications.Controls.Add(this.cbPlaySoundAfterCapture);
            this.gbNotifications.Location = new System.Drawing.Point(16, 136);
            this.gbNotifications.Name = "gbNotifications";
            this.gbNotifications.Size = new System.Drawing.Size(512, 105);
            this.gbNotifications.TabIndex = 4;
            this.gbNotifications.TabStop = false;
            this.gbNotifications.Text = "Notifications";
            // 
            // chkShowBalloonAfterUpload
            // 
            this.chkShowBalloonAfterUpload.AutoSize = true;
            this.chkShowBalloonAfterUpload.Location = new System.Drawing.Point(16, 71);
            this.chkShowBalloonAfterUpload.Name = "chkShowBalloonAfterUpload";
            this.chkShowBalloonAfterUpload.Size = new System.Drawing.Size(211, 17);
            this.chkShowBalloonAfterUpload.TabIndex = 2;
            this.chkShowBalloonAfterUpload.Text = "Show balloon after upload is completed";
            this.chkShowBalloonAfterUpload.UseVisualStyleBackColor = true;
            this.chkShowBalloonAfterUpload.CheckedChanged += new System.EventHandler(this.chkShowBalloon_CheckedChanged);
            // 
            // chkPlaySoundAfterUpload
            // 
            this.chkPlaySoundAfterUpload.AutoSize = true;
            this.chkPlaySoundAfterUpload.Location = new System.Drawing.Point(16, 48);
            this.chkPlaySoundAfterUpload.Name = "chkPlaySoundAfterUpload";
            this.chkPlaySoundAfterUpload.Size = new System.Drawing.Size(199, 17);
            this.chkPlaySoundAfterUpload.TabIndex = 1;
            this.chkPlaySoundAfterUpload.Text = "Play sound after upload is completed";
            this.chkPlaySoundAfterUpload.UseVisualStyleBackColor = true;
            this.chkPlaySoundAfterUpload.CheckedChanged += new System.EventHandler(this.chkPlaySoundAfterUpload_CheckedChanged);
            // 
            // cbPlaySoundAfterCapture
            // 
            this.cbPlaySoundAfterCapture.AutoSize = true;
            this.cbPlaySoundAfterCapture.Location = new System.Drawing.Point(16, 24);
            this.cbPlaySoundAfterCapture.Name = "cbPlaySoundAfterCapture";
            this.cbPlaySoundAfterCapture.Size = new System.Drawing.Size(180, 17);
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
            this.lblGeneralInfo.Location = new System.Drawing.Point(0, 451);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(543, 29);
            this.lblGeneralInfo.TabIndex = 5;
            this.lblGeneralInfo.Text = "Shell context menu is Windows Explorer right click menu for files and folders.";
            this.lblGeneralInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbCheckUpdates
            // 
            this.cbCheckUpdates.AutoSize = true;
            this.cbCheckUpdates.Location = new System.Drawing.Point(16, 88);
            this.cbCheckUpdates.Name = "cbCheckUpdates";
            this.cbCheckUpdates.Size = new System.Drawing.Size(209, 17);
            this.cbCheckUpdates.TabIndex = 3;
            this.cbCheckUpdates.Text = "Automatically check updates at startup";
            this.cbCheckUpdates.UseVisualStyleBackColor = true;
            this.cbCheckUpdates.CheckedChanged += new System.EventHandler(this.cbCheckUpdates_CheckedChanged);
            // 
            // cbStartWithWindows
            // 
            this.cbStartWithWindows.AutoSize = true;
            this.cbStartWithWindows.Location = new System.Drawing.Point(16, 40);
            this.cbStartWithWindows.Name = "cbStartWithWindows";
            this.cbStartWithWindows.Size = new System.Drawing.Size(155, 17);
            this.cbStartWithWindows.TabIndex = 1;
            this.cbStartWithWindows.Text = "Start ShareX with Windows";
            this.cbStartWithWindows.UseVisualStyleBackColor = true;
            this.cbStartWithWindows.CheckedChanged += new System.EventHandler(this.cbStartWithWindows_CheckedChanged);
            // 
            // cbShowTray
            // 
            this.cbShowTray.AutoSize = true;
            this.cbShowTray.Location = new System.Drawing.Point(16, 16);
            this.cbShowTray.Name = "cbShowTray";
            this.cbShowTray.Size = new System.Drawing.Size(134, 17);
            this.cbShowTray.TabIndex = 0;
            this.cbShowTray.Text = "Show ShareX tray icon";
            this.cbShowTray.UseVisualStyleBackColor = true;
            this.cbShowTray.CheckedChanged += new System.EventHandler(this.cbShowTray_CheckedChanged);
            // 
            // cbShellContextMenu
            // 
            this.cbShellContextMenu.AutoSize = true;
            this.cbShellContextMenu.Location = new System.Drawing.Point(16, 64);
            this.cbShellContextMenu.Name = "cbShellContextMenu";
            this.cbShellContextMenu.Size = new System.Drawing.Size(211, 17);
            this.cbShellContextMenu.TabIndex = 2;
            this.cbShellContextMenu.Text = "Show in \"Send To\" Shell context menu";
            this.cbShellContextMenu.UseVisualStyleBackColor = true;
            this.cbShellContextMenu.CheckedChanged += new System.EventHandler(this.cbShellContextMenu_CheckedChanged);
            // 
            // tpAdvanced
            // 
            this.tpAdvanced.Controls.Add(this.panelAdvanced);
            this.tpAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tpAdvanced.Name = "tpAdvanced";
            this.tpAdvanced.Size = new System.Drawing.Size(543, 480);
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
            this.panelAdvanced.Name = "panelAdvanced";
            this.panelAdvanced.Size = new System.Drawing.Size(543, 480);
            this.panelAdvanced.TabIndex = 0;
            // 
            // pgSettings
            // 
            this.pgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSettings.Location = new System.Drawing.Point(0, 0);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.Size = new System.Drawing.Size(543, 480);
            this.pgSettings.TabIndex = 0;
            // 
            // tpCapture
            // 
            this.tpCapture.Controls.Add(this.panelCapture);
            this.tpCapture.Location = new System.Drawing.Point(4, 22);
            this.tpCapture.Name = "tpCapture";
            this.tpCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tpCapture.Size = new System.Drawing.Size(543, 480);
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
            this.panelCapture.Location = new System.Drawing.Point(3, 3);
            this.panelCapture.Name = "panelCapture";
            this.panelCapture.Size = new System.Drawing.Size(537, 474);
            this.panelCapture.TabIndex = 0;
            // 
            // gbCaptureAfter
            // 
            this.gbCaptureAfter.Controls.Add(this.ucAfterCaptureTasks);
            this.gbCaptureAfter.Location = new System.Drawing.Point(11, 96);
            this.gbCaptureAfter.Name = "gbCaptureAfter";
            this.gbCaptureAfter.Size = new System.Drawing.Size(501, 296);
            this.gbCaptureAfter.TabIndex = 3;
            this.gbCaptureAfter.TabStop = false;
            this.gbCaptureAfter.Text = "After capture tasks (only applicable for workflows that contain this activity)";
            this.gbCaptureAfter.Visible = false;
            // 
            // ucAfterCaptureTasks
            // 
            this.ucAfterCaptureTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterCaptureTasks.Location = new System.Drawing.Point(3, 16);
            this.ucAfterCaptureTasks.Name = "ucAfterCaptureTasks";
            this.ucAfterCaptureTasks.Size = new System.Drawing.Size(495, 277);
            this.ucAfterCaptureTasks.TabIndex = 0;
            // 
            // cbCaptureShadow
            // 
            this.cbCaptureShadow.AutoSize = true;
            this.cbCaptureShadow.Location = new System.Drawing.Point(16, 64);
            this.cbCaptureShadow.Name = "cbCaptureShadow";
            this.cbCaptureShadow.Size = new System.Drawing.Size(274, 17);
            this.cbCaptureShadow.TabIndex = 2;
            this.cbCaptureShadow.Text = "Capture window with shadow (requires transparency)";
            this.cbCaptureShadow.UseVisualStyleBackColor = true;
            this.cbCaptureShadow.CheckedChanged += new System.EventHandler(this.cbCaptureShadow_CheckedChanged);
            // 
            // cbShowCursor
            // 
            this.cbShowCursor.AutoSize = true;
            this.cbShowCursor.Location = new System.Drawing.Point(16, 16);
            this.cbShowCursor.Name = "cbShowCursor";
            this.cbShowCursor.Size = new System.Drawing.Size(156, 17);
            this.cbShowCursor.TabIndex = 0;
            this.cbShowCursor.Text = "Show cursor in screenshots";
            this.cbShowCursor.UseVisualStyleBackColor = true;
            this.cbShowCursor.CheckedChanged += new System.EventHandler(this.cbShowCursor_CheckedChanged);
            // 
            // cbCaptureTransparent
            // 
            this.cbCaptureTransparent.AutoSize = true;
            this.cbCaptureTransparent.Location = new System.Drawing.Point(16, 40);
            this.cbCaptureTransparent.Name = "cbCaptureTransparent";
            this.cbCaptureTransparent.Size = new System.Drawing.Size(188, 17);
            this.cbCaptureTransparent.TabIndex = 1;
            this.cbCaptureTransparent.Text = "Capture window with transparency";
            this.cbCaptureTransparent.UseVisualStyleBackColor = true;
            this.cbCaptureTransparent.CheckedChanged += new System.EventHandler(this.cbCaptureTransparent_CheckedChanged);
            // 
            // tpShapes
            // 
            this.tpShapes.Controls.Add(this.panelShapes);
            this.tpShapes.Location = new System.Drawing.Point(4, 22);
            this.tpShapes.Name = "tpShapes";
            this.tpShapes.Padding = new System.Windows.Forms.Padding(3);
            this.tpShapes.Size = new System.Drawing.Size(543, 480);
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
            this.panelShapes.Location = new System.Drawing.Point(3, 3);
            this.panelShapes.Name = "panelShapes";
            this.panelShapes.Size = new System.Drawing.Size(537, 474);
            this.panelShapes.TabIndex = 0;
            // 
            // cbShapeForceWindowCapture
            // 
            this.cbShapeForceWindowCapture.AutoSize = true;
            this.cbShapeForceWindowCapture.Location = new System.Drawing.Point(16, 176);
            this.cbShapeForceWindowCapture.Name = "cbShapeForceWindowCapture";
            this.cbShapeForceWindowCapture.Size = new System.Drawing.Size(287, 17);
            this.cbShapeForceWindowCapture.TabIndex = 9;
            this.cbShapeForceWindowCapture.Text = "Use window capture mode for all rectangle type shapes";
            this.cbShapeForceWindowCapture.UseVisualStyleBackColor = true;
            this.cbShapeForceWindowCapture.CheckedChanged += new System.EventHandler(this.cbShapeForceWindowCapture_CheckedChanged);
            // 
            // cbShapeIncludeControls
            // 
            this.cbShapeIncludeControls.AutoSize = true;
            this.cbShapeIncludeControls.Location = new System.Drawing.Point(16, 152);
            this.cbShapeIncludeControls.Name = "cbShapeIncludeControls";
            this.cbShapeIncludeControls.Size = new System.Drawing.Size(329, 17);
            this.cbShapeIncludeControls.TabIndex = 8;
            this.cbShapeIncludeControls.Text = "Allow capturing controls in window capture (buttons, panels etc.)";
            this.cbShapeIncludeControls.UseVisualStyleBackColor = true;
            this.cbShapeIncludeControls.CheckedChanged += new System.EventHandler(this.cbShapeIncludeControls_CheckedChanged);
            // 
            // lblFixedShapeSizeHeight
            // 
            this.lblFixedShapeSizeHeight.AutoSize = true;
            this.lblFixedShapeSizeHeight.Location = new System.Drawing.Point(160, 116);
            this.lblFixedShapeSizeHeight.Name = "lblFixedShapeSizeHeight";
            this.lblFixedShapeSizeHeight.Size = new System.Drawing.Size(41, 13);
            this.lblFixedShapeSizeHeight.TabIndex = 6;
            this.lblFixedShapeSizeHeight.Text = "Height:";
            // 
            // cbDrawBorder
            // 
            this.cbDrawBorder.AutoSize = true;
            this.cbDrawBorder.Location = new System.Drawing.Point(16, 16);
            this.cbDrawBorder.Name = "cbDrawBorder";
            this.cbDrawBorder.Size = new System.Drawing.Size(170, 17);
            this.cbDrawBorder.TabIndex = 0;
            this.cbDrawBorder.Text = "Draw border around the shape";
            this.cbDrawBorder.UseVisualStyleBackColor = true;
            this.cbDrawBorder.CheckedChanged += new System.EventHandler(this.cbDrawBorder_CheckedChanged);
            // 
            // lblFixedShapeSizeWidth
            // 
            this.lblFixedShapeSizeWidth.AutoSize = true;
            this.lblFixedShapeSizeWidth.Location = new System.Drawing.Point(44, 116);
            this.lblFixedShapeSizeWidth.Name = "lblFixedShapeSizeWidth";
            this.lblFixedShapeSizeWidth.Size = new System.Drawing.Size(38, 13);
            this.lblFixedShapeSizeWidth.TabIndex = 4;
            this.lblFixedShapeSizeWidth.Text = "Width:";
            // 
            // cbQuickCrop
            // 
            this.cbQuickCrop.AutoSize = true;
            this.cbQuickCrop.Location = new System.Drawing.Point(16, 64);
            this.cbQuickCrop.Name = "cbQuickCrop";
            this.cbQuickCrop.Size = new System.Drawing.Size(455, 17);
            this.cbQuickCrop.TabIndex = 2;
            this.cbQuickCrop.Text = "Complete capture as soon as the mouse button is released, except when capturing p" +
    "olygon";
            this.cbQuickCrop.UseVisualStyleBackColor = true;
            this.cbQuickCrop.CheckedChanged += new System.EventHandler(this.cbQuickCrop_CheckedChanged);
            // 
            // nudFixedShapeSizeHeight
            // 
            this.nudFixedShapeSizeHeight.Location = new System.Drawing.Point(208, 112);
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
            this.nudFixedShapeSizeHeight.Size = new System.Drawing.Size(56, 20);
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
            this.cbDrawCheckerboard.Location = new System.Drawing.Point(16, 40);
            this.cbDrawCheckerboard.Name = "cbDrawCheckerboard";
            this.cbDrawCheckerboard.Size = new System.Drawing.Size(287, 17);
            this.cbDrawCheckerboard.TabIndex = 1;
            this.cbDrawCheckerboard.Text = "Draw checkerboard pattern replacing transparent areas";
            this.cbDrawCheckerboard.UseVisualStyleBackColor = true;
            this.cbDrawCheckerboard.CheckedChanged += new System.EventHandler(this.cbDrawCheckerboard_CheckedChanged);
            // 
            // nudFixedShapeSizeWidth
            // 
            this.nudFixedShapeSizeWidth.Location = new System.Drawing.Point(88, 112);
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
            this.nudFixedShapeSizeWidth.Size = new System.Drawing.Size(56, 20);
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
            this.cbFixedShapeSize.Location = new System.Drawing.Point(16, 88);
            this.cbFixedShapeSize.Name = "cbFixedShapeSize";
            this.cbFixedShapeSize.Size = new System.Drawing.Size(107, 17);
            this.cbFixedShapeSize.TabIndex = 3;
            this.cbFixedShapeSize.Text = "Fixed shape size:";
            this.cbFixedShapeSize.UseVisualStyleBackColor = true;
            this.cbFixedShapeSize.CheckedChanged += new System.EventHandler(this.cbFixedShapeSize_CheckedChanged);
            // 
            // tpProxy
            // 
            this.tpProxy.Controls.Add(this.panelProxy);
            this.tpProxy.Location = new System.Drawing.Point(4, 22);
            this.tpProxy.Name = "tpProxy";
            this.tpProxy.Padding = new System.Windows.Forms.Padding(3);
            this.tpProxy.Size = new System.Drawing.Size(543, 480);
            this.tpProxy.TabIndex = 4;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // panelProxy
            // 
            this.panelProxy.Controls.Add(this.btnAutofillProxy);
            this.panelProxy.Controls.Add(this.pgProxy);
            this.panelProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProxy.Location = new System.Drawing.Point(3, 3);
            this.panelProxy.Name = "panelProxy";
            this.panelProxy.Size = new System.Drawing.Size(537, 474);
            this.panelProxy.TabIndex = 0;
            // 
            // btnAutofillProxy
            // 
            this.btnAutofillProxy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutofillProxy.Location = new System.Drawing.Point(452, 438);
            this.btnAutofillProxy.Name = "btnAutofillProxy";
            this.btnAutofillProxy.Size = new System.Drawing.Size(75, 23);
            this.btnAutofillProxy.TabIndex = 1;
            this.btnAutofillProxy.Text = "Autofill";
            this.btnAutofillProxy.UseVisualStyleBackColor = true;
            this.btnAutofillProxy.Click += new System.EventHandler(this.btnAutofillProxy_Click);
            // 
            // pgProxy
            // 
            this.pgProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProxy.Location = new System.Drawing.Point(0, 0);
            this.pgProxy.Name = "pgProxy";
            this.pgProxy.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgProxy.Size = new System.Drawing.Size(537, 474);
            this.pgProxy.TabIndex = 0;
            this.pgProxy.ToolbarVisible = false;
            // 
            // tpClipboardUpload
            // 
            this.tpClipboardUpload.Controls.Add(this.panelClipboardUpload);
            this.tpClipboardUpload.Location = new System.Drawing.Point(4, 22);
            this.tpClipboardUpload.Name = "tpClipboardUpload";
            this.tpClipboardUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpClipboardUpload.Size = new System.Drawing.Size(543, 480);
            this.tpClipboardUpload.TabIndex = 5;
            this.tpClipboardUpload.Text = "Clipboard Upload";
            this.tpClipboardUpload.UseVisualStyleBackColor = true;
            // 
            // panelClipboardUpload
            // 
            this.panelClipboardUpload.Controls.Add(this.cbClipboardUploadAutoDetectURL);
            this.panelClipboardUpload.Controls.Add(this.lblClipboardUploadInfo);
            this.panelClipboardUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelClipboardUpload.Location = new System.Drawing.Point(3, 3);
            this.panelClipboardUpload.Name = "panelClipboardUpload";
            this.panelClipboardUpload.Size = new System.Drawing.Size(537, 474);
            this.panelClipboardUpload.TabIndex = 0;
            // 
            // cbClipboardUploadAutoDetectURL
            // 
            this.cbClipboardUploadAutoDetectURL.AutoSize = true;
            this.cbClipboardUploadAutoDetectURL.Location = new System.Drawing.Point(16, 16);
            this.cbClipboardUploadAutoDetectURL.Name = "cbClipboardUploadAutoDetectURL";
            this.cbClipboardUploadAutoDetectURL.Size = new System.Drawing.Size(401, 17);
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
            this.lblClipboardUploadInfo.Location = new System.Drawing.Point(0, 445);
            this.lblClipboardUploadInfo.Name = "lblClipboardUploadInfo";
            this.lblClipboardUploadInfo.Size = new System.Drawing.Size(537, 29);
            this.lblClipboardUploadInfo.TabIndex = 1;
            this.lblClipboardUploadInfo.Text = "Clipboard upload automatically detects the data type and selects the upload servi" +
    "ce accordingly.";
            this.lblClipboardUploadInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpUpload
            // 
            this.tpUpload.Controls.Add(this.panelUpload);
            this.tpUpload.Location = new System.Drawing.Point(4, 22);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpload.Size = new System.Drawing.Size(543, 480);
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
            this.panelUpload.Location = new System.Drawing.Point(3, 3);
            this.panelUpload.Name = "panelUpload";
            this.panelUpload.Size = new System.Drawing.Size(537, 474);
            this.panelUpload.TabIndex = 0;
            // 
            // gbAfterUpload
            // 
            this.gbAfterUpload.Controls.Add(this.ucAfterUploadTasks);
            this.gbAfterUpload.Location = new System.Drawing.Point(16, 88);
            this.gbAfterUpload.Name = "gbAfterUpload";
            this.gbAfterUpload.Size = new System.Drawing.Size(483, 112);
            this.gbAfterUpload.TabIndex = 6;
            this.gbAfterUpload.TabStop = false;
            this.gbAfterUpload.Text = "After Upload";
            this.gbAfterUpload.Visible = false;
            // 
            // ucAfterUploadTasks
            // 
            this.ucAfterUploadTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterUploadTasks.Location = new System.Drawing.Point(3, 16);
            this.ucAfterUploadTasks.Name = "ucAfterUploadTasks";
            this.ucAfterUploadTasks.Size = new System.Drawing.Size(477, 93);
            this.ucAfterUploadTasks.TabIndex = 0;
            // 
            // lblUploadLimitHint
            // 
            this.lblUploadLimitHint.AutoSize = true;
            this.lblUploadLimitHint.Location = new System.Drawing.Point(216, 24);
            this.lblUploadLimitHint.Name = "lblUploadLimitHint";
            this.lblUploadLimitHint.Size = new System.Drawing.Size(90, 13);
            this.lblUploadLimitHint.TabIndex = 2;
            this.lblUploadLimitHint.Text = "0 - 25 (0 disables)";
            // 
            // nudUploadLimit
            // 
            this.nudUploadLimit.Location = new System.Drawing.Point(152, 20);
            this.nudUploadLimit.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudUploadLimit.Name = "nudUploadLimit";
            this.nudUploadLimit.Size = new System.Drawing.Size(56, 20);
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
            this.lblUploadLimit.Location = new System.Drawing.Point(16, 24);
            this.lblUploadLimit.Name = "lblUploadLimit";
            this.lblUploadLimit.Size = new System.Drawing.Size(128, 13);
            this.lblUploadLimit.TabIndex = 0;
            this.lblUploadLimit.Text = "Simultaneous upload limit:";
            // 
            // lblBufferSize
            // 
            this.lblBufferSize.AutoSize = true;
            this.lblBufferSize.Location = new System.Drawing.Point(16, 56);
            this.lblBufferSize.Name = "lblBufferSize";
            this.lblBufferSize.Size = new System.Drawing.Size(59, 13);
            this.lblBufferSize.TabIndex = 3;
            this.lblBufferSize.Text = "Buffer size:";
            // 
            // lblBufferSizeInfo
            // 
            this.lblBufferSizeInfo.AutoSize = true;
            this.lblBufferSizeInfo.Location = new System.Drawing.Point(152, 56);
            this.lblBufferSizeInfo.Name = "lblBufferSizeInfo";
            this.lblBufferSizeInfo.Size = new System.Drawing.Size(23, 13);
            this.lblBufferSizeInfo.TabIndex = 5;
            this.lblBufferSizeInfo.Text = "KiB";
            // 
            // cbBufferSize
            // 
            this.cbBufferSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBufferSize.FormattingEnabled = true;
            this.cbBufferSize.Location = new System.Drawing.Point(80, 52);
            this.cbBufferSize.Name = "cbBufferSize";
            this.cbBufferSize.Size = new System.Drawing.Size(64, 21);
            this.cbBufferSize.TabIndex = 4;
            this.cbBufferSize.SelectedIndexChanged += new System.EventHandler(this.cbBufferSize_SelectedIndexChanged);
            // 
            // tpImageResize
            // 
            this.tpImageResize.Controls.Add(this.panelImageResize);
            this.tpImageResize.Location = new System.Drawing.Point(4, 22);
            this.tpImageResize.Name = "tpImageResize";
            this.tpImageResize.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageResize.Size = new System.Drawing.Size(543, 480);
            this.tpImageResize.TabIndex = 7;
            this.tpImageResize.Text = "Resize";
            this.tpImageResize.UseVisualStyleBackColor = true;
            // 
            // panelImageResize
            // 
            this.panelImageResize.Controls.Add(this.ucImageResizeUI);
            this.panelImageResize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImageResize.Location = new System.Drawing.Point(3, 3);
            this.panelImageResize.Name = "panelImageResize";
            this.panelImageResize.Size = new System.Drawing.Size(537, 474);
            this.panelImageResize.TabIndex = 0;
            // 
            // ucImageResizeUI
            // 
            this.ucImageResizeUI.Location = new System.Drawing.Point(8, 8);
            this.ucImageResizeUI.Name = "ucImageResizeUI";
            this.ucImageResizeUI.Size = new System.Drawing.Size(240, 384);
            this.ucImageResizeUI.TabIndex = 0;
            // 
            // tpImageQuality2
            // 
            this.tpImageQuality2.Controls.Add(this.panelImageQuality);
            this.tpImageQuality2.Location = new System.Drawing.Point(4, 22);
            this.tpImageQuality2.Name = "tpImageQuality2";
            this.tpImageQuality2.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageQuality2.Size = new System.Drawing.Size(543, 480);
            this.tpImageQuality2.TabIndex = 8;
            this.tpImageQuality2.Text = "Quality";
            this.tpImageQuality2.UseVisualStyleBackColor = true;
            // 
            // panelImageQuality
            // 
            this.panelImageQuality.Controls.Add(this.ucImageQualityUI);
            this.panelImageQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImageQuality.Location = new System.Drawing.Point(3, 3);
            this.panelImageQuality.Name = "panelImageQuality";
            this.panelImageQuality.Size = new System.Drawing.Size(537, 474);
            this.panelImageQuality.TabIndex = 0;
            // 
            // ucImageQualityUI
            // 
            this.ucImageQualityUI.Location = new System.Drawing.Point(16, 16);
            this.ucImageQualityUI.Name = "ucImageQualityUI";
            this.ucImageQualityUI.Size = new System.Drawing.Size(382, 222);
            this.ucImageQualityUI.TabIndex = 0;
            // 
            // tpPaths
            // 
            this.tpPaths.Controls.Add(this.panelPaths);
            this.tpPaths.Location = new System.Drawing.Point(4, 22);
            this.tpPaths.Name = "tpPaths";
            this.tpPaths.Padding = new System.Windows.Forms.Padding(3);
            this.tpPaths.Size = new System.Drawing.Size(543, 480);
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
            this.panelPaths.Location = new System.Drawing.Point(3, 3);
            this.panelPaths.Name = "panelPaths";
            this.panelPaths.Size = new System.Drawing.Size(537, 474);
            this.panelPaths.TabIndex = 0;
            // 
            // gbPathRoot
            // 
            this.gbPathRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPathRoot.Controls.Add(this.lblOpenZUploaderPath);
            this.gbPathRoot.Controls.Add(this.btnOpenZUploaderPath);
            this.gbPathRoot.Location = new System.Drawing.Point(8, 360);
            this.gbPathRoot.Name = "gbPathRoot";
            this.gbPathRoot.Size = new System.Drawing.Size(421, 88);
            this.gbPathRoot.TabIndex = 3;
            this.gbPathRoot.TabStop = false;
            this.gbPathRoot.Text = "Root";
            // 
            // lblOpenZUploaderPath
            // 
            this.lblOpenZUploaderPath.AutoSize = true;
            this.lblOpenZUploaderPath.Location = new System.Drawing.Point(16, 56);
            this.lblOpenZUploaderPath.Name = "lblOpenZUploaderPath";
            this.lblOpenZUploaderPath.Size = new System.Drawing.Size(257, 13);
            this.lblOpenZUploaderPath.TabIndex = 1;
            this.lblOpenZUploaderPath.Text = "This folder has settings, history database and log files";
            // 
            // btnOpenZUploaderPath
            // 
            this.btnOpenZUploaderPath.Location = new System.Drawing.Point(16, 24);
            this.btnOpenZUploaderPath.Name = "btnOpenZUploaderPath";
            this.btnOpenZUploaderPath.Size = new System.Drawing.Size(176, 23);
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
            this.gbScreenshots.Location = new System.Drawing.Point(8, 8);
            this.gbScreenshots.Name = "gbScreenshots";
            this.gbScreenshots.Size = new System.Drawing.Size(462, 88);
            this.gbScreenshots.TabIndex = 0;
            this.gbScreenshots.TabStop = false;
            this.gbScreenshots.Text = "Screenshots";
            // 
            // btnImagesOrganise
            // 
            this.btnImagesOrganise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImagesOrganise.AutoSize = true;
            this.btnImagesOrganise.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnImagesOrganise.Location = new System.Drawing.Point(374, 51);
            this.btnImagesOrganise.Name = "btnImagesOrganise";
            this.btnImagesOrganise.Size = new System.Drawing.Size(68, 23);
            this.btnImagesOrganise.TabIndex = 5;
            this.btnImagesOrganise.Text = "&Organise...";
            this.btnImagesOrganise.UseVisualStyleBackColor = true;
            this.btnImagesOrganise.Click += new System.EventHandler(this.btnImagesOrganise_Click);
            // 
            // txtSaveImageSubFolderPatternPreview
            // 
            this.txtSaveImageSubFolderPatternPreview.Location = new System.Drawing.Point(248, 53);
            this.txtSaveImageSubFolderPatternPreview.Name = "txtSaveImageSubFolderPatternPreview";
            this.txtSaveImageSubFolderPatternPreview.ReadOnly = true;
            this.txtSaveImageSubFolderPatternPreview.Size = new System.Drawing.Size(120, 20);
            this.txtSaveImageSubFolderPatternPreview.TabIndex = 4;
            // 
            // lblSaveImageSubFolderPattern
            // 
            this.lblSaveImageSubFolderPattern.AutoSize = true;
            this.lblSaveImageSubFolderPattern.Location = new System.Drawing.Point(16, 56);
            this.lblSaveImageSubFolderPattern.Name = "lblSaveImageSubFolderPattern";
            this.lblSaveImageSubFolderPattern.Size = new System.Drawing.Size(94, 13);
            this.lblSaveImageSubFolderPattern.TabIndex = 2;
            this.lblSaveImageSubFolderPattern.Text = "Sub folder pattern:";
            // 
            // txtSaveImageSubFolderPattern
            // 
            this.txtSaveImageSubFolderPattern.Location = new System.Drawing.Point(120, 53);
            this.txtSaveImageSubFolderPattern.Name = "txtSaveImageSubFolderPattern";
            this.txtSaveImageSubFolderPattern.Size = new System.Drawing.Size(120, 20);
            this.txtSaveImageSubFolderPattern.TabIndex = 3;
            this.txtSaveImageSubFolderPattern.TextChanged += new System.EventHandler(this.txtSaveImageSubFolderPattern_TextChanged);
            // 
            // btnBrowseScreenshotsDir
            // 
            this.btnBrowseScreenshotsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseScreenshotsDir.AutoSize = true;
            this.btnBrowseScreenshotsDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseScreenshotsDir.Location = new System.Drawing.Point(381, 22);
            this.btnBrowseScreenshotsDir.Name = "btnBrowseScreenshotsDir";
            this.btnBrowseScreenshotsDir.Size = new System.Drawing.Size(61, 23);
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
            this.txtScreenshotsPath.Location = new System.Drawing.Point(21, 24);
            this.txtScreenshotsPath.Name = "txtScreenshotsPath";
            this.txtScreenshotsPath.Size = new System.Drawing.Size(337, 20);
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
            this.gbUploadersConfig.Location = new System.Drawing.Point(8, 112);
            this.gbUploadersConfig.Name = "gbUploadersConfig";
            this.gbUploadersConfig.Size = new System.Drawing.Size(462, 80);
            this.gbUploadersConfig.TabIndex = 1;
            this.gbUploadersConfig.TabStop = false;
            this.gbUploadersConfig.Text = "Uploaders Config";
            // 
            // btnLoadUploadersConfig
            // 
            this.btnLoadUploadersConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadUploadersConfig.AutoSize = true;
            this.btnLoadUploadersConfig.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadUploadersConfig.Location = new System.Drawing.Point(398, 16);
            this.btnLoadUploadersConfig.Name = "btnLoadUploadersConfig";
            this.btnLoadUploadersConfig.Size = new System.Drawing.Size(41, 23);
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
            this.btnBrowseCustomUploadersConfigPath.Location = new System.Drawing.Point(380, 45);
            this.btnBrowseCustomUploadersConfigPath.Name = "btnBrowseCustomUploadersConfigPath";
            this.btnBrowseCustomUploadersConfigPath.Size = new System.Drawing.Size(61, 23);
            this.btnBrowseCustomUploadersConfigPath.TabIndex = 3;
            this.btnBrowseCustomUploadersConfigPath.Text = "Browse...";
            this.btnBrowseCustomUploadersConfigPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomUploadersConfigPath.Click += new System.EventHandler(this.btnBrowseCustomUploadersConfigPath_Click);
            // 
            // txtCustomUploadersConfigPath
            // 
            this.txtCustomUploadersConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomUploadersConfigPath.Location = new System.Drawing.Point(16, 48);
            this.txtCustomUploadersConfigPath.Name = "txtCustomUploadersConfigPath";
            this.txtCustomUploadersConfigPath.Size = new System.Drawing.Size(342, 20);
            this.txtCustomUploadersConfigPath.TabIndex = 2;
            this.txtCustomUploadersConfigPath.TextChanged += new System.EventHandler(this.txtCustomUploadersConfigPath_TextChanged);
            // 
            // cbUseCustomUploadersConfigPath
            // 
            this.cbUseCustomUploadersConfigPath.AutoSize = true;
            this.cbUseCustomUploadersConfigPath.Location = new System.Drawing.Point(16, 24);
            this.cbUseCustomUploadersConfigPath.Name = "cbUseCustomUploadersConfigPath";
            this.cbUseCustomUploadersConfigPath.Size = new System.Drawing.Size(206, 17);
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
            this.gbHistory.Location = new System.Drawing.Point(8, 216);
            this.gbHistory.Name = "gbHistory";
            this.gbHistory.Size = new System.Drawing.Size(462, 136);
            this.gbHistory.TabIndex = 2;
            this.gbHistory.TabStop = false;
            this.gbHistory.Text = "History";
            // 
            // nudHistoryMaxItemCount
            // 
            this.nudHistoryMaxItemCount.Location = new System.Drawing.Point(216, 100);
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
            this.nudHistoryMaxItemCount.Size = new System.Drawing.Size(80, 20);
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
            this.lblHistoryMaxItemCount.Location = new System.Drawing.Point(16, 104);
            this.lblHistoryMaxItemCount.Name = "lblHistoryMaxItemCount";
            this.lblHistoryMaxItemCount.Size = new System.Drawing.Size(192, 13);
            this.lblHistoryMaxItemCount.TabIndex = 4;
            this.lblHistoryMaxItemCount.Text = "Max item count for filtering (-1 disables):";
            // 
            // btnBrowseCustomHistoryPath
            // 
            this.btnBrowseCustomHistoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseCustomHistoryPath.AutoSize = true;
            this.btnBrowseCustomHistoryPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseCustomHistoryPath.Location = new System.Drawing.Point(382, 72);
            this.btnBrowseCustomHistoryPath.Name = "btnBrowseCustomHistoryPath";
            this.btnBrowseCustomHistoryPath.Size = new System.Drawing.Size(61, 23);
            this.btnBrowseCustomHistoryPath.TabIndex = 3;
            this.btnBrowseCustomHistoryPath.Text = "Browse...";
            this.btnBrowseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomHistoryPath.Click += new System.EventHandler(this.btnBrowseCustomHistoryPath_Click);
            // 
            // txtCustomHistoryPath
            // 
            this.txtCustomHistoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomHistoryPath.Location = new System.Drawing.Point(16, 72);
            this.txtCustomHistoryPath.Name = "txtCustomHistoryPath";
            this.txtCustomHistoryPath.Size = new System.Drawing.Size(342, 20);
            this.txtCustomHistoryPath.TabIndex = 2;
            this.txtCustomHistoryPath.TextChanged += new System.EventHandler(this.txtCustomHistoryPath_TextChanged);
            // 
            // cbUseCustomHistoryPath
            // 
            this.cbUseCustomHistoryPath.AutoSize = true;
            this.cbUseCustomHistoryPath.Location = new System.Drawing.Point(16, 48);
            this.cbUseCustomHistoryPath.Name = "cbUseCustomHistoryPath";
            this.cbUseCustomHistoryPath.Size = new System.Drawing.Size(158, 17);
            this.cbUseCustomHistoryPath.TabIndex = 1;
            this.cbUseCustomHistoryPath.Text = "Use custom history file path:";
            this.cbUseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.cbUseCustomHistoryPath.CheckedChanged += new System.EventHandler(this.cbUseCustomHistoryPath_CheckedChanged);
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(16, 24);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(118, 17);
            this.cbHistorySave.TabIndex = 0;
            this.cbHistorySave.Text = "Enable history save";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // tpSync
            // 
            this.tpSync.Controls.Add(this.panelSync);
            this.tpSync.Location = new System.Drawing.Point(4, 22);
            this.tpSync.Name = "tpSync";
            this.tpSync.Padding = new System.Windows.Forms.Padding(3);
            this.tpSync.Size = new System.Drawing.Size(543, 480);
            this.tpSync.TabIndex = 10;
            this.tpSync.Text = "Sync";
            this.tpSync.UseVisualStyleBackColor = true;
            // 
            // panelSync
            // 
            this.panelSync.Controls.Add(this.btnDropboxSyncExport);
            this.panelSync.Controls.Add(this.btnDropboxSyncImport);
            this.panelSync.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSync.Location = new System.Drawing.Point(3, 3);
            this.panelSync.Name = "panelSync";
            this.panelSync.Size = new System.Drawing.Size(537, 474);
            this.panelSync.TabIndex = 0;
            // 
            // btnDropboxSyncExport
            // 
            this.btnDropboxSyncExport.Location = new System.Drawing.Point(8, 40);
            this.btnDropboxSyncExport.Name = "btnDropboxSyncExport";
            this.btnDropboxSyncExport.Size = new System.Drawing.Size(184, 22);
            this.btnDropboxSyncExport.TabIndex = 1;
            this.btnDropboxSyncExport.Text = "&Export Settings to Dropbox";
            this.btnDropboxSyncExport.UseVisualStyleBackColor = true;
            this.btnDropboxSyncExport.Click += new System.EventHandler(this.btnDropboxSyncExport_Click);
            // 
            // btnDropboxSyncImport
            // 
            this.btnDropboxSyncImport.Location = new System.Drawing.Point(8, 8);
            this.btnDropboxSyncImport.Name = "btnDropboxSyncImport";
            this.btnDropboxSyncImport.Size = new System.Drawing.Size(184, 22);
            this.btnDropboxSyncImport.TabIndex = 0;
            this.btnDropboxSyncImport.Text = "&Import Settings from Dropbox";
            this.btnDropboxSyncImport.UseVisualStyleBackColor = true;
            this.btnDropboxSyncImport.Click += new System.EventHandler(this.btnDropboxSyncImport_Click);
            // 
            // tpFileNaming
            // 
            this.tpFileNaming.Controls.Add(this.panelFileNaming);
            this.tpFileNaming.Location = new System.Drawing.Point(4, 22);
            this.tpFileNaming.Name = "tpFileNaming";
            this.tpFileNaming.Padding = new System.Windows.Forms.Padding(3);
            this.tpFileNaming.Size = new System.Drawing.Size(543, 480);
            this.tpFileNaming.TabIndex = 11;
            this.tpFileNaming.Text = "File Naming";
            this.tpFileNaming.UseVisualStyleBackColor = true;
            // 
            // panelFileNaming
            // 
            this.panelFileNaming.Controls.Add(this.gbFilenamingPatternOthers);
            this.panelFileNaming.Controls.Add(this.gbFilenamingPatternImages);
            this.panelFileNaming.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFileNaming.Location = new System.Drawing.Point(3, 3);
            this.panelFileNaming.Name = "panelFileNaming";
            this.panelFileNaming.Size = new System.Drawing.Size(537, 474);
            this.panelFileNaming.TabIndex = 0;
            // 
            // gbFilenamingPatternOthers
            // 
            this.gbFilenamingPatternOthers.Controls.Add(this.txtNameFormatPatternOther);
            this.gbFilenamingPatternOthers.Controls.Add(this.lblNameFormatPatternPreviewOther);
            this.gbFilenamingPatternOthers.Controls.Add(this.btnNameFormatPatternHelpOther);
            this.gbFilenamingPatternOthers.Location = new System.Drawing.Point(8, 104);
            this.gbFilenamingPatternOthers.Name = "gbFilenamingPatternOthers";
            this.gbFilenamingPatternOthers.Size = new System.Drawing.Size(509, 80);
            this.gbFilenamingPatternOthers.TabIndex = 1;
            this.gbFilenamingPatternOthers.TabStop = false;
            this.gbFilenamingPatternOthers.Text = "File naming pattern for other files that do not already exist in the computer:";
            // 
            // txtNameFormatPatternOther
            // 
            this.txtNameFormatPatternOther.Location = new System.Drawing.Point(16, 24);
            this.txtNameFormatPatternOther.Name = "txtNameFormatPatternOther";
            this.txtNameFormatPatternOther.Size = new System.Drawing.Size(416, 20);
            this.txtNameFormatPatternOther.TabIndex = 0;
            this.txtNameFormatPatternOther.TextChanged += new System.EventHandler(this.txtNameFormatPatternOther_TextChanged);
            // 
            // lblNameFormatPatternPreviewOther
            // 
            this.lblNameFormatPatternPreviewOther.AutoSize = true;
            this.lblNameFormatPatternPreviewOther.Location = new System.Drawing.Point(16, 56);
            this.lblNameFormatPatternPreviewOther.Name = "lblNameFormatPatternPreviewOther";
            this.lblNameFormatPatternPreviewOther.Size = new System.Drawing.Size(48, 13);
            this.lblNameFormatPatternPreviewOther.TabIndex = 2;
            this.lblNameFormatPatternPreviewOther.Text = "Preview:";
            // 
            // btnNameFormatPatternHelpOther
            // 
            this.btnNameFormatPatternHelpOther.Location = new System.Drawing.Point(440, 24);
            this.btnNameFormatPatternHelpOther.Name = "btnNameFormatPatternHelpOther";
            this.btnNameFormatPatternHelpOther.Size = new System.Drawing.Size(24, 23);
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
            this.gbFilenamingPatternImages.Location = new System.Drawing.Point(8, 8);
            this.gbFilenamingPatternImages.Name = "gbFilenamingPatternImages";
            this.gbFilenamingPatternImages.Size = new System.Drawing.Size(509, 80);
            this.gbFilenamingPatternImages.TabIndex = 0;
            this.gbFilenamingPatternImages.TabStop = false;
            this.gbFilenamingPatternImages.Text = "File naming pattern for image files that do not already exist in the computer:";
            // 
            // lblNameFormatPatternPreviewImages
            // 
            this.lblNameFormatPatternPreviewImages.AutoSize = true;
            this.lblNameFormatPatternPreviewImages.Location = new System.Drawing.Point(16, 56);
            this.lblNameFormatPatternPreviewImages.Name = "lblNameFormatPatternPreviewImages";
            this.lblNameFormatPatternPreviewImages.Size = new System.Drawing.Size(48, 13);
            this.lblNameFormatPatternPreviewImages.TabIndex = 2;
            this.lblNameFormatPatternPreviewImages.Text = "Preview:";
            // 
            // txtNameFormatPatternImages
            // 
            this.txtNameFormatPatternImages.Location = new System.Drawing.Point(16, 24);
            this.txtNameFormatPatternImages.Name = "txtNameFormatPatternImages";
            this.txtNameFormatPatternImages.Size = new System.Drawing.Size(416, 20);
            this.txtNameFormatPatternImages.TabIndex = 0;
            this.txtNameFormatPatternImages.TextChanged += new System.EventHandler(this.txtNameFormatPattern_TextChanged);
            // 
            // btnNameFormatPatternHelpImages
            // 
            this.btnNameFormatPatternHelpImages.Location = new System.Drawing.Point(440, 24);
            this.btnNameFormatPatternHelpImages.Name = "btnNameFormatPatternHelpImages";
            this.btnNameFormatPatternHelpImages.Size = new System.Drawing.Size(24, 23);
            this.btnNameFormatPatternHelpImages.TabIndex = 1;
            this.btnNameFormatPatternHelpImages.Text = "?";
            this.btnNameFormatPatternHelpImages.UseVisualStyleBackColor = true;
            this.btnNameFormatPatternHelpImages.Click += new System.EventHandler(this.btnNameFormatPatternHelp_Click);
            // 
            // tpUploaderConfig
            // 
            this.tpUploaderConfig.Controls.Add(this.panelUploaderConfig);
            this.tpUploaderConfig.Location = new System.Drawing.Point(4, 22);
            this.tpUploaderConfig.Name = "tpUploaderConfig";
            this.tpUploaderConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpUploaderConfig.Size = new System.Drawing.Size(543, 480);
            this.tpUploaderConfig.TabIndex = 12;
            this.tpUploaderConfig.Text = "Uploader Config";
            this.tpUploaderConfig.UseVisualStyleBackColor = true;
            // 
            // panelUploaderConfig
            // 
            this.panelUploaderConfig.Controls.Add(this.pgUploaderConfig);
            this.panelUploaderConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUploaderConfig.Location = new System.Drawing.Point(3, 3);
            this.panelUploaderConfig.Name = "panelUploaderConfig";
            this.panelUploaderConfig.Size = new System.Drawing.Size(537, 474);
            this.panelUploaderConfig.TabIndex = 0;
            // 
            // pgUploaderConfig
            // 
            this.pgUploaderConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgUploaderConfig.Location = new System.Drawing.Point(0, 0);
            this.pgUploaderConfig.Name = "pgUploaderConfig";
            this.pgUploaderConfig.Size = new System.Drawing.Size(537, 474);
            this.pgUploaderConfig.TabIndex = 0;
            // 
            // tpShapes2
            // 
            this.tpShapes2.Controls.Add(this.panelShapes2);
            this.tpShapes2.Location = new System.Drawing.Point(4, 22);
            this.tpShapes2.Name = "tpShapes2";
            this.tpShapes2.Padding = new System.Windows.Forms.Padding(3);
            this.tpShapes2.Size = new System.Drawing.Size(543, 480);
            this.tpShapes2.TabIndex = 13;
            this.tpShapes2.Text = "Shapes";
            this.tpShapes2.UseVisualStyleBackColor = true;
            // 
            // panelShapes2
            // 
            this.panelShapes2.Controls.Add(this.pgShapes);
            this.panelShapes2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShapes2.Location = new System.Drawing.Point(3, 3);
            this.panelShapes2.Name = "panelShapes2";
            this.panelShapes2.Size = new System.Drawing.Size(537, 474);
            this.panelShapes2.TabIndex = 0;
            // 
            // pgShapes
            // 
            this.pgShapes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgShapes.Location = new System.Drawing.Point(0, 0);
            this.pgShapes.Name = "pgShapes";
            this.pgShapes.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgShapes.Size = new System.Drawing.Size(537, 474);
            this.pgShapes.TabIndex = 0;
            this.pgShapes.ToolbarVisible = false;
            // 
            // tpUserConfig
            // 
            this.tpUserConfig.Controls.Add(this.panelUserConfig);
            this.tpUserConfig.Location = new System.Drawing.Point(4, 22);
            this.tpUserConfig.Name = "tpUserConfig";
            this.tpUserConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpUserConfig.Size = new System.Drawing.Size(543, 480);
            this.tpUserConfig.TabIndex = 14;
            this.tpUserConfig.Text = "User Config";
            this.tpUserConfig.UseVisualStyleBackColor = true;
            // 
            // panelUserConfig
            // 
            this.panelUserConfig.Controls.Add(this.pgUserConfig);
            this.panelUserConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserConfig.Location = new System.Drawing.Point(3, 3);
            this.panelUserConfig.Name = "panelUserConfig";
            this.panelUserConfig.Size = new System.Drawing.Size(537, 474);
            this.panelUserConfig.TabIndex = 0;
            // 
            // pgUserConfig
            // 
            this.pgUserConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgUserConfig.Location = new System.Drawing.Point(0, 0);
            this.pgUserConfig.Name = "pgUserConfig";
            this.pgUserConfig.Size = new System.Drawing.Size(537, 474);
            this.pgUserConfig.TabIndex = 0;
            // 
            // OptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 566);
            this.Controls.Add(this.tlpMain);
            this.MinimumSize = new System.Drawing.Size(720, 600);
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
            this.tpUploaderConfig.ResumeLayout(false);
            this.panelUploaderConfig.ResumeLayout(false);
            this.tpShapes2.ResumeLayout(false);
            this.panelShapes2.ResumeLayout(false);
            this.tpUserConfig.ResumeLayout(false);
            this.panelUserConfig.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tpUploaderConfig;
        private System.Windows.Forms.Panel panelUploaderConfig;
        private System.Windows.Forms.PropertyGrid pgUploaderConfig;
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
    }
}