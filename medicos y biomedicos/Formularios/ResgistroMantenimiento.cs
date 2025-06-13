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
    public partial class ResgistroMantenimiento : Form
    {
        private Usuario usuarioActual;
        public ResgistroMantenimiento(Usuario us)
        {
            InitializeComponent();
            usuarioActual = us;
        }
        private void ResgistroMantenimiento_Load(object sender, EventArgs e)
        {
            CargarMantenimientos();
        }
        
        private void CargarMantenimientos()
        {
            MantenimientoDAL dal = new MantenimientoDAL();

            dataMantenimiento.Controls.Clear();

            foreach (Mantenimiento item in dal.Listar())
            {
                // Crear panel contenedor
                Panel contenedor = new Panel
                {
                    Width = 180,
                    Height = 310,
                    Margin = new Padding(10),
                    BorderStyle = BorderStyle.None
                };

                // Imagen
                PictureBox pictureBox = new PictureBox
                {
                    Width = 160,
                    Height = 165,
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
                    Text = $"{item.FechaIngreso}\n{item.Descripcion}\n{item.Estado}\n{ item.Precio }Bs",
                    AutoSize = false,
                    Width = 160,
                    Height = 60,
                    Location = new Point(10, 180),
                    TextAlign = ContentAlignment.TopCenter
                };

                // Botón Vender

                Button btnEstado = new Button
                {
                    Text = "Estado",
                    Width = 50,
                    Height = 30,
                    Location = new Point(10, 240)
                };
                btnEstado.Click += (s, e) => {

                    if (item != null)
                    {
                        string nuevoEstado = item.Estado.ToLower() == "en proceso" ? "Finalizado" : "En proceso";
                        bool actualizado = dal.CambiarEstado(item.IdMantenimiento, nuevoEstado);

                        if (actualizado)
                        {
                            CargarMantenimientos(); // recargar lista
                        }
                        else
                        {
                            MessageBox.Show("No se pudo cambiar el estado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el mantenimiento con ese ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                // Botón Editar
                Button btnEditar = new Button
                {
                    Text = "Editar",
                    Width = 50,
                    Height = 30,
                    Location = new Point(65, 240)
                };
                btnEditar.Click += (s, e) => {

                    RegistrarMantenimiento formEditar = new RegistrarMantenimiento(item.IdMantenimiento);
                    if (formEditar.ShowDialog() == DialogResult.OK)
                    {
                        CargarMantenimientos(); // recargar si se editó
                    }
                };

                // Botón Eliminar
                Button btnEliminar = new Button
                {
                    Text = "Elimina",
                    Width = 50,
                    Height = 30,
                    Location = new Point(120, 240)
                };
                btnEliminar.Click += (s, e) =>
                {
                    if (MessageBox.Show("¿Estás seguro de eliminar este equipo?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (dal.Eliminar(item.IdMantenimiento))
                        {
                            MessageBox.Show("Equipo eliminado correctamente.");
                            CargarMantenimientos(); // Recargar lista
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar.");
                        }
                    }
                };


                Button btnFactura = new Button
                {
                    Text = "Factura",
                    Width = 160,
                    Height = 30,
                    Location = new Point(10, 275)
                };
                btnFactura.Click += (s, e) => {
                    facturaMantenimiento facturaForm = new facturaMantenimiento(usuarioActual,(int)item.IdMantenimiento);
                    facturaForm.ShowDialog();

                };

                // Agregar controles al panel contenedor
                contenedor.Controls.Add(pictureBox);
                contenedor.Controls.Add(label);
                contenedor.Controls.Add(btnEstado);
                contenedor.Controls.Add(btnEditar);
                contenedor.Controls.Add(btnEliminar);
                contenedor.Controls.Add(btnFactura);

                // Agregar al FlowLayoutPanel
                dataMantenimiento.Controls.Add(contenedor);
            }
        }

        

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMantenimientos();
        }
    }
}   
