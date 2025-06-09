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
    public partial class RegistrarEquipo : Form
    {
        private int id;
        public RegistrarEquipo()
        {
            InitializeComponent();
            this.id = -1; // Inicializar id a 0 para nuevo registro
            
        }
        public RegistrarEquipo(int id)
        {
            InitializeComponent();
            this.id = id; // Asignar el id del equipo a editar
        }
        private void RegistrarEquipo_Load(object sender, EventArgs e)
        {
            if (id != -1)
            {
                // Obtener equipo desde la base de datos y rellenar campos
                EquipoDAL dal= new EquipoDAL();
                Equipo equipo = dal.ObtenerPorId(id);
                if (equipo != null)
                {
                    textBoxEquipo.Text = equipo.Nombre;
                    textBoxMarca.Text = equipo.Marca;
                    textBoxModelo.Text = equipo.Modelo;
                    numericPrecio.Value = equipo.Precio;
                    MostrarImagen(equipo.Imagen); // Mostrar imagen si existe
                }
                else
                {
                    MessageBox.Show("No se encontró el equipo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            EquipoDAL equipoDAL = new EquipoDAL();
            // Validar campos
            if (string.IsNullOrWhiteSpace(textBoxEquipo.Text) ||
                string.IsNullOrWhiteSpace(textBoxMarca.Text) ||
                string.IsNullOrWhiteSpace(textBoxModelo.Text) ||
                numericPrecio.Value <= 1)
            {
                MessageBox.Show("Por favor, complete todos los campos correctamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Equipo nuevo = new Equipo
                {
                    IdEquipo = this.id, // puede ser -1 o un valor válido
                    Nombre = textBoxEquipo.Text.Trim(),
                    Marca = textBoxMarca.Text.Trim(),
                    Modelo = textBoxModelo.Text.Trim(),
                    Precio = numericPrecio.Value,
                    Imagen = ImagenAPBytes() // Convertir imagen a bytes
                };

                byte[] imgBytes = ImagenAPBytes();
                if (imgBytes == null || imgBytes.Length == 0)
                {
                    MessageBox.Show("Imagen vacía o no válida");
                    return;
                }

                bool resultado;

                if (id == -1)
                {
                    resultado = equipoDAL.Insertar(nuevo);
                    if (resultado)
                        MessageBox.Show("Equipo registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    resultado = equipoDAL.Actualizar(nuevo);
                    if (resultado)
                        MessageBox.Show("Equipo actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (resultado)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al guardar los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            textBoxEquipo.Text = "";
            textBoxMarca.Text = "";
            textBoxModelo.Text = "";
            numericPrecio.Value = 0;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
        private byte[] ImagenAPBytes()
        {
            if (pictureBox1.Image == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                return ms.ToArray();
            }
        }
        private void MostrarImagen(byte[] datosImagen)
        {
            if (datosImagen != null)
            {
                using (MemoryStream ms = new MemoryStream(datosImagen))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pictureBox1.Image = null; // O alguna imagen por defecto
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
