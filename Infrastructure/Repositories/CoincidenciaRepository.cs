using MySql.Data.MySqlClient;
using CampusLove.Domain.Ports;
using CampusLove.Domain.Entities;
using CampusLove.Repositories;

namespace CampusLove.Infrastructure.Repositories
{
    public class CoincidenciaRepository : IGenericRepository<Coincidencias>, ICoincidenciaRepository
    {
        private readonly MySqlConnection _connection;
        private readonly IUsuarioRepository _usuarioRepository;

        public CoincidenciaRepository(MySqlConnection connection, IUsuarioRepository usuarioRepository)
        {
            _connection = connection;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Coincidencias?> GetByIdAsync(object id)
        {
            const string query = @"SELECT c.id, c.FechaMatch, 
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
                    FechaMatch = DateTime.Parse(reader["FechaMatch"].ToString() ?? string.Empty),
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
    string query = @"
        SELECT 
  ...,
  c1.Nombre AS carrera1,
  c2.Nombre AS carrera2,
  g1.Nombre AS genero1,
  g2.Nombre AS genero2
FROM coincidencias c
JOIN usuarios u1 ON c.id_usuario1 = u1.id
JOIN usuarios u2 ON c.id_usuario2 = u2.id
LEFT JOIN carreras c1 ON u1.id_carrera = c1.id
LEFT JOIN carreras c2 ON u2.id_carrera = c2.id
LEFT JOIN generos g1 ON u1.id_genero = g1.id
LEFT JOIN generos g2 ON u2.id_genero = g2.id
";

    using var command = new MySqlCommand(query, _connection);
    var coincidenciasList = new List<Coincidencias>();

    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        var usuario1 = new Usuarios
        {
            id = Convert.ToInt32(reader["id_usuario1"]),
            nombre = reader["nombre1"].ToString() ?? string.Empty,
            edad = Convert.ToInt32(reader["edad1"]),
            FrasePerfil = reader["FrasePerfil1"].ToString() ?? string.Empty,
            login = reader["login1"].ToString() ?? string.Empty,
            Password = reader["Password1"].ToString() ?? string.Empty,
            idCarrera = new Carrera { Nombre = reader["carrera1"].ToString() ?? string.Empty },  
            idGenero = new Genero { Nombre = reader["genero1"].ToString() ?? string.Empty }     
        };

        var usuario2 = new Usuarios
        {
            id = Convert.ToInt32(reader["id_usuario2"]),
            nombre = reader["nombre2"].ToString() ?? string.Empty,
            edad = Convert.ToInt32(reader["edad2"]),
            FrasePerfil = reader["FrasePerfil2"].ToString() ?? string.Empty,
            login = reader["login2"].ToString() ?? string.Empty,
            Password = reader["Password2"].ToString() ?? string.Empty,
            idCarrera = new Carrera { Nombre = reader["carrera2"].ToString() ?? string.Empty },  
            idGenero = new Genero { Nombre = reader["genero2"].ToString() ?? string.Empty }      
        };

        coincidenciasList.Add(new Coincidencias
        {
            id = Convert.ToInt32(reader["id"]),
            FechaMatch = reader["FechaMatch"] != DBNull.Value ? Convert.ToDateTime(reader["FechaMatch"]) : DateTime.MinValue,
            id_usuario1 = usuario1,
            id_usuario2 = usuario2
        });
    }

    return coincidenciasList;
}

        public async Task<bool> InsertAsync(int id, Coincidencias coincidencias)
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

        public async Task InsertCoincidenciaAsync(int id, Coincidencias coincidencias)
        {
            const string query = @"
                INSERT INTO coincidencias (id_usuario1, id_usuario2, FechaMatch) 
                VALUES (@id_usuario1, @id_usuario2, CURRENT_TIMESTAMP)";
            
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id_usuario1", coincidencias.id_usuario1?.id ?? 0);
            command.Parameters.AddWithValue("@id_usuario2", coincidencias.id_usuario2?.id ?? 0);

            await command.ExecuteNonQueryAsync();
        }

       public async Task<IEnumerable<Coincidencias>> GetMatchesByUsuarioAsync(int usuarioId)
{
    string query = @"
        SELECT 
            c.id,
            c.FechaMatch,
            c.id_usuario1,
            c.id_usuario2
        FROM coincidencias c
        WHERE c.id_usuario1 = @usuarioId OR c.id_usuario2 = @usuarioId
        ORDER BY c.FechaMatch DESC";

    using var command = new MySqlCommand(query, _connection);
    command.Parameters.AddWithValue("@usuarioId", usuarioId);

    // Paso 1: Guarda los datos crudos
    var datosTemporales = new List<(int id, DateTime fechaMatch, int idUsuario1, int idUsuario2)>();

    using (var reader = await command.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
        {
            var id = Convert.ToInt32(reader["id"]);
            var fecha = reader["FechaMatch"] != DBNull.Value ? Convert.ToDateTime(reader["FechaMatch"]) : DateTime.MinValue;
            var idU1 = Convert.ToInt32(reader["id_usuario1"]);
            var idU2 = Convert.ToInt32(reader["id_usuario2"]);
            datosTemporales.Add((id, fecha, idU1, idU2));
        }
    }

    // Paso 2: Carga los usuarios ahora que el reader está cerrado
    var coincidenciasList = new List<Coincidencias>();
    foreach (var item in datosTemporales)
    {
        var usuario1 = await _usuarioRepository.GetByIdAsync(item.idUsuario1);
        var usuario2 = await _usuarioRepository.GetByIdAsync(item.idUsuario2);

        coincidenciasList.Add(new Coincidencias
        {
            id = item.id,
            FechaMatch = item.fechaMatch,
            id_usuario1 = usuario1,
            id_usuario2 = usuario2
        });
    }

    return coincidenciasList;
}

    }
}