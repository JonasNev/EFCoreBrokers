﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreBrokers.Controllers
{
    public class ApartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}