using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Attention.ViewModel
{
    class RunWindowVM : INotifyPropertyChanged
    {

        public ObservableCollection<PlanVM> TodayPlans { get; set; }
        public string TodayDate { get; set; }
        public List<TodoList> toDoList;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        public Thread monitorThread;
        private DispatcherTimer timer;

        private ObservableCollection<Process> ps;
        public ObservableCollection<Process> Processes {
            get { return ps; }
            set {
                if (ps != value)
                {
                    ps = value;
                    OnPropertyChanged("Processes");
                }
            }
        }

        public RunWindowVM()
        {
            TodayDate = DateTime.Today.ToString("yyyy/MM/dd");
            TodayPlans = GetTodayPlans();
            toDoList = new List<TodoList>();
            foreach (var p in TodayPlans)
            {
                toDoList.Add(new TodoList(p.StartTime, p.EndTime, p.Ban, p.Tip));
            }
            monitorThread = new Thread(checkProcessThread);
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Start();   //開始計時
        }
        // 每分鐘檢查，todolist的時間時出現行事曆
        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var tdl in toDoList)
            {
                // 設定時間的整點，會出現提示
                if (DateTime.Now.Minute == 0 && tdl.StartTime < DateTime.Now && tdl.EndTime > DateTime.Now)
                {
                    string str = string.Format("{0}:00 ~ {1}:00 : {2}", tdl.StartTime.Hour, tdl.EndTime.Hour, tdl.Tip);
                    notifyIcon.ShowBalloonTip(1000, "Attention!", str, System.Windows.Forms.ToolTipIcon.None);
                }
            }
        }

        private void checkProcessThread()
        {
            while (true)
            {
                foreach (var tdl in toDoList)  // check 時段
                {
                    if (tdl.StartTime < DateTime.Now && tdl.EndTime > DateTime.Now)  // 現在是某時段內
                    {
                        // 從當前運行的process中，找有沒有todolist禁止的process
                        foreach (var pName in tdl.Procs)
                        {
                            var forbidProcs = (from p in Process.GetProcesses()
                                               where p.ProcessName.ToLower() == pName
                                               select p).ToList();
                            if (forbidProcs.Count != 0)
                            {
                                foreach (var p in forbidProcs)
                                {
                                    string str = string.Format("你開了{0}程式，5秒後強制關閉。", pName);
                                    notifyIcon.ShowBalloonTip(1000, "Attention!", str, System.Windows.Forms.ToolTipIcon.None);
                                    Thread.Sleep(5000);
                                    try
                                    {
                                        if (!p.HasExited)
                                        {
                                            p.Kill();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("delete exception!" + ex.Message);
                                    }
                                }
                            }
                        }
                        // 從當前運行的process中(含瀏覽器)，找有沒有todolist禁止的keyword
                        foreach (var kName in tdl.Keys)
                        {
                            var forbidKeys = (from p in Process.GetProcesses()
                                              where p.MainWindowTitle.ToLower().Contains(kName)
                                              select p).ToList();
                            if (forbidKeys.Count != 0)
                            {
                                foreach (var k in forbidKeys)
                                {
                                    // chrome當前的標題
                                    if (k.ProcessName == "chrome")
                                    {
                                        string str = string.Format("你開了含「{0}」關鍵字的分頁，5秒後強制關閉chrome。", kName);
                                        notifyIcon.ShowBalloonTip(1000, "Attention!", str, System.Windows.Forms.ToolTipIcon.None);
                                        Thread.Sleep(5000);
                                        if (!k.HasExited)
                                        {
                                            k.Refresh();
                                            if (k.MainWindowTitle.ToLower().Contains(kName))  // 標題還是含keyword，強制關閉
                                            {
                                                Process[] chromeInstances = Process.GetProcessesByName("chrome");
                                                foreach (var c in chromeInstances)
                                                {
                                                    c.Kill();
                                                }
                                            }
                                        }
                                    }
                                    else  // 一般程式的標題
                                    {
                                        string str = string.Format("你開了含「{0}」關鍵字的程式，5秒後強制關閉。", kName);
                                        notifyIcon.ShowBalloonTip(1000, "Attention!", str, System.Windows.Forms.ToolTipIcon.None);
                                        Thread.Sleep(5000);
                                        if (!k.HasExited)
                                        {
                                            k.Refresh();
                                            if (k.MainWindowTitle.ToLower().Contains(kName))
                                            {
                                                k.Kill();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(5000);
            }
        }
        public ObservableCollection<PlanVM> GetTodayPlans()
        {
            using (MyDBContext db = new MyDBContext())
            {
                var res = (from o in (
                                        from p in db.Plans
                                        join r in db.Records on p.RID equals r.RID
                                        where r.Today.Date == DateTime.Today.Date
                                        orderby p.StartTime
                                        select p
                                     )
                           join b in db.Bans on o.BID equals b.BID
                           into g
                           from a in g.DefaultIfEmpty()
                           select new PlanVM() { plan = o, Ban = (a == null ? null : new BanVM() { ban = a }) }).ToList();
                return new ObservableCollection<PlanVM>(res);
            }
        }

        //ShowBanItemsCommand
        public ICommand ShowBanItemsCommand {
            get {
                return new GenericRelayCommand<BanVM>(ShowBanItems);
            }
        }
        private void ShowBanItems(BanVM ban)
        {
            if (ban == null)
            {
                MessageBox.Show("無", "封鎖列表");
            }
            else
            {
                MessageBox.Show(ban.Content, "封鎖列表");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;  // implement INotifyPropertyChanged 必須實作
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // UI的資料有更動的時，會修改資料
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
    class TodoList
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> Keys { get; set; }
        public List<string> Procs { get; set; }
        public string Tip { get; set; }
        public TodoList(int startTime, int endTime, BanVM ban, string tip)
        {
            Keys = new List<string>();
            Procs = new List<string>();
            StartTime = DateTime.Today.AddHours(startTime);
            EndTime = DateTime.Today.AddHours(endTime);
            if (ban != null)
            {
                if (ban.Content != null || ban.Content != "")
                {
                    string[] str = ban.Content.Split(';');
                    if (str[0] != null || str[0] != "")
                    {
                        string[] keyArr = str[0].Split(',');
                        for (int i = 0; i < keyArr.Length; i++)
                        {
                            Keys.Add(keyArr[i].ToLower());
                        }
                    }
                    if (str[1] != null || str[1] != "")
                    {
                        string[] procArr = str[1].Split(',');
                        for (int i = 0; i < procArr.Length; i++)
                        {
                            // 去附檔名
                            int index = procArr[i].IndexOf('.');
                            if (index != -1)
                            {
                                string s = procArr[i].Remove(index);
                                Procs.Add(procArr[i].ToLower());
                            }
                        }
                    }
                }
            }
            Tip = tip;
        }
    }
}
