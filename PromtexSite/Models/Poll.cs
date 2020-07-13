using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PromtexSite.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromtexSite.Models
{
    public class Poll
    {
        public DateTime TimeCreate { get; set; }
        public string Email { get; set; }
        public IdPlusText TypeOfBusinessEntity { get; set; }
        public IdPlusText TaxSystem { get; set; }
        public IdPlusText AvailabilityOfActivities { get; set; }
        public Dictionary<int, string> WhatIsNeeded { get; set; }
        public Dictionary<int, string> Activities { get; set; }
        public Dictionary<int, string> WhatIsNeeded2 { get; set; }
        public Dictionary<int, string> WhatIsNeeded3 { get; set; }
        public int? Employees { get; set; }
        public IdPlusText UseOfCashRegisters { get; set; }
        public IdPlusText Software { get; set; }
        public IdPlusText[] BusinessActivity { get; set; } = new IdPlusText[6] { new IdPlusText { ID = 0, Text = "Товарная накладная на отгрузку покупателю" },
            new IdPlusText { ID = 0, Text = "Акт приема-передачи работ/услуг заказчику" }, new IdPlusText { ID = 0, Text = "Отчет комиссионера/агент" },
        new IdPlusText { ID = 0, Text = "Товарная накладная по поступлению от поставщика" }, new IdPlusText { ID = 0, Text = "Акт приема-передачи работ/услуг от поставщик" },
        new IdPlusText { ID = 0, Text = "Кассовый + товарный чеки по покупкам за наличный расчет" }};
        public string Tel { get; set; }
        public string Comment { get; set; }
        public bool Sent { get; set; } = false;
        private readonly string EmailTo;
        private readonly IEmailWorker EmailWorker;

        public Poll(string emailTo, IEmailWorker emailYandexWorker)
        {
            EmailTo = emailTo;
            EmailWorker = emailYandexWorker;
        }
        public override string ToString()
        {
            End();
            StringBuilder str = new StringBuilder("Email: " + Email + "\n");
            if (TypeOfBusinessEntity != null)
                str.Append("Тип хозяйствующего субъекта: "+ TypeOfBusinessEntity.Text + "\n");
            if (TaxSystem != null)
                str.Append("Система налогообложения: " + TaxSystem.Text + "\n");
            if (AvailabilityOfActivities != null)
                str.Append("Наличие деятельности: " + AvailabilityOfActivities.Text + "\n");
            if (WhatIsNeeded != null)
            {
                str.Append("Что требуется: ");
                foreach (var item in WhatIsNeeded)
                    str.Append(item.Value + "\n");
                str.Append("\n");
            }
            if (Activities != null)
            {
                str.Append("Виды деятельности: ");
                foreach (var item in Activities)
                    str.Append(item.Value + "\n");
                str.Append("\n");
            }
            if (WhatIsNeeded2 != null)
            {
                str.Append("Что требуется: ");
                foreach (var item in WhatIsNeeded2)
                    str.Append(item.Value + "\n");
                str.Append("\n");
            }
            if (WhatIsNeeded3 != null)
            {
                str.Append("Что требуется: ");
                foreach (var item in WhatIsNeeded3)
                    str.Append(item.Value + "\n");
                str.Append("\n");
            }
            if (Employees != null)
                str.Append("Колличество сотрудников: " + Employees + "\n");
            if (UseOfCashRegisters != null)
                str.Append("Использование контрольно-кассовой техники: " + UseOfCashRegisters.Text + "\n");
            if (Software != null)
                str.Append("Использование программ для ведения учета: " + Software.Text + "\n");
            if (BusinessActivity != null)
            {
                bool flag = false;
                foreach (var item in BusinessActivity)
                    if (item.ID > 0)
                        flag = true;
                if (flag)
                {
                    str.Append("Количество первичной документации в среднем в месяц: ");
                    foreach (var item in BusinessActivity)
                        str.Append(item.Text + " " + item.ID + "\n");
                    str.Append("\n");
                }
            }
            if (Tel != null)
                str.Append("Номер телефона: " + Tel + "\n");
            if (Comment != null)
                str.Append("Комментарий: " + Comment + "\n");
            return str.ToString();
        }

        private void End()
        {
            if (TypeOfBusinessEntity != null)
            {
                if (TypeOfBusinessEntity.ID == 1)
                    TypeOfBusinessEntity.Text = "ИП";
                if (TypeOfBusinessEntity.ID == 2)
                    TypeOfBusinessEntity.Text = "ООО";
            }
            if (TaxSystem != null)
            {
                TaxSystem.Text = TaxSystem.ID == 1 ? "ОСНО" :
                     TaxSystem.ID == 2 ? "УСН с доходов" :
                     TaxSystem.ID == 3 ? "УСН доходы – расходы" :
                       TaxSystem.ID == 4 ? "ЕНВД" :
                        TaxSystem.ID == 5 ? "Патент" :
                         TaxSystem.ID == 6 ? "ОСНО + ЕНВД" :
                          TaxSystem.ID == 7 ? "ОСНО + Патент " :
                           TaxSystem.ID == 8 ? "УСН с доходов + ЕНВД" :
                            TaxSystem.ID == 9 ? "УСН с доходов + Патент " :
                             TaxSystem.ID == 10 ? "УСН доходы-расходы + ЕНВД" : "УСН доходы-расходы + Патент ";
            }
            if (AvailabilityOfActivities != null)
            {
                if (AvailabilityOfActivities.ID == 1)
                {
                    AvailabilityOfActivities.Text = "Деятельность отсутствует полностью (нет движения денег, товаров и т.п.)";
                }
                if (AvailabilityOfActivities.ID == 2)
                {
                    AvailabilityOfActivities.Text = "Деятельность ведется";
                }
            }
            if (UseOfCashRegisters != null)
            {
                UseOfCashRegisters.Text = UseOfCashRegisters.ID == 1 ? "Не используется" :
                    UseOfCashRegisters.ID == 2 ? "Используется ежедневно, включая выходные дни" :
                    UseOfCashRegisters.ID == 3 ? "Используется только в рабочие дни" :
                    "Используется время от времени, в среднем дней в месяце " + UseOfCashRegisters.Text;
            }
            if (Software != null)
            {
                Software.Text = Software.ID == 1 ? "Не используется ни одна" :
                    Software.ID == 2 ? "1С:Бухгалтерия" :
                    Software.ID == 3 ? "1С:Управление торговлей" :
                    Software.ID == 4 ? "1С:Управление нашей фирмой" :
                    Software.ID == 5 ? "1С:Розница" :
                    Software.ID == 6 ? "1С:Общепит" :
                    Software.Text;
            }
        }

        public async Task Send()
        {
            await EmailWorker.SendEmail(ToString(), EmailTo);
            Sent = true;
        }
    }

    public class IdPlusText
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
