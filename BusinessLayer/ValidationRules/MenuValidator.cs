using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MenuValidator : AbstractValidator<Menu>
    {
        public MenuValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Menü Adı alanını boş bırakmayınız!");
            RuleFor(x => x.MenuUrl)
                .NotEmpty().WithMessage("Menü Adresi alannı boş bırakmayınız!");
        }
    }
}
