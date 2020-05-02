using System;
using System.Collections.Generic;
using System.Text;

namespace ZeenaPower.Models
{
   public class Meter
    {
        public string meter_id { get; set; }
        public string tariff { get; set; }
        public DateTime StartDate { get; set; }
        public string house_number { get; set; }
        public string street_name { get; set; }
        public bool connectivity { get; set; }
        public string status_color => (connectivity) ? "green" : "red";

    }

    public class measurement
    {
        public bool connectivity { get; set; }
      
        public double meter { get; set; }
        public double power { get; set; }
        public double voltage { get; set; }
        public double energy { get; set; }
        public double frequency { get; set; }
        public double current { get; set; }
        public double cost { get; set; }
        public double current_cost_reading { get; set; }
        public DateTime dump_time { get; set; }
        public DateTime created_at { get; set; }
        public DateTime datetime { get; set; }
    }
}
