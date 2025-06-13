using medicos_y_biomedicos.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace medicos_y_biomedicos.Formularios
{
    public partial class DetalleVentas : Form
    {
        public DetalleVentas()
        {
            InitializeComponent();
            MostrarTodosLosDetalles();
        }

        private void MostrarTodosLosDetalles()
        {
            // Obtener los datos extendidos
            VentaDAL ventaDAL = new VentaDAL();
            var lista = ventaDAL.ObtenerTodosLosDetallesVentaExtendido();

            // Limpiar las filas anteriores
            registros.Rows.Clear();

            // Configuración de las columnas
            if (registros.Columns.Count == 0)
            {
                // Crear las columnas necesarias
                registros.Columns.Add("Nombre", "Nombre");
                registros.Columns.Add("Marca", "Marca");
                registros.Columns.Add("Modelo", "Modelo");
                registros.Columns.Add("Precio", "Precio");
                registros.Columns.Add("Categoria", "Categoria");
                registros.Columns.Add("Cantidad", "Cantidad");
                registros.Columns.Add("Total", "Total");
                registros.Columns.Add("NombreUsuario", "Nombre Usuario");
                registros.Columns.Add("ApellidoUsuario", "Apellido Usuario");
                registros.Columns.Add("CuentaUsuario", "Cuenta Usuario");
                registros.Columns.Add("DireccionUsuario", "Dirección Usuario");
            }

            // Insertar los datos en el DataGridView
            foreach (var detalle in lista)
            {
                // Aquí 'detalle' es un objeto que contiene los datos para cada fila.
                // Necesitarás mapear sus propiedades a las columnas del DataGridView.

                // Ejemplo de cómo insertar una fila de datos
                registros.Rows.Add(
                    detalle.NombreEquipo,
                    detalle.Marca,
                    detalle.Modelo,
                    detalle.Precio.ToString("C"), // Formatear como moneda
                    detalle.Categoria,
                    detalle.Cantidad,
                    detalle.TotalDetalle.ToString("C"), // Formatear como moneda
                    detalle.NombreUsuario,
                    detalle.Apellido,
                    detalle.Cuenta,
                    detalle.Direccion
                );
            }

            // Si necesitas ordenar por alguna columna, por ejemplo por 'Precio':
            registros.Sort(registros.Columns["Precio"], ListSortDirection.Descending);
        }



    }
}
