using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Properties;

namespace WebAPI.Validators
{
    public class FluxoItemChecklistValidator : AbstractValidator<FluxoItemChecklist>
    {
        private readonly IFluxoItemCheckListService _service;

        public FluxoItemChecklistValidator(IFluxoItemCheckListService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Nome", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Nome do Check-list"));

            RuleFor(x => x.FluxoItemId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Fluxo Item"));
        }

        private bool BeUnique(FluxoItemChecklist entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }
    }
}
