using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pi_ETicaret_Sitesi.Entities;
using Pi_ETicaret_Sitesi.Interfaces;
using Pi_ETicaret_Sitesi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrunRepository _urunRepository;
        private readonly SignInManager<AppUser> _signInManager;

        //urunRepository nesne örneğini aldık
        public HomeController(IUrunRepository urunRepository, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _urunRepository = urunRepository;

        }

        public IActionResult Index()
        {
            SetSession("kisi", "cookieDeneme");
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

        public IActionResult GirisYap()
        {
            return View(new KullaniciGirisModel());
        }

        [HttpPost]
        public IActionResult GirisYap(KullaniciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = _signInManager.PasswordSignInAsync(model.KullaniciAd, model.Sifre, model.BeniHatırla, false).Result;

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("", "Kullanici adi veya şifre hatalı");
            }
            return View(model);
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
