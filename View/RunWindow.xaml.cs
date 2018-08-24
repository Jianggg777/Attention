using Attention.ViewModel;
using System;
using System.Windows;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace Attention
{
    /// <summary>
    /// RunWindow.xaml 的互動邏輯
    /// </summary>
    public partial class RunWindow : Window
    {
        private RunWindowVM rwvm = new RunWindowVM();
        public NotifyIcon notifyIcon = new NotifyIcon();
        public RunWindow()
        {
            InitializeComponent();
            setNotifyIcon(notifyIcon);
            rwvm.notifyIcon = notifyIcon;
            this.DataContext = rwvm;
            rwvm.monitorThread.Start();   // monitor current process thread run~
            createMonitor(); // 開啟監視程式，監視Attention有沒有被關閉
        }
        private void createMonitor()
        {
            Process[] procs = Process.GetProcessesByName("Monitor");
            if (procs.Length == 0)
            {
                try {
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Monitor.exe");
                }
                catch(Exception ex)
                {
                    Logger.WriteLog("  error"+ex.Message);
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            notifyIcon.ShowBalloonTip(1000, "Attention!", "啟動後不可離開，程式已縮小。", ToolTipIcon.None);  // 顯示BalloonTip
        }

        private void setNotifyIcon(NotifyIcon notifyIcon)
        {
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("exit", null, MenuTest1_Click);
            notifyIcon.Icon = new System.Drawing.Icon(AppDomain.CurrentDomain.BaseDirectory + "Clipboard.ico");   // Debug資料夾的Clipboard.ico
            notifyIcon.Visible = true;
            notifyIcon.Text = "Attention!";   // 滑鼠移過去顯示的字
        }
        private void MenuTest1_Click(object sender, EventArgs e)
        {
            string pass = Interaction.InputBox("輸入驗證碼", "關閉程式", "");
            if (pass == "yo!")
            {
                Process[] procs = Process.GetProcessesByName("Monitor");
                if(procs.Length != 0)
                {
                    procs[0].Kill();
                }
                Environment.Exit(0);
            }
        }
        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

    }
}
