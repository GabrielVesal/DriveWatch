using Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Commands.UpdateVehicleEntry
{
    [SwaggerSchema("Comando para atualizar os dados de uma entrada de veículo.")]
    public class UpdateVehicleEntryCommand : IRequest<VehicleAccess>
    {
        [SwaggerSchema("ID do registro de entrada que será atualizado.")]
        public int Id { get; set; }

        [SwaggerSchema("Placa atualizada do veículo.")]
        public string Plate { get; set; }

        [SwaggerSchema("Nome do motorista.")]
        public string DriverName { get; set; }

        [SwaggerSchema("Tipo do veículo (ex: Carro, Caminhão, Van).")]
        public string VehicleType { get; set; }

        [SwaggerSchema("Quantidade de pessoas no veículo.")]
        public int PeopleCount { get; set; }

        [SwaggerSchema("Observações adicionais (opcional).")]
        public string Observations { get; set; }
    }
}
