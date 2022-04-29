using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.AdminRole
{
    public class AdminRoleAddViewModel
    {
        [Required(ErrorMessage ="Lütfen Yetki Adı Giriniz!")]
        public string Name { get; set; }
    }
}
