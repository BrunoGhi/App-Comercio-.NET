using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace MPP
{
    public class MPPCliente
    {
        Datos datos;
        Hashtable parametros;
        public MPPCliente() { }
        public List<BECliente> listarTodo()
        {
            try
            {
                datos = new Datos();
                List<BECliente> lista = new List<BECliente>();
                DataTable tabla = datos.Leer("S_Clientes_ListarTodo", null);
                foreach (DataRow fila in tabla.Rows)
                {
                    if (fila["precioExc"].ToString() == "False")
                    {
                        BEEstandar std = new BEEstandar();
                        std.DNI = fila["DNI"].ToString();
                        std.Nombre = fila["Nombre"].ToString();
                        std.Apellido = fila["Apellido"].ToString();
                        lista.Add(std);
                    }
                    else
                    {
                        BEPremium prem = new BEPremium();
                        prem.DNI = fila["DNI"].ToString();
                        prem.Nombre = fila["Nombre"].ToString();
                        prem.Apellido = fila["Apellido"].ToString();
                        lista.Add(prem);
                    }
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
        } 

        public bool guardar(BECliente pCliente)
        {
            try
            {
                string consulta = "";
                string s = pCliente is BEPremium ? "1" : "0";
                parametros = new Hashtable();
                parametros.Add("@Dni",pCliente.DNI);
                parametros.Add("@Nombre",pCliente.Nombre);
                parametros.Add("@Apellido",pCliente.Apellido);
                parametros.Add("@Exc",s);
                if (existeDNI(pCliente) == true)
                {
                    consulta = $"S_Clientes_Actualizar";
                }
                else
                {
                    consulta = $"S_Clientes_Crear";
                }
                datos = new Datos();
                return datos.Escribir(consulta,parametros);
            }
            catch (Exception ex) { throw ex; }
        }

        public bool eliminar(BECliente pCliente)
        {
            try
            {
                if (clienteAsociado(pCliente) == false)
                {
                    datos = new Datos();
                    parametros = new Hashtable();
                    parametros.Add("@Dni", pCliente.DNI);
                    return datos.Escribir($"S_Clientes_Baja", parametros);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private bool existeDNI(BECliente pCliente)
        {
            try
            {
                datos = new Datos();
                Hashtable parametros2 = new Hashtable();
                parametros2.Add("@Dni",pCliente.DNI);
                return datos.LeerScalar($"S_Clientes_Existe",parametros2);
            }
            catch (Exception ex) { throw ex; }
            
        }
        private bool clienteAsociado(BECliente pCliente)
        {
            try
            {
                datos = new Datos();
                parametros = new Hashtable();
                parametros.Add("@Dni", pCliente.DNI);
                return datos.LeerScalar($"S_Clientes_ClienteAsociado", parametros);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
