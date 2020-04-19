using System;
using System.Collections.Generic;
using System.Text;
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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Controls.xaml
    /// </summary>
    public partial class Controls : UserControl
    {
       
        public Controls()
        {
            InitializeComponent();
           
        }
        private void Rudder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (Application.Current as App).MySimulatorViewModel.VM_Rudder = Rudder.Value;

        }

        private void Joystick_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (Application.Current as App).MySimulatorViewModel.VM_Throttle = Throttle.Value;

        }
    }
}
