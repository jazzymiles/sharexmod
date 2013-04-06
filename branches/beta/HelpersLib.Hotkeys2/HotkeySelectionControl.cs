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
using System.Drawing;
using System.Windows.Forms;

namespace HelpersLib.Hotkeys2
{
    public partial class HotkeySelectionControl : UserControl
    {
        public event EventHandler HotkeyChanged;

        public Workflow Workflow { get; set; }

        public bool Checked { get; set; }

        public HotkeySelectionControl(Workflow wf)
        {
            InitializeComponent();
            Workflow = wf;
            chkHotkeyDescription.Text = Workflow.HotkeyConfig.Description;
            btnSetHotkey.Text = new KeyInfo(Workflow.HotkeyConfig.Hotkey).ToString();
            if (!this.Workflow.HotkeyConfig.SystemHotkey)
                this.set_HotkeyFontBold();
            UpdateHotkeyStatus();
        }

        public void set_HotkeyDescription(string txt)
        {
            chkHotkeyDescription.Text = txt;
        }

        public void set_HotkeyFontBold()
        {
            chkHotkeyDescription.Font = new Font(chkHotkeyDescription.Font, FontStyle.Bold);
        }

        private void btnSetHotkey_Click(object sender, EventArgs e)
        {
            using (HotkeyInputForm inputForm = new HotkeyInputForm(Workflow.HotkeyConfig.Hotkey, Workflow.HotkeyConfig.HotkeyDefault))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    Workflow.HotkeyConfig.Hotkey = inputForm.SelectedKey;
                    btnSetHotkey.Text = new KeyInfo(Workflow.HotkeyConfig.Hotkey).ToString();
                    OnHotkeyChanged();
                    UpdateHotkeyStatus();
                }
            }
        }

        private void UpdateHotkeyStatus()
        {
            switch (Workflow.HotkeyConfig.HotkeyStatus)
            {
                case HotkeyStatus.Failed:
                    lblIsHotkeyActive.BackColor = Color.IndianRed;
                    break;
                case HotkeyStatus.NotConfigured:
                    lblIsHotkeyActive.BackColor = Color.LightGoldenrodYellow;
                    break;
                case HotkeyStatus.Registered:
                    lblIsHotkeyActive.BackColor = Color.PaleGreen;
                    break;
            }
        }

        protected void OnHotkeyChanged()
        {
            if (HotkeyChanged != null)
            {
                HotkeyChanged(this, null);
            }
        }

        private void chkHotkeyDescription_CheckedChanged(object sender, EventArgs e)
        {
            Checked = chkHotkeyDescription.Checked;
        }
    }
}