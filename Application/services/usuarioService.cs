
using campusLove.Domain.Ports;
using campusLove.Domain.Entities;


namespace campusLove.Application.Services
{

    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }
        public void CrearUsuario(usuarios usuario)
        {
            _repo.Crear(usuario);
        }
    }
}