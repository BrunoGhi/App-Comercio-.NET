using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BE;
using DAL;

namespace MPP
{
    public class MPPUsuario
    {
        public MPPUsuario() { }

        Datos datos;
        Hashtable parametros;
        public bool iniciarSesion(BEUsuario usuario)
        {
            bool res = false;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("Usuarios.xml");
            var doc = xmlDocument.SelectNodes("Usuarios/Usuario");
            foreach (XmlNode usu in doc)
            {
                string nombre = usu.SelectSingleNode("Nombre").InnerText;
                if(nombre == usuario.Nombre)
                {
                    string contrasenaEncriptada = usu.SelectSingleNode("Contrasena").InnerText;
                    if(contrasenaEncriptada == usuario.Contrasena)
                    {
                        res=true;
                    }
                }
            }
            /*
            datos = new Datos();
            parametros = new Hashtable();
            parametros.Add("@Name",usuario.Nombre);
            parametros.Add("@Pass",usuario.Contrasena);
            if (datos.LeerScalar("S_VerificarUsuario",parametros))
            {
                return true;
            }
            return false;
            */
            return res;
        }
    }
}
