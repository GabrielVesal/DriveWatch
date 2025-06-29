using Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Commands.RegisterExit
{
    [SwaggerSchema("Comando para registrar a saída de um veículo.")]
    public class RegisterExitCommand : IRequest<VehicleAccess?>
    {
        [SwaggerSchema("ID do registro de entrada do veículo.")]
        public int Id { get; set; }

        public RegisterExitCommand(int id)
        {
            Id = id;
        }
    }
}
