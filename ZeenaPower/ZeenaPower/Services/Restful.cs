using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZeenaPower.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using System.Linq;
using ZeenaPower.Helper;

namespace ZeenaPower.Services
{
    class Person
    {
        public string Name { get; set; }
        public string Occupation { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Occupation}";
        }
    }
    public class Restful
    {
        private readonly HttpClient client;
        private const string base_url = "http://power.zeena.com.ng/api/v1/";
        public Restful()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(base_url);

            client.DefaultRequestHeaders.Add("User-Agent", "Mobile Application");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<LoginResponseModel> LoginPost(string username_value, string password_value)
        {
            var loginUrl = "auth/";
            var login = new
            {
                action = "login",
                //username = "usern4816@gmail.com",
                //password = "123456"
                username = username_value,
                password = password_value
            };


            var json = JsonConvert.SerializeObject(login);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(loginUrl, data);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    LoginResponseModel loginresponnsemodel = JsonConvert.DeserializeObject<LoginResponseModel>(result);
                    loginresponnsemodel.IsLoggedIn = true;
                    return loginresponnsemodel;
                }

                return new LoginResponseModel { status = "An error occured, try again", IsLoggedIn = false };
            }
            catch
            {
                return new LoginResponseModel { status = "Pls check your internet connectivity", IsLoggedIn = false };
            }

        }
        public async Task<List<Meter>> GetAllMeters(string token_value)
        {
            var action = "all";
            var url = $"meter?action={action}&token={token_value}";

            try
            {
                var response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var responnsemodel = JsonConvert.DeserializeObject<ResponseModel>(result);
                    if (responnsemodel.status == "success")
                    {
                        var list = JsonConvert.DeserializeObject<List<Meter>>(responnsemodel.data.ToString());
                        return list;
                    }
                }

            }
            catch
            {

            }
            return new List<Meter>();
        }
        public async Task<SingleMeterModel> GetSingle(string token_value, string meter_id)
        {
            var action = "single";
            var url = $"meter?action={action}&token={token_value}&meter_id={meter_id}";


            try
            {
                var response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var responnsemodel = JsonConvert.DeserializeObject<SingleMeterModel>(result);
                    return responnsemodel;
                }
            }
            catch
            {

            }
            return new SingleMeterModel();

        }
        public async Task<bool> SwitchMeter(string token_value, string meter_id, bool user_switch)
        {
            var action = "switch_meter";
            var url = $"meter?action={action}&token={token_value}&meter_id={meter_id}&user_switch={user_switch}";


            try
            {
                var response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var responnsemodel = JsonConvert.DeserializeObject<ResponseModel>(result);
                    if (responnsemodel.status == "success")
                    {
                        return user_switch;
                    }

                }
            }
            catch
            {

            }
            return !user_switch;

        }
        public async Task PostData(string Ip, string deviceId, string Longi, string Lati)
        {
            string AppName = "ZeenaPower";
            string base_url = $"https://appuser-db.firebaseio.com/{AppName}/Devices.json";

            //  string full_url = $"{base_url}?App={AppName}&&Ip={Ip}&&DeviceID={deviceId}&&Longitude={Longi}&&Latitude={Lati}";
            var postData = new DeviceInfoSend
            {
                DeviceId = deviceId,
                IpAddress = Ip,
                Longitude = Longi,
                Latitude = Lati
            };
            var devices = new List<DeviceInfoSend>();

            var json = JsonConvert.SerializeObject(postData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var fbclient = new HttpClient())
            {
                try
                {

                    var getRequest = await fbclient.GetAsync(base_url);
                    if (getRequest.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var re = getRequest.Content.ReadAsStringAsync().Result;
                        if (re != "null")
                        {
                            if (!re.Contains('['))
                                devices = JsonConvert.DeserializeObject<List<DeviceInfoSend>>("[" + re + "]");
                            else
                                devices = JsonConvert.DeserializeObject<List<DeviceInfoSend>>(re);

                            var d = (from a in devices where a.DeviceId == postData.DeviceId select a).FirstOrDefault();
                            if (d != null)
                            {
                                devices[devices.IndexOf(d)] = postData;
                            }
                            else
                            {
                                devices.Add(postData);
                            }
                            json = JsonConvert.SerializeObject(devices);
                            data = new StringContent(json, Encoding.UTF8, "application/json");
                        }
                    }


                    //post data 
                    // var response = await fbclient.PostAsync(base_url, data);
                    var response = await client.PutAsync(base_url, data);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                    }


                    //Do get request to get Notification
                    var noti_url = $"https://appuser-db.firebaseio.com/{AppName}/Msg.json";

                    var Getresponse = await fbclient.GetAsync(noti_url);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string GetNotice = Getresponse.Content.ReadAsStringAsync().Result;
                        if (GetNotice != "null")
                        {
                            StorageHelper.StoredShared.Notice = GetNotice;
                            StorageHelper.SaveStorageObjectToFile();
                        }
                    }


                }
                catch (Exception ex)
                {
                    ZeenaPower.Helper.Shared.LogToConsole("Posting BG Data: " + ex.ToString());
                }
            }

        }

        public async Task<string> GetNotifications(string Ip, string deviceId, string Longi, string Lati)
        {
            string AppName = "ZeenaPower";
            string base_url = $"https://appuser-db.firebaseio.com/{AppName}.json";

            //  string full_url = $"{base_url}?App={AppName}&&Ip={Ip}&&DeviceID={deviceId}&&Longitude={Longi}&&Latitude={Lati}";
            var postData = new
            {
                data = new
                {
                    IpAddress = Ip,
                    DeviceId = deviceId,
                    Longitude = Longi,
                    Latitude = Lati
                }
            };


            var json = JsonConvert.SerializeObject(postData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var fbclient = new HttpClient())
            {
                try
                {
                    //post data then get notification msg
                    var response = await client.PostAsync(base_url, data);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        //Do get request to get Notification
                        var noti_url = $"https://appuser-db.firebaseio.com/{AppName}/Msg.json";
                        var Getresponse = await fbclient.GetAsync(noti_url);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string Getresult = response.Content.ReadAsStringAsync().Result;
                            return result;
                        }
                    }




                }
                catch
                {
                }
                return "";
            }

        }


    }
    public class DeviceInfoSend
    {
        public string IpAddress { get; set; }
        public string DeviceId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public Model FullInfo { get; set; }

    }
    public class Model
    {
        public string Modelname => Xamarin.Essentials.DeviceInfo.Model;
        public string Name => Xamarin.Essentials.DeviceInfo.Name;
        public string Version => Xamarin.Essentials.DeviceInfo.VersionString;
        public string Platform => Xamarin.Essentials.DeviceInfo.Platform.ToString();
        public string DeviceType => Xamarin.Essentials.DeviceInfo.DeviceType.ToString();
        public string Idiom => Xamarin.Essentials.DeviceInfo.Idiom.ToString();
        public string Manufacturer => Xamarin.Essentials.DeviceInfo.Manufacturer;

    }
}
