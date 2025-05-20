using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Repositories;
using System.Data;
using MySql.Data.MySqlClient;

namespace CampusLove.Infrastructure.Repositories
{
    public class AdministradorRepository : IGenericRepository<Administrador>, IAdministradorRepository
    {
        private readonly MySqlConnection _connection;

        public AdministradorRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Administrador>> GetAllAsync()
        {
            var lista = new List<Administrador>();
            string query = "SELECT * FROM administradores";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Administrador
                {
                    id = reader.GetInt32("id"),
                    nombre = reader.GetString("nombre"),
                    usuario = reader.GetString("usuario"),
                    contrasena = reader.GetString("contrasena"),
                    correo = reader.GetString("correo"),
                    nivel_acceso = reader.GetInt32("nivel_acceso"),
                    fecha_creacion = reader.GetDateTime("fecha_creacion"),
                    ultimo_acceso = reader.IsDBNull(reader.GetOrdinal("ultimo_acceso")) ? null : reader.GetDateTime("ultimo_acceso"),
                    activo = reader.GetBoolean("activo")
                });
            }

            await _connection.CloseAsync();
            return lista;
        }

        public async Task<Administrador?> GetByIdAsync(object id)
        {
            Administrador? admin = null;
            string query = "SELECT * FROM administradores WHERE id = @id";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                admin = new Administrador
                {
                    id = reader.GetInt32("id"),
                    nombre = reader.GetString("nombre"),
                    usuario = reader.GetString("usuario"),
                    contrasena = reader.GetString("contrasena"),
                    correo = reader.GetString("correo"),
                    nivel_acceso = reader.GetInt32("nivel_acceso"),
                    fecha_creacion = reader.GetDateTime("fecha_creacion"),
                    ultimo_acceso = reader.IsDBNull(reader.GetOrdinal("ultimo_acceso")) ? null : reader.GetDateTime("ultimo_acceso"),
                    activo = reader.GetBoolean("activo")
                };
            }

            await _connection.CloseAsync();
            return admin;
        }

        public async Task<Administrador?> GetByUsuarioAsync(string usuario)
        {
            Administrador? admin = null;
            string query = "SELECT * FROM administradores WHERE usuario = @usuario";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@usuario", usuario);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                admin = new Administrador
                {
                    id = reader.GetInt32("id"),
                    nombre = reader.GetString("nombre"),
                    usuario = reader.GetString("usuario"),
                    contrasena = reader.GetString("contrasena"),
                    correo = reader.GetString("correo"),
                    nivel_acceso = reader.GetInt32("nivel_acceso"),
                    fecha_creacion = reader.GetDateTime("fecha_creacion"),
                    ultimo_acceso = reader.IsDBNull(reader.GetOrdinal("ultimo_acceso")) ? null : reader.GetDateTime("ultimo_acceso"),
                    activo = reader.GetBoolean("activo")
                };
            }

            await _connection.CloseAsync();
            return admin;
        }

        public async Task<bool> InsertAsync(int id, Administrador entity)
        {
            string query = @"
                INSERT INTO administradores (nombre, usuario, contrasena, correo, nivel_acceso, fecha_creacion, ultimo_acceso, activo)
                VALUES (@nombre, @usuario, @contrasena, @correo, @nivel_acceso, @fecha_creacion, @ultimo_acceso, @activo)";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);

            cmd.Parameters.AddWithValue("@nombre", entity.nombre);
            cmd.Parameters.AddWithValue("@usuario", entity.usuario);
            cmd.Parameters.AddWithValue("@contrasena", entity.contrasena);
            cmd.Parameters.AddWithValue("@correo", entity.correo);
            cmd.Parameters.AddWithValue("@nivel_acceso", entity.nivel_acceso);
            cmd.Parameters.AddWithValue("@fecha_creacion", entity.fecha_creacion);
            cmd.Parameters.AddWithValue("@ultimo_acceso", (object?)entity.ultimo_acceso ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", entity.activo);

            int filas = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return filas > 0;
        }

        public async Task<bool> UpdateAsync(Administrador entity)
        {
            string query = @"
                UPDATE administradores 
                SET nombre = @nombre, usuario = @usuario, contrasena = @contrasena, correo = @correo, 
                    nivel_acceso = @nivel_acceso, ultimo_acceso = @ultimo_acceso, activo = @activo 
                WHERE id = @id";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);

            cmd.Parameters.AddWithValue("@id", entity.id);
            cmd.Parameters.AddWithValue("@nombre", entity.nombre);
            cmd.Parameters.AddWithValue("@usuario", entity.usuario);
            cmd.Parameters.AddWithValue("@contrasena", entity.contrasena);
            cmd.Parameters.AddWithValue("@correo", entity.correo);
            cmd.Parameters.AddWithValue("@nivel_acceso", entity.nivel_acceso);
            cmd.Parameters.AddWithValue("@ultimo_acceso", (object?)entity.ultimo_acceso ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", entity.activo);

            int filas = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return filas > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            string query = "DELETE FROM administradores WHERE id = @id";

            await _connection.OpenAsync();
            using var cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", id);

            int filas = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return filas > 0;
        }
    }
}
