
using campusLove.Domain.Entities;
using CampusLove.Domain.Ports;
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


        public async Task<IEnumerable<Usuarios>> GetAllAsync()
        {
            var userList = new List<Usuarios>();
            const string query = "SELECT id, nombre, edad, FrasePerfil, login, Password, idCarrera, nombreCarrera, idGenero, nombreGenero FROM usuarios";

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
                        Id = Convert.ToInt32(reader["idCarrera"]),
                        Nombre = reader["nombreCarrera"].ToString() ?? string.Empty
                    },
                    idGenero = new Genero
                    {
                        Id = Convert.ToInt32(reader["idGenero"]),
                        Nombre = reader["nombreGenero"].ToString() ?? string.Empty
                    },
                });
            }

            return userList;
        }

        public async Task<Usuarios?> GetByIdAsync(object id)
        {
            const string query = "SELECT  id, nombre, edad, FrasePerfil, login, Password, idCarrera, nombreCarrera, idGenero, nombreGenero  FROM usuarios WHERE id = @Id";

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
                        Id = Convert.ToInt32(reader["idCarrera"]),
                        Nombre = reader["nombreCarrera"].ToString() ?? string.Empty
                    },
                    idGenero = new Genero
                    {
                        Id = Convert.ToInt32(reader["idGenero"]),
                        Nombre = reader["nombreGenero"].ToString() ?? string.Empty
                    },
                };
            }

            return null;
        }

        // Implementación de los métodos de IGenericRepository<Usuarios>
        public IEnumerable<Usuarios> ObtenerTodos()
        {
            
            return GetAllAsync().Result;
        }

        public void Crear(Usuarios usuario)
        {
            const string query = "INSERT INTO usuarios (nombre, edad, FrasePerfil, login, Password, idCarrera, nombreCarrera, idGenero, nombreGenero) VALUES (@nombre, @edad, @FrasePerfil, @login, @Password, @idCarrera, @nombreCarrera, @idGenero, @nombreGenero)";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@nombre", usuario.nombre);
            command.Parameters.AddWithValue("@edad", usuario.edad);
            command.Parameters.AddWithValue("@FrasePerfil", usuario.FrasePerfil);
            command.Parameters.AddWithValue("@login", usuario.login);
            command.Parameters.AddWithValue("@Password", usuario.Password);
            command.Parameters.AddWithValue("@idCarrera", usuario.idCarrera?.Id ?? 0);
            command.Parameters.AddWithValue("@nombreCarrera", usuario.idCarrera?.Nombre ?? string.Empty);
            command.Parameters.AddWithValue("@idGenero", usuario.idGenero?.Id ?? 0);
            command.Parameters.AddWithValue("@nombreGenero", usuario.idGenero?.Nombre ?? string.Empty);
            command.ExecuteNonQuery();
        }

        public void Actualizar(Usuarios usuario)
        {
            const string query = "UPDATE usuarios SET nombre = @nombre, edad = @edad, FrasePerfil = @FrasePerfil, login = @login, Password = @Password, idCarrera = @idCarrera, nombreCarrera = @nombreCarrera, idGenero = @idGenero, nombreGenero = @nombreGenero WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", usuario.id);
            command.Parameters.AddWithValue("@nombre", usuario.nombre);
            command.Parameters.AddWithValue("@edad", usuario.edad);
            command.Parameters.AddWithValue("@FrasePerfil", usuario.FrasePerfil);
            command.Parameters.AddWithValue("@login", usuario.login);
            command.Parameters.AddWithValue("@Password", usuario.Password);
            command.Parameters.AddWithValue("@idCarrera", usuario.idCarrera?.Id ?? 0);
            command.Parameters.AddWithValue("@nombreCarrera", usuario.idCarrera?.Nombre ?? string.Empty);
            command.Parameters.AddWithValue("@idGenero", usuario.idGenero?.Id ?? 0);
            command.Parameters.AddWithValue("@nombreGenero", usuario.idGenero?.Nombre ?? string.Empty);
            command.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            const string query = "DELETE FROM usuarios WHERE id = @id";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        List<Usuarios> IGenericRepository<Usuarios>.ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}