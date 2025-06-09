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
    public partial class RegistrarMantenimiento : Form
    {
        private int id;
        public RegistrarMantenimiento()
        {
            InitializeComponent();
            this.id = -1;
        }
        public RegistrarMantenimiento(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void RegistrarMantenimiento_Load(object sender, EventArgs e)
        {
            if (id != -1)
            {
                MantenimientoDAL mantenimientoDAL = new MantenimientoDAL();
                Mantenimiento m = mantenimientoDAL.ObtenerPorId(id);
                if (m != null)
                {
                    richTextBoxdetalles.Text = m.Descripcion;
                    monthCalendar.SetDate(m.FechaIngreso);
                    MostrarImagen(m.Imagen); // Mostrar imagen si existe
                }
                else
                {
                    MessageBox.Show("No se encontró el mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBoxdetalles.Text))
            {
                MessageBox.Show("Por favor, escriba los detalles del mantenimiento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MantenimientoDAL mantenimientoDAL = new MantenimientoDAL();
                Mantenimiento nuevo = new Mantenimiento
                {
                    IdMantenimiento = this.id,
                    FechaIngreso = monthCalendar.SelectionStart,
                    Estado = "En proceso",
                    Descripcion = richTextBoxdetalles.Text.Trim(),
                    Imagen = ImagenAPBytes()
                };

                bool resultado;

                if (id == -1)
                {
                    resultado = mantenimientoDAL.Insertar(nuevo);
                    if (resultado)
                        MessageBox.Show("Mantenimiento registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    resultado = mantenimientoDAL.Actualizar(nuevo);
                    if (resultado)
                        MessageBox.Show("Mantenimiento actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (resultado)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al guardar el mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LimpiarCamposMantenimiento();
        }
        private void LimpiarCamposMantenimiento()
        {
            richTextBoxdetalles.Text = "";
            monthCalendar.SetDate(DateTime.Today); // Opcional: reset a la fecha actual
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
    }
}
