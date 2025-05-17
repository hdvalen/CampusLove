

using CampusLove.Domain.Entities;

public class Usuarios
{
    public int id { get; set; }
    public  Carrera? idCarrera { get; set; }
    public Genero? idGenero { get; set; }
    public string? nombre { get; set; }
    public int edad { get; set; }
    public string? login { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? FrasePerfil { get; set; } = string.Empty;
    public int CreditosDisponibles { get; set; } = 10;

}