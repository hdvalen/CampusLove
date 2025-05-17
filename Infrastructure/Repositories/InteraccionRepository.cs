using System;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using MySql.Data.MySqlClient;

namespace CampusLove.Infrastructure.Repositories
{
    public class InteraccionRepository : IInteraccionRepository<Interacciones>
    {
        private readonly MySqlConnection _connection;

        public InteraccionRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Interacciones>> GetAllAsync()
        {
            var list = new List<Interacciones>();
            const string query = "SELECT id, tipo, fecha, descripcion FROM interacciones";

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new Interacciones
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Tipo = Convert.ToChar(reader["tipo"]),
                    Fecha = Convert.ToDateTime(reader["fecha"]),
                    Descripcion = reader["descripcion"]?.ToString()
                });
            }

            return list;
        }

        public async Task<Interacciones?> GetByIdAsync(object id)
        {
            const string query = "SELECT id, tipo, fecha, descripcion FROM interacciones WHERE id = @Id";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Interacciones
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Tipo = Convert.ToChar(reader["tipo"]),
                    Fecha = Convert.ToDateTime(reader["fecha"]),
                    Descripcion = reader["descripcion"]?.ToString()
                };
            }

            return null;
        }

        public async Task<bool> InsertAsync(Interacciones interaccion)
        {
            const string query = "INSERT INTO interacciones (tipo, fecha, descripcion) VALUES (@Tipo, @Fecha, @Descripcion)";

            using var transaction = await _connection.BeginTransactionAsync();
            try
            {
                using var command = new MySqlCommand(query, _connection, transaction);
                command.Parameters.AddWithValue("@Tipo", interaccion.Tipo);
                command.Parameters.AddWithValue("@Fecha", interaccion.Fecha);
                command.Parameters.AddWithValue("@Descripcion", interaccion.Descripcion);

                var result = await command.ExecuteNonQueryAsync() > 0;
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Interacciones interaccion)
        {
            const string query = "UPDATE interacciones SET tipo = @Tipo, fecha = @Fecha, descripcion = @Descripcion WHERE id = @Id";

            using var transaction = await _connection.BeginTransactionAsync();
            try
            {
                using var command = new MySqlCommand(query, _connection, transaction);
                command.Parameters.AddWithValue("@Tipo", interaccion.Tipo);
                command.Parameters.AddWithValue("@Fecha", interaccion.Fecha);
                command.Parameters.AddWithValue("@Descripcion", interaccion.Descripcion);
                command.Parameters.AddWithValue("@Id", interaccion.Id);

                var result = await command.ExecuteNonQueryAsync() > 0;
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            const string query = "DELETE FROM interacciones WHERE id = @Id";

            using var transaction = await _connection.BeginTransactionAsync();
            try
            {
                using var command = new MySqlCommand(query, _connection, transaction);
                command.Parameters.AddWithValue("@Id", id);

                var result = await command.ExecuteNonQueryAsync() > 0;
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
