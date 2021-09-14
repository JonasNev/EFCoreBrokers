using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos;
using EFCoreBrokers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Services
{
    public class CompanyService
    {
        private readonly DataContext _context;

        public CompanyService(DataContext context)
        {
            _context = context;
        }

        public void AddCompany(CompanyCreate companyCreate)
        {
            CompaniesBrokers companiesBrokers = new CompaniesBrokers();
            _context.Companies.Add(companyCreate.Company);
            _context.SaveChanges();
            companyCreate.Company.Address = companyCreate.Company.Street + companyCreate.Company.Number + ", " + companyCreate.Company.City;
            companiesBrokers.CompanyId = GetLastId();
            _context.SaveChanges();
            foreach (var brokerid in companyCreate.BrokerIds)
            {
                companiesBrokers.BrokerId = brokerid;
                _context.CompaniesBrokers.Add(companiesBrokers);
                _context.SaveChanges();
            }

        }

        public void DeleteCompany(int id)
        {
            CompanyCreate company = new();
            List<CompaniesBrokers> companiesBrokers = new();
            company.Company = _context.Companies.FirstOrDefault(x => x.Id == id);
            companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == id).Include(x => x.Broker).ToList();
            List<ApartmentModel> apartments = _context.Apartments.ToList();
            foreach (var broker in companiesBrokers)
            {
                _context.CompaniesBrokers.Remove(broker);
                _context.SaveChanges();
            }
            foreach (var apartment in apartments)
            {
                if (apartment.Company_id == id)
                {
                    _context.Apartments.Remove(apartment);
                }
            }
            _context.Companies.Remove(company.Company);
            _context.SaveChanges();
        }

        public CompanyCreate GetCompanyDetails(int id)
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
            return companyCreate;
        }

        public CompanyCreate GetCompanyForEdit(int id)
        {
            CompanyCreate company = new();
            List<CompaniesBrokers> companiesBrokers = new();
            company.Company = _context.Companies.FirstOrDefault(x => x.Id == id);
            company.Brokers = _context.Brokers.ToList();
            companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == id).Include(x => x.Broker).ToList();
            foreach (var broker in companiesBrokers)
            {
                _context.CompaniesBrokers.Remove(broker);
                _context.SaveChanges();
            }
            foreach (var broker in companiesBrokers)
            {
                if (broker.CompanyId == id)
                {
                    company.CompanyBrokers.Add(broker.Broker);
                }
            }
            return company;
        }

        public void UpdateCompany(CompanyCreate companyCreate)
        {
            CompaniesBrokers companiesBrokers = new();
            companyCreate.Company.Address = companyCreate.Company.Street + companyCreate.Company.Number + ", " + companyCreate.Company.City;
            _context.Companies.Update(companyCreate.Company);
            _context.SaveChanges();
            companiesBrokers.CompanyId = companyCreate.Company.Id;
            foreach (var broker in companyCreate.BrokerIds)
            {
                companiesBrokers.BrokerId = broker;
                _context.CompaniesBrokers.Add(companiesBrokers);
                _context.SaveChanges();
            }
        }

        private int GetLastId()
        {
            int id = 0;
            List<CompanyModel> companies = _context.Companies.OrderByDescending(x => x.Id).ToList();
            id = companies[0].Id;
            return id;
        }
    }
}
