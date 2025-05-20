namespace CampusLove.Domain.Entities
{
    public class Administrador
    {
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string usuario { get; set; } = string.Empty;
        public string contrasena { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public int nivel_acceso { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime? ultimo_acceso { get; set; }
        public bool activo { get; set; }
    }
}
