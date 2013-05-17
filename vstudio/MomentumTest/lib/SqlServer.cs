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

        /**
         * Constructor
         * Sets the conn string
         */
        public SqlServer()
        {
            try
            {
                //_conn_str = System.Configuration.ConfigurationSettings.AppSettings["sqlconn"];
                _conn_str = "Server=192.168.2.13;Database=MomentumTest;User Id=sa;Password=chuck111;";
            }
            catch
            {
            }
        }

        /**
         * method runCommandReturnDataSet
         * Takes a SqlCommand and runs it
         */
        public DataSet runCommandReturnDataSet(SqlCommand command)
        {
            if (!openConn())
            {
                return null;
            }

            try
            {
                SqlDataAdapter comm = new SqlDataAdapter();
                command.Connection = _conn;
                comm.SelectCommand = command;
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

        /**
         * method runSqlReturnDataSet
         * takes a string of sql and throws it
         * at the database
         */
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

        /**
         * method openConn
         * opens a connection to the database
         */
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

        /**
         * method closeConn
         * closes a conncetion to the database
         */
        private void closeConn()
        {
            _conn.Close();
            _conn.Dispose();
        }
    }
}