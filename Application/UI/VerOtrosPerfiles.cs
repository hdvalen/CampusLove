using CampusLove.Domain.Ports;

namespace CampusLove.Application.UI
{
    public class VerOtrosPerfiles
    {
        private Usuarios _usuarioActual;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICoincidenciaRepository _coincidenciaRepository;

        public VerOtrosPerfiles(Usuarios usuarioActual, IUsuarioRepository usuarioRepository, ICoincidenciaRepository coincidenciaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _coincidenciaRepository = coincidenciaRepository;
            _usuarioActual = usuarioActual;
        }

        private int _likesDisponibles = 10; // Límite diario de likes
        private HashSet<int> _usuariosConLike = new HashSet<int>();

        public async Task MostrarMasPerfiles()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           💘 ¿Qué género te interesa ver?              ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.WriteLine("║   1. Hombre                                            ║");
            Console.WriteLine("║   2. Mujer                                             ║");
            Console.WriteLine("║   3. Ambos                                             ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("👉 Selecciona una opción: ");
            string? opcionGenero = Console.ReadLine();

            int? generoInteres = null; 

            switch (opcionGenero)
            {
                case "1":
                    generoInteres = 1; 
                    break;
                case "2":
                    generoInteres = 2; 
                    break;
                case "3":
                    generoInteres = null; // Ambos
                    break;
                default:
                    Console.WriteLine("❌ Opción no válida. Volviendo al menú...");
                    Console.ReadKey();
                    return;
            }

            var usuarios = _usuarioRepository.GetAllAsync().Result;

            var otrosUsuarios = usuarios
                .Where(u => u.id != _usuarioActual.id &&
                            !_usuariosConLike.Contains(u.id) &&
                            (generoInteres == null || u.idGenero?.Id == generoInteres))
                .ToList();

            if (!otrosUsuarios.Any())
            {
                Console.WriteLine("\n🎉 No hay más perfiles disponibles según tu filtro.");
                Console.WriteLine("Presiona una tecla para volver al menú...");
                Console.ReadKey();
                return;
            }

            foreach (var usuario in otrosUsuarios)
            {
                bool opcionValida = false;

                while (!opcionValida)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                    Console.WriteLine($"║ 📛 Nombre: {usuario.nombre,-44}║");
                    Console.WriteLine($"║ 🎂 Edad: {usuario.edad,-47}║");
                    Console.WriteLine($"║ 📝 Frase de perfil: {(usuario.FrasePerfil?.Length > 35 ? usuario.FrasePerfil.Substring(0, 35) + "..." : usuario.FrasePerfil),-35}║");
                    Console.WriteLine($"║ 🎓 Carrera: {usuario.idCarrera?.Nombre,-44}║");
                    Console.WriteLine($"║ ⚧️ Género: {usuario.idGenero?.Nombre,-46}║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                    Console.ResetColor();

                    Console.WriteLine($"❤️ Likes disponibles hoy: {_likesDisponibles}");
                    Console.WriteLine("¿Qué deseas hacer?");
                    Console.WriteLine("[L] Like    [D] Dislike    [0] Salir");
                    Console.Write("👉 Opción: ");
                    string? opcion = Console.ReadLine()?.Trim().ToUpper();

                    switch (opcion)
                    {
                        case "0":
                            return;

                        case "L":
                            if (_likesDisponibles <= 0)
                            {
                                Console.WriteLine("❌ Ya no te quedan más likes por hoy.");
                                Console.ReadKey();
                                return;
                            }

                            await _usuarioRepository.AgregarLikeAsync(_usuarioActual.id, usuario.id);
                            _usuariosConLike.Add(usuario.id);
                            _likesDisponibles--;

                            var likeMutuoResult = await _usuarioRepository.ExisteLikeMutuoAsync(_usuarioActual.id, usuario.id);
                            bool esMatch = Convert.ToBoolean(likeMutuoResult);

                            Console.WriteLine($"💘 ¡Le diste like a {usuario.nombre}!");

                            if (esMatch)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("🎉 ¡MATCH! A ambos les gustaron sus perfiles. ❤️");
                                Console.ResetColor();
                            }

                            opcionValida = true;
                            break;

                        case "D":
                            Console.WriteLine($"👎 Diste dislike a {usuario.nombre}.");
                            opcionValida = true;
                            break;

                        default:
                            Console.WriteLine("❌ Opción inválida. Intenta de nuevo.");
                            Console.ReadKey();
                            break;
                    }

                    if (opcionValida)
                    {
                        Console.WriteLine("\nPresiona una tecla para ver el siguiente perfil...");
                        Console.ReadKey();
                    }
                }
            }

            Console.WriteLine("\n🌟 ¡Has terminado de revisar los perfiles disponibles!");
            Console.WriteLine("Presiona una tecla para volver al menú...");
            Console.ReadKey();
        }
    }
}
