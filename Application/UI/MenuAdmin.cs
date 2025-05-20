using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using MySql.Data.MySqlClient;

namespace CampusLove.Application.UI
{
    public class MenuAdministrador
    {
        private readonly IAdministradorRepository _repo;

        public MenuAdministrador(IAdministradorRepository repo)
        {
            _repo = repo;
        }

        public async Task MostrarMenuAdministradores()
        {
            int opcion = -1;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔═══════════════════════════════════════╗");
                Console.WriteLine("║        🛠️  MENÚ ADMINISTRADORES        ║");
                Console.WriteLine("╠═══════════════════════════════════════╣");
                Console.WriteLine("║ 1. 📋 Listar administradores           ║");
                Console.WriteLine("║ 2. ➕ Agregar administrador            ║");
                Console.WriteLine("║ 3. ✏️  Actualizar administrador         ║");
                Console.WriteLine("║ 4. ❌ Eliminar administrador           ║");
                Console.WriteLine("║ 0. 🔙 Salir                            ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.Clear();
                    switch (opcion)
                    {
                        case 1:
                            await ListarAsync();
                            break;
                        case 2:
                            await AgregarAsync();
                            break;
                        case 3:
                            await ActualizarAsync();
                            break;
                        case 4:
                            await EliminarAsync();
                            break;
                        case 0:
                            Console.WriteLine("👋 Saliendo del menú...");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("⚠️ Opción inválida. Intente nuevamente.");
                            Console.ResetColor();
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Entrada no válida. Por favor ingrese un número.");
                    Console.ResetColor();
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }

        private async Task ListarAsync()
{
    var lista = await _repo.GetAllAsync();

    if (lista == null || !lista.Any())
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("⚠️ No hay administradores registrados.");
        Console.ResetColor();
        return;
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("🧾 === LISTADO DE ADMINISTRADORES ===\n");
    Console.ResetColor();

    foreach (var a in lista)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        Console.ResetColor();

        Console.WriteLine($"🆔 ID:              {a.id}");
        Console.WriteLine($"👥 Usuario:         {a.usuario}");
        Console.WriteLine($"👤 Nombre:          {a.nombre}");
        Console.WriteLine($"📧 Correo:          {a.correo}");
        Console.WriteLine($"🔐 Nivel de acceso: {a.nivel_acceso}");
        Console.WriteLine($"✅ Activo:          {(a.activo ? "Sí" : "No")}");
        Console.WriteLine($"📅 Creado:          {a.fecha_creacion}");
        Console.WriteLine($"🕒 Último acceso:   {(a.ultimo_acceso.HasValue ? a.ultimo_acceso.Value.ToString() : "Nunca")}");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
        Console.ResetColor();
    }
}



        private async Task AgregarAsync()
        {
            var admin = new Administrador();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🆕 === CREAR NUEVO ADMINISTRADOR ===\n");
            Console.ResetColor();

            Console.Write("👤 Nombre: "); admin.nombre = Console.ReadLine()!;
            Console.Write("👥 Usuario: "); admin.usuario = Console.ReadLine()!;
            Console.Write("🔑 Contraseña: "); admin.contrasena = Console.ReadLine()!;
            Console.Write("📧 Correo: "); admin.correo = Console.ReadLine()!;
            Console.Write("🔐 Nivel de acceso (ej. 1): "); admin.nivel_acceso = int.Parse(Console.ReadLine()!);

            admin.fecha_creacion = DateTime.Now;
            admin.ultimo_acceso = null;
            admin.activo = true;

            bool ok = await _repo.InsertAsync(0, admin);

            Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(ok ? "\n✅ Administrador creado con éxito." : "\n❌ Error al crear administrador.");
            Console.ResetColor();
        }


        private async Task ActualizarAsync()
        {
            Console.Write("🔎 Ingrese el ID del administrador a actualizar: ");
            int id = int.Parse(Console.ReadLine()!);
            var admin = await _repo.GetByIdAsync(id);

            if (admin == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Administrador no encontrado.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n✏️ === ACTUALIZAR ADMINISTRADOR ===");
            Console.ResetColor();

            Console.Write($"👤 Nombre ({admin.nombre}): "); var n = Console.ReadLine();
            Console.Write($"👥 Usuario ({admin.usuario}): "); var u = Console.ReadLine();
            Console.Write($"🔑 Contraseña ({admin.contrasena}): "); var c = Console.ReadLine();
            Console.Write($"📧 Correo ({admin.correo}): "); var m = Console.ReadLine();
            Console.Write($"🔐 Nivel de acceso ({admin.nivel_acceso}): "); var na = Console.ReadLine();
            Console.Write($"✅ Activo ({admin.activo}): "); var a = Console.ReadLine();

            admin.nombre = string.IsNullOrEmpty(n) ? admin.nombre : n;
            admin.usuario = string.IsNullOrEmpty(u) ? admin.usuario : u;
            admin.contrasena = string.IsNullOrEmpty(c) ? admin.contrasena : c;
            admin.correo = string.IsNullOrEmpty(m) ? admin.correo : m;
            admin.nivel_acceso = string.IsNullOrEmpty(na) ? admin.nivel_acceso : int.Parse(na);
            admin.activo = string.IsNullOrEmpty(a) ? admin.activo : bool.Parse(a);
            admin.ultimo_acceso = DateTime.Now;

            bool ok = await _repo.UpdateAsync(admin);

            Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(ok ? "\n✅ Administrador actualizado." : "\n❌ Error al actualizar administrador.");
            Console.ResetColor();
        }


        private async Task EliminarAsync()
        {
            Console.Write("🗑️ Ingrese el ID del administrador a eliminar: ");
            int id = int.Parse(Console.ReadLine()!);
            bool ok = await _repo.DeleteAsync(id);

            Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(ok ? "\n✅ Administrador eliminado correctamente." : "\n❌ No se pudo eliminar. ¿Existe ese ID?");
            Console.ResetColor();
        }

    }
}
