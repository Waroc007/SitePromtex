using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromtexSite.db;

namespace PromtexSite.Controllers
{
    public class TariffsAndPricesController : Controller
    {
        public async Task<IActionResult> ComprehensiveService([FromServices]ApplicationContext db)
        {
            var table = await db.Prices.Where(x => x.Type == Models.TableType.ComprehensiveService).ToListAsync();
            return View(table);
        }

        public IActionResult Reporting()
        {
            return View();
        }

        public IActionResult ProgramAndConsultation()
        {
            return View();
        }

        public IActionResult PayrollPreparation()
        {
            return View();
        }

        public IActionResult PayrollAndPersonnelRecords()
        {
            return View();
        }

        public async Task<IActionResult> ForOneTimeServices([FromServices]ApplicationContext db)
        {
            var table = await db.Prices.ToListAsync();
            return View(table);
        }
    }
}