using FlightSimulatorApp;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
//using FlightSimulator.Model;

namespace FlightSimulatorApp
{
    public class MySimulatorViewModel : ISimulatorViewModel
    {
        private ISimulatorModel model;
        private double throttle;
        private double rudder;
        private double elevator;
        private double aileron;
        private double lon;
        private double lat;
        
                string settings_ip_saved;
        string settings_info_port_saved;
        string settings_command_port_saved;

        public ICommand Settings_Ok_Click { get; set; }
        public ICommand Settings_Cancel_Click { get; set; }
        //MW_ConnectCommand
        public ICommand MW_ConnectCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;



        public MySimulatorViewModel(ISimulatorModel sm)
        {
            this.model = sm;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                OnPropertyChanged("VM_" + e.PropertyName);
                if (e.PropertyName.ToLower().Contains("lon") || e.PropertyName.ToLower().Contains("lat"))
                {
                    MySimulatorModel se = sender as MySimulatorModel;
                     lon = se.longitude;
                     lat = se.latitude;
                    Location loc = new Location(lat, lon);
                    VM_Location = loc;
                }
            };
            Err_visiblity_Cannot_Connect = Visibility.Collapsed;
            model.Err_Server_Format = Visibility.Collapsed;
            VM_Err_visiblity_Not_Connected = Visibility.Visible;
            model.Err_Server_IO = Visibility.Collapsed;

            color = new SolidColorBrush();
            color.Color = System.Windows.Media.Color.FromRgb(255,0,0);
            VM_Color = color;
      
         VM_Ip_address = "127.0.0.1";
            VM_Info_port = "5402";
            VM_Command_port = "5400";
            settings_ip_saved = VM_Ip_address;
            settings_info_port_saved = VM_Info_port;
            settings_command_port_saved = VM_Command_port;


            Settings_Cancel_Click = new RelayCommand(SettingsCancelClicked);
            Settings_Ok_Click = new RelayCommand(SettingsOkClicked);
            MW_ConnectCommand = new RelayCommand(Connect);
         }
        void SettingsOkClicked(object o)
        {
            settings_ip_saved = VM_Ip_address;
            settings_info_port_saved = VM_Info_port;
            settings_command_port_saved = VM_Command_port;
            MessageBox.Show("Succefully Saved");
            //return true;
        }
        void SettingsCancelClicked(object o)
        {
            VM_Ip_address=settings_ip_saved;
            VM_Info_port=settings_info_port_saved  ;
           VM_Command_port= settings_command_port_saved  ;

        }


        //Proerties
        public double VM_Degree
        {
            get { return model.Degree; }
            set
            {
                model.Degree = value;
                OnPropertyChanged("VM_Degree");
            }
        }


        public double VM_Longitude
        {
            get { return model.Longitude; }
            set
            {
                model.Longitude = value;
        
                OnPropertyChanged("VM_Longitude");
            }
        }


        public double VM_Latitude
        {
            get { return model.Latitude; }
            set
            {
                model.Latitude = value;
             
                OnPropertyChanged("VM_Latitude");
            }
        }




        private Location location;
        public Location VM_Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("VM_Location");
            }
        }

        



        public double VM_VerticalSpeed
        {
            get { return model.VerticalSpeed; }

        }

        public double VM_GroundSpeed
        {
            get { return model.GroundSpeed; }

        }

        public double VM_AirSpeed
        {
            get { return model.AirSpeed; }

        }

        public double VM_GpsAltitude
        {
            get { return model.GpsAltitude; }

        }
        public double VM_RollDegree
        {
            get { return model.RollDegree; }

        }
        public double VM_PitchDegree
        {
            get { return model.PitchDegree; }

        }
        public double VM_AltimeterAltitude
        {
            get { return model.AltimeterAltitude; }

        }


        public double VM_Throttle
        {
            get { return this.throttle; }
            set
            {
                if (this.throttle != value)
                {
                    this.throttle = value;

                    model.ChangeThrottle(throttle);
                }
            }
        }

        public double VM_Rudder
        {
            get { return rudder; }
            set
            {
                if (this.rudder != value)
                {
                    this.rudder = value;
                    model.ChangeRudder(rudder);
                    OnPropertyChanged("VM_Rudder");
                }
            }
        }

        //Err_visiblity
        //private Visibility err_Out_Of_Bounds;
        public Visibility VM_Err_Out_Of_Bounds
        {
            get { return model.Err_Out_Of_Bounds; }
          
          

           
        }

        //Error in server output format
        //private Visibility VM_err_server_format;
        public Visibility VM_Err_Server_Format
        {
            get { return model.Err_Server_IO; }
        
        }

        //private Visibility VM_err_server_IO;
        public Visibility VM_Err_Server_IO
        {
            get { return model.Err_Server_IO; }

        }

        private Visibility VM_err_msg_error_server;
        public Visibility VM_Err_Msg_Error_Server
        {
            get { return VM_err_msg_error_server; }
            set
            {
                this.VM_err_msg_error_server = value;
                OnPropertyChanged("VM_Err_visiblity_Error_Server");

            }
        }


        private Visibility err_msg_cannot_connect;
        public Visibility Err_visiblity_Cannot_Connect
        {
            get { return err_msg_cannot_connect; }
            set
            {
                this.err_msg_cannot_connect = value;

                OnPropertyChanged("Err_visiblity_Cannot_Connect");

            }
        }

        private Visibility VM_err_msg_not_connected;
        public Visibility VM_Err_visiblity_Not_Connected
        {
            get { return model.Err_visiblity_Not_Connected; }
            set
            {
                this.VM_err_msg_not_connected = value;
                OnPropertyChanged("VM_Err_visiblity_Not_Connected");

            }
        }

        //Elevator and Rudder are controlled by joystick and get changed together,so using function.

        public void ChangeCoordinates(double elevator, double aileron)
        {
            VM_ailron = aileron;
            VM_elevator = elevator;

            this.elevator = elevator;
            this.aileron = aileron;
            model.ChangeCoordinates(elevator, aileron);
        }
        


        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public void Connect(object o)
        {

            try
            {
                
                Err_visiblity_Cannot_Connect = Visibility.Collapsed;
                 model.Connect(VM_Ip_address, int.Parse(VM_Info_port));
                color.Color = System.Windows.Media.Color.FromRgb(0, 255, 0);
            }
            catch
            {
                color.Color = System.Windows.Media.Color.FromRgb(255, 0, 0);
                Err_visiblity_Cannot_Connect = Visibility.Visible;

            }
            VM_Err_visiblity_Not_Connected = Visibility.Collapsed;

        }

        public void Disconnect()
        {
            VM_Err_visiblity_Not_Connected = Visibility.Visible;
            model.Err_Server_IO = Visibility.Collapsed;
            color.Color = System.Windows.Media.Color.FromRgb(255, 0, 0);
            model.Disconnect();
        }

        private SolidColorBrush color;
        public SolidColorBrush VM_Color
        {
            get { return color; }
            set
            {
                if (this.color != value)
                {
                    this.color = value;

                    OnPropertyChanged("VM_Color");
                }
            }
        }
        
                #region ailron and elevator
        private double vm_ailron;
        public double VM_ailron
        {
            get { return vm_ailron; }
            set
            {

                this.vm_ailron = value;

                OnPropertyChanged("VM_ailron");

            }
        }
        private double vm_elevator;
        public double VM_elevator
        {
            get { return vm_elevator; }
            set
            {
                this.vm_elevator = value;
                OnPropertyChanged("VM_elevator");
            }
        }
        #endregion

        #region for settings
        private string ip_address;
        public string VM_Ip_address
        {
            get { return ip_address; }
            set
            {

                    this.ip_address = value;

                    OnPropertyChanged("VM_Ip_address");

            }
        }
        private string info_port;
        public string VM_Info_port
        {
            get { return info_port; }
            set
            {

                this.info_port = value;

                OnPropertyChanged("VM_Info_port");

            }
        }

        private string command_port;
        public string VM_Command_port
        {
            get { return command_port; }
            set
            {

                this.command_port = value;

                OnPropertyChanged("VM_Command_port");

            }
        }
        #endregion

    }
}
