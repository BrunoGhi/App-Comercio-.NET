using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL;
using MPP;

namespace BLL
{
    public class BLLEstandar : BLLCliente
    {
        public BLLEstandar() { }

        public override float CalcularPrecio(BECompra pCompra)
        {
            try
            {
                float rta = 0;
                foreach (BEProducto p in pCompra.productos)
                {
                    rta += (p.Precio) * (float)(1.1) * p.Cantidad;
                }
                return rta;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
