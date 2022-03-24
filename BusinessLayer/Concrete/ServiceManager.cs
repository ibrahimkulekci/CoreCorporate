using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.Service;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Concrete
{
    public class ServiceManager : IServiceService
    {
        IServiceDal _serviceDal;

        public ServiceManager(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public BaseResultListModel<ServiceWithDetail> GetAllByQuery(ServiceQueryModel queryModel)
        {
            BaseResultListModel<ServiceWithDetail> resultList = new BaseResultListModel<ServiceWithDetail>
            {
                DataList = new List<ServiceWithDetail>(),
                TotalRecordCount = 0
            };

            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = from s in dbContext.Services
                             join sc in dbContext.ServiceCategories on s.ServiceCategoryID equals sc.ServiceCategoryID
                             select new ServiceWithDetail()
                             {
                                 ServiceID = s.ServiceID,
                                 ServiceCategoryID = s.ServiceCategoryID,
                                 ServiceTitle = s.ServiceTitle,
                                 ServiceImage = s.ServiceImage,
                                 ServiceUrl = s.ServiceUrl,
                                 ServiceContent = s.ServiceContent,
                                 ServiceStatus = s.ServiceStatus,
                                 ServiceCreatedDate = s.ServiceCreatedDate,
                                 ServiceUpdatedDate = s.ServiceUpdatedDate,

                                 ServiceCategoryTitle = sc.ServiceCategoryTitle,
                                 ServiceCategoryContent = sc.ServiceCategoryContent,
                                 ServiceCategoryStatus = sc.ServiceCategoryStatus,
                                 ServiceCategoryUrl = sc.ServiceCategoryUrl
                             };

                //filtering
                if (queryModel.Filter_ServiceID.HasValue)
                {
                    record = record.Where(x => x.ServiceID == queryModel.Filter_ServiceID.Value);
                }
                if (queryModel.Filter_ServiceCategoryID.HasValue)
                {
                    record = record.Where(x => x.ServiceCategoryID == queryModel.Filter_ServiceCategoryID.Value);
                }
                if (queryModel.Filter_ServiceStatus.HasValue)
                {
                    record = record.Where(x => x.ServiceStatus == queryModel.Filter_ServiceStatus.Value);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(x => x.ServiceCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && x.ServiceCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.ServiceTitle.Contains(queryModel.Filter_Search));
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

        public List<Service> GetList()
        {
            return _serviceDal.GetListAll().OrderByDescending(x => x.ServiceID).ToList();
        }

        public void TAdd(Service p)
        {
            _serviceDal.Insert(p);
        }

        public void TDelete(Service p)
        {
            _serviceDal.Delete(p);
        }

        public Service TGetById(int id)
        {
            return _serviceDal.GetById(id);
        }

        public Service TGetByUrl(string p)
        {
            return _serviceDal.GetByUrl(p);
        }

        public void TUpdate(Service p)
        {
            _serviceDal.Update(p);
        }
    }
}
