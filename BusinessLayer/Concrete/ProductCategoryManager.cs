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
    public class ProductCategoryManager:IProductCategoryService
    {
        IProductCategoryDal _productCategoryManager;

        public ProductCategoryManager(IProductCategoryDal productCategoryManager)
        {
            _productCategoryManager = productCategoryManager;
        }

        public List<ProductCategory> GetList()
        {
            return _productCategoryManager.GetListAll().OrderByDescending(x => x.ProductCategoryID).ToList();
        }

        public void TAdd(ProductCategory p)
        {
            _productCategoryManager.Insert(p);
        }

        public void TDelete(ProductCategory p)
        {
            _productCategoryManager.Delete(p);
        }

        public ProductCategory TGetById(int id)
        {
            return _productCategoryManager.GetById(id);
        }

        public ProductCategory TGetByUrl(string p)
        {
            return _productCategoryManager.GetByUrl(p);
        }

        public void TUpdate(ProductCategory p)
        {
            _productCategoryManager.Update(p);
        }
    }
}
