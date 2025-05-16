
namespace campusLove.Domain.Entities;

public class Coincidencias
{
    public int id {get; set;}
    public Usuarios? id_usuario1 {get; set;}
    public Usuarios? id_usuario2 {get; set;}
    public DateTime FechaMatch {get; set;} = DateTime.Now;
}
