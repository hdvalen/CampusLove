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
                Console.WriteLine("=== MENÚ ADMINISTRADORES ===");
                Console.WriteLine("1. Listar administradores");
                Console.WriteLine("2. Agregar administrador");
                Console.WriteLine("3. Actualizar administrador");
                Console.WriteLine("4. Eliminar administrador");
                Console.WriteLine("0. Salir");
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
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            Console.WriteLine("Opción inválida.");
                            break;
                    }
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 0);
        }

        private async Task ListarAsync()
        {
            var lista = await _repo.GetAllAsync();

            Console.WriteLine("=== LISTADO DE ADMINISTRADORES ===");
            foreach (var a in lista)
            {
                Console.WriteLine($"ID: {a.id}, Usuario: {a.usuario}, Nombre: {a.nombre}, Correo: {a.correo}, Activo: {a.activo}");
            }
        }

        private async Task AgregarAsync()
        {
            var admin = new Administrador();

            Console.WriteLine("=== NUEVO ADMINISTRADOR ===");
            Console.Write("Nombre: "); admin.nombre = Console.ReadLine()!;
            Console.Write("Usuario: "); admin.usuario = Console.ReadLine()!;
            Console.Write("Contraseña: "); admin.contrasena = Console.ReadLine()!;
            Console.Write("Correo: "); admin.correo = Console.ReadLine()!;
            Console.Write("Nivel de acceso (ej. 1): "); admin.nivel_acceso = int.Parse(Console.ReadLine()!);

            admin.fecha_creacion = DateTime.Now;
            admin.ultimo_acceso = null;
            admin.activo = true;

            bool ok = await _repo.InsertAsync(0, admin);
            Console.WriteLine(ok ? "Administrador creado con éxito." : "Error al crear administrador.");
        }

        private async Task ActualizarAsync()
        {
            Console.Write("Ingrese el ID del administrador a actualizar: ");
            int id = int.Parse(Console.ReadLine()!);
            var admin = await _repo.GetByIdAsync(id);

            if (admin == null)
            {
                Console.WriteLine("Administrador no encontrado.");
                return;
            }

            Console.WriteLine("=== ACTUALIZAR ADMINISTRADOR ===");
            Console.Write($"Nombre ({admin.nombre}): "); var n = Console.ReadLine();
            Console.Write($"Usuario ({admin.usuario}): "); var u = Console.ReadLine();
            Console.Write($"Contraseña ({admin.contrasena}): "); var c = Console.ReadLine();
            Console.Write($"Correo ({admin.correo}): "); var m = Console.ReadLine();
            Console.Write($"Nivel de acceso ({admin.nivel_acceso}): "); var na = Console.ReadLine();
            Console.Write($"Activo ({admin.activo}): "); var a = Console.ReadLine();

            admin.nombre = string.IsNullOrEmpty(n) ? admin.nombre : n;
            admin.usuario = string.IsNullOrEmpty(u) ? admin.usuario : u;
            admin.contrasena = string.IsNullOrEmpty(c) ? admin.contrasena : c;
            admin.correo = string.IsNullOrEmpty(m) ? admin.correo : m;
            admin.nivel_acceso = string.IsNullOrEmpty(na) ? admin.nivel_acceso : int.Parse(na);
            admin.activo = string.IsNullOrEmpty(a) ? admin.activo : bool.Parse(a);
            admin.ultimo_acceso = DateTime.Now;

            bool ok = await _repo.UpdateAsync(admin);
            Console.WriteLine(ok ? "Administrador actualizado." : "Error al actualizar.");
        }

        private async Task EliminarAsync()
        {
            Console.Write("Ingrese el ID del administrador a eliminar: ");
            int id = int.Parse(Console.ReadLine()!);
            bool ok = await _repo.DeleteAsync(id);
            Console.WriteLine(ok ? "Administrador eliminado." : "No se pudo eliminar (¿Existe?).");
        }
    }
}
