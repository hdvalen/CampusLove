
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Repositories;
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
            const string query = "SELECT id, nombre FROM generos";
            
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
            const string query = "SELECT id, nombre FROM generos WHERE id = @Id";
            
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
        
        public async Task<bool> InsertAsync(int id, Genero entity)
        {
            const string query = "INSERT INTO generos (id, nombre) VALUES (@Id, @Nombre)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Nombre", entity.Nombre);
            
            return await command.ExecuteNonQueryAsync() > 0;
        }
        
        public async Task<bool> UpdateAsync(Genero entity)
        {
            const string query = "UPDATE generos SET nombre = @Nombre WHERE id = @Id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Nombre", entity.Nombre);
            command.Parameters.AddWithValue("@Id", entity.Id);
            
            return await command.ExecuteNonQueryAsync() > 0;
        }
        
        public async Task<bool> DeleteAsync(object id)
        {
            const string query = "DELETE FROM generos WHERE id = @Id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);
            
            return await command.ExecuteNonQueryAsync() > 0;
        }

    }
}