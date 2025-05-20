using System.Collections.Generic;
using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Ports
{
    public interface IAdministrador
    {
        List<Administrador> ObtenerTodosAdministradores();
        
        Administrador ObtenerAdministradorPorId(int id);
        Administrador ObtenerAdministradorPorUsuario(string usuario);
        bool VerificarCredenciales(string usuario, string contrasena);
        bool CrearAdministrador(Administrador administrador);
        bool ActualizarAdministrador(Administrador administrador);
        bool EliminarAdministrador(int id);
        bool CambiarContrasena(int id, string nuevaContrasena);
        
        List<Usuario> ObtenerTodosUsuarios();
    }
}