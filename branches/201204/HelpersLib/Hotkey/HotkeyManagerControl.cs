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
using HelplersLib;

namespace HelpersLib.Hotkey
{
    public partial class HotkeyManagerControl : UserControl
    {
        private HotkeyManager manager;

        public HotkeyManagerControl()
        {
            InitializeComponent();
        }

        public void PrepareHotkeys(HotkeyManager hotkeyManager)
        {
            if (manager == null)
            {
                manager = hotkeyManager;

                foreach (Workflow wf in manager.Workflows)
                {
                    HotkeySelectionControl control = new HotkeySelectionControl(wf);
                    control.HotkeyChanged += new EventHandler(control_HotkeyChanged);
                    flpHotkeys.Controls.Add(control);
                }
            }
        }

        private void control_HotkeyChanged(object sender, EventArgs e)
        {
            HotkeySelectionControl control = (HotkeySelectionControl)sender;
            manager.UpdateHotkey(control.Workflow.HotkeyConfig);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Workflow wf = new Workflow("New Workflow", new HotkeySetting());
            WindowWorkflow wwf = new WindowWorkflow(wf);
            wwf.ShowDialog();

            manager.Workflows.Add(wf);

            HotkeySelectionControl control = new HotkeySelectionControl(wf);
            control.HotkeyChanged += new EventHandler(control_HotkeyChanged);

            flpHotkeys.Controls.Add(control);
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            foreach (HotkeySelectionControl hk in flpHotkeys.Controls)
            {
                if (hk.Checked)
                {
                    WindowWorkflow wwf = new WindowWorkflow(hk.Workflow);
                    wwf.ShowDialog();
                    hk.Workflow = wwf.Workflow;
                    break;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (HotkeySelectionControl hk in flpHotkeys.Controls)
            {
                if (hk.Checked && !hk.Workflow.HotkeyConfig.SystemHotkey)
                {
                    flpHotkeys.Controls.Remove(hk);
                    manager.Workflows.Remove(hk.Workflow);
                }
            }
        }
    }
}