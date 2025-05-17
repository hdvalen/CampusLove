
using campusLove.Domain.Entities;
using CampusLove.Domain.Ports;



namespace CampusLove.Application.UI
{
    public class MenuRegistro
    {
        public readonly IGeneroRepository _generoRepository;

        public MenuRegistro(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;

        }

        public void MostrarMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  📝 REGISTRO DE USUARIO                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();

       
        }
        public void RegistrarUsuario()
        {

            // Usa el repositorio inyectado en lugar de crear uno nuevo
            var generoRepository = _generoRepository;

            string nombre = MenuRegistro.ReadText("📛 Nombre: ");
            int edad = MenuRegistro.ReadInt("🎂 Edad: ");
            while (!int.TryParse(Console.ReadLine(), out edad))
            {
                Console.Write("⚠️ Edad no válida. Ingrese un número: ");
            }
            string frasePerfil = MenuRegistro.ReadText("📝 Frase Perfil: ");
            //Mostrar generos


            Console.WriteLine(" 👤 Seleccione un género:");
            var generos = generoRepository.ObtenerTodos();
            foreach (var genero in generos)
            {
                Console.WriteLine($"  {genero.Id} - {genero.Nombre}");
            }
            Console.Write("Seleccione el número del género: ");
            int idGenero;
            while (!int.TryParse(Console.ReadLine(), out idGenero) || generos.All(g => (g as Genero)?.Id != idGenero))
            {
                Console.Write("⚠️ Género no válido. Ingrese un número: ");
            }


            Console.Write(" ");

            Console.Write("=======================================================\n");

            Console.Write("📛 Nombre de usuario: ");
            string? nombreUsuario = Console.ReadLine();

            Console.Write("🔐 Contraseña: ");
            string? contrasena = Console.ReadLine();



            var usuario = new Usuarios
            {
                nombre = nombre,
                edad = edad,
                FrasePerfil = frasePerfil,
                Password = contrasena,
                login = nombreUsuario
            };
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Registro exitoso. ¡Bienvenido/a a Campus Love!");
            Console.ResetColor();

            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        private static int ReadInt(string v)
        {
            throw new NotImplementedException();
        }

        private static string ReadText(string v)
        {
            throw new NotImplementedException();
        }
    }
}
