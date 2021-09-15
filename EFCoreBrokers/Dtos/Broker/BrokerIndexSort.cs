using EFCoreBrokers.Dtos.SortFilter;
using EFCoreBrokers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Dtos.Broker
{
    public class BrokerIndexSort
    {
        public List<BrokerModel> Brokers { get; set; }
        public string SortBy { get; set; } 
        public List<string> SortSelection { get; set; } = new() { "Name", "Surname" };
    }
}
