using Microsoft.AspNetCore.Razor.TagHelpers;
using Pi_ETicaret_Sitesi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.TagHelpers
{
    [HtmlTargetElement("getirKategoriAd")]
    public class KategoriAd : TagHelper
    {
        private readonly UrunRepository _urunRepository;
        public int UrunId { get; set; }


        public KategoriAd()
        {
            UrunRepository u1 = new UrunRepository();
            _urunRepository = u1;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string data = "";
            var gelenKategoriler = _urunRepository.GetirKategoriler(UrunId).Select(I => I.ad);

            foreach (var item in gelenKategoriler)
            {
                data += item + " , ";
            }

            output.Content.SetContent(data);

        }
    }
}
