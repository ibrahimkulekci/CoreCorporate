using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class SettingValidator: AbstractValidator<Setting>
    {
        public SettingValidator()
        {
            RuleFor(x => x.Key).NotEmpty().WithMessage("Lütfen Key Adı alanını boş bırakmayınız!");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Lütfen Value Değeri alanını boş bırakmayınız!");
        }
    }
}
