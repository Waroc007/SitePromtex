using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Models
{
    public class TablePrice
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public TableType Type { get; set; }
    }

    public enum TableType
    {
        RecordKeeping, //Ведение учета
        PersonnelAccounting, //Кадровый учет
        PayrollPreparation,  //Расчет заработной платы
        ReportingAndSubmission, //Составление и сдача отчетности
        StorageServices, //Услуги хранения
        AccountingAdvice, //Консультации по вопросам ведения бухгалтерского и налогового учета
        SecretaryServices, //Курьерская служба/услуги секретаря
        LegalServices, //Юридические услуги
        ComprehensiveService //Тариф «Комплексный сервис»
    }
}
