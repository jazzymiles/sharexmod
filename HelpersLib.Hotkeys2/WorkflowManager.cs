using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpersLib.Hotkeys2
{
    public partial class WorkflowManager : Form
    {
        private HotkeyManager _HotkeyManager = null;

        public WorkflowManager(HotkeyManager hm)
        {
            _HotkeyManager = hm;

            InitializeComponent();
            this.Text = Application.ProductName + " - Workflows";

            PrepareWorkflows();
        }

        public void PrepareWorkflows()
        {
            if (_HotkeyManager != null)
                hmHotkeys.PrepareHotkeys(_HotkeyManager);
        }
    }
}