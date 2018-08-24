using Attention.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Attention
{
    /// <summary>
    /// SettingWindow.xaml 的互動邏輯
    /// </summary>
    public partial class SettingWindow : Window
    {
        private SettingWindowVM swvm = new SettingWindowVM();
        public SettingWindow()
        {
            InitializeComponent();
            this.DataContext = swvm;
            swvm.CloseView += Swvm_CloseView;
        }

        private void Swvm_CloseView()
        {
            this.Close();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (swvm.MouseDownRec != null)
            {
                swvm.ResetMouseSelectRec();
                return;
            }
            var rec = sender as Rectangle;
            rec.Fill = Brushes.Firebrick;  // select color
            swvm.MouseDownRec = rec;
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (swvm.MouseDownRec == null)
            {
                swvm.ResetMouseSelectRec();
                return;
            }
            var rec = sender as Rectangle;
            swvm.MouseUpRec = rec;
            int MouseDownNum = Convert.ToInt32(swvm.MouseDownRec.Tag);
            int MouseUpNum = Convert.ToInt32(swvm.MouseUpRec.Tag);
            if (MouseUpNum > MouseDownNum)
            {
                // 選的兩個時間之間，沒有做設定
                for (int i = MouseDownNum; i < MouseUpNum; i++)
                {
                    if (swvm.TimeIsUsed[i] == true)
                    {
                        MessageBox.Show("error select!");
                        swvm.ResetMouseSelectRec();
                        return;
                    }
                }
                // 標記已使用的區域
                for (int i = MouseDownNum; i < MouseUpNum; i++)
                {
                    swvm.TimeIsUsed[i] = true;
                }
                swvm.CurrentPlanVisibility = Visibility.Visible;
                // show this plan style
                PlanVM newPlan = new PlanVM();
                newPlan.StartTime = MouseDownNum;
                newPlan.EndTime = MouseUpNum;
                newPlan.planNumber = swvm.planCount;
                newPlan.BackgroundColor = swvm.colors[swvm.planCount % 24];
                swvm.CurrentPlan = newPlan;
                swvm.Plans.Add(newPlan);
                // draw selected area
                if (newPlan.StartTime < 12 && 12 < newPlan.EndTime)
                {
                    // 畫兩塊
                    // 第一塊  ex:10~12
                    TimeAreaVM timeArea1 = new TimeAreaVM();
                    timeArea1.Top = Canvas.GetTop(swvm.MouseDownRec);
                    timeArea1.Left = Canvas.GetLeft(swvm.MouseDownRec);
                    int size = 12 - MouseDownNum;
                    timeArea1.Width = size * 50;
                    timeArea1.BackgroundColor = swvm.CurrentPlan.BackgroundColor;
                    timeArea1.PlanNumber = swvm.planCount;
                    swvm.TimeAreas.Add(timeArea1);
                    // 第二塊
                    TimeAreaVM timeArea2 = new TimeAreaVM();
                    timeArea2.Top = 80;
                    timeArea2.Left = 0;
                    size = MouseUpNum - 12;
                    timeArea2.Width = size * 50;
                    timeArea2.BackgroundColor = swvm.CurrentPlan.BackgroundColor;
                    timeArea2.PlanNumber = swvm.planCount;
                    swvm.TimeAreas.Add(timeArea2);
                    swvm.planCount++;
                }
                else
                {
                    // 畫一塊
                    TimeAreaVM tmp = new TimeAreaVM();
                    tmp.Top = Canvas.GetTop(swvm.MouseDownRec);
                    tmp.Left = Canvas.GetLeft(swvm.MouseDownRec);
                    int size = MouseUpNum - MouseDownNum;
                    tmp.Width = size * 50;
                    tmp.BackgroundColor = swvm.CurrentPlan.BackgroundColor;
                    tmp.PlanNumber = swvm.planCount;
                    swvm.TimeAreas.Add(tmp);
                    swvm.planCount++;
                }
                swvm.ResetMouseSelectRec();
            }
            else
            {
                MessageBox.Show("error select!  from start time to end time");
                swvm.ResetMouseSelectRec();
            }
        }

        private void ColorArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var rec = sender as Rectangle;
            int num = Convert.ToInt32(rec.Tag);
            var res = (from p in swvm.Plans
                       where p.planNumber == num
                       select p).FirstOrDefault();
            swvm.CurrentPlan = res;
            swvm.CurrentPlanVisibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (swvm.SelectedRecord == null)
            {
                return;
            }
            swvm.SettingFormVisibility = Visibility.Visible;
            swvm.ResetRecord();
            swvm.CurrentPlan = null;
            swvm.CurrentPlanVisibility = Visibility.Hidden;
            // get plans and timearea
            using (MyDBContext db = new MyDBContext())
            {
                var res = (from p in db.Plans
                           where p.RID == swvm.SelectedRecord.RID
                           select new PlanVM() { plan = p}).ToList();
                foreach (var p in res)
                {
                    // set Ban(from Bans) && backgroundcolor
                    p.planNumber = swvm.planCount;
                    p.BackgroundColor = swvm.colors[p.planNumber%24];
                    var banVM = (from b in swvm.Bans
                                 where b.BID == p.bID
                                 select b).FirstOrDefault();
                    p.Ban = banVM;
                    swvm.Plans.Add(p);
                    swvm.AddTimeArea(p.StartTime, p.EndTime, p.planNumber);
                    // 標記已使用的區域
                    for (int i = p.StartTime; i < p.EndTime; i++)
                    {
                        swvm.TimeIsUsed[i] = true;
                    }
                    swvm.planCount++;
                }
            }
        }



        /*
        // just test
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(MyDBContext db = new MyDBContext())
            {
                var res = db.Bans.ToList();
                foreach (var item in res)
                {
                    db.Bans.Remove(item);
                }
                db.SaveChanges();
            }
            MessageBox.Show("OK");
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RunWindow runWindow = new RunWindow();
            runWindow.Show();
            Application.Current.MainWindow.Close();
        }
        */

    }
}
