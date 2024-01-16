using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace DAL
{
    public class Datos
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        public Datos() { conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaConexion"].ToString()); }

        public DataTable Leer(string query, Hashtable parametros)
        {
            DataTable tabla = new DataTable();
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter Da = new SqlDataAdapter(cmd);
                if (parametros != null)
                {
                    foreach (string key in parametros.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, parametros[key]);
                    }
                }
                Da.Fill(tabla);
            }
            catch (SqlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally
            {
                conn.Close();
            }
            return tabla;
        }
        public bool LeerScalar(string query, Hashtable parametros)
        {
            conn.Open();
            cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if(parametros != null)
                {
                    foreach (string key in parametros.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, parametros[key]);
                    }
                }
                int rta = Convert.ToInt32(cmd.ExecuteScalar());
                if (rta > 0)
                { return true; }
                else
                { return false; }
            }
            catch (SqlException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
        }
        public bool Escribir(string query,Hashtable parametros)
        {
            bool res = false;
            if(conn.State == ConnectionState.Closed) { conn.Open(); }
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            try
            {
                cmd = new SqlCommand(query, conn, sqlTransaction);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parametros != null)
                {
                    foreach (string key in parametros.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, parametros[key]);
                    }
                }
                cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                res = true;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();
                res = false;
                throw ex;
            }
            catch( Exception ex) { res = false; throw ex; }
            finally
            { conn.Close(); }
            return res;
        }
    }
}
