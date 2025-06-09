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

    public class VentaDAL
    {
        private readonly Conexion conexion = new Conexion();

        // Insertar una nueva venta
        public bool Insertar(Venta v)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Venta (IdEquipo, IdUsuario, Fecha, Cantidad, Total) VALUES (@IdEquipo, @IdUsuario, @Fecha, @Cantidad, @Total)",
                    conn
                );
                cmd.Parameters.AddWithValue("@IdEquipo", v.IdEquipo);
                cmd.Parameters.AddWithValue("@IdUsuario", v.IdUsuario);
                cmd.Parameters.AddWithValue("@Fecha", v.Fecha);
                cmd.Parameters.AddWithValue("@Cantidad", v.Cantidad);
                cmd.Parameters.AddWithValue("@Total", v.Total);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Listar todas las ventas
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Venta
                    {
                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        Fecha = Convert.ToDateTime(dr["Fecha"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Total = Convert.ToDecimal(dr["Total"])
                    });
                }
            }
            return lista;
        }

        // Actualizar una venta existente
        public bool Actualizar(Venta v)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Venta SET IdEquipo = @IdEquipo, IdUsuario = @IdUsuario, Fecha = @Fecha, Cantidad = @Cantidad, Total = @Total WHERE IdVenta = @IdVenta",
                    conn
                );
                cmd.Parameters.AddWithValue("@IdVenta", v.IdVenta);
                cmd.Parameters.AddWithValue("@IdEquipo", v.IdEquipo);
                cmd.Parameters.AddWithValue("@IdUsuario", v.IdUsuario);
                cmd.Parameters.AddWithValue("@Fecha", v.Fecha);
                cmd.Parameters.AddWithValue("@Cantidad", v.Cantidad);
                cmd.Parameters.AddWithValue("@Total", v.Total);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Eliminar venta
        public bool Eliminar(int id)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Venta WHERE IdVenta = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Obtener venta por Id
        public Venta ObtenerPorId(int id)
        {
            Venta venta = null;
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Venta WHERE IdVenta = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    venta = new Venta
                    {
                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        Fecha = Convert.ToDateTime(dr["Fecha"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Total = Convert.ToDecimal(dr["Total"])
                    };
                }
            }
            return venta;
        }
    }

}
