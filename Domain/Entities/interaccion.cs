
namespace campusLove.Domain.Entities;

public class Interacciones
{
    public int id {get; set;}
    public int id_deUsuario {get; set;}
    public int id_paraUsuario {get; set;}
    public char tipo {get; set;}
    public DateTime Fecha {get; set;}
}
