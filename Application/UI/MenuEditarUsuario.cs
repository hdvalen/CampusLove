/* using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SistemaPerfiles
{
    class Program
    {
        static List<Usuario> usuarios = new List<Usuario>();
        static string archivoUsuarios = "usuarios.json";
        
        static void Main(string[] args)
        {
            CargarUsuarios();
            MostrarMenuPrincipal();
        }

        static void MostrarMenuPrincipal()
        {
            bool salir = false;
            
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║       SISTEMA DE PERFILES            ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nSeleccione una opción:");
                Console.WriteLine("1. Ver todos los perfiles");
                Console.WriteLine("2. Crear nuevo perfil");
                Console.WriteLine("3. Ver detalle de perfil");
                Console.WriteLine("4. Editar perfil");
                Console.WriteLine("5. Guardar y salir");
                Console.Write("\nOpción: ");
                
                string opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        MostrarTodosLosPerfiles();
                        break;
                    case "2":
                        CrearPerfil();
                        break;
                    case "3":
                        VerDetallePerfil();
                        break;
                    case "4":
                        EditarPerfil();
                        break;
                    case "5":
                        GuardarUsuarios();
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MostrarTodosLosPerfiles()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          LISTA DE PERFILES           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            if (usuarios.Count == 0)
            {
                Console.WriteLine("\nNo hay perfiles registrados.");
            }
            else
            {
                for (int i = 0; i < usuarios.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {usuarios[i].Nombre} - {usuarios[i].Edad} años - {usuarios[i].Carrera}");
                }
            }
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void CrearPerfil()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          CREAR NUEVO PERFIL          ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            Usuario nuevoUsuario = new Usuario();
            
            Console.Write("\nNombre: ");
            nuevoUsuario.Nombre = Console.ReadLine();
            
            bool edadValida = false;
            while (!edadValida)
            {
                Console.Write("Edad: ");
                if (int.TryParse(Console.ReadLine(), out int edad))
                {
                    nuevoUsuario.Edad = edad;
                    edadValida = true;
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido para la edad.");
                }
            }
            
            Console.Write("Género (M/F/Otro): ");
            nuevoUsuario.Genero = Console.ReadLine();
            
            Console.Write("Interés: ");
            nuevoUsuario.Interes = Console.ReadLine();
            
            Console.Write("Carrera: ");
            nuevoUsuario.Carrera = Console.ReadLine();
            
            Console.Write("Frase de perfil: ");
            nuevoUsuario.FrasePerfil = Console.ReadLine();
            
            usuarios.Add(nuevoUsuario);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n¡Perfil creado exitosamente!");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void VerDetallePerfil()
        {
            if (usuarios.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No hay perfiles registrados.");
                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
                Console.ReadKey();
                return;
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          DETALLE DE PERFIL           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine("\nSeleccione un perfil:");
            
            for (int i = 0; i < usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {usuarios[i].Nombre}");
            }
            
            Console.Write("\nNúmero de perfil: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= usuarios.Count)
            {
                Usuario usuario = usuarios[indice - 1];
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║          DETALLE DE PERFIL           ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                
                Console.WriteLine($"\nNombre: {usuario.Nombre}");
                Console.WriteLine($"Edad: {usuario.Edad} años");
                Console.WriteLine($"Género: {usuario.Genero}");
                Console.WriteLine($"Interés: {usuario.Interes}");
                Console.WriteLine($"Carrera: {usuario.Carrera}");
                Console.WriteLine($"Frase de perfil: \"{usuario.FrasePerfil}\"");
            }
            else
            {
                Console.WriteLine("\nSelección no válida.");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void EditarPerfil()
        {
            if (usuarios.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No hay perfiles registrados.");
                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
                Console.ReadKey();
                return;
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║            EDITAR PERFIL             ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine("\nSeleccione un perfil para editar:");
            
            for (int i = 0; i < usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {usuarios[i].Nombre}");
            }
            
            Console.Write("\nNúmero de perfil: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= usuarios.Count)
            {
                Usuario usuario = usuarios[indice - 1];
                using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SistemaPerfiles
{
    public class Program
    {
        private readonly MostrarMenuPrincipal() _menuPrincipal;
        public MenuUsurio()
        {
            _menuPrincipal = new MostrarMenuPrincipal();
        }

        public void  MostrarMenu()
        {
            bool salir = false;
            
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║       SISTEMA DE PERFILES            ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nSeleccione una opción:");
                Console.WriteLine("1. Crear nuevo perfil");
                Console.WriteLine("2. Ver detalle de perfil");
                Console.WriteLine("3. Editar perfil");
                Console.WriteLine("4. Guardar y salir");
                Console.Write("\nOpción: ");
                
                string opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        CrearPerfil().wait();
                        break;
                    case "2":
                        VerDetallePerfil().wait();
                        break;
                    case "3":
                        EditarPerfil().wait();
                        break;
                    case "4":
                        GuardarUsuarios().wait();
                        salir = true;
                        break;
                    default:
                        MenuPrincipal.MostrarMensaje("Opción no válida. Intente de nuevo.", ConsoleColor.DarkMagenta);
                        Console.ReadKey();
                        break;
                }
            }
        }


        static void CrearPerfil()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          CREAR NUEVO PERFIL          ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            Usuario nuevoUsuario = new Usuario();
            
            Console.Write("\nNombre: ");
            nuevoUsuario.Nombre = Console.ReadLine();
            
            bool edadValida = false;
            while (!edadValida)
            {
                Console.Write("Edad: ");
                if (int.TryParse(Console.ReadLine(), out int edad))
                {
                    nuevoUsuario.Edad = edad;
                    edadValida = true;
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido para la edad.");
                }
            }
            
            Console.Write("Género (M/F/Otro): ");
            nuevoUsuario.Genero = Console.ReadLine();
            
            Console.Write("Interés: ");
            nuevoUsuario.Interes = Console.ReadLine();
            
            Console.Write("Carrera: ");
            nuevoUsuario.Carrera = Console.ReadLine();
            
            Console.Write("Frase de perfil: ");
            nuevoUsuario.FrasePerfil = Console.ReadLine();
            
            usuarios.Add(nuevoUsuario);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n¡Perfil creado exitosamente!");
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void VerDetallePerfil()
        {
            if (usuarios.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No hay perfiles registrados.");
                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
                Console.ReadKey();
                return;
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║          DETALLE DE PERFIL           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine("\nSeleccione un perfil:");
            
            for (int i = 0; i < usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {usuarios[i].Nombre}");
            }
            
            Console.Write("\nNúmero de perfil: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= usuarios.Count)
            {
                Usuario usuario = usuarios[indice - 1];
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║          DETALLE DE PERFIL           ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                
                Console.WriteLine($"\nNombre: {usuario.Nombre}");
                Console.WriteLine($"Edad: {usuario.Edad} años");
                Console.WriteLine($"Género: {usuario.Genero}");
                Console.WriteLine($"Interés: {usuario.Interes}");
                Console.WriteLine($"Carrera: {usuario.Carrera}");
                Console.WriteLine($"Frase de perfil: \"{usuario.FrasePerfil}\"");
            }
            else
            {
                Console.WriteLine("\nSelección no válida.");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void EditarPerfil()
        {
            if (usuarios.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No hay perfiles registrados.");
                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
                Console.ReadKey();
                return;
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║            EDITAR PERFIL             ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine("\nSeleccione un perfil para editar:");
            
            for (int i = 0; i < usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {usuarios[i].Nombre}");
            }
            
            Console.Write("\nNúmero de perfil: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= usuarios.Count)
            {
                Usuario usuario = usuarios[indice - 1];
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║            EDITAR PERFIL             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                
                Console.WriteLine("\nDeje en blanco para mantener el valor actual.");
                
                Console.Write($"Nombre [{usuario.Nombre}]: ");
                string nombre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    usuario.Nombre = nombre;
                }
                
                Console.Write($"Edad [{usuario.Edad}]: ");
                string edadStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(edadStr) && int.TryParse(edadStr, out int edad))
                {
                    usuario.Edad = edad;
                }
                
                Console.Write($"Género [{usuario.Genero}]: ");
                string genero = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(genero))
                {
                    usuario.Genero = genero;
                }
                
                Console.Write($"Interés [{usuario.Interes}]: ");
                string interes = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(interes))
                {
                    usuario.Interes = interes;
                }
                
                Console.Write($"Carrera [{usuario.Carrera}]: ");
                string carrera = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(carrera))
                {
                    usuario.Carrera = carrera;
                }
                
                Console.Write($"Frase de perfil [{usuario.FrasePerfil}]: ");
                string frase = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(frase))
                {
                    usuario.FrasePerfil = frase;
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n¡Perfil actualizado exitosamente!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\nSelección no válida.");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void CargarUsuarios()
        {
            try
            {
                if (File.Exists(archivoUsuarios))
                {
                    string json = File.ReadAllText(archivoUsuarios);
                    usuarios = JsonSerializer.Deserialize<List<Usuario>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar usuarios: {ex.Message}");
                usuarios = new List<Usuario>();
            }
        }

        static void GuardarUsuarios()
        {
            try
            {
                string json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(archivoUsuarios, json);
                Console.WriteLine("\nDatos guardados exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al guardar usuarios: {ex.Message}");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║            EDITAR PERFIL             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.ResetColor();
                
                Console.WriteLine("\nDeje en blanco para mantener el valor actual.");
                
                Console.Write($"Nombre [{usuario.Nombre}]: ");
                string nombre = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    usuario.Nombre = nombre;
                }
                
                Console.Write($"Edad [{usuario.Edad}]: ");
                string edadStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(edadStr) && int.TryParse(edadStr, out int edad))
                {
                    usuario.Edad = edad;
                }
                
                Console.Write($"Género [{usuario.Genero}]: ");
                string genero = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(genero))
                {
                    usuario.Genero = genero;
                }
                
                Console.Write($"Interés [{usuario.Interes}]: ");
                string interes = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(interes))
                {
                    usuario.Interes = interes;
                }
                
                Console.Write($"Carrera [{usuario.Carrera}]: ");
                string carrera = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(carrera))
                {
                    usuario.Carrera = carrera;
                }
                
                Console.Write($"Frase de perfil [{usuario.FrasePerfil}]: ");
                string frase = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(frase))
                {
                    usuario.FrasePerfil = frase;
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n¡Perfil actualizado exitosamente!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\nSelección no válida.");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void EliminarPerfil()
        {
            if (usuarios.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No hay perfiles registrados.");
                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
                Console.ReadKey();
                return;
            }
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║           ELIMINAR PERFIL            ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            
            Console.WriteLine("\nSeleccione un perfil para eliminar:");
            
            for (int i = 0; i < usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {usuarios[i].Nombre}");
            }
            
            Console.Write("\nNúmero de perfil: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= usuarios.Count)
            {
                Usuario usuario = usuarios[indice - 1];
                
                Console.WriteLine($"\n¿Está seguro que desea eliminar el perfil de {usuario.Nombre}? (S/N)");
                string confirmacion = Console.ReadLine().ToUpper();
                
                if (confirmacion == "S")
                {
                    usuarios.RemoveAt(indice - 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n¡Perfil eliminado exitosamente!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("\nOperación cancelada.");
                }
            }
            else
            {
                Console.WriteLine("\nSelección no válida.");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }

        static void CargarUsuarios()
        {
            try
            {
                if (File.Exists(archivoUsuarios))
                {
                    string json = File.ReadAllText(archivoUsuarios);
                    usuarios = JsonSerializer.Deserialize<List<Usuario>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar usuarios: {ex.Message}");
                usuarios = new List<Usuario>();
            }
        }

        static void GuardarUsuarios()
        {
            try
            {
                string json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(archivoUsuarios, json);
                Console.WriteLine("\nDatos guardados exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError al guardar usuarios: {ex.Message}");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
} */