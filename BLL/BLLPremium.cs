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
    public class BLLPremium : BLLCliente
    {
        public BLLPremium() { }

        public override float CalcularPrecio(BECompra pCompra)
        {
            try
            {
                float rta = 0;
                foreach (BEProducto p in pCompra.productos)
                {
                    rta += p.Precio*p.Cantidad;
                }
                return rta;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}