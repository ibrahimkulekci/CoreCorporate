using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.User
{
    public class UserLoginViewModel
    {
        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen Parola Giriniz!")]
        public string Password { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Giriniz!")]
        public string Username { get; set; }
    }
}
