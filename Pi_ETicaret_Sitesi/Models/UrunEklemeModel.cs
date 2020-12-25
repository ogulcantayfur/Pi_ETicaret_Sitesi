using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Models
{
    public class UrunEklemeModel
    {
        [Required(ErrorMessage="Ad alanı gereklidir")]
        public string ad { get; set; }

        [Range(0,double.MaxValue,ErrorMessage ="Geçerli bir fiyat giriniz.")]
        public decimal fiyat { get; set; }
        public IFormFile resim { get; set; }
    }
}
