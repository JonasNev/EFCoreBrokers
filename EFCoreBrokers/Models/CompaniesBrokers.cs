using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBrokers.Models
{
    public class CompaniesBrokers
    {
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }
        public int BrokerId { get; set; }
        public BrokerModel Broker { get; set; }
    }
}
