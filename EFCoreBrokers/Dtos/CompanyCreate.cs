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
        public List<BrokerModel> Brokers { get; set; }
        public CompanyModel Company { get; set; }
    }
}
