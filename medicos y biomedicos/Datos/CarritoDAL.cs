using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class CarritoDAL
{
    private readonly Conexion conexion = new Conexion();

    public int CrearCarritoSiNoExiste(int idUsuario)
    {
        using (SqlConnection conn = conexion.AbrirConexion())
        {
            // Verificar si ya existe un carrito para este usuario
            string sqlSelect = "SELECT IdCarrito FROM Carrito WHERE IdUsuario = @IdUsuario";
            SqlCommand cmdSelect = new SqlCommand(sqlSelect, conn);
            cmdSelect.Parameters.AddWithValue("@IdUsuario", idUsuario);

            object result = cmdSelect.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }

            // Crear nuevo carrito
            string sqlInsert = "INSERT INTO Carrito (IdUsuario) OUTPUT INSERTED.IdCarrito VALUES (@IdUsuario)";
            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
            cmdInsert.Parameters.AddWithValue("@IdUsuario", idUsuario);
            return (int)cmdInsert.ExecuteScalar();
        }
    }

    public List<Equipo> ObtenerPorUsuario(int idUsuario)
    {
        List<Equipo> equipos = new List<Equipo>();

        using (SqlConnection conn = conexion.AbrirConexion())
        {
            string sql = @"
            SELECT e.IdEquipo, e.Nombre, e.Precio, cd.Cantidad
            FROM Carrito c
            INNER JOIN CarritoDetalle cd ON c.IdCarrito = cd.IdCarrito
            INNER JOIN Equipo e ON cd.IdEquipo = e.IdEquipo
            WHERE c.IdUsuario = @IdUsuario";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    equipos.Add(new Equipo
                    {
                        IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                        Nombre = dr["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(dr["Precio"]),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]) // Cantidad en el carrito
                    });
                }
            }
        }

        return equipos;
    }
    public void EliminarCarrito(int idUsuario)
    {
        using (SqlConnection conn = conexion.AbrirConexion())
        {
            // Obtener IdCarrito
            string sqlGetId = "SELECT IdCarrito FROM Carrito WHERE IdUsuario = @IdUsuario";
            SqlCommand cmdGetId = new SqlCommand(sqlGetId, conn);
            cmdGetId.Parameters.AddWithValue("@IdUsuario", idUsuario);

            object result = cmdGetId.ExecuteScalar();
            if (result == null) return;

            int idCarrito = Convert.ToInt32(result);

            // Eliminar detalles del carrito primero (por integridad referencial)
            string sqlDeleteDetalles = "DELETE FROM CarritoDetalle WHERE IdCarrito = @IdCarrito";
            SqlCommand cmdDeleteDetalles = new SqlCommand(sqlDeleteDetalles, conn);
            cmdDeleteDetalles.Parameters.AddWithValue("@IdCarrito", idCarrito);
            cmdDeleteDetalles.ExecuteNonQuery();

            // Eliminar carrito
            string sqlDeleteCarrito = "DELETE FROM Carrito WHERE IdCarrito = @IdCarrito";
            SqlCommand cmdDeleteCarrito = new SqlCommand(sqlDeleteCarrito, conn);
            cmdDeleteCarrito.Parameters.AddWithValue("@IdCarrito", idCarrito);
            cmdDeleteCarrito.ExecuteNonQuery();
        }
    }
}