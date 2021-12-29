using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ServiceCategoryValidator: AbstractValidator<ServiceCategory>
    {
        public ServiceCategoryValidator()
        {
            RuleFor(x => x.ServiceCategoryTitle)
                .NotEmpty().WithMessage("Hizmet Kategori Adı alanını boş bırakmayınız!");
            RuleFor(x => x.ServiceCategoryContent)
                .NotEmpty().WithMessage("Hizmet Kategori Açıklaması alanını boş bırakmayınız!");
        }
    }
}
