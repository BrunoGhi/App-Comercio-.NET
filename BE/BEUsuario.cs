using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEUsuario
    {
        public BEUsuario() { }

        public BEUsuario(string pNombre, string pContrasena) { Nombre = pNombre; Contrasena = pContrasena; }
        public string Nombre { get; set; }

        public string Contrasena { get; set; }
    }
}
