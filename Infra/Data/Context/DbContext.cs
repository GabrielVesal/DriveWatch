using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Data.Context
{
    public class DbContext : IDbContext
    {
        public readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DriveWatch");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
        
    }
}
