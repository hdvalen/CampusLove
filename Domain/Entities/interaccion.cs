namespace CampusLove.Domain.Entities 
{
    public class Interacciones
    {
        public int Id { get; set; }
        public Usuarios? Id_deUsuario { get; set; }
        public Usuarios? Id_paraUsuario { get; set; }
        public char Tipo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string? Descripcion { get; set; }
    }
}

