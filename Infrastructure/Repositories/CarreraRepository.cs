using MySql.Data.MySqlClient;
using CampusLove.Domain.Ports;
using CampusLove.Domain.Entities;
using CampusLove.Repositories;

namespace CampusLove.Infrastructure.Repositories
{
    public class CarreraRepository : IGenericRepository<Carrera>, ICarreraRepository
    {
        private readonly MySqlConnection _connection;

        public CarreraRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Carrera>> GetAllAsync()
        {
            var carreraList = new List<Carrera>();
            const string query = "SELECT id, nombre FROM carreras";

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                carreraList.Add(new Carrera
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nombre = reader["nombre"].ToString() ?? string.Empty,
                });
            }

            return carreraList;
        }

        public async Task<Carrera?> GetByIdAsync(object id)
        {
            const string query = "SELECT id, nombre FROM carreras WHERE id = @Id";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Carrera
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nombre = reader["nombre"].ToString() ?? string.Empty,
                };
            }

            return null;
        }

        public async Task<bool> InsertAsync(int id, Carrera entity)
        {
            const string query = "INSERT INTO carreras (id, nombre) VALUES (@Id, @Nombre)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Nombre", entity.Nombre);

            return await command.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Carrera entity)
        {
            const string query = "UPDATE carreras SET nombre = @Nombre WHERE id = @Id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Nombre", entity.Nombre);

            return await command.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            const string query = "DELETE FROM carreras WHERE id = @Id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        

    }
}