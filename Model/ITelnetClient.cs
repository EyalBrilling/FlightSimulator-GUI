using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public interface ITelnetClient
    {
        void Connect(string ip, int port);
        void Disconnect();
        void Write(string command);
        string Read();

        bool IsConnected();


    }
}