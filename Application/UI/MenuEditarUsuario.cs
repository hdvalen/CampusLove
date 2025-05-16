using System;
using campusLove.Application.UI;
using campusLove.Domain.Entities;

namespace campusLove.Application.UI
{
    public class MenuEditarUsuario
    {
        private object usuario;

        public void MostrarMenu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║              ✨ MENÚ DE EDICIÓN DE USUARIO ✨         ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 🧍 INFORMACIÓN PERSONAL                               ║");
                Console.WriteLine("║   1. 📝 Cambiar Nombre                                ║");
                Console.WriteLine("║   2. 🎂 Cambiar Edad                                  ║");
                Console.WriteLine("║   3. 🚻 Cambiar Género                                ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 🎯 PREFERENCIAS E INTERESES                           ║");
                Console.WriteLine("║   4. ✏️ Modificar Intereses                           ║");
                Console.WriteLine("║   5. 🎓 Actualizar Carrera                            ║");
                Console.WriteLine("║   6. 💬 Editar Frase de Perfil                        ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 💾 ACCIONES                                           ║");
                Console.WriteLine("║   7. ✅ Guardar Cambios                               ║");
                Console.WriteLine("║   0. ❌ Salir sin Guardar                             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("🡺 Seleccione una opción: ");
                string opcion = Console.ReadLine();
                Console.ResetColor();

                switch (opcion)
                {
                    case "1":
                        MostrarEncabezado("Cambiar Nombre");
                        string nuevoNombre = LeerEntrada("Ingrese el nuevo nombre: ");
                        Usuario.Nombre = nuevoNombre;
                        MostrarMensaje("✅ Nombre actualizado correctamente.", ConsoleColor.Green);
                        break;

                    case "2":
                        MostrarEncabezado("Cambiar Edad");
                        int nuevaEdad = LeerEnteroPositivo("Ingrese la nueva edad: ");
                        usuario.Edad = nuevaEdad;
                        MostrarMensaje("✅ Edad actualizada correctamente.", ConsoleColor.Green);
                        break;

                    case "3":
                        MostrarEncabezado("Cambiar Género");
                        string nuevoGenero = LeerEntrada("Ingrese el nuevo género: ");
                        usuario.Genero = nuevoGenero;
                        MostrarMensaje("✅ Género actualizado correctamente.", ConsoleColor.Green);
                        break;

                    case "4":
                        MostrarEncabezado("Modificar Intereses");
                        string nuevosIntereses = LeerEntrada("Ingrese nuevos intereses separados por coma: ");
                        usuario.Intereses = nuevosIntereses;
                        MostrarMensaje("✅ Intereses actualizados correctamente.", ConsoleColor.Green);
                        break;

                    case "5":
                        MostrarEncabezado("Actualizar Carrera");
                        string nuevaCarrera = LeerEntrada("Ingrese la nueva carrera: ");
                        Usuarios.Carrera = nuevaCarrera;
                        MostrarMensaje("✅ Carrera actualizada correctamente.", ConsoleColor.Green);
                        break;

                    case "6":
                        MostrarEncabezado("Editar Frase de Perfil");
                        string nuevaFrase = LeerEntrada("Ingrese la nueva frase de perfil: ");
                        usuario.FrasePerfil = nuevaFrase;
                        MostrarMensaje("✅ Frase de perfil actualizada.", ConsoleColor.Green);
                        break;

                    case "7":
                        MostrarEncabezado("Guardar Cambios");
                        MostrarMensaje("💾 Cambios guardados con éxito.", ConsoleColor.Green);
                        break;

                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.", ConsoleColor.DarkGreen);
                        break;
                }
            }
        }

        private int LeerEnteroPositivo(string v)
        {
            throw new NotImplementedException();
        }

        private string LeerEntrada(string v)
        {
            throw new NotImplementedException();
        }

        private void MostrarMensaje(string v, ConsoleColor green)
        {
            throw new NotImplementedException();
        }

        private void MostrarEncabezado(string v)
        {
            throw new NotImplementedException();
        }
    }
