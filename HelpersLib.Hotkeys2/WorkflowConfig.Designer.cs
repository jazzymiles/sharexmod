using HelpersLib.UserControls;
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.lblTextFormat = new System.Windows.Forms.Label();
            this.txtTextFormat = new System.Windows.Forms.TextBox();
            this.chkPerformGlobalAfterCaptureTasks = new System.Windows.Forms.CheckBox();
            this.cboCapture = new System.Windows.Forms.ComboBox();
            this.tpAfterCapture = new System.Windows.Forms.TabPage();
            this.tlpTasks = new System.Windows.Forms.TableLayoutPanel();
            this.gbAfterCaptureTasks = new System.Windows.Forms.GroupBox();
            this.ucAfterCaptureTasks = new HelpersLib.UserControls.AfterCaptureTasksUI();
            this.gbAfterUploadTasks = new System.Windows.Forms.GroupBox();
            this.ucAfterUploadTasks = new HelpersLib.UserControls.AfterUploadTasksUI();
            this.tpRunExternalPrograms = new System.Windows.Forms.TabPage();
            this.btnActionsEdit = new System.Windows.Forms.Button();
            this.btnActionsRemove = new System.Windows.Forms.Button();
            this.btnActionsAdd = new System.Windows.Forms.Button();
            this.lvActions = new HelpersLib.MyListView();
            this.chActionsName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActionsPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActionsArgs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpUpload = new System.Windows.Forms.TabPage();
            this.tlpUpload = new System.Windows.Forms.TableLayoutPanel();
            this.gbFileUploaders = new System.Windows.Forms.GroupBox();
            this.flpFileUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.gbImageUploaders = new System.Windows.Forms.GroupBox();
            this.flpImageUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.gbTextUploaders = new System.Windows.Forms.GroupBox();
            this.flpTextUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.tpUpload2 = new System.Windows.Forms.TabPage();
            this.tlpUpload2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbFileUploaders2 = new System.Windows.Forms.GroupBox();
            this.flpFileUploaders2 = new System.Windows.Forms.FlowLayoutPanel();
            this.gpImageUploaders2 = new System.Windows.Forms.GroupBox();
            this.flpImageUploaders2 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbTextUploaders2 = new System.Windows.Forms.GroupBox();
            this.flpTextUploaders2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tpShare = new System.Windows.Forms.TabPage();
            this.tlpShare = new System.Windows.Forms.TableLayoutPanel();
            this.gbUrlShorteners = new System.Windows.Forms.GroupBox();
            this.flpUrlShorteners = new System.Windows.Forms.FlowLayoutPanel();
            this.gbSocialNetworkingServices = new System.Windows.Forms.GroupBox();
            this.flpSocialNetworkingServices = new System.Windows.Forms.FlowLayoutPanel();
            this.tpSummary = new System.Windows.Forms.TabPage();
            this.lblSummary = new System.Windows.Forms.Label();
            this.tcWorkflow.SuspendLayout();
            this.tpCapture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbSettings.SuspendLayout();
            this.tpAfterCapture.SuspendLayout();
            this.tlpTasks.SuspendLayout();
            this.gbAfterCaptureTasks.SuspendLayout();
            this.gbAfterUploadTasks.SuspendLayout();
            this.tpRunExternalPrograms.SuspendLayout();
            this.tpUpload.SuspendLayout();
            this.tlpUpload.SuspendLayout();
            this.gbFileUploaders.SuspendLayout();
            this.gbImageUploaders.SuspendLayout();
            this.gbTextUploaders.SuspendLayout();
            this.tpUpload2.SuspendLayout();
            this.tlpUpload2.SuspendLayout();
            this.gbFileUploaders2.SuspendLayout();
            this.gpImageUploaders2.SuspendLayout();
            this.gbTextUploaders2.SuspendLayout();
            this.tpShare.SuspendLayout();
            this.tlpShare.SuspendLayout();
            this.gbUrlShorteners.SuspendLayout();
            this.gbSocialNetworkingServices.SuspendLayout();
            this.tpSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BackColor = System.Drawing.SystemColors.Info;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(11, 10);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(881, 29);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.Text = "New workflow";
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(787, 551);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 27);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(680, 551);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 27);
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
            this.tcWorkflow.Controls.Add(this.tpUpload);
            this.tcWorkflow.Controls.Add(this.tpUpload2);
            this.tcWorkflow.Controls.Add(this.tpShare);
            this.tcWorkflow.Controls.Add(this.tpSummary);
            this.tcWorkflow.Location = new System.Drawing.Point(11, 49);
            this.tcWorkflow.Margin = new System.Windows.Forms.Padding(4);
            this.tcWorkflow.Name = "tcWorkflow";
            this.tcWorkflow.SelectedIndex = 0;
            this.tcWorkflow.Size = new System.Drawing.Size(883, 492);
            this.tcWorkflow.TabIndex = 1;
            this.tcWorkflow.SelectedIndexChanged += new System.EventHandler(this.tcWorkflow_SelectedIndexChanged);
            // 
            // tpCapture
            // 
            this.tpCapture.Controls.Add(this.pictureBox1);
            this.tpCapture.Controls.Add(this.gbSettings);
            this.tpCapture.Controls.Add(this.chkPerformGlobalAfterCaptureTasks);
            this.tpCapture.Controls.Add(this.cboCapture);
            this.tpCapture.Location = new System.Drawing.Point(4, 25);
            this.tpCapture.Margin = new System.Windows.Forms.Padding(4);
            this.tpCapture.Name = "tpCapture";
            this.tpCapture.Padding = new System.Windows.Forms.Padding(4);
            this.tpCapture.Size = new System.Drawing.Size(875, 463);
            this.tpCapture.TabIndex = 0;
            this.tpCapture.Text = "Capture";
            this.tpCapture.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HelpersLib.Hotkeys2.Properties.Resources.camera_add;
            this.pictureBox1.Location = new System.Drawing.Point(299, 59);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.lblTextFormat);
            this.gbSettings.Controls.Add(this.txtTextFormat);
            this.gbSettings.Location = new System.Drawing.Point(21, 98);
            this.gbSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbSettings.Size = new System.Drawing.Size(821, 89);
            this.gbSettings.TabIndex = 2;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Customize text uploader settings";
            // 
            // lblTextFormat
            // 
            this.lblTextFormat.AutoSize = true;
            this.lblTextFormat.Location = new System.Drawing.Point(21, 30);
            this.lblTextFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTextFormat.Name = "lblTextFormat";
            this.lblTextFormat.Size = new System.Drawing.Size(216, 17);
            this.lblTextFormat.TabIndex = 0;
            this.lblTextFormat.Text = "Text format e.g. csharp, cpp, etc.";
            // 
            // txtTextFormat
            // 
            this.txtTextFormat.Location = new System.Drawing.Point(21, 49);
            this.txtTextFormat.Margin = new System.Windows.Forms.Padding(4);
            this.txtTextFormat.Name = "txtTextFormat";
            this.txtTextFormat.Size = new System.Drawing.Size(396, 22);
            this.txtTextFormat.TabIndex = 1;
            // 
            // chkPerformGlobalAfterCaptureTasks
            // 
            this.chkPerformGlobalAfterCaptureTasks.AutoSize = true;
            this.chkPerformGlobalAfterCaptureTasks.Location = new System.Drawing.Point(21, 59);
            this.chkPerformGlobalAfterCaptureTasks.Margin = new System.Windows.Forms.Padding(4);
            this.chkPerformGlobalAfterCaptureTasks.Name = "chkPerformGlobalAfterCaptureTasks";
            this.chkPerformGlobalAfterCaptureTasks.Size = new System.Drawing.Size(522, 21);
            this.chkPerformGlobalAfterCaptureTasks.TabIndex = 1;
            this.chkPerformGlobalAfterCaptureTasks.Text = "Apply default settings ( access the icon         from the main form to configure " +
    ") ";
            this.chkPerformGlobalAfterCaptureTasks.UseVisualStyleBackColor = true;
            this.chkPerformGlobalAfterCaptureTasks.CheckedChanged += new System.EventHandler(this.chkPerformGlobalAfterCaptureTasks_CheckedChanged);
            // 
            // cboCapture
            // 
            this.cboCapture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCapture.FormattingEnabled = true;
            this.cboCapture.Location = new System.Drawing.Point(21, 20);
            this.cboCapture.Margin = new System.Windows.Forms.Padding(4);
            this.cboCapture.Name = "cboCapture";
            this.cboCapture.Size = new System.Drawing.Size(511, 24);
            this.cboCapture.TabIndex = 0;
            this.cboCapture.SelectedIndexChanged += new System.EventHandler(this.cboCapture_SelectedIndexChanged);
            // 
            // tpAfterCapture
            // 
            this.tpAfterCapture.Controls.Add(this.tlpTasks);
            this.tpAfterCapture.Location = new System.Drawing.Point(4, 25);
            this.tpAfterCapture.Margin = new System.Windows.Forms.Padding(4);
            this.tpAfterCapture.Name = "tpAfterCapture";
            this.tpAfterCapture.Padding = new System.Windows.Forms.Padding(4);
            this.tpAfterCapture.Size = new System.Drawing.Size(875, 463);
            this.tpAfterCapture.TabIndex = 1;
            this.tpAfterCapture.Text = "Tasks";
            this.tpAfterCapture.UseVisualStyleBackColor = true;
            // 
            // tlpTasks
            // 
            this.tlpTasks.ColumnCount = 2;
            this.tlpTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpTasks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpTasks.Controls.Add(this.gbAfterCaptureTasks, 0, 0);
            this.tlpTasks.Controls.Add(this.gbAfterUploadTasks, 1, 0);
            this.tlpTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTasks.Location = new System.Drawing.Point(4, 4);
            this.tlpTasks.Margin = new System.Windows.Forms.Padding(4);
            this.tlpTasks.Name = "tlpTasks";
            this.tlpTasks.RowCount = 1;
            this.tlpTasks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 455F));
            this.tlpTasks.Size = new System.Drawing.Size(867, 455);
            this.tlpTasks.TabIndex = 0;
            // 
            // gbAfterCaptureTasks
            // 
            this.gbAfterCaptureTasks.Controls.Add(this.ucAfterCaptureTasks);
            this.gbAfterCaptureTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAfterCaptureTasks.Location = new System.Drawing.Point(4, 4);
            this.gbAfterCaptureTasks.Margin = new System.Windows.Forms.Padding(4);
            this.gbAfterCaptureTasks.Name = "gbAfterCaptureTasks";
            this.gbAfterCaptureTasks.Padding = new System.Windows.Forms.Padding(4);
            this.gbAfterCaptureTasks.Size = new System.Drawing.Size(425, 447);
            this.gbAfterCaptureTasks.TabIndex = 0;
            this.gbAfterCaptureTasks.TabStop = false;
            this.gbAfterCaptureTasks.Text = "After Capture Tasks";
            // 
            // ucAfterCaptureTasks
            // 
            this.ucAfterCaptureTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterCaptureTasks.Location = new System.Drawing.Point(4, 19);
            this.ucAfterCaptureTasks.Margin = new System.Windows.Forms.Padding(5);
            this.ucAfterCaptureTasks.Name = "ucAfterCaptureTasks";
            this.ucAfterCaptureTasks.Size = new System.Drawing.Size(417, 424);
            this.ucAfterCaptureTasks.TabIndex = 0;
            // 
            // gbAfterUploadTasks
            // 
            this.gbAfterUploadTasks.Controls.Add(this.ucAfterUploadTasks);
            this.gbAfterUploadTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAfterUploadTasks.Location = new System.Drawing.Point(437, 4);
            this.gbAfterUploadTasks.Margin = new System.Windows.Forms.Padding(4);
            this.gbAfterUploadTasks.Name = "gbAfterUploadTasks";
            this.gbAfterUploadTasks.Padding = new System.Windows.Forms.Padding(4);
            this.gbAfterUploadTasks.Size = new System.Drawing.Size(426, 447);
            this.gbAfterUploadTasks.TabIndex = 1;
            this.gbAfterUploadTasks.TabStop = false;
            this.gbAfterUploadTasks.Text = "After Upload Tasks";
            // 
            // ucAfterUploadTasks
            // 
            this.ucAfterUploadTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterUploadTasks.Location = new System.Drawing.Point(4, 19);
            this.ucAfterUploadTasks.Margin = new System.Windows.Forms.Padding(5);
            this.ucAfterUploadTasks.Name = "ucAfterUploadTasks";
            this.ucAfterUploadTasks.Size = new System.Drawing.Size(418, 424);
            this.ucAfterUploadTasks.TabIndex = 0;
            // 
            // tpRunExternalPrograms
            // 
            this.tpRunExternalPrograms.Controls.Add(this.btnActionsEdit);
            this.tpRunExternalPrograms.Controls.Add(this.btnActionsRemove);
            this.tpRunExternalPrograms.Controls.Add(this.btnActionsAdd);
            this.tpRunExternalPrograms.Controls.Add(this.lvActions);
            this.tpRunExternalPrograms.Location = new System.Drawing.Point(4, 25);
            this.tpRunExternalPrograms.Margin = new System.Windows.Forms.Padding(4);
            this.tpRunExternalPrograms.Name = "tpRunExternalPrograms";
            this.tpRunExternalPrograms.Padding = new System.Windows.Forms.Padding(4);
            this.tpRunExternalPrograms.Size = new System.Drawing.Size(875, 463);
            this.tpRunExternalPrograms.TabIndex = 2;
            this.tpRunExternalPrograms.Text = "External Programs";
            this.tpRunExternalPrograms.UseVisualStyleBackColor = true;
            // 
            // btnActionsEdit
            // 
            this.btnActionsEdit.Location = new System.Drawing.Point(117, 10);
            this.btnActionsEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionsEdit.Name = "btnActionsEdit";
            this.btnActionsEdit.Size = new System.Drawing.Size(100, 28);
            this.btnActionsEdit.TabIndex = 1;
            this.btnActionsEdit.Text = "Edit...";
            this.btnActionsEdit.UseVisualStyleBackColor = true;
            this.btnActionsEdit.Click += new System.EventHandler(this.btnActionsEdit_Click);
            // 
            // btnActionsRemove
            // 
            this.btnActionsRemove.Location = new System.Drawing.Point(224, 10);
            this.btnActionsRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionsRemove.Name = "btnActionsRemove";
            this.btnActionsRemove.Size = new System.Drawing.Size(100, 28);
            this.btnActionsRemove.TabIndex = 2;
            this.btnActionsRemove.Text = "Remove";
            this.btnActionsRemove.UseVisualStyleBackColor = true;
            this.btnActionsRemove.Click += new System.EventHandler(this.btnActionsRemove_Click);
            // 
            // btnActionsAdd
            // 
            this.btnActionsAdd.Location = new System.Drawing.Point(11, 10);
            this.btnActionsAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnActionsAdd.Name = "btnActionsAdd";
            this.btnActionsAdd.Size = new System.Drawing.Size(100, 28);
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
            this.lvActions.Location = new System.Drawing.Point(4, 53);
            this.lvActions.Margin = new System.Windows.Forms.Padding(4);
            this.lvActions.MultiSelect = false;
            this.lvActions.Name = "lvActions";
            this.lvActions.Size = new System.Drawing.Size(867, 406);
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
            // tpUpload
            // 
            this.tpUpload.Controls.Add(this.tlpUpload);
            this.tpUpload.Location = new System.Drawing.Point(4, 25);
            this.tpUpload.Margin = new System.Windows.Forms.Padding(4);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Padding = new System.Windows.Forms.Padding(4);
            this.tpUpload.Size = new System.Drawing.Size(875, 463);
            this.tpUpload.TabIndex = 3;
            this.tpUpload.Text = "Upload";
            this.tpUpload.UseVisualStyleBackColor = true;
            // 
            // tlpUpload
            // 
            this.tlpUpload.ColumnCount = 3;
            this.tlpUpload.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpUpload.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpUpload.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpUpload.Controls.Add(this.gbFileUploaders, 2, 0);
            this.tlpUpload.Controls.Add(this.gbImageUploaders, 0, 0);
            this.tlpUpload.Controls.Add(this.gbTextUploaders, 1, 0);
            this.tlpUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUpload.Location = new System.Drawing.Point(4, 4);
            this.tlpUpload.Margin = new System.Windows.Forms.Padding(4);
            this.tlpUpload.Name = "tlpUpload";
            this.tlpUpload.RowCount = 1;
            this.tlpUpload.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 455F));
            this.tlpUpload.Size = new System.Drawing.Size(867, 455);
            this.tlpUpload.TabIndex = 0;
            // 
            // gbFileUploaders
            // 
            this.gbFileUploaders.Controls.Add(this.flpFileUploaders);
            this.gbFileUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFileUploaders.Location = new System.Drawing.Point(4, 4);
            this.gbFileUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.gbFileUploaders.Name = "gbFileUploaders";
            this.gbFileUploaders.Padding = new System.Windows.Forms.Padding(4);
            this.gbFileUploaders.Size = new System.Drawing.Size(281, 447);
            this.gbFileUploaders.TabIndex = 0;
            this.gbFileUploaders.TabStop = false;
            this.gbFileUploaders.Text = "File uploaders";
            // 
            // flpFileUploaders
            // 
            this.flpFileUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFileUploaders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFileUploaders.Location = new System.Drawing.Point(4, 19);
            this.flpFileUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.flpFileUploaders.Name = "flpFileUploaders";
            this.flpFileUploaders.Size = new System.Drawing.Size(273, 424);
            this.flpFileUploaders.TabIndex = 0;
            // 
            // gbImageUploaders
            // 
            this.gbImageUploaders.Controls.Add(this.flpImageUploaders);
            this.gbImageUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbImageUploaders.Location = new System.Drawing.Point(293, 4);
            this.gbImageUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageUploaders.Name = "gbImageUploaders";
            this.gbImageUploaders.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageUploaders.Size = new System.Drawing.Size(281, 447);
            this.gbImageUploaders.TabIndex = 1;
            this.gbImageUploaders.TabStop = false;
            this.gbImageUploaders.Text = "Image uploaders";
            // 
            // flpImageUploaders
            // 
            this.flpImageUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImageUploaders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpImageUploaders.Location = new System.Drawing.Point(4, 19);
            this.flpImageUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.flpImageUploaders.Name = "flpImageUploaders";
            this.flpImageUploaders.Size = new System.Drawing.Size(273, 424);
            this.flpImageUploaders.TabIndex = 0;
            // 
            // gbTextUploaders
            // 
            this.gbTextUploaders.Controls.Add(this.flpTextUploaders);
            this.gbTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTextUploaders.Location = new System.Drawing.Point(582, 4);
            this.gbTextUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.gbTextUploaders.Name = "gbTextUploaders";
            this.gbTextUploaders.Padding = new System.Windows.Forms.Padding(4);
            this.gbTextUploaders.Size = new System.Drawing.Size(281, 447);
            this.gbTextUploaders.TabIndex = 2;
            this.gbTextUploaders.TabStop = false;
            this.gbTextUploaders.Text = "Text uploaders";
            // 
            // flpTextUploaders
            // 
            this.flpTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTextUploaders.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTextUploaders.Location = new System.Drawing.Point(4, 19);
            this.flpTextUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.flpTextUploaders.Name = "flpTextUploaders";
            this.flpTextUploaders.Size = new System.Drawing.Size(273, 424);
            this.flpTextUploaders.TabIndex = 0;
            // 
            // tpUpload2
            // 
            this.tpUpload2.Controls.Add(this.tlpUpload2);
            this.tpUpload2.Location = new System.Drawing.Point(4, 25);
            this.tpUpload2.Name = "tpUpload2";
            this.tpUpload2.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpload2.Size = new System.Drawing.Size(875, 463);
            this.tpUpload2.TabIndex = 6;
            this.tpUpload2.Text = "Secondary Upload";
            this.tpUpload2.UseVisualStyleBackColor = true;
            // 
            // tlpUpload2
            // 
            this.tlpUpload2.ColumnCount = 3;
            this.tlpUpload2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpUpload2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpUpload2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpUpload2.Controls.Add(this.gbFileUploaders2, 2, 0);
            this.tlpUpload2.Controls.Add(this.gpImageUploaders2, 0, 0);
            this.tlpUpload2.Controls.Add(this.gbTextUploaders2, 1, 0);
            this.tlpUpload2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUpload2.Location = new System.Drawing.Point(3, 3);
            this.tlpUpload2.Margin = new System.Windows.Forms.Padding(4);
            this.tlpUpload2.Name = "tlpUpload2";
            this.tlpUpload2.RowCount = 1;
            this.tlpUpload2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 457F));
            this.tlpUpload2.Size = new System.Drawing.Size(869, 457);
            this.tlpUpload2.TabIndex = 1;
            // 
            // gbFileUploaders2
            // 
            this.gbFileUploaders2.Controls.Add(this.flpFileUploaders2);
            this.gbFileUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFileUploaders2.Location = new System.Drawing.Point(4, 4);
            this.gbFileUploaders2.Margin = new System.Windows.Forms.Padding(4);
            this.gbFileUploaders2.Name = "gbFileUploaders2";
            this.gbFileUploaders2.Padding = new System.Windows.Forms.Padding(4);
            this.gbFileUploaders2.Size = new System.Drawing.Size(281, 449);
            this.gbFileUploaders2.TabIndex = 0;
            this.gbFileUploaders2.TabStop = false;
            this.gbFileUploaders2.Text = "File uploaders";
            // 
            // flpFileUploaders2
            // 
            this.flpFileUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFileUploaders2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFileUploaders2.Location = new System.Drawing.Point(4, 19);
            this.flpFileUploaders2.Margin = new System.Windows.Forms.Padding(4);
            this.flpFileUploaders2.Name = "flpFileUploaders2";
            this.flpFileUploaders2.Size = new System.Drawing.Size(273, 426);
            this.flpFileUploaders2.TabIndex = 0;
            // 
            // gpImageUploaders2
            // 
            this.gpImageUploaders2.Controls.Add(this.flpImageUploaders2);
            this.gpImageUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpImageUploaders2.Location = new System.Drawing.Point(293, 4);
            this.gpImageUploaders2.Margin = new System.Windows.Forms.Padding(4);
            this.gpImageUploaders2.Name = "gpImageUploaders2";
            this.gpImageUploaders2.Padding = new System.Windows.Forms.Padding(4);
            this.gpImageUploaders2.Size = new System.Drawing.Size(281, 449);
            this.gpImageUploaders2.TabIndex = 1;
            this.gpImageUploaders2.TabStop = false;
            this.gpImageUploaders2.Text = "Image uploaders";
            // 
            // flpImageUploaders2
            // 
            this.flpImageUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImageUploaders2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpImageUploaders2.Location = new System.Drawing.Point(4, 19);
            this.flpImageUploaders2.Margin = new System.Windows.Forms.Padding(4);
            this.flpImageUploaders2.Name = "flpImageUploaders2";
            this.flpImageUploaders2.Size = new System.Drawing.Size(273, 426);
            this.flpImageUploaders2.TabIndex = 0;
            // 
            // gbTextUploaders2
            // 
            this.gbTextUploaders2.Controls.Add(this.flpTextUploaders2);
            this.gbTextUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTextUploaders2.Location = new System.Drawing.Point(582, 4);
            this.gbTextUploaders2.Margin = new System.Windows.Forms.Padding(4);
            this.gbTextUploaders2.Name = "gbTextUploaders2";
            this.gbTextUploaders2.Padding = new System.Windows.Forms.Padding(4);
            this.gbTextUploaders2.Size = new System.Drawing.Size(283, 449);
            this.gbTextUploaders2.TabIndex = 2;
            this.gbTextUploaders2.TabStop = false;
            this.gbTextUploaders2.Text = "Text uploaders";
            // 
            // flpTextUploaders2
            // 
            this.flpTextUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTextUploaders2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTextUploaders2.Location = new System.Drawing.Point(4, 19);
            this.flpTextUploaders2.Margin = new System.Windows.Forms.Padding(4);
            this.flpTextUploaders2.Name = "flpTextUploaders2";
            this.flpTextUploaders2.Size = new System.Drawing.Size(275, 426);
            this.flpTextUploaders2.TabIndex = 0;
            // 
            // tpShare
            // 
            this.tpShare.Controls.Add(this.tlpShare);
            this.tpShare.Location = new System.Drawing.Point(4, 25);
            this.tpShare.Margin = new System.Windows.Forms.Padding(4);
            this.tpShare.Name = "tpShare";
            this.tpShare.Padding = new System.Windows.Forms.Padding(4);
            this.tpShare.Size = new System.Drawing.Size(875, 463);
            this.tpShare.TabIndex = 4;
            this.tpShare.Text = "Share";
            this.tpShare.UseVisualStyleBackColor = true;
            // 
            // tlpShare
            // 
            this.tlpShare.ColumnCount = 2;
            this.tlpShare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpShare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpShare.Controls.Add(this.gbUrlShorteners, 0, 0);
            this.tlpShare.Controls.Add(this.gbSocialNetworkingServices, 1, 0);
            this.tlpShare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpShare.Location = new System.Drawing.Point(4, 4);
            this.tlpShare.Margin = new System.Windows.Forms.Padding(4);
            this.tlpShare.Name = "tlpShare";
            this.tlpShare.RowCount = 1;
            this.tlpShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 455F));
            this.tlpShare.Size = new System.Drawing.Size(867, 455);
            this.tlpShare.TabIndex = 0;
            // 
            // gbUrlShorteners
            // 
            this.gbUrlShorteners.Controls.Add(this.flpUrlShorteners);
            this.gbUrlShorteners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUrlShorteners.Location = new System.Drawing.Point(4, 4);
            this.gbUrlShorteners.Margin = new System.Windows.Forms.Padding(4);
            this.gbUrlShorteners.Name = "gbUrlShorteners";
            this.gbUrlShorteners.Padding = new System.Windows.Forms.Padding(4);
            this.gbUrlShorteners.Size = new System.Drawing.Size(425, 447);
            this.gbUrlShorteners.TabIndex = 0;
            this.gbUrlShorteners.TabStop = false;
            this.gbUrlShorteners.Text = "URL Shorteners";
            // 
            // flpUrlShorteners
            // 
            this.flpUrlShorteners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpUrlShorteners.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpUrlShorteners.Location = new System.Drawing.Point(4, 19);
            this.flpUrlShorteners.Margin = new System.Windows.Forms.Padding(4);
            this.flpUrlShorteners.Name = "flpUrlShorteners";
            this.flpUrlShorteners.Size = new System.Drawing.Size(417, 424);
            this.flpUrlShorteners.TabIndex = 0;
            // 
            // gbSocialNetworkingServices
            // 
            this.gbSocialNetworkingServices.Controls.Add(this.flpSocialNetworkingServices);
            this.gbSocialNetworkingServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSocialNetworkingServices.Location = new System.Drawing.Point(437, 4);
            this.gbSocialNetworkingServices.Margin = new System.Windows.Forms.Padding(4);
            this.gbSocialNetworkingServices.Name = "gbSocialNetworkingServices";
            this.gbSocialNetworkingServices.Padding = new System.Windows.Forms.Padding(4);
            this.gbSocialNetworkingServices.Size = new System.Drawing.Size(426, 447);
            this.gbSocialNetworkingServices.TabIndex = 1;
            this.gbSocialNetworkingServices.TabStop = false;
            this.gbSocialNetworkingServices.Text = "Social Networking Services";
            // 
            // flpSocialNetworkingServices
            // 
            this.flpSocialNetworkingServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSocialNetworkingServices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSocialNetworkingServices.Location = new System.Drawing.Point(4, 19);
            this.flpSocialNetworkingServices.Margin = new System.Windows.Forms.Padding(4);
            this.flpSocialNetworkingServices.Name = "flpSocialNetworkingServices";
            this.flpSocialNetworkingServices.Size = new System.Drawing.Size(418, 424);
            this.flpSocialNetworkingServices.TabIndex = 0;
            // 
            // tpSummary
            // 
            this.tpSummary.Controls.Add(this.lblSummary);
            this.tpSummary.Location = new System.Drawing.Point(4, 25);
            this.tpSummary.Margin = new System.Windows.Forms.Padding(4);
            this.tpSummary.Name = "tpSummary";
            this.tpSummary.Padding = new System.Windows.Forms.Padding(4);
            this.tpSummary.Size = new System.Drawing.Size(875, 463);
            this.tpSummary.TabIndex = 5;
            this.tpSummary.Text = "Summary";
            this.tpSummary.UseVisualStyleBackColor = true;
            // 
            // lblSummary
            // 
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(4, 4);
            this.lblSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(867, 455);
            this.lblSummary.TabIndex = 0;
            // 
            // WindowWorkflow
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(917, 591);
            this.Controls.Add(this.tcWorkflow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(922, 619);
            this.Name = "WindowWorkflow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WindowWorkflow";
            this.Resize += new System.EventHandler(this.WindowWorkflow_Resize);
            this.tcWorkflow.ResumeLayout(false);
            this.tpCapture.ResumeLayout(false);
            this.tpCapture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.tpAfterCapture.ResumeLayout(false);
            this.tlpTasks.ResumeLayout(false);
            this.gbAfterCaptureTasks.ResumeLayout(false);
            this.gbAfterUploadTasks.ResumeLayout(false);
            this.tpRunExternalPrograms.ResumeLayout(false);
            this.tpUpload.ResumeLayout(false);
            this.tlpUpload.ResumeLayout(false);
            this.gbFileUploaders.ResumeLayout(false);
            this.gbImageUploaders.ResumeLayout(false);
            this.gbTextUploaders.ResumeLayout(false);
            this.tpUpload2.ResumeLayout(false);
            this.tlpUpload2.ResumeLayout(false);
            this.gbFileUploaders2.ResumeLayout(false);
            this.gpImageUploaders2.ResumeLayout(false);
            this.gbTextUploaders2.ResumeLayout(false);
            this.tpShare.ResumeLayout(false);
            this.tlpShare.ResumeLayout(false);
            this.gbUrlShorteners.ResumeLayout(false);
            this.gbSocialNetworkingServices.ResumeLayout(false);
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
        private AfterCaptureTasksUI ucAfterCaptureTasks;
        private System.Windows.Forms.TabPage tpSummary;
        private System.Windows.Forms.ComboBox cboCapture;
        private System.Windows.Forms.TabPage tpCapture;
        private System.Windows.Forms.CheckBox chkPerformGlobalAfterCaptureTasks;
        private System.Windows.Forms.TabPage tpUpload;
        private System.Windows.Forms.TableLayoutPanel tlpUpload;
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
        private System.Windows.Forms.TabPage tpShare;
        private System.Windows.Forms.TableLayoutPanel tlpShare;
        private System.Windows.Forms.GroupBox gbUrlShorteners;
        private System.Windows.Forms.FlowLayoutPanel flpUrlShorteners;
        private System.Windows.Forms.GroupBox gbSocialNetworkingServices;
        private System.Windows.Forms.FlowLayoutPanel flpSocialNetworkingServices;
        private System.Windows.Forms.TableLayoutPanel tlpTasks;
        private System.Windows.Forms.GroupBox gbAfterCaptureTasks;
        private System.Windows.Forms.GroupBox gbAfterUploadTasks;
        private UserControls.AfterUploadTasksUI ucAfterUploadTasks;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tpUpload2;
        private System.Windows.Forms.TableLayoutPanel tlpUpload2;
        private System.Windows.Forms.GroupBox gbFileUploaders2;
        private System.Windows.Forms.FlowLayoutPanel flpFileUploaders2;
        private System.Windows.Forms.GroupBox gpImageUploaders2;
        private System.Windows.Forms.FlowLayoutPanel flpImageUploaders2;
        private System.Windows.Forms.GroupBox gbTextUploaders2;
        private System.Windows.Forms.FlowLayoutPanel flpTextUploaders2;

    }
}