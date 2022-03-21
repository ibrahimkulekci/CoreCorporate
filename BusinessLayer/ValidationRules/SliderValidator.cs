using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class SliderValidator: AbstractValidator<Slider>
    {
        public SliderValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Slider Resim alanını boş bırakmayınız!");
        }
    }
}
