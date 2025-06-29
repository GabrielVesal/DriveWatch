using System.Data;

namespace Infra.Data.Context
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
