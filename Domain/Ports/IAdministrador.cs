using CampusLove.Domain.Entities;
using CampusLove.Repositories;

namespace CampusLove.Domain.Ports
{
    public interface IAdministradorRepository : IGenericRepository<Administrador>
    {
        Task<Administrador?> GetByUsuarioAsync(string usuario);
    }
}
