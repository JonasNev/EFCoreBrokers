using EFCoreBrokers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Dtos.Broker
{
    public class BrokerDetails
    {
        public BrokerModel Broker { get; set; }
        public List<ApartmentModel> Apartments { get; set; }
    }
}
