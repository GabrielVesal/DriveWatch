using Domain.Contracts.Repositories;
using MediatR;

namespace Application.Commands.DeleteVehicleEntry
{
    public class DeleteVehicleEntryHandler : IRequestHandler<DeleteVehicleEntryCommand, bool>
    {
        private readonly IVehicleAccessRepository _repository;

        public DeleteVehicleEntryHandler(IVehicleAccessRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteVehicleEntryCommand request, CancellationToken cancellationToken)
        {
            var access = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (access == null)
                return false;

            await _repository.DeleteAsync(access, cancellationToken);
            return true;
        }
    }
}
