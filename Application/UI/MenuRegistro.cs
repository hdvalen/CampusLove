
using CampusLove.Domain.Ports;
using CampusLove.Infrastructure.Repositories;
using MySql.Data.MySqlClient;


namespace CampusLove.Application.UI
{
    public class MenuRegistro
    {
        public readonly IGeneroRepository _generoRepository;
        public readonly ICarreraRepository _carreraRepository;
        public readonly IUsuarioRepository _usuarioRepository;

        public MenuRegistro(MySqlConnection connection)
        {
            _generoRepository = new GeneroRepository(connection);
            _carreraRepository = new CarreraRepository(connection);
            _usuarioRepository = new UsuarioRepository(connection);
        }

        public void RegistrarUsuario()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  📝 REGISTRO DE USUARIO                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            // Usa el repositorio inyectado en lugar de crear uno nuevo
            var generoRepository = _generoRepository;
            var carreraRepository = _carreraRepository;

            string nombre = MenuPrincipal.ReadText("📛 Nombre "); 
            int edad = MenuPrincipal.ReadInt("🎂 Edad ");
            string frasePerfil = MenuPrincipal.ReadText("📝 Frase Perfil ");

            //Mostrar generos
            Console.WriteLine(" 👤 Seleccione un género:");
            var generos = generoRepository.GetAllAsync().Result.ToList();
            for (int i = 0; i < generos.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {generos[i].Nombre}");
            }
            Console.Write("Seleccione el número del género: ");
            int generoSeleccionado;
            while (!int.TryParse(Console.ReadLine(), out generoSeleccionado) || generoSeleccionado < 1 || generoSeleccionado > generos.Count)
            {
                Console.Write("⚠️ Selección no válida. Ingrese un número válido: ");
            }
            Console.Write(" ");
            //Mostrar carreras
            Console.WriteLine(" 🎓 Seleccione una carrera:");
            var carreras = carreraRepository.GetAllAsync().Result.ToList();
            for (int i = 0; i < carreras.Count; i++)
            {
                Console.WriteLine($" {i + 1}. {carreras[i].Nombre}");
            }
            Console.Write("Seleccione el número de la carrera: ");
            int carreraSeleccionada;
            while (!int.TryParse(Console.ReadLine(), out carreraSeleccionada) || carreraSeleccionada < 1 || carreraSeleccionada > carreras.Count)
            {
                Console.Write("⚠️ Selección no válida. Ingrese un número válido: ");
            }

            Console.Write(" ");

            Console.Write("=======================================================\n");

            Console.Write("📛 Nombre de usuario: ");
            string? nombreUsuario = Console.ReadLine();

            Console.Write("🔐 Contraseña: ");
            string? contrasena = LeerContraseñaConAsteriscos();
            var usuario = new Usuarios
            {
                nombre = nombre,
                edad = edad,
                FrasePerfil = frasePerfil,
                Password = contrasena,
                login = nombreUsuario,
                idCarrera = carreras[carreraSeleccionada - 1],
                idGenero = generos[generoSeleccionado - 1]
            };
            var resultado = _usuarioRepository.InsertAsync(usuario).Result;

            if ((bool)resultado)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n✅ Registro exitoso. ¡Bienvenido/a a Campus Love!");
                Console.ResetColor();
                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
                Console.ReadKey();
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Error al registrar el usuario. Intente de nuevo.");
                Console.ResetColor();
            }

            
        }
        public static string LeerContraseñaConAsteriscos()
{
    string contraseña = string.Empty;
    ConsoleKeyInfo tecla;

    do
    {
        tecla = Console.ReadKey(intercept: true); // No muestra la tecla en consola

        if (tecla.Key == ConsoleKey.Backspace && contraseña.Length > 0)
        {
            // Eliminar último carácter
            contraseña = contraseña[..^1];
            Console.Write("\b \b"); // Borra un asterisco de la consola
        }
        else if (!char.IsControl(tecla.KeyChar))
        {
            contraseña += tecla.KeyChar;
            Console.Write("*"); // Muestra un asterisco
        }

    } while (tecla.Key != ConsoleKey.Enter);

    Console.WriteLine(); // Salto de línea al terminar
    return contraseña;
}

    }
}
