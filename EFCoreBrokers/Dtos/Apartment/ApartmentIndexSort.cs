using EFCoreBrokers.Dtos.SortFilter;
using EFCoreBrokers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Dtos.Apartment
{
    public class ApartmentIndexSort
    {
        public List<ApartmentModel> Apartments { get; set; }
        public SortFilterModel SortFilter { get; set; }
        public ApartmentModel ApartmentFilter { get; set; }
        public int CompanyFilterId { get; set; }
        public List<CompanyModel> Companies { get; set; }
        public int BrokerFilterId { get; set; }
        public List<BrokerModel> Brokers { get; set; }


    }
}
