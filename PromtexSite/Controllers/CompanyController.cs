using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromtexSite.db;
using PromtexSite.Models;

namespace PromtexSite.Controllers
{
    public class CompanyController : Controller
    {
        
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult WhoAreWe()
        {
            return View();
        }

        public async Task<IActionResult> Reviews([FromServices]ApplicationContext db, int select = -1, int page = 1)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>()
            {
                new SelectListItem("Все отзывы",  "-1"),
                new SelectListItem("Сфера услуг",  $"{(int)TypeReview.ServicesSector}"),
                new SelectListItem("Оптовая торговля",  $"{(int)TypeReview.Wholesale}"),
                new SelectListItem("Розничная торговля",  $"{(int)TypeReview.Retail}"),
                new SelectListItem("Общественное питание",  $"{(int)TypeReview.Catering}"),
                new SelectListItem("Производство",  $"{(int)TypeReview.Production}"),
                new SelectListItem("Строительство",  $"{(int)TypeReview.Construction}")
            };
            ViewBag.Select = selectListItems;

            int pageSize = 10;
            int reviewsCount;
            List<Review> reviews;
            if (select == -1)
            {
                reviewsCount = await db.Reviews.CountAsync();
                reviews = await db.Reviews.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                reviewsCount = await db.Reviews.Where(_ => _.Type == (TypeReview)select).CountAsync();
                selectListItems.SingleOrDefault(_ => _.Value == $"{select}").Selected = true;
                reviews = await db.Reviews.Where(_ => _.Type == (TypeReview)select).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            PageViewModel<Review> result = new PageViewModel<Review>(reviewsCount, page, pageSize, reviews);
            
            return View(result);
        }

        public async Task<IActionResult> Review([FromServices]ApplicationContext db, int? ID)
        {
            if (ID != null)
            {
                var review = await db.Reviews.FirstOrDefaultAsync(_ => _.ID == ID);
                return View(review);
            }
            return NotFound();
        }

        public async Task<IActionResult> News([FromServices]ApplicationContext db, int page = 1)
        {
            int pageSize = 15;
            var newsCount = await db.News.CountAsync();
            var news = await db.News.OrderByDescending(_ => _.Date).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel<News> result = new PageViewModel<News>(newsCount, page, pageSize, news);
            return View(result);
        }

        public async Task<IActionResult> OneNews([FromServices]ApplicationContext db, int? ID)
        {
            if (ID != null)
            {
                var news = await db.News.FirstOrDefaultAsync(_ => _.ID == ID);
                return View(news);
            }
            return NotFound();
        }

        public IActionResult Lead()
        {
            return View();
        }
        public IActionResult Lead1()
        {
            return View();
        }
        public IActionResult Lead2()
        {
            return View();
        }
        public IActionResult Team()
        {
            return View();
        }
    }
}