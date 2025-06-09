using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using medicos_y_biomedicos.Entidades;

namespace medicos_y_biomedicos.Formularios
{
    public partial class VentaFormulario : Form
    {
        private int idEquipo;
        private string facturaTexto;
        private Usuario usuarioActual;

        public VentaFormulario(int id, Usuario us)
        {
            InitializeComponent();
            usuarioActual = us; // Guardar el usuario actual para futuras referencias
            this.idEquipo = id;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarFactura();
        }

        private string GetFacturaTexto()
        {
            return facturaTexto;
        }

        private void GenerarFactura()
        {
            // Validar campos
            string nombreCliente = usuarioActual.Nombre;
            string direccionCliente = "No especificada"; // Asegúrate de que exista este campo

            if (string.IsNullOrEmpty(nombreCliente))
            {
                MessageBox.Show("Por favor, complete los datos del cliente.", "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Equipo equipoSeleccionado = ObtenerEquipoSeleccionado();

            if (equipoSeleccionado == null)
            {
                MessageBox.Show("No se ha seleccionado ningún equipo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Datos ficticios de la empresa
            string nombreEmpresa = "BioMedTech S.R.L.";
            string direccionEmpresa = "Av. Salud 123 - Cochabamba, Bolivia";

            // Datos de la factura
            string numeroFactura = $"#FAC{DateTime.Now.Ticks % 100000}";
            DateTime fecha = DateTime.Now;
            DateTime fechaVencimiento = fecha.AddDays(7); // por ejemplo, 7 días de plazo
            decimal cantidad = 1;
            decimal precioUnitario = equipoSeleccionado.Precio;
            decimal subtotal = cantidad * precioUnitario;
            decimal impuesto = subtotal * 0.13m; // 13% de impuesto
            decimal total = subtotal + impuesto;

            // Crear factura en texto
            StringBuilder factura = new StringBuilder();
            factura.AppendLine($"FACTURA {numeroFactura}");
            factura.AppendLine($"Fecha: {fecha:dd/MM/yyyy}");
            factura.AppendLine();
            factura.AppendLine("EMISOR:");
            factura.AppendLine($"Nombre: {nombreEmpresa}");
            factura.AppendLine($"Dirección: {direccionEmpresa}");
            factura.AppendLine();
            factura.AppendLine("CLIENTE:");
            factura.AppendLine($"Nombre: {nombreCliente}");
            factura.AppendLine($"Dirección: {direccionCliente}");
            factura.AppendLine();
            factura.AppendLine("----------------------------------------------------");
            factura.AppendLine("| Artículo/Servicio     | Cantidad | Precio Unitario |");
            factura.AppendLine("----------------------------------------------------");
            factura.AppendLine($"| {equipoSeleccionado.Nombre,-21} | {cantidad,7} | {precioUnitario,15:C} |");
            factura.AppendLine("----------------------------------------------------");
            factura.AppendLine($"Subtotal:   {subtotal:C}");
            factura.AppendLine($"Impuestos:  {impuesto:C}");
            factura.AppendLine($"Total:      {total:C}");
            factura.AppendLine();
            factura.AppendLine("Condiciones de Pago: Pago contra entrega");
            factura.AppendLine($"Fecha de Vencimiento: {fechaVencimiento:dd/MM/yyyy}");
            factura.AppendLine();
            factura.AppendLine("Gracias por su compra!");

            // Guardar factura
            facturaTexto = factura.ToString();

            // Mostrar en el RichTextBox
            textFactura.Text = facturaTexto;

            // Preguntar si desea imprimir
            DialogResult resultado = MessageBox.Show("¿Desea imprimir la factura?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                ImprimirFactura();
            }
        }


        private void ImprimirFactura()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(PrintFacturaPage);

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDoc
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void PrintFacturaPage(object sender, PrintPageEventArgs e)
        {
            Font fuente = new Font("Consolas", 12);
            float y = 10;
            float x = 10;
            float lineHeight = fuente.GetHeight(e.Graphics);

            using (StringReader reader = new StringReader(facturaTexto))
            {
                string linea;
                while ((linea = reader.ReadLine()) != null)
                {
                    e.Graphics.DrawString(linea, fuente, Brushes.Black, x, y);
                    y += lineHeight;
                }
            }
        }

        private Equipo ObtenerEquipoSeleccionado()
        {
            EquipoDAL equipoDAL = new EquipoDAL();
            return equipoDAL.ObtenerPorId(idEquipo);
        }
    }

}
