using System;
using System.Data;
using System.Data.SqlClient;

namespace VotUcaWebApi
{
    public class DBConn
    {
        public static SqlConnection ConexionSQL()
        {
            SqlConnection ConectString = new SqlConnection();
            ConectString.ConnectionString = "server = REPLICA; database = VotUcaWebApi; Integrated security = true";
            return ConectString;
        }

        public static DataTable ConsultaSQL(string Query)
        {
            DataTable _Consulta = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn = ConexionSQL();
            SqlCommand cmd = new SqlCommand(Query, conn);
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                else
                {
                    conn.Open();
                }
                _Consulta.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return _Consulta;
        }
    }

}
