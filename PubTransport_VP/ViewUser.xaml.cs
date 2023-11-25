using System;
using System.Collections.Generic;
using System.Data;
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

namespace PubTransport_VP
{
    /// <summary>
    /// Логика взаимодействия для ViewUser.xaml
    /// </summary>
    public partial class ViewUser : Window
    {
        public ViewUser()
        {
            InitializeComponent();
        }
        public void SetData(DataView dataView)
        {
            DataGridUsers.ItemsSource = dataView;
        }
    }
}
