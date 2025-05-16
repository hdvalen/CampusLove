using System;
using

using campusLove.Domain.Entities;


namespace MenuPerfil
{
    public class MenuUsuarios
    {
        private readonly MenuUsuarios _menuUsuarios;
        
        public MenuUsuarios()
        {
            RepositoryUsuarios = new _repositoryUsuarios();
        }
        
        public void MenuUsuarios()
        {
            bool salir = false;
            
            while (!salir)
            {
                Console.Clear();
                MenuPrincipal.MostrarEncabezado("MENÚ DE USUARIOS");
                Console.WriteLine("2. Ver detalle de usuario");
                Console.WriteLine("3. Editar usuario");
                Console.WriteLine("0. Regresar al menú principal");
                
                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine() ?? "";
                
                switch (opcion)
                {
                    case "2":
                        VerDetalleUsuario().Wait();
                        break;
                    case "3":
                        EditarUsuario().Wait();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        MenuPrincipal.MostrarMensaje("Opción no válida. Intente de nuevo.", ConsoleColor.DarkMagenta);
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        private async Task ListarProductos()
        {
            Console.Clear();
            MenuPrincipal.MostrarEncabezado("MENÚ USUARIOS");
            
            try
            {
                var productos = await _repositoryUsuarios.GetAllAsync();
                
                if (!productos.Any())
                {
                    MenuPrincipal.MostrarMensaje("\nNo hay productos registrados.", ConsoleColor.DarkMagenta);
                }
                else
                {
                    Console.WriteLine("\n{0,-10} {1,-30} {2,-8} {3,-15}", "ID", "Nombre", "Edad", "Genero", "Intereses", "Carrera", "FrasePerfil");
                    Console.WriteLine(new string('-', 70));
                    
                    foreach (var producto in productos)
                    {
                        Console.WriteLine("{0,-10} {1,-30} {2,-8} {3,-15}", 
                            usuario.Id, 
                            usuario.Nombre.Length > 27 ? producto.Nombre.Substring(0, 27) + "..." : producto.Nombre, 
                            usuario.Intereses.Length > 27 ? usuario.Intereses.Substring(0, 27) + "..." : usuario.Intereses,
                            usuario.Carrera.Length > 27 ? usuario.Carrera.Substring(0, 27) + "..." : usuario.Carrera,
                            usuario.FrasePerfil.Length > 27 ? usuario.FrasePerfil.Substring(0, 27) + "..." : usuario.FrasePerfil);
                        Console.WriteLine(new string('-', 70));
                    }
                }
            }
            catch (Exception ex)
            {
                MenuPrincipal.MostrarMensaje($"\n ⚠ Error al listar productos: {ex.Message}", ConsoleColor.Red);
            }
            
            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private async Task BuscarProducto()
        {
            Console.Clear();
            MenuPrincipal.MostrarEncabezado("BUSCAR USUARIO");
            
            string id = MenuPrincipal.LeerEntrada("\nIngrese el ID usuario: ");
            
            try
            {
                var producto = await _repositoryUsuario.GetByIdAsync(id);
                
                if (producto == null)
                {
                    MenuPrincipal.MostrarMensaje("\nEl producto no existe.", ConsoleColor.DarkMagenta);
                }
                else
                {
                    Console.WriteLine("\nINFORMACIÓN DEL USUARIO:");
                    Console.WriteLine($"ID: {usuario.Id}");
                    Console.WriteLine($"Nombre: {usuario.Nombre}");
                    Console.WriteLine($"Stock: {usuario.Edad}");
                    Console.WriteLine($"Stock Mínimo: {usuario.Genero}");
                    Console.WriteLine($"Stock Actual: {usuario.Intereses}");    
                    Console.WriteLine($"Stock Mínimo: {usuario.Carrera}");
                    Console.WriteLine($"Stock Actual: {usuario.FrasePerfil}");
                    
                    
            catch (Exception ex)
            {
                MenuPrincipal.MostrarMensaje($"\n ⚠ Error al buscar usuario: {ex.Message}", ConsoleColor.Red);
            }
            
            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
 