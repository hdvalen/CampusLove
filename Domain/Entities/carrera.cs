

namespace campusLove.Domain.Entities;

public class Carrera
{
    public int IdCarrera { get; set; }
    public string Nombre { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
}
