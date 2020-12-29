using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Models;
using Pi_ETicaret_Sitesi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KategoriController : Controller
    {
        private readonly KategoriRepository _kategoriRepository;

        public KategoriController()
        {
            KategoriRepository k1 =new KategoriRepository();
            _kategoriRepository = k1;
        }

        public IActionResult Index()
        {
            return View(_kategoriRepository.GetirHepsi());
        }

        public IActionResult Ekle()
        {
            return View(new KategoriEkleme());
        }

        [HttpPost]
        public IActionResult Ekle(KategoriEkleme model)
        {
            if(ModelState.IsValid)
            {
                _kategoriRepository.Ekle(new Kategori
                {
                    ad = model.ad
                });
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Guncelle(int id)
        {
            var kategori = _kategoriRepository.GetirIdile(id);
            KategoriGuncelleme model = new KategoriGuncelleme {
                id = kategori.id,
                ad = kategori.ad
            };
           return View(model);
        }
        [HttpPost]
        public IActionResult Guncelle(KategoriGuncelleme model)
        {
            if(ModelState.IsValid)
            {
                var kategori = _kategoriRepository.GetirIdile(model.id);
                kategori.ad = model.ad;
                _kategoriRepository.Guncelle(kategori);
                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        public IActionResult Sil(int id)
        {
            _kategoriRepository.Sil(new Kategori { id = id });
            return RedirectToAction("Index");

        }

    }
}
