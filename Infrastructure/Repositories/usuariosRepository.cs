
using campusLove.Data;
using campusLove.Domain.Entities;


namespace campusLove.Infrastructure.Repositories;

public class ProductoRepository : IGenericRepository<Usuarios>
{
    public async Task<IEnumerable<Usuarios>> GetAllAsync()
    {
        List<Usuarios> usuarios = new List<Usuarios>();

        using(var db = new DataBase())
        {
            var connection = db.Connection;
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Usuarios";
                using(var reader = await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var usuario = new Usuarios
                        {
                            id = Convert.ToInt32(reader["id"]),
                            nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            edad = reader.GetInt32(reader.GetOrdinal("Edad")),
                            FrasePerfil = reader.GetString(reader.GetOrdinal("FrasePerfil")),
                            CreditosDisponibles = reader.GetInt32(reader.GetOrdinal("CreditosDisponibles")),
                            login = reader.GetString(reader.GetOrdinal("Login")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            idCarrera = reader.IsDBNull(reader.GetOrdinal("IdCarrera")) ? null : new Carrera { Id = reader.GetInt32(reader.GetOrdinal("IdCarrera")) },
                            idGenero = reader.IsDBNull(reader.GetOrdinal("IdGenero")) ? null : new Genero { Id = reader.GetInt32(reader.GetOrdinal("IdGenero")) }
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
        }
        return usuarios;
    }
    public async Task<bool> CreateAsync(Usuarios entity)
    {
        using(var db = new DataBase())
        {
            var connection = db.Connection;
            using(var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Usuarios (Nombre, Edad,) VALUES (@Nombre, @Edad)";
                command.Parameters.AddWithValue("@Nombre", entity.nombre);
                command.Parameters.AddWithValue("@Edad", entity.edad);
                command.Parameters.AddWithValue("@IdCarrera", entity.idCarrera?.Id);
                command.Parameters.AddWithValue("@IdGenero", entity.idGenero?.Id);
                command.Parameters.AddWithValue("@FrasePerfil", entity.FrasePerfil);
                command.Parameters.AddWithValue("@CreditosDisponibles", entity.CreditosDisponibles);
                command.Parameters.AddWithValue("@Login", entity.login);
                command.Parameters.AddWithValue("@Password", entity.Password);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}

public interface IGenericRepository<T>
{
}