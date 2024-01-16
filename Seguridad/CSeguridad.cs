using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;

namespace Seguridad
{
    public class CSeguridad
    {
        public CSeguridad() { }

        private string encriptarMD5(string pCadena)
        {
            try
            {
                //Crear un objeto de codificación para garantizar el estándar de codificación para el texto fuente
                UnicodeEncoding UeCodigo = new UnicodeEncoding();
                //Recuperar una matriz de bytes basado en el texto de origen
                byte[] ByteSourceText = UeCodigo.GetBytes(pCadena);
                //Instancias de un objeto MD5 Proveedor
                MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
                //Calcular el valor hash MD5 de la fuente
                byte[] ByteHash = Md5.ComputeHash(ByteSourceText);
                //Y es convertir a formato de cadena para el retorno
                return Convert.ToBase64String(ByteHash);

            }
            catch (CryptographicException ex) { throw (ex); }
            catch (Exception ex) { throw (ex); }
        }
        public bool verificar(BEUsuario usuario)
        {
            MPPUsuario log = new MPPUsuario();
            usuario.Contrasena = encriptarMD5(usuario.Contrasena);
            return log.iniciarSesion(usuario);
        }

    }
}
