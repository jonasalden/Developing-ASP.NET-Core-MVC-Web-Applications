using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using AspNetCore_06_Developing_Models.Validators;
namespace AspNetCore_06_Developing_Models.Models
{
    public class Butterfly
    {
        public int Id { get; set; }

        [Display(Name = "Common Name:")]
        [Required(ErrorMessage = "Please enter butterfly name")]
        public string CommonName { get; set; }
        [Display(Name = "Butterfly family:")]
        [Required(ErrorMessage = "Please enter butterfly family")]
        public Family? ButterflyFamily { get; set; }
        [Display(Name = "Butterfly Quantity:")]
        [Required(ErrorMessage = "Please enter quantity")]
        [MaxButterflyQuantityValidation(maxAmount: 50)]
        public int? Quantity { get; set; }
        [Display(Name = "Characteristics:")]
        [Required(ErrorMessage = "Please type the charatcheristics")]
        [StringLength(maximumLength:50)]
        public string Characteristics { get; set; }
        [Display(Name = "Updated on:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Butterflies Picture:")]
        [Required(ErrorMessage = "Please select the butterflies picture")]
        public IFormFile PhotoAvatar { get; set; }
        public string ImageName { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] Photofile { get; set; }
    }
}
