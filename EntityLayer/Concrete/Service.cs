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
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        public int ServiceCategoryID { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceUrl { get; set; }
        public string ServiceContent { get; set; }
        public bool ServiceStatus { get; set; }
        public DateTime ServiceCreatedDate { get; set; }
        public DateTime ServiceUpdatedDate { get; set; }

        /*Resim yüklemek için kullanılacak alan*/
        [NotMapped]
        public IFormFile ServiceImageFile { get; set; }

    }
}
