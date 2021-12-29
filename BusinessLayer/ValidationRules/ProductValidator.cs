using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductTitle)
                .NotEmpty().WithMessage("Ürün Adı alanını boş bırakmayınız!");
            RuleFor(x => x.ProductCode)
                .NotEmpty().WithMessage("Ürün Kodu alanını boş bırakmayınız!");
            RuleFor(x => x.ProductContent)
                .NotEmpty().WithMessage("Ürün Açıklaması alanını boş bırakmayınız!");
        }
    }
}
