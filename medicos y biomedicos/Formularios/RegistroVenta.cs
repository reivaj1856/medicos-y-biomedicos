using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Entidades;
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
            VentaDAL ventaDAL = new VentaDAL();           

            List<Venta> ventas = ventaDAL.Listar();

            panelReVentas.Controls.Clear();

            foreach (Venta venta in ventas)
            {
                EquipoDAL equipoDAL = new EquipoDAL();
                UsuarioDAL usuarioDAL = new UsuarioDAL();
 
                Usuario usuario = usuarioDAL.ObtenerPorId(venta.IdUsuario);

                Panel contenedor = new Panel
                {
                    Width = 240,
                    Height = 110,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.None
                };

                Label label = new Label
                {
                    Text = $"Venta ID: {venta.IdVenta}\n" +
                           $"Cuenta: {usuario?.Cuenta ?? "N/A"}\n" +                   
                           $"Total: {venta.Total} Bs\n" +
                           $"Fecha: {venta.Fecha:dd/MM/yyyy}",
                    AutoSize = false,
                    Width = 220,
                    Height = 70,
                    Location = new Point(10, 10),
                    TextAlign = ContentAlignment.TopLeft,
                    Font = new Font("Consolas", 10, FontStyle.Regular)
                };

                // Botón Editar
                Button btnEditar = new Button
                {
                    Text = "Editar",
                    Width = 70,
                    Height = 30,
                    Location = new Point(35, 80)
                };
                btnEditar.Click += (s, e) => {

                    EditVenta formEditar = new EditVenta(venta.IdVenta);
                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        CargarVentas(); // recargar si se editó
                    }
                };

                // Botón Eliminar
                Button btnEliminar = new Button
                {
                    Text = "Eliminar",
                    Width = 70,
                    Height = 30,
                    Location = new Point(130, 80)
                };

                btnEliminar.Click += (s, e) =>
                {
                    DialogResult result = MessageBox.Show(
                        "¿Estás seguro de eliminar esta venta?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {

                        if (ventaDAL.Eliminar(venta.IdVenta))
                        {
                            MessageBox.Show("Venta eliminada correctamente.");
                            CargarVentas(); // Recargar lista
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la venta.");
                        }
                    }
                };
                contenedor.Controls.Add(btnEditar);
                contenedor.Controls.Add(btnEliminar);
                contenedor.Controls.Add(label);
                panelReVentas.Controls.Add(contenedor);
            }
        }

        private void FormReVentas_Shown(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarVentas();
        }
    }
}
