using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Gallery
{
    public class GalleryQueryModel: BaseQueryModel
    {
        public long? Filter_Id { get; set; }
        public string? Filter_Search { get; set; }
        public bool? Filter_IsActive { get; set; }


        public DateTime? Filter_PublishDateTime_Begin { get; set; }
        public DateTime? Filter_PublishDateTime_End { get; set; }
    }
}
