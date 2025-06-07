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
    public partial class ResgistroMantenimiento : Form
    {
        public ResgistroMantenimiento()
        {
            InitializeComponent();
        }
        private void ResgistroMantenimiento_Load(object sender, EventArgs e)
        {
            CargarMantenimientos();
        }
        
        private void CargarMantenimientos()
        {
            MantenimientoDAL dal = new MantenimientoDAL();
            dataMantenimiento.DataSource = dal.Listar();
            dataMantenimiento.ClearSelection(); // Evita que esté preseleccionado
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataMantenimiento.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataMantenimiento.SelectedRows[0].Cells["IdMantenimiento"].Value);

                DialogResult r = MessageBox.Show("¿Seguro que desea eliminar?", "Confirmar", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    new MantenimientoDAL().Eliminar(id);
                    CargarMantenimientos();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro primero.");
            }
        }

        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (dataMantenimiento.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataMantenimiento.SelectedRows[0].Cells["IdMantenimiento"].Value);
                string estadoActual = dataMantenimiento.SelectedRows[0].Cells["Estado"].Value.ToString();
                string nuevoEstado = estadoActual == "En proceso" ? "Finalizado" : "En proceso";

                new MantenimientoDAL().CambiarEstado(id, nuevoEstado);
                CargarMantenimientos();
            }
            else
            {
                MessageBox.Show("Seleccione un registro para cambiar su estado.");
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataMantenimiento.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataMantenimiento.SelectedRows[0].Cells["IdMantenimiento"].Value);
                RegistrarMantenimiento formEditar = new RegistrarMantenimiento(id);
                formEditar.ShowDialog();
                CargarMantenimientos();
            }
            else
            {
                MessageBox.Show("Seleccione un registro para editar.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMantenimientos();
        }
    }
}   
