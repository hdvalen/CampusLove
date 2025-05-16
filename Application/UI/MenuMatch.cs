/* using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;
using System.IO;
    public class MenuMatch
        {
            private readonly Db dbService;
            private readonly int usuarioActualId;
            private List<Usuario> posiblesMatches;
            private int indiceActual = 0;

            public MenuMatch(int usuarioId)
            {
                dbService = new Db();
                usuarioActualId = usuarioId;
                CargarPosiblesMatches();
            }

            private void CargarPosiblesMatches()
            {
                // Consulta para obtener usuarios que podrían ser match
                // Excluye al usuario actual y a los que ya recibieron like o dislike
                string query = @"
                        SELECT u.id, u.nombre, u.apellido, u.nombre_usuario, u.carrera, 
                            u.intereses, u.genero, u.foto_perfil, u.frase_perfil, u.edad
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
                        LIMIT 20";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@usuarioId", usuarioActualId }
                    };

                DataTable result = dbService.ExecuteQuery(query, parameters);
                posiblesMatches = new List<Usuario>();

                foreach (DataRow row in result.Rows)
                {
                    posiblesMatches.Add(new Usuario
                    {
                        Id = Convert.ToInt32(row["id"]),
                        NombreCompleto = $"{row["nombre"]} {row["apellido"]}",
                        NombreUsuario = row["nombre_usuario"].ToString(),
                        Carrera = row["carrera"].ToString(),
                        Intereses = row["intereses"].ToString(),
                        Genero = row["genero"].ToString(),
                        FotoPerfil = row["foto_perfil"].ToString(),
                        FrasePerfil = row["frase_perfil"].ToString(),
                        Edad = Convert.ToInt32(row["edad"])
                    });
                }
            }

            public void MostrarMenu()
            {
                bool salir = false;

                while (!salir)
                {
                    if (posiblesMatches.Count == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                        Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
                        Console.WriteLine("║                                                        ║");
                        Console.WriteLine("║       ¡No hay más perfiles disponibles por ahora!      ║");
                        Console.WriteLine("║       Vuelve más tarde para encontrar nuevos matches   ║");
                        Console.WriteLine("║                                                        ║");
                        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                        Console.ResetColor();
                        Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
                        Console.ReadKey();
                        salir = true;
                        continue;
                    }

                    if (indiceActual >= posiblesMatches.Count)
                    {
                        indiceActual = 0;
                        CargarPosiblesMatches();
                        if (posiblesMatches.Count == 0)
                            continue;
                    }

                    Usuario usuarioActual = posiblesMatches[indiceActual];
                    MostrarPerfilUsuario(usuarioActual);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║ OPCIONES:                                              ║");
                    Console.WriteLine("║   L - ❤️  Me gusta (Like)                             ║");
                    Console.WriteLine("║   D - 👎 No me interesa (Dislike)                     ║");
                    Console.WriteLine("║   S - 🚪 Salir                                        ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                    Console.ResetColor();

                    Console.Write("🡺 Seleccione una opción: ");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    Console.WriteLine();

                    switch (char.ToUpper(keyInfo.KeyChar))
                    {
                        case 'L':
                            DarLike(usuarioActual.Id);
                            indiceActual++;
                            break;
                        case 'D':
                            DarDislike(usuarioActual.Id);
                            indiceActual++;
                            break;
                        case 'S':
                            salir = true;
                            break;
                        default:
                            MostrarMensaje("⚠️ Opción no válida. Intente de nuevo.", ConsoleColor.Red);
                            break;
                    }
                }
            }

            private void MostrarPerfilUsuario(Usuario usuario)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                  ❤️ CAMPUS LOVE ❤️                    ║");
                Console.WriteLine("║                   PERFIL DE USUARIO                    ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.ResetColor();

                // Información del usuario
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine($"║ 👤 Nombre: {PadRight(usuario.NombreCompleto, 42)} ║");
                Console.WriteLine($"║ 🆔 Usuario: {PadRight(usuario.NombreUsuario, 41)} ║");
                Console.WriteLine($"║ 🎓 Carrera: {PadRight(usuario.Carrera, 41)} ║");
                Console.WriteLine($"║ 🚻 Género: {PadRight(usuario.Genero, 42)} ║");
                Console.WriteLine($"║ 🎂 Edad: {PadRight(usuario.Edad.ToString(), 44)} ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 🎯 INTERESES:                                          ║");

                string intereses = usuario.Intereses;
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

                Console.WriteLine("╠════════════════════════════════════════════════════════╣");

                // Frase de perfil
                if (!string.IsNullOrEmpty(usuario.FrasePerfil))
                {
                    Console.WriteLine("║ 💬 FRASE:                                              ║");

                    string frase = usuario.FrasePerfil;
                    if (frase.Length > 45)
                    {
                        int mitad = frase.Length / 2;
                        int indiceEspacio = frase.IndexOf(' ', mitad);
                        if (indiceEspacio == -1) indiceEspacio = mitad;

                        string primeraLinea = frase.Substring(0, indiceEspacio);
                        string segundaLinea = frase.Substring(indiceEspacio).Trim();

                        Console.WriteLine($"║ {PadRight(primeraLinea, 54)} ║");
                        Console.WriteLine($"║ {PadRight(segundaLinea, 54)} ║");
                    }
                    else
                    {
                        Console.WriteLine($"║ {PadRight(frase, 54)} ║");
                    }

                    Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                }

                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.ResetColor();
            }

            private string PadRight(string text, int totalWidth)
            {
                if (string.IsNullOrEmpty(text))
                    return new string(' ', totalWidth);

                if (text.Length > totalWidth)
                    return text.Substring(0, totalWidth - 3) + "...";

                return text + new string(' ', totalWidth - text.Length);
            }

            private void DarLike(int usuarioDestinoId)
            {
                string query = @"
                        INSERT INTO likes (usuario_origen_id, usuario_destino_id, fecha_creacion)
                        VALUES (@usuarioOrigenId, @usuarioDestinoId, NOW())";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@usuarioOrigenId", usuarioActualId },
                        { "@usuarioDestinoId", usuarioDestinoId }
                    };

                int result = dbService.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    // Verificar si hay match (si el otro usuario también dio like)
                    string matchQuery = @"
                            SELECT COUNT(*) AS match_count
                            FROM likes
                            WHERE usuario_origen_id = @usuarioDestinoId
                            AND usuario_destino_id = @usuarioOrigenId";

                    DataTable matchResult = dbService.ExecuteQuery(matchQuery, parameters);

                    if (matchResult.Rows.Count > 0 && Convert.ToInt32(matchResult.Rows[0]["match_count"]) > 0)
                    {
                        // Crear registro de match
                        string createMatchQuery = @"
                                INSERT INTO matches (usuario_id1, usuario_id2, fecha_creacion)
                                VALUES (@usuarioOrigenId, @usuarioDestinoId, NOW())";

                        dbService.ExecuteNonQuery(createMatchQuery, parameters);

                        // Mostrar mensaje de match
                        MostrarMensajeMatch();
                    }
                    else
                    {
                        MostrarMensaje("✅ Has dado like a este perfil.", ConsoleColor.Green);
                    }
                }
                else
                {
                    MostrarMensaje("❌ Error al dar like. Intente de nuevo.", ConsoleColor.Red);
                }
            }

            private void DarDislike(int usuarioDestinoId)
            {
                string query = @"
                        INSERT INTO dislikes (usuario_origen_id, usuario_destino_id, fecha_creacion)
                        VALUES (@usuarioOrigenId, @usuarioDestinoId, NOW())";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "@usuarioOrigenId", usuarioActualId },
                        { "@usuarioDestinoId", usuarioDestinoId }
                    };

                int result = dbService.ExecuteNonQuery(query, parameters);

                if (result > 0)
                {
                    MostrarMensaje("✅ Has pasado este perfil.", ConsoleColor.Yellow);
                }
                else
                {
                    MostrarMensaje("❌ Error al pasar perfil. Intente de nuevo.", ConsoleColor.Red);
                }
            }

            private void MostrarMensajeMatch()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                        ║");
                Console.WriteLine("║                   ¡¡¡ES UN MATCH!!!                    ║");
                Console.WriteLine("║                                                        ║");
                Console.WriteLine("║                      ❤️  ❤️  ❤️                       ║");
                Console.WriteLine("║                                                        ║");
                Console.WriteLine("║           ¡A esta persona también le gustas!           ║");
                Console.WriteLine("║                                                        ║");
                Console.WriteLine("║           Ahora pueden comenzar a chatear              ║");
                Console.WriteLine("║                                                        ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }

            private void MostrarMensaje(string mensaje, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(mensaje);
                Console.ResetColor();
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
         */