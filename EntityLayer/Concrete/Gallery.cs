using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Gallery
    {
        [Key]
        public int GalleryID { get; set; }
        public string GalleryTitle { get; set; }
        public string GalleryUrl { get; set; }
        public string GalleryContent { get; set; }
        public string GalleryImage { get; set; }
        public bool GalleryStatus { get; set; }
        public DateTime GalleryCreatedDate { get; set; }
        public DateTime GalleryUpdatedDate { get; set; }

        /*Resim yüklemek için kullanılacak alan*/
        [NotMapped]
        public IFormFile GalleryImageFile { get; set; }
    }
}
