using FlightSimulatorApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MySimulatorViewModel MySimulatorViewModel { get; internal set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MySimulatorViewModel = new MySimulatorViewModel(new MySimulatorModel(new MyTelnetClient()));
            MainWindow = new MainWindow();
            MainWindow.Show();
        }


    }
}
