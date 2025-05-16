
namespace campusLove.Domain.Entities;

public class Interacciones
{
    public int id {get; set;}
    public Usuarios? id_deUsuario {get; set;}
    public Usuarios? id_paraUsuario {get; set;}
    public char tipo {get; set;}
    public DateTime Fecha {get; set;} = DateTime.Now;
}
