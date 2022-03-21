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
    public class GalleryService: IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;

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
