using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos;
using EFCoreBrokers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Controllers
{
    public class BrokerController : Controller
    {
        private readonly DataContext _context;
        public BrokerController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Brokers = _context.Brokers.ToList();
            return View(Brokers);
        }

        public IActionResult Create()
        {
            var shop = new BrokerModel();
            return View(shop);
        }

        [HttpPost]
        public IActionResult Create(BrokerModel broker)
        {

            _context.Brokers.Add(broker);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int companyId)
        {
            List<BrokerModel> brokers = new();
            CompanyCreate companyCreate = new();
            List<CompaniesBrokers> companiesBrokers = new();
            companyCreate.Company = _context.Companies.First(x => x.Id == companyId);

            companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == companyId).Include(x => x.Broker).ToList();
            foreach (var broker in companiesBrokers)
            {
                if (broker.CompanyId == companyId)
                {
                    companyCreate.CompanyBrokers.Add(broker.Broker);
                }
            }
            return View(brokers);
        }
    }
}
