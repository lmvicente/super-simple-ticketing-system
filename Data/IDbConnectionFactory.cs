using System.Data.Common;

namespace super_simple_ticketing_system.Data
{
    public interface IDbConnectionFactory
    {
        DbConnection Create();
    }
}

