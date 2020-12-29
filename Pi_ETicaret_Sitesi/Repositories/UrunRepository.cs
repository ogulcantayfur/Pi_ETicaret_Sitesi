using Pi_ETicaret_Sitesi.Contexts;
using Pi_ETicaret_Sitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Repositories
{
    public class UrunRepository :GenericRepository<Urun>
    {
        private readonly UrunKategoriRepository _urunKategoriRepository;
        public UrunRepository()
        {
            UrunKategoriRepository uk1 = new UrunKategoriRepository();
            _urunKategoriRepository = uk1;
        }
     

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

        public void SilKategori(UrunKategori urunKategori)
        {
           var kayitKontrol= _urunKategoriRepository.GetirFiltreile(I => I.kategoriId == urunKategori.kategoriId && I.urunId == urunKategori.urunId);
            if(kayitKontrol!=null)
            {
                _urunKategoriRepository.Sil(kayitKontrol);
            }
        }

        public void EkleKategori(UrunKategori urunKategori)
        {
            var kayitKontrol = _urunKategoriRepository.GetirFiltreile(I => I.kategoriId == urunKategori.kategoriId && I.urunId == urunKategori.urunId);
            if (kayitKontrol == null)
            {
                _urunKategoriRepository.Ekle(urunKategori);
            }
        }

        public List<Urun> GetirKategoriIdile(int kategoriId)
        {
            using var context = new Context();

            return context.Urunler.Join(context.UrunKategoriler, u => u.id, uc => uc.urunId, (urun, urunKategori) => new
            {
                Urun = urun,
                UrunKategori = urunKategori
            }).Where(I => I.UrunKategori.kategoriId == kategoriId).Select(I => new Urun
            {
                id=I.Urun.id,
                ad=I.Urun.ad,
                fiyat=I.Urun.fiyat,
                resim = I.Urun.resim
            }).ToList();
        }
    }
}
