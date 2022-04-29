using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCorporate.Areas.AdminPanel.Models.User
{
    public class UserRegisterViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen Ad Soyad Giriniz!")]
        public string NameSurname { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Lütfen Parola Giriniz!")]
        public string Password { get; set; }

        [Display(Name = "Parola Tekrar")]
        [Compare("Password", ErrorMessage = "Parolalar Uyuşmuyor!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-Posta")]
        [Required(ErrorMessage = "Lütfen E-Posta Giriniz!")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adı Giriniz!")]
        public string Username { get; set; }
    }
}
