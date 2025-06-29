using Domain.Contracts.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.GetVehicleEntryById
{
    public class GetVehicleEntryByIdHandler : IRequestHandler<GetVehicleEntryByIdQuery, VehicleAccess?>
    {
        private readonly IVehicleAccessRepository _repository;

        public GetVehicleEntryByIdHandler(IVehicleAccessRepository repository)
        {
            _repository = repository;
        }

        public async Task<VehicleAccess?> Handle(GetVehicleEntryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
