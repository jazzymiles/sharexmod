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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(16, 16);
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
            this.lbActivitiesAll.Location = new System.Drawing.Point(16, 48);
            this.lbActivitiesAll.Name = "lbActivitiesAll";
            this.lbActivitiesAll.Size = new System.Drawing.Size(176, 199);
            this.lbActivitiesAll.TabIndex = 1;
            // 
            // lbActivitiesUser
            // 
            this.lbActivitiesUser.FormattingEnabled = true;
            this.lbActivitiesUser.Location = new System.Drawing.Point(296, 48);
            this.lbActivitiesUser.Name = "lbActivitiesUser";
            this.lbActivitiesUser.Size = new System.Drawing.Size(176, 199);
            this.lbActivitiesUser.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add ==>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(200, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "<== Remove";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // WindowWorkflow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 263);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbActivitiesUser);
            this.Controls.Add(this.lbActivitiesAll);
            this.Controls.Add(this.txtDescription);
            this.Name = "WindowWorkflow";
            this.Text = "WindowWorkflow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ListBox lbActivitiesAll;
        private System.Windows.Forms.ListBox lbActivitiesUser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

    }
}