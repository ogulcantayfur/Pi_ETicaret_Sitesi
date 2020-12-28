using Pi_ETicaret_Sitesi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pi_ETicaret_Sitesi.Interfaces
{
    public interface IUrunKategoriRepository: IGenericRepository<UrunKategori>
    {
        public UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter);
    }
}
