using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZeenaPower.Droid.InterfaceHelper;
using ZeenaPower.Interfaces;
using static Android.Provider.Settings;
using Xamarin.Forms;

[assembly:Dependency(typeof(DeviceInformation))]
namespace ZeenaPower.Droid.InterfaceHelper
{
    class DeviceInformation : IDeviceInformation
    {
        public string GetDeviceId()
        {
            string id = Android.OS.Build.Serial;
            if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
            {
                try
                {
                    var context = Android.App.Application.Context;
                    id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                }
                catch (Exception ex)
                {
                    Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                    id = "Null";
                }
            }

            return id;
        }
    }
}