using Attention.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Attention.ViewModel
{
    public class BanListWindowVM
    {
        public ObservableCollection<string> Processes { get; set; }
        public ObservableCollection<Keyword> Keywords { get; set; }
        public string SelectedProcess { get; set; }
        public Keyword SelectedKeyword { get; set; }

        public BanListWindowVM()
        {
            Processes = new ObservableCollection<string>();
            Keywords = new ObservableCollection<Keyword>();
        }

        public ICommand AddProcessCommand {
            get {
                return new RelayCommand(AddProcess, null);
            }
        }
        public ICommand DeleteProcessCommand {
            get {
                return new RelayCommand(DeleteProcess, null);
            }
        }
        public ICommand AddKeywordCommand {
            get {
                return new RelayCommand(AddKeyword, null);
            }
        }
        public ICommand DeleteKeywordCommand {
            get {
                return new RelayCommand(DeleteKeyword, null);
            }
        }
        public ICommand SaveBanListCommand {
            get {
                return new RelayCommand(SaveBanList, null);
            }
        }
        private void AddProcess()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            if (file.SafeFileName!="")
            {
                Processes.Add(file.SafeFileName);
            }
        }
        private void DeleteProcess()
        {
            if(SelectedProcess != null)
            {
                Processes.Remove(SelectedProcess);
            }
        }
        private void AddKeyword()
        {
            Keywords.Add(new Keyword());
        }
        private void DeleteKeyword()
        {
            if (SelectedKeyword != null)
            {
                Keywords.Remove(SelectedKeyword);
            }
        }

        public void SaveBanList()
        {
            if (Keywords.Count ==0 && Processes.Count ==0)
            {
                return;
            }
            // keywords and processes ====> "kw1,kw2,kw3;p1,p2,p3"
            string temp = "";
            foreach (var k in Keywords)
            {
                if(k.Name!=null && k.Name != "")
                {
                    temp += (k.Name + ",");
                }
            }
            string content = "";
            if (temp.Length != 0)
            {
                content = temp.Remove(temp.Length - 1);
            }
            content += ";";
            temp = "";
            foreach (var p in Processes)
            {
                temp += (p + ",");
            }
            if (temp.Length != 0)
            {
                temp = temp.Remove(temp.Length - 1);
            }
            content += temp;
            using(MyDBContext db = new MyDBContext())
            {
                db.Bans.Add(new Ban() { Content = content });
                db.SaveChanges();
            }
            OnCloseView();
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
    public class Keyword : INotifyPropertyChanged
    {
        private string name;
        public string Name {
            get { return name; }
            set {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
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
