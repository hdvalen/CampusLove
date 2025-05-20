

using CampusLove.Application.services;



namespace CampusLove.Application.UI
{
    public class MenuPrincipal
    {
        private readonly MenuRegistro _menuRegistro;
        private readonly MenuUsurio _menuUsuario;

        public MenuPrincipal()
        {
            var connection = dbSettings.GetConnection();
            _menuRegistro = new MenuRegistro(connection);
            _menuUsuario = new MenuUsurio(connection);
        }
        public async Task MostrarMenu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 🔑 ACCESO AL SISTEMA                                  ║");
                Console.WriteLine("║   1. 🔐 Iniciar sesión                                ║");
                Console.WriteLine("║   2. 📝 Registrarse                                   ║");
                Console.WriteLine("║   3. 🧑‍💼 Administrador                                 ║");
                Console.WriteLine("║   0. 🚪 Salir                                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ResetColor();
                Console.Write("🡺 Seleccione una opción: ");
                string? opcion = Console.ReadLine();
                

                switch (opcion)
                {
                    case "1":
                        MostrarMensaje("Iniciando sesión, presiona una tecla...", ConsoleColor.Green);
                        Console.Clear();
                       await _menuUsuario.IniciarSesion();
                        break;
                    case "2":
                        MostrarMensaje("Abriendo formulario de registro, presiona una tecla...", ConsoleColor.Green);
                        Console.Clear();
                        _menuRegistro.RegistrarUsuario();
                        break;
                    case "3":
                        MostrarMensaje("Abriendo formulario de registro, presiona una tecla...", ConsoleColor.Green);
                        Console.Clear();
                        _menuAdministrador.Administrador();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        MostrarMensaje("⚠️ Opción no válida. Intente de nuevo.", ConsoleColor.Red);
                        break;
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            MostrarMensaje("Gracias por usar Campus Love. ¡Hasta pronto! ❤️\nPresione cualquier letra", ConsoleColor.Green);
            Console.ReadKey();
        }

        public static void MostrarMensaje(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            Console.ReadKey();
        }

        internal static string ReadText(string v)
        {
            Console.Write(v + ": ");
            return Console.ReadLine() ?? string.Empty;
        }

        internal static int ReadInt(string v)
        {
            int resultado;
            while (true)
            {
                Console.Write(v + ": ");
                string? entrada = Console.ReadLine();

                if (int.TryParse(entrada, out resultado))
                {
                    return resultado;
                }

                Console.WriteLine("❌ Entrada no válida. Por favor, ingresa un número entero.");
            }
        }

    }
}