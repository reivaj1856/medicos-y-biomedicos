using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Entidades
{
    public class Mantenimiento
    {
        public int IdMantenimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public byte[] Imagen { get; internal set; }
        public decimal Precio { get; set; } // Precio del mantenimiento

    }
}
