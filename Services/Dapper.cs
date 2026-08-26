using Dapper;
using Microsoft.Data.SqlClient;
using super_simple_ticketing_system.Models;

namespace super_simple_ticketing_system.Services
{
    public class Dapper
    {

        private readonly string _connectionString;
        public Dapper(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DevPlayground")!;
        }

        public async Task<IEnumerable<T>> DoQueryAsync<T>(string sql, object? param = null)
        {
            using var connection = new SqlConnection(_connectionString); //this pulls from the appsettings.json file and creates a new SQL
            return await connection.QueryAsync<T>(sql, param);
        }

        public async Task<int> ExecuteAsync(string sql, object? param = null)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(sql, param);
        }

        public async Task<List<Technicians>> GetTechniciansAsync()
        {
            return (await DoQueryAsync<Technicians>("SELECT * FROM Technicians")).ToList();
        }

        public async Task<List<TicketStatus>> GetTicketStatusesAsync()
        {
            return (await DoQueryAsync<TicketStatus>("SELECT * FROM TicketStatus")).ToList();
        }

        public async Task<List<TicketType>> GetTicketTypesAsync()
        {
            return (await DoQueryAsync<TicketType>("SELECT * FROM TicketTypes")).ToList();
        }


    }


}