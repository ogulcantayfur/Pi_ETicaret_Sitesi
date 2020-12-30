using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Controllers
{
    public class SepetController : Controller
    {

        private readonly SepetRepository _sepetRepository;
        private readonly UrunRepository _urunRepository;

        public SepetController()
        {
            UrunRepository u1 = new UrunRepository();
            SepetRepository s1 = new SepetRepository();
            _sepetRepository = s1;
            _urunRepository = u1;
        }

        public IActionResult Sepet()
        {
            return View(_sepetRepository.GetirSepettekiUrunler());
        }

        public IActionResult SepettenCikar(int id)
        {
            var cikarilacakUrun = _urunRepository.GetirIdile(id);
            _sepetRepository.SepettenCikar(cikarilacakUrun);
            return RedirectToAction("Sepet","Sepet");
        }

        [Authorize]
        public IActionResult SepetiSatinAl(decimal fiyat)
        {
            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Odeme","Sepet", new { fiyat = fiyat });
        }

        public IActionResult SepetiBosalt()
        {
            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Sepet","Sepet");
        }

        [Authorize]
        public IActionResult Tesekkur(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }

        [Authorize]
        public IActionResult Odeme(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }
        public IActionResult SepeteEkle(int id)
        {
            var urun = _urunRepository.GetirIdile(id);
            _sepetRepository.SepeteEkle(urun);
            TempData["bildirim"] = "Ürün sepete eklendi";
            return RedirectToAction("Index","Home");
        }
    }
}
