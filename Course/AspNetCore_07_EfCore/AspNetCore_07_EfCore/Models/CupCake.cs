using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AspNetCore_07_EfCore.Models
{
    public class CupCake
    {
        [Key]
        public int CupcakeId { get; set; }
        [Required(ErrorMessage = "Please select a cupcake type")]
        [Display(Name = "Cypcake type:")]
        public CupcakeType? CupcakeType { get; set; }
        [Required]
        [Display(Name = "Description:")]
        public string Description { get; set; }
        [Display(Name = "Gluten free:")]
        public bool GlutenFree { get; set; }
        [Range(1,15)]
        [Required(ErrorMessage = "Please enter a cupcake price")]
        [DataType(DataType.Currency)]
        [Display(Name = "Price:")]
        public double? Price { get; set; }
        [NotMapped]
        [Display(Name = "Cupcake Picture:")]
        public IFormFile PhotoAvatar { get; set; }
        public string ImageName { get; set; }
        public byte[] PhotoFile { get; set; }
        public string ImageMimeType { get; set; }
        [Required]
        public int? BakeryId { get; set; }
        public Bakery Bakery { get; set; }
        [Display(Name = "Caloric Value:")]
        public int CaloricValue { get; set; }
    }
}
