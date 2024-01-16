using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProducto:Entidad
    {
        public BEProducto() { }
        public BEProducto(int pId, int pPrecio, string pDescripcion,int pCantidad) { Codigo = pId; Precio = pPrecio; Descripcion = pDescripcion; Cantidad = pCantidad; }

        #region atributos
        public int Precio { get; set; }

        public string Descripcion { get; set; }

        public int Cantidad { get; set; }
        #endregion
    }
}
