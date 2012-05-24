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
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using UpdateCheckerLib;
using UploadersLib;

namespace ShareX
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Text = Program.Title;
            lblProductName.Text = Program.Title;
            lblCopyright.Text = AssemblyCopyright;

            AppendBoldLine("Committers:");
            AppendLine("Josh Lovins (thedeathly)");
            AppendLine();

            AppendBoldLine("Acknowledgements:");
            AppendLine("FTP Library: http://www.starksoft.com");
            AppendLine("Json.NET: http://json.codeplex.com");
            AppendLine("SSH.NET: http://sshnet.codeplex.com");
            AppendLine("Image Editor: http://getgreenshot.org");
            AppendLine("Application Icon:  Mopquill ( www.mpql.net )");
            AppendLine("Other Icons: http://p.yusukekamiyamane.com");
            AppendLine();
            AppendBoldLine("Thanks to:");
            AppendLine("Andrew Moore (zathman) for introducing ZScreen");
            AppendLine("Brandon Zimmerman (rgrthat) for leading the way");
            AppendLine();

            if (Program.LibNames != null)
            {
                AppendBoldLine("Referenced assemblies:");
                foreach (string dll in Program.LibNames)
                {
                    AppendLine(dll);
                }
            }

            UpdateChecker updateChecker = new UpdateChecker(Program.URL_UPDATE, Application.ProductName, new Version(Program.AssemblyVersion),
                ReleaseChannelType.Stable, Uploader.ProxySettings.GetWebProxy);
            uclUpdate.CheckUpdate(updateChecker);
        }

        private void AppendLine(string text = "")
        {
            txtDetails.AppendText(text + Environment.NewLine);
        }

        private void AppendBoldLine(string text)
        {
            txtDetails.SelectionFont = new Font(txtDetails.SelectionFont, FontStyle.Bold);
            txtDetails.AppendText(text + Environment.NewLine);
            txtDetails.SelectionFont = new Font(txtDetails.SelectionFont, FontStyle.Regular);
        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        private void lblWebsite_Click(object sender, EventArgs e)
        {
            Helpers.LoadBrowserAsync(Program.URL_WEBSITE);
        }

        private void lblBugs_Click(object sender, EventArgs e)
        {
            Helpers.LoadBrowserAsync(Program.URL_ISSUES);
        }

        private void pbBerkURL_Click(object sender, EventArgs e)
        {
            Helpers.LoadBrowserAsync(Links.URL_BERK);
        }

        private void pbMikeURL_Click(object sender, EventArgs e)
        {
            Helpers.LoadBrowserAsync(Links.URL_MIKE);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDetails_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}