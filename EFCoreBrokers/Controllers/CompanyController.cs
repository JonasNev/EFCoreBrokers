using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos;
using EFCoreBrokers.Models;
using EFCoreBrokers.Services;
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
        private readonly CompanyService _companyService;

        public CompanyController(DataContext context, CompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
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
            _companyService.AddCompany(companyCreate);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            CompanyCreate companyCreate = _companyService.GetCompanyDetails(id);
            return View(companyCreate);
        }


        public IActionResult Edit(int id)
        {
            CompanyCreate company =  _companyService.GetCompanyForEdit(id);
            return View(company);
        }

        [HttpPost]
        public IActionResult Edit(CompanyCreate companyCreate)
        {
            _companyService.UpdateCompany(companyCreate);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int Id)
        {
            CompanyCreate company = new();
            List<CompaniesBrokers> companiesBrokers = new();
            company.Company = _context.Companies.FirstOrDefault(x => x.Id == Id);
            companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == Id).Include(x => x.Broker).ToList();
            List<ApartmentModel> apartments = _context.Apartments.ToList();
            foreach (var broker in companiesBrokers)
            {
                _context.CompaniesBrokers.Remove(broker);
                _context.SaveChanges();
            }
            foreach (var apartment in apartments)
            {
                if (apartment.Company_id == Id)
                {
                    _context.Apartments.Remove(apartment);

                }
            }
            _context.Companies.Remove(company.Company);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
