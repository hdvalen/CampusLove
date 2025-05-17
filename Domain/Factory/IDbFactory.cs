using CampusLove.Domain.Ports;

namespace CampusLove.Domain.Factory;

public interface IDbFactory
{
   IUsuarioRepository CrearUsuarioRepository();
   IGeneroRepository CrearGeneroRepository();
   ICarreraRepository CrearCarreraRepository();
}

