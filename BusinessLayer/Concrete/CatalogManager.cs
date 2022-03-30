using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.Catalog;
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
    public class CatalogManager:ICatalogService
    {
        ICatalogDal _catalogDal;

        public CatalogManager(ICatalogDal catalogDal)
        {
            _catalogDal = catalogDal;
        }

        public BaseResultListModel<Catalog> GetAllByQuery(CatalogQueryModel queryModel)
        {
            BaseResultListModel<Catalog> resultList = new BaseResultListModel<Catalog>()
            {
                DataList = new List<Catalog>(),
                TotalRecordCount = 0
            };
            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = (from n in dbContext.Catalogs
                              select n);

                //filtering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(x => x.CatalogID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_IsActive.HasValue)
                {
                    record = record.Where(x => x.CatalogStatus == queryModel.Filter_IsActive);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(x => x.CatalogCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && x.CatalogCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.CatalogTitle.Contains(queryModel.Filter_Search));
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
            }

            return resultList;
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
