using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using medicos_y_biomedicos.Entidades;

namespace medicos_y_biomedicos.Datos
{
    public class VentaDAL
    {
        private readonly Conexion conexion = new Conexion();

        // Insertar una nueva venta con detalles
        public int Insertar(Venta v)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // Validación previa
                    if (v.Detalles == null || v.Detalles.Count == 0)
                        throw new Exception("No hay detalles de venta para registrar.");

                        // Insertar la venta
                    string sqlVenta = "INSERT INTO Venta (IdUsuario, Fecha, Total) OUTPUT INSERTED.IdVenta VALUES (@IdUsuario, @Fecha, @Total)";
                    SqlCommand cmdVenta = new SqlCommand(sqlVenta, conn, trans);
                    cmdVenta.Parameters.AddWithValue("@IdUsuario", v.IdUsuario);
                    cmdVenta.Parameters.AddWithValue("@Fecha", v.Fecha);
                    cmdVenta.Parameters.AddWithValue("@Total", v.Total);

                    object result = cmdVenta.ExecuteScalar();
                    if (result == null)
                        throw new Exception("No se generó un IdVenta.");
                    int idVenta = Convert.ToInt32(result);
                    v.IdVenta = idVenta;

                    // Insertar detalles
                    foreach (var d in v.Detalles)
                    {
                        string sqlDetalle = @"INSERT INTO DetalleVenta (IdVenta, IdEquipo, Cantidad, Total)
                                  VALUES (@IdVenta, @IdEquipo, @Cantidad, @Total)";
                        SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, conn, trans);
                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                        cmdDetalle.Parameters.AddWithValue("@IdEquipo", d.IdEquipo);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", d.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@Total", d.Total);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return idVenta;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al insertar venta: " + ex.Message);
                    return -1;
                }
            }
        }

        // Listar todas las ventas (sin detalles)
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
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        Fecha = Convert.ToDateTime(dr["Fecha"]),
                        Total = Convert.ToDecimal(dr["Total"])
                    });
                }
            }
            return lista;
        }

        // Obtener una venta con sus detalles por Id
        public Venta ObtenerPorId(int id)
        {
            Venta venta = null;
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                // Venta
                SqlCommand cmd = new SqlCommand("SELECT * FROM Venta WHERE IdVenta = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    venta = new Venta
                    {
                        IdVenta = Convert.ToInt32(dr["IdVenta"]),
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        Fecha = Convert.ToDateTime(dr["Fecha"]),
                        Total = Convert.ToDecimal(dr["Total"]),
                        Detalles = new List<DetalleVenta>()

                    };
                }
                dr.Close();

                if (venta != null)
                {
                    // Detalles
                    SqlCommand cmdDet = new SqlCommand("SELECT * FROM DetalleVenta WHERE IdVenta = @Id", conn);
                    cmdDet.Parameters.AddWithValue("@Id", id);
                    SqlDataReader drDet = cmdDet.ExecuteReader();
                    while (drDet.Read())
                    {
                        venta.Detalles.Add(new DetalleVenta
                        {
                            IdDetalle = Convert.ToInt32(drDet["IdDetalle"]),
                            IdVenta = id,
                            IdEquipo = Convert.ToInt32(drDet["IdEquipo"]),
                            Cantidad = Convert.ToInt32(drDet["Cantidad"]),
                            Total = Convert.ToDecimal(drDet["Total"])
                        });
                    }
                }
            }
            return venta;
        }

        public bool Eliminar(int idVenta)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // Primero eliminar los detalles de la venta
                    string sqlDetalle = "DELETE FROM DetalleVenta WHERE IdVenta = @IdVenta";
                    SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, conn, trans);
                    cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmdDetalle.ExecuteNonQuery();

                        // Luego eliminar la venta
                    string sqlVenta = "DELETE FROM Venta WHERE IdVenta = @IdVenta";
                    SqlCommand cmdVenta = new SqlCommand(sqlVenta, conn, trans);
                    cmdVenta.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmdVenta.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        public bool Actualizar(Venta venta)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // Actualizar la tabla Venta (por ejemplo, solo la fecha y el usuario)
                    string sqlVenta = "UPDATE Venta SET IdUsuario = @IdUsuario, Fecha = @Fecha, @Total = Total  WHERE IdVenta = @IdVenta";
                    SqlCommand cmdVenta = new SqlCommand(sqlVenta, conn, trans);
                    cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                    cmdVenta.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmdVenta.Parameters.AddWithValue("@IdVenta", venta.IdVenta);
                    cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                    cmdVenta.ExecuteNonQuery();
                        // En esta lógica asumimos que solo hay un detalle por venta (como tú mencionaste)
                    if (venta.Detalles != null && venta.Detalles.Count > 0)
                    {
                        var detalle = venta.Detalles[0];

                        // Actualizar la tabla DetalleVenta
                        string sqlDetalle = @"UPDATE DetalleVenta 
                                  SET IdEquipo = @IdEquipo, Cantidad = @Cantidad, Total = @Total 
                                  WHERE IdVenta = @IdVenta";
                        SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, conn, trans);
                        cmdDetalle.Parameters.AddWithValue("@IdVenta", venta.IdVenta);
                        cmdDetalle.Parameters.AddWithValue("@IdEquipo", detalle.IdEquipo);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@Total", detalle.Total);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        public bool ActualizarSolo(Venta venta)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // Solo actualizamos la tabla Venta
                    string sqlVenta = "UPDATE Venta SET IdUsuario = @IdUsuario, Fecha = @Fecha, Total = @Total WHERE IdVenta = @IdVenta";

                    SqlCommand cmdVenta = new SqlCommand(sqlVenta, conn, trans);
                    cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                    cmdVenta.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                    cmdVenta.Parameters.AddWithValue("@IdVenta", venta.IdVenta);

                    cmdVenta.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
        public List<DetalleVentaExtendido> ObtenerTodosLosDetallesVentaExtendido()
        {
            List<DetalleVentaExtendido> resultado = new List<DetalleVentaExtendido>();

            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = @"
            SELECT 
                u.Nombre AS NombreUsuario,
                u.Apellido,
                u.Cuenta,
                u.Direccion,
                e.Nombre AS NombreEquipo,
                e.Marca,
                e.Modelo,
                e.Precio,
                e.Categoria,
                dv.Cantidad,
                dv.Total
            FROM DetalleVenta dv
            INNER JOIN Venta v ON v.IdVenta = dv.IdVenta
            INNER JOIN Usuario u ON v.IdUsuario = u.IdUsuario
            INNER JOIN Equipo e ON e.IdEquipo = dv.IdEquipo";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    resultado.Add(new DetalleVentaExtendido
                    {
                        NombreUsuario = reader["NombreUsuario"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Cuenta = reader["Cuenta"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        NombreEquipo = reader["NombreEquipo"].ToString(),
                        Marca = reader["Marca"].ToString(),
                        Modelo = reader["Modelo"].ToString(),
                        Precio = Convert.ToDecimal(reader["Precio"]),
                        Categoria = reader["Categoria"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        TotalDetalle = Convert.ToDecimal(reader["Total"])
                    });
                }
            }

            return resultado;
        }

    }

}
