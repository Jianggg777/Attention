using Attention.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention.ViewModel
{
    public class BanVM:INotifyPropertyChanged
    {
        public Ban ban;
        public BanVM()
        {
            ban = new Ban();
        }
        public int BID {
            get { return ban.BID; }
            set {
                if (ban.BID != value)
                {
                    ban.BID = value;
                    OnPropertyChanged("BID");
                }
            }
        }

        public string Content {
            get { return ban.Content; }
            set {
                if (ban.Content != value)
                {
                    ban.Content = value;
                    OnPropertyChanged("Content");
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
