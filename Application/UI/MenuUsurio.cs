using System.Threading.Tasks;
using CampusLove.Domain.Entities;
using CampusLove.Infrastructure.Repositories;
using MySql.Data.MySqlClient;

namespace CampusLove.Application.UI
{
    public class MenuUsurio
    {
        private readonly UsuarioRepository _usuarioRepository;
        private MenuPerfil? _menuPerfil;

        public MenuUsurio(MySqlConnection connection)
        {
            _usuarioRepository = new UsuarioRepository(connection);
        }

        public async Task IniciarSesion()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 🔐 INICIAR SESIÓN                      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("📛 Nombre de usuario: ");
            string? nombreUsuario = Console.ReadLine();

            Console.Write("🔐 Contraseña: ");
            string? contrasena = Console.ReadLine();

            // Buscar usuario en la base de datos
            var usuarios = _usuarioRepository.GetAllAsync().Result;
            var usuario = usuarios.FirstOrDefault(u =>
                u.login == nombreUsuario && u.Password == contrasena);

            if (usuario != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ ¡Bienvenido/a {usuario.login}! Has iniciado sesión correctamente.");
                Console.ResetColor();
                _menuPerfil = new MenuPerfil(usuario, _usuarioRepository, new CoincidenciaRepository(_usuarioRepository.GetConnection(), _usuarioRepository));
                await _menuPerfil.MostrarMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Usuario o contraseña incorrectos. Intente de nuevo.");
                Console.ResetColor();
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
