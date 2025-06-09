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
            this.us = us; // Guardar el usuario para futuras referencias si es necesario
        }

        private void CargarEquipos()
        {
            EquipoDAL dal = new EquipoDAL();
            List<Equipo> listaEquipos = dal.Listar();

            // HashSet para evitar modelos duplicados
            HashSet<string> modelosMostrados = new HashSet<string>();

            panelVentas.Controls.Clear();

            foreach (Equipo item in listaEquipos)
            {
                if (modelosMostrados.Contains(item.Modelo))
                    continue; // Ya se mostró un equipo de este modelo

                modelosMostrados.Add(item.Modelo); // Registrar el modelo como mostrado

                // Crear panel contenedor
                Panel contenedor = new Panel
                {
                    Width = 240,
                    Height = 275,
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
                    Text = $"{item.Modelo}",
                    AutoSize = false,
                    Width = 220,
                    Height = 60,
                    Location = new Point(10, 210),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("Consolas", 14, FontStyle.Regular),
                };

                // Eventos opcionales de clic
                pictureBox.Click += (s, e) =>  AbrirFormulario(new MostrarMarcas(item.Modelo,this.us)); 
                label.Click += (s, e) => AbrirFormulario(new MostrarMarcas(item.Modelo, this.us));
                contenedor.Click += (s, e) => AbrirFormulario(new MostrarMarcas(item.Modelo, this.us));

                // Agregar controles al panel contenedor
                contenedor.Controls.Add(pictureBox);
                contenedor.Controls.Add(label);

                // Agregar al FlowLayoutPanel
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


    }
}
