using medicos_y_biomedicos.Datos;
using medicos_y_biomedicos.Entidades;
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

namespace medicos_y_biomedicos.Formularios
{
    public partial class facturaMantenimiento : Form
    {
        private int idMantenimiento;
        private string facturaTexto;
        private Usuario usuarioActual;
        public facturaMantenimiento(Usuario us, int idMantenimiento)
        {
            InitializeComponent();
            usuarioActual = us; // Guardar el usuario actual para futuras referencias
            this.idMantenimiento = idMantenimiento;
            textFactura.Text = GetFacturaTexto();
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
        private void GenerarFactura()
        {
            // Validar que el usuario actual tiene un nombre
            string nombreCliente = usuarioActual.Nombre;
            string direccionCliente = "No especificada"; // Asegúrate de que exista este campo en usuarioActual

            if (string.IsNullOrEmpty(nombreCliente))
            {
                MessageBox.Show("Por favor, complete los datos del cliente.", "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Datos ficticios de la empresa
            string nombreEmpresa = "BioMedTech S.R.L.";
            string direccionEmpresa = "Av. Salud 123 - Cochabamba, Bolivia";

            // Obtener el mantenimiento por ID
            MantenimientoDAL mantenimientoDAL = new MantenimientoDAL();
            Mantenimiento mantenimiento = mantenimientoDAL.ObtenerPorId(idMantenimiento); // Obtener el mantenimiento por idMantenimiento
            if (mantenimiento == null)
            {
                MessageBox.Show("No se pudo obtener el mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Datos de la factura
            string numeroFactura = $"#FAC{mantenimiento.IdMantenimiento}";
            DateTime fecha = mantenimiento.FechaIngreso;
            DateTime fechaVencimiento = fecha.AddDays(7); // Por ejemplo, 7 días de plazo

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
            factura.AppendLine("| Servicio          | Descripción                     | Precio   |");
            factura.AppendLine("----------------------------------------------------");

            // Mostrar detalles del mantenimiento
            decimal totalFactura = mantenimiento.Precio;
            factura.AppendLine($"| Mantenimiento:    | {mantenimiento.Descripcion,-30} | {mantenimiento.Precio:C} |");
            factura.AppendLine("----------------------------------------------------");

            // Calcular impuestos y total
            decimal impuesto = totalFactura * 0.13m; // 13% de impuesto
            decimal total = totalFactura + impuesto;

            factura.AppendLine($"Subtotal:   {totalFactura:C}");
            factura.AppendLine($"Impuestos:  {impuesto:C}");
            factura.AppendLine($"Total:      {total:C}");
            factura.AppendLine();
            factura.AppendLine("Condiciones de Pago: Pago contra entrega");
            factura.AppendLine($"Fecha de Vencimiento: {fechaVencimiento:dd/MM/yyyy}");
            factura.AppendLine();
            factura.AppendLine("Gracias por confiar en nosotros.");

            // Guardar factura en el campo facturaTexto
            facturaTexto = factura.ToString();

            // Mostrar la factura en el RichTextBox
            textFactura.Text = facturaTexto;

            // Preguntar si desea imprimir
            DialogResult resultado = MessageBox.Show("¿Desea imprimir la factura?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                ImprimirFactura();
            }
        }
        private string GetFacturaTexto()
        {
            return facturaTexto;
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            GenerarFactura();
        }
    }
}
