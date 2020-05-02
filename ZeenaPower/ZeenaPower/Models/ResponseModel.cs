using System;
using System.Collections.Generic;
using System.Text;

namespace ZeenaPower.Models
{
  public  class ResponseModel
    {
        public string status { get; set; }
        public object data { get; set; }
    }
    public class SingleMeterModel
    {
        public string status { get; set; }
        public measurement measurement { get; set; }
        public Meter data { get; set; }
    }
    public class LoginResponseModel
    {
        public string token { get; set; }
        public User user { get; set; }
        public int meter { get; set; }
        public int invoice { get; set; }
        public int complaint { get; set; }
        public string status { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
