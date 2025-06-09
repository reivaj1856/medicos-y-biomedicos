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

namespace medicos_y_biomedicos.Formularios
{
    public partial class EditVenta : Form
    {
        private int idVenta;
        private int idEquipo;
        private int idUsuario;
        public EditVenta(int id)
        {
            InitializeComponent();
            this.idVenta = id;
        }
        private void EditarVenta_Load(object sender, EventArgs e)
        {
            // Cargar datos si el id es válido
            VentaDAL ventaDAL = new VentaDAL();
            Venta venta = ventaDAL.ObtenerPorId(idVenta);
            idEquipo = venta.IdEquipo; // Asignar el IdEquipo para usarlo al guardar
            idUsuario = venta.IdUsuario;
            if (venta != null)
            {
                calendarFecha.SetDate(venta.Fecha);
                numericCantidad.Value = venta.Cantidad;
                numericTotal.Value = venta.Total;
            }
            else
            {
                MessageBox.Show("No se encontró la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Venta ventaEditada = new Venta
                {
                    IdVenta = idVenta,
                    IdEquipo = idEquipo, // Usar el IdEquipo cargado
                    IdUsuario = idUsuario,
                    Fecha = calendarFecha.SelectionStart,
                    Cantidad = (int)numericCantidad.Value,
                    Total = numericTotal.Value

                };
                VentaDAL ventaDAL = new VentaDAL();
                bool resultado = ventaDAL.Actualizar(ventaEditada);

                if (resultado)
                {
                    MessageBox.Show("Venta actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
