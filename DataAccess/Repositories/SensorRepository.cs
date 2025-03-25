using Domain;
using MySql.Data.MySqlClient;

namespace DataAccess.Repositories
{
    public class SensorRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public SensorRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<Sensor>> GetAllAsync()
        {
            var sensors = new List<Sensor>();
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT id, location, type FROM sensors";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sensors.Add(new Sensor(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            ));
                        }
                    }
                }
            }
            return sensors;
        }

        public async Task<Sensor> GetByIdAsync(int id)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT id, location, type FROM sensors WHERE id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Sensor(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            );
                        }
                    }
                }
            }
            return null; // Sensor niet gevonden
        }

        public async Task AddAsync(Sensor sensor)
        {
            Console.WriteLine(" Sensor wordt opgeslagen in de database...");

            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                using (var command = new MySqlCommand("INSERT INTO Sensors (Location, Type) VALUES (@Location, @Type)", connection))
                {
                    command.Parameters.AddWithValue("@Location", sensor.GetLocation());
                    command.Parameters.AddWithValue("@Type", sensor.GetType());

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine(" Sensor succesvol toegevoegd!");
                    }
                    else
                    {
                        Console.WriteLine(" Sensor toevoegen mislukt!");
                    }
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            Console.WriteLine($" Verwijderen van sensor met ID {id}...");

            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                using (var command = new MySqlCommand("DELETE FROM Sensors WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Sensor {id} is succesvol verwijderd.");
                    }
                    else
                    {
                        Console.WriteLine($"Sensor {id} kon niet worden gevonden of verwijderd!");
                    }
                }
            }
        }

        public async Task UpdateAsync(Sensor sensor)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                using (var command = new MySqlCommand("UPDATE sensors SET location = @Location, type = @Type WHERE id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", sensor.GetId());
                    command.Parameters.AddWithValue("@Location", sensor.GetLocation());
                    command.Parameters.AddWithValue("@Type", sensor.GetType());

                    await command.ExecuteNonQueryAsync();
                }
            }


        }
    }
}
