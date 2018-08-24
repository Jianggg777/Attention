using Attention.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Attention.ViewModel
{
    public class PlanVM : INotifyPropertyChanged
    {
        // set plan, Ban, BackgroundColor
        public int planNumber;

        public Plan plan;
        public PlanVM()
        {
            plan = new Plan();
            bID = plan.BID;
        }

        public int StartTime {
            get { return plan.StartTime; }
            set {
                if (value != plan.StartTime)
                {
                    plan.StartTime = value;
                    OnPropertyChanged("StartTime");
                }
            }
        }

        public int EndTime {
            get { return plan.EndTime; }
            set {
                if (value != plan.EndTime)
                {
                    plan.EndTime = value;
                    OnPropertyChanged("EndTime");
                }
            }
        }

        public int bID;  

        private BanVM ban;
        public BanVM Ban {
            get { return ban; }
            set {
                if (ban != value)
                {
                    ban = value;
                    OnPropertyChanged("Ban");
                }
            }
        }


        public string Tip {
            get { return plan.Tip; }
            set {
                if (value != plan.Tip)
                {
                    plan.Tip = value;
                    OnPropertyChanged("Tip");
                }
            }
        }

        private Brush backgroundColor;
        public Brush BackgroundColor {
            get { return backgroundColor; }
            set {
                if (value != backgroundColor)
                {
                    backgroundColor = value;
                    OnPropertyChanged("BackgroundColor");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // UI的資料有更動的時，會修改資料
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
