
namespace campusLove.Domain.Entities;

public class Usuarios
{
    public static string Carrera { get; internal set; }
    public int id {get; set;}
    public string? nombre {get; set;}
    public int edad {get; set;}
    public string? login {get; set;}
    public string? Password {get; set;}
    public string FrasePerfil {get; set;}

}