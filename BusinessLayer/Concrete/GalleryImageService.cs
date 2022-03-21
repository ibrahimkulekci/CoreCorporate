using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GalleryImageService: IGalleryImageService
    {
        private readonly IGalleryImageRepository _galleryImageRepository;

        public GalleryImageService(IGalleryImageRepository galleryImageRepository)
        {
            _galleryImageRepository = galleryImageRepository;

        }

        public List<GalleryImage> GetList()
        {
            return _galleryImageRepository.GetListAll().OrderByDescending(x => x.GalleryImageId).ToList();
        }

        public void TAdd(GalleryImage p)
        {
            _galleryImageRepository.Insert(p);
        }

        public void TDelete(GalleryImage p)
        {
            _galleryImageRepository.Delete(p);
        }

        public GalleryImage TGetById(int id)
        {
            return _galleryImageRepository.GetById(id);
        }

        public GalleryImage TGetByUrl(string p)
        {
            return _galleryImageRepository.GetByUrl(p);
        }

        public void TUpdate(GalleryImage p)
        {
            _galleryImageRepository.Update(p);
        }

        public List<GalleryImage> GetAllByGalleryId(int galleryId)
        {
            return _galleryImageRepository.GetListAll(r => r.GalleryId == galleryId);
        }

    }
}
