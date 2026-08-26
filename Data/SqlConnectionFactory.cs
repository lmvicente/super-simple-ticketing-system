using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace super_simple_ticketing_system.Data
{
    public sealed class SqlConnectionFactory(IConfiguration config) : IDbConnectionFactory
    {
        public DbConnection Create() =>
            new SqlConnection(config.GetConnectionString("DevPlayground"));
    }
}
