using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }
        public string Name { get; set; }
        public string MenuUrl { get; set; }
        public int MenuParentID { get; set; }
        public int MenuDisplayOrder { get; set; }
        public bool MenuStatus { get; set; }
        public DateTime MenuCreatedDate { get; set; }
        public DateTime MenuUpdatedDate { get; set; }
    }
}
