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
            companyCreate.Company = _context.Companies.First(x => x.Id == id);

            companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == id).Include(x => x.Broker).ToList();
            foreach (var broker in companiesBrokers)
            {
                if (broker.CompanyId == id)
                {
                    companyCreate.CompanyBrokers.Add(broker.Broker);
                }
            }

            return View(companyCreate);
        }

        public int GetLastCompanyId()
        {
            int id = 0;
            List<CompanyModel> companies = _context.Companies.OrderByDescending(x => x.Id).ToList();
            id = companies[0].Id;
            return id;
        }

        public IActionResult Edit(int id)
        {
            CompanyCreate company = new();
            List<CompaniesBrokers> companiesBrokers = new();
            company.Company = _context.Companies.FirstOrDefault(x => x.Id == id);
            company.Brokers = _context.Brokers.ToList();
            companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == id).Include(x => x.Broker).ToList();
            foreach (var broker in companiesBrokers)
            {
                if (broker.CompanyId == id)
                {
                    company.CompanyBrokers.Add(broker.Broker);
                }
            }
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(CompanyCreate companyCreate)
        {
            CompaniesBrokers companiesBrokers = new();
            companyCreate.Company.Address = companyCreate.Company.Street + companyCreate.Company.Number + ", " + companyCreate.Company.City;
            _context.Companies.Update(companyCreate.Company);
            _context.SaveChanges();

            //reikia sutvarkyti junction seno istrynima ir naujo pridejima
            foreach (var broker in companyCreate.Brokers)
            {
                companiesBrokers.BrokerId = broker.Id;
                companiesBrokers.CompanyId = companyCreate.Company.Id;

                _context.CompaniesBrokers.Add(companiesBrokers);
                _context.SaveChanges();
            }
            // _context.CompaniesBrokers.RemoveRange()
            return RedirectToAction("Index");
        }

        public void AddJunction()
        {

        }
    }

}
