using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Entidades;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

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
                    Precio = Convert.ToDecimal(dr["Precio"]),
                    Imagen = dr["Imagen"] == DBNull.Value ? null : (byte[])dr["Imagen"]
                });
            }
        }
        return lista;
    }

    public bool Insertar(Equipo eq)
    {
        using (SqlConnection conn = conexion.AbrirConexion())
        {
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Equipo (Nombre, Marca, Modelo, Precio, Imagen) VALUES (@Nombre, @Marca, @Modelo, @Precio, @Imagen)",
                conn
            );
            cmd.Parameters.AddWithValue("@Nombre", eq.Nombre);
            cmd.Parameters.AddWithValue("@Marca", eq.Marca);
            cmd.Parameters.AddWithValue("@Modelo", eq.Modelo);
            cmd.Parameters.AddWithValue("@Precio", eq.Precio);
            cmd.Parameters.Add("@Imagen", SqlDbType.VarBinary).Value = (object)eq.Imagen ?? DBNull.Value;

            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool Actualizar(Equipo eq)
    {
        using (SqlConnection conn = conexion.AbrirConexion())
        {
            SqlCommand cmd = new SqlCommand(
                "UPDATE Equipo SET Nombre = @Nombre, Marca = @Marca, Modelo = @Modelo, Precio = @Precio, Imagen = @Imagen WHERE IdEquipo = @IdEquipo",
                conn
            );
            cmd.Parameters.AddWithValue("@IdEquipo", eq.IdEquipo);
            cmd.Parameters.AddWithValue("@Nombre", eq.Nombre);
            cmd.Parameters.AddWithValue("@Marca", eq.Marca);
            cmd.Parameters.AddWithValue("@Modelo", eq.Modelo);
            cmd.Parameters.AddWithValue("@Precio", eq.Precio);
            cmd.Parameters.Add("@Imagen", SqlDbType.VarBinary).Value = (object)eq.Imagen ?? DBNull.Value;

            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool Eliminar(int id)
    {
        using (SqlConnection conn = conexion.AbrirConexion())
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Equipo WHERE IdEquipo = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }

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
                    Precio = Convert.ToDecimal(dr["Precio"]),
                    Imagen = dr["Imagen"] == DBNull.Value ? null : (byte[])dr["Imagen"]
                };
            }
        }
        return eq;
    }
    public List<Equipo> ObtenerPorModelo(string modelo)
    {
        List<Equipo> lista = new List<Equipo>();

        using (SqlConnection conn = conexion.AbrirConexion())
        {

            string query = "SELECT * FROM Equipo WHERE Modelo = @Modelo"; // Asegúrate que 'Equipo' sea el nombre correcto

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Modelo", modelo);

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
                            Precio = Convert.ToInt32(reader["Precio"]),
                            Imagen = reader["Imagen"] as byte[]
                        };
                        lista.Add(equipo);
                    }
                }
            }
        }

        return lista;
    }

}
