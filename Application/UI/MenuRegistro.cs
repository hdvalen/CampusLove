using System;
using System.IO;

namespace SistemaPerfiles
{
    class Program
    {
public class MenuRegistro
    {
        private readonly RepositoryRegistro _repositoryRegistro;

            public MenuRegistro()
            {
                _repositoryRegistro = new RepositoryRegistro();
        }
        
        public void MostrarMenu()
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
                        CrearPerfil();
                        break;
                    case "2":
                        VerDetallePerfil();
                        break;
                    case "3":
                        EditarPerfil();
                        break;
                    case "4":
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
                }
            }
        }
        
        
               private async Task NuevoProducto()
        {
            Console.Clear();
            MenuPrincipal.MostrarEncabezado("NUEVO PRODUCTO");

            try
            {
                string id = MenuPrincipal.LeerEntrada("\nIngrese el ID del producto: ");

                // Verificar si ya existe
                var existe = await _productoRepository.GetByIdAsync(id);
                if (existe != null)
                {
                    MenuPrincipal.MostrarMensaje("\n ⚠ Error: Producto ya existente.", ConsoleColor.Red);
                    Console.ReadKey();
                    return;
                }

                string nombre = MenuPrincipal.LeerEntrada("Ingrese el nombre del producto: ");
                string codigoBarra = MenuPrincipal.LeerEntrada("Ingrese el código de barras: ");
                int stock = MenuPrincipal.LeerEnteroPositivo("Ingrese el stock inicial: ");
                int stockMin = MenuPrincipal.LeerEnteroPositivo("Ingrese el stock mínimo: ");
                int stockMax = MenuPrincipal.LeerEnteroPositivo("Ingrese el stock máximo: ");

                var producto = new Producto
                {
                    Id = id,
                    Nombre = nombre,
                    Stock = stock,
                    StockMin = stockMin,
                    StockMax = stockMax,
                    Fecha_Creacion = DateTime.Now,
                    Fecha_Actualizacion = DateTime.Now,
                    Codigo_Barra = codigoBarra
                };

                bool resultado = await _productoRepository.InsertAsync(producto);

                if (resultado)
                {
                    MenuPrincipal.MostrarMensaje("\n ✔ Producto registrado correctamente.", ConsoleColor.Green);
                }
                else
                {
                    MenuPrincipal.MostrarMensaje("\nNo se pudo registrar el producto.", ConsoleColor.Red);
                }
            }
            catch (Exception ex)
            {
                MenuPrincipal.MostrarMensaje($"\n ⚠ Error al registrar el producto: {ex.Message}", ConsoleColor.Red);
            }

            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private async Task ActualizarProducto()
        {
            Console.Clear();
            MenuPrincipal.MostrarEncabezado("ACTUALIZAR PRODUCTO");
            
            string id = MenuPrincipal.LeerEntrada("\nIngrese el ID del producto : ");
            
            try
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                
                if (producto == null)
                {
                    MenuPrincipal.MostrarMensaje("\nEl producto no existe.", ConsoleColor.DarkMagenta);
                }
                else
                {
                    Console.WriteLine($"\nProducto actual: {producto.Nombre}");
                    
                    string nombre = MenuPrincipal.LeerEntrada($"Ingrese el nuevo nombre ({producto.Nombre}): ");
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        producto.Nombre = nombre;
                    }
                    
                    string codigoBarra = MenuPrincipal.LeerEntrada($"Ingrese el nuevo código de barras ({producto.Codigo_Barra}): ");
                    if (!string.IsNullOrWhiteSpace(codigoBarra))
                    {
                        producto.Codigo_Barra = codigoBarra;
                    }
                    
                    Console.Write($"Ingrese el nuevo stock ({producto.Stock}): ");
                    string stockStr = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(stockStr) && int.TryParse(stockStr, out int stock) && stock >= 0)
                    {
                        producto.Stock = stock;
                    }
                    
                    Console.Write($"Ingrese el nuevo stock mínimo ({producto.StockMin}): ");
                    string stockMinStr = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(stockMinStr) && int.TryParse(stockMinStr, out int stockMin) && stockMin >= 0)
                    {
                        producto.StockMin = stockMin;
                    }
                    
                    Console.Write($"Ingrese el nuevo stock máximo ({producto.StockMax}): ");
                    string stockMaxStr = Console.ReadLine() ?? "";
                    if (!string.IsNullOrWhiteSpace(stockMaxStr) && int.TryParse(stockMaxStr, out int stockMax) && stockMax >= 0)
                    {
                        producto.StockMax = stockMax;
                    }
                    
                    producto.Fecha_Actualizacion = DateTime.Now;
                    
                    bool resultado = await _productoRepository.UpdateAsync(producto);
                    
                    if (resultado)
                    {
                        MenuPrincipal.MostrarMensaje("\n ✔ Producto actualizado correctamente.", ConsoleColor.Green);
                    }
                    else
                    {
                        MenuPrincipal.MostrarMensaje("\nNo se pudo actualizar el producto.", ConsoleColor.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                MenuPrincipal.MostrarMensaje($"\n ⚠ Error al actualizar el producto: {ex.Message}", ConsoleColor.Red);
            }
            
            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private async Task EliminarProducto()
        {
            Console.Clear();
            MenuPrincipal.MostrarEncabezado("ELIMINAR PRODUCTO");
            
            string id = MenuPrincipal.LeerEntrada("\nIngrese el ID del producto a eliminar: ");
            
            try
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                
                if (producto == null)
                {
                    MenuPrincipal.MostrarMensaje("\nEl producto no existe.", ConsoleColor.DarkMagenta);
                }
                else
                {
                    Console.WriteLine($"\nProducto a eliminar: {producto.Nombre}");
                    
                    string confirmacion = MenuPrincipal.LeerEntrada("\n¿Está seguro de eliminar este producto? (S/N): ");
                    
                    if (confirmacion.ToUpper() == "S")
                    {
                        bool resultado = await _productoRepository.DeleteAsync(id);
                        
                        if (resultado)
                        {
                            MenuPrincipal.MostrarMensaje("\n ✔ Producto eliminado correctamente.", ConsoleColor.Green);
                        }
                        else
                        {
                            MenuPrincipal.MostrarMensaje("\nNo se pudo eliminar el producto.", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        MenuPrincipal.MostrarMensaje("\nOperación cancelada.", ConsoleColor.DarkMagenta);
                    }
                }
            }
            catch (Exception ex)
            {
                MenuPrincipal.MostrarMensaje($"\n ⚠ Error al eliminar el producto: {ex.Message}", ConsoleColor.Red);
            }
            
            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        
        private async Task ProductosStockBajo()
        {
            Console.Clear();
            MenuPrincipal.MostrarEncabezado("PRODUCTOS CON STOCK BAJO");
            
            try
            {
                var productos = await _productoRepository.GetProductosBajoStockAsync();
                
                if (!productos.Any())
                {
                    MenuPrincipal.MostrarMensaje("\nNo hay productos con stock bajo.", ConsoleColor.Green);
                }
                else
                {
                    MenuPrincipal.MostrarMensaje($"\nSe encontraron {productos.Count()} productos con stock bajo o crítico.", ConsoleColor.DarkMagenta);
                    
                    Console.WriteLine("\n{0,-10} {1,-30} {2,-8} {3,-8} {4,-8}", "ID", "Nombre", "Stock", "Mínimo", "Estado");
                    Console.WriteLine(new string('-', 70));
                    
                    foreach (var producto in productos)
                    {
                        string estado = producto.Stock == 0 ? "CRÍTICO" : "BAJO";
                        ConsoleColor color = producto.Stock == 0 ? ConsoleColor.Red : ConsoleColor.Yellow;
                        
                        Console.ForegroundColor = color;
                        Console.WriteLine("{0,-10} {1,-30} {2,-8} {3,-8} {4,-8}", 
                            producto.Id, 
                            producto.Nombre.Length > 27 ? producto.Nombre.Substring(0, 27) + "..." : producto.Nombre, 
                            producto.Stock, 
                            producto.StockMin,
                            estado);
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception ex)
            {
                MenuPrincipal.MostrarMensaje($"\n ⚠ Error al buscar productos con stock bajo: {ex.Message}", ConsoleColor.Red);
            }
            
            Console.Write("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
        
