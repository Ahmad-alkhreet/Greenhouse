using Domain;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace DataAccess.Repositories
{
    public class MeasurementRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public MeasurementRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<Measurement>> GetAllAsync()
        {
            var measurements = new List<Measurement>();
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT Id, Temperature, Humidity, Timestamp, SensorId FROM Measurements";
                using (var command = new MySqlCommand (query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            measurements.Add(new Measurement(
                                reader.GetInt32(0),
                                reader.GetDouble(1),  // Ensure these match the column data types
                                reader.GetDouble(2),
                                reader.GetDateTime(3),
                                reader.GetInt32(4)
                            ));
                        }
                    }
                }
            }
            return measurements;
        }


        // splitsen en proberen alles maken voor de bulding  
        //quiries zijn goed

        // 
        public async Task<List<Measurement>> GetMeasurementsBySensorIdAsync(int sensorId)
        {
            var measurements = new List<Measurement>();

            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT Id, Temperature, Humidity, Timestamp FROM Measurements WHERE SensorId = @SensorId";
                using (var command = new MySqlCommand (query, connection))
                {
                    command.Parameters.AddWithValue("@SensorId", sensorId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            measurements.Add(new Measurement(
                                reader.GetInt32(0),
                                reader.GetDouble(1),
                                reader.GetDouble(2),
                                reader.GetDateTime(3),
                                sensorId
                            ));
                        }
                    }
                }
            }
            return measurements;
        }

        public async Task AddAsync(Measurement measurement)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "INSERT INTO Measurements (Temperature, Humidity, Timestamp, SensorId) VALUES (@Temperature, @Humidity, @Timestamp, @SensorId)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Temperature", measurement.GetTemperature());
                    command.Parameters.AddWithValue("@Humidity", measurement.GetHumidity());
                    command.Parameters.AddWithValue("@Timestamp", measurement.GetTimestamp());
                    command.Parameters.AddWithValue("@SensorId", measurement.GetSensorId());
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
