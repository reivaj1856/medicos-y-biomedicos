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
                numericPrecio.Value <= 0)
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
                    Precio = numericPrecio.Value
                };

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
    }
}
