using System;
using System.Collections.Generic;
using System.Text;

namespace ZeenaPower.Models
{
    public enum actionType
    {
        login,
        forgot,
        reset,
        all,
        single,
        switch_meter
    }
    public class RequestModel
    {
        public actionType action { get; set; }
    }

}
