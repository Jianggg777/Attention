using Attention.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// BanListWindow.xaml 的互動邏輯
    /// </summary>
    public partial class BanListWindow : Window
    {
        public BanListWindowVM blwvm = new BanListWindowVM();
        public BanListWindow()
        {
            InitializeComponent();
            this.DataContext = blwvm;
            blwvm.CloseView += Blwvm_CloseView;
        }

        private void Blwvm_CloseView()
        {
            this.Close();
        }
    }
}
