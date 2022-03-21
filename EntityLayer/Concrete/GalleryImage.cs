using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{

    //[Table("GalleryImage")]
    public class GalleryImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GalleryImageId { get; set; }

        public int GalleryId { get; set; }

        [Required]
        [StringLength(1000)]
        public string ImageUrl { get; set; }

        public int DisplayOrder { get; set; }

    }
}
