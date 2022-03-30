using BusinessLayer.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.Product
{
    public class ListViewModel:ProductQueryModel
    {
        public List<BusinessLayer.Models.Product.ProductWithDetail> DataList { get; set; }


        public long TotalRecordCount { get; set; }
        public long TotalPageCount { get { return TotalRecordCount == 0 ? 0 : (TotalRecordCount % PageSize == 0 ? TotalRecordCount / PageSize : (TotalRecordCount / PageSize) + 1); } }


        public string UIMessageContent { get; set; }
        public string UIMessageType { get; set; }

    }
}
