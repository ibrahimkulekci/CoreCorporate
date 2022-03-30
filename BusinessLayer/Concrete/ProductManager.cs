using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.Product;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public BaseResultListModel<ProductWithDetail> GetAllByQuery(ProductQueryModel queryModel)
        {
            BaseResultListModel<ProductWithDetail> resultList = new BaseResultListModel<ProductWithDetail>() {
                DataList = new List<ProductWithDetail>(),
                TotalRecordCount = 0
            };

            using (AppDbContext dbContext=new AppDbContext())
            {
                var record = from p in dbContext.Products
                             join pc in dbContext.ProductCategories on p.ProductCategoryID equals pc.ProductCategoryID
                             select new ProductWithDetail()
                             {
                                 ProductCategoryID = p.ProductCategoryID,
                                 ProductCategory_Image = pc.ProductCategoryImage,
                                 ProductCategory_Name = pc.ProductCategoryTitle,
                                 ProductCategory_Status = pc.ProductCategoryStatus,
                                 ProductCategory_Url = pc.ProductCategoryUrl,
                                 ProductCode = p.ProductCode,
                                 ProductContent = p.ProductContent,
                                 ProductCreatedDate = p.ProductCreatedDate,
                                 ProductID = p.ProductID,
                                 ProductImage = p.ProductImage,
                                 ProductStatus = p.ProductStatus,
                                 ProductTitle = p.ProductTitle,
                                 ProductUpdatedDate = p.ProductUpdatedDate,
                                 ProductUrl = p.ProductUrl
                             };

                //fitering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(x => x.ProductID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_ProdcutCategoryId.HasValue)
                {
                    record = record.Where(x => x.ProductCategoryID == queryModel.Filter_ProdcutCategoryId.Value);
                }
                if (queryModel.Filter_IsActive.HasValue)
                {
                    record = record.Where(x => x.ProductCategory_Status == queryModel.Filter_IsActive.Value);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(x => x.ProductCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && x.ProductCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.ProductTitle.Contains(queryModel.Filter_Search));
                }

                // sorting
                if (!string.IsNullOrEmpty(queryModel.SortOn) && !string.IsNullOrEmpty(queryModel.SortDirection))
                {
                    record = record.OrderBy(string.Format("{0} {1}", queryModel.SortOn, queryModel.SortDirection));
                }

                // total count
                resultList.TotalRecordCount = record.Count();

                // paging
                record = record.Skip(queryModel.PageSize * (queryModel.CurrentPage - 1)).Take(queryModel.PageSize);


                resultList.DataList.AddRange(record.AsNoTracking().ToList());

            }

            return resultList;
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
