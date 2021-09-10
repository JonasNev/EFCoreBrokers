using EFCoreBrokers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBrokers.Dtos
{
    public class CompanyCreate
    {
        public List<BrokerModel> Brokers { get; set; } = new();
        public CompanyModel Company { get; set; }
        public List<BrokerModel> CompanyBrokers { get; set; } = new List<BrokerModel>();
        public List<int> BrokerIds { get; set; }
    }
}
