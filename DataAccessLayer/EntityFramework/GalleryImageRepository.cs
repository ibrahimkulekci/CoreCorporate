using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class GalleryImageRepository: GenericRepository<GalleryImage>, IGalleryImageRepository
    {
        public GalleryImageRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public List<GalleryImage> GetAllByGalleryId(int galleryId)
        {
            return this.GetListAll(r => r.GalleryId == galleryId);
        }

    }
}
