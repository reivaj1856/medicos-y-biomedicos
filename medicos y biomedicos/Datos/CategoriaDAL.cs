using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Datos
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;

    public class CategoriaDAL
    {
        private readonly Conexion conexion = new Conexion();

        // Método para insertar una categoría
        public void InsertarCategoria(string nombreCategoria)
        {
            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = "INSERT INTO Categoria (categoria) VALUES (@categoria)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@categoria", nombreCategoria);

                command.ExecuteNonQuery();
            }
        }

        // Método para obtener todas las categorías
        public List<string> ObtenerCategorias()
        {
            List<string> categorias = new List<string>();

            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = "SELECT categoria FROM Categoria ORDER BY categoria";
                SqlCommand command = new SqlCommand(query, conn);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categorias.Add(reader["categoria"].ToString());
                }

                reader.Close();
            }

            return categorias;
        }

        // Método para cargar categorías en ComboBox
        public void CargarCategoriasEnComboBox(ComboBox combo)
        {
            combo.Items.Clear();

            using (SqlConnection conn = conexion.AbrirConexion())
            {
                string query = "SELECT categoria FROM Categoria ORDER BY categoria";
                SqlCommand command = new SqlCommand(query, conn);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    combo.Items.Add(reader["categoria"].ToString());
                }

                reader.Close();
            }
        }
    }
}