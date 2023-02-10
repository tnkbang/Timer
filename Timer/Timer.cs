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
            txtNoiDung.Text = text;
            dtpSetTimer.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            notifyIcon.Icon = new Icon("icon.ico");
        }

        DateTime start;
        System.Timers.Timer t;
        long s;
        string text = "Đến giờ tắt máy !";

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
                this.BeginInvoke((Action)(() => lblTimer.Text = "Xong !"));
                MessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            this.BeginInvoke((Action)(() => lblTimer.Text = string.Format("{0} giây...", remainingSeconds)));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (t != null && t.Enabled)
            {
                t.Dispose();
            }

            if (!string.IsNullOrEmpty(txtNoiDung.Text)) text = txtNoiDung.Text;

            t = new System.Timers.Timer { Interval = 1000 };
            t.Elapsed += Tick;
            CountDown(10, DateTime.Parse(dtpSetTimer.Value.ToString()));
            lblTimer.Text = dtpSetTimer.Value.ToString();
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

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
