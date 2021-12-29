using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class NewsValidator: AbstractValidator<News>
    {
        public NewsValidator()
        {
            RuleFor(x => x.NewsTitle)
                .NotEmpty().WithMessage("Haber Başlık alanını boş bırakmayınız!");
            RuleFor(x => x.NewsContent)
                .NotEmpty().WithMessage("Haber İçerik alanını boş bırakmayınız!");
        }
    }
}
