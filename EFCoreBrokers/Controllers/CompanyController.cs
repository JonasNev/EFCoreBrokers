using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos;
using EFCoreBrokers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Companies = _context.Companies.ToList();
            return View(Companies);
        }

        public IActionResult Create()
        {
            var company = new CompanyCreate();
            company.Brokers = _context.Brokers.ToList();
            return View(company);
        }

        [HttpPost]
        public IActionResult Create(CompanyCreate companyCreate)
        {
            CompaniesBrokers companiesBrokers = new CompaniesBrokers();
            _context.Companies.Add(companyCreate.Company);
            _context.SaveChanges();
            companyCreate.Company.Address = companyCreate.Company.Street + companyCreate.Company.Number + ", " + companyCreate.Company.City;
            companiesBrokers.CompanyId = GetLastCompanyId();
            _context.SaveChanges();
            foreach (var brokerid in companyCreate.BrokerIds)
            {
                companiesBrokers.BrokerId = brokerid;
                _context.CompaniesBrokers.Add(companiesBrokers);
                _context.SaveChanges();
            }


            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            CompanyCreate companyCreate = new();
            List<CompaniesBrokers> companiesBrokers = new();
            companyCreate.Brokers = _context.Brokers.ToList();

            companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == id).ToList();
            foreach (var broker in companiesBrokers)
            {
                if (broker.BrokerId == )
                {

                }
            }
            companyCreate.CompanyBrokers = _context.Brokers.Where(x => x.Id == 0).ToList();

            return View(companyCreate);
        }

        public int GetLastCompanyId()
        {
            int id = 0;
            List<CompanyModel> companies = _context.Companies.OrderByDescending(x => x.Id).ToList();
            id = companies[0].Id;
            return id;
        }
    }
}
