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
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public string ProductCategoryTitle { get; set; }
        public string ProductCategoryUrl { get; set; }
        public string ProductCategoryContent { get; set; }
        public string ProductCategoryImage { get; set; }
        public bool ProductCategoryStatus { get; set; }
        public DateTime ProductCategoryCreatedDate { get; set; }
        public DateTime ProductCategoryUpdatedDate { get; set; }

        /*Resim yüklemek için kullanılacak alan*/
        [NotMapped]
        public IFormFile ProductCategoryImageFile { get; set; }
    }
}
