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

using HelpersLib;
using HelpersLibMod;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ShareX.HelperClasses
{
    public static class TaskHelper
    {
        public static string GetFilename(string extension = "")
        {
            NameParser nameParser = new NameParser(NameParserType.FileName)
            {
                AutoIncrementNumber = SettingsManager.ConfigCore.AutoIncrementNumber,
                MaxNameLength = 100
            };

            string filename = nameParser.Parse(SettingsManager.ConfigCore.NameFormatPattern);
            if (!string.IsNullOrEmpty(extension)) filename += "." + extension;

            SettingsManager.ConfigCore.AutoIncrementNumber = nameParser.AutoIncrementNumber;

            return filename;
        }

        public static bool CheckExpressionEncoder()
        {
            if (SettingsManager.ConfigUser.ScreencastFileType != EScreencastFileType.gif && !HelpersMod.HasExpressionEncoder())
            {
                System.Windows.Forms.MessageBox.Show("Microsoft Expression Encoder 4 is required to perform screencast.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Helpers.LoadBrowserAsync("http://www.microsoft.com/en-au/download/details.aspx?id=27870");
                return false;
            }

            return true;
        }
    }
}