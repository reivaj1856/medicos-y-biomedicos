using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Entidades
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdEquipo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        // Constructor vacío
        public Venta() { }

        // Constructor con parámetros
        public Venta(int idVenta, int idEquipo, int idUsuario, DateTime fecha, int cantidad, decimal total)
        {
            IdVenta = idVenta;
            IdEquipo = idEquipo;
            IdUsuario = idUsuario;
            Fecha = fecha;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
