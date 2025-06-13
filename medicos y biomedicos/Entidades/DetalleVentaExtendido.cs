using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Entidades
{
    public class DetalleVentaExtendido
    {
        // Usuario
        public string NombreUsuario { get; set; }
        public string Apellido { get; set; }
        public string Cuenta { get; set; }
        public string Direccion { get; set; }

        // Equipo
        public string NombreEquipo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public string Categoria { get; set; }

        // DetalleVenta
        public int Cantidad { get; set; }
        public decimal TotalDetalle { get; set; }
    }
}
