using Swashbuckle.AspNetCore.Annotations;

namespace Domain.Entities
{
    [SwaggerSchema("Representa um registro de entrada e saída de veículo.")]
    public class VehicleAccess
    {
        [SwaggerSchema("Identificador único do registro.")]
        public int Id { get; set; }

        [SwaggerSchema("Placa do veículo.")]
        public string Plate { get; set; }

        [SwaggerSchema("Nome do motorista.")]
        public string DriverName { get; set; }

        [SwaggerSchema("Tipo do veículo.")]
        public string VehicleType { get; set; }

        [SwaggerSchema("Número de pessoas no veículo.")]
        public int PeopleCount { get; set; }

        [SwaggerSchema("Observações adicionais.")]
        public string Observations { get; set; }

        [SwaggerSchema("Data e hora de entrada.")]
        public DateTime EntryTime { get; set; }

        [SwaggerSchema("Data e hora de saída (se houver).")]
        public DateTime? ExitTime { get; set; }
    }
}
