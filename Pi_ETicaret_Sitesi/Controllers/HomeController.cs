using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pi_ETicaret_Sitesi.Models;
using Pi_ETicaret_Sitesi.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly SepetRepository _sepetRepository;
        private readonly UrunRepository _urunRepository;
        private readonly SignInManager<AppUser> _signInManager;

        //urunRepository nesne örneğini aldık
        public HomeController(SignInManager<AppUser> signInManager)
        {
            SepetRepository s1 = new SepetRepository();
            UrunRepository u1 = new UrunRepository();
            _signInManager = signInManager;
            _urunRepository = u1;
            _sepetRepository = s1;

        }

        public IActionResult Index(int ?kategoriId)
        {
            ViewBag.kategoriId = kategoriId;
            return View(_urunRepository.GetirHepsi());
        }

        public IActionResult UrunDetay(int id)
        {  
            ViewBag.Session = GetSession("kisi");
            return View(_urunRepository.GetirIdile(id));
        }


        public void SetSession(string key,string value) //Session,sunucu tarafında kaynakları tüketir.
        {
            HttpContext.Session.SetString(key, value);
        }
        public string GetSession(string key)
        {
            
            return HttpContext.Session.GetString(key);
        }

        public IActionResult Sepet()
        {

            return View(_sepetRepository.GetirSepettekiUrunler());
        }

        public IActionResult SepettenCikar(int id)
        {
            var cikarilacakUrun = _urunRepository.GetirIdile(id);
            _sepetRepository.SepettenCikar(cikarilacakUrun);
            return RedirectToAction("Sepet");
        }

        public IActionResult SepetiSatinAl(decimal fiyat)
        {
            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Tesekkur", new { fiyat = fiyat });
        }

        public IActionResult SepetiBosalt()
        {
            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Sepet");
        }


        public IActionResult Tesekkur(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }


        public IActionResult SepeteEkle(int id)
        {
            var urun = _urunRepository.GetirIdile(id);
            _sepetRepository.SepeteEkle(urun);
            TempData["bildirim"] = "Ürün sepete eklendi";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
