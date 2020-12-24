using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.ViewComponents
{
    public class KategoriComponent : ViewComponent
    {
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriComponent(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_kategoriRepository.GetirHepsi());
        }
    }
}
