using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Entidades
{
    public class CarritoDetalle
    {
        public int IdDetalle { get; set; }
        public int IdCarrito { get; set; }
        public int IdEquipo { get; set; }
        public int Cantidad { get; set; }
        // (Opcional) Si deseas acceder al nombre o precio del equipo desde la clase
        public Equipo Equipo { get; set; }
    }
}
