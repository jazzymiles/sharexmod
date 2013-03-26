using HelpersLib;
using ShareX.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using UploadersLibMod;

namespace ShareX
{
    public partial class WindowAfterUpload : Form
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private UploadInfo mUploadInfo { get; set; }

        public WindowAfterUpload(UploadInfo info)
        {
            InitializeComponent();
            tmrClose.Start();
            mUploadInfo = info;

            if (info.DataType == EDataType.Image)
            {
                if (File.Exists(info.FilePath))
                    pbPreview.LoadImageFromFile(info.FilePath);
                else
                    pbPreview.LoadImageFromURL(info.Result.URL);
            }
            else
            {
                pbPreview.LoadImage(Resources.folder);
            }

            this.Text = File.Exists(info.FilePath) ? info.FilePath : info.FileName;

            foreach (LinkFormatEnum type in Enum.GetValues(typeof(LinkFormatEnum)))
            {
                string url = new UploadResultHelper(info.Result).GetUrlByType(type, info.Result.URL);
                if (!string.IsNullOrEmpty(url))
                {
                    TreeNode tviUrlType = new TreeNode();
                    tviUrlType.Text = type.GetDescription();

                    TreeNode tviUrl = new TreeNode();
                    tviUrl.Text = url;

                    tviUrlType.Nodes.Add(tviUrl);
                    tvMain.Nodes.Add(tviUrlType);
                }
            }

            tvMain.ExpandAll();
        }

        private void tvMain_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
                Helpers.CopyTextSafely(e.Node.FirstNode.Text);
            else
                Helpers.CopyTextSafely(e.Node.Text);
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFolderOpen_Click(object sender, EventArgs e)
        {
            if (File.Exists(mUploadInfo.FilePath))
                Helpers.OpenFolderWithFile(mUploadInfo.FilePath);
        }

        private void btnCopyImage_Click(object sender, EventArgs e)
        {
            if (File.Exists(mUploadInfo.FilePath))
                Helpers.CopyImageFileToClipboard(mUploadInfo.FilePath);
        }

        private void btnOpenLink_Click(object sender, EventArgs e)
        {
            string url = mUploadInfo.Result.URL;
            if (!string.IsNullOrEmpty(url))
                Helpers.LoadBrowserAsync(url);
        }

        private void btnCopyLink_Click(object sender, EventArgs e)
        {
            string url = string.Empty;
            if (tvMain.SelectedNode != null)
            {
                url = tvMain.SelectedNode.Text;
            }
            else
            {
                url = tvMain.Nodes[0].Nodes[0].Text;
            }
            if (!string.IsNullOrEmpty(url))
                Helpers.CopyTextSafely(url);
        }
    }
}