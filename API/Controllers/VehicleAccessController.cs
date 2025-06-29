using Microsoft.AspNetCore.Mvc;
using Application.Commands.CreateVehicleEntry;
using Application.Commands.RegisterExit;
using Application.Commands.UpdateVehicleEntry;
using Application.Commands.DeleteVehicleEntry;
using Application.Queries.GetAllVehicleEntries;
using Application.Queries.GetVehicleEntryById;
using MediatR;
using Domain.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleAccessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleAccessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo registro de entrada de veículo.
        /// </summary>
        /// <param name="command">Dados da entrada do veículo.</param>
        /// <returns>O registro criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(VehicleAccess), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEntry([FromBody] CreateVehicleEntryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Registra a saída de um veículo.
        /// </summary>
        /// <param name="id">ID do registro de entrada.</param>
        /// <returns>O registro atualizado com a saída.</returns>
        [HttpPut("{id}/exit")]
        [ProducesResponseType(typeof(VehicleAccess), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterExit(int id)
        {
            var command = new RegisterExitCommand(id);
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Atualiza os dados de uma entrada de veículo existente.
        /// </summary>
        /// <param name="command">Dados atualizados do veículo.</param>
        /// <returns>O registro atualizado.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(VehicleAccess), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEntry([FromBody] UpdateVehicleEntryCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Exclui um registro de entrada de veículo.
        /// </summary>
        /// <param name="id">ID do registro.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var command = new DeleteVehicleEntryCommand(id);
            var success = await _mediator.Send(command);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Busca um registro de entrada de veículo pelo ID.
        /// </summary>
        /// <param name="id">ID do registro.</param>
        /// <returns>O registro encontrado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VehicleAccess), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetVehicleEntryByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Lista todos os registros de entrada de veículos.
        /// </summary>
        /// <returns>Lista de registros.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VehicleAccess>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllVehicleEntriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}