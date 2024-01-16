using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;
using BE;
using MPP;

namespace BLL
{
    public class BLLCliente
    {
        public BLLCliente() { }

        MPPCliente mCliente = new MPPCliente();
        MPPCompra mCompra = new MPPCompra();

        public List<BECliente> listarTodo()
        {
            try
            {
                List<BECliente> lista = new List<BECliente>();
                lista = mCliente.listarTodo();
                return mCliente.listarTodo();
            }
            catch (Exception ex) { throw ex; }
        }
        public bool guardar(BECliente pCliente)
        {
            try
            {
                return mCliente.guardar(pCliente);
            }
            catch (Exception ex) { throw ex; }
        }
        public bool eliminar(BECliente pCliente)
        {
            try
            {
                return mCliente.eliminar(pCliente);
            }
            catch (Exception ex) { throw ex; }  
        }

        public List<BECompra> listaCompras(BECliente pCliente)
        {
            try
            {
                return mCompra.retornarListaComprasCliente(pCliente);
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual float CalcularPrecio(BECompra pCompra)
        {
            try
            {
                return 0;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
