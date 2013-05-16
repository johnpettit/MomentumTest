using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace MomentumTest.lib
{
    /**
     * SqlServer class
     * Single class to abstract away the database
     * Handles conncetions, conncetion strings, runs queries, etc
     */
    public class SqlServer : MBase
    {
        private string _conn_str;

        private SqlConnection _conn;

        public SqlServer()
        {
            try
            {
                _conn_str = System.Configuration.ConfigurationSettings.AppSettings["sqlconn"];
            }
            catch
            {
            }
        }

        public DataSet runSqlReturnDataSet(string sql)
        {
            if (!openConn())
            {
                return null;
            }

            try
            {
                SqlDataAdapter comm = new SqlDataAdapter(sql, _conn);
                DataSet rs = new DataSet();

                comm.Fill(rs);

                closeConn();

                return rs;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " " + ex.StackTrace;
                closeConn();
                return null;
            }
        }

        private bool openConn()
        {
            try
            {
                _conn = new SqlConnection(_conn_str);
                _conn.Open();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message + " " + ex.StackTrace;
                return false;
            }
        }

        private void closeConn()
        {
            _conn.Close();
            _conn.Dispose();
        }
    }
}