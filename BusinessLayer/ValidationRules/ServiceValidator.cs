using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
            RuleFor(x => x.ServiceTitle)
                .NotEmpty().WithMessage("Hizmet Adı alanını boş bırakmayınız!")
                .MinimumLength(2).WithMessage("Hizmet Adı alanını 2 karakterden fazla giriniz!");
            RuleFor(x => x.ServiceContent)
                .NotEmpty().WithMessage("Hizmet Açıklaması alanını boş bırakmayınız!");
        }
    }
}
