using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Domain;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public UserRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT Id, Name, Role FROM Users WHERE Name = @Name";
                using (var command = new MySqlCommand (query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                Enum.Parse<UserRole>(reader.GetString(2))
                            );
                        }
                    }
                }
            }
            return null;
        }

        public async Task AddAsync(User user)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "INSERT INTO Users (Name, Role) VALUES (@Name, @Role)";
                using (var command = new MySqlCommand (query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.GetName());
                    command.Parameters.AddWithValue("@Role", user.GetRole().ToString());
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
