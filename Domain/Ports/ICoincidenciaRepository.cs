using CampusLove.Domain.Entities;
using CampusLove.Repositories;

namespace CampusLove.Domain.Ports;

public interface ICoincidenciaRepository : IGenericRepository<Coincidencias>
{
    Task InsertCoincidenciaAsync(int v, Coincidencias coincidencias);
    Task<IEnumerable<Coincidencias>> GetMatchesByUsuarioAsync(int usuarioId);
}
