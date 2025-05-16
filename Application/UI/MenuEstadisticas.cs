/* using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using System.IO;

namespace campusLove.Application.UI
{
    public class MenuMatch
    {
        private readonly string connectionString;
        private readonly int usuarioActualId;

        public MenuMatch(int usuarioActualId)
        {
            // mirar bien lo de la conexion de la base de datos 
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db", "insert.msql");
            connectionString = $"Server=localhost;Database={dbPath};Uid=root;Pwd=;";
            this.usuarioActualId = usuarioActualId;
        }

        public void MostrarMenu()
        {
            bool salir = false;
            int indiceActual = 0;
            List<Dictionary<string, object>> posiblesMatches = ObtenerPosiblesMatches();

            while (!salir)
            {
                Console.Clear();
                
                if (posiblesMatches.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
                    Console.WriteLine("║                                                        ║");
                    Console.WriteLine("║       ¡No hay más perfiles disponibles por ahora!      ║");
                    Console.WriteLine("║                                                        ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                    Console.ResetColor();
                    Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
                    Console.ReadKey();
                    return;
                }

                if (indiceActual >= posiblesMatches.Count)
                {
                    indiceActual = 0;
                }

                var usuario = posiblesMatches[indiceActual];
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
                Console.WriteLine("║                   PERFIL DE USUARIO                    ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine($"║ 👤 Nombre: {PadRight($"{usuario["nombre"]} {usuario["apellido"]}", 42)} ║");
                Console.WriteLine($"║ 🆔 Usuario: {PadRight(usuario["nombre_usuario"].ToString(), 41)} ║");
                Console.WriteLine($"║ 🎓 Carrera: {PadRight(usuario["carrera"].ToString(), 41)} ║");
                Console.WriteLine($"║ 🚻 Género: {PadRight(usuario["genero"].ToString(), 42)} ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 🎯 INTERESES:                                          ║");
                
                string intereses = usuario["intereses"].ToString();
                if (intereses.Length > 45)
                {
                    int mitad = intereses.Length / 2;
                    int indiceEspacio = intereses.IndexOf(' ', mitad);
                    if (indiceEspacio == -1) indiceEspacio = mitad;
                    
                    string primeraLinea = intereses.Substring(0, indiceEspacio);
                    string segundaLinea = intereses.Substring(indiceEspacio).Trim();
                    
                    Console.WriteLine($"║ {PadRight(primeraLinea, 54)} ║");
                    Console.WriteLine($"║ {PadRight(segundaLinea, 54)} ║");
                }
                else
                {
                    Console.WriteLine($"║ {PadRight(intereses, 54)} ║");
                }
                
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");

                // Mostrar opciones
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║ OPCIONES:                                             ║");
                Console.WriteLine("║   L - ❤️  Me gusta                                     ║");
                Console.WriteLine("║   D - 👎 No me interesa                               ║");
                Console.WriteLine("║   N - ➡️  Siguiente perfil                            ║");
                Console.WriteLine("║   P - ⬅️  Perfil anterior                             ║");
                Console.WriteLine("║   S - 🚪 Salir                                        ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.Write("🡺 Seleccione una opción: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.WriteLine();

                switch (char.ToUpper(keyInfo.KeyChar))
                {
                    case 'L':
                        RegistrarAccion(Convert.ToInt32(usuario["id"]), "like");
                        Console.WriteLine("Has dado like a este perfil.");
                        Console.ReadKey();
                        indiceActual++;
                        break;
                    case 'D':
                        RegistrarAccion(Convert.ToInt32(usuario["id"]), "dislike");
                        Console.WriteLine("Has pasado este perfil.");
                        Console.ReadKey();
                        indiceActual++;
                        break;
                    case 'N':
                        indiceActual++;
                        if (indiceActual >= posiblesMatches.Count)
                            indiceActual = 0;
                        break;
                    case 'P':
                        indiceActual--;
                        if (indiceActual < 0)
                            indiceActual = posiblesMatches.Count - 1;
                        break;
                    case 'S':
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("⚠️ Opción no válida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private List<Dictionary<string, object>> ObtenerPosiblesMatches()
        {
            List<Dictionary<string, object>> usuarios = new List<Dictionary<string, object>>();
            
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // Consulta para obtener usuarios que podrían ser match
                    string query = @"
                        SELECT u.id, u.nombre, u.apellido, u.nombre_usuario, u.carrera, 
                               u.intereses, u.genero
                        FROM usuarios u
                        WHERE u.id != @usuarioId
                        AND u.id NOT IN (
                            SELECT usuario_destino_id 
                            FROM likes 
                            WHERE usuario_origen_id = @usuarioId
                        )
                        AND u.id NOT IN (
                            SELECT usuario_destino_id 
                            FROM dislikes 
                            WHERE usuario_origen_id = @usuarioId
                        )
                        ORDER BY RAND()
                        LIMIT 10";
                    
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@usuarioId", usuarioActualId);
                        
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> usuario = new Dictionary<string, object>();
                                
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    usuario[reader.GetName(i)] = reader.GetValue(i);
                                }
                                
                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al obtener perfiles: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }
            
            return usuarios;
        }

        private void RegistrarAccion(int usuarioDestinoId, string tipoAccion)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    string tabla = tipoAccion == "like" ? "likes" : "dislikes";
                    
                    string query = $@"
                        INSERT INTO {tabla} (usuario_origen_id, usuario_destino_id, fecha_creacion)
                        VALUES (@usuarioOrigenId, @usuarioDestinoId, NOW())";
                    
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@usuarioOrigenId", usuarioActualId);
                        command.Parameters.AddWithValue("@usuarioDestinoId", usuarioDestinoId);
                        
                        command.ExecuteNonQuery();
                    }
                    
                    if (tipoAccion == "like")
                    {
                        string matchQuery = @"
                            SELECT COUNT(*) FROM likes
                            WHERE usuario_origen_id = @usuarioDestinoId
                            AND usuario_destino_id = @usuarioOrigenId";
                        
                        using (MySqlCommand matchCommand = new MySqlCommand(matchQuery, connection))
                        {
                            matchCommand.Parameters.AddWithValue("@usuarioOrigenId", usuarioActualId);
                            matchCommand.Parameters.AddWithValue("@usuarioDestinoId", usuarioDestinoId);
                            
                            int matchCount = Convert.ToInt32(matchCommand.ExecuteScalar());
                            
                            if (matchCount > 0)
                            {
                                // Es un match
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                                Console.WriteLine("║                                                        ║");
                                Console.WriteLine("║                   ¡¡¡ES UN MATCH!!!                    ║");
                                Console.WriteLine("║                                                        ║");
                                Console.WriteLine("║                      ❤️  ❤️  ❤️                       ║");
                                Console.WriteLine("║                                                        ║");
                                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error al registrar acción: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        private string PadRight(string text, int totalWidth)
        {
            if (string.IsNullOrEmpty(text))
                return new string(' ', totalWidth);
                
            if (text.Length > totalWidth)
                return text.Substring(0, totalWidth - 3) + "...";
                
            return text + new string(' ', totalWidth - text.Length);
        }
    }
} */