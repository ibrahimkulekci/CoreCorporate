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
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public string ProductUrl { get; set; }
        public string ProductContent { get; set; }
        public bool ProductStatus { get; set; }
        public DateTime ProductCreatedDate { get; set; }
        public DateTime ProductUpdatedDate { get; set; }

        /*Resim yüklemek için kullanılacak alan*/
        [NotMapped]
        public IFormFile ProductImageFile { get; set; }
    }
}
