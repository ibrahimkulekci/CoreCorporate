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
    public class News
    {
        [Key]
        public int NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsImage { get; set; }
        public string NewsUrl { get; set; }
        public string NewsContent { get; set; }
        public bool NewsStatus { get; set; }
        public DateTime NewsCreatedDate { get; set; }
        public DateTime NewsUpdatedDate { get; set; }

        /*Resim yüklemek için kullanılacak alan*/
        [NotMapped]
        public IFormFile NewsImageFile { get; set; }
    }
}
