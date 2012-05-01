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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Shapes");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Capture", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Upload");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Paths");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Advanced");
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tvMain = new System.Windows.Forms.TreeView();
            this.panelBase = new System.Windows.Forms.Panel();
            this.panelCapture = new System.Windows.Forms.Panel();
            this.cbCaptureShadow = new System.Windows.Forms.CheckBox();
            this.cbShowCursor = new System.Windows.Forms.CheckBox();
            this.cbCaptureTransparent = new System.Windows.Forms.CheckBox();
            this.panelShapes = new System.Windows.Forms.Panel();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.lblGeneralInfo = new System.Windows.Forms.Label();
            this.cbCheckUpdates = new System.Windows.Forms.CheckBox();
            this.cbStartWithWindows = new System.Windows.Forms.CheckBox();
            this.cbShowTray = new System.Windows.Forms.CheckBox();
            this.cbShellContextMenu = new System.Windows.Forms.CheckBox();
            this.panelAdvanced = new System.Windows.Forms.Panel();
            this.pgSettings = new System.Windows.Forms.PropertyGrid();
            this.tlpMain.SuspendLayout();
            this.panelBase.SuspendLayout();
            this.panelCapture.SuspendLayout();
            this.panelGeneral.SuspendLayout();
            this.panelAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpMain.Controls.Add(this.tvMain, 0, 0);
            this.tlpMain.Controls.Add(this.panelBase, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(663, 554);
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
            treeNode2.Name = "tnShapes";
            treeNode2.Tag = "panelShapes";
            treeNode2.Text = "Shapes";
            treeNode3.Name = "tnCapture";
            treeNode3.Tag = "panelCapture";
            treeNode3.Text = "Capture";
            treeNode4.Name = "tnUpload";
            treeNode4.Text = "Upload";
            treeNode5.Name = "tnPaths";
            treeNode5.Text = "Paths";
            treeNode6.Name = "tnAdvanced";
            treeNode6.Text = "Advanced";
            this.tvMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.tvMain.Size = new System.Drawing.Size(192, 548);
            this.tvMain.TabIndex = 0;
            // 
            // panelBase
            // 
            this.panelBase.BackColor = System.Drawing.SystemColors.Control;
            this.panelBase.Controls.Add(this.panelAdvanced);
            this.panelBase.Controls.Add(this.panelCapture);
            this.panelBase.Controls.Add(this.panelShapes);
            this.panelBase.Controls.Add(this.panelGeneral);
            this.panelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBase.Location = new System.Drawing.Point(201, 3);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(459, 548);
            this.panelBase.TabIndex = 1;
            // 
            // panelCapture
            // 
            this.panelCapture.BackColor = System.Drawing.SystemColors.Control;
            this.panelCapture.Controls.Add(this.cbCaptureShadow);
            this.panelCapture.Controls.Add(this.cbShowCursor);
            this.panelCapture.Controls.Add(this.cbCaptureTransparent);
            this.panelCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCapture.Location = new System.Drawing.Point(0, 0);
            this.panelCapture.Name = "panelCapture";
            this.panelCapture.Size = new System.Drawing.Size(459, 548);
            this.panelCapture.TabIndex = 1;
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
            // panelShapes
            // 
            this.panelShapes.BackColor = System.Drawing.SystemColors.Control;
            this.panelShapes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShapes.Location = new System.Drawing.Point(0, 0);
            this.panelShapes.Name = "panelShapes";
            this.panelShapes.Size = new System.Drawing.Size(459, 548);
            this.panelShapes.TabIndex = 2;
            // 
            // panelGeneral
            // 
            this.panelGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.panelGeneral.Controls.Add(this.lblGeneralInfo);
            this.panelGeneral.Controls.Add(this.cbCheckUpdates);
            this.panelGeneral.Controls.Add(this.cbStartWithWindows);
            this.panelGeneral.Controls.Add(this.cbShowTray);
            this.panelGeneral.Controls.Add(this.cbShellContextMenu);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGeneral.Location = new System.Drawing.Point(0, 0);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(459, 548);
            this.panelGeneral.TabIndex = 3;
            // 
            // lblGeneralInfo
            // 
            this.lblGeneralInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblGeneralInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGeneralInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGeneralInfo.ForeColor = System.Drawing.Color.White;
            this.lblGeneralInfo.Location = new System.Drawing.Point(0, 519);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(459, 29);
            this.lblGeneralInfo.TabIndex = 4;
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
            this.cbShellContextMenu.Size = new System.Drawing.Size(270, 17);
            this.cbShellContextMenu.TabIndex = 2;
            this.cbShellContextMenu.Text = "Show \"Upload using ShareX\" in Shell context menu";
            this.cbShellContextMenu.UseVisualStyleBackColor = true;
            this.cbShellContextMenu.CheckedChanged += new System.EventHandler(this.cbShellContextMenu_CheckedChanged);
            // 
            // panelAdvanced
            // 
            this.panelAdvanced.BackColor = System.Drawing.SystemColors.Control;
            this.panelAdvanced.Controls.Add(this.pgSettings);
            this.panelAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAdvanced.Location = new System.Drawing.Point(0, 0);
            this.panelAdvanced.Name = "panelAdvanced";
            this.panelAdvanced.Size = new System.Drawing.Size(459, 548);
            this.panelAdvanced.TabIndex = 0;
            // 
            // pgSettings
            // 
            this.pgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSettings.Location = new System.Drawing.Point(0, 0);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.Size = new System.Drawing.Size(459, 548);
            this.pgSettings.TabIndex = 1;
            // 
            // OptionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 554);
            this.Controls.Add(this.tlpMain);
            this.Name = "OptionsWindow";
            this.Text = "OptionsWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsWindow_FormClosed);
            this.tlpMain.ResumeLayout(false);
            this.panelBase.ResumeLayout(false);
            this.panelCapture.ResumeLayout(false);
            this.panelCapture.PerformLayout();
            this.panelGeneral.ResumeLayout(false);
            this.panelGeneral.PerformLayout();
            this.panelAdvanced.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.Panel panelBase;
        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Panel panelCapture;
        private System.Windows.Forms.Panel panelShapes;
        private System.Windows.Forms.Panel panelAdvanced;
        private System.Windows.Forms.CheckBox cbCheckUpdates;
        private System.Windows.Forms.CheckBox cbStartWithWindows;
        private System.Windows.Forms.CheckBox cbShowTray;
        private System.Windows.Forms.CheckBox cbShellContextMenu;
        private System.Windows.Forms.CheckBox cbCaptureShadow;
        private System.Windows.Forms.CheckBox cbShowCursor;
        private System.Windows.Forms.CheckBox cbCaptureTransparent;
        private System.Windows.Forms.Label lblGeneralInfo;
        private System.Windows.Forms.PropertyGrid pgSettings;
    }
}