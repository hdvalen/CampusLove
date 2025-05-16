using System;

namespace campusLove.Application.UI
{
    public class MenuPrincipal
    {
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
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 🔑 ACCESO AL SISTEMA                                  ║");
                Console.WriteLine("║   1. 🔐 Iniciar sesión                                ║");
                Console.WriteLine("║   2. 📝 Registrarse                                   ║");
                Console.WriteLine("║   0. 🚪 Salir                                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("🡺 Seleccione una opción: ");
                string opcion = Console.ReadLine();
                Console.ResetColor();

                switch (opcion)
                {
                    case "1":
                        MostrarMensaje("Iniciando sesión...", ConsoleColor.Green);
                        break;
                    case "2":
                        MostrarMensaje("Abriendo formulario de registro...", ConsoleColor.Green);
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
            MostrarMensaje("Gracias por usar Campus Love. ¡Hasta pronto! ❤️", ConsoleColor.Green);
            Console.WriteLine("Presione cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        public static void MostrarMensaje(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        

    }
}