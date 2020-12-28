﻿using Pi_ETicaret_Sitesi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Interfaces
{
    public interface IUrunRepository : IGenericRepository<Urun>
    {
        public List<Kategori> GetirKategoriler(int urunId);
        public void EkleKategori(UrunKategori urunKategori);
        public void SilKategori(UrunKategori urunKategori);
        List<Urun> GetirKategoriIdile(int kategoriId);
    }
}
