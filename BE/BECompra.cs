using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECompra : Entidad
    {
        public BECompra() { }

        #region Atributos
        public float Monto { get; set; }
        public BEGarantia garantia { get; set; }

        public BECliente cliente { get; set; }
        public List<BEProducto> productos { get; set; }

        #endregion
    }
}
