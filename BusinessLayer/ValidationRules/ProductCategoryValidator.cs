using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ProductCategoryValidator: AbstractValidator<ProductCategory>
    {
        public ProductCategoryValidator()
        {
            RuleFor(x => x.ProductCategoryTitle)
                .NotEmpty().WithMessage("Ürün Kategori Adı alanını boş bırakmayınız!");
            RuleFor(x => x.ProductCategoryContent)
                .NotEmpty().WithMessage("Ürün Kategori Açıklaması alanını boş bırakmayınız!");
        }
    }
}
