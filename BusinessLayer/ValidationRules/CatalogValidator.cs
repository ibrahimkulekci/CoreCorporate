using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CatalogValidator: AbstractValidator<Catalog>
    {
        public CatalogValidator()
        {
            RuleFor(x => x.CatalogTitle)
                .NotEmpty().WithMessage("Katalog Adı alanını boş bırakmayınız!");
            RuleFor(x => x.CatalogContent)
                .NotEmpty().WithMessage("Katalog Açıklaması alanını boş bırakmayınız!");
        }
    }
}
