using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.Gallery
{
    public class GalleryImageAddViewModel
    {
        public GalleryImageAddViewModel()
        {
            GalleryImageList = new List<GalleryImage>();
            FileList = new List<IFormFile>();
        }


        public int GalleryId { get; set; }
        public string GalleryTitle { get; set; }

        public List<IFormFile> FileList { get; set; }

        public List<GalleryImage> GalleryImageList { get; set; }

    }
}
