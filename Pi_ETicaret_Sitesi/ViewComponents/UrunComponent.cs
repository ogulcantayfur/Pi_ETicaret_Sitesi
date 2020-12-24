using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.ViewComponents
{
    public class UrunComponent : ViewComponent
    {
        private readonly IUrunRepository _urunRepository;
        public UrunComponent(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_urunRepository.GetirHepsi());
        }
    }
}
