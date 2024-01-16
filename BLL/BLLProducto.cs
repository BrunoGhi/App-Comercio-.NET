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
    public class BLLProducto : IGestor1<BEProducto>
    {
        public BLLProducto() { mProducto = new MPPProducto(); }

        MPPProducto mProducto;

        #region Metodos
        public bool Guardar(BEProducto pProducto)
        {
            try
            {
                return mProducto.Guardar(pProducto);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Baja(BEProducto pProducto)
        {
            try
            {
                return mProducto.Baja(pProducto);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<BEProducto> ListarTodo()
        {
            try
            {
                List<BEProducto> lista = new List<BEProducto>();
                lista = mProducto.ListarTodo();
                return lista;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
    }
}
