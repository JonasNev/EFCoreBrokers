using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos;
using EFCoreBrokers.Dtos.Apartment;
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
    public class ApartmentController : Controller
    {
        private readonly DataContext _context;
        private readonly ApartmentService _apartmentService;

        public ApartmentController(DataContext context, ApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
            _context = context;
        }
        public IActionResult Index()
        {

            ApartmentIndexSort apartments = new()
            {
                Apartments = _apartmentService.GetApartments(),
                Brokers = _context.Brokers.ToList(),
                Companies = _context.Companies.ToList()
            };
            return View(apartments);
        }

        public IActionResult Create()
        {
            ApartmentCreate apartmentCreate = new ApartmentCreate();
            apartmentCreate.Companies = _context.Companies.ToList();
            return View(apartmentCreate);
        }

        [HttpPost]
        public IActionResult Create(ApartmentCreate apartmentCreate)
        {
            _apartmentService.AddApartment(apartmentCreate);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            ApartmentCreate apartment = _apartmentService.GetApartmentEdit(id);
            return View(apartment);
        }
        [HttpPost]
        public IActionResult Edit(ApartmentCreate apartmentCreate)
        {
            _apartmentService.UpdateApartment(apartmentCreate);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _apartmentService.DeleteApartment(id);
            return RedirectToAction("Index");
        }

        public IActionResult SortFilter(ApartmentIndexSort sortFilter)
        {
            ApartmentIndexSort sortModel = _apartmentService.FilterAndSortApartments(sortFilter);
            sortModel.Brokers = _context.Brokers.ToList();
            sortModel.Companies = _context.Companies.ToList();
            return View("Index", sortModel);
        }
    }
}
