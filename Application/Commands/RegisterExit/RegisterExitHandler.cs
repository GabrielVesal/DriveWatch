using Domain.Contracts.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.RegisterExit
{
    public class RegisterExitHandler : IRequestHandler<RegisterExitCommand, VehicleAccess?>
    {
        private readonly IVehicleAccessRepository _repository;

        public RegisterExitHandler(IVehicleAccessRepository repository)
        {
            _repository = repository;
        }

        public async Task<VehicleAccess?> Handle(RegisterExitCommand request, CancellationToken cancellationToken)
        {
            var access = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (access == null || access.ExitTime != null)
                return null;

            access.ExitTime = DateTime.Now;

            await _repository.UpdateAsync(access, cancellationToken);

            return access;
        }
    }
}
