﻿using Pi_ETicaret_Sitesi.Contexts;
using Pi_ETicaret_Sitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Repositories
{
    public class UrunKategoriRepository : GenericRepository<UrunKategori>
    {
        public UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter)
        {
            using var context = new Context();
            return context.UrunKategoriler.FirstOrDefault(filter);

        }
    }
}
