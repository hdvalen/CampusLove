
using CampusLove.Domain.Ports;
using CampusLove.Repositories;
using CampusLoveampusLove.Domain.Entities;
using MySql.Data.MySqlClient;
using System.Data;

namespace CampusLove.Infrastructure.Repositories
{
    public class InteresesRepository : IGenericRepository<Intereses>, IInteresesRepository
    {
        private readonly string _connectionString;

        public InteresesRepository(MySqlConnection connection)
        {
            _connectionString = connection.ConnectionString;
        }

        // Constructor alternativo que recibe directamente el connection string
        public InteresesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Intereses>> GetAllAsync()
        {
            var intereses = new List<Intereses>();
            string query = "SELECT id, nombre FROM intereses";

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new MySqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                intereses.Add(new Intereses
                {
                    id = Convert.ToInt32(reader.GetInt32("id")),
                    nombre = reader["nombre"].ToString() ?? string.Empty
                });
            }

            return intereses;
        }

        public async Task<Intereses?> GetByIdAsync(object id)
        {
            Intereses? interes = null;
            string query = "SELECT id, nombre FROM intereses WHERE id = @id";

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                interes = new Intereses
                {
                    id = Convert.ToInt32(reader.GetInt32("id")),
                    nombre = reader["nombre"].ToString() ?? string.Empty
                };
            }

            return interes;
        }

        public async Task<bool> InsertAsync(int id, Intereses entity)
        {
            string query = "INSERT INTO intereses (nombre) VALUES (@nombre)";

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nombre", entity.nombre);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAsync(Intereses entity)
        {
            string query = "UPDATE intereses SET nombre = @nombre WHERE id = @id";

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nombre", entity.nombre);
            cmd.Parameters.AddWithValue("@id", entity.id);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            string query = "DELETE FROM intereses WHERE id = @id";

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<Intereses?> GetByNombreAsync(string nombre)
        {
            Intereses? interes = null;
            string query = "SELECT id, nombre FROM intereses WHERE nombre = @nombre";

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                interes = new Intereses
                {
                    id = Convert.ToInt32(reader["id"]),
                    nombre = reader["nombre"].ToString() ?? string.Empty
                };
            }

            return interes;
        }

        public async Task<int> InsertAndReturnIdAsync(Intereses entity)
        {
            string query = "INSERT INTO intereses (nombre) VALUES (@nombre); SELECT LAST_INSERT_ID();";

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nombre", entity.nombre);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        Task IInteresesRepository.GetByNombreAsync(string interesInput)
        {
            return GetByNombreAsync(interesInput);
        }
    }
}