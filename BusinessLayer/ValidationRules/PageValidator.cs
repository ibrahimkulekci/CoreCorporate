using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class PageValidator: AbstractValidator<Page>
    {
        public PageValidator()
        {
            RuleFor(x => x.PageContent)
                .NotEmpty().WithMessage("Sayfa içerik alanını boş bırakmayınız!");
            RuleFor(x => x.PageTitle)
                .NotEmpty().WithMessage("Sayfa başlık alanını boş bırakmayınız!")
                .MinimumLength(2).WithMessage("Sayfa başlık alanını 2 karakterden fazla giriniz!");           
        }
    }
}
