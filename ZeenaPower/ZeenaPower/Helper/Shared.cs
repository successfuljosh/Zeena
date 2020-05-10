using System;
using System.Collections.Generic;
using System.Text;
using ZeenaPower.Interfaces;
using ZeenaPower.Models;

namespace ZeenaPower.Helper
{
    public class Shared
    {
        public LoginResponseModel LoggedInModel { get; set; }
        public string DeviceId
        {
            get
            {
                return Xamarin.Forms.DependencyService.Get<IDeviceInformation>().GetDeviceId();
            }
        }
        public string IPAddress
        {
            get
            {
                foreach (var IpAddress in System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()))
                    if (IpAddress != null)
                    {
                        return IpAddress.ToString();
                    }
                return "";
            }
        }

        public double SavedLongitude { get; set; }
        public double SavedLatitude { get; set; }
        public string Notice { get; set; }


        public static void LogToConsole(string msg)
        {
            Console.WriteLine($"Zeena Power: {msg}");
        }
    }
}
