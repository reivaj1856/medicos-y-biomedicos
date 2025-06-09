using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using medicos_y_biomedicos.Entidades;

namespace medicos_y_biomedicos.Datos
{
    public class UsuarioDAL
    {
        private readonly Conexion conexion = new Conexion();

        // Verifica si el usuario existe con la cuenta y la contraseña dadas
        public bool ValidarCredenciales(string cuenta, string contraseña)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuario WHERE Cuenta = @Cuenta AND Contraseña = @Contraseña", conn);
                cmd.Parameters.AddWithValue("@Cuenta", cuenta);
                cmd.Parameters.AddWithValue("@Contraseña", contraseña);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Crear nuevo usuario
        public bool CrearUsuario(Usuario usuario)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO Usuario (Nombre, Cuenta, Contraseña, administrador, Imagen)
            VALUES (@Nombre, @Cuenta, @Contraseña, @Administrador, @Imagen)", conn);

                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Cuenta", usuario.Cuenta);
                cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                cmd.Parameters.AddWithValue("@Administrador", usuario.Administrador ?? "no"); // Valor por defecto
                cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen ?? (object)DBNull.Value); // Imagen puede ser null

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Obtener usuario por cuenta (opcional)
        public Usuario ObtenerUsuarioPorCuenta(string cuenta)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE Cuenta = @Cuenta", conn);
                cmd.Parameters.AddWithValue("@Cuenta", cuenta);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                        Nombre = reader["Nombre"].ToString(),
                        Cuenta = reader["Cuenta"].ToString(),
                        Contraseña = reader["Contraseña"].ToString(),
                        Administrador = reader["administrador"].ToString(),
                        Imagen = reader["Imagen"] as byte[]
                    };
                }
                return null;
            }
        }
        public bool ExisteCuenta(string cuenta)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuario WHERE Cuenta = @Cuenta", conn);
                cmd.Parameters.AddWithValue("@Cuenta", cuenta);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public Usuario ObtenerPorId(int idUsuario)
        {
            Usuario usuario = null;
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = "SELECT * FROM Usuario WHERE IdUsuario = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", idUsuario);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = (int)reader["IdUsuario"],
                        Cuenta = reader["Cuenta"].ToString(),
                        // Añade más campos si los necesitas
                    };
                }
            }
            return usuario;
        }

    }
}
