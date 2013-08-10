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
            this.cboCapture = new System.Windows.Forms.ComboBox();
            this.tpAfterCapture = new System.Windows.Forms.TabPage();
            this.tlpTasks = new System.Windows.Forms.TableLayoutPanel();
            this.gbAfterCaptureTasks = new System.Windows.Forms.GroupBox();
            this.gbAfterUploadTasks = new System.Windows.Forms.GroupBox();
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
            this.pgWorkflow = new System.Windows.Forms.PropertyGrid();
            this.ucAfterCaptureTasks = new HelpersLib.UserControls.AfterCaptureTasksUI();
            this.ucAfterUploadTasks = new HelpersLib.UserControls.AfterUploadTasksUI();
            this.tcWorkflow.SuspendLayout();
            this.tpCapture.SuspendLayout();
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
            this.tcWorkflow.Controls.Add(this.tpUpload);
            this.tcWorkflow.Controls.Add(this.tpUpload2);
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
            this.tpCapture.Controls.Add(this.pgWorkflow);
            this.tpCapture.Controls.Add(this.cboCapture);
            this.tpCapture.Location = new System.Drawing.Point(4, 22);
            this.tpCapture.Name = "tpCapture";
            this.tpCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tpCapture.Size = new System.Drawing.Size(654, 374);
            this.tpCapture.TabIndex = 0;
            this.tpCapture.Text = "Capture";
            this.tpCapture.UseVisualStyleBackColor = true;
            // 
            // cboCapture
            // 
            this.cboCapture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCapture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCapture.FormattingEnabled = true;
            this.cboCapture.Location = new System.Drawing.Point(16, 16);
            this.cboCapture.Name = "cboCapture";
            this.cboCapture.Size = new System.Drawing.Size(624, 21);
            this.cboCapture.TabIndex = 0;
            this.cboCapture.SelectedIndexChanged += new System.EventHandler(this.cboCapture_SelectedIndexChanged);
            // 
            // tpAfterCapture
            // 
            this.tpAfterCapture.Controls.Add(this.tlpTasks);
            this.tpAfterCapture.Location = new System.Drawing.Point(4, 22);
            this.tpAfterCapture.Name = "tpAfterCapture";
            this.tpAfterCapture.Padding = new System.Windows.Forms.Padding(3);
            this.tpAfterCapture.Size = new System.Drawing.Size(654, 374);
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
            this.tlpTasks.Location = new System.Drawing.Point(3, 3);
            this.tlpTasks.Name = "tlpTasks";
            this.tlpTasks.RowCount = 1;
            this.tlpTasks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 370F));
            this.tlpTasks.Size = new System.Drawing.Size(648, 368);
            this.tlpTasks.TabIndex = 0;
            // 
            // gbAfterCaptureTasks
            // 
            this.gbAfterCaptureTasks.Controls.Add(this.ucAfterCaptureTasks);
            this.gbAfterCaptureTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAfterCaptureTasks.Location = new System.Drawing.Point(3, 3);
            this.gbAfterCaptureTasks.Name = "gbAfterCaptureTasks";
            this.gbAfterCaptureTasks.Size = new System.Drawing.Size(318, 364);
            this.gbAfterCaptureTasks.TabIndex = 0;
            this.gbAfterCaptureTasks.TabStop = false;
            this.gbAfterCaptureTasks.Text = "After Capture Tasks";
            // 
            // gbAfterUploadTasks
            // 
            this.gbAfterUploadTasks.Controls.Add(this.ucAfterUploadTasks);
            this.gbAfterUploadTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAfterUploadTasks.Location = new System.Drawing.Point(327, 3);
            this.gbAfterUploadTasks.Name = "gbAfterUploadTasks";
            this.gbAfterUploadTasks.Size = new System.Drawing.Size(318, 364);
            this.gbAfterUploadTasks.TabIndex = 1;
            this.gbAfterUploadTasks.TabStop = false;
            this.gbAfterUploadTasks.Text = "After Upload Tasks";
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
            // tpUpload
            // 
            this.tpUpload.Controls.Add(this.tlpUpload);
            this.tpUpload.Location = new System.Drawing.Point(4, 22);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpUpload.Size = new System.Drawing.Size(654, 374);
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
            this.tlpUpload.Location = new System.Drawing.Point(3, 3);
            this.tlpUpload.Name = "tlpUpload";
            this.tlpUpload.RowCount = 1;
            this.tlpUpload.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 370F));
            this.tlpUpload.Size = new System.Drawing.Size(648, 368);
            this.tlpUpload.TabIndex = 0;
            // 
            // gbFileUploaders
            // 
            this.gbFileUploaders.Controls.Add(this.flpFileUploaders);
            this.gbFileUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFileUploaders.Location = new System.Drawing.Point(435, 3);
            this.gbFileUploaders.Name = "gbFileUploaders";
            this.gbFileUploaders.Size = new System.Drawing.Size(210, 364);
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
            this.flpFileUploaders.Size = new System.Drawing.Size(204, 345);
            this.flpFileUploaders.TabIndex = 0;
            // 
            // gbImageUploaders
            // 
            this.gbImageUploaders.Controls.Add(this.flpImageUploaders);
            this.gbImageUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbImageUploaders.Location = new System.Drawing.Point(3, 3);
            this.gbImageUploaders.Name = "gbImageUploaders";
            this.gbImageUploaders.Size = new System.Drawing.Size(210, 364);
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
            this.flpImageUploaders.Size = new System.Drawing.Size(204, 345);
            this.flpImageUploaders.TabIndex = 0;
            // 
            // gbTextUploaders
            // 
            this.gbTextUploaders.Controls.Add(this.flpTextUploaders);
            this.gbTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTextUploaders.Location = new System.Drawing.Point(219, 3);
            this.gbTextUploaders.Name = "gbTextUploaders";
            this.gbTextUploaders.Size = new System.Drawing.Size(210, 364);
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
            this.flpTextUploaders.Size = new System.Drawing.Size(204, 345);
            this.flpTextUploaders.TabIndex = 0;
            // 
            // tpUpload2
            // 
            this.tpUpload2.Controls.Add(this.tlpUpload2);
            this.tpUpload2.Location = new System.Drawing.Point(4, 22);
            this.tpUpload2.Margin = new System.Windows.Forms.Padding(2);
            this.tpUpload2.Name = "tpUpload2";
            this.tpUpload2.Padding = new System.Windows.Forms.Padding(2);
            this.tpUpload2.Size = new System.Drawing.Size(654, 374);
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
            this.tlpUpload2.Location = new System.Drawing.Point(2, 2);
            this.tlpUpload2.Name = "tlpUpload2";
            this.tlpUpload2.RowCount = 1;
            this.tlpUpload2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 371F));
            this.tlpUpload2.Size = new System.Drawing.Size(650, 370);
            this.tlpUpload2.TabIndex = 1;
            // 
            // gbFileUploaders2
            // 
            this.gbFileUploaders2.Controls.Add(this.flpFileUploaders2);
            this.gbFileUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFileUploaders2.Location = new System.Drawing.Point(435, 3);
            this.gbFileUploaders2.Name = "gbFileUploaders2";
            this.gbFileUploaders2.Size = new System.Drawing.Size(212, 365);
            this.gbFileUploaders2.TabIndex = 0;
            this.gbFileUploaders2.TabStop = false;
            this.gbFileUploaders2.Text = "File uploaders";
            // 
            // flpFileUploaders2
            // 
            this.flpFileUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFileUploaders2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpFileUploaders2.Location = new System.Drawing.Point(3, 16);
            this.flpFileUploaders2.Name = "flpFileUploaders2";
            this.flpFileUploaders2.Size = new System.Drawing.Size(206, 346);
            this.flpFileUploaders2.TabIndex = 0;
            // 
            // gpImageUploaders2
            // 
            this.gpImageUploaders2.Controls.Add(this.flpImageUploaders2);
            this.gpImageUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpImageUploaders2.Location = new System.Drawing.Point(3, 3);
            this.gpImageUploaders2.Name = "gpImageUploaders2";
            this.gpImageUploaders2.Size = new System.Drawing.Size(210, 365);
            this.gpImageUploaders2.TabIndex = 1;
            this.gpImageUploaders2.TabStop = false;
            this.gpImageUploaders2.Text = "Image uploaders";
            // 
            // flpImageUploaders2
            // 
            this.flpImageUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImageUploaders2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpImageUploaders2.Location = new System.Drawing.Point(3, 16);
            this.flpImageUploaders2.Name = "flpImageUploaders2";
            this.flpImageUploaders2.Size = new System.Drawing.Size(204, 346);
            this.flpImageUploaders2.TabIndex = 0;
            // 
            // gbTextUploaders2
            // 
            this.gbTextUploaders2.Controls.Add(this.flpTextUploaders2);
            this.gbTextUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTextUploaders2.Location = new System.Drawing.Point(219, 3);
            this.gbTextUploaders2.Name = "gbTextUploaders2";
            this.gbTextUploaders2.Size = new System.Drawing.Size(210, 365);
            this.gbTextUploaders2.TabIndex = 2;
            this.gbTextUploaders2.TabStop = false;
            this.gbTextUploaders2.Text = "Text uploaders";
            // 
            // flpTextUploaders2
            // 
            this.flpTextUploaders2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTextUploaders2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTextUploaders2.Location = new System.Drawing.Point(3, 16);
            this.flpTextUploaders2.Name = "flpTextUploaders2";
            this.flpTextUploaders2.Size = new System.Drawing.Size(204, 346);
            this.flpTextUploaders2.TabIndex = 0;
            // 
            // tpShare
            // 
            this.tpShare.Controls.Add(this.tlpShare);
            this.tpShare.Location = new System.Drawing.Point(4, 22);
            this.tpShare.Name = "tpShare";
            this.tpShare.Padding = new System.Windows.Forms.Padding(3);
            this.tpShare.Size = new System.Drawing.Size(654, 374);
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
            this.tlpShare.Location = new System.Drawing.Point(3, 3);
            this.tlpShare.Name = "tlpShare";
            this.tlpShare.RowCount = 1;
            this.tlpShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 370F));
            this.tlpShare.Size = new System.Drawing.Size(648, 368);
            this.tlpShare.TabIndex = 0;
            // 
            // gbUrlShorteners
            // 
            this.gbUrlShorteners.Controls.Add(this.flpUrlShorteners);
            this.gbUrlShorteners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUrlShorteners.Location = new System.Drawing.Point(3, 3);
            this.gbUrlShorteners.Name = "gbUrlShorteners";
            this.gbUrlShorteners.Size = new System.Drawing.Size(318, 364);
            this.gbUrlShorteners.TabIndex = 0;
            this.gbUrlShorteners.TabStop = false;
            this.gbUrlShorteners.Text = "URL Shorteners";
            // 
            // flpUrlShorteners
            // 
            this.flpUrlShorteners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpUrlShorteners.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpUrlShorteners.Location = new System.Drawing.Point(3, 16);
            this.flpUrlShorteners.Name = "flpUrlShorteners";
            this.flpUrlShorteners.Size = new System.Drawing.Size(312, 345);
            this.flpUrlShorteners.TabIndex = 0;
            // 
            // gbSocialNetworkingServices
            // 
            this.gbSocialNetworkingServices.Controls.Add(this.flpSocialNetworkingServices);
            this.gbSocialNetworkingServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSocialNetworkingServices.Location = new System.Drawing.Point(327, 3);
            this.gbSocialNetworkingServices.Name = "gbSocialNetworkingServices";
            this.gbSocialNetworkingServices.Size = new System.Drawing.Size(318, 364);
            this.gbSocialNetworkingServices.TabIndex = 1;
            this.gbSocialNetworkingServices.TabStop = false;
            this.gbSocialNetworkingServices.Text = "Social Networking Services";
            // 
            // flpSocialNetworkingServices
            // 
            this.flpSocialNetworkingServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSocialNetworkingServices.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpSocialNetworkingServices.Location = new System.Drawing.Point(3, 16);
            this.flpSocialNetworkingServices.Name = "flpSocialNetworkingServices";
            this.flpSocialNetworkingServices.Size = new System.Drawing.Size(312, 345);
            this.flpSocialNetworkingServices.TabIndex = 0;
            // 
            // tpSummary
            // 
            this.tpSummary.Controls.Add(this.lblSummary);
            this.tpSummary.Location = new System.Drawing.Point(4, 22);
            this.tpSummary.Name = "tpSummary";
            this.tpSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tpSummary.Size = new System.Drawing.Size(654, 374);
            this.tpSummary.TabIndex = 5;
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
            // pgWorkflow
            // 
            this.pgWorkflow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgWorkflow.Location = new System.Drawing.Point(16, 48);
            this.pgWorkflow.Name = "pgWorkflow";
            this.pgWorkflow.Size = new System.Drawing.Size(624, 312);
            this.pgWorkflow.TabIndex = 2;
            this.pgWorkflow.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgWorkflow_PropertyValueChanged);
            // 
            // ucAfterCaptureTasks
            // 
            this.ucAfterCaptureTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterCaptureTasks.Location = new System.Drawing.Point(3, 16);
            this.ucAfterCaptureTasks.Margin = new System.Windows.Forms.Padding(4);
            this.ucAfterCaptureTasks.Name = "ucAfterCaptureTasks";
            this.ucAfterCaptureTasks.Size = new System.Drawing.Size(312, 345);
            this.ucAfterCaptureTasks.TabIndex = 0;
            // 
            // ucAfterUploadTasks
            // 
            this.ucAfterUploadTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAfterUploadTasks.Location = new System.Drawing.Point(3, 16);
            this.ucAfterUploadTasks.Margin = new System.Windows.Forms.Padding(4);
            this.ucAfterUploadTasks.Name = "ucAfterUploadTasks";
            this.ucAfterUploadTasks.Size = new System.Drawing.Size(312, 345);
            this.ucAfterUploadTasks.TabIndex = 0;
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
            this.MinimumSize = new System.Drawing.Size(696, 510);
            this.Name = "WindowWorkflow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WindowWorkflow";
            this.Load += new System.EventHandler(this.WindowWorkflow_Load);
            this.Resize += new System.EventHandler(this.WindowWorkflow_Resize);
            this.tcWorkflow.ResumeLayout(false);
            this.tpCapture.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tpUpload;
        private System.Windows.Forms.TableLayoutPanel tlpUpload;
        private System.Windows.Forms.FlowLayoutPanel flpFileUploaders;
        private System.Windows.Forms.FlowLayoutPanel flpImageUploaders;
        private System.Windows.Forms.FlowLayoutPanel flpTextUploaders;
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
        private System.Windows.Forms.TabPage tpUpload2;
        private System.Windows.Forms.TableLayoutPanel tlpUpload2;
        private System.Windows.Forms.GroupBox gbFileUploaders2;
        private System.Windows.Forms.FlowLayoutPanel flpFileUploaders2;
        private System.Windows.Forms.GroupBox gpImageUploaders2;
        private System.Windows.Forms.FlowLayoutPanel flpImageUploaders2;
        private System.Windows.Forms.GroupBox gbTextUploaders2;
        private System.Windows.Forms.FlowLayoutPanel flpTextUploaders2;
        private System.Windows.Forms.PropertyGrid pgWorkflow;

    }
}