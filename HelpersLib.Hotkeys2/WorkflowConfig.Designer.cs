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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvActivitiesAll = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvActivitiesUser = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tcWorkflow = new System.Windows.Forms.TabControl();
            this.tpActivities = new System.Windows.Forms.TabPage();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.pgSettings = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tcWorkflow.SuspendLayout();
            this.tpActivities.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(8, 8);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(661, 23);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.Text = "New workflow";
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(96, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add ==>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(3, 32);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(96, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "<== Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
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
            // lvActivitiesAll
            // 
            this.lvActivitiesAll.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvActivitiesAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvActivitiesAll.FullRowSelect = true;
            this.lvActivitiesAll.GridLines = true;
            this.lvActivitiesAll.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvActivitiesAll.Location = new System.Drawing.Point(3, 3);
            this.lvActivitiesAll.Name = "lvActivitiesAll";
            this.lvActivitiesAll.Size = new System.Drawing.Size(262, 362);
            this.lvActivitiesAll.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvActivitiesAll.TabIndex = 0;
            this.lvActivitiesAll.UseCompatibleStateImageBehavior = false;
            this.lvActivitiesAll.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "All Activities";
            this.columnHeader1.Width = 250;
            // 
            // lvActivitiesUser
            // 
            this.lvActivitiesUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvActivitiesUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvActivitiesUser.FullRowSelect = true;
            this.lvActivitiesUser.GridLines = true;
            this.lvActivitiesUser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvActivitiesUser.Location = new System.Drawing.Point(383, 3);
            this.lvActivitiesUser.Name = "lvActivitiesUser";
            this.lvActivitiesUser.Size = new System.Drawing.Size(262, 362);
            this.lvActivitiesUser.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvActivitiesUser.TabIndex = 2;
            this.lvActivitiesUser.UseCompatibleStateImageBehavior = false;
            this.lvActivitiesUser.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "User Activities";
            this.columnHeader2.Width = 250;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lvActivitiesAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvActivitiesUser, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(648, 368);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnRemove);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(271, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(106, 362);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // tcWorkflow
            // 
            this.tcWorkflow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcWorkflow.Controls.Add(this.tpActivities);
            this.tcWorkflow.Controls.Add(this.tpSettings);
            this.tcWorkflow.Location = new System.Drawing.Point(8, 40);
            this.tcWorkflow.Name = "tcWorkflow";
            this.tcWorkflow.SelectedIndex = 0;
            this.tcWorkflow.Size = new System.Drawing.Size(662, 400);
            this.tcWorkflow.TabIndex = 1;
            // 
            // tpActivities
            // 
            this.tpActivities.Controls.Add(this.tableLayoutPanel1);
            this.tpActivities.Location = new System.Drawing.Point(4, 22);
            this.tpActivities.Name = "tpActivities";
            this.tpActivities.Padding = new System.Windows.Forms.Padding(3);
            this.tpActivities.Size = new System.Drawing.Size(654, 374);
            this.tpActivities.TabIndex = 0;
            this.tpActivities.Text = "Activities";
            this.tpActivities.UseVisualStyleBackColor = true;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.pgSettings);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(654, 374);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // pgSettings
            // 
            this.pgSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSettings.Location = new System.Drawing.Point(3, 3);
            this.pgSettings.Name = "pgSettings";
            this.pgSettings.Size = new System.Drawing.Size(648, 368);
            this.pgSettings.TabIndex = 0;
            this.pgSettings.ToolbarVisible = false;
            // 
            // WindowWorkflow
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(680, 480);
            this.Controls.Add(this.tcWorkflow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(696, 512);
            this.Name = "WindowWorkflow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WindowWorkflow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tcWorkflow.ResumeLayout(false);
            this.tpActivities.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvActivitiesAll;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lvActivitiesUser;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabControl tcWorkflow;
        private System.Windows.Forms.TabPage tpActivities;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.PropertyGrid pgSettings;

    }
}