using EFCoreBrokers.Data;
using EFCoreBrokers.Dtos;
using EFCoreBrokers.Dtos.Broker;
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
    public class BrokerController : Controller
    {
        private readonly DataContext _context;
        private readonly BrokerService _brokerService;
        public BrokerController(DataContext context, BrokerService brokerService)
        {
            _brokerService = brokerService;
            _context = context;
        }

        public IActionResult Index()
        {
            List<BrokerModel> brokers = _brokerService.GetBrokers();
            return View(brokers);
        }

        public IActionResult Create()
        {
            var shop = new BrokerModel();
            return View(shop);
        }

        [HttpPost]
        public IActionResult Create(BrokerModel broker)
        {
            _brokerService.AddBroker(broker);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            BrokerDetails brokers = _brokerService.GetCertainBrokers(id);
            return View(brokers);
        }

        public IActionResult Remove(int id)
        {
            _brokerService.DeleteBroker(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            BrokerModel model = _context.Brokers.First(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BrokerModel model)
        {
            _brokerService.UpdateBroker(model);
            return RedirectToAction("Index");
        }
    }
}
