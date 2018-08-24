using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention
{
    static class Logger
    {
        static object lockLog = new object();
        public static void WriteLog(string txt)
        {
            //今日日期
            DateTime Date = DateTime.Now;
            string TodyTime = Date.ToString("yyyy-MM-dd HH:mm:ss");
            string Tody = Date.ToString("yyyy-MM-dd");

            //檢查此路徑有無資料夾
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory+"log"))
            {
                //新增資料夾
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory+"log");
            }
            lock (lockLog)
            {
                //把內容寫到目的檔案，若檔案存在則附加在原本內容之後(換行)
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "log\\log.txt", "\r\n" + TodyTime + "：" + txt);
            }
        }
    }
}
