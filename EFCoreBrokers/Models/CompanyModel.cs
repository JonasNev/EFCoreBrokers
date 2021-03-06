using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreBrokers.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public ICollection<CompaniesBrokers> CompaniesBrokers { get; set; }
    }
}
