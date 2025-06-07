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
using medicos_y_biomedicos.Entidades;
using medicos_y_biomedicos.Formularios;

namespace medicos_y_biomedicos
{
    public partial class VentanaVenta : Form
    {
        
        public VentanaVenta()
        {
            InitializeComponent();
        }

        private void CargarEquipos()
        {
            EquipoDAL dal = new EquipoDAL();
            dataGridVenta.DataSource = dal.Listar();
            dataGridVenta.ClearSelection();
            dataGridVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void VentanaVenta_Load(object sender, EventArgs e)
        {
            CargarEquipos();
        }
        private void btnVenta_Click(object sender, EventArgs e)
        {
            if (dataGridVenta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un equipo para vender.");
                return;
            }
            var fila = dataGridVenta.SelectedRows[0];
            int idEquipo = Convert.ToInt32(fila.Cells["IdEquipo"].Value);
            decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);

            // Podrías usar un formulario aparte o un NumericUpDown para definir cantidad
            int cantidad = 1;
            decimal total = cantidad * precio;

            Venta nuevaVenta = new Venta
            {
                IdEquipo = idEquipo,
                Fecha = DateTime.Now,
                Cantidad = cantidad,
                Total = total
            };

            VentaDAL ventaDAL = new VentaDAL();
            if (ventaDAL.Insertar(nuevaVenta))
            {
                MessageBox.Show("Venta realizada correctamente.");
            }
            else
            {
                MessageBox.Show("No se pudo realizar la venta.");
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridVenta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un equipo para eliminar.");
                return;
            }

            var fila = dataGridVenta.SelectedRows[0];
            
            int idEquipo = Convert.ToInt32(dataGridVenta.SelectedRows[0].Cells["IdEquipo"].Value);
            DialogResult result = MessageBox.Show("¿Está seguro de eliminar este equipo?", "Confirmación", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                EquipoDAL equi = new EquipoDAL();
                try
                {
                    if (equi.Eliminar(idEquipo))
                    {
                        MessageBox.Show("Equipo eliminado.");
                        CargarEquipos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar.");
                    }
                }
                catch
                {
                    MessageBox.Show("No se pudo eliminar la tabla es dependiente de otra referencia.");
                }
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridVenta.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un equipo para editar.");
                return;
            }

            int idEquipo = Convert.ToInt32(dataGridVenta.SelectedRows[0].Cells["IdEquipo"].Value);

             RegistrarEquipo formEditar = new RegistrarEquipo(idEquipo);
             if (formEditar.ShowDialog() == DialogResult.OK)
             {
                 CargarEquipos(); // recargar si se editó
             }
        }
    }
}
