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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HelpersLib.Hotkeys2
{
    public class HotkeyManager
    {
        public List<Workflow> Workflows = new List<Workflow>();

        private HotkeyForm hotkeyForm;

        public HotkeyManager(HotkeyForm hotkeyForm)
        {
            this.hotkeyForm = hotkeyForm;
        }

        public void AddHotkey(Workflow wf, Action action, ToolStripMenuItem menuItem = null)
        {
            wf.HotkeyConfig.Action = action;
            wf.HotkeyConfig.MenuItem = menuItem;

            Workflows.Add(wf);

            wf.HotkeyConfig.UpdateMenuItemShortcut();
            wf.HotkeyConfig.HotkeyStatus = hotkeyForm.RegisterHotkey(wf.HotkeyConfig.Hotkey, action, wf.HotkeyConfig.Tag);
        }

        public HotkeyStatus UpdateHotkey(HotkeySetting setting)
        {
            setting.UpdateMenuItemShortcut();
            setting.HotkeyStatus = hotkeyForm.ChangeHotkey(setting.Tag, setting.Hotkey, setting.Action);
            return setting.HotkeyStatus;
        }

        public bool IsHotkeyRegisterFailed(out string failedHotkeys)
        {
            failedHotkeys = null;
            bool status = false;
            var failedHotkeysList = Workflows.Where(x => x.HotkeyConfig.HotkeyStatus == HotkeyStatus.Failed);
            if (status = failedHotkeysList.Count() > 0)
            {
                failedHotkeys = string.Join("\r\n", failedHotkeysList.Select(wf => wf.HotkeyConfig.Description + ": " + wf.HotkeyConfig.ToString()).ToArray());
            }
            return status;
        }
    }
}