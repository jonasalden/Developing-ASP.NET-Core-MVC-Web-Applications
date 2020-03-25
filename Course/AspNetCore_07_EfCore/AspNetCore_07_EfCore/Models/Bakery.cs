using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_07_EfCore.Models
{
    public class Bakery
    {
        [Key]
        public int BakeryId { get; set; }
        [StringLength(50, MinimumLength = 4)]
        public string BakeryName { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public ICollection<CupCake> Cupcakes { get; set; }
    }
}
