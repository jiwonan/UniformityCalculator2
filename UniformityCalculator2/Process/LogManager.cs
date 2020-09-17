using System;
using System.Threading;
using System.Windows.Forms;

namespace UniformityCalculator2.Process
{
    public static class LogManager
    {
        private static TextBox logBox = null;

        public static void setTextBox(TextBox textBox)
        {
            logBox = textBox;
        }

        public static void SetLog(string msg)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            if (logBox == null) return;

            if (logBox.InvokeRequired)
            {
                logBox.Invoke(new MethodInvoker(() =>
                {
                    AddLogText(threadId, msg);
                }));
            }
            else
            {
                AddLogText(threadId, msg);
            }
        }

        private static void AddLogText(int id, string msg)
        {
            logBox.AppendText($"[{id}] [{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {msg}\r\n");
            logBox.SelectionStart = logBox.Text.Length;
        }
    }
}
