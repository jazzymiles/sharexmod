namespace HelplersLib
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
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lbActivitiesAll = new System.Windows.Forms.ListBox();
            this.lbActivitiesUser = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(8, 8);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(464, 20);
            this.txtDescription.TabIndex = 0;
            this.txtDescription.Text = "New workflow";
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // lbActivitiesAll
            // 
            this.lbActivitiesAll.FormattingEnabled = true;
            this.lbActivitiesAll.Location = new System.Drawing.Point(8, 40);
            this.lbActivitiesAll.Name = "lbActivitiesAll";
            this.lbActivitiesAll.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbActivitiesAll.Size = new System.Drawing.Size(176, 199);
            this.lbActivitiesAll.TabIndex = 1;
            // 
            // lbActivitiesUser
            // 
            this.lbActivitiesUser.FormattingEnabled = true;
            this.lbActivitiesUser.Location = new System.Drawing.Point(296, 40);
            this.lbActivitiesUser.Name = "lbActivitiesUser";
            this.lbActivitiesUser.Size = new System.Drawing.Size(176, 199);
            this.lbActivitiesUser.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(200, 48);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add ==>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(200, 80);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(88, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "<== Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // WindowWorkflow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 253);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbActivitiesUser);
            this.Controls.Add(this.lbActivitiesAll);
            this.Controls.Add(this.txtDescription);
            this.Name = "WindowWorkflow";
            this.Text = "WindowWorkflow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WindowWorkflow_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ListBox lbActivitiesAll;
        private System.Windows.Forms.ListBox lbActivitiesUser;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;

    }
}