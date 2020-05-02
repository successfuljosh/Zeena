using System;
using System.Collections.Generic;
using System.Text;

namespace ZeenaPower.Models
{
   public class Transaction
    {
        public string RefId { get; set; }
        public string MeterId { get; set; }
        public string Tariff { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
    }
}
