using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.ServiceCategory;
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
    public class ServiceCategoryManager : IServiceCategoryService
    {
        IServiceCategoryDal _serviceCategoryDal;

        public ServiceCategoryManager(IServiceCategoryDal serviceCategoryDal)
        {
            _serviceCategoryDal = serviceCategoryDal;
        }

        public BaseResultListModel<ServiceCategory> GetAllByQuery(ServiceCategoryQueryModel queryModel)
        {
            BaseResultListModel<ServiceCategory> resultList = new BaseResultListModel<ServiceCategory>()
            {
                DataList = new List<ServiceCategory>(),
                TotalRecordCount = 0
            };

            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = (from sc in dbContext.ServiceCategories
                             select sc);

                //filtering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(r => r.ServiceCategoryID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_Status.HasValue)
                {
                    record = record.Where(r => r.ServiceCategoryStatus == queryModel.Filter_Status.Value);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(r => r.ServiceCategoryCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && r.ServiceCategoryCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.ServiceCategoryTitle.Contains(queryModel.Filter_Search));
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

        public List<ServiceCategory> GetList()
        {
            return _serviceCategoryDal.GetListAll().OrderByDescending(x => x.ServiceCategoryID).ToList();
        }

        public void TAdd(ServiceCategory p)
        {
            _serviceCategoryDal.Insert(p);
        }

        public void TDelete(ServiceCategory p)
        {
            _serviceCategoryDal.Delete(p);
        }

        public ServiceCategory TGetById(int id)
        {
            return _serviceCategoryDal.GetById(id);
        }

        public ServiceCategory TGetByUrl(string p)
        {
            return _serviceCategoryDal.GetByUrl(p);
        }

        public void TUpdate(ServiceCategory p)
        {
            _serviceCategoryDal.Update(p);
        }
    }
}
