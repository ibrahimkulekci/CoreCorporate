using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class GalleryValidator:AbstractValidator<Gallery>
    {
        public GalleryValidator()
        {
            RuleFor(x => x.GalleryTitle)
                .NotEmpty().WithMessage("Galleri Adı alanını boş bırakmayınız!");
            RuleFor(x => x.GalleryContent)
                .NotEmpty().WithMessage("Galleri Açıklaması alannı boş bırakmayınız!");
        }
    }
}
