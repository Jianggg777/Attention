using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Attention
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // catch global exception
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // 註冊開機自動啟動
            Logger.WriteLog("write regedit?");
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            string AppName = "Attention";
            if (rk.GetValue(AppName) == null)  // 沒註冊過，註冊
            {
                Logger.WriteLog("  yes");
                rk.SetValue(AppName, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\Attention.exe");
            }
            else
            {
                Logger.WriteLog("  has existed");
            }
            rk.Close();

            // 讀取今天的plan 
            //     有 => run plan
            //     沒有 => 進入setting 
            using (MyDBContext db = new MyDBContext())
            {
                var res = (from r in db.Records
                           where r.Today.Date == DateTime.Today
                           select r).FirstOrDefault();
                if (res != null)
                {
                    Logger.WriteLog("go RunWindow.xaml");
                    this.StartupUri = new Uri("/View/RunWindow.xaml", UriKind.Relative);
                }
                else
                {
                    Logger.WriteLog("go SettingWindow.xaml");
                    this.StartupUri = new Uri("/View/SettingWindow.xaml", UriKind.Relative);
                }
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("發生致命錯誤，將終止。\n");
            }
            sbEx.Append("捕獲未處理異常：");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception)e.ExceptionObject).Message);
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }
            MessageBox.Show(sbEx.ToString());
            Logger.WriteLog("thread捕獲未處理異常:" + e.ExceptionObject);
            Process[] sub = Process.GetProcessesByName("Monitor");
            if (sub.Length != 0)
            {
                sub[0].Kill();
            }
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true; //把 Handled 屬性設為true，表示磁異常已處理，進程可以繼續運行，不會強制退出   
            Logger.WriteLog("UI捕獲未處理異常:" + e.Exception.Message);
            MessageBox.Show("捕獲未處理異常:" + e.Exception.Message);
        }
    }
}

