using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Commands.DeleteVehicleEntry
{
    [SwaggerSchema("Comando para deletar um registro de entrada de veículo.")]
    public class DeleteVehicleEntryCommand : IRequest<bool>
    {
        [SwaggerSchema("ID do registro de entrada que será deletado.")]
        public int Id { get; set; }

        public DeleteVehicleEntryCommand(int id)
        {
            Id = id;
        }
    }
}
