using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ZeenaPower.Helper
{
    public class PermissionCheck
    {
        public async Task<bool> CheckStorageAccess()
        {
            //  try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                    {

                        //   await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    status = results[Permission.Storage];
                }

                else if (status == PermissionStatus.Granted)
                {
                    return true;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    return false;
                }

                return true;

            }
        }

        public async Task<string> CheckLocationAccess()
        {
            Shared.LogToConsole("CheckLocationAccess0");

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                Shared.LogToConsole("CheckLocationAccess2");

                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    Shared.LogToConsole("CheckLocationAccess3");

                    //   await DisplayAlert("Need location", "Gunna need that location", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                Shared.LogToConsole("CheckLocationAccess4");
                status = results[Permission.Location];
                Shared.LogToConsole("CheckLocationAccess5");

            }
            else if (status == PermissionStatus.Granted)
            {
                Shared.LogToConsole("CheckLocationAccess6");

                var notices = await GetLocationData();
                Shared.LogToConsole("CheckLocationAccess7");

                if (!string.IsNullOrEmpty(notices))
                    return notices + "success";
            }
            else if (status != PermissionStatus.Denied)
            {
                Shared.LogToConsole("CheckLocationAccess8");

                return "Denied";
            }
            else if (status != PermissionStatus.Disabled)
            {
                Shared.LogToConsole("CheckLocationAccess9");

                return "Phone not supported";
            }
            else if (status != PermissionStatus.Restricted)
            {
                Shared.LogToConsole("CheckLocationAccess10");

                return "Restricted by device";
            }
            else if (status != PermissionStatus.Unknown)
            {
                Shared.LogToConsole("CheckLocationAccess11");
                return "Unknown";
            }
            Console.WriteLine("InfoWARE Finance: Leaving GetLocationAccess;");
            return "success";
        }
        public async Task<string> GetLocationData()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, new TimeSpan(0, 0, 1));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Shared.LogToConsole("Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                    StorageHelper.StoredShared.SavedLongitude = location.Longitude;
                    Shared.LogToConsole("After getting Longitude");

                    StorageHelper.StoredShared.SavedLatitude = location.Latitude;
                    Shared.LogToConsole("After getting Latitude");

                    StorageHelper.SaveStorageObjectToFile();
                    Shared.LogToConsole("After Saving object");

                }
                Shared.LogToConsole("Leaving GetLocationData3");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine("Device not supported" + fnsEx.Message);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                Console.WriteLine("Not enabled" + fneEx.Message);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Console.WriteLine(pEx.Message);
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine("InfoWARE Finance: Other exception;" + ex.Message);
            }
            Console.WriteLine("InfoWARE Finance: Leaving GetLocationData;");
            return "";
        }
    }
}
