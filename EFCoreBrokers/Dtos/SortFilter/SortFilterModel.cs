using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Dtos.SortFilter
{
    public class SortFilterModel
    {
        public List<string> Sort { get; set; } = new();
        public string SortBy { get; set; }
    }
}
