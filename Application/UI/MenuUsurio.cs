

namespace campusLove.Application.UI
{
    public class MenuUsuario
    {

        public void MostrarMenu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("Usuario");
                Console.WriteLine("1. Nombre");
                Console.WriteLine("2. Dislike");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        // Lógica para iniciar sesión
                        break;
                    case "2":
                        // Lógica para registrarse
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.", ConsoleColor.DarkGreen);
                        break;
                }
            }
            MostrarMensaje("Gracias por usar Campus Love. ¡Hasta luego!", ConsoleColor.DarkGreen);
        }
        public static void MostrarEncabezado(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            
            string borde = new string('=', titulo.Length + 4);
            Console.WriteLine(borde);
            Console.WriteLine($"| {titulo} |");
            Console.WriteLine(borde);
            
            Console.ResetColor();
        }
        
        public static void MostrarMensaje(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
        
        public static string LeerEntrada(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }
        
        public static int LeerEnteroPositivo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int valor) && valor >= 0)
                {
                    return valor;
                }
                
                MostrarMensaje("⚠ Error: Debe ingresar un número entero positivo.", ConsoleColor.Red);
            }
        }
        
        public static decimal LeerDecimalPositivo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal valor) && valor >= 0)
                {
                    return valor;
                }
                
                MostrarMensaje("⚠ Error: Debe ingresar un número decimal positivo.", ConsoleColor.Red);
            }
        }
        
        public static DateTime LeerFecha(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime fecha))
                {
                    return fecha;
                }
                
                MostrarMensaje("⚠ Error: Formato de fecha incorrecto. Use DD/MM/AAAA.", ConsoleColor.Red);
            }
        }

    }
}