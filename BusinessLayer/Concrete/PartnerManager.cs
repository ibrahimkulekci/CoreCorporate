using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.Partner;
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
    public class PartnerManager:IPartnerService
    {
        IPartnerDal _partnerDal;

        public PartnerManager(IPartnerDal partnerDal)
        {
            _partnerDal = partnerDal;
        }

        public BaseResultListModel<Partner> GetAllByQuery(PartnerQueryModel queryModel)
        {
            BaseResultListModel<Partner> resultList = new BaseResultListModel<Partner>()
            {
                DataList = new List<Partner>(),
                TotalRecordCount = 0
            };
            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = (from n in dbContext.Partners
                              select n);

                //filtering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(x => x.PartnerID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_IsActive.HasValue)
                {
                    record = record.Where(x => x.PartnerStatus == queryModel.Filter_IsActive);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(x => x.PartnerCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && x.PartnerCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.PartnerTitle.Contains(queryModel.Filter_Search));
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

        public List<Partner> GetList()
        {
            return _partnerDal.GetListAll().OrderByDescending(x => x.PartnerID).ToList();
        }

        public void TAdd(Partner p)
        {
            _partnerDal.Insert(p);
        }

        public void TDelete(Partner p)
        {
            _partnerDal.Delete(p);
        }

        public Partner TGetById(int id)
        {
            return _partnerDal.GetById(id);
        }

        public Partner TGetByUrl(string p)
        {
            return _partnerDal.GetByUrl(p);
        }

        public void TUpdate(Partner p)
        {
            _partnerDal.Update(p);
        }
    }
}
