
using MySql.Data.MySqlClient;
using CampusLove.Domain.Ports;
using campusLove.Domain.Entities;
using CampusLove.Repositories;

namespace CampusLove.Infrastructure.Repositories
{
    public class CoincidenciaRepository : IGenericRepository<Coincidencias>, ICoincidenciaRepository
    {
        private readonly MySqlConnection _connection;

        public CoincidenciaRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Coincidencias?> GetByIdAsync(object id)
        {
            const string query = @"SELECT c.id, c.fecha_match, 
        u1.id AS id_usuario1, u1.nombre AS nombre1, u1.edad AS edad1, u1.FrasePerfil AS FrasePerfil1, u1.login AS login1, u1.Password AS Password1,
        u2.id AS id_usuario2, u2.nombre AS nombre2, u2.edad AS edad2, u2.FrasePerfil AS FrasePerfil2, u2.login AS login2, u2.Password AS Password2
        FROM coincidencias c
        JOIN usuarios u1 ON c.id_usuario1 = u1.id
        JOIN usuarios u2 ON c.id_usuario2 = u2.id
        WHERE c.id = @id
";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Coincidencias
                {
                    id = Convert.ToInt32(reader["id"]),
                    FechaMatch = DateTime.Parse(reader["fecha_match"].ToString() ?? string.Empty),
                    id_usuario1 = new Usuarios
                    {
                        id = Convert.ToInt32(reader["id_usuario1"]),
                        nombre = reader["nombre"].ToString() ?? string.Empty,
                        edad = Convert.ToInt32(reader["edad"]),
                        FrasePerfil = reader["FrasePerfil"].ToString() ?? string.Empty,
                        login = reader["login"].ToString() ?? string.Empty,
                        Password = reader["Password"].ToString() ?? string.Empty,
                    },

                    id_usuario2 = new Usuarios
                    {
                        id = Convert.ToInt32(reader["id_usuario2"]),
                        nombre = reader["nombre"].ToString() ?? string.Empty,
                        edad = Convert.ToInt32(reader["edad"]),
                        FrasePerfil = reader["FrasePerfil"].ToString() ?? string.Empty,
                        login = reader["login"].ToString() ?? string.Empty,
                        Password = reader["Password"].ToString() ?? string.Empty,
                    }
                };
            }

            return null;
        }
         public async Task<IEnumerable<Coincidencias>> GetAllAsync()
        {
            const string query = "SELECT c.id, c.fecha_match, u1.id AS id_usuario1, u1.nombre AS nombre1, u1.edad AS edad1, u1.FrasePerfil AS FrasePerfil1, u1.login AS login1, u1.Password AS Password1, u2.id AS id_usuario2, u2.nombre AS nombre2, u2.edad AS edad2, u2.FrasePerfil AS FrasePerfil2, u2.login AS login2, u2.Password AS Password2 FROM coincidencias c JOIN usuarios u1 ON c.id_usuario1 = u1.id JOIN usuarios u2 ON c.id_usuario2 = u2.id";
            using var command = new MySqlCommand(query, _connection);
            var coincidenciasList = new List<Coincidencias>();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                coincidenciasList.Add(new Coincidencias
                {
                    id = Convert.ToInt32(reader["id"]),
                    FechaMatch = reader["fecha_match"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_match"]) : DateTime.MinValue,
                    id_usuario1 = new Usuarios
                    {
                        id = Convert.ToInt32(reader["id_usuario1"]),
                        nombre = reader["nombre1"].ToString() ?? string.Empty,
                        edad = Convert.ToInt32(reader["edad1"]),
                        FrasePerfil = reader["FrasePerfil1"].ToString() ?? string.Empty,
                        login = reader["login1"].ToString() ?? string.Empty,
                        Password = reader["Password1"].ToString() ?? string.Empty,
                    },
                    id_usuario2 = new Usuarios
                    {
                        id = Convert.ToInt32(reader["id_usuario2"]),
                        nombre = reader["nombre2"].ToString() ?? string.Empty,
                        edad = Convert.ToInt32(reader["edad2"]),
                        FrasePerfil = reader["FrasePerfil2"].ToString() ?? string.Empty,
                        login = reader["login2"].ToString() ?? string.Empty,
                        Password = reader["Password2"].ToString() ?? string.Empty,
                    }
                });
            }

            return coincidenciasList;
        }
        public async Task<bool> InsertAsync(Coincidencias coincidencias)
        {
            const string query = "INSERT INTO coincidencias (id_usuario1, id_usuario2) VALUES (@id_usuario1, @id_usuario2)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id_usuario1", coincidencias.id_usuario1 != null ? coincidencias.id_usuario1.id : 0);
            command.Parameters.AddWithValue("@id_usuario2", coincidencias.id_usuario2 != null ? coincidencias.id_usuario2.id : 0);

            return await command.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Coincidencias coincidencias)
        {
            const string query = "UPDATE coincidencias SET id_usuario1 = @id_usuario1, id_usuario2 = @id_usuario2 WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", coincidencias.id);
            command.Parameters.AddWithValue("@id_usuario1", coincidencias.id_usuario1 != null ? coincidencias.id_usuario1.id : 0);
            command.Parameters.AddWithValue("@id_usuario2", coincidencias.id_usuario2 != null ? coincidencias.id_usuario2.id : 0);

            return await command.ExecuteNonQueryAsync() > 0;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            const string query = "DELETE FROM coincidencias WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);

            return await command.ExecuteNonQueryAsync() > 0;
        }

       
    }
}