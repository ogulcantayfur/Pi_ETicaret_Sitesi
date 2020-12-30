using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Models;
using Pi_ETicaret_Sitesi.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UrunController : Controller
    {
        private readonly UrunRepository _urunRepository;
        private readonly KategoriRepository _kategoriRepository;

        public UrunController()
        {
            UrunRepository u1 = new UrunRepository();
            KategoriRepository k1 = new KategoriRepository();

            _kategoriRepository =k1;
            _urunRepository = u1;
        }
        public IActionResult Index()
        {
            return View(_urunRepository.GetirHepsi());
        }


        public IActionResult Ekle()
        {
            return View(new UrunEklemeModel());
        }

        [HttpPost]
        public IActionResult Ekle(UrunEklemeModel model)
        {
            if(ModelState.IsValid)
            {
                Urun urun = new Urun();
                if (model.resim!=null)
                {
                    var uzanti = Path.GetExtension(model.resim.FileName);
                    var yeniResim = Guid.NewGuid() + uzanti;
                    var dosyaKonumu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + yeniResim);
                    if(model.resim.ContentType=="image/jpeg"|| model.resim.ContentType == "image/png")
                    {
                        var stream = new FileStream(dosyaKonumu, FileMode.Create);
                        model.resim.CopyTo(stream);
                    }
                    urun.resim = yeniResim;
                }
                
                urun.ad = model.ad;
                urun.fiyat = model.fiyat;

                _urunRepository.Ekle(urun);

                return RedirectToAction("Index", "Urun");
            }
            return View(model);

        }

        public IActionResult Guncelle(int id)
        {
            var urun = _urunRepository.GetirIdile(id);

            UrunGuncelleme model = new UrunGuncelleme
            {
                ad = urun.ad,
                fiyat = urun.fiyat,
                id = urun.id
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Guncelle (UrunGuncelleme model)
        {
            if (ModelState.IsValid)
            {
                var urun = _urunRepository.GetirIdile(model.id);
                if (model.resim != null)
                {
                    var uzanti = Path.GetExtension(model.resim.FileName);
                    var yeniResim = Guid.NewGuid() + uzanti;
                    var dosyaKonumu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + yeniResim);
                    if (model.resim.ContentType == "image/jpeg" || model.resim.ContentType == "image/png")
                    {
                        var stream = new FileStream(dosyaKonumu, FileMode.Create);
                        model.resim.CopyTo(stream);
                    }
                    urun.resim = yeniResim;
                }

                urun.ad = model.ad;
                urun.fiyat = model.fiyat;

                _urunRepository.Guncelle(urun);

                return RedirectToAction("Index", "Urun");
            }
            return View(model);
        }


        public IActionResult Sil(int id)
        {
            _urunRepository.Sil(new Urun { id = id });
            return RedirectToAction("Index");

        }


        public IActionResult KategoriAta(int id)
        {
            var urunKategorileri = _urunRepository.GetirKategoriler(id).Select(I=>I.ad);
            var kategoriler = _kategoriRepository.GetirHepsi();

            TempData["UrunId"] = id;
            List<KategoriAtama> list = new List<KategoriAtama>();

            foreach(var item in kategoriler)
            {
                KategoriAtama model = new KategoriAtama();
                model.kategoriId = item.id;
                model.kategoriAd = item.ad;
                model.varMi = urunKategorileri.Contains(item.ad);

                list.Add(model);
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult KategoriAta(List<KategoriAtama> list)
        {
            int urunId = (int)TempData["UrunId"];
            foreach (var item in list)
            {
                if(item.varMi)
                {
                    _urunRepository.EkleKategori(new UrunKategori
                    {
                        kategoriId = item.kategoriId,
                        urunId = urunId
                    });
                }
                else
                {
                    _urunRepository.SilKategori(new UrunKategori
                    {
                        kategoriId = item.kategoriId,
                        urunId = urunId
                    });
                }
            }
            return RedirectToAction("Index");

        }

    }
}









       