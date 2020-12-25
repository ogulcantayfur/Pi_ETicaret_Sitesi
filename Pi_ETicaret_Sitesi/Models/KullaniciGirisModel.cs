using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Models
{
    public class KullaniciGirisModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilmez")]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Kullanıcı şifresi boş geçilmez")]
        public string Sifre { get; set; }

        public bool BeniHatırla { get; set; }
    }
}
