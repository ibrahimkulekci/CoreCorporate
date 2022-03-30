using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Product
{
    public class ProductQueryModel:BaseQueryModel
    {
        //filters

        public long? Filter_Id { get; set; }

        public long? Filter_ProdcutCategoryId { get; set; }
        public bool? Filter_IsActive { get; set; }
        public string? Filter_Search { get; set; }

        public DateTime? Filter_PublishDateTime_Begin { get; set; }
        public DateTime? Filter_PublishDateTime_End { get; set; }
    }
}
