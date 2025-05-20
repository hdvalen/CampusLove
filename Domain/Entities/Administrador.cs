using System;

namespace CampusLove.Domain.Entities
{
    public class Administrador
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        public int NivelAcceso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimoAcceso { get; set; }
        public bool Activo { get; set; }
    }
}