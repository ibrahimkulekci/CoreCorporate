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
    public class Reference
    {
        [Key]
        public int ReferenceID { get; set; }
        public string ReferenceTitle { get; set; }
        public string ReferenceUrl { get; set; }
        public string ReferenceContent { get; set; }
        public string ReferenceImage { get; set; }
        public bool ReferenceStatus { get; set; }
        public DateTime ReferenceCreatedDate { get; set; }
        public DateTime ReferenceUpdatedDate { get; set; }

        /*Resim yüklemek için kullanılacak alan*/
        [NotMapped]
        public IFormFile ReferenceImageFile { get; set; }
    }
}
