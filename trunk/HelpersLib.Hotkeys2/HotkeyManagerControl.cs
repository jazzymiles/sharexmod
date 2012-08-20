#region License Information (GPL v3)

/*
    ShareX - A program that allows you to take screenshots and share any file type
    Copyright (C) 2012 ShareX Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using System;
using System.Windows.Forms;

namespace HelpersLib.Hotkeys2
{
    public partial class HotkeyManagerControl : UserControl
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private HotkeyManager _manager;
        private Workflow _workflow;

        public HotkeyManagerControl()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F2))
            {
                Configure();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void PrepareHotkeys(HotkeyManager hotkeyManager, Workflow workflow)
        {
            if (hotkeyManager != null)
            {
                _manager = hotkeyManager;
                _workflow = workflow;

                log.DebugFormat("{0} hotkeys are ready.", _manager.Workflows.Count);

                flpHotkeys.Controls.Clear();
                foreach (Control ctl in this.Controls)
                {
                    ctl.Enabled = true;
                }

                foreach (Workflow wf in _manager.Workflows)
                {
                    HotkeySelectionControl control = new HotkeySelectionControl(wf);
                    control.HotkeyChanged += new EventHandler(control_HotkeyChanged);
                    flpHotkeys.Controls.Add(control);
                }
            }
            else
            {
                log.Debug("Hotkeys are not ready yet.");
                foreach (Control ctl in this.Controls)
                {
                    ctl.Enabled = false;
                }

                flpHotkeys.Controls.Clear();
                flpHotkeys.Controls.Add(new Label()
                {
                    Text = "Hotkeys are not ready yet. \n\nThis is most probably due to Dropbox Sync is still in progress.",
                    AutoSize = true
                });
            }
        }

        private void control_HotkeyChanged(object sender, EventArgs e)
        {
            HotkeySelectionControl control = (HotkeySelectionControl)sender;
            _manager.UpdateHotkey(control.Workflow.HotkeyConfig);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Workflow wf = new Workflow("New Workflow", new HotkeySetting(), false);
            WindowWorkflow wwf = new WindowWorkflow(wf);

            if (wwf.ShowDialog() == DialogResult.OK)
            {
                _manager.Workflows.Add(wf);
                HotkeySelectionControl control = new HotkeySelectionControl(wf);
                control.HotkeyChanged += new EventHandler(control_HotkeyChanged);
                flpHotkeys.Controls.Add(control);
                lblHelp.Text = "Your new hotkey will not be active until you close Settings.";
            }
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            Configure();
        }

        private void Configure()
        {
            foreach (HotkeySelectionControl hksc in flpHotkeys.Controls)
            {
                if (hksc.Checked)
                {
                    WindowWorkflow wwf = new WindowWorkflow(hksc.Workflow);
                    wwf.ShowDialog();
                    hksc.set_HotkeyDescription(wwf.Workflow.HotkeyConfig.Description);
                    hksc.Workflow = wwf.Workflow;
                    break;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            bool userError = false;
            foreach (HotkeySelectionControl hksc in flpHotkeys.Controls)
            {
                if (hksc.Checked)
                {
                    if (!hksc.Workflow.HotkeyConfig.SystemHotkey)
                    {
                        if (MessageBox.Show(string.Format("Are you sure that you want to remove the workflow:\n{0}?",
                                hksc.Workflow.HotkeyConfig.Description), Application.ProductName,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            flpHotkeys.Controls.Remove(hksc);
                            _manager.Workflows.Remove(hksc.Workflow);
                        }
                    }
                    else
                    {
                        userError = true;
                    }
                }
            }

            if (userError)
                MessageBox.Show("You cannot remove application generated workflows. \n\nHowever, you can configure them.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnApplyDefaults_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to apply default settings for the selected workfows?", Application.ProductName,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                foreach (HotkeySelectionControl hksc in flpHotkeys.Controls)
                {
                    HotkeySetting hotkeyConfig = hksc.Workflow.HotkeyConfig;
                    hksc.Workflow = _workflow.Clone();
                    hksc.Workflow.HotkeyConfig = hotkeyConfig;
                }
            }
        }
    }
}