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
    public class ProductManager:IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetList()
        {
            return _productDal.GetListAll().OrderByDescending(x => x.ProductID).ToList();
        }

        public void TAdd(Product p)
        {
            _productDal.Insert(p);
        }

        public void TDelete(Product p)
        {
            _productDal.Delete(p);
        }

        public Product TGetById(int id)
        {
            return _productDal.GetById(id);
        }

        public Product TGetByUrl(string p)
        {
            return _productDal.GetByUrl(p);
        }

        public void TUpdate(Product p)
        {
            _productDal.Update(p);
        }
    }
}
