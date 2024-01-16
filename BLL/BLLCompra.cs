using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;
using BE;
using BLL;
using MPP;

namespace BLL
{
    public class BLLCompra : IGestor1<BECompra>
    {
        MPPCompra mCompra;
        public BLLCompra() { }

        public bool Baja(BECompra objeto)
        {
            throw new NotImplementedException();
        }

        #region Metodos
        public bool Guardar(BECompra pCompra)
        {
            try
            {
                mCompra = new MPPCompra();
                return mCompra.Guardar(pCompra);
            }
            catch (Exception ex) { throw ex; }
        }
        public List<BECompra> ListarTodo()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
