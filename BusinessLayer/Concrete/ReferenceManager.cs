using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.Reference;
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
    public class ReferenceManager:IReferenceService
    {
        IReferenceDal _referenceDal;

        public ReferenceManager(IReferenceDal referenceDal)
        {
            _referenceDal = referenceDal;
        }

        public BaseResultListModel<Reference> GetAllByQuery(ReferenceQueryModel queryModel)
        {
            BaseResultListModel<Reference> resultList = new BaseResultListModel<Reference>()
            {
                DataList = new List<Reference>(),
                TotalRecordCount = 0
            };
            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = (from n in dbContext.References
                              select n);

                //filtering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(x => x.ReferenceID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_IsActive.HasValue)
                {
                    record = record.Where(x => x.ReferenceStatus == queryModel.Filter_IsActive);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(x => x.ReferenceCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && x.ReferenceCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.ReferenceTitle.Contains(queryModel.Filter_Search));
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


        public List<Reference> GetList()
        {
            return _referenceDal.GetListAll().OrderByDescending(x => x.ReferenceID).ToList();
        }

        public void TAdd(Reference p)
        {
            _referenceDal.Insert(p);
        }

        public void TDelete(Reference p)
        {
            _referenceDal.Delete(p);
        }

        public Reference TGetById(int id)
        {
            return _referenceDal.GetById(id);
        }

        public Reference TGetByUrl(string p)
        {
            return _referenceDal.GetByUrl(p);
        }

        public void TUpdate(Reference p)
        {
            _referenceDal.Update(p);
        }
    }
}
