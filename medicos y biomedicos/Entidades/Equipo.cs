using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Entidades
{
    public class Equipo
    {
        public int IdEquipo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public string Categoria { get; set; }
        public byte[] Imagen { get; internal set; }
    }
}
