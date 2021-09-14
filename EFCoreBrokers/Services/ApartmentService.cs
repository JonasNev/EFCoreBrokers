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
    public class ApartmentService
    {
        private readonly DataContext _context;

        public ApartmentService(DataContext context)
        {
            _context = context;
        }

        public void AddApartment(ApartmentCreate apartmentCreate)
        {
            apartmentCreate.Apartment.Address = apartmentCreate.Apartment.Street + ", " + apartmentCreate.Apartment.HouseNR + "-" + apartmentCreate.Apartment.FlatNr + ", " + apartmentCreate.Apartment.City;
            _context.Apartments.Add(apartmentCreate.Apartment);
            _context.SaveChanges();
        }

        public ApartmentCreate GetApartmentEdit(int id)
        {
            ApartmentCreate apartment = new ApartmentCreate();
            apartment.Apartment = _context.Apartments.First(x => x.Id == id);
            apartment.Company = _context.Companies.First(x => x.Id == apartment.Apartment.Company_id);
            apartment.companiesBrokers = _context.CompaniesBrokers.Where(x => x.CompanyId == apartment.Company.Id).Include(x => x.Broker).ToList();
            foreach (var broker in apartment.companiesBrokers)
            {
                if (broker.CompanyId == apartment.Company.Id)
                {
                    apartment.Brokers.Add(broker.Broker);
                    apartment.Apartment.Company_id = broker.CompanyId;
                }
            }
            return apartment;
        }


        public List<ApartmentModel> GetApartments()
        {
            var apartments = _context.Apartments.Include(x => x.Broker).ToList();
            var companies = _context.Companies.ToList();
            foreach (var apartment in apartments)
            {
                apartment.Company = _context.Companies.First(x => x.Id == apartment.Company_id);
                if (apartment.Broker_id != null)
                {
                    apartment.Broker = _context.Brokers.First(x => x.Id == apartment.Broker_id);
                }
            }
            return apartments;
        }

        public void UpdateApartment(ApartmentCreate apartmentCreate)
        {
            apartmentCreate.Apartment.Address = apartmentCreate.Apartment.Street + ", " + apartmentCreate.Apartment.HouseNR + "-" + apartmentCreate.Apartment.FlatNr + ", " + apartmentCreate.Apartment.City;
            _context.Apartments.Update(apartmentCreate.Apartment);
            _context.SaveChanges();
        }

        public void DeleteApartment(int id)
        {
            ApartmentModel apartmentModel = _context.Apartments.First(x => x.Id == id);
            _context.Apartments.Remove(apartmentModel);
            _context.SaveChanges();
        }

    }
}
