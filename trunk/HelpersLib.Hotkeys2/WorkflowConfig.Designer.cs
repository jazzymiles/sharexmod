namespace HelpersLib.Hotkeys2
{
    partial class WindowWorkflow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowWorkflow));
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcWorkflow = new System.Windows.Forms.TabControl();
            this.tpCapture = new System.Windows.Forms.TabPage();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.lblTextFormat = new System.Windows.Forms.Label();
            this.txtTextFormat = new System.Windows.Forms.TextBox();
            this.chkPerformGlobalAfterCaptureTasks = new System.Windows.Forms.CheckBox();
            this.cboCapture = new System.Windows.Forms.ComboBox();
            this.tpAfterCapture = new System.Windows.Forms.TabPage();
            this.ucAfterCaptureTasks = new HelpersLib.UserControls.AfterCatureTasksUI();
            this.tpRunExternalPrograms = new System.Windows.Forms.TabPage();
            this.btnActionsEdit = new System.Windows.Forms.Button();
            this.btnActionsRemove = new System.Windows.Forms.Button();
            this.btnActionsAdd = new System.Windows.Forms.Button();
            this.lvActions = new HelpersLib.MyListView();
            this.chActionsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActionsPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActionsArgs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpShare = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbFileUploaders = new System.Windows.Forms.GroupBox();
            this.flpFileUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.gbImageUploaders = new System.Windows.Forms.GroupBox();
            this.flpImageUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.gbTextUploaders = new System.Windows.Forms.GroupBox();
            this.flpTextUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.tpSummary = new System.Windows.Forms.TabPage();
            this.lblSummary = new System.Windows.Forms.Label();
            this.tcWorkflow.SuspendLayout();
            this.tpCapture.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.tpAfterCapture.SuspendLayout();
            this.tpRunExternalPrograms.SuspendLayout();
            this.tpShare.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbFileUploaders.SuspendLayout();
            this.gbImageUploaders.SuspendLayout();
            this.gbTextUploaders.SuspendLayout();
            this.tpSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(8, 8);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(662, 25);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.Text = "New workflow";
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(590, 448);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(74, 22);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(510, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 22);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tcWorkflow
            // 
            this.tcWorkflow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcWorkflow.Controls.Add(this.tpCapture);
            this.tcWorkflow.Controls.Add(this.tpAfterCapture);
            this.tcWorkflow.Controls.Add(this.tpRunExternalPrograms);
            this.tcWorkflow.Controls.Add(this.tpShare);
            this.tcWorkflow.Controls.Add(this.tpSummary);
            this.tcWorkflow.Location = new System.Drawing.Point(8, 40);
            this.tcWorkflow.Name = "tcWorkflow";
            this.tcWorkflow.SelectedIndex = 0;
            this.tcWorkflow.Size = new System.Drawing.Size(662, 400);
            this.tcWorkflow.TabIndex = 1;
            this.tcWorkflow.SelectedIndexChanged += new System.EventHandler(this.tcWorkflow_SelectedIndexChanged);
            // 
            // tpCapture
            // 
            this.tpCapture.Controls.Add(this.gbSettings);
            this.tpCapture.Controls.Add(this.chkPerformGlobalAfterCaptureTasks);
            this.tpCapture.Controls.Add(this.cboCapture);
            this.tpCapture.Location = new System.Drawing.Point(4, 22);
            this.tpCapture.Name = "tpCapture";
            this.tpCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tpCapture.Size = new System.Drawing.Size(654, 374);
            this.tpCapture.TabIndex = 0;
            this.tpCapture.Text = "Capture";
            this.tpCapture.UseVisualStyleBackColor = true;
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.lblTextFormat);
            this.gbSettings.Controls.Add(this.txtTextFormat);
            this.gbSettings.Location = new System.Drawing.Point(16, 80);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(616, 72);
            this.gbSettings.TabIndex = 2;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Customize text uploader settings";
            // 
            // lblTextFormat
            // 
            this.lblTextFormat.AutoSize = true;
            this.lblTextFormat.Location = new System.Drawing.Point(16, 24);
            this.lblTextFormat.Name = "lblTextFormat";
            this.lblTextFormat.Size = new System.Drawing.Size(164, 13);
            this.lblTextFormat.TabIndex = 0;
            this.lblTextFormat.Text = "Text format e.g. csharp, cpp, etc.";
            // 
            // txtTextFormat
            // 
            this.txtTextFormat.Location = new System.Drawing.Point(16, 40);
            this.txtTextFormat.Name = "txtTextFormat";
            this.txtTextFormat.Size = new System.Drawing.Size(298, 20);
            this.txtTextFormat.TabIndex = 1;
            // 
            // chkPerformGlobalAfterCaptureTasks
            // 
            this.chkPerformGlobalAfterCaptureTasks.AutoSize = true;
            this.chkPerformGlobalAfterCaptureTasks.Location = new System.Drawing.Point(16, 48);
            this.chkPerformGlobalAfterCaptureTasks.Name = "chkPerformGlobalAfterCaptureTasks";
            this.chkPerformGlobalAfterCaptureTasks.Size = new System.Drawing.Size(455, 17);
            this.chkPerformGlobalAfterCaptureTasks.TabIndex = 1;
            this.chkPerformGlobalAfterCaptureTasks.Text = "Perform global After Capture Tasks ( to configure go to Settings > Configuration " +
    "> Capture ) ";
            this.chkPerformGlobalAfterCaptureTasks.UseVisualStyleBackColor = true;
            this.chkPerformGlobalAfterCaptureTasks.CheckedChanged += new System.EventHandler(this.chkPerformGlobalAfterCaptureTasks_CheckedChanged);
            // 
            // cboCapture
            // 
            this.cboCapture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCapture.FormattingEnabled = true;
            this.cboCapture.Location = new System.Drawing.Point(16, 16);
            this.cboCapture.Name = "cboCapture";
            this.cboCapture.Size = new System.Drawing.Size(384, 21);
            this.cboCapture.TabIndex = 0;
            this.cboCapture.SelectedIndexChanged += new System.EventHandler(this.cboCapture_SelectedIndexChanged);
            // 
            // tpAfterCapture
            // 
            this.tpAfterCapture.Controls.Add(this.ucAfterCaptureTasks);
            this.tpAfterCapture.Location = new System.Drawing.Point(4, 22);
            this.tpAfterCapture.Name = "tpAfterCapture";
            this.tpAfterCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tpAfterCapture.Size = new System.Drawing.Size(654, 374);
            this.tpAfterCapture.TabIndex = 1;
            this.tpAfterCapture.Text = "After Capture";
            this.tpAfterCapture.UseVisualStyleBackColor = true;
            // 
            // ucAfterCaptureTasks
            // 
            this.ucAfterCaptureTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterCaptureTasks.Location = new System.Drawing.Point(3, 3);
            this.ucAfterCaptureTasks.Name = "ucAfterCaptureTasks";
            this.ucAfterCaptureTasks.Size = new System.Drawing.Size(648, 368);
            this.ucAfterCaptureTasks.TabIndex = 0;
            // 
            // tpRunExternalPrograms
            // 
            this.tpRunExternalPrograms.Controls.Add(this.btnActionsEdit);
            this.tpRunExternalPrograms.Controls.Add(this.btnActionsRemove);
            this.tpRunExternalPrograms.Controls.Add(this.btnActionsAdd);
            this.tpRunExternalPrograms.Controls.Add(this.lvActions);
            this.tpRunExternalPrograms.Location = new System.Drawing.Point(4, 22);
            this.tpRunExternalPrograms.Name = "tpRunExternalPrograms";
            this.tpRunExternalPrograms.Padding = new System.Windows.Forms.Padding(3);
            this.tpRunExternalPrograms.Size = new System.Drawing.Size(654, 374);
            this.tpRunExternalPrograms.TabIndex = 2;
            this.tpRunExternalPrograms.Text = "External Programs";
            this.tpRunExternalPrograms.UseVisualStyleBackColor = true;
            // 
            // btnActionsEdit
            // 
            this.btnActionsEdit.Location = new System.Drawing.Point(88, 8);
            this.btnActionsEdit.Name = "btnActionsEdit";
            this.btnActionsEdit.Size = new System.Drawing.Size(75, 23);
            this.btnActionsEdit.TabIndex = 1;
            this.btnActionsEdit.Text = "Edit...";
            this.btnActionsEdit.UseVisualStyleBackColor = true;
            this.btnActionsEdit.Click += new System.EventHandler(this.btnActionsEdit_Click);
            // 
            // btnActionsRemove
            // 
            this.btnActionsRemove.Location = new System.Drawing.Point(168, 8);
            this.btnActionsRemove.Name = "btnActionsRemove";
            this.btnActionsRemove.Size = new System.Drawing.Size(75, 23);
            this.btnActionsRemove.TabIndex = 2;
            this.btnActionsRemove.Text = "Remove";
            this.btnActionsRemove.UseVisualStyleBackColor = true;
            this.btnActionsRemove.Click += new System.EventHandler(this.btnActionsRemove_Click);
            // 
            // btnActionsAdd
            // 
            this.btnActionsAdd.Location = new System.Drawing.Point(8, 8);
            this.btnActionsAdd.Name = "btnActionsAdd";
            this.btnActionsAdd.Size = new System.Drawing.Size(75, 23);
            this.btnActionsAdd.TabIndex = 0;
            this.btnActionsAdd.Text = "Add...";
            this.btnActionsAdd.UseVisualStyleBackColor = true;
            this.btnActionsAdd.Click += new System.EventHandler(this.btnActionsAdd_Click);
            // 
            // lvActions
            // 
            this.lvActions.CheckBoxes = true;
            this.lvActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chActionsName,
            this.chActionsPath,
            this.chActionsArgs});
            this.lvActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvActions.FullRowSelect = true;
            this.lvActions.Location = new System.Drawing.Point(3, 40);
            this.lvActions.MultiSelect = false;
            this.lvActions.Name = "lvActions";
            this.lvActions.Size = new System.Drawing.Size(648, 331);
            this.lvActions.TabIndex = 3;
            this.lvActions.UseCompatibleStateImageBehavior = false;
            this.lvActions.View = System.Windows.Forms.View.Details;
            this.lvActions.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvActions_ItemChecked);
            // 
            // chActionsName
            // 
            this.chActionsName.Text = "Name";
            this.chActionsName.Width = 100;
            // 
            // chActionsPath
            // 
            this.chActionsPath.Text = "Path";
            this.chActionsPath.Width = 250;
            // 
            // chActionsArgs
            // 
            this.chActionsArgs.Text = "Args";
            this.chActionsArgs.Width = 134;
            // 
            // tpShare
            // 
            this.tpShare.Controls.Add(this.tableLayoutPanel1);
            this.tpShare.Location = new System.Drawing.Point(4, 22);
            this.tpShare.Name = "tpShare";
            this.tpShare.Padding = new System.Windows.Forms.Padding(3);
            this.tpShare.Size = new System.Drawing.Size(654, 374);
            this.tpShare.TabIndex = 3;
            this.tpShare.Text = "Share";
            this.tpShare.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.gbFileUploaders, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbImageUploaders, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbTextUploaders, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 364);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbFileUploaders
            // 
            this.gbFileUploaders.Controls.Add(this.flpFileUploaders);
            this.gbFileUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFileUploaders.Location = new System.Drawing.Point(3, 3);
            this.gbFileUploaders.Name = "gbFileUploaders";
            this.gbFileUploaders.Size = new System.Drawing.Size(208, 358);
            this.gbFileUploaders.TabIndex = 0;
            this.gbFileUploaders.TabStop = false;
            this.gbFileUploaders.Text = "File uploaders";
            // 
            // flpFileUploaders
            // 
            this.flpFileUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFileUploaders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFileUploaders.Location = new System.Drawing.Point(3, 16);
            this.flpFileUploaders.Name = "flpFileUploaders";
            this.flpFileUploaders.Size = new System.Drawing.Size(202, 339);
            this.flpFileUploaders.TabIndex = 0;
            // 
            // gbImageUploaders
            // 
            this.gbImageUploaders.Controls.Add(this.flpImageUploaders);
            this.gbImageUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbImageUploaders.Location = new System.Drawing.Point(217, 3);
            this.gbImageUploaders.Name = "gbImageUploaders";
            this.gbImageUploaders.Size = new System.Drawing.Size(208, 358);
            this.gbImageUploaders.TabIndex = 1;
            this.gbImageUploaders.TabStop = false;
            this.gbImageUploaders.Text = "Image uploaders";
            // 
            // flpImageUploaders
            // 
            this.flpImageUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImageUploaders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpImageUploaders.Location = new System.Drawing.Point(3, 16);
            this.flpImageUploaders.Name = "flpImageUploaders";
            this.flpImageUploaders.Size = new System.Drawing.Size(202, 339);
            this.flpImageUploaders.TabIndex = 0;
            // 
            // gbTextUploaders
            // 
            this.gbTextUploaders.Controls.Add(this.flpTextUploaders);
            this.gbTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTextUploaders.Location = new System.Drawing.Point(431, 3);
            this.gbTextUploaders.Name = "gbTextUploaders";
            this.gbTextUploaders.Size = new System.Drawing.Size(210, 358);
            this.gbTextUploaders.TabIndex = 2;
            this.gbTextUploaders.TabStop = false;
            this.gbTextUploaders.Text = "Text uploaders";
            // 
            // flpTextUploaders
            // 
            this.flpTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTextUploaders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTextUploaders.Location = new System.Drawing.Point(3, 16);
            this.flpTextUploaders.Name = "flpTextUploaders";
            this.flpTextUploaders.Size = new System.Drawing.Size(204, 339);
            this.flpTextUploaders.TabIndex = 0;
            // 
            // tpSummary
            // 
            this.tpSummary.Controls.Add(this.lblSummary);
            this.tpSummary.Location = new System.Drawing.Point(4, 22);
            this.tpSummary.Name = "tpSummary";
            this.tpSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tpSummary.Size = new System.Drawing.Size(654, 374);
            this.tpSummary.TabIndex = 4;
            this.tpSummary.Text = "Summary";
            this.tpSummary.UseVisualStyleBackColor = true;
            // 
            // lblSummary
            // 
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(3, 3);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(648, 368);
            this.lblSummary.TabIndex = 0;
            // 
            // WindowWorkflow
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(688, 480);
            this.Controls.Add(this.tcWorkflow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(696, 512);
            this.Name = "WindowWorkflow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WindowWorkflow";
            this.Resize += new System.EventHandler(this.WindowWorkflow_Resize);
            this.tcWorkflow.ResumeLayout(false);
            this.tpCapture.ResumeLayout(false);
            this.tpCapture.PerformLayout();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.tpAfterCapture.ResumeLayout(false);
            this.tpRunExternalPrograms.ResumeLayout(false);
            this.tpShare.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbFileUploaders.ResumeLayout(false);
            this.gbImageUploaders.ResumeLayout(false);
            this.gbTextUploaders.ResumeLayout(false);
            this.tpSummary.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tcWorkflow;
        private System.Windows.Forms.TabPage tpRunExternalPrograms;
        private System.Windows.Forms.Button btnActionsEdit;
        private System.Windows.Forms.Button btnActionsRemove;
        private System.Windows.Forms.Button btnActionsAdd;
        private MyListView lvActions;
        private System.Windows.Forms.ColumnHeader chActionsName;
        private System.Windows.Forms.ColumnHeader chActionsPath;
        private System.Windows.Forms.ColumnHeader chActionsArgs;
        private System.Windows.Forms.TabPage tpAfterCapture;
        private UserControls.AfterCatureTasksUI ucAfterCaptureTasks;
        private System.Windows.Forms.TabPage tpSummary;
        private System.Windows.Forms.ComboBox cboCapture;
        private System.Windows.Forms.TabPage tpCapture;
        private System.Windows.Forms.CheckBox chkPerformGlobalAfterCaptureTasks;
        private System.Windows.Forms.TabPage tpShare;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flpFileUploaders;
        private System.Windows.Forms.FlowLayoutPanel flpImageUploaders;
        private System.Windows.Forms.FlowLayoutPanel flpTextUploaders;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label lblTextFormat;
        private System.Windows.Forms.TextBox txtTextFormat;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.GroupBox gbFileUploaders;
        private System.Windows.Forms.GroupBox gbImageUploaders;
        private System.Windows.Forms.GroupBox gbTextUploaders;

    }
}