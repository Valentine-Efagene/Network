﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Network_Monitor
{
    /// <summary>
    /// Interaction logic for UserControl_Dashboard.xaml
    /// </summary>
    public partial class UserControl_Dashboard : UserControl
    {
        public UserControl_Dashboard()
        {
            InitializeComponent();

            //UserControl pieChart = new UserControl_PieChart();
            //Grid_PieChart.Children.Add(pieChart);
        }
    }
}
