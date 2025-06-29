using Application.Validators.Error;
using Domain.Contracts.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands.UpdateVehicleEntry
{
    public class UpdateVehicleEntryHandler : IRequestHandler<UpdateVehicleEntryCommand, VehicleAccess?>
    {
        private readonly IVehicleAccessRepository _repository;
        private readonly IValidator<UpdateVehicleEntryCommand> _validator;

        public UpdateVehicleEntryHandler(IVehicleAccessRepository repository, IValidator<UpdateVehicleEntryCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<VehicleAccess?> Handle(UpdateVehicleEntryCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException((IEnumerable<FluentValidation.Results.ValidationFailure>)validationResult.Errors.ToCustomValidatorFailures());

            var access = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (access == null)
                return null;

            access.Plate = command.Plate;
            access.DriverName = command.DriverName;
            access.VehicleType = command.VehicleType;
            access.PeopleCount = command.PeopleCount;
            access.Observations = command.Observations;

            await _repository.UpdateAsync(access, cancellationToken);
            return access;
        }
    }
}
