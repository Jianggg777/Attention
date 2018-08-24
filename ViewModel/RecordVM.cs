using Attention.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention.ViewModel
{
    public class RecordVM:INotifyPropertyChanged
    {
        public Record record;
        public int RID {
            get { return record.RID; }
            set {
                if (record.RID != value)
                {
                    record.RID = value;
                    OnPropertyChanged("RID");
                }
            }
        }
        public string Name {
            get { return record.Name; }
            set {
                if (record.Name != value)
                {
                    record.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public DateTime Today {
            get { return record.Today; }
            set {
                if (record.Today != value)
                {
                    record.Today = value;
                    OnPropertyChanged("Today");
                }
            }
        }

        public bool IsSaved {
            get { return record.IsSaved; }
            set {
                if (record.IsSaved != value)
                {
                    record.IsSaved = value;
                    OnPropertyChanged("IsSaved");
                }
            }
        }

        public RecordVM()
        {
            record = new Record();
        }

        public event PropertyChangedEventHandler PropertyChanged;  // implement INotifyPropertyChanged 必須實作
        public void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null) // UI的資料有更動的時，會修改資料
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
