using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Entities
{
    public class UrunKategori
    {
        public int id { get; set; }
        public int urunId { get; set; }
        public Urun urun { get; set; }
        public int kategoriId { get; set; }
        public Kategori kategori { get; set; }
    }
}
