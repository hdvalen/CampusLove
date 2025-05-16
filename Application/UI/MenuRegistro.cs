using System;
using campusLove.Domain.Entities;

namespace campusLove.Application.UI
{
    public class MenuRegistro
    {
        public void MostrarMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  📝 REGISTRO DE USUARIO                ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();


        public async Task RegistrarUsuario(){

            string nombre = MenuRegistro.ReadText("📛 Nombre: ");
            int edad = MenuRegistro.ReadInt("🎂 Edad: ");
            while (!int.TryParse(Console.ReadLine(), out edad))
            {
                Console.Write("⚠️ Edad no válida. Ingrese un número: ");
            }
            string frasePerfil = MenuRegistro.ReadText("📝 Frase Perfil: ");
            string genero = MenuRegistro.ReadText("👤 Género: ");
            

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
        }
    }
}
