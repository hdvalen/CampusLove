
using CampusLove.Domain.Ports;
using CampusLove.Domain.Entities;
using CampusLoveampusLove.Domain.Entities;


namespace CampusLove.Application.UI
{
    public class MenuIntereses
    {
        private readonly IInteresesRepository _interesesRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly Usuarios _usuarioActual;

        public MenuIntereses(IInteresesRepository interesesRepository, IUsuarioRepository usuarioRepository, Usuarios usuarioActual)
        {
            _interesesRepository = interesesRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioActual = usuarioActual;
        }
        public async Task MostrarMenuIntereses()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                   ❤️  CAMPUS LOVE  ❤️                    ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("║ 📝 INTERESES DE USUARIO                                 ║");
                Console.WriteLine("║   1. Registrar mis intereses                              ║");
                Console.WriteLine("║   2. Ver mis intereses                                    ║");
                Console.WriteLine("║   3. Editar mis intereses                                 ║");
                Console.WriteLine("║   4. Eliminar mis intereses                               ║");
                Console.WriteLine("║   0. Volver al menú principal                             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("\nSeleccione una opción: ");
                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        await RegistrarMisIntereses();
                        break;
                    case "2":
                        await VerMisIntereses();
                        break;
                    case "3":
                        await EditarMisIntereses();
                        break;
                    case "4":
                        await EliminarMisIntereses();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }

        private async Task RegistrarMisIntereses()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   ❤️  CAMPUS LOVE  ❤️                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║ 📝 REGISTRAR MIS INTERESES                              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            try
            {
                while (true)
                {
                    Console.WriteLine("\nIngrese su interés (o escriba 'salir' para terminar): ");
                    string? interesInput = Console.ReadLine()?.Trim();

                    // Validar entrada
                    if (string.IsNullOrEmpty(interesInput))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("⚠️  Por favor, ingrese un interés válido.");
                        Console.ResetColor();
                        continue;
                    }

                    // Opción para salir
                    if (interesInput.ToLower() == "salir")
                    {
                        break;
                    }

                    // Crear nuevo interés
                    var nuevoInteres = new Intereses
                    {
                        nombre = interesInput
                    };

                    // Insertar el interés y obtener el ID generado
                    int nuevoId = await _interesesRepository.InsertAndReturnIdAsync(nuevoInteres);

                    if (nuevoId > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"✅ Interés '{interesInput}' registrado exitosamente con ID: {nuevoId}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"❌ Error al registrar el interés '{interesInput}'");
                        Console.ResetColor();
                    }

                    // Preguntar si quiere agregar otro interés
                    Console.WriteLine("\n¿Desea agregar otro interés? (s/n): ");
                    string? respuesta = Console.ReadLine()?.Trim().ToLower();
                    if (respuesta != "s" && respuesta != "si")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ Error inesperado: {ex.Message}");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n✨ Proceso de registro de intereses completado.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }
        private async Task VerMisIntereses()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   ❤️  CAMPUS LOVE  ❤️                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║ 📝 MIS INTERESES                                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            var intereses = await _interesesRepository.GetByIdAsync(_usuarioActual.id);
            if (intereses == null)
            {
                Console.WriteLine("No tienes intereses registrados.");
            }
            else if (intereses is IEnumerable<Intereses> listaIntereses && listaIntereses.Any())
            {
                foreach (var interes in listaIntereses)
                {
                    Console.WriteLine($"- {interes.nombre}");
                }
            }
            else if (intereses is Intereses interesUnico)
            {
                Console.WriteLine($"- {interesUnico.nombre}");
            }
            else
            {
                Console.WriteLine("No tienes intereses registrados.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        private async Task EditarMisIntereses()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   ❤️  CAMPUS LOVE  ❤️                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║ 📝 EDITAR MIS INTERESES                                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();


            var intereses = await _interesesRepository.GetByIdAsync(_usuarioActual.id);
            if (intereses != null)
            {
                Console.WriteLine($"- {intereses.nombre}");
            }
            else
            {
                Console.WriteLine("No tienes intereses registrados.");
            }

            Console.WriteLine("\nIngrese el nuevo interés (o 'salir' para cancelar): ");
            string? nuevoInteres = Console.ReadLine();
            if (nuevoInteres?.ToLower() != "salir")
            {
                var interesNuevo = new Intereses { nombre = nuevoInteres };
                await _interesesRepository.InsertAsync(_usuarioActual.id, interesNuevo);
                Console.WriteLine($"✅ Interés '{nuevoInteres}' agregado exitosamente.");
            }
            else
            {
                Console.WriteLine("❌ Edición cancelada.");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
        }
        private async Task EliminarMisIntereses()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   ❤️  CAMPUS LOVE  ❤️                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════╣");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║ 📝 ELIMINAR MIS INTERESES                              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            var intereses = await _interesesRepository.GetByIdAsync(_usuarioActual.id);
            if (intereses == null || (intereses is IEnumerable<Intereses> lista && !lista.Any()))
            {
                Console.WriteLine("No tienes intereses registrados.");
            }
            else if (intereses is IEnumerable<Intereses> listaIntereses)
            {
                Console.WriteLine("Tus intereses actuales:");
                foreach (var interes in listaIntereses)
                {
                    Console.WriteLine($"- {interes.nombre}");
                }
            }
            else
            {
                Console.WriteLine("Tus intereses actuales:");
                Console.WriteLine($"- {intereses.nombre}");
            }

            Console.WriteLine("\nIngrese el interés a eliminar (o 'salir' para cancelar): ");
            string? interesAEliminar = Console.ReadLine();
            if (interesAEliminar?.ToLower() != "salir")
            {
                var interes = await _interesesRepository.GetByIdAsync(interesAEliminar);
                if (interes != null)
                {
                    await _interesesRepository.DeleteAsync(interes.id);
                    Console.WriteLine($"✅ Interés '{interes.nombre}' eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine($"❌ No se encontró el interés '{interesAEliminar}'.");
                }
            }
            else
            {
                Console.WriteLine("❌ Eliminación cancelada.");
            }


            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}