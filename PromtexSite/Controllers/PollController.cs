using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PromtexSite.Models;
using PromtexSite.Services;

namespace PromtexSite.Controllers
{
    public class PollController : Controller
    {
        private readonly IConfiguration _config;

        public PollController(IConfiguration config)
        {
            _config = config;
        }
        static Dictionary<Guid, Poll> DPoll = new Dictionary<Guid, Poll>();
        private static TimerCallback TimerPoll;
        private static Timer timerPoll;

        [HttpGet]
        public IActionResult Email(Guid? key)
        {
            if (TimerPoll == null)
            {
                TimerPoll = new TimerCallback(PollCleaner.CleanOldPoll);
                timerPoll = new Timer(TimerPoll, DPoll, 0, 600000);
            }

            if (key != null)
            {
                return View(DPoll[key.Value]);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Email([FromServices]IEmailWorker emailWorker, Guid? key, string email)
        {

            if (key != null)
            {
                if (email == null)
                {
                    ViewBag.Error = "Введите Email";
                    return View(DPoll[key.Value]);
                }
                DPoll[key.Value].Email = email;
                return RedirectToAction("TypeOfBusinessEntity", new { key = key.Value });
            }
            else
            {
                if (email == null)
                {
                    ViewBag.Error = "Введите Email";
                    return View();
                }
                do
                {
                    key = Guid.NewGuid();
                } while (DPoll.ContainsKey(key.Value));
                DPoll.Add(key.Value, new Poll(_config.GetConnectionString("EmailTo"), emailWorker) { Email = email, TimeCreate = DateTime.Now });
                return RedirectToAction("TypeOfBusinessEntity", new { key = key.Value });
            }

        }

        [HttpGet]
        public IActionResult TypeOfBusinessEntity(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult TypeOfBusinessEntity(Guid? key, int? id, string text)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id == null || id < 0 || id > 3)
                {
                    return View(DPoll[key.Value]);
                }
                if (id == 3 && text == null)
                {
                    ViewBag.Error = "Поясните тип хозяйствующего субъекта";
                    DPoll[key.Value].TypeOfBusinessEntity = new IdPlusText { ID = id.Value };
                    return View(DPoll[key.Value]);
                }
                DPoll[key.Value].TypeOfBusinessEntity = new IdPlusText { ID = id.Value, Text = text };
                return RedirectToAction("TaxSystem", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult TaxSystem(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult TaxSystem(Guid? key, int? id)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id == null || id < 0 || id > 11)
                {
                    return View(DPoll[key.Value]);
                }
                DPoll[key.Value].TaxSystem = new IdPlusText { ID = id.Value };
                return RedirectToAction("AvailabilityOfActivities", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult AvailabilityOfActivities(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AvailabilityOfActivities(Guid? key, int? id)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id == null || id < 0 || id > 2)
                {
                    return View(DPoll[key.Value]);
                }
                DPoll[key.Value].AvailabilityOfActivities = new IdPlusText { ID = id.Value };
                if (id == 1)
                    return RedirectToAction("WhatIsNeeded", new { key = key.Value });
                else
                    return RedirectToAction("Activities", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }


        [HttpGet]
        public IActionResult WhatIsNeeded(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult WhatIsNeeded(Guid? key, int? id1, int? id2, int? id3, string text)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id1 == null && id2 == null && id3 == null)
                {
                    ViewBag.Error = "Выберите хотя бы один подходящий вариант";
                    return View(DPoll[key.Value]);
                }
                DPoll[key.Value].WhatIsNeeded = new Dictionary<int, string>();
                var Error = false;
                if (id1 != null)
                {
                    if (id1 == 1)
                        DPoll[key.Value].WhatIsNeeded.Add(id1.Value, "Сдача «нулевой» отчетности");
                    else
                        Error = true;
                }

                if (id2 != null)
                {
                    if (id2 == 2)
                        DPoll[key.Value].WhatIsNeeded.Add(id2.Value, "Консультация");
                    else
                        Error = true;
                }

                if (id3 != null)
                {
                    if (id3 == 3 && text != null)
                        DPoll[key.Value].WhatIsNeeded.Add(id3.Value, text);
                    else if (id3 == 3 && text == null)
                    {
                        DPoll[key.Value].WhatIsNeeded.Add(id3.Value, text);
                        ViewBag.Error = "Заполните поле \"Другое\"";
                        return View(DPoll[key.Value]);
                    }
                    else
                        Error = true;
                }
                if (Error)
                    return View(DPoll[key.Value]);
                return RedirectToAction("Ended", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }


        [HttpGet]
        public IActionResult Activities(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Activities(Guid? key, int? id1, int? id2, int? id3, int? id4, int? id5, int? id6, int? id7, int? id8, string text)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id1 == null && id2 == null && id3 == null && id4 == null && id5 == null && id6 == null && id7 == null && id8 == null)
                {
                    ViewBag.Error = "Выберите хотя бы один подходящий вариант";
                    return View(DPoll[key.Value]);
                }
                DPoll[key.Value].Activities = new Dictionary<int, string>();
                var Error = false;
                if (id1 != null)
                {
                    if (id1 == 1)
                        DPoll[key.Value].Activities.Add(id1.Value, "Бытовые услуги физическим лицам");
                    else
                        Error = true;
                }
                if (id2 != null)
                {
                    if (id2 == 2)
                        DPoll[key.Value].Activities.Add(id2.Value, "Услуги организациям");
                    else
                        Error = true;
                }
                if (id3 != null)
                {
                    if (id3 == 3)
                        DPoll[key.Value].Activities.Add(id3.Value, "Розничная торговля");
                    else
                        Error = true;
                }
                if (id4 != null)
                {
                    if (id4 == 4)
                        DPoll[key.Value].Activities.Add(id4.Value, "Оптовая торговля");
                    else
                        Error = true;
                }
                if (id5 != null)
                {
                    if (id5 == 5)
                        DPoll[key.Value].Activities.Add(id5.Value, "Общественное питание");
                    else
                        Error = true;
                }
                if (id6 != null)
                {
                    if (id6 == 6)
                        DPoll[key.Value].Activities.Add(id6.Value, "Строительство");
                    else
                        Error = true;
                }
                if (id7 != null)
                {
                    if (id7 == 7)
                        DPoll[key.Value].Activities.Add(id7.Value, "Производство");
                    else
                        Error = true;
                }
                if (id8 != null)
                {
                    if (id8 == 8 && text != null)
                        DPoll[key.Value].Activities.Add(id8.Value, text);
                    else if (text == null)
                    {
                        ViewBag.Error = "Заполните поле \"Другое\"";
                        DPoll[key.Value].Activities.Add(id8.Value, text);
                        return View(DPoll[key.Value]);
                    }
                    else
                        Error = true;
                }
                if (Error)
                {
                    return View(DPoll[key.Value]);
                }
                if (DPoll[key.Value].TypeOfBusinessEntity.ID == 1 && (DPoll[key.Value].TaxSystem.ID == 2
                    || DPoll[key.Value].TaxSystem.ID == 4 || DPoll[key.Value].TaxSystem.ID == 5))
                {
                    return RedirectToAction("WhatIsNeeded2", new { key = key.Value });
                }
                return RedirectToAction("WhatIsNeeded3", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult WhatIsNeeded2(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult WhatIsNeeded2(Guid? key, int? id1, int? id2, int? id3, int? id4, int? id5, int? id6, int? id7, int? id8, string text)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id1 == null && id2 == null && id3 == null && id4 == null && id5 == null && id6 == null && id7 == null && id8 == null)
                {
                    ViewBag.Error = "Выберите хотя бы один подходящий вариант";
                    return View(DPoll[key.Value]);
                }
                var Error = false;
                DPoll[key.Value].WhatIsNeeded2 = new Dictionary<int, string>();
                if (id1 != null)
                {
                    if (id1 == 1)
                        DPoll[key.Value].WhatIsNeeded2.Add(id1.Value, "Складской учет товаров, материалов и т.п.");
                    else
                        Error = true;
                }
                if (id2 != null)
                {
                    if (id2 == 2)
                        DPoll[key.Value].WhatIsNeeded2.Add(id2.Value, "Учет взаиморасчетов с контрагентами");
                    else
                        Error = true;
                }
                if (id3 != null)
                {
                    if (id3 == 3)
                        DPoll[key.Value].WhatIsNeeded2.Add(id3.Value, "Расчет налогов по деятельности, составление и сдача отчетности");
                    else
                        Error = true;
                }
                if (id4 != null)
                {
                    if (id4 == 4)
                        DPoll[key.Value].WhatIsNeeded2.Add(id4.Value, "Кадровый учет");
                    else
                        Error = true;
                }
                if (id5 != null)
                {
                    if (id5 == 5)
                        DPoll[key.Value].WhatIsNeeded2.Add(id5.Value, "Расчет заработной платы");
                    else
                        Error = true;
                }
                if (id6 != null)
                {
                    if (id6 == 6)
                        DPoll[key.Value].WhatIsNeeded2.Add(id6.Value, "Составление и сдача отчетности по налогам и сборам с заработной платы");
                    else
                        Error = true;
                }
                if (id7 != null)
                {
                    if (id7 == 7)
                        DPoll[key.Value].WhatIsNeeded2.Add(id7.Value, "Консультации");
                    else
                        Error = true;
                }
                if (id8 != null)
                {
                    if (text != null && id8 == 8)
                        DPoll[key.Value].WhatIsNeeded2.Add(id8.Value, text);
                    else if (text == null)
                    {
                        ViewBag.Error = "Заполните поле \"Другое\"";
                        DPoll[key.Value].WhatIsNeeded2.Add(id8.Value, text);
                        return View(DPoll[key.Value]);
                    }
                    else
                        Error = true;
                }
                if (Error)
                {
                    return View(DPoll[key.Value]);
                }
                if (id4 != null || id5 != null)
                {
                    return RedirectToAction("Employees", new { key = key.Value });
                }
                return RedirectToAction("UseOfCashRegisters", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult WhatIsNeeded3(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult WhatIsNeeded3(Guid? key, int? id1, int? id2, int? id3, int? id4, int? id5, int? id6, int? id7, string text)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id1 == null && id2 == null && id3 == null && id4 == null && id5 == null && id6 == null && id7 == null)
                {
                    ViewBag.Error = "Выберите хотя бы один подходящий вариант";
                    return View(DPoll[key.Value]);
                }
                var Error = false;
                DPoll[key.Value].WhatIsNeeded3 = new Dictionary<int, string>();
                if (id1 != null)
                {
                    if (id1 == 1)
                        DPoll[key.Value].WhatIsNeeded3.Add(id1.Value, "Ведение учета");
                    else
                        Error = true;

                }
                if (id2 != null)
                {
                    if (id2 == 2)
                        DPoll[key.Value].WhatIsNeeded3.Add(id2.Value, "Расчет налогов по деятельности, составление и сдача отчетности");
                    else
                        Error = true;
                }
                if (id3 != null)
                {
                    if (id3 == 3)
                        DPoll[key.Value].WhatIsNeeded3.Add(id3.Value, "Кадровый учет");
                    else
                        Error = true;
                }
                if (id4 != null)
                {
                    if (id4 == 4)
                        DPoll[key.Value].WhatIsNeeded3.Add(id4.Value, "Расчет заработной платы");
                    else
                        Error = true;
                }
                if (id5 != null)
                {
                    if (id5 == 5)
                        DPoll[key.Value].WhatIsNeeded3.Add(id5.Value, "Составление и сдача отчетности по налогам и сборам с заработной платы");
                    else
                        Error = true;
                }
                if (id6 != null)
                {
                    if (id6 == 6)
                        DPoll[key.Value].WhatIsNeeded3.Add(id6.Value, "Консультации");
                    else
                        Error = true;
                }
                if (id7 != null)
                {
                    if (text != null && id7 == 7)
                        DPoll[key.Value].WhatIsNeeded3.Add(id7.Value, text);
                    else if (text == null)
                    {
                        ViewBag.Error = "Заполните поле \"Другое\"";
                        DPoll[key.Value].WhatIsNeeded3.Add(id7.Value, text);
                        return View(DPoll[key.Value]);
                    }
                    else
                        Error = true;
                }
                if (Error)
                {
                    return View(DPoll[key.Value]);
                }
                if (id3 != null || id4 != null)
                {
                    return RedirectToAction("Employees", new { key = key.Value });
                }
                return RedirectToAction("UseOfCashRegisters", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult Employees(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Employees(Guid? key, int? employees)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (employees != null)
                {
                    if(employees == null || employees <= 0)
                    {
                        ViewBag.Error = "Введите корректное значение";
                        return View(DPoll[key.Value]);
                    }
                    DPoll[key.Value].Employees = employees.Value;
                    if (DPoll[key.Value].WhatIsNeeded2 != null)
                    {
                        if (DPoll[key.Value].WhatIsNeeded2.Where(x => x.Key != 4 && x.Key != 5).Count() > 0)
                        {
                            return RedirectToAction("UseOfCashRegisters", new { key = key.Value });
                        }
                        else
                        {
                            return RedirectToAction("Ended", new { key = key.Value });
                        }
                    }
                    if (DPoll[key.Value].WhatIsNeeded3 != null)
                    {
                        if (DPoll[key.Value].WhatIsNeeded3.Where(x => x.Key != 3 && x.Key != 4).Count() > 0)
                        {
                            return RedirectToAction("UseOfCashRegisters", new { key = key.Value });
                        }
                        else
                        {
                            return RedirectToAction("Ended", new { key = key.Value });

                        }
                    }
                }
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult UseOfCashRegisters(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult UseOfCashRegisters(Guid? key, int? id, string text)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id == null || id < 0 || id > 4)
                {
                    return View(DPoll[key.Value]);
                }
                if (id == 4 && text == null)
                {
                    ViewBag.Error = "Укажите сколько в среднем дней в месяце используется";
                    DPoll[key.Value].UseOfCashRegisters = new IdPlusText { ID = id.Value };
                    return View(DPoll[key.Value]);
                }
                DPoll[key.Value].UseOfCashRegisters = new IdPlusText { ID = id.Value, Text = text };
                return RedirectToAction("Software", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult Software(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Software(Guid? key, int? id, string text)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (id == null || id < 0 || id > 7)
                {
                    return View(DPoll[key.Value]);
                }
                if (id == 7)
                {
                    if (text == null)
                    {
                        ViewBag.Error = "Заполните используемые программы";
                        DPoll[key.Value].Software = new IdPlusText { ID = id.Value};
                        return View(DPoll[key.Value]);
                    }
                }
                DPoll[key.Value].Software = new IdPlusText { ID = id.Value, Text = text };
                return RedirectToAction("BusinessActivity", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult BusinessActivity(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult BusinessActivity(Guid? key, int num1, int num2, int num3, int num4, int num5, int num6)
        {
            ViewBag.Key = key;
            if (key != null)
            {
                if (num1 < 0 || num2 < 0 || num3 < 0 || num4 < 0 || num5 < 0 || num6 < 0)
                {
                    return View(DPoll[key.Value]);
                }
                if (num1 > 0)
                    DPoll[key.Value].BusinessActivity[0].ID = num1;
                if (num2 > 0)
                    DPoll[key.Value].BusinessActivity[1].ID = num2;
                if (num3 > 0)
                    DPoll[key.Value].BusinessActivity[2].ID = num3;
                if (num4 > 0)
                    DPoll[key.Value].BusinessActivity[3].ID = num4;
                if (num5 > 0)
                    DPoll[key.Value].BusinessActivity[4].ID = num5;
                if (num6 > 0)
                    DPoll[key.Value].BusinessActivity[5].ID = num6;
                return RedirectToAction("Ended", new { key = key.Value });
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult Ended(Guid? key)
        {
            if (key != null)
            {
                ViewBag.Key = key;
                return View(DPoll[key.Value]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Ended([FromServices]IEmailWorker emailWorker, Guid? key, string tel, string com)
        {
            if (key != null)
            {
                DPoll[key.Value].Tel = tel;
                DPoll[key.Value].Comment = com;
                await DPoll[key.Value].Send();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();
            }

        }
    }
}