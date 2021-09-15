using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos;
using EFCoreBrokers.Dtos.Broker;
using EFCoreBrokers.Dtos.SortFilter;
using EFCoreBrokers.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.Brokers.Add(broker);
            _context.SaveChanges();
        }

        public BrokerIndexSort SortBrokers(BrokerIndexSort sortFilter) 
        {
            BrokerIndexSort brokers = new();
            switch (sortFilter.SortBy)
            {
                case "Name":
                    brokers.Brokers = _context.Brokers.OrderBy(x => x.Name).ToList();
                    break;
                case "Surname":
                    brokers.Brokers = _context.Brokers.OrderBy(x => x.Surname).ToList();
                    break;
                default:
                    brokers.Brokers = _context.Brokers.OrderBy(x => x.Name).ToList();
                    break;
            }
            return brokers;
        }
        public List<BrokerModel> GetBrokers()
        {
            List<BrokerModel> brokers = _context.Brokers.ToList();

            return brokers;
        }

        public BrokerDetails GetCertainBrokers(int id)
        {
            BrokerDetails brokers = new();
            brokers.Broker = _context.Brokers.First(x => x.Id == id);
            brokers.Apartments = _context.Apartments.Where(x => x.Broker_id == id).ToList();
            return brokers;

        }
        public void UpdateBroker(BrokerModel model)
        {
            _context.Brokers.Update(model);
            _context.SaveChanges();
        }

        public void DeleteBroker(int id)
        {
            List<CompaniesBrokers> companiesBrokers = _context.CompaniesBrokers.Where(x => x.BrokerId == id).ToList();
            BrokerModel model = _context.Brokers.First(x => x.Id == id);
            List<CompanyModel> companies = _context.Companies.ToList();
            foreach (var broker in companiesBrokers)
            {
                _context.CompaniesBrokers.Remove(broker);
                _context.SaveChanges();
            }

            _context.Brokers.Remove(model);
            _context.SaveChanges();
        }
    }
}
