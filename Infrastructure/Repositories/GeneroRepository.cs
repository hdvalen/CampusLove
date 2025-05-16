using campusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using MySql.Data.MySqlClient;

namespace CampusLove.Infrastructure.Repositories
{
    public class GeneroRepository : IGenericRepository<Genero>, IGeneroRepository
    {
        private readonly MySqlConnection _connection;

        public GeneroRepository(MySqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<IEnumerable<Genero>> GetAllAsync()
        {
            var generoList = new List<Genero>();
            const string query = "SELECT id, nombre FROM genero";

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                generoList.Add(new Genero
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nombre = reader["nombre"].ToString() ?? string.Empty,
                });
            }

            return generoList;
        }

        public async Task<Genero?> GetByIdAsync(object id)
        {
            const string query = "SELECT id, nombre FROM genero WHERE id = @Id";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Genero
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nombre = reader["nombre"].ToString() ?? string.Empty,
                };
            }

            return null;
        }

        public async Task CrearAsync(Genero entidad)
        {
            const string query = "INSERT INTO genero (nombre) VALUES (@nombre)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nombre", entidad.Nombre);
            await command.ExecuteNonQueryAsync();
        }

        public async Task ActualizarAsync(Genero entidad)
        {
            const string query = "UPDATE genero SET nombre = @nombre WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nombre", entidad.Nombre);
            command.Parameters.AddWithValue("@id", entidad.Id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task EliminarAsync(object id)
        {
            const string query = "DELETE FROM genero WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
        }

        public List<Genero> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public void Crear(Genero entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Genero entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }
    }
}
