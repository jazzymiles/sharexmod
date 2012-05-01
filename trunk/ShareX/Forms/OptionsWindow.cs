using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;

namespace ShareX.Forms
{
    public partial class OptionsWindow : Form
    {
        private bool loaded;

        public OptionsWindow()
        {
            InitializeComponent();
            loaded = true;

            foreach (Control ctl in panelBase.Controls)
            {
                if (ctl.Name == panelGeneral.Name)
                    ctl.Visible = true;
                else
                    ctl.Visible = false;
            }

            for (int i = 0; i < tvMain.Nodes.Count; i++)
            {
                tvMain.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvMain_NodeMouseClick);
            }
        }

        private void tvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tvNode = e.Node;
            Panel myPanel = GetPanelByName(tvNode.Tag.ToString());
            foreach (Control ctl in panelBase.Controls)
            {
                if (ctl.Name == myPanel.Name)
                    ctl.Visible = true;
                else
                    ctl.Visible = false;
            }
        }

        private Panel GetPanelByName(string name)
        {
            foreach (Control ctl in panelBase.Controls)
            {
                if (ctl.GetType() == typeof(Panel) && ctl.Name == name)
                    return ctl as Panel;
            }
            return new Panel();
        }

        private void cbStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ShortcutHelper.SetShortcut(cbStartWithWindows.Checked, Environment.SpecialFolder.Startup, "-silent");
            }
        }

        private void cbShellContextMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ShortcutHelper.SetShortcut(cbShellContextMenu.Checked, Environment.SpecialFolder.SendTo);
            }
        }

        private void cbShowTray_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ShowTray = cbShowTray.Checked;

            if (loaded)
            {
                Program.mainForm.niTray.Visible = Program.Settings.ShowTray;
            }
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutoCheckUpdate = cbCheckUpdates.Checked;
        }

        private void cbCaptureShadow_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.CaptureShadow = cbCaptureShadow.Checked;
        }

        private void cbCaptureTransparent_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.CaptureTransparent = cbCaptureTransparent.Checked;

            cbCaptureShadow.Enabled = Program.Settings.CaptureTransparent;
        }

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ShowCursor = cbShowCursor.Checked;
        }

        private void OptionsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            UploadManager.UpdateProxySettings();
            Program.Settings.SaveAsync();
        }
    }
}