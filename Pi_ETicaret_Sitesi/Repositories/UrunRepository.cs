using Pi_ETicaret_Sitesi.Contexts;
using Pi_ETicaret_Sitesi.Entities;
using Pi_ETicaret_Sitesi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Repositories
{
    public class UrunRepository :GenericRepository<Urun>,IUrunRepository
    {
        public List<Kategori> GetirKategoriler(int urunId)
        {
            using var context = new Context();
            return context.Urunler.Join(context.UrunKategoriler, urun => urun.id, urunKategori => urunKategori.urunId,
                (u, uc) => new
                {
                    urun = u,
                    urunKategori = uc
                }).Join(context.Kategoriler, iki => iki.urunKategori.kategoriId, kategori => kategori.id,
                (uc, k) => new
                {
                    urun = uc.urun,
                    kategori = k,
                    urunKategori = uc.urunKategori
                }).Where(I => I.urun.id == urunId).Select(I => new Kategori
                {
                    ad = I.kategori.ad,
                    id = I.kategori.id

                }).ToList();
        }
    }
}
