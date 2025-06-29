using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IVehicleAccessRepository
    {
        Task InsertAsync(VehicleAccess entity, CancellationToken cancellationToken);
        Task<VehicleAccess?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<VehicleAccess>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(VehicleAccess entity, CancellationToken cancellationToken);
        Task DeleteAsync(VehicleAccess entity, CancellationToken cancellationToken);
    }
}


