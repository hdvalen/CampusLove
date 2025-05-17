using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using Org.BouncyCastle.Crypto.Engines;

namespace CampusLove.Application.UI
{
    public class MenuPerfil
    {
        private Usuarios _usuarioActual;
        private readonly IUsuarioRepository _usuarioRepository;

        public MenuPerfil(Usuarios usuarioActual, IUsuarioRepository usuarioRepository)
        {
            _usuarioActual = usuarioActual;
            _usuarioRepository = usuarioRepository;
        }

        public void MostrarMenu()
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
                Console.WriteLine("║ 📝 PERFIL DE USUARIO                                   ║");
                Console.WriteLine("║   1. Ver mi perfil                                     ║");
                Console.WriteLine("║   2. Editar mi perfil                                  ║");
                Console.WriteLine("║   3. Eliminar mi perfil                                ║");
                Console.WriteLine("║   4. Ver otros perfiles                                ║");
                Console.WriteLine("║   5. Ver Matches                                       ║");
                Console.WriteLine("║   0. Volver al menú principal                          ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("\nSeleccione una opción: ");
                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        VerMiPerfil();
                        break;
                    case "2":
                        EditarPerfil();
                        break;
                    case "3":
                        EliminarPerfil();
                        salir = true;
                        break;
                    case "4":
                        // Aquí puedes agregar la opción de ver otros perfiles
                        break;
                    case "5":
                        // Aquí puedes agregar la opción de ver mensajes
                        break;
                    case "0":
                      salir = true;
                        break;
                    default:
                        Console.WriteLine("❌ Opción no válida. Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void VerMiPerfil()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.ResetColor();

            Console.WriteLine("🧾 Mi Perfil:");
            Console.WriteLine($"📛 Nombre: {_usuarioActual.nombre}");
            Console.WriteLine($"👤 Login: {_usuarioActual.login}");
            Console.WriteLine($"🎂 Edad: {_usuarioActual.edad}");
            Console.WriteLine($"📝 Frase de perfil: {_usuarioActual.FrasePerfil}");
            Console.WriteLine($"🎓 Carrera: {_usuarioActual.idCarrera?.Nombre}");
            Console.WriteLine($"⚧️ Género: {_usuarioActual.idGenero?.Nombre}");

            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }

        private void EditarPerfil()
        {
            Console.Clear();
            Console.WriteLine("✏️ Editar Perfil");

            Console.Write($"Nuevo nombre (actual: {_usuarioActual.nombre}): ");
            string? nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
                _usuarioActual.nombre = nuevoNombre;

            Console.Write($"Nueva frase de perfil (actual: {_usuarioActual.FrasePerfil}): ");
            string? nuevaFrase = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevaFrase))
                _usuarioActual.FrasePerfil = nuevaFrase;

            Console.Write($"Nueva edad (actual: {_usuarioActual.edad}): ");
            string? nuevaEdad = Console.ReadLine();
            if (int.TryParse(nuevaEdad, out int edad) && edad > 0)
                _usuarioActual.edad = edad;

            // Puedes agregar edición de carrera y género si lo deseas

            _usuarioRepository.UpdateAsync(_usuarioActual).Wait();

            Console.WriteLine("✅ Perfil actualizado. Presione una tecla para continuar...");
            Console.ReadKey();
        }

        private void EliminarPerfil()
        {
            Console.WriteLine("⚠️ ¿Estás seguro de que quieres eliminar tu perfil? (s/n)");
            string confirmacion = Console.ReadLine() ?? "n";
            if (confirmacion.ToLower() == "s")
            {
                _usuarioRepository.DeleteAsync(_usuarioActual.id).Wait();
                Console.WriteLine("🗑️ Perfil eliminado exitosamente. Presione una tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("❌ Eliminación cancelada. Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}