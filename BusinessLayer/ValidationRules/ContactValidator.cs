using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator: AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad Soyad alanını boş bırakmayınız!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-Posta alanını boş bırakmayınız!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu alanını boş bırakmayınız!");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Mesaj alanını boş bırakmayınız!");
        }
    }
}
