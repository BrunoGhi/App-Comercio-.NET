using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using System.Collections;
using Abstraccion;

namespace MPP
{
    public class MPPProducto : IGestor1<BEProducto>
    {
        public MPPProducto() { }

        Datos datos;
        Hashtable parametros;
        public List<BEProducto> ListarTodo()
        {
            try
            {
                datos = new Datos();
                List<BEProducto> lista = new List<BEProducto>();
                DataTable tabla = datos.Leer("S_Productos_ListarTodo",null);
                foreach (DataRow fila in tabla.Rows)
                {
                    BEProducto bEProducto = new BEProducto(int.Parse(fila["codigo"].ToString()), int.Parse(fila["precio"].ToString()), fila["descripcion"].ToString(), int.Parse(fila["cantidad"].ToString()));
                    lista.Add(bEProducto);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
        }


        /*
        public BEProducto retornarProducto(int pCodigo)
        {
            try
            {
                datos = new Datos();
                parametros = new Hashtable();
                parametros.Add("@Codigo",pCodigo);
                DataTable tabla = datos.Leer($"S_Productos_RetornarProducto", parametros);
                BEProducto producto = new BEProducto(int.Parse(tabla.Rows[0]["codigo"].ToString()), int.Parse(tabla.Rows[0]["precio"].ToString()), tabla.Rows[0]["descripcion"].ToString(), int.Parse(tabla.Rows[0]["cantidad"].ToString()));
                return producto;
            }
            catch (Exception ex) { throw ex; }
        }
        */

        public bool Guardar(BEProducto pProducto)
        {
            try
            {
                parametros = new Hashtable();
                string consulta = "";
                if (pProducto.Codigo != 0) { consulta = "S_Productos_Actualizar"; parametros.Add("@Codigo", pProducto.Codigo); }
                else { consulta = $"S_Productos_Crear"; }
                datos = new Datos();
                parametros.Add("@Precio", pProducto.Precio);
                parametros.Add("@Descrip", pProducto.Descripcion);
                parametros.Add("@Cant", pProducto.Cantidad);
                return datos.Escribir(consulta, parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Baja(BEProducto pProducto)
        {
            try
            {
                if (productoAsociado(pProducto) == false)
                {
                    string consulta = $"S_Productos_Baja";
                    datos = new Datos();
                    parametros = new Hashtable();
                    parametros.Add("@Codigo",pProducto.Codigo);
                    return datos.Escribir(consulta,parametros);
                }
                else { return false; }
            }
            catch (Exception ex) { throw ex; }
        }

        public bool productoAsociado(BEProducto pProducto)
        {
            try
            {
                datos = new Datos();
                parametros = new Hashtable();
                string consulta = $"S_Productos_Asociado";
                parametros.Add("@Codigo",pProducto.Codigo);
                return datos.LeerScalar(consulta,parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        
    }
}
