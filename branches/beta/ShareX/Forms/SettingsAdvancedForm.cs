using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX
{
    public partial class SettingsAdvancedForm : Form
    {
        public SettingsAdvancedForm()
        {
            InitializeComponent();
        }

        private void SettingsAdvancedForm_Load(object sender, EventArgs e)
        {
            Text = Program.Title + " - Advanced Settings";
            pgSettings.SelectedObject = Program.Settings;
        }
    }
}
