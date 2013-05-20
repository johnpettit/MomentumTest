using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MomentumTest.lib
{
    /**
     * Contact class
     * Represents one Contact, with all of its data
     * Also used to create a Contact
     * 
     * General usage is to instantiate a Contact object
     * then call initContact with the id of the Contact 
     * you want to fill it with.
     */
    public class Contact : MBase
    {
        /**
         * Properties
         */
        public int id { get; set; }
        public int customerId { get; set; }
        public DateTime createDate { get; set; }
        public string note { get; set; }

        /**
         * Constructor
         * Dont do much
         * id == 0 means an empty Contact
         */
        public Contact()
        {
            id = 0;
        }

        /**
         * Method initContact
         * Takes a Contact ID, fetches it from the DB
         * and poplates the object with the data from the DB
         */
        public bool initContact(int contactId)
        {
            SqlServer sqlsvr = new SqlServer();
            DataSet ds;
            string sql;

            try
            {
                sql = "SELECT * FROM Contact WHERE id = " + contactId;
                ds = sqlsvr.runSqlReturnDataSet(sql);
                if (ds == null)
                {
                    errorMessage = "Sql Server Error in Contact.initContact:" + sqlsvr.errorMessage;
                    return false;
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    errorMessage = "No record in Contact.initContact";
                    return false;
                }

                DataRow row = ds.Tables[0].Rows[0];

                id = contactId; ;
                customerId = int.Parse(row["CustomerId"].ToString());
                createDate = DateTime.Parse(row["CreateDate"].ToString());
                note = row["Note"].ToString();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in Contact.initContact:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method insertContact
         * inserts a new Contact into the DB and
         * returns it's (newly created) id
         * as well as populates its fields
         * Returns 0 on error
         */
        public int insertContact(int pCustomerId, DateTime pCreateDate, string pNote)
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;

            try
            {
                SqlCommand comm = new SqlCommand();

                sql = "INSERT INTO Contact ";
                sql += "(CustomerId, CreateDate, Note) ";
                sql += "VALUES ";
                sql += "(@CustomerId, @CreateDate, @Note)";
                sql += ";SELECT @@IDENTITY AS new_id FROM Contact";

                comm.CommandText = sql;

                comm.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int));
                comm.Parameters["@CustomerId"].Value = pCustomerId;

                comm.Parameters.Add(new SqlParameter("@CreateDate", SqlDbType.DateTime));
                comm.Parameters["@CreateDate"].Value = pCreateDate;

                comm.Parameters.Add(new SqlParameter("@Note", SqlDbType.VarChar,240));
                comm.Parameters["@Note"].Value = pNote;

                DataSet ds = sqlsvr.runCommandReturnDataSet(comm);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in Contact.insertContact:" + sqlsvr.errorMessage;
                    return 0;
                }

                id = int.Parse(ds.Tables[0].Rows[0]["new_id"].ToString());
                customerId = pCustomerId;
                createDate = pCreateDate;
                note = pNote;

                return id;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in Contact.insertContact:" + ex.Message + ex.StackTrace;
                return 0;
            }
        }
    }
}