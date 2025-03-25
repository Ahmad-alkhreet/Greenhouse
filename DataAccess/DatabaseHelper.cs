using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<MySqlConnection> GetConnectionAsync()
        {
            try
            {
                Console.WriteLine(" Verbinden met database...");
                var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                Console.WriteLine(" Databaseverbinding succesvol geopend!");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Databasefout: {ex.Message}");
                throw;
            }
        }
    }
}
