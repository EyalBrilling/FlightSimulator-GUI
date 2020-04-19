using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace FlightSimulatorApp
{
    public interface ISimulatorModel : INotifyPropertyChanged
    {

        void Connect(string ip, int port);

        void Start();
        void Disconnect();

        double Degree
        {
            get;
            set;
        }

        double VerticalSpeed
        {
            get;
            set;
        }

        double GroundSpeed
        {
            get;
            set;
        }

        double AirSpeed
        {
            get;
            set;
        }

        double GpsAltitude
        {
            get;
            set;
        }

        double RollDegree
        {
            get;
            set;
        }

        double PitchDegree
        {
            get;
            set;
        }

        double AltimeterAltitude
        {
            get;
            set;
        }

        double Longitude
        {
            get;
            set;
        }

        double Latitude
        {
            get;
            set;
        }
        Visibility Err_Out_Of_Bounds { get; set; }
        Visibility Err_Server_IO { get; set; }

        Visibility Err_Server_Format { get; set; }
        Visibility Err_visiblity_Not_Connected { get; set; }

        void ChangeThrottle(double value);
        void ChangeRudder(double value);
        void ChangeCoordinates(double elevator, double aileron);

     
    }
}