using Microsoft.AspNetCore.Http;
using Pi_ETicaret_Sitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Repositories
{
    public class SepetRepository
    {
        private readonly HttpContextAccessor _httpContextAccessor;
        public SepetRepository()
        {
            HttpContextAccessor hca1 = new HttpContextAccessor();
            _httpContextAccessor = hca1;
        }
        public void SepeteEkle(Urun urun)
        {
            var gelenListe = _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
            if (gelenListe == null)
            {
                gelenListe = new List<Urun>();
                gelenListe.Add(urun);
            }
            else
            {
                gelenListe.Add(urun);
            }
            _httpContextAccessor.HttpContext.Session.SetObject("sepet", gelenListe);

        }
        public void SepettenCikar(Urun urun)
        {
            var gelenListe =
                _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
            gelenListe.Remove(urun);

            _httpContextAccessor.HttpContext.Session.SetObject("sepet", gelenListe);
        }

        public List<Urun> GetirSepettekiUrunler()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");

        }

        public void SepetiBosalt()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sepet");
        }
    }
}
