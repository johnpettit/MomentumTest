using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MomentumTest.lib;

namespace MomentumTest.lib
{
    /**
     * Customer class
     * Represents one Customer with its data,
     * including its ContactCollection
     * 
     * General usage
     * Instantiate a Customer object then
     * call initCustomer with the id of
     * the Customer you want to retrieve
     */
    public class Customer : MBase
    {
        /**
         * Properties
        */
        public int id {get;set;}
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public DateTime lastContact {get; set;}
        public ContactCollection contacts { get; set; }

        /**
         * Constructor
         * just set id 0
         * id 0 indicates an empty Customer
         */
        public Customer()
        {
            id = 0;
        }

        /**
         * method initCustomer
         * fetch from the database the Customer
         * with the supplied id and populate the
         * objects properties
         */
        public bool initCustomer(int customerId)
        {
            SqlServer sqlsvr = new SqlServer();
            DataSet ds;
            string sql;

            try
            {
                sql = "SELECT * FROM Customer WHERE id = " + customerId;
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server Error in Customer.initCustomer:" + sqlsvr.errorMessage;
                    return false;
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    errorMessage = "No record in Customer.initCustomer";
                    return false;
                }

                DataRow row = ds.Tables[0].Rows[0];
                id = customerId;
                firstName = row["FirstName"].ToString();
                lastName = row["LastName"].ToString();
                phoneNumber = row["PhoneNumber"].ToString();
                email = row["Email"].ToString();

                // fetch all Contacts for this Customer
                // sorted by newest first
                ContactCollection cc = new ContactCollection();
                if (! cc.getAllContactsForCustomer(id))
                {
                    errorMessage = "Error getting Contacts in in Customer.initCustomer:" + cc.errorMessage;
                    return false;
                }
                contacts = cc;
                lastContact = cc[0].createDate;  //Last Contact

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in Customer.initCustomer:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method insertCustomer
         * insert a new Customer into the database
         * and return the (newly created) id of that Customer
         * as well as populate its properties
         * returns 0 on error
         * 
         * NOTE Every newly created Customer get a new Contact
         * record with the DateTime of now and a note of "Created"
         */
        public int insertCustomer(string pFirstName, string pLastName, string pPhoneNumber, string pEmail)
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;

            try
            {
                SqlCommand comm = new SqlCommand();

                sql = "INSERT INTO Customer ";
                sql += "(FirstName, LastName, PhoneNumber, Email) ";
                sql += "VALUES ";
                sql += "(@FirstName, @LastName, @PhoneNumber, @Email)";
                sql += ";SELECT @@IDENTITY AS new_id FROM Customer";

                comm.CommandText = sql;

                comm.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar,70));
                comm.Parameters["@FirstName"].Value = pFirstName;

                comm.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 70));
                comm.Parameters["@LastName"].Value = pLastName;

                comm.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 50));
                comm.Parameters["@PhoneNumber"].Value = pPhoneNumber;

                comm.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 50));
                comm.Parameters["@Email"].Value = pEmail;

                DataSet ds = sqlsvr.runCommandReturnDataSet(comm);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in Customer.insertCustomer:" + sqlsvr.errorMessage;
                    return 0;
                }

                id = int.Parse(ds.Tables[0].Rows[0]["new_id"].ToString());
                firstName = pFirstName;
                lastName = pLastName;
                phoneNumber = pPhoneNumber;
                email = pEmail;
                DateTime now = DateTime.Now;

                Contact co = new Contact();
                int res = co.insertContact(id, now, "Created");

                if (res == 0)
                {
                    errorMessage = "Customer.insertCustomer error creating initial Contact:" + co.errorMessage;
                    return 0;
                }

                lastContact = now;

                return id;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in Customer.insertCustomer:" + ex.Message + ex.StackTrace;
                return 0;
            }
        }
    }
}