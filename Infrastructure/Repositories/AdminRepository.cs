using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;

namespace CampusLove.Infrastructure.Repositories
{
    public class AdminRepository : IAdministrador
    {
        private readonly string _conexionString;
        public AdminRepository(string conexionString)
        {
            _conexionString = conexionString;
        }
        private MySqlConnection CrearConexion()
        {
            return new MySqlConnection(_conexionString);
        }
        public List<Administrador> ObtenerTodosAdministradores()
        {
            var administradores = new List<Administrador>();
            using (var conexion = CrearConexion())
            {
                try
                {
                    conexion.Open();
                    var comando = new MySqlCommand("SELECT * FROM administradores", conexion);
                    
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var admin = new Administrador
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Usuario = reader["usuario"].ToString(),
                                Contrasena = reader["contrasena"].ToString(),
                                Correo = reader["correo"].ToString(),
                                NivelAcceso = Convert.ToInt32(reader["nivel_acceso"]),
                                FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
                                UltimoAcceso = reader["ultimo_acceso"] != DBNull.Value 
                                    ? Convert.ToDateTime(reader["ultimo_acceso"]) 
                                    : DateTime.MinValue,
                                Activo = Convert.ToBoolean(reader["activo"])
                            };
                            
                            administradores.Add(admin);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener administradores: " + ex.Message);
                }
            }
            
            return administradores;
        }
        public Administrador ObtenerAdministradorPorId(int id)
        {
            Administrador? admin = null;
            using (var conexion = CrearConexion())
            {
                try
                {
                    conexion.Open();
                    
                    var comando = new MySqlCommand("SELECT * FROM administradores WHERE id = @id", conexion);
                    comando.Parameters.AddWithValue("@id", id);
                    
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            admin = new Administrador
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Usuario = reader["usuario"].ToString(),
                                Contrasena = reader["contrasena"].ToString(),
                                Correo = reader["correo"].ToString(),
                                NivelAcceso = Convert.ToInt32(reader["nivel_acceso"]),
                                FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
                                UltimoAcceso = reader["ultimo_acceso"] != DBNull.Value 
                                    ? Convert.ToDateTime(reader["ultimo_acceso"]) 
                                    : DateTime.MinValue,
                                Activo = Convert.ToBoolean(reader["activo"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener administrador por ID: " + ex.Message);
                }
            }
            
            return admin;
        }
        
        public Administrador ObtenerAdministradorPorUsuario(string usuario)
        {
            Administrador admin = null;
            
            using (var conexion = CrearConexion())
            {
                try
                {
                    conexion.Open();
                    
                    var comando = new MySqlCommand("SELECT * FROM administradores WHERE usuario = @usuario", conexion);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            admin = new Administrador
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Usuario = reader["usuario"].ToString(),
                                Contrasena = reader["contrasena"].ToString(),
                                Correo = reader["correo"].ToString(),
                                NivelAcceso = Convert.ToInt32(reader["nivel_acceso"]),
                                FechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]),
                                UltimoAcceso = reader["ultimo_acceso"] != DBNull.Value 
                                    ? Convert.ToDateTime(reader["ultimo_acceso"]) 
                                    : DateTime.MinValue,
                                Activo = Convert.ToBoolean(reader["activo"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener administrador por usuario: " + ex.Message);
                }
            }
            
            return admin;
        }
        public bool VerificarCredenciales(string usuario, string contrasena)
        {
            bool credencialesValidas = false;
            
            using (var conexion = CrearConexion())
            {
                try
                {
                    conexion.Open();
                    
                    var comando = new MySqlCommand(
                        "SELECT COUNT(*) FROM administradores WHERE usuario = @usuario AND contrasena = @contrasena AND activo = true", 
                        conexion);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contrasena", contrasena);
                    
                    int count = Convert.ToInt32(comando.ExecuteScalar());
                    credencialesValidas = count > 0;
                    
                    if (credencialesValidas)
                    {
                        var updateComando = new MySqlCommand(
                            "UPDATE administradores SET ultimo_acceso = @fecha WHERE usuario = @usuario", 
                            conexion);
                        updateComando.Parameters.AddWithValue("@fecha", DateTime.Now);
                        updateComando.Parameters.AddWithValue("@usuario", usuario);
                        updateComando.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al verificar credenciales: " + ex.Message);
                }
            }
            
            return credencialesValidas;
        }
        
        public bool CrearAdministrador(Administrador administrador)
        {
            bool exito = false;
            
            using (var conexion = CrearConexion())
            {
                MySqlTransaction transaccion = null;
                
                try
                {
                    conexion.Open();
                    
                    transaccion = conexion.BeginTransaction();
                    
                    var comando = new MySqlCommand(
                        @"INSERT INTO administradores 
                        (nombre, usuario, contrasena, correo, nivel_acceso, fecha_creacion, activo) 
                        VALUES 
                        (@nombre, @usuario, @contrasena, @correo, @nivelAcceso, @fechaCreacion, @activo)",
                        conexion, transaccion);
                    
                    comando.Parameters.AddWithValue("@nombre", administrador.Nombre);
                    comando.Parameters.AddWithValue("@usuario", administrador.Usuario);
                    comando.Parameters.AddWithValue("@contrasena", administrador.Contrasena);
                    comando.Parameters.AddWithValue("@correo", administrador.Correo);
                    comando.Parameters.AddWithValue("@nivelAcceso", administrador.NivelAcceso);
                    comando.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);
                    comando.Parameters.AddWithValue("@activo", administrador.Activo);
                    
                    int filasAfectadas = comando.ExecuteNonQuery();
                    
                    if (filasAfectadas > 0)
                    {
                        transaccion.Commit();
                        exito = true;
                    }
                    else
                    {
                        transaccion.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }

                    Console.WriteLine("Error al crear administrador: " + ex.Message);
                }
            }
            
            return exito;
        }
        
        public bool ActualizarAdministrador(Administrador administrador)
        {
            bool exito = false;
            
            if (administrador.Id <= 0)
            {
                Console.WriteLine("ID de administrador no válido");
                return false;
            }
            
            using (var conexion = CrearConexion())
            {
                
                MySqlTransaction transaccion = null;
                
                try
                {
                    conexion.Open();
                    
                    transaccion = conexion.BeginTransaction();
                    
                    var comando = new MySqlCommand(
                        @"UPDATE administradores 
                        SET nombre = @nombre, 
                            usuario = @usuario, 
                            correo = @correo, 
                            nivel_acceso = @nivelAcceso, 
                            activo = @activo 
                        WHERE id = @id",
                        conexion, transaccion);
                    
                    comando.Parameters.AddWithValue("@id", administrador.Id);
                    comando.Parameters.AddWithValue("@nombre", administrador.Nombre);
                    comando.Parameters.AddWithValue("@usuario", administrador.Usuario);
                    comando.Parameters.AddWithValue("@correo", administrador.Correo);
                    comando.Parameters.AddWithValue("@nivelAcceso", administrador.NivelAcceso);
                    comando.Parameters.AddWithValue("@activo", administrador.Activo);
                    
                    int filasAfectadas = comando.ExecuteNonQuery();
                    
                    if (filasAfectadas > 0)
                    {
                       
                        transaccion.Commit();
                        exito = true;
                    }
                    else
                    {
                      
                        transaccion.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }
                    
                    Console.WriteLine("Error al actualizar administrador: " + ex.Message);
                }
            }
            
            return exito;
        }
        public bool EliminarAdministrador(int id)
        {
            bool exito = false;
            using (var conexion = CrearConexion())
            {
               
                MySqlTransaction transaccion = null;
                
                try
                {
                    conexion.Open();
                    
                    transaccion = conexion.BeginTransaction();
                    
                    var comando = new MySqlCommand("DELETE FROM administradores WHERE id = @id", conexion, transaccion);
                    comando.Parameters.AddWithValue("@id", id);
                    
                    int filasAfectadas = comando.ExecuteNonQuery();
                    
                    if (filasAfectadas > 0)
                    {
                     
                        transaccion.Commit();
                        exito = true;
                    }
                    else
                    {
                        transaccion.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }
                    
                    Console.WriteLine("Error al eliminar administrador: " + ex.Message);
                }
            }
            
            return exito;
        }
        
        public bool CambiarContrasena(int id, string nuevaContrasena)
        {
            bool exito = false;
            using (var conexion = CrearConexion())
            {
                MySqlTransaction transaccion = null;
                
                try
                {
                    conexion.Open();
                    
                    transaccion = conexion.BeginTransaction();
                    
                    var comando = new MySqlCommand(
                        "UPDATE administradores SET contrasena = @contrasena WHERE id = @id",
                        conexion, transaccion);
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                    
                   
                    int filasAfectadas = comando.ExecuteNonQuery();
                    
                    if (filasAfectadas > 0)
                    {
                        transaccion.Commit();
                        exito = true;
                    }
                    else
                    {
                        transaccion.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                    {
                        transaccion.Rollback();
                    }
                    
                    Console.WriteLine("Error al cambiar contraseña: " + ex.Message);
                }
            }
            
            return exito;
        }
        public List<Usuario> ObtenerTodosUsuarios()
        {
            var usuarios = new List<Usuario>();
            
            using (var conexion = CrearConexion())
            {
                try
                {
                    conexion.Open();
                    
                    var comando = new MySqlCommand(
                        @"SELECT u.*, c.nombre as nombre_ciudad 
                        FROM usuarios u 
                        LEFT JOIN ciudades c ON u.id_ciudad = c.id", 
                        conexion);
                    
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nombre = reader["nombre"].ToString(),
                                Genero = reader["genero"].ToString(),
                                Edad = Convert.ToInt32(reader["edad"]),
                                Carrera = reader["carrera"].ToString(),
                                FrasePerfil = reader["frase_perfil"] != DBNull.Value ? reader["frase_perfil"].ToString() : null,
                                IdCiudad = reader["id_ciudad"] != DBNull.Value ? Convert.ToInt32(reader["id_ciudad"]) : 0
                            };
                            
                            usuarios.Add(usuario);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener usuarios: " + ex.Message);
                }
            }
            
            return usuarios;
        }
    }
}