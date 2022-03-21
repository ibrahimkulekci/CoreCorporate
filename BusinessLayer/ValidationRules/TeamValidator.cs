using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class TeamValidator: AbstractValidator<Team>
    {
        public TeamValidator()
        {
            RuleFor(x => x.TeamName)
                .NotEmpty().WithMessage("Ekip Üyesi Adı Soyadı alanını boş bırakmayınız!");
            RuleFor(x => x.TeamPosition)
                .NotEmpty().WithMessage("Ekip Üyesi Ünvanı alanını boş bırakmayınız!");
            RuleFor(x => x.TeamDescription)
                .NotEmpty().WithMessage("Ekip Üyesi Açıklaması alanını boş bırakmayınız!");
        }
    }
}
