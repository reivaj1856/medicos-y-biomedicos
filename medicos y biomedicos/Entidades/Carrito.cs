using medicos_y_biomedicos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Datos
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public int IdUsuario { get; set; }
        // Relación: un carrito tiene muchos detalles
        public List<CarritoDetalle> Detalles { get; set; } = new List<CarritoDetalle>();
        }
}
