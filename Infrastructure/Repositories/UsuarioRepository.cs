using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Repositories;
using MySql.Data.MySqlClient;

namespace CampusLove.Infrastructure.Repositories
{
    public class UsuarioRepository : IGenericRepository<Usuarios>, IUsuarioRepository
    {
        private readonly MySqlConnection _connection;

        public UsuarioRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public MySqlConnection GetConnection()
        {
            return _connection;
        }

        public async Task<IEnumerable<Usuarios>> GetAllAsync()
        {
            var userList = new List<Usuarios>();
            const string query = @"SELECT 
        u.id, u.nombre, u.edad, u.FrasePerfil, u.login, u.Password,
        c.id AS id_carrera, c.nombre AS nombreCarrera,
        g.id AS id_genero, g.nombre AS nombreGenero
        FROM usuarios u
        JOIN carreras c ON u.id_carrera = c.id
        JOIN generos g ON u.id_genero = g.id;";

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                userList.Add(new Usuarios
                {
                    id = Convert.ToInt32(reader["id"]),
                    nombre = reader["nombre"].ToString() ?? string.Empty,
                    edad = Convert.ToInt32(reader["edad"]),
                    FrasePerfil = reader["FrasePerfil"].ToString() ?? string.Empty,
                    login = reader["login"].ToString() ?? string.Empty,
                    Password = reader["Password"].ToString() ?? string.Empty,
                    idCarrera = new Carrera
                    {
                        Id = Convert.ToInt32(reader["id_carrera"]),
                        Nombre = reader["nombreCarrera"].ToString() ?? string.Empty
                    },
                    idGenero = new Genero
                    {
                        Id = Convert.ToInt32(reader["id_genero"]),
                        Nombre = reader["nombreGenero"].ToString() ?? string.Empty
                    },
                });
            }

            return userList;
        }

        public async Task<Usuarios?> GetByIdAsync(object id)
        {
            const string query = "SELECT  id, nombre, edad, FrasePerfil, login, Password, id_carrera, nombreCarrera, id_genero, nombreGenero  FROM usuarios WHERE id = @Id";

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Usuarios
                {
                    id = Convert.ToInt32(reader["id"]),
                    nombre = reader["nombre"].ToString() ?? string.Empty,
                    edad = Convert.ToInt32(reader["edad"]),
                    FrasePerfil = reader["FrasePerfil"].ToString() ?? string.Empty,
                    login = reader["login"].ToString() ?? string.Empty,
                    Password = reader["Password"].ToString() ?? string.Empty,
                    idCarrera = new Carrera
                    {
                        Id = Convert.ToInt32(reader["id_carrera"]),
                        Nombre = reader["nombreCarrera"].ToString() ?? string.Empty
                    },
                    idGenero = new Genero
                    {
                        Id = Convert.ToInt32(reader["id_genero"]),
                        Nombre = reader["nombreGenero"].ToString() ?? string.Empty
                    },
                };
            }

            return null;
        }

        async Task<object> IUsuarioRepository.InsertAsync(Usuarios usuarios)
        {
            const string query = "INSERT INTO usuarios (nombre, edad, FrasePerfil, login, Password, id_carrera, id_genero) VALUES (@nombre, @edad, @FrasePerfil, @login, @Password, @id_carrera, @id_genero)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nombre", usuarios.nombre);
            command.Parameters.AddWithValue("@edad", usuarios.edad);
            command.Parameters.AddWithValue("@FrasePerfil", usuarios.FrasePerfil);
            command.Parameters.AddWithValue("@login", usuarios.login);
            command.Parameters.AddWithValue("@Password", usuarios.Password);
            command.Parameters.AddWithValue("@id_carrera", usuarios.idCarrera?.Id ?? 0);
            command.Parameters.AddWithValue("@id_genero", usuarios.idGenero?.Id ?? 0);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Usuarios usuario)
        {
            const string query = "UPDATE usuarios SET nombre = @nombre, edad = @edad, FrasePerfil = @FrasePerfil, login = @login, Password = @Password, id_carrera = @id_carrera, id_genero = @id_genero WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", usuario.id);
            command.Parameters.AddWithValue("@nombre", usuario.nombre);
            command.Parameters.AddWithValue("@edad", usuario.edad);
            command.Parameters.AddWithValue("@FrasePerfil", usuario.FrasePerfil);
            command.Parameters.AddWithValue("@login", usuario.login);
            command.Parameters.AddWithValue("@Password", usuario.Password);
            command.Parameters.AddWithValue("@id_carrera", usuario.idCarrera?.Id ?? 0);
            command.Parameters.AddWithValue("@id_genero", usuario.idGenero?.Id ?? 0);
            command.Parameters.AddWithValue("@nombreGenero", usuario.idGenero?.Nombre ?? string.Empty);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            const string query = "DELETE FROM usuarios WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            return await command.ExecuteNonQueryAsync() > 0;
        }
        async Task<object> IUsuarioRepository.AgregarLikeAsync(int id1, object id2)
        {
            const string query = "INSERT INTO likes (id_usuario1, id_usuario2) VALUES (@id_usuario1, @id_usuario2)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id_usuario1", id1);
            command.Parameters.AddWithValue("@id_usuario2", id2);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        async Task<object> IUsuarioRepository.ExisteLikeMutuoAsync(int id1, object id2)
        {
            const string query = "SELECT COUNT(*) FROM likes WHERE id_usuario1 = @id_usuario1 AND id_usuario2 = @id_usuario2";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id_usuario1", id1);
            command.Parameters.AddWithValue("@id_usuario2", id2);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }
        async Task<bool> IUsuarioRepository.ExisteLikeAsync(int id1, int id2)
        {
            const string query = "SELECT COUNT(*) FROM likes WHERE id_usuario1 = @id_usuario1 AND id_usuario2 = @id_usuario2";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id_usuario1", id1);
            command.Parameters.AddWithValue("@id_usuario2", id2);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        async Task<object> IUsuarioRepository.SaveLikeAsync(int id1, int id2)
        {
            const string query = "INSERT INTO likes (id_usuario1, id_usuario2) VALUES (@id_usuario1, @id_usuario2)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id_usuario1", id1); 
            command.Parameters.AddWithValue("@id_usuario2", id2);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> InsertUsuarioAsync(int id, Usuarios entity)
        {
            const string query = "INSERT INTO usuarios (nombre, edad, FrasePerfil, login, Password, id_carrera, id_genero) VALUES (@nombre, @edad, @FrasePerfil, @login, @Password, @id_carrera, @id_genero)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nombre", entity.nombre);
            command.Parameters.AddWithValue("@edad", entity.edad);
            command.Parameters.AddWithValue("@FrasePerfil", entity.FrasePerfil);
            command.Parameters.AddWithValue("@login", entity.login);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("@id_carrera", entity.idCarrera?.Id ?? 0);
            command.Parameters.AddWithValue("@id_genero", entity.idGenero?.Id ?? 0);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> InsertAsync(int id, Usuarios entity)
        {
           const string query = "INSERT INTO usuarios (nombre, edad, FrasePerfil, login, Password, id_carrera, id_genero) VALUES (@nombre, @edad, @FrasePerfil, @login, @Password, @id_carrera, @id_genero)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nombre", entity.nombre);
            command.Parameters.AddWithValue("@edad", entity.edad);
            command.Parameters.AddWithValue("@FrasePerfil", entity.FrasePerfil);
            command.Parameters.AddWithValue("@login", entity.login);
            command.Parameters.AddWithValue("@Password", entity.Password);
            command.Parameters.AddWithValue("@id_carrera", entity.idCarrera?.Id ?? 0);
            command.Parameters.AddWithValue("@id_genero", entity.idGenero?.Id ?? 0);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        
    }
}