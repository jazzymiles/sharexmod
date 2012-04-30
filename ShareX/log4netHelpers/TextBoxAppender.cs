using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net.Appender;
using log4net.Core;

namespace ShareX
{
    public class TextBoxAppender : AppenderSkeleton
    {
        public TextBoxAppender()
        {
        }

        public TextBox _textBox = new TextBox();

        private delegate void UpdateControlDelegate(LoggingEvent loggingEvent);

        private void UpdateControl(LoggingEvent loggingEvent)
        {
            _textBox.AppendText(loggingEvent.RenderedMessage + "\r\n");
        }

        protected override void Append(LoggingEvent LoggingEvent)
        {
            if (_textBox != null && _textBox.Created)
            {
                if (_textBox.InvokeRequired)
                {
                    _textBox.Invoke(new UpdateControlDelegate(UpdateControl), new object[] { LoggingEvent });
                }
                else
                {
                    UpdateControl(LoggingEvent);
                }
            }
        }
    }
}