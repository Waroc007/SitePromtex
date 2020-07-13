using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromtexSite.Models
{
    public class Medal
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool Visible { get; set; }
    }
}
