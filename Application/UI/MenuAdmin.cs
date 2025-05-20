using System;
using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Infrastructure.Repositories;

namespace CampusLove.UI.Console
{
    public class MenuAdministrador
    {
        private static ConsoleColor colorTitulo = ConsoleColor.DarkBlue;
        private static ConsoleColor colorBorde = ConsoleColor.Blue;
        private static ConsoleColor colorOpcion = ConsoleColor.Cyan;
        private static ConsoleColor colorMensaje = ConsoleColor.Green;
        private static ConsoleColor colorError = ConsoleColor.Red;
        private static ConsoleColor colorDato = ConsoleColor.Yellow;
        
        private readonly AdminRepository AdminRepository;
        private Administrador _adminActual;
        
        public MenuAdministrador(string conexionString, Administrador adminActual)
        {
            AdminRepository = new AdministradorRepository(conexionString);
            _adminActual = adminActual;
        }
        
        public void MostrarMenu()
        {
            bool salir = false;
            
            while (!salir)
            {
                System.Console.Clear();
                
                System.Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                System.Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
                System.Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine("║ 📝 Administrador                                       ║");
                System.Console.WriteLine("║   1. Ver perfiles Administrador                        ║");
                System.Console.WriteLine("║   2. Editar perfiles Administrador                     ║");
                System.Console.WriteLine("║   3. Eliminar Administrador                            ║");
                System.Console.WriteLine("║   4. Ver perfiles registrados                          ║");
                System.Console.WriteLine("║   0. Volver al menú principal                          ║");
                System.Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                System.Console.ResetColor();
                System.Console.Write("\nSeleccione una opción: ");
                string? opcion = System.Console.ReadLine();
                
                MostrarBordeInferior();
                
                MostrarInfoAdministrador();
                
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("\n  Selecciona una opción: ");
                string? opcion = System.Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        VerPerfilesAdministrador();
                        break;
                    case "2":
                        EditarPerfilAdministrador();
                        break;
                    case "3":
                        EliminarAdministrador();
                        break;
                    case "4":
                        VerPerfilesRegistrados();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        MostrarMensajeError("Opción no válida. Intenta de nuevo.");
                        break;
                }
            }
        }
        
        private void MostrarTitulo()
        {
            System.Console.ForegroundColor = colorBorde;
            System.Console.WriteLine("\n  ╔════════════════════════════════════════════════════╗");
            System.Console.ForegroundColor = colorTitulo;
            System.Console.WriteLine("  ║              PANEL DE ADMINISTRADOR                  ║");
            System.Console.ForegroundColor = colorBorde;
            System.Console.WriteLine("  ╠════════════════════════════════════════════════════╣");
        }
        private void MostrarBordeInferior()
        {
            System.Console.ForegroundColor = colorBorde;
            System.Console.WriteLine("  ╚════════════════════════════════════════════════════╝");
        }
        
        private void MostrarInfoAdministrador()
        {
            System.Console.ForegroundColor = colorDato;
            System.Console.WriteLine($"\n  Administrador: {_adminActual.Nombre} | Nivel: {_adminActual.NivelAcceso} | Último acceso: {_adminActual.UltimoAcceso}");
        }
        
        private void MostrarMensajeExito(string mensaje)
        {
            System.Console.ForegroundColor = colorMensaje;
            System.Console.WriteLine($"\n  {mensaje}");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\n  Presiona cualquier tecla para continuar...");
            System.Console.ReadKey();
        }
        
        private void MostrarMensajeError(string mensaje)
        {
            System.Console.ForegroundColor = colorError;
            System.Console.WriteLine($"\n  {mensaje}");
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\n  Presiona cualquier tecla para continuar...");
            System.Console.ReadKey();
        }
        
        private void VerPerfilesAdministrador()
        {
            System.Console.Clear();
            MostrarTitulo();
            
            var administradores = _adminRepo.ObtenerTodosAdministradores();
            
            if (administradores.Count == 0)
            {
                System.Console.ForegroundColor = colorMensaje;
                System.Console.WriteLine("\n  No hay administradores registrados.");
            }
            else
            {
                System.Console.ForegroundColor = colorDato;
                System.Console.WriteLine("\n  LISTA DE ADMINISTRADORES");
                System.Console.WriteLine("\n  ID | Nombre | Usuario | Correo | Nivel | Estado");
                System.Console.WriteLine("  ------------------------------------------------");
                
                foreach (var admin in administradores)
                {
                    string estado = admin.Activo ? "Activo" : "Inactivo";
                    
                    if (admin.Id == _adminActual.Id)
                    {
                        System.Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        System.Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                    System.Console.WriteLine($"  {admin.Id} | {admin.Nombre} | {admin.Usuario} | {admin.Correo} | {admin.NivelAcceso} | {estado}");
                }
            }
            
            MostrarBordeInferior();
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\n  Presiona cualquier tecla para volver...");
            System.Console.ReadKey();
        }
        
        // editar perfil de administrador
        private void EditarPerfilAdministrador()
        {
            System.Console.Clear();
            MostrarTitulo();
            
            if (_adminActual.NivelAcceso < 2)
            {
                MostrarMensajeError("No tienes permisos suficientes para editar perfiles de administrador.");
                return;
            }
            
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write("\n  Ingresa el ID del administrador a editar: ");
            if (!int.TryParse(System.Console.ReadLine(), out int adminId))
            {
                MostrarMensajeError("ID no válido.");
                return;
            }
            
            var admin = _adminRepo.ObtenerAdministradorPorId(adminId);
            
            if (admin == null)
            {
                MostrarMensajeError("Administrador no encontrado.");
                return;
            }
            
            if (admin.NivelAcceso > _adminActual.NivelAcceso)
            {
                MostrarMensajeError("No puedes editar a un administrador de nivel superior.");
                return;
            }

            System.Console.ForegroundColor = colorDato;
            System.Console.WriteLine("\n  DATOS ACTUALES:");
            System.Console.WriteLine($"  ID: {admin.Id}");
            System.Console.WriteLine($"  Nombre: {admin.Nombre}");
            System.Console.WriteLine($"  Usuario: {admin.Usuario}");
            System.Console.WriteLine($"  Correo: {admin.Correo}");
            System.Console.WriteLine($"  Nivel de acceso: {admin.NivelAcceso}");
            System.Console.WriteLine($"  Estado: {(admin.Activo ? "Activo" : "Inactivo")}");
            
            
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\n  NUEVOS DATOS (deja en blanco para mantener el valor actual):");
            
            System.Console.Write("  Nombre: ");
            string nombre = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                admin.Nombre = nombre;
            }
            
            System.Console.Write("  Usuario: ");
            string usuario = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(usuario))
            {
                admin.Usuario = usuario;
            }
            
            System.Console.Write("  Correo: ");
            string correo = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(correo))
            {
                admin.Correo = correo;
            }
            
            System.Console.Write("  Nivel de acceso (1-3): ");
            string nivelStr = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nivelStr) && int.TryParse(nivelStr, out int nivel))
            {
                if (nivel > _adminActual.NivelAcceso)
                {
                    MostrarMensajeError("No puedes asignar un nivel superior al tuyo.");
                    return;
                }
                
                admin.NivelAcceso = nivel;
            }
            
            System.Console.Write("  Estado (A: Activo, I: Inactivo): ");
            string estadoStr = System.Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(estadoStr))
            {
                admin.Activo = estadoStr.ToUpper() == "A";
            }
            
            System.Console.Write("  ¿Cambiar contraseña? (S/N): ");
            string cambiarPass = System.Console.ReadLine();
            if (cambiarPass.ToUpper() == "S")
            {
                System.Console.Write("  Nueva contraseña: ");
                string nuevaPass = System.Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(nuevaPass))
                {
                   
                    bool resultadoPass = _adminRepo.CambiarContrasena(admin.Id, nuevaPass);
                    if (!resultadoPass)
                    {
                        MostrarMensajeError("Error al cambiar la contraseña.");
                    }
                }
            }
            
            bool resultado = _adminRepo.ActualizarAdministrador(admin);
            
            if (resultado)
            {
                MostrarMensajeExito("Administrador actualizado con éxito.");
                
                if (admin.Id == _adminActual.Id)
                {
                    _adminActual = admin;
                }
            }
            else
            {
                MostrarMensajeError("Error al actualizar el administrador.");
            }
        }
        
        private void EliminarAdministrador()
        {
            System.Console.Clear();
            MostrarTitulo();
            
            if (_adminActual.NivelAcceso < 3)
            {
                MostrarMensajeError("No tienes permisos suficientes para eliminar administradores.");
                return;
            }
            
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write("\n  Ingresa el ID del administrador a eliminar: ");
            if (!int.TryParse(System.Console.ReadLine(), out int adminId))
            {
                MostrarMensajeError("ID no válido.");
                return;
            }
            
            if (adminId == _adminActual.Id)
            {
                MostrarMensajeError("No puedes eliminarte a ti mismo.");
                return;
            }
            
            var admin = _adminRepo.ObtenerAdministradorPorId(adminId);
            
            if (admin == null)
            {
                MostrarMensajeError("Administrador no encontrado.");
                return;
            }
            
            if (admin.NivelAcceso > _adminActual.NivelAcceso)
            {
                MostrarMensajeError("No puedes eliminar a un administrador de nivel superior.");
                return;
            }
            
            System.Console.ForegroundColor = colorError;
            System.Console.WriteLine($"\n  ¿Estás seguro de que deseas eliminar al administrador {admin.Nombre}?");
            System.Console.Write("  Esta acción no se puede deshacer (S/N): ");
            string confirmacion = System.Console.ReadLine();
            
            if (confirmacion.ToUpper() != "S")
            {
                MostrarMensajeExito("Operación cancelada.");
                return;
            }
            
            bool resultado = _adminRepo.EliminarAdministrador(adminId);
            
            if (resultado)
            {
                MostrarMensajeExito("Administrador eliminado con éxito.");
            }
            else
            {
                MostrarMensajeError("Error al eliminar el administrador.");
            }
        }
        
        private void VerPerfilesRegistrados()
        {
            System.Console.Clear();
            MostrarTitulo();
            
            var usuarios = _adminRepo.ObtenerTodosUsuarios();
            
            if (usuarios.Count == 0)
            {
                System.Console.ForegroundColor = colorMensaje;
                System.Console.WriteLine("\n  No hay usuarios registrados.");
            }
            else
            {
                System.Console.ForegroundColor = colorDato;
                System.Console.WriteLine("\n  LISTA DE USUARIOS REGISTRADOS");
                System.Console.WriteLine("\n  ID | Nombre | Género | Edad | Carrera");
                System.Console.WriteLine("  ----------------------------------------");
                
                foreach (var usuario in usuarios)
                {
                    System.Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine($"  {usuario.Id} | {usuario.Nombre} | {usuario.Genero} | {usuario.Edad} | {usuario.Carrera}");
                }
                
                System.Console.ForegroundColor = colorOpcion;
                System.Console.WriteLine("\n  Opciones:");
                System.Console.WriteLine("  1. Ver detalles de un usuario");
                System.Console.WriteLine("  0. Volver");
                
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write("\n  Selecciona una opción: ");
                string opcion = System.Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        VerDetallesUsuario(usuarios);
                        break;
                    case "0":
                        break;
                    default:
                        MostrarMensajeError("Opción no válida.");
                        break;
                }
            }
            
            MostrarBordeInferior();
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\n  Presiona cualquier tecla para volver...");
            System.Console.ReadKey();
        }
        
        private void VerDetallesUsuario(List<Usuario> usuarios)
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.Write("\n  Ingresa el ID del usuario: ");
            if (!int.TryParse(System.Console.ReadLine(), out int usuarioId))
            {
                MostrarMensajeError("ID no válido.");
                return;
            }
            
            var usuario = usuarios.Find(u => u.Id == usuarioId);
            
            if (usuario == null)
            {
                MostrarMensajeError("Usuario no encontrado.");
                return;
            }
            
            System.Console.Clear();
            MostrarTitulo();
            
            System.Console.ForegroundColor = colorDato;
            System.Console.WriteLine("\n  DETALLES DEL USUARIO");
            System.Console.WriteLine($"\n  ID: {usuario.Id}");
            System.Console.WriteLine($"  Nombre: {usuario.Nombre}");
            System.Console.WriteLine($"  Género: {usuario.Genero}");
            System.Console.WriteLine($"  Edad: {usuario.Edad}");
            System.Console.WriteLine($"  Carrera: {usuario.Carrera}");
            System.Console.WriteLine($"  Frase de perfil: {usuario.FrasePerfil ?? "No especificada"}");
            System.Console.WriteLine($"  Ciudad ID: {usuario.IdCiudad}");
            
            // Aquí se podrían mostrar más detalles como interacciones, matches, etc.
            
            MostrarBordeInferior();
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("\n  Presiona cualquier tecla para volver...");
            System.Console.ReadKey();
        }
    }
}
