using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Entidades;

namespace medicos_y_biomedicos.Formularios
{
    public partial class login : Form
    {
        private Usuario usuarioActual; // Para almacenar el usuario actual después del login
        public login()
        {
            InitializeComponent();
            textContraseña.UseSystemPasswordChar = true;
            this.usuarioActual = null; // Inicializar el usuario actual como null
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iniciarSesion();
        }
        private void iniciarSesion() {
            string cuenta = textCuenta.Text.Trim();
            string contrasena = textContraseña.Text.Trim();

            if (string.IsNullOrEmpty(cuenta) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Debe llenar ambos campos.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UsuarioDAL dal = new UsuarioDAL();
            bool valido = dal.ValidarCredenciales(cuenta, contrasena);

            if (valido)
            {
                usuarioActual = dal.ObtenerUsuarioPorCuenta(cuenta); // Obtener el usuario actual
                this.Close(); // Cierra el formulario actual
                              // Puedes abrir tu ventana principal aquí, si deseas
            }
            else
            {
                MessageBox.Show("La cuenta o la contraseña son incorrectas.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBoxCerrar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Debe iniciar sesión para continuar.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.Exit();
        }
        
        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.ShowDialog(); // Muestra el formulario de registro como un diálogo modal
        }

        private void textContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                iniciarSesion();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        public Usuario getUsuario()
        {
            return usuarioActual; // Retorna el usuario actual después del login
        }
    }
}
