using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Datos
{
    public class Conexion
    {
        private readonly string connectionString = "Server=CROMER;Database=medicosybiometicos;Trusted_Connection=True;";

        public SqlConnection AbrirConexion()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
