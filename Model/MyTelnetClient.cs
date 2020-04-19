using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using FlightSimulatorApp;
using System.Linq.Expressions;

namespace FlightSimulatorApp
{
    public class MyTelnetClient : ITelnetClient
    {
        TcpClient tcpClient;
        NetworkStream netStream;
         
        

        public void Connect(string ip, int port)
        {
            try
            {
                tcpClient = new TcpClient(ip, port);
                netStream = tcpClient.GetStream();
                netStream.ReadTimeout = 10000;
            }
            catch (IOException)
            {
                throw new IOException();

            }

        }

        public void Disconnect()
        {
            if (IsConnected()){
                netStream.Close();
                tcpClient.Close();
            }
        }

        public string Read()
        {
            if (IsConnected())
            {
                byte[] myReadBuffer = new byte[256];
                try
                {
                    netStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                }
                catch (IOException)
                {
                    Console.WriteLine("server isn't sending output.disconnecting.");
                    throw new IOException();
                    
                    
                  
                    

                }
                catch(FormatException)
                {
                    Console.WriteLine("Error in format sent from server");
                    throw new FormatException();
                }

                string commandRecived = Encoding.ASCII.GetString(myReadBuffer);
                
                return commandRecived;
            }
            return null;
            
        }

        public void Write(string command)
        {
            if (IsConnected())
            {
                byte[] commandToSend = Encoding.ASCII.GetBytes(command);
                try
                {
                    netStream.Write(commandToSend, 0, commandToSend.Length);
                }
                catch (IOException)
                {
                    Console.WriteLine("Error in writing to server");
                }
            }
        }

        public bool IsConnected()
        {
           
            if(tcpClient == null)
            {
                return false;
            }
           
            return tcpClient.Connected;
            
 
            
        }
    }
}

