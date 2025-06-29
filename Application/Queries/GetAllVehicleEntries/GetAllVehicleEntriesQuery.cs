using Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Queries.GetAllVehicleEntries
{
    [SwaggerSchema("Consulta para obter todos os registros de entrada de veículos.")]
    public class GetAllVehicleEntriesQuery : IRequest<IEnumerable<VehicleAccess>>
    {
    }
}
