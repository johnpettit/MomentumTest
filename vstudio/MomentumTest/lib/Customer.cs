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
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private string _email;
        private ContactCollection _contacts;

        public Customer()
        {
            _id = 0;
        }

        public bool initCustomer(int theId)
        {
            SqlServer sqlsvr = new SqlServer();
            DataSet ds;
            string sql;

            try
            {
                sql = "SELECT * FROM Customer WHERE id = " + id;

                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server Error in Customer.initCustomer:" + sqlsvr.errorMessage;
                    return false;
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    errorMessage = "No record";
                    return false;
                }

                DataRow row = ds.Tables[0].Rows[0];

                _id = theId;
                _firstName = row["FirstName"].ToString();
                _lastName = row["LastName"].ToString();
                _phoneNumber = row["Phone"].ToString();
                _email = row["Email"].ToString();

                //init contacts

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in Customer.initCustomer:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        public int createCustomer(string pFirstName, string pLastName, string pPhoneNumber, string pEmail)
        {
            return 1;
        }

        //************ PROPERTIES ************************/
        public int id
        {
            get { return _id; }
        }

        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string lastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string fullName
        {
            get { return _firstName + " " + _lastName; }
        }

        public string phoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}