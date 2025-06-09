using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using medicos_y_biomedicos.Entidades;

namespace medicos_y_biomedicos.Datos
{
    public class MantenimientoDAL
    {
        private readonly Conexion conexion = new Conexion();
        public bool Insertar(Mantenimiento m)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Mantenimiento (FechaIngreso, Estado, Descripcion) VALUES (@FechaIngreso, @Estado, @Descripcion)",
                    conn
                );
                cmd.Parameters.AddWithValue("@FechaIngreso", m.FechaIngreso);
                cmd.Parameters.AddWithValue("@Estado", m.Estado);
                cmd.Parameters.AddWithValue("@Descripcion", m.Descripcion);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Método para listar todos los mantenimientos
        public List<Mantenimiento> Listar()
        {
            List<Mantenimiento> lista = new List<Mantenimiento>();
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Mantenimiento", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Mantenimiento
                    {
                        IdMantenimiento = Convert.ToInt32(dr["IdMantenimiento"]),
                        FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]),
                        Estado = dr["Estado"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    });
                }
            }

        // Método para actualizar un mantenimiento existente
        public bool Actualizar(Mantenimiento m)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Mantenimiento SET FechaIngreso = @FechaIngreso, Estado = @Estado, Descripcion = @Descripcion WHERE IdMantenimiento = @IdMantenimiento",
                    conn
                );
                cmd.Parameters.AddWithValue("@IdMantenimiento", m.IdMantenimiento);
                cmd.Parameters.AddWithValue("@FechaIngreso", m.FechaIngreso);
                cmd.Parameters.AddWithValue("@Estado", m.Estado);
                cmd.Parameters.AddWithValue("@Descripcion", m.Descripcion);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Método para eliminar un mantenimiento por su Id
        public bool Eliminar(int id)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Mantenimiento WHERE IdMantenimiento = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Método para obtener un mantenimiento específico por su Id
        public Mantenimiento ObtenerPorId(int id)
        {
            Mantenimiento m = null;
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Mantenimiento WHERE IdMantenimiento = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    m = new Mantenimiento
                    {
                        IdMantenimiento = Convert.ToInt32(dr["IdMantenimiento"]),
                        FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]),
                        Estado = dr["Estado"].ToString(),
                        Descripcion = dr["Descripcion"].ToString()
                    };
                }
            }
            return m;
        }
        public bool CambiarEstado(int id, string nuevoEstado)
        {
            bool resultado = false;

            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = "UPDATE Mantenimiento SET Estado = @Estado WHERE IdMantenimiento = @IdMantenimiento";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdMantenimiento", id);

                    // conn.Open(); // Elimina esta línea

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas > 0;
                }
            }

            return resultado;
        }

        // Asegúrate de tener también este método para cargar los datos:
        
    }

}

