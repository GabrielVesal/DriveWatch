using Application.Commands.CreateVehicleEntry;
using FluentValidation;

namespace Application.Validators
{
    public class CreateVehicleEntryValidator : AbstractValidator<CreateVehicleEntryCommand>
    {
        public CreateVehicleEntryValidator()
        {
            RuleFor(x => x.Plate)
                .NotEmpty().WithMessage("Placa é obrigatória.")
                .Length(1, 10).WithMessage("A placa deve ter entre 1 e 10 caracteres.");
            RuleFor(x => x.DriverName)
                .NotEmpty().WithMessage("Nome do motorista é obrigatório.")
                .Length(1, 50).WithMessage("O nome do motorista deve ter entre 1 e 50 caracteres.");
            RuleFor(x => x.VehicleType)
                .NotEmpty().WithMessage("Tipo do veículo é obrigatório.")
                .Length(1, 20).WithMessage("O tipo do veículo deve ter entre 1 e 20 caracteres.");
            RuleFor(x => x.PeopleCount)
                .GreaterThan(0).WithMessage("A quantidade de pessoas deve ser maior que zero.")
                .LessThanOrEqualTo(100).WithMessage("A quantidade de pessoas não pode exceder 100.");
            RuleFor(x => x.Observations)
                .MaximumLength(200).WithMessage("As observações não podem ultrapassar 200 caracteres.");
        }
    }
}
