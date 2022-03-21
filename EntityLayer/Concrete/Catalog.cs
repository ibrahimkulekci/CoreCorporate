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
    public class Catalog
    {
        [Key]
        public int CatalogID { get; set; }
        public string CatalogTitle { get; set; }
        public string CatalogUrl { get; set; }
        public string CatalogContent { get; set; }
        public string CatalogPdfUrl { get; set; }
        public string CatalogPdfIframe { get; set; }
        public string CatalogImage { get; set; }
        public bool CatalogStatus { get; set; }
        public DateTime CatalogCreatedDate { get; set; }
        public DateTime CatalogUpdatedDate { get; set; }

        /*Resim yüklemek için kullanılacak alan*/
        [NotMapped]
        public IFormFile CatalogImageFile { get; set; }
    }
}
