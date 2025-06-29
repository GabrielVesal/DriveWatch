using Dapper;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Repositories
{
    public class VehicleAccessRepository : IVehicleAccessRepository
    {
        private readonly IDbContext _dbcontext;

        public VehicleAccessRepository(IDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task InsertAsync(VehicleAccess entity, CancellationToken cancellationToken)
        {
            using var conn = _dbcontext.CreateConnection();

            var sql = @"INSERT INTO VehicleAccesses 
                (Plate, DriverName, VehicleType, PeopleCount, EntryTime, ExitTime, Observations) 
                VALUES 
                (@Plate, @DriverName, @VehicleType, @PeopleCount, @EntryTime, @ExitTime, @Observations);       
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var command = new CommandDefinition(sql, entity, cancellationToken: cancellationToken);

            entity.Id = await conn.ExecuteScalarAsync<int>(command);
        }

        public async Task<IEnumerable<VehicleAccess>> GetAllAsync(CancellationToken cancellationToken)
        {
            using var conn = _dbcontext.CreateConnection();

            var command = new CommandDefinition(
                "SELECT * FROM VehicleAccesses",
                cancellationToken: cancellationToken);

            return await conn.QueryAsync<VehicleAccess>(command);
        }

        public async Task<VehicleAccess?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            using var conn = _dbcontext.CreateConnection();

            var command = new CommandDefinition(
                "SELECT * FROM VehicleAccesses WHERE Id = @id",
                new { id },
                cancellationToken: cancellationToken);

            return await conn.QueryFirstOrDefaultAsync<VehicleAccess>(command);
        }

        public async Task UpdateAsync(VehicleAccess entity, CancellationToken cancellationToken)
        {
            using var conn = _dbcontext.CreateConnection();

            var sql = @"UPDATE VehicleAccesses SET
                        Plate = @Plate,
                        DriverName = @DriverName,
                        VehicleType = @VehicleType,
                        PeopleCount = @PeopleCount,
                        EntryTime = @EntryTime,
                        ExitTime = @ExitTime,
                        Observations = @Observations
                        WHERE Id = @Id";

            var command = new CommandDefinition(sql, entity, cancellationToken: cancellationToken);

            await conn.ExecuteAsync(command);
        }

        public async Task DeleteAsync(VehicleAccess entity, CancellationToken cancellationToken)
        {
            using var conn = _dbcontext.CreateConnection();

            var command = new CommandDefinition(
                "DELETE FROM VehicleAccesses WHERE Id = @Id",
                new { entity.Id },
                cancellationToken: cancellationToken);

            await conn.ExecuteAsync(command);
        }
    }
}

