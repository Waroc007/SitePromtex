using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromtexSite.db;
using PromtexSite.Models;
using PromtexSite.Services;

namespace PromtexSite.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        IHostingEnvironment _appEnvironment;

        public AdministrationController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Reviews([FromServices]ApplicationContext db)
        {
            var reviews = await db.Reviews.ToListAsync();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult AddReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromServices]ApplicationContext db, Review review, IFormFile PhotoFolder, IFormFile ReviewFolder)
        {
            if (PhotoFolder != null && ReviewFolder != null)
            {
                var folderP = await Img.AddFile(PhotoFolder, _appEnvironment.WebRootPath, "img/Review");
                var folderR = await Img.AddFile(ReviewFolder, _appEnvironment.WebRootPath, "img/Review");
                if (folderP != null && folderR != null)
                {
                    review.PhotoFolder = folderP;
                    review.ReviewFolder = folderR;
                    db.Add(review);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Reviews", "Administration");
                }
            }
            return View(review);

        }

        [HttpGet]
        public async Task<IActionResult> UpdateReview([FromServices]ApplicationContext db, int ID)
        {
            var review = await db.Reviews.FirstOrDefaultAsync(_ => _.ID == ID);
            if (review != null)
                return View(review);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateReview([FromServices]ApplicationContext db, Review review, IFormFile PhotoFolder, IFormFile ReviewFolder)
        {
            var reviewdb = await db.Reviews.FirstOrDefaultAsync(_ => _.ID == review.ID);

            if (PhotoFolder == null)
                review.PhotoFolder = reviewdb.PhotoFolder;
            else
            {
                var folder = await Img.AddFile(PhotoFolder, _appEnvironment.WebRootPath, "img/Review");
                review.PhotoFolder = folder;
            }
            if (ReviewFolder == null)
                review.ReviewFolder = reviewdb.ReviewFolder;
            else
            {
                var folder = await Img.AddFile(ReviewFolder, _appEnvironment.WebRootPath, "img/Review");
                review.ReviewFolder = folder;
            }

            db.Entry(reviewdb).State = EntityState.Detached;
            db.Reviews.Update(review);
            await db.SaveChangesAsync();
            return RedirectToAction("Reviews", "Administration");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview([FromServices]ApplicationContext db, int? ID)
        {
            if (ID != null)
            {
                var review = await db.Reviews.FirstOrDefaultAsync(_ => _.ID == ID);
                if (review != null)
                {
                    db.Reviews.Remove(review);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Reviews", "Administration");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> News([FromServices]ApplicationContext db)
        {
            var news = await db.News.ToListAsync();
            return View(news);
        }

        [HttpGet]
        public IActionResult AddNews()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNews([FromServices]ApplicationContext db, News news)
        {
            db.Add(news);
            await db.SaveChangesAsync();

            return RedirectToAction("News", "Administration");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateNews([FromServices]ApplicationContext db, int ID)
        {
            var news = await db.News.FirstOrDefaultAsync(_ => _.ID == ID);
            if (news != null)
                return View(news);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateNews([FromServices]ApplicationContext db, News news)
        {
            db.News.Update(news);
            await db.SaveChangesAsync();
            return RedirectToAction("News", "Administration");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteNews([FromServices]ApplicationContext db, int? ID)
        {
            if (ID != null)
            {
                var news = await db.News.FirstOrDefaultAsync(_ => _.ID == ID);
                if (news != null)
                {
                    db.News.Remove(news);
                    await db.SaveChangesAsync();
                    return RedirectToAction("News", "Administration");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Prices([FromServices]ApplicationContext db)
        {
            var prices = await db.Prices.ToListAsync();
            return View(prices);
        }

        [HttpGet]
        public IActionResult AddPrice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPrice([FromServices]ApplicationContext db, TablePrice tablePrice)
        {
            db.Add(tablePrice);
            await db.SaveChangesAsync();

            return RedirectToAction("Prices", "Administration");
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePrice([FromServices]ApplicationContext db, int ID)
        {
            var price = await db.Prices.FirstOrDefaultAsync(_ => _.ID == ID);
            if (price != null)
                return View(price);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePrice([FromServices]ApplicationContext db, TablePrice tablePrice)
        {
            db.Prices.Update(tablePrice);
            await db.SaveChangesAsync();
            return RedirectToAction("Prices", "Administration");
        }


        [HttpPost]
        public async Task<IActionResult> DeletePrice([FromServices]ApplicationContext db, int? ID)
        {
            if (ID != null)
            {
                var price = await db.Prices.FirstOrDefaultAsync(_ => _.ID == ID);
                if (price != null)
                {
                    db.Prices.Remove(price);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Prices", "Administration");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Medals([FromServices]ApplicationContext db)
        {
            var medals = await db.Medals.ToListAsync();
            return View(medals);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMedal([FromServices]ApplicationContext db, int? ID)
        {
            var news = await db.Medals.FirstOrDefaultAsync(_ => _.ID == ID);
            if (news != null)
            {
                news.Visible = !news.Visible;
                db.Medals.Update(news);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Medals", "Administration");
        }

        [HttpGet]
        public IActionResult Files()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Files(IFormFile tariff, IFormFile pologenie, IFormFile soglasie, IFormFile file1, IFormFile file2, IFormFile file3,
            IFormFile file4, IFormFile file5, IFormFile file6, IFormFile file7, IFormFile file8, IFormFile we1, IFormFile we2,
            IFormFile we3, IFormFile we4, IFormFile we5, IFormFile we6, IFormFile we7, IFormFile we8)
        {
            if (tariff != null && tariff.ContentType == "application/pdf")
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/docs/tariff.pdf", FileMode.Create))
                {
                    await tariff.CopyToAsync(fileStream);
                }
            }
            if (pologenie != null && pologenie.ContentType == "application/pdf")
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/docs/pologenie.pdf", FileMode.Create))
                {
                    await pologenie.CopyToAsync(fileStream);
                }
            }
            if (soglasie != null && soglasie.ContentType == "application/pdf")
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/docs/soglasie.pdf", FileMode.Create))
                {
                    await soglasie.CopyToAsync(fileStream);
                }
            }

            if (file1 != null && (file1.ContentType == "image/jpg" || file1.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/gramota.jpeg", FileMode.Create))
                {
                    await file1.CopyToAsync(fileStream);
                }
            }

            if (file2 != null && (file2.ContentType == "image/jpg" || file2.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/blagod1.jpeg", FileMode.Create))
                {
                    await file2.CopyToAsync(fileStream);
                }
            }

            if (file3 != null && (file3.ContentType == "image/jpg" || file3.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/blagod2.jpeg", FileMode.Create))
                {
                    await file3.CopyToAsync(fileStream);
                }
            }

            if (file4 != null && (file4.ContentType == "image/jpg" || file4.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/sertif1.jpeg", FileMode.Create))
                {
                    await file4.CopyToAsync(fileStream);
                }
            }
            if (file5 != null && (file5.ContentType == "image/jpg" || file5.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/sertif2.jpeg", FileMode.Create))
                {
                    await file5.CopyToAsync(fileStream);
                }
            }
            if (file6 != null && (file6.ContentType == "image/jpg" || file6.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/sertif3.jpeg", FileMode.Create))
                {
                    await file6.CopyToAsync(fileStream);
                }
            }
            if (file7 != null && (file7.ContentType == "image/jpg" || file7.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/sertif4.jpeg", FileMode.Create))
                {
                    await file7.CopyToAsync(fileStream);
                }
            }
            if (file8 != null && (file8.ContentType == "image/jpg" || file8.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/ratings/sertif5.jpeg", FileMode.Create))
                {
                    await file8.CopyToAsync(fileStream);
                }
            }
            if (we1 != null && (we1.ContentType == "image/jpg" || we1.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we1.jpeg", FileMode.Create))
                {
                    await we1.CopyToAsync(fileStream);
                }
            }
            if (we2 != null && (we2.ContentType == "image/jpg" || we2.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we2.jpeg", FileMode.Create))
                {
                    await we2.CopyToAsync(fileStream);
                }
            }
            if (we3 != null && (we3.ContentType == "image/jpg" || we3.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we3.jpeg", FileMode.Create))
                {
                    await we3.CopyToAsync(fileStream);
                }
            }
            if (we4 != null && (we4.ContentType == "image/jpg" || we4.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we4.jpeg", FileMode.Create))
                {
                    await we4.CopyToAsync(fileStream);
                }
            }
            if (we5 != null && (we5.ContentType == "image/jpg" || we5.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we5.jpeg", FileMode.Create))
                {
                    await we5.CopyToAsync(fileStream);
                }
            }
            if (we6 != null && (we6.ContentType == "image/jpg" || we6.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we6.jpeg", FileMode.Create))
                {
                    await we6.CopyToAsync(fileStream);
                }
            }
            if (we7 != null && (we7.ContentType == "image/jpg" || we7.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we7.jpeg", FileMode.Create))
                {
                    await we7.CopyToAsync(fileStream);
                }
            }
            if (we8 != null && (we8.ContentType == "image/jpg" || we8.ContentType == "image/jpeg"))
            {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + "/img/We/we8.jpeg", FileMode.Create))
                {
                    await we8.CopyToAsync(fileStream);
                }
            }
            return View();
        }

        public async Task<IActionResult> Questions([FromServices]ApplicationContext db)
        {
            var questions = await db.Questions.ToListAsync();
            return View(questions);
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromServices]ApplicationContext db, Question question)
        {
            db.Add(question);
            await db.SaveChangesAsync();

            return RedirectToAction("Questions", "Administration");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateQuestion([FromServices]ApplicationContext db, int ID)
        {
            var question = await db.Questions.FirstOrDefaultAsync(_ => _.ID == ID);
            if (question != null)
                return View(question);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuestion([FromServices]ApplicationContext db, Question question)
        {
            db.Questions.Update(question);
            await db.SaveChangesAsync();
            return RedirectToAction("Questions", "Administration");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteQuestion([FromServices]ApplicationContext db, int? ID)
        {
            if (ID != null)
            {
                var question = await db.Questions.FirstOrDefaultAsync(_ => _.ID == ID);
                if (question != null)
                {
                    db.Questions.Remove(question);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Questions", "Administration");
                }
            }
            return NotFound();
        }
    }
}