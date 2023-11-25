﻿using System;
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
    /// Логика взаимодействия для ViewTicket.xaml
    /// </summary>
    public partial class ViewTicket : Window
    {
        public ViewTicket()
        {
            InitializeComponent();
        }
        public void SetData(DataView dataView)
        {
            DataGridTickets.ItemsSource = dataView;
        }
    }
}
