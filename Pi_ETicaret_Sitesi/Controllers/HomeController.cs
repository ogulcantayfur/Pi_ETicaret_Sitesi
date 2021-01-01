using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pi_ETicaret_Sitesi.Contexts;
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
        private readonly UrunRepository _urunRepository;
        private readonly SignInManager<AppUser> _signInManager;

        //urunRepository nesne örneğini aldık
        public HomeController(SignInManager<AppUser> signInManager)
        {
            UrunRepository u1 = new UrunRepository();
            _signInManager = signInManager;
            _urunRepository = u1;
        }

        public IActionResult Index(int? kategoriId)
        {
            ViewBag.kategoriId = kategoriId;
            return View(_urunRepository.GetirHepsi());
        }

        public IActionResult UrunDetay(int id)
        {
            SetSession("id", id.ToString());
            ViewBag.Session = GetSession("kisi");
            return View(_urunRepository.GetirIdile(id));
        }

        [HttpPost]
        public IActionResult UrunDetay(string txtAd, string txtYorum)
        {

            using var context = new Context();
            Yorum y1 = new Yorum();
            y1.kullaniciAdi = txtAd;
            y1.yorum = txtYorum;
            y1.urunId = Int32.Parse(GetSession("id"));
            y1.yorumTarihi = DateTime.Now;
            context.Set<Yorum>().Add(y1);
            context.SaveChanges();
            return RedirectToAction("UrunDetay", new { id = Int32.Parse(GetSession("id")) });
        }

        public void SetSession(string key, string value) //Session,sunucu tarafında kaynakları tüketir.
        {
            HttpContext.Session.SetString(key, value);
        }
        public string GetSession(string key)
        {

            return HttpContext.Session.GetString(key);
        }



        [HttpPost]
        public IActionResult DilYonetimi(string dil,string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(dil)),
                new CookieOptions {Expires=DateTimeOffset.Now.AddDays(30)});
            return LocalRedirect(returnUrl);
           // return RedirectToAction("Index");
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
