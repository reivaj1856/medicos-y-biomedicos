using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using medicos_y_biomedicos.Entidades;

namespace medicos_y_biomedicos.Datos
{
    public class DetalleVentaDAL
    {
        private readonly Conexion conexion = new Conexion();

        // Insertar un detalle de venta
        public bool Insertar(DetalleVenta detalle)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = @"INSERT INTO DetalleVenta (IdVenta, IdEquipo, Cantidad, Total) 
                           VALUES (@IdVenta, @IdEquipo, @Cantidad, @Total)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdVenta", detalle.IdVenta);
                cmd.Parameters.AddWithValue("@IdEquipo", detalle.IdEquipo);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@Total", detalle.Total);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Listar todos los detalles
        public List<DetalleVenta> Listar()
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM DetalleVenta", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new DetalleVenta
                    {
                        IdDetalle = Convert.ToInt32(dr["IdDetalle"]),
                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Total = Convert.ToDecimal(dr["Total"])
                    });
                }
            }
            return lista;
        }

        // Obtener detalle por Id
        public DetalleVenta ObtenerPorId(int idDetalle)
        {
            DetalleVenta detalle = null;
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM DetalleVenta WHERE IdDetalle = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", idDetalle);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    detalle = new DetalleVenta
                    {
                        IdDetalle = Convert.ToInt32(dr["IdDetalle"]),
                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Total = Convert.ToDecimal(dr["Total"])
                    };
                }
            }
            return detalle;
        }

        // Actualizar un detalle
        public bool Actualizar(DetalleVenta detalle)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE DetalleVenta SET IdVenta = @IdVenta, IdEquipo = @IdEquipo, Cantidad = @Cantidad, Total = @Total WHERE IdDetalle = @IdDetalle",
                    conn
                );
                cmd.Parameters.AddWithValue("@IdDetalle", detalle.IdDetalle);
                cmd.Parameters.AddWithValue("@IdVenta", detalle.IdVenta);
                cmd.Parameters.AddWithValue("@IdEquipo", detalle.IdEquipo);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@Total", detalle.Total);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Eliminar un detalle
        public bool Eliminar(int idDetalle)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM DetalleVenta WHERE IdDetalle = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", idDetalle);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Listar detalles por IdVenta
        public List<DetalleVenta> ListarPorVenta(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM DetalleVenta WHERE IdVenta = @IdVenta", conn);
                cmd.Parameters.AddWithValue("@IdVenta", idVenta);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new DetalleVenta
                    {
                        IdDetalle = Convert.ToInt32(dr["IdDetalle"]),
                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Total = Convert.ToDecimal(dr["Total"])
                    });
                }
            }
            return lista;
        }
    }
}