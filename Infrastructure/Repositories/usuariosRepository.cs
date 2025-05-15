
using campusLove.Domain.Entities;
using campusLove.Domain.Ports;
using campusLove.Infrastructure.Mysql;

namespace campusLove.Infrastructure.Repositories;

public class ProductoRepository : IGenericRepository<usuarios>, IUsuarioRepository
{
    private readonly ConexionSingleton _conexion;

    public ProductoRepository(string connectionString)
    {
        _conexion = ConexionSingleton.Instancia(connectionString);
    }

    public void Actualizar(usuarios entity)
    {
        throw new NotImplementedException();
    }

    public void Crear(usuarios usuario)
    {
        var connection = _conexion.ObtenerConexion();
        string query = "INSERT INTO usuarios  (nombre, edad) VALUES (@nombre, @edad)";
        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
        cmd.Parameters.AddWithValue("@stock", usuario.edad);
        cmd.ExecuteNonQuery(); 
    }

    public void Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public List<usuarios> ObtenerTodos()
    {
        throw new NotImplementedException();
    }
}