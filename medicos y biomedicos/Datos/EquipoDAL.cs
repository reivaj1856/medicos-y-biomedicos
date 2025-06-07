using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using medicos_y_biomedicos.Entidades;
using System.Data;

namespace medicos_y_biomedicos.Datos
{
    public class EquipoDAL
    {
        private readonly Conexion conexion = new Conexion();
        public List<Equipo> Listar()
        {
            List<Equipo> lista = new List<Equipo>();
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Equipo", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Equipo
                    {
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        Nombre = dr["Nombre"].ToString(),
                        Marca = dr["Marca"].ToString(),
                        Modelo = dr["Modelo"].ToString(),
                        Precio = Convert.ToDecimal(dr["Precio"])
                    });
                }
            }
            return lista;
        }

        // Método para insertar un nuevo equipo
        public bool Insertar(Equipo eq)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Equipo (Nombre, Marca, Modelo, Precio) VALUES (@Nombre, @Marca, @Modelo, @Precio)",
                    conn
                );
                cmd.Parameters.AddWithValue("@Nombre", eq.Nombre);
                cmd.Parameters.AddWithValue("@Marca", eq.Marca);
                cmd.Parameters.AddWithValue("@Modelo", eq.Modelo);
                cmd.Parameters.AddWithValue("@Precio", eq.Precio);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Método para actualizar un equipo existente
        public bool Actualizar(Equipo eq)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Equipo SET Nombre = @Nombre, Marca = @Marca, Modelo = @Modelo, Precio = @Precio WHERE IdEquipo = @IdEquipo",
                    conn
                );
                cmd.Parameters.AddWithValue("@IdEquipo", eq.IdEquipo);
                cmd.Parameters.AddWithValue("@Nombre", eq.Nombre);
                cmd.Parameters.AddWithValue("@Marca", eq.Marca);
                cmd.Parameters.AddWithValue("@Modelo", eq.Modelo);
                cmd.Parameters.AddWithValue("@Precio", eq.Precio);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Método para eliminar un equipo por su Id
        public bool Eliminar(int id)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Equipo WHERE IdEquipo = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }


        // Método para obtener un equipo específico por su Id
        public Equipo ObtenerPorId(int id)
        {
            Equipo eq = null;
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Equipo WHERE IdEquipo = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    eq = new Equipo
                    {
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        Nombre = dr["Nombre"].ToString(),
                        Marca = dr["Marca"].ToString(),
                        Modelo = dr["Modelo"].ToString(),
                        Precio = Convert.ToDecimal(dr["Precio"])
                    };
                }
            }
            return eq;
        }
    }
}
