using CampusLove.Application.UI;

namespace CampusLove
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Iniciar la aplicación
            try
            {
                // Crear y ejecutar el menú principal
                MenuPrincipal menu = new MenuPrincipal();
                await menu.MostrarMenu();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error fatal en la aplicación: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Console.ResetColor();
            }
        }
    }
}