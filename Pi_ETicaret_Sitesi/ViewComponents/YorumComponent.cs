using Microsoft.AspNetCore.Mvc;
using Pi_ETicaret_Sitesi.Contexts;
using Pi_ETicaret_Sitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.ViewComponents
{
    public class YorumComponent : ViewComponent
    {
        public List<Yorum> UrunYorumlariniGetir(int urunId)
        {
            using var context = new Context();
            var yorumListTemp = context.Set<Yorum>().ToList();
            var yorumList = new List<Yorum>();
            for (int i = 0; i < yorumListTemp.Count; i++)
            {
                if (yorumListTemp[i].urunId==urunId)
                {
                    yorumList.Add(yorumListTemp[i]);
                }
            }
            return(yorumList);
        }

        public IViewComponentResult Invoke(int urunId)
        {
            return View(UrunYorumlariniGetir(urunId));
        }

       
    }
}
