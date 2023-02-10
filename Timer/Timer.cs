using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timer
{
    public partial class Timer : Form
    {
        public Timer()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            notifyIcon1.Icon = new Icon("icon.ico");
        }

        DateTime start;
        System.Timers.Timer t;
        long s;

        public void CountDown(long seconds, DateTime slt)
        {
            start = slt;
            s = seconds;
            t.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            long remainingSeconds = (long)(s - (DateTime.Now - start).TotalSeconds);
            if (remainingSeconds <= 0)
            {
                t.Stop();
                this.BeginInvoke((Action)(() => toolStripStatusLabel1.Text = "Xong !"));
                MessageBox.Show("Đến giờ tắt máy !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            this.BeginInvoke((Action)(() => toolStripStatusLabel1.Text = string.Format("{0} giây...", remainingSeconds)));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (t != null && t.Enabled)
            {
                t.Dispose();
            }

            t = new System.Timers.Timer { Interval = 1000 };
            t.Elapsed += Tick;
            CountDown(10, DateTime.Parse(dtpSetTimer.Value.ToString()));
            toolStripStatusLabel1.Text = dtpSetTimer.Value.ToString();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Timer_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) this.Hide();
            else this.Show();
        }

        private void hiểnThịToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
