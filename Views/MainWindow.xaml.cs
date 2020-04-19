using System;
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
using FlightSimulatorApp;
using FlightSimulatorApp.Views;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       


        public MainWindow()
        {

            InitializeComponent();
            
            DataContext = (Application.Current as App).MySimulatorViewModel;                 
        }

        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }
       
        private void Rudder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        public string ip = "127.0.0.1";
        private void B1_Click(object sender, RoutedEventArgs e)
        {
            //(Application.Current as App).MySimulatorViewModel.Connect("127.0.0.1", 5402);

        }


        private void B2_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).MySimulatorViewModel.Disconnect();

        }

        private void Controls_Loaded(object sender, RoutedEventArgs e)
        {

        }
                private void Set_Click(object sender, RoutedEventArgs e)
        {
            SettingsUserControl s = new SettingsUserControl();
            s.ShowDialog();
        }
    }
}
