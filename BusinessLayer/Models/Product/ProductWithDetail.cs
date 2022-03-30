using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Product
{
    public class ProductWithDetail: EntityLayer.Concrete.Product
    {
        //ProductCategory property'leri
        public string ProductCategory_Name { get; set; }
        public string ProductCategory_Url { get; set; }
        public string ProductCategory_Image { get; set; }
        public bool ProductCategory_Status { get; set; }

    }
}
