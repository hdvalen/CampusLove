
using CampusLove.Domain.Entities;
using CampusLove.Repositories;

namespace CampusLove.Domain.Ports;

public interface IUsuarioRepository : IGenericRepository<Usuarios>
{
    Task<object> AgregarLikeAsync(int id1, object id2);
    Task<object> ExisteLikeMutuoAsync(int id1, object id2);
}