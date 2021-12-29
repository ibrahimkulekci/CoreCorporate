using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Partner
    {
        [Key]
        public int PartnerID { get; set; }
        public string PartnerTitle { get; set; }
        public string PartnerUrl { get; set; }
        public string PartnerContent { get; set; }
        public string PartnerImage { get; set; }
        public bool PartnerStatus { get; set; }
        public DateTime PartnerCreatedDate { get; set; }
        public DateTime PartnerUpdateDate { get; set; }

    }
}
