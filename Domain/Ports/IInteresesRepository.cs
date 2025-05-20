using CampusLove.Domain.Entities;
using CampusLove.Repositories;
using CampusLoveampusLove.Domain.Entities;

namespace CampusLove.Domain.Ports;

public interface IInteresesRepository : IGenericRepository<Intereses>
{
    Task GetByNombreAsync(string interesInput);
    Task<int> InsertAndReturnIdAsync(Intereses nuevoInteres);
}