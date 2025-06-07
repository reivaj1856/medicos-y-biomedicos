using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Formularios;

namespace medicos_y_biomedicos
{
    public partial class RegistroVenta : Form
    {
        public RegistroVenta()
        {
            InitializeComponent();
        }
        private void CargarVentas()
        {
            VentaDAL vent = new VentaDAL(); 
            dataGridReVentas.DataSource = vent.Listar(); // este método debe retornar List<Venta>
            dataGridReVentas.ClearSelection();

        }
        private void FormReVentas_Shown(object sender, EventArgs e)
        {
            CargarVentas();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridReVentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una venta para eliminar.");
                return;
            }
            int idVenta = Convert.ToInt32(dataGridReVentas.SelectedRows[0].Cells["IdVenta"].Value);

            DialogResult confirm = MessageBox.Show("¿Desea eliminar esta venta?", "Confirmación", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                VentaDAL ventaDAL = new VentaDAL(); // Asegúrate de que este DAL tenga el método Eliminar(int idVenta)
                if (ventaDAL.Eliminar(idVenta))
                {
                    MessageBox.Show("Venta eliminada correctamente.");
                    CargarVentas();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la venta.");
                }
            }

        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de edición no implementada aún.");
            /*
             if (dataGridReVentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una venta para editar.");
                return;
            }
            int idVenta = Convert.ToInt32(dataGridReVentas.SelectedRows[0].Cells["IdVenta"].Value);

           
              RegistrarEquipo formEditar = new RegistrarEquipo(idVenta);
             if (formEditar.ShowDialog() == DialogResult.OK)
             {
                 CargarVentas(); // recargar al cerrar la edición
             }
             */
        }
    }
}
