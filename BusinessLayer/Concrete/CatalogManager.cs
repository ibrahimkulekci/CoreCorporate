using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CatalogManager:ICatalogService
    {
        ICatalogDal _catalogDal;

        public CatalogManager(ICatalogDal catalogDal)
        {
            _catalogDal = catalogDal;
        }

        public List<Catalog> GetList()
        {
            return _catalogDal.GetListAll().OrderByDescending(x => x.CatalogID).ToList();
        }

        public List<Catalog> GetListBySatusTrue()
        {
            return _catalogDal.GetListAll().Where(x => x.CatalogStatus == true).ToList();
        }

        public void TAdd(Catalog p)
        {
            _catalogDal.Insert(p);
        }

        public void TDelete(Catalog p)
        {
            _catalogDal.Delete(p);
        }

        public Catalog TGetById(int id)
        {
            return _catalogDal.GetById(id);
        }

        public Catalog TGetByUrl(string p)
        {
            return _catalogDal.GetByUrl(p);
        }

        public void TUpdate(Catalog p)
        {
            _catalogDal.Update(p);
        }
    }
}
