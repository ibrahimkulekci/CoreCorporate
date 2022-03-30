using BusinessLayer.Models;
using BusinessLayer.Models.Gallery;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGalleryService : IGenericService<Gallery>
    {
        public BaseResultListModel<Gallery> GetAllByQuery(GalleryQueryModel queryModel);
    }
}
