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
            VentaDAL ventaDAL = new VentaDAL();
            Venta venta = ventaDAL.ObtenerPorId(idVenta);

            if (venta != null)
            {
                // Asignar usuario y fecha
                idUsuario = venta.IdUsuario;
                calendarFecha.SetDate(venta.Fecha);

                // Obtener el primer detalle de venta (asumimos uno solo)
                if (venta.Detalles != null && venta.Detalles.Count > 0)
                {
                    var detalle = venta.Detalles.First();

                    idEquipo = detalle.IdEquipo; // Guardar IdEquipo para actualizar luego

                    numericTotal.Value = venta.Total;
                }
                else
                {
                    MessageBox.Show("La venta no contiene detalles.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
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
                VentaDAL ventaDAL = new VentaDAL();
                // Crear entidad Venta con su detalle
                Venta ventaEditada = new Venta
                {
                    IdVenta = idVenta,
                    IdUsuario = idUsuario,
                    Fecha = calendarFecha.SelectionStart,
                    Total = numericTotal.Value,
                   
                };
                bool resultado = ventaDAL.ActualizarSolo(ventaEditada); // Este método debe actualizar Venta y DetalleVenta

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
