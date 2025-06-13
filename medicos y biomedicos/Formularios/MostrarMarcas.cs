using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace medicos_y_biomedicos.Formularios
{
    public partial class MostrarMarcas : Form
    {
        private string valorFiltro;
        private bool porModelo;
        private Usuario us;

        public MostrarMarcas(string valorFiltro, Usuario us, bool porModelo)
        {
            InitializeComponent();
            this.valorFiltro = valorFiltro;
            this.porModelo = porModelo;
            this.us = us;
        }

        private void MostrarMarcas_Load(object sender, EventArgs e)
        {
            CargarEquipos();
        }

        private void CargarEquipos()
        {
            EquipoDAL dal = new EquipoDAL();
            List<Equipo> equipos = porModelo ? dal.ObtenerPorModelo(valorFiltro)
                                             : dal.ObtenerPorCategoria(valorFiltro);

            panelVentas.Controls.Clear();

            foreach (Equipo item in equipos)
            {
                Panel contenedor = new Panel
                {
                    Width = 240,
                    Height = 375,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.None,
                };

                PictureBox pictureBox = new PictureBox
                {
                    Width = 220,
                    Height = 200,
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
                    Text = $"{item.Nombre}\nMarca: {item.Marca} \n Categoria: {item.Categoria}\nPrecio: {item.Precio} Bs\nCantidad: {item.Cantidad} ",
                    AutoSize = false,
                    Width = 220,
                    Height = 105,
                    Location = new Point(10, 210),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Consolas", 14, FontStyle.Regular),
                };

                NumericUpDown numericCantidad = new NumericUpDown
                {
                    Width = 100,
                    Height = 30,
                    Location = new Point(50, 317),
                    Minimum = 1,
                    Maximum = item.Cantidad
                };

                Label sinStock = new Label
                {
                    Text = $"SIN EXISTENCIAS",
                    AutoSize = false,
                    Width = 220,
                    Height = 30,
                    Location = new Point(20, 317),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Consolas", 14, FontStyle.Regular),
                };

                Button btnRealizarCompra = new Button
                {
                    Text = "Realizar Compra",
                    Width = 100,
                    Height = 30,
                    Location = new Point(10, 340)
                };

                btnRealizarCompra.Click += (s, e) =>
                {
                    EquipoDAL equipoDAL = new EquipoDAL();
                    Equipo equipo = equipoDAL.ObtenerPorId(item.IdEquipo);
                    decimal cantidad = numericCantidad.Value;

                    if (equipo.Cantidad > 0)
                    {
                        decimal total = cantidad * item.Precio;
                        DetalleVenta det = new DetalleVenta
                        {
                            IdEquipo = equipo.IdEquipo,
                            Cantidad = cantidad,
                            Total = total
                        };

                        Venta nuevaVenta = new Venta
                        {
                            IdUsuario = us.IdUsuario,
                            Fecha = DateTime.Now,
                            Total = total,
                            Detalles = new List<DetalleVenta> { det }
                        };

                        VentaDAL ventaDAL = new VentaDAL();
                        int idVentaGenerado = ventaDAL.Insertar(nuevaVenta);

                        if (idVentaGenerado > 0)
                        {
                            item.Cantidad = item.Cantidad- cantidad;
                            equipoDAL.Actualizar(item);
                            MessageBox.Show("Venta registrada con éxito.");
                            VentaFormulario ventaForm = new VentaFormulario(idVentaGenerado, us);
                            ventaForm.ShowDialog();
                            CargarEquipos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar la venta.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay existencias disponibles.");
                    }
                };

                Button btnCarrito = new Button
                {
                    Text = "Agregar al carrito",
                    Width = 100,
                    Height = 30,
                    Location = new Point(120, 340)
                };

                btnCarrito.Click += (s, e) =>
                {
                    decimal cantidad = numericCantidad.Value;
                    decimal total = cantidad * item.Precio;

                    CarritoDAL carritoDAL = new CarritoDAL();
                    int idCarrito = carritoDAL.CrearCarritoSiNoExiste(us.IdUsuario);

                    CarritoDetalle detalle = new CarritoDetalle
                    {
                        IdCarrito = idCarrito,
                        IdEquipo = item.IdEquipo,
                        Cantidad = (int)cantidad
                    };

                    CarritoDetalleDAL detalleDAL = new CarritoDetalleDAL();
                    bool insertado = detalleDAL.Insertar(detalle);

                    if (insertado)
                    {
                        MessageBox.Show("Producto agregado al carrito.");

                        CargarEquipos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar al carrito.");
                    }
                };

                contenedor.Controls.Add(btnRealizarCompra);
                contenedor.Controls.Add(btnCarrito);
                contenedor.Controls.Add(pictureBox);
                contenedor.Controls.Add(label);

                if (item.Cantidad > 0)
                    contenedor.Controls.Add(numericCantidad);
                else
                    contenedor.Controls.Add(sinStock);

                panelVentas.Controls.Add(contenedor);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e) => Close();
        private void btnActualizar_Click(object sender, EventArgs e) => CargarEquipos();
    }

}
