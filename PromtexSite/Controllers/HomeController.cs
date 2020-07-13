using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PromtexSite.db;
using PromtexSite.Models;
using PromtexSite.Services;

namespace PromtexSite.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> Index([FromServices]ApplicationContext db)
        {
            ViewBag.Reviews = await db.Reviews.ToListAsync();
            ViewBag.Medals = await db.Medals.ToListAsync();
            return View();
        }

        public async Task<IActionResult> Questions([FromServices]ApplicationContext db)
        {
            var que = await db.Questions.ToListAsync();
            return View(que);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> SendRequest([FromServices]IEmailWorker emailWorker, string email, string tel, string comment, string check)
        {
            if(email !=null && tel != null && check == "on")
            {
                await emailWorker.SendEmail(tel, email, comment, _config.GetConnectionString("EmailTo"));
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
