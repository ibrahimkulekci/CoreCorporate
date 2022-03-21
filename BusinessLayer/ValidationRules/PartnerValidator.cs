using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class PartnerValidator: AbstractValidator<Partner>
    {
        public PartnerValidator()
        {
            RuleFor(x => x.PartnerTitle)
                .NotEmpty().WithMessage("Çözüm Ortağı Adı alanını boş bırakmayınız!");
            RuleFor(x => x.PartnerContent)
                .NotEmpty().WithMessage("Çözüm Ortağı Açıklaması alanını boş bırakmayınız!");
        }
    }
}
