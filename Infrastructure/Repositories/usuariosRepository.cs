
using campusLove.Domain.Entities;
using campusLove.Domain.Ports;
using MiAppHexagonal.Infrastructure.Mysql;


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

    public void Crear(usuarios producto)
    {
        // var connection = _conexion.ObtenerConexion();
        // string query = "INSERT INTO productos (nombre, stock) VALUES (@nombre, @stock)";
        // using var cmd = new MySqlCommand(query, connection);
        // cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
        // cmd.Parameters.AddWithValue("@stock", producto.Stock);
        // cmd.ExecuteNonQuery(); 
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