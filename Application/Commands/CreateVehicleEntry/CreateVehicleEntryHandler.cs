using Application.Validators.Error;
using Domain.Contracts.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands.CreateVehicleEntry
{
    public class CreateVehicleEntryHandler : IRequestHandler<CreateVehicleEntryCommand, VehicleAccess>
    {
        private readonly IVehicleAccessRepository _repository;

        private readonly IValidator<CreateVehicleEntryCommand> _validator;

        public CreateVehicleEntryHandler(
            IVehicleAccessRepository repository,
            IValidator<CreateVehicleEntryCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<VehicleAccess> Handle(CreateVehicleEntryCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException((IEnumerable<FluentValidation.Results.ValidationFailure>)validationResult.Errors.ToCustomValidatorFailures());

            var entity = new VehicleAccess
            {
                Plate = command.Plate,
                DriverName = command.DriverName,
                VehicleType = command.VehicleType,
                PeopleCount = command.PeopleCount,
                Observations = command.Observations,
                EntryTime = DateTime.Now
            };

            await _repository.InsertAsync(entity, cancellationToken);

            return entity;
        }
    }
}
