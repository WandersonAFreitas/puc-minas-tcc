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
    public class EstadoValidator : AbstractValidator<Estado>
    {
        private readonly IEstadoService _service;

        public EstadoValidator(IEstadoService service)
        {
            _service = service;

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Nome"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Nome", 3))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Nome", 300))
                .Must(BeUnique).WithMessage(string.Format(Resources.MustBeUnique, "O Nome do Estado"));
            
            RuleFor(x => x.Sigla)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Sigla"))
                .MinimumLength(3).WithMessage(string.Format(Resources.MaximumLength, "Sigla", 1))
                .MaximumLength(300).WithMessage(string.Format(Resources.MaximumLength, "Sigla", 2));

            RuleFor(x => x.PaisId)
                .NotEmpty().WithMessage(string.Format(Resources.NotEmpty, "Pais"));
        }

        private bool BeUnique(Estado entity, string nome)
        {
            var query = _service.GetQueryableAsync().Result;
            var exist = query.Any(x => x.Nome == entity.Nome && x.Id != entity.Id);
            return !exist;
        }
    }
}
