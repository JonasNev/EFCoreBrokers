using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos.SortFilter;
using EFCoreBrokers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Services
{
    public class BrokerService
    {
        private readonly DataContext _context;

        public BrokerService(DataContext context)
        {
            _context = context;
        }

        public void AddBroker(BrokerModel broker)
        {

        }

        public List<BrokerModel> SortBrokers(SortFilterModel sortFilter)
        {
            List<BrokerModel> brokers = new();

            return brokers;

        }
        public List<BrokerModel> GetBrokers()
        {
            List<BrokerModel> brokers = new();


            return brokers;
        }

        public void UpdateBroker(BrokerModel model)
        {

        }

        public void DeleteBroker(int deleteid)
        {

        }
        public void DeleteJunction(int deleteId)
        {


        }
    }
}
