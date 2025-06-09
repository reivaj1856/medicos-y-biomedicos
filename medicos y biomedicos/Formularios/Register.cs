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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void pictureBoxCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = textNombre.Text.Trim();
            string cuenta = textCuenta.Text.Trim();
            string contraseña = textContraseña.Text.Trim();
            string administrador = "no";
            byte[] imagen = null;

            // Validar campos básicos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(cuenta) || string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Debes ingresar un nombre, una cuenta y una contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener imagen si existe
            if (pictureBox3.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox3.Image.Save(ms, pictureBox3.Image.RawFormat);
                    imagen = ms.ToArray();
                }
            }

            UsuarioDAL dal = new UsuarioDAL();

            // Verificar si la cuenta ya existe
            if (dal.ExisteCuenta(cuenta))
            {
                MessageBox.Show("La cuenta ya existe. Por favor, ingresa otra diferente.", "Cuenta existente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear nuevo usuario
            Usuario nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Cuenta = cuenta,
                Contraseña = contraseña,
                Administrador = administrador,
                Imagen = imagen
            };

            bool resultado = dal.CrearUsuario(nuevoUsuario);

            if (resultado)
            {
                MessageBox.Show("Usuario registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textNombre.Clear();
                textCuenta.Clear();
                textContraseña.Clear();
                pictureBox3.Image = null;
                
            }
            else
            {
                MessageBox.Show("Error al registrar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBoxCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        public byte[] ConvertirImagenABytes(Image imagen)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
