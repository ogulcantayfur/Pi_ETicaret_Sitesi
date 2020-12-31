using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Models
{
    public class Urun
    {
        public int id { get; set; }
        [MaxLength(100)]
        public string ad { get; set; }
        [MaxLength(300)]
        public string resim { get; set; }
        public decimal fiyat { get; set; }
        

        public List<UrunKategori> UrunKategoriler { get; set; }
    }
}
