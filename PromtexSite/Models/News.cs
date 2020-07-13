using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Models
{
    public class News
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string TextMin { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
