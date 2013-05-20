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
     * 
     * NOTE: gets clear the List setting to what was requested
     */
    public class CustomerCollection : List<Customer>
    {
        /**
         * Properties
         */
        public string errorMessage { get; set; }

        /**
         * Constructor
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

            clearInternal();

            try
            {
                sql = "SELECT id FROM Customer ORDER BY FirstName";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getAllCustomers:" + sqlsvr.errorMessage;
                    return false;
                }

                if(! this.fillInternal(ds.Tables[0].Rows))
                {
                    errorMessage = "Exception in CustomerCollection.getAllCustomers.fillInternal";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getAllCustomers:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method searchCustomer
         * searches the Customers in the 
         * DB with the specified criteria.
         * Does an AND search as opposed to an OR search
         */
        public bool searchCustomer(string pFirstName, string pLastName, string pPhoneNumber, string pEmail)
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Customer ";
                
                string searchSQL = "";

                if (pFirstName != "")
                    searchSQL += "FirstName = '" + pFirstName + "' ";
                if(pLastName != "")
                {
                    if (searchSQL == "")
                        searchSQL += "LastName = '" + pLastName + "' ";
                    else
                        searchSQL += " AND LastName = '" + pLastName + "' ";
                }
                if (pPhoneNumber != "")
                {
                    if (searchSQL == "")
                        searchSQL += "PhoneNumber = '" + pPhoneNumber + "' ";
                    else
                        searchSQL += " AND PhoneNumber = '" + pPhoneNumber + "' ";
                }
                if (pEmail != "")
                {
                    if (searchSQL == "")
                        searchSQL += "Email = '" + pEmail + "' ";
                    else
                        searchSQL += " AND Email = '" + pEmail + "' ";
                }

                if(searchSQL != "")
                    sql += " WHERE + " + searchSQL;

                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.searchCustomer:" + sqlsvr.errorMessage;
                    return false;
                }

                if (!this.fillInternal(ds.Tables[0].Rows))
                {
                    errorMessage = "Exception in CustomerCollection.searchCustomer.fillInternal";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.searchCustomer:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method getCustomerOdd
         * get all Customers in database
         * with an odd number of
         * characters in their first name
         * and add them to the List
         */
        public bool getCustomersOdd()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id, LEN(FirstName) as length FROM Customer";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getCustomersOdd:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Customer cu = new Customer();

                    //is length odd?
                    int remainder = (int.Parse(row["length"].ToString())) % 2;
                    if (remainder > 0)
                    {
                        if (!cu.initCustomer(int.Parse(row["id"].ToString())))
                        {
                            errorMessage = "Error initing Customer in CustomerCollection.getCustomersOdd:" + cu.errorMessage;
                            return false;
                        }
                        this.Add(cu);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getCustomersOdd:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method getCustomerEven
         * get all customer with an
         * even number of characters in the
         * last name and
         * add them to the list
         */
        public bool getCustomerEven()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id, LEN(LastName) as length FROM Customer";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getCustomerEven:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Customer cu = new Customer();

                    //is length even?
                    int remainder = (int.Parse(row["length"].ToString())) % 2;
                    if (remainder == 0)
                    {
                        if (!cu.initCustomer(int.Parse(row["id"].ToString())))
                        {
                            errorMessage = "Error initing Customer in CustomerCollection.getCustomerEven:" + cu.errorMessage;
                            return false;
                        }
                        this.Add(cu);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getCustomerEven:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method getCustomerFamily
         * counts DISTINCT last names
         * in DB and return the count
         * 
         * NOTE: DOES NOT fill internal List
         */
        public int getCustomerFamily()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;

            try
            {
                sql = "SELECT COUNT( DISTINCT LastName) FROM Customer";
                int result = sqlsvr.runSqlReturnInt(sql);

                if (result == -1)
                {
                    errorMessage = "Exception in CustomerCollection.getCustomerFamily:" + sqlsvr.errorMessage;
                    return -1;
                }

                return result;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getCustomerFamily:" + ex.Message + ex.StackTrace;
                return -1;
            }
        }

        /** 
         * method getTodayCustomerContacts
         * get all customers with a contact
         * today and add
         * them to the list
         */
        public bool getTodayCustomerContacts()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Customer";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getTodayCustomerContacts:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Customer cu = new Customer();

                    if (!cu.initCustomer(int.Parse(row["id"].ToString())))
                    {
                        errorMessage = "Error initing Customer in CustomerCollection.getTodayCustomerContacts:" + cu.errorMessage;
                        return false;
                    }

                    //only add customers with a contact date matching today
                    if(cu.lastContact.Date == DateTime.Now.Date)
                        this.Add(cu);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getTodayCustomerContacts:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /** 
        * method getRareCustomer
        * get all customers with a contact
        * date older than 6 months (rare) and add
        * them to the list
        */
        public bool getRareCustomer()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Customer";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getRareCustomer:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Customer cu = new Customer();

                    if (!cu.initCustomer(int.Parse(row["id"].ToString())))
                    {
                        errorMessage = "Error initing Customer in CustomerCollection.getRareCustomer:" + cu.errorMessage;
                        return false;
                    }

                    //only add customers with a last contact date greater than 6 months ago
                    if (cu.lastContact.Date < DateTime.Now.AddMonths(-6).Date)
                        this.Add(cu);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getRareCustomer:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method getRecentCustomer
         * fetch customer who have been 
         * contacted recently (in the last week)
         * and add them 
         * to the list
         */
        public bool getRecentCustomer()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Customer";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getRecentCustomer:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Customer cu = new Customer();

                    if (!cu.initCustomer(int.Parse(row["id"].ToString())))
                    {
                        errorMessage = "Error initing Customer in CustomerCollection.getRecentCustomer:" + cu.errorMessage;
                        return false;
                    }

                    //only add customers with a last contact date within the last week (7 days)
                    if (cu.lastContact.Date >= DateTime.Now.AddDays(-7).Date)
                        this.Add(cu);
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getRecentCustomer:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method getRecentActiveCustomer
         * fetch customer who have been 
         * contacted recently (in the last week)
         * and are active (more than twice in the last week)
         * and add them to the list
         */
        public bool getRecentActiveCustomer()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Customer";
                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in CustomerCollection.getRecentActiveCustomer:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Customer cu = new Customer();

                    if (!cu.initCustomer(int.Parse(row["id"].ToString())))
                    {
                        errorMessage = "Error initing Customer in CustomerCollection.getRecentActiveCustomer:" + cu.errorMessage;
                        return false;
                    }

                    //only start with customers with a last contact date within the last week (7 days)
                    if (cu.lastContact.Date >= DateTime.Now.AddDays(-7).Date)
                    {
                        int numContacts = 0;
                        foreach (Contact con in cu.contacts)
                        {
                            if (con.createDate.Date >= DateTime.Now.AddDays(-7).Date)
                                numContacts++;
                        }
                        //if theyve been contacted more then 2 times, add them (active)
                        if (numContacts > 2)
                            this.Add(cu);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in CustomerCollection.getRecentActiveCustomer:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method fillInternal
         * fills the internal data structure
         * with data fetched frtom the database
         * usable for all of the fetch methods
         * 
         * NOTE: requires the "id" field to of been selected from the DB
         */
        private bool fillInternal(DataRowCollection rows)
        {
            foreach (DataRow row in rows)
            {
                Customer cu = new Customer();
                if (! cu.initCustomer(int.Parse(row["id"].ToString())))
                {
                    errorMessage = "Error initing Customer in CustomerCollection.fillInternal:" + cu.errorMessage;
                    return false;
                }
                this.Add(cu);
            }
            return true;
        }

        /**
         * method clearInternal
         * clears the internal List of all Customers
         */
        private void clearInternal()
        {
            this.Clear();
        }
    }
}