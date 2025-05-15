﻿using campusLove.Application.UI;

namespace campusLove
{
    class Program
    {
        static void Main(string[] args)
        {
            // Iniciar la aplicación
            try
            {
                // Crear y ejecutar el menú principal
                MenuPrincipal menu = new MenuPrincipal();
                menu.MostrarMenu();
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