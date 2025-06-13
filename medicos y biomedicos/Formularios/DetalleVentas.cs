using medicos_y_biomedicos.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace medicos_y_biomedicos.Formularios
{
    public partial class DetalleVentas : Form
    {
        public DetalleVentas()
        {
            InitializeComponent();
            MostrarTodosLosDetalles();
        }

        private void MostrarTodosLosDetalles()
        {
            VentaDAL ventaDAL = new VentaDAL();
            var lista = ventaDAL.ObtenerTodosLosDetallesVentaExtendido();
            registros.DataSource = lista;
        }

    }
}
