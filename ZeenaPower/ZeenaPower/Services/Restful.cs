using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZeenaPower.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;

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
            client.BaseAddress =  new Uri(base_url);

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


    }

}
