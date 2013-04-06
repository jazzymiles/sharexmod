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
        private Workflow _Workflow = null; 

        public WorkflowManager(HotkeyManager hm, Workflow wf)
        {
            _HotkeyManager = hm;
            _Workflow = wf;

            InitializeComponent();
            this.Text = Application.ProductName + " - Workflows";

            PrepareWorkflows();
        }

        public void PrepareWorkflows()
        {
            if (_HotkeyManager != null)
                hmHotkeys.PrepareHotkeys(_HotkeyManager, _Workflow);
        }
    }
}