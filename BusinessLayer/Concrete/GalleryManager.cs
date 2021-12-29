using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GalleryManager:IGalleryService
    {
        IGalleryDal _galleryDal;

        public GalleryManager(IGalleryDal galleryDal)
        {
            _galleryDal = galleryDal;
        }

        public List<Gallery> GetList()
        {
            return _galleryDal.GetListAll().OrderByDescending(x => x.GalleryID).ToList();
        }

        public void TAdd(Gallery p)
        {
            _galleryDal.Insert(p);
        }

        public void TDelete(Gallery p)
        {
            _galleryDal.Delete(p);
        }

        public Gallery TGetById(int id)
        {
            return _galleryDal.GetById(id);
        }

        public Gallery TGetByUrl(string p)
        {
            return _galleryDal.GetByUrl(p);
        }

        public void TUpdate(Gallery p)
        {
            _galleryDal.Update(p);
        }
    }
}
