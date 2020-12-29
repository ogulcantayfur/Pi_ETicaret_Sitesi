using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.ViewComponents
{
    public class UrunComponent : ViewComponent
    {
        private readonly UrunRepository _urunRepository;
        public UrunComponent()
        {
            UrunRepository u1 = new UrunRepository();
            _urunRepository = u1;
        }
        public IViewComponentResult Invoke(int? kategoriId)
        {
            if (kategoriId.HasValue)
            {
                return View(_urunRepository.GetirKategoriIdile((int)kategoriId));
            }
            return View(_urunRepository.GetirHepsi());
        }
    }
}
