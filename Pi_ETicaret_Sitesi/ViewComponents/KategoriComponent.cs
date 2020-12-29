using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.ViewComponents
{
    public class KategoriComponent : ViewComponent
    {
        private readonly KategoriRepository _kategoriRepository;
        public KategoriComponent()
        {
            KategoriRepository k1 = new KategoriRepository();
            _kategoriRepository = k1;
        }
        public IViewComponentResult Invoke()
        {
            return View(_kategoriRepository.GetirHepsi());
        }
    }
}
