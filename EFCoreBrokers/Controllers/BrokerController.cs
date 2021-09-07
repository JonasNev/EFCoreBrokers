using EFCoreBrokers.Data;
using EFCoreBrokers.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}
