using Application.Commands.UpdateVehicleEntry;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateVehicleEntryValidator : AbstractValidator<UpdateVehicleEntryCommand>
    {
        public UpdateVehicleEntryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("o Id é obrigatório.");
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("A placa é obrigatória.")
                .Length(1, 10).WithMessage("A placa deve ter entre 1 e 10 caracteres.");
            RuleFor(x => x.DriverName)
                .NotEmpty().WithMessage("O nome do motorista é obrigatório.")
                .Length(1, 50).WithMessage("O nome do motorista deve ter entre 1 e 50 caracteres.");
            RuleFor(x => x.VehicleType)
                .NotEmpty().WithMessage("O tipo do veículo é obrigatório.")
                .Length(1, 20).WithMessage("O tipo do veículo deve ter entre 1 e 20 caracteres.");
            RuleFor(x => x.PeopleCount)
                .GreaterThan(0).WithMessage("A quantidade de pessoas deve ser maior que zero.")
                .LessThanOrEqualTo(100).WithMessage("A quantidade de pessoas não pode exceder 100.");
            RuleFor(x => x.Observations)
                .MaximumLength(200).WithMessage("As observações não podem ultrapassar 200 caracteres.");
        }
    }
}
