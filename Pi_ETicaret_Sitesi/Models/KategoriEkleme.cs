using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Models
{
    public class KategoriEkleme
    {
        [Required(ErrorMessage="Ad alanı boş geçilmez")]
        public string ad { get; set; }
    }
}
