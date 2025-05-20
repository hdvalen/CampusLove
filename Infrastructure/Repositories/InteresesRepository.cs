using campusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Repositories;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CampusLove.Infrastructure.Repositories
{
    public class InteresesRepository : IGenericRepository<Intereses>, IInteresesRepository
    {
        private readonly MySqlConnection _connection;

        public InteresesRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Intereses>> GetAllAsync()
        {
            var intereses = new List<Intereses>();
            string query = "SELECT id, nombre FROM intereses";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                intereses.Add(new Intereses
                {
                    id = Convert.ToInt32(reader.GetInt32("id")),
                    nombre = reader["nombre"].ToString() ?? string.Empty
                });
            }

            await _connection.CloseAsync();
            return intereses;
        }

        public async Task<Intereses?> GetByIdAsync(object id)
        {
            Intereses? interes = null;
            string query = "SELECT id, nombre FROM intereses WHERE id = @id";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
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

            await _connection.CloseAsync();
            return interes;
        }

        public async Task<bool> InsertAsync(int id, Intereses entity)
        {
            string query = "INSERT INTO intereses (nombre) VALUES (@nombre)";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@nombre", entity.nombre);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAsync(Intereses entity)
        {
            string query = "UPDATE intereses SET nombre = @nombre WHERE id = @id";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@nombre", entity.nombre);
            cmd.Parameters.AddWithValue("@id", entity.id);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            string query = "DELETE FROM intereses WHERE id = @id";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();

            return rowsAffected > 0;
        }
    }
}
