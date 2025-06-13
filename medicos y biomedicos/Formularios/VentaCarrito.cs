using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Entidades;
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

namespace medicos_y_biomedicos.Formularios
{
    public partial class VentaCarrito : Form
    {
        private Usuario usuarioActual; // Variable para almacenar el usuario actual
        public VentaCarrito(Usuario usuario)
        {
            usuarioActual= usuario; // Asignar el usuario actual
            InitializeComponent();
            CargarEquiposDelCarrito(usuarioActual.IdUsuario);
        }
        private void CargarEquiposDelCarrito(int idUsuario)
        {
            CarritoDetalleDAL carritoDAL = new CarritoDetalleDAL();
            List<Equipo> equipos = carritoDAL.ObtenerEquiposDelCarrito(idUsuario);

            panelCarrito.Controls.Clear(); // Asegúrate de tener un FlowLayoutPanel llamado panelCarrito

            foreach (Equipo item in equipos)
            {
                Panel contenedor = new Panel
                {
                    Width = 200,
                    Height = 300,
                    Margin = new Padding(10),
                    BorderStyle = BorderStyle.FixedSingle
                };

                PictureBox pictureBox = new PictureBox
                {
                    Width = 180,
                    Height = 150,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(10, 10)
                };

                if (item.Imagen != null)
                {
                    using (MemoryStream ms = new MemoryStream(item.Imagen))
                    {
                        pictureBox.Image = Image.FromStream(ms);
                    }
                }

                Label label = new Label
                {
                    Text = $"{item.Nombre}\nMarca: {item.Marca}\nModelo: {item.Modelo}\nPrecio: {item.Precio} Bs\nCantidad: {item.Cantidad}",
                    AutoSize = false,
                    Width = 180,
                    Height = 80,
                    Location = new Point(10, 170),
                    TextAlign = ContentAlignment.TopLeft
                };

                Button btnEliminar = new Button
                {
                    Text = "Eliminar",
                    Width = 80,
                    Height = 30,
                    Location = new Point(60, 260)
                };

                btnEliminar.Click += (s, e) =>
                {
                    CarritoDetalleDAL detalleDAL = new CarritoDetalleDAL();
                    bool eliminado = detalleDAL.EliminarItemDelCarrito(idUsuario, item.IdEquipo); // necesitas este método

                    if (eliminado)
                    {
                        MessageBox.Show("Producto eliminado del carrito.");
                        CargarEquiposDelCarrito(idUsuario);
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el producto.");
                    }
                };

                contenedor.Controls.Add(pictureBox);
                contenedor.Controls.Add(label);
                contenedor.Controls.Add(btnEliminar);
                panelCarrito.Controls.Add(contenedor);
            }

            if (equipos.Count == 0)
            {
                MessageBox.Show("Tu carrito está vacío.");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarEquiposDelCarrito(usuarioActual.IdUsuario);
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            RealizarCompraDesdeCarrito(usuarioActual.IdUsuario);
            CargarEquiposDelCarrito(usuarioActual.IdUsuario);
        }
        public bool RealizarCompraDesdeCarrito(int idUsuario)
        {
            CarritoDAL carritoDAL = new CarritoDAL();
            CarritoDetalleDAL detalleCarritoDAL = new CarritoDetalleDAL();
            EquipoDAL equipoDAL = new EquipoDAL();
            VentaDAL ventaDAL = new VentaDAL();

            List<Equipo> equipos = carritoDAL.ObtenerPorUsuario(idUsuario);

            if (equipos.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.");
                return false;
            }

            List<DetalleVenta> detalles = new List<DetalleVenta>();
            decimal totalVenta = 0;

            foreach (Equipo equipo in equipos)
            {
                if (equipo.Cantidad <= 0)
                {
                    MessageBox.Show($"El producto {equipo.Nombre} no tiene cantidad válida en el carrito.");
                    return false;
                }

                Equipo equipoStock = equipoDAL.ObtenerPorId(equipo.IdEquipo);
                if (equipoStock.Cantidad < equipo.Cantidad)
                {
                    MessageBox.Show($"No hay suficiente stock para {equipo.Nombre}. Stock disponible: {equipoStock.Cantidad}");
                    return false;
                }

                decimal subtotal = equipo.Precio * equipo.Cantidad;

                detalles.Add(new DetalleVenta
                {
                    IdEquipo = equipo.IdEquipo,
                    Cantidad = equipo.Cantidad,
                    Total = subtotal
                });

                totalVenta += subtotal;
            }

            // Crear venta
            Venta venta = new Venta
            {
                IdUsuario = idUsuario,
                Fecha = DateTime.Now,
                Total = totalVenta,
                Detalles = detalles
            };

            int idVentaGenerada = ventaDAL.Insertar(venta); // Inserta venta y sus detalles

            if (idVentaGenerada > 0)
            {
                // Actualizar el stock
                foreach (var detalle in detalles)
                {
                    Equipo equipo = equipoDAL.ObtenerPorId(detalle.IdEquipo);
                    equipo.Cantidad -= detalle.Cantidad;
                    equipoDAL.Actualizar(equipo);
                }

                // Vaciar el carrito
                carritoDAL.EliminarCarrito(idUsuario);
                VentaFormulario ventaForm = new VentaFormulario(idVentaGenerada, usuarioActual);
                ventaForm.ShowDialog(); // Mostrar la venta realizada
                MessageBox.Show("Compra realizada con éxito.");
                return true;
            }
            else
            {
                MessageBox.Show("Error al registrar la venta.");
                return false;
            }
        }

    }
}
