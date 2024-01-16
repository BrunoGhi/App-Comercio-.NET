using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using Abstraccion;
using System.Collections;

namespace MPP
{
    public class MPPCompra : IGestor1<BECompra>
    {
        Datos datos;
        Hashtable parametros;
        public MPPCompra() { }

        public List<BECompra> retornarListaComprasCliente(BECliente pCliente)
        {
            try
            {
                datos = new Datos();
                parametros = new Hashtable();
                List<BECompra> lista = new List<BECompra>();
                parametros.Add("@Dni",pCliente.DNI);
                DataTable tabla = datos.Leer($"S_Compras_ListarXCliente", parametros);
                foreach (DataRow r in tabla.Rows)
                {
                    BECompra compra = new BECompra();
                    compra.Codigo = int.Parse(r[0].ToString());
                    compra.Monto = int.Parse(r[1].ToString());
                    if (r[2] is DBNull) { compra.garantia = null; }
                    else
                    {
                        Hashtable ht2 = new Hashtable();
                        ht2.Add("@Id", r[2]);
                        DataTable garantia = datos.Leer($"S_Garantia", ht2);
                        compra.garantia = new BEGarantia() { duracionDias = int.Parse(garantia.Rows[0][0].ToString()) };
                    }
                    Hashtable param2 = new Hashtable();
                    param2.Add("@Codigo", compra.Codigo);
                    DataTable prods = datos.Leer($"S_Productos_ListarXCodigo", param2);
                    List<BEProducto> productos = new List<BEProducto>();
                    foreach (DataRow p in prods.Rows)
                    {
                        BEProducto pr = new BEProducto();
                        pr.Precio = int.Parse(p[0].ToString());
                        pr.Descripcion = p[1].ToString();
                        pr.Cantidad = int.Parse(p[2].ToString());
                        productos.Add(pr);
                    }
                    compra.productos = productos;
                    lista.Add(compra);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
        }
        public bool Guardar(BECompra pCompra)
        {
            try
            {
                string consulta = "";
                datos = new Datos();
                parametros = new Hashtable();
                if (pCompra.cliente is BEPremium)
                {
                    consulta = $"S_Compras_CrearPremium";
                    parametros.Add("@Monto", pCompra.Monto);
                    parametros.Add("@IdGar", pCompra.garantia.Codigo);
                    parametros.Add("@Dni", pCompra.cliente.DNI);
                    datos.Escribir(consulta, parametros);
                    int id = int.Parse(datos.Leer($"S_Compras_RetornarUltima",null).Rows[0]["id"].ToString());
                    foreach (BEProducto p in pCompra.productos)
                    {
                        consulta = $"S_ComprasProducto_CrearPremium";
                        parametros = new Hashtable();
                        parametros.Add("@CodPro",p.Codigo);
                        parametros.Add("@CantPro",p.Cantidad);
                        parametros.Add("@Id",id);
                        datos.Escribir(consulta, parametros);
                    }
                }
                else
                {
                    consulta = $"S_Compras_CrearEstandar";
                    parametros.Add("@Monto",pCompra.Monto);
                    parametros.Add("@Dni",pCompra.cliente.DNI);
                    datos.Escribir(consulta, parametros);
                    int id = int.Parse(datos.Leer($"S_Compras_RetornarUltima", null).Rows[0]["id"].ToString());
                    foreach (BEProducto p in pCompra.productos)
                    {
                        parametros = new Hashtable();
                        parametros.Add("@Id",id);
                        parametros.Add("@Codigo",p.Codigo);
                        parametros.Add("@Cant", p.Cantidad);
                        consulta = "S_ComprasProducto_CrearEstandar";
                        datos.Escribir(consulta, parametros);
                    }
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }



        // --------------------------------
        public bool Baja(BECompra objeto)
        {
            throw new NotImplementedException();
        }

        public List<BECompra> ListarTodo()
        {
            throw new NotImplementedException();
        }
    }
}
