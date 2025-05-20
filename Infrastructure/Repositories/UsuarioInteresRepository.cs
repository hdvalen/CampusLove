using MySql.Data.MySqlClient;

public class UsuarioInteresesRepository
{
    private readonly MySqlConnection _connection;

    public UsuarioInteresesRepository(MySqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> InsertAsync(int idUsuario, int idInteres)
    {
        string query = "INSERT INTO usuario_intereses (id_usuario, id_interes) VALUES (@idUsuario, @idInteres)";

        await _connection.OpenAsync();
        using var cmd = new MySqlCommand(query, _connection);
        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
        cmd.Parameters.AddWithValue("@idInteres", idInteres);

        int rowsAffected = await cmd.ExecuteNonQueryAsync();
        await _connection.CloseAsync();

        return rowsAffected > 0;
    }
}