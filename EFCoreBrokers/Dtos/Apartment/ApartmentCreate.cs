using EFCoreBrokers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Dtos
{
    public class ApartmentCreate
    {
        public ApartmentModel Apartment { get; set; }
        public BrokerModel Broker { get; set; }
        public CompanyModel Company { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public List<BrokerModel> Brokers { get; set; } = new();
        public List<CompaniesBrokers> companiesBrokers { get; set; }

    }
}
