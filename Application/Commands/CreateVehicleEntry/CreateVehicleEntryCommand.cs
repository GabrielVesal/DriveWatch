using Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Commands.CreateVehicleEntry
{
    [SwaggerSchema("Comando para registrar a entrada de um veículo.")]

    public class CreateVehicleEntryCommand : IRequest<VehicleAccess>
    {
        [SwaggerSchema("Placa do veículo (ex: ABC-1234).")]
        public string Plate { get; set; }

        [SwaggerSchema("Nome do motorista.")]
        public string DriverName { get; set; }

        [SwaggerSchema("Tipo do veículo (ex: Caminhão, Carro, Van).")]
        public string VehicleType { get; set; }

        [SwaggerSchema("Número de pessoas no veículo.")]
        public int PeopleCount { get; set; }

        [SwaggerSchema("Observações adicionais (opcional).")]
        public string Observations { get; set; }
    }
}
