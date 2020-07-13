using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PromtexSite.Controllers
{
    public class SpecialOffersController : Controller
    {
        public IActionResult Free()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Reestablish()
        {
            return View();
        }

        public IActionResult Audit()
        {
            return View();
        }

        public IActionResult Taxation()
        {
            return View();
        }
        
    }
}