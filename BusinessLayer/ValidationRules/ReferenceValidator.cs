using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ReferenceValidator: AbstractValidator<Reference>
    {
        public ReferenceValidator()
        {
            RuleFor(x => x.ReferenceTitle)
                .NotEmpty().WithMessage("Referans Adı alanını boş bırakmayınız!");
            RuleFor(x => x.ReferenceContent)
                .NotEmpty().WithMessage("Referans Açıklaması alanını boş bırakmayınız!");
        }
    }
}
