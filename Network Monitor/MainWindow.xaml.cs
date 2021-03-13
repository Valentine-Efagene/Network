using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WindowState oldWindowState = WindowState.Maximized;
        WindowState newWindowState = WindowState.Maximized;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            UserControl dashboard = new UserControl_Dashboard();
            Grid_Main.Children.Add(dashboard);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == Window.WindowStateProperty)
            {
                var oldState = (WindowState)e.OldValue;
                var newState = (WindowState)e.NewValue;

                oldWindowState = oldState;
                newWindowState = newState;
                Debug.WriteLine("{0} -> {1}", oldState, newState);
            }
        }

        private void Button_WindowClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_ExpandMenu_Click(object sender, RoutedEventArgs e)
        {
            Button_ExpandMenu.Visibility = Visibility.Collapsed;
            Button_CollapseMenu.Visibility = Visibility.Visible;
        }

        private void Button_CollapseMenu_Click(object sender, RoutedEventArgs e)
        {
            Button_CollapseMenu.Visibility = Visibility.Collapsed;
            Button_ExpandMenu.Visibility = Visibility.Visible;
        }

        private void ListView_Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            Button_WindowMaximize.Visibility = Visibility.Collapsed;
            Button_WindowRestore.Visibility = Visibility.Visible;
        }

        private void Button_WindowRestore_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
            Button_WindowRestore.Visibility = Visibility.Collapsed;
            Button_WindowMaximize.Visibility = Visibility.Visible;
        }
    }
}
