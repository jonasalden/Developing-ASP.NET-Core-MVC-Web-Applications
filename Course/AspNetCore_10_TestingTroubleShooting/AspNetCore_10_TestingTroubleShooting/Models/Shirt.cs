﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AspNetCore_10_TestingTroubleShooting.Models
{
    public class Shirt
    {
        public int Id { get; set; }
        [Display(Name = "Size"), Required]
        public ShirtSize Size { get; set; }
        [Display(Name = "Color"), Required]
        public ShirtColor Color { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public float Tax { get; set; }

        #region Constructor
        public string GetFormattedTaxedPrice()
        {
            return (Price * Tax).ToString($"C2", CultureInfo.GetCultureInfo("en-US"));
        }
        #endregion
    }
}
