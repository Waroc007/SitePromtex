using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Models
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Counter { get; set; }
        [Required]
        public string PhotoFolder { get; set; }
        [Required]
        public string ReviewFolder { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string TextMin { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyPosition { get; set; }
        [Required]
        public TypeReview Type { get; set; }
    }
    public enum TypeReview
    {
        ServicesSector, // Сфера услуг
        Wholesale, // Оптовая торговля 
        Retail, // Розничная торговля
        Catering, // Общественное питание 
        Production, // Производство
        Construction //Строительство
    }
}
