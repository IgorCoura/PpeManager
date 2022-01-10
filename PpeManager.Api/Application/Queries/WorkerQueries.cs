using Dapper;
using Npgsql;

namespace PpeManager.Api.Application.Queries
{
    public class WorkerQueries : IWorkerQueries
    {
        private readonly string _connectionString;

        public WorkerQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<dynamic> GetAllWorkers()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT * FROM ppemanager.workers"
                    );

                return result;
            }
        }
    }
}
