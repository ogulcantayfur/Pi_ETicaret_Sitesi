using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Entities
{
    public class Kategori
    {
        public int id { get; set; }
        [MaxLength(100)]
        public string ad { get; set; }

        public List<UrunKategori> UrunKategoriler { get; set; }
    }
}
