using System.Windows;

namespace FlightSimulatorApp.Views
{
    public partial class SettingsUserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).MySimulatorViewModel;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
          //(Application.Current as App).MySimulatorViewModel.Connect(ServerAdress.Text, 5402);
          //.ServerAdress.Text

        }
    }
}
