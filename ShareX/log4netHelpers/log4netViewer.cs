using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShareX.log4netHelpers
{
    public partial class log4netViewer : Form
    {
        public log4netViewer()
        {
            InitializeComponent();
            log4netHelper.ConfigureRichTextBox(richTextBoxLog);
        }
    }
}