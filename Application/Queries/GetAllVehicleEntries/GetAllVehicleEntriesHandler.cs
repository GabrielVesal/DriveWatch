using Domain.Contracts.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.GetAllVehicleEntries
{
    public class GetAllVehicleEntriesHandler : IRequestHandler<GetAllVehicleEntriesQuery, IEnumerable<VehicleAccess>>
    {
        private readonly IVehicleAccessRepository _repository;

        public GetAllVehicleEntriesHandler(IVehicleAccessRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VehicleAccess>> Handle(GetAllVehicleEntriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
