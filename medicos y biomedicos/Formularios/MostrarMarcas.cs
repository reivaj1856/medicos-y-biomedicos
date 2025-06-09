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

namespace medicos_y_biomedicos.Formularios
{
    public partial class MostrarMarcas : Form
    {
        private string modelo;
        private Usuario us;
        public MostrarMarcas(string modelo,Usuario us)
        {
            InitializeComponent();
            this.modelo = modelo;
            this.us = us; // Guardar el usuario para futuras referencias si es necesario    
        }
        private void MostrarMarcas_Load(object sender, EventArgs e)
        {
            CargarEquiposPorModelo(this.modelo);
        }

        private void CargarEquiposPorModelo(string modelo)
        {
            EquipoDAL dal = new EquipoDAL();
            List<Equipo> equipos = dal.ObtenerPorModelo(modelo); // ✅ Pasamos el parámetro correctamente

        

            panelVentas.Controls.Clear(); // Limpiar el panel antes de cargar nuevos

            foreach (Equipo item in equipos)
            {
     
                // Crear panel contenedor
                Panel contenedor = new Panel
                {
                    Width = 240,
                    Height = 320,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.None,
                };

                // Imagen
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

                // Etiqueta con texto
                Label label = new Label
                {
                    Text = $"{item.Nombre}\n  Marca: {item.Marca}\n Precio: {item.Precio}Bs",
                    AutoSize = false,
                    Width = 220,
                    Height = 80,
                    Location = new Point(10, 210),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Consolas", 14, FontStyle.Regular),
                };

                Button btnRealizarCompra = new Button
                {
                    Text = "Realizar Compra",
                    Width = 220,
                    Height = 30,
                    Location = new Point(10, 290)
                };
                btnRealizarCompra.Click += (s, e) => {
                    int cantidad = 1;
                    decimal total = cantidad * item.Precio;

                    Venta nuevaVenta = new Venta
                    {
                        IdUsuario = us.IdUsuario, // Asegúrate de que el objeto 'item' tenga la propiedad IdUsuario
                        IdEquipo = item.IdEquipo,
                        Fecha = DateTime.Now,
                        Cantidad = cantidad,
                        Total = total
                    };
                    
                    VentaFormulario formEditar = new VentaFormulario(item.IdEquipo, us);
                    // recargar si se editó
                    VentaDAL ventaDAL = new VentaDAL();
                    if (ventaDAL.Insertar(nuevaVenta))
                    {
                        formEditar.ShowDialog();      
                    }
                    else
                    {
                        MessageBox.Show("No se pudo realizar la venta.");
                    }


                };

                contenedor.Controls.Add(btnRealizarCompra);
                contenedor.Controls.Add(pictureBox);
                contenedor.Controls.Add(label);

                panelVentas.Controls.Add(contenedor);
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarEquiposPorModelo(this.modelo);
        }
    }
}
