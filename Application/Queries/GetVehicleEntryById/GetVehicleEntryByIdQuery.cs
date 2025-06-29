using Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Queries.GetVehicleEntryById
{
    [SwaggerSchema("Consulta para obter um registro de entrada de veículo pelo ID.")]
    public class GetVehicleEntryByIdQuery : IRequest<VehicleAccess>
    {
        [SwaggerSchema("ID do registro de entrada a ser buscado.")]
        public int Id { get; set; }

        public GetVehicleEntryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
