using System;
using campusLove.Domain.Ports;

namespace campusLove.Domain.Factory;

public interface IDbFactory
{
    IUsuarioRepository CrearClienteRepository();
}
    