using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Attention.ViewModel
{
    class SettingWindowVM : INotifyPropertyChanged
    {
        private BanListWindow banListWindow;

        private Visibility settingFormVisibility;    // 整個step2
        public Visibility SettingFormVisibility {
            get { return settingFormVisibility; }
            set {
                if (value != settingFormVisibility)
                {
                    settingFormVisibility = value;
                    OnPropertyChanged("SettingFormVisibility");
                }
            }
        }
        private Visibility currentPlanVisibility;   // step2的form
        public Visibility CurrentPlanVisibility {
            get { return currentPlanVisibility; }
            set {
                if (value != currentPlanVisibility)
                {
                    currentPlanVisibility = value;
                    OnPropertyChanged("CurrentPlanVisibility");
                }
            }
        }

        private PlanVM currentPlan;
        public PlanVM CurrentPlan {
            get { return currentPlan; }
            set {
                if (value != currentPlan)
                {
                    currentPlan = value;
                    OnPropertyChanged("CurrentPlan");
                }
            }
        }

        public Rectangle MouseDownRec { get; set; }
        public Rectangle MouseUpRec { get; set; }

        private RecordVM selectedRecord;
        public RecordVM SelectedRecord {
            get { return selectedRecord; }
            set {
                if (selectedRecord != value)
                {
                    selectedRecord = value;
                    OnPropertyChanged("SelectedRecord");
                }
            }
        }

        private bool isSavedRecord;
        public bool IsSavedRecord {
            get { return isSavedRecord; }
            set {
                if (isSavedRecord != value)
                {
                    isSavedRecord = value;
                    OnPropertyChanged("IsSavedRecord");
                }
            }
        }
        private string recordName;
        public string RecordName {
            get { return recordName; }
            set {
                if (recordName != value)
                {
                    recordName = value;
                    OnPropertyChanged("RecordName");
                }
            }
        }

        public ObservableCollection<PlanVM> Plans { get; set; }
        private ObservableCollection<RecordVM> records;
        public ObservableCollection<RecordVM> Records {// step1的combobox
            get { return records; }
            set {
                if (records != value)
                {
                    records = value;
                    OnPropertyChanged("Records");
                }
            }
        }

        private ObservableCollection<BanVM> bans;
        public ObservableCollection<BanVM> Bans {  // step2的combobox
            get { return bans; }
            set {
                if (bans != value)
                {
                    bans = value;
                    OnPropertyChanged("Bans");
                }
            }
        }
        private ObservableCollection<TimeAreaVM> timeAreas;
        public ObservableCollection<TimeAreaVM> TimeAreas { //plans的著色區塊 
            get { return timeAreas; }
            set {
                if(timeAreas != value)
                {
                    timeAreas = value;
                    OnPropertyChanged("TimeAreas");
                }
            }
        }  

        // field
        public Brush[] colors;
        public int planCount;
        public bool[] TimeIsUsed;

        public SettingWindowVM()
        {
            SettingFormVisibility = Visibility.Hidden;
            CurrentPlanVisibility = Visibility.Hidden;
            colors = new Brush[] {
                Brushes.PaleGreen, Brushes.Pink,Brushes.Aquamarine,Brushes.Salmon,Brushes.Khaki,Brushes.Silver,
                Brushes.Plum,Brushes.Yellow,Brushes.Chartreuse,Brushes.LightSkyBlue,Brushes.Orange,Brushes.Peru,
                Brushes.YellowGreen,Brushes.RosyBrown,Brushes.OrangeRed,Brushes.DarkKhaki,Brushes.Gold,Brushes.Gray,
                Brushes.Sienna,Brushes.Teal,Brushes.Red,Brushes.Navy,Brushes.Indigo,Brushes.Lime
            };
            Records = GetRecords();
            Bans = GetBans();
            ResetRecord();
        }
        public void ResetRecord()
        {
            TimeIsUsed = new bool[24];
            Plans = new ObservableCollection<PlanVM>();
            TimeAreas = new ObservableCollection<TimeAreaVM>();
            planCount = 0;
        }

        public ICommand NewRecordCommand {
            get {
                return new RelayCommand(NewRecord, null);
            }
        }
        private void NewRecord()
        {
            SettingFormVisibility = Visibility.Visible;
            SelectedRecord = null;
            ResetRecord();
            CurrentPlan = null;
            CurrentPlanVisibility = Visibility.Hidden;
        }

        public ICommand OpenBanListWindowCommand {
            get {
                return new RelayCommand(OpenBanListWindow, null);
            }
        }
        private void OpenBanListWindow()
        {
            banListWindow = new BanListWindow();
            banListWindow.blwvm.CloseView += Blwvm_CloseView;
            banListWindow.Show();
        }

        private void Blwvm_CloseView()
        {
            using (MyDBContext db = new MyDBContext())
            {
                var res = (from b in db.Bans
                           select new BanVM() { ban = b }).LastOrDefault();
                if (res != Bans.LastOrDefault())
                {
                    Bans.Add(res);
                }
            }
        }


        public ICommand DeleteCurrentPlanCommand {
            get {
                return new RelayCommand(DeleteCurrentPlan, null);
            }
        }
        private void DeleteCurrentPlan()
        {
            // delete timeArea
            var res = (from t in TimeAreas
                       where t.PlanNumber == CurrentPlan.planNumber
                       select t).ToList();
            foreach (var item in res)
            {
                TimeAreas.Remove(item);
            }
            // set isUsed
            for (int i = CurrentPlan.StartTime; i < CurrentPlan.EndTime; i++)
            {
                TimeIsUsed[i] = false;
            }
            // delete plan
            Plans.Remove(CurrentPlan);
            CurrentPlan = null;
            CurrentPlanVisibility = Visibility.Hidden;
        }

        public ICommand SavePlanCommand {
            get {
                return new RelayCommand(SavePlan, null);
            }
        }
        private void SavePlan()
        {
            MessageBox.Show("OK!");
        }

        public ICommand RunClockCommand {
            get {
                return new RelayCommand(RunClock, null);
            }
        }

        private void RunClock()
        {
            if(Plans.Count == 0)
            {
                return;
            }
            if(IsSavedRecord == true)
            {
                if( RecordName != "" || RecordName != null)
                {
                    using(MyDBContext db = new MyDBContext())
                    {
                        db.Records.Add(new Model.Record() { Name = RecordName, IsSaved = true, Today = DateTime.Today.Date });
                        db.SaveChanges();
                        var rid = (from r in db.Records
                                   where r.Today == DateTime.Today.Date
                                   select r.RID).FirstOrDefault();
                        foreach (var p in Plans)
                        {
                            db.Plans.Add(new Model.Plan() { RID = rid, BID =(p.Ban==null?0:p.Ban.BID), StartTime = p.StartTime, EndTime = p.EndTime, Tip = p.Tip });
                        }
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                using (MyDBContext db = new MyDBContext())
                {
                    db.Records.Add(new Model.Record() { Today = DateTime.Today.Date });
                    db.SaveChanges();
                    var rid = (from r in db.Records
                               where r.Today == DateTime.Today.Date
                               select r.RID).FirstOrDefault();
                    foreach (var p in Plans)
                    {
                        db.Plans.Add(new Model.Plan() { RID = rid, BID = (p.Ban == null ? 0 : p.Ban.BID), StartTime = p.StartTime, EndTime = p.EndTime, Tip = p.Tip });
                    }
                    db.SaveChanges();
                }
            }
            RunWindow runWindow = new RunWindow();
            runWindow.Show();
            OnCloseView();
        }

        public ObservableCollection<RecordVM> GetRecords()
        {
            using (MyDBContext db = new MyDBContext())
            {
                var res = (from r in db.Records
                           where r.IsSaved == true
                           select new RecordVM() { record = r }
                           ).ToList();
                ObservableCollection<RecordVM> records = new ObservableCollection<RecordVM>(res);
                return records;
            }
        }
        public ObservableCollection<BanVM> GetBans()
        {
            using (MyDBContext db = new MyDBContext())
            {
                var res = (from b in db.Bans
                           select new BanVM { ban = b }
                           ).ToList();
                ObservableCollection<BanVM> bans = new ObservableCollection<BanVM>(res);
                return bans;
            }
        }
        public void ResetMouseSelectRec()
        {
            if (MouseDownRec != null)
            {
                MouseDownRec.Fill = Brushes.Black;
            }
            if (MouseUpRec != null)
            {
                MouseUpRec.Fill = Brushes.Black;
            }
            MouseDownRec = null;
            MouseUpRec = null;
        }

        public void AddTimeArea(int startTime, int endTime, int planNumber)
        {
            // 標記已使用的區域
            for (int i = startTime; i < endTime; i++)
            {
                TimeIsUsed[i] = true;
            }
            if (startTime < 12 && 12 < endTime)
            {
                // 畫兩塊
                // 第一塊  ex:10~12
                TimeAreaVM timeArea1 = new TimeAreaVM();
                timeArea1.Top = 0; 
                timeArea1.Left = startTime * 50;  
                int size = 12 - startTime;
                timeArea1.Width = size * 50;
                timeArea1.BackgroundColor = colors[planNumber];
                timeArea1.PlanNumber = planNumber;
                TimeAreas.Add(timeArea1);
                // 第二塊
                TimeAreaVM timeArea2 = new TimeAreaVM();
                timeArea2.Top = 80;
                timeArea2.Left = 0;
                size = endTime - 12;
                timeArea2.Width = size * 50;
                timeArea2.BackgroundColor = colors[planNumber];
                timeArea2.PlanNumber = planNumber;
                TimeAreas.Add(timeArea2);
            }
            else
            {
                // 畫一塊
                if (startTime<=12)
                {
                    TimeAreaVM timeArea = new TimeAreaVM();
                    timeArea.Top = 0;   
                    timeArea.Left = startTime*50;  
                    int size = endTime - startTime;
                    timeArea.Width = size * 50;
                    timeArea.BackgroundColor = colors[planNumber];
                    timeArea.PlanNumber = planNumber;
                    TimeAreas.Add(timeArea);
                }
                else
                {
                    TimeAreaVM timeArea = new TimeAreaVM();
                    timeArea.Top = 80;
                    timeArea.Left = (startTime-12) * 50;  
                    int size = endTime - startTime;
                    timeArea.Width = size * 50;
                    timeArea.BackgroundColor = colors[planNumber];
                    timeArea.PlanNumber = planNumber;
                    TimeAreas.Add(timeArea);
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // UI的資料有更動的時，會修改資料
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public delegate void CloseEventHandler();
        public event CloseEventHandler CloseView;
        private void OnCloseView()
        {
            if (CloseView != null)
            {
                CloseView();
            }
        }
    }
}
