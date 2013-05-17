using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

namespace MomentumTest.lib
{
    /**
     * CustomerCollection class
     * Represents a Collection of Customers
     * Also has the various slices of Customers as
     * well as the search methods
     * 
     * General usage
     * Instantiate a CustomerCollection object
     * then call getAllCustomers
     * to retrive the Customer list
     */
    public class CustomerCollection : List<Customer>
    {
        /**
         * Properties
         */
        public string errorMessage { get; set; }

        /**
         * Constructor
         * not much to do
         */
        public CustomerCollection()
        {
        }

        /**
         * method getAllCustomers
         * get all Customers in database
         * and add them to the List
         */
        public bool getAllCustomers()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;
            Customer cu;

            try
            {
                sql = "SELECT id FROM Customer";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getAllCustomers:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cu = new Customer();
                    if (!cu.initCustomer(int.Parse(row["id"].ToString())))
                    {
                        errorMessage = "Error initing Customer:" + cu.errorMessage;
                        return false;
                    }
                    this.Add(cu);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getAllCustomers:" + ex.Message + ex.StackTrace;
                return false;
            }
        }
    }
}