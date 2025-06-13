using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Datos
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using medicos_y_biomedicos.Entidades;

    public class CarritoDetalleDAL
    {
        private readonly Conexion conexion = new Conexion();

        public bool Insertar(CarritoDetalle detalle)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = @"INSERT INTO CarritoDetalle (IdCarrito, IdEquipo, Cantidad)
                       VALUES (@IdCarrito, @IdEquipo, @Cantidad)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdCarrito", detalle.IdCarrito);
                cmd.Parameters.AddWithValue("@IdEquipo", detalle.IdEquipo);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<CarritoDetalle> ObtenerPorCarrito(int idCarrito)
        {
            List<CarritoDetalle> lista = new List<CarritoDetalle>();

            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = @"SELECT cd.IdDetalle, cd.IdCarrito, cd.IdEquipo, cd.Cantidad,
                              e.Nombre, e.Precio
                       FROM CarritoDetalle cd
                       INNER JOIN Equipo e ON cd.IdEquipo = e.IdEquipo
                       WHERE cd.IdCarrito = @IdCarrito";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdCarrito", idCarrito);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new CarritoDetalle
                    {
                        IdDetalle = Convert.ToInt32(dr["IdDetalle"]),
                        IdCarrito = Convert.ToInt32(dr["IdCarrito"]),
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Equipo = new Equipo
                        {
                            IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                            Nombre = dr["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(dr["Precio"])
                        }
                    });
                }
            }

            return lista;
        }

        public bool EliminarDetalle(int idDetalle)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = "DELETE FROM CarritoDetalle WHERE IdDetalle = @IdDetalle";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdDetalle", idDetalle);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool VaciarCarrito(int idCarrito)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string sql = "DELETE FROM CarritoDetalle WHERE IdCarrito = @IdCarrito";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdCarrito", idCarrito);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<Equipo> ObtenerEquiposDelCarrito(int idUsuario)
        {
            List<Equipo> equipos = new List<Equipo>();

            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = @"
                SELECT E.*, CD.Cantidad AS CantidadCarrito
                FROM CarritoDetalle CD
                INNER JOIN Carrito C ON CD.IdCarrito = C.IdCarrito
                INNER JOIN Equipo E ON CD.IdEquipo = E.IdEquipo
                WHERE C.IdUsuario = @IdUsuario";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Equipo equipo = new Equipo
                            {
                                IdEquipo = Convert.ToInt32(reader["IdEquipo"]),
                                Nombre = reader["Nombre"].ToString(),
                                Marca = reader["Marca"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"]),
                                Cantidad = Convert.ToDecimal(reader["CantidadCarrito"]), // Cantidad desde el carrito
                                Categoria = reader["Categoria"].ToString(),
                                Imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null
                            };

                            equipos.Add(equipo);
                        }
                    }
                }
            }

            return equipos;
        }
        public bool EliminarItemDelCarrito(int idUsuario, int idEquipo)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = @"
            DELETE FROM CarritoDetalle
            WHERE IdCarrito = (
                SELECT TOP 1 IdCarrito FROM Carrito WHERE IdUsuario = @IdUsuario
            ) AND IdEquipo = @IdEquipo";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@IdEquipo", idEquipo);


                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }
}
