using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using medicos_y_biomedicos.Entidades;

namespace medicos_y_biomedicos.Datos
{
    using System;
    using System.Data.SqlClient;
    using medicos_y_biomedicos.Datos;
    using medicos_y_biomedicos.Entidades;

    public class UsuarioDAL
    {
        private readonly Conexion conexion = new Conexion();

        // Validar si la cuenta y la contraseña son correctas
        public bool ValidarCredenciales(string cuenta, string contraseña)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = "SELECT COUNT(*) FROM Usuario WHERE Cuenta = @Cuenta AND Contraseña = @Contraseña";
                SqlCommand cmd = new SqlCommand(sql, conn);
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
                string sql = @"
                INSERT INTO Usuario (Nombre, Apellido, Direccion, NIT, Cuenta, Contraseña, Administrador, Imagen)
                VALUES (@Nombre, @Apellido, @Direccion, @NIT, @Cuenta, @Contraseña, @Administrador, @Imagen)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                cmd.Parameters.AddWithValue("@NIT", usuario.NIT);
                cmd.Parameters.AddWithValue("@Cuenta", usuario.Cuenta);
                cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                cmd.Parameters.AddWithValue("@Administrador", usuario.Administrador ?? "no");
                cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen ?? (object)DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Obtener usuario por cuenta
        public Usuario ObtenerUsuarioPorCuenta(string cuenta)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = "SELECT * FROM Usuario WHERE Cuenta = @Cuenta";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Cuenta", cuenta);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            NIT = reader["NIT"].ToString(),
                            Cuenta = reader["Cuenta"].ToString(),
                            Contraseña = reader["Contraseña"].ToString(),
                            Administrador = reader["Administrador"]?.ToString(),
                            Imagen = reader["Imagen"] == DBNull.Value ? null : (byte[])reader["Imagen"]
                        };
                    }
                }
            }

            return null;
        }

        // Verificar si una cuenta ya existe
        public bool ExisteCuenta(string cuenta)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = "SELECT COUNT(*) FROM Usuario WHERE Cuenta = @Cuenta";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Cuenta", cuenta);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Obtener usuario por Id
        public Usuario ObtenerPorId(int idUsuario)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = "SELECT * FROM Usuario WHERE IdUsuario = @Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", idUsuario);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            NIT = reader["NIT"].ToString(),
                            Cuenta = reader["Cuenta"].ToString(),
                            Contraseña = reader["Contraseña"].ToString(),
                            Administrador = reader["Administrador"]?.ToString(),
                            Imagen = reader["Imagen"] == DBNull.Value ? null : (byte[])reader["Imagen"]
                        };
                    }
                }
            }

            return null;
        }
    }

}
