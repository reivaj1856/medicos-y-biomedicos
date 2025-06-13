using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicos_y_biomedicos.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string NIT { get; set; }
        public string Cuenta { get; set; }
        public string Contraseña { get; set; }
        public string Administrador { get; set; }  // "sí" o "no", o cualquier otra convención que uses
        public byte[] Imagen { get; set; }

        // Constructor por defecto
        public Usuario() {
            IdUsuario = 9999;
            Nombre = "nombre";
            Cuenta = "cuenta";
            Contraseña = "contraseña";
            Administrador = "administrador";
            Imagen = null;
        }

        // Constructor con parámetros
        public Usuario(int idUsuario, string nombre, string cuenta, string contraseña, string administrador, byte[] imagen,
            string apellido, string direccion, string nit)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Cuenta = cuenta;
            Contraseña = contraseña;
            Administrador = administrador;
            Imagen = imagen;
            Apellido = apellido;
            Direccion = direccion;
            NIT = nit;
        }
    }
}
