using CampusLove.Domain.Ports;
using CampusLove.Domain.Factory;
using CampusLove.Infrastructure.Repositories;

namespace CampusLove.Infrastructure.Mysql;

public class MySqlDbFactory : IDbFactory
{
    private readonly string _connectionString;

    public MySqlDbFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IUsuarioRepository CrearUsuarioRepository()
    {
        var connection = ConexionSingleton.Instancia(_connectionString).ObtenerConexion();
        return new UsuarioRepository(connection);
    }

    public IGeneroRepository CrearGeneroRepository()
    {
        var connection = ConexionSingleton.Instancia(_connectionString).ObtenerConexion();
        return new GeneroRepository(connection);
    }
}

