using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private Usuario us;
        public VentanaVenta(Usuario us)
        {
            InitializeComponent();  
            comboBox1.SelectedIndex = 0; // Seleccionar el primer elemento por defecto
            this.us = us; // Guardar el usuario para futuras referencias si es necesario
            // o agrégalo a un panel si es necesario
        }

        private void CargarEquipos()
        {
            EquipoDAL dal = new EquipoDAL();
            List<Equipo> listaEquipos = dal.Listar();

            // Obtener criterio seleccionado del ComboBox
            string criterio = comboBox1.SelectedItem?.ToString();

            // Usamos un HashSet para evitar duplicados
            HashSet<string> itemsMostrados = new HashSet<string>();
            panelVentas.Controls.Clear();

            foreach (Equipo item in listaEquipos)
            {
                string clave = criterio == "Categoría" ? item.Categoria : item.Modelo;

                if (itemsMostrados.Contains(clave))
                    continue;

                itemsMostrados.Add(clave);

                // Crear panel contenedor
                Panel contenedor = new Panel
                {
                    Width = 240,
                    Height = 275,
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
                    Text = clave,
                    AutoSize = false,
                    Width = 220,
                    Height = 60,
                    Location = new Point(10, 210),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Consolas", 14, FontStyle.Regular),
                };

                // Eventos según filtro
                if (criterio == "Categoría")
                {
                    pictureBox.Click += (s, e) => AbrirFormulario(new MostrarMarcas(clave, us,false));
                    label.Click += (s, e) => AbrirFormulario(new MostrarMarcas(clave, us, false));
                    contenedor.Click += (s, e) => AbrirFormulario(new MostrarMarcas(clave, us, false));
                }
                else // Modelo
                {
                    pictureBox.Click += (s, e) => AbrirFormulario(new MostrarMarcas(clave, us, true));
                    label.Click += (s, e) => AbrirFormulario(new MostrarMarcas(clave, us, true));
                    contenedor.Click += (s, e) => AbrirFormulario(new MostrarMarcas(clave, us, true));
                }

                contenedor.Controls.Add(pictureBox);
                contenedor.Controls.Add(label);
                panelVentas.Controls.Add(contenedor);
            }
        }


        private void VentanaVenta_Load(object sender, EventArgs e)
        {
            CargarEquipos();
           
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarEquipos();
        }
        private void AbrirFormulario(Form formulario)
        {
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            panelVentas1.Controls.Clear();
            panelVentas1.Controls.Add(formulario);
            panelVentas1.Tag = formulario;

            formulario.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEquipos();
        }
    }
}
