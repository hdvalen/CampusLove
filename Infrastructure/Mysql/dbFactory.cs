using System;

using campusLove.Domain.Factory;
using campusLove.Domain.Ports;
using campusLove.Infrastructure.Repositories;


namespace campusLove.Infrastructure.Mysql;

public class MySqlDbFactory : IDbFactory
{
    private readonly string _connectionString;

    public MySqlDbFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IUsuarioRepository CrearClienteRepository()
    {
        return new usuariosRepository(_connectionString);
    }

}
