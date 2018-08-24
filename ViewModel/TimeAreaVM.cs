using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Attention.ViewModel
{
    public class TimeAreaVM : INotifyPropertyChanged
    {
        private double left;
        public double Left {
            get { return left; }
            set {
                if (left != value)
                {
                    left = value;
                    OnPropertyChanged("Left");
                }
            }
        }
        public double top;
        public double Top {
            get { return top; }
            set {
                if (top != value)
                {
                    top = value;
                    OnPropertyChanged("Top");
                }
            }
        }

        public double width;
        public double Width {
            get { return width; }
            set {
                if (width != value)
                {
                    width = value;
                    OnPropertyChanged("Width");
                }
            }
        }

        public Brush backgroundColor;
        public Brush BackgroundColor {
            get { return backgroundColor; }
            set {
                if (backgroundColor != value)
                {
                    backgroundColor = value;
                    OnPropertyChanged("BackgroundColor");
                }
            }
        }
        private int planNumber;
        public int PlanNumber {
            get { return planNumber; }
            set {
                if (planNumber != value)
                {
                    planNumber = value;
                    OnPropertyChanged("PlanNumber");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;  // implement INotifyPropertyChanged 必須實作
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // UI的資料有更動的時，會修改資料
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
