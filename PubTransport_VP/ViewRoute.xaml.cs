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
    /// Логика взаимодействия для ViewRoute.xaml
    /// </summary>
    public partial class ViewRoute : Window
    {
        public ViewRoute()
        {
            InitializeComponent();
        }
        public void SetData(DataView dataView)
        {
            DataGridRoutes.ItemsSource = dataView;
        }
    }
}
