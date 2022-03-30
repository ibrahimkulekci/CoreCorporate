using BusinessLayer.Abstract;
using BusinessLayer.Models;
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
    public class NewsManager:INewsService
    {
        INewsDal _newsDal;

        public NewsManager(INewsDal newsDal)
        {
            _newsDal = newsDal;
        }

        public BaseResultListModel<News> GetAllByQuery(NewsQueryModel queryModel)
        {
            BaseResultListModel<News> resultList = new BaseResultListModel<News>()
            {
                DataList = new List<News>(),
                TotalRecordCount = 0
            };
            using (AppDbContext dbContext =new AppDbContext())
            {
                var record = (from n in dbContext.News
                              select n);

                //filtering
                if (queryModel.Filter_NewsID.HasValue)
                {
                    record = record.Where(x => x.NewsID == queryModel.Filter_NewsID.Value);
                }
                if (queryModel.Filter_NewsStatus.HasValue)
                {
                    record = record.Where(x => x.NewsStatus == queryModel.Filter_NewsStatus);
                }
                if(queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(x => x.NewsCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && x.NewsCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.NewsTitle.Contains(queryModel.Filter_Search));
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

        public List<News> GetList()
        {
            return _newsDal.GetListAll().OrderByDescending(x => x.NewsID).ToList();
        }

        public void TAdd(News p)
        {
            _newsDal.Insert(p);
        }

        public void TDelete(News p)
        {
            _newsDal.Delete(p);
        }

        public News TGetById(int id)
        {
            return _newsDal.GetById(id);
        }

        public News TGetByUrl(string p)
        {
            return _newsDal.GetByUrl(p);
        }

        public void TUpdate(News p)
        {
            _newsDal.Update(p);
        }
    }
}
