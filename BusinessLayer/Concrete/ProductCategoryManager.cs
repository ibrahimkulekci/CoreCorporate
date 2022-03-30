using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.ProductCategory;
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
    public class ProductCategoryManager:IProductCategoryService
    {
        IProductCategoryDal _productCategoryManager;

        public ProductCategoryManager(IProductCategoryDal productCategoryManager)
        {
            _productCategoryManager = productCategoryManager;
        }

        public BaseResultListModel<ProductCategory> GetAllByQuery(ProductCategoryQueryModel queryModel)
        {
            BaseResultListModel<ProductCategory> resultList = new BaseResultListModel<ProductCategory>()
            {
                DataList = new List<ProductCategory>(),
                TotalRecordCount = 0
            };

            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = (from sc in dbContext.ProductCategories
                              select sc);

                //filtering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(r => r.ProductCategoryID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_IsActive.HasValue)
                {
                    record = record.Where(r => r.ProductCategoryStatus == queryModel.Filter_IsActive.Value);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(r => r.ProductCategoryCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && r.ProductCategoryCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.ProductCategoryTitle.Contains(queryModel.Filter_Search));
                }

                //shorting
                if (!string.IsNullOrEmpty(queryModel.SortOn) && !string.IsNullOrEmpty(queryModel.SortDirection))
                {
                    record = record.OrderBy(string.Format("{0} {1}", queryModel.SortOn, queryModel.SortDirection));
                }

                //total count
                resultList.TotalRecordCount = record.Count();

                //paging
                record = record.Skip(queryModel.PageSize * (queryModel.CurrentPage - 1)).Take(queryModel.PageSize);

                resultList.DataList.AddRange(record.AsNoTracking().ToList());

            };

            return resultList;
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
