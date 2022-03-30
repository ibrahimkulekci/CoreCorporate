using BusinessLayer.Abstract;
using BusinessLayer.Models;
using BusinessLayer.Models.Gallery;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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
    public class GalleryService: IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;

        }

        public BaseResultListModel<Gallery> GetAllByQuery(GalleryQueryModel queryModel)
        {
            BaseResultListModel<Gallery> resultList = new BaseResultListModel<Gallery>()
            {
                DataList = new List<Gallery>(),
                TotalRecordCount = 0
            };

            using (AppDbContext dbContext = new AppDbContext())
            {
                var record = (from sc in dbContext.Galleries
                              select sc);

                //filtering
                if (queryModel.Filter_Id.HasValue)
                {
                    record = record.Where(r => r.GalleryID == queryModel.Filter_Id.Value);
                }
                if (queryModel.Filter_IsActive.HasValue)
                {
                    record = record.Where(r => r.GalleryStatus == queryModel.Filter_IsActive.Value);
                }
                if (queryModel.Filter_PublishDateTime_Begin.HasValue && queryModel.Filter_PublishDateTime_End.HasValue)
                {
                    record = record.Where(r => r.GalleryCreatedDate >= queryModel.Filter_PublishDateTime_Begin.Value && r.GalleryCreatedDate < queryModel.Filter_PublishDateTime_End.Value);
                }
                if (queryModel.Filter_Search != null)
                {
                    record = record.Where(x => x.GalleryTitle.Contains(queryModel.Filter_Search));
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

        public List<Gallery> GetList()
        {
            return _galleryRepository.GetListAll().OrderByDescending(x => x.GalleryID).ToList();
        }

        public void TAdd(Gallery p)
        {
            _galleryRepository.Insert(p);
        }

        public void TDelete(Gallery p)
        {
            _galleryRepository.Delete(p);
        }

        public Gallery TGetById(int id)
        {
            return _galleryRepository.GetById(id);
        }

        public Gallery TGetByUrl(string p)
        {
            return _galleryRepository.GetByUrl(p);
        }

        public void TUpdate(Gallery p)
        {
            _galleryRepository.Update(p);
        }


    }
}
