using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

namespace MomentumTest.lib
{
    /**
     * ContactCollection class
     * Represents a List of Contacts,
     * generally the List of Contacts 
     * that pertain to one Customer
     * 
     * General usage
     * Instantiate a ContactCollection object
     * then call getAllContactsForCustomer
     * with the Customer id to retrive
     * all Contacts for that Customer.
     * 
     * NOTE: Since the Customer object, upon initCustomer
     * fills its internal ContactCollection with it's
     * Contacts, this object may be rarely needed.
     * 
     * NOTE: All gets clear the internal List
     */
    public class ContactCollection : List<Contact>
    {
        /**
         * Properties
         */
        public string errorMessage { get; set; }

        /**
         * Constructor
         */
        public ContactCollection()
        {
        }

        /**
         * method getAllContacts
         * fetch all Contacts from DB
         */
        public bool getAllContacts()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Contact";

                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in ContactCollection.getAllContacts:" + sqlsvr.errorMessage;
                    return false;
                }

                if (!this.fillInternal(ds.Tables[0].Rows))
                {
                    errorMessage = "Error in ContactCollection.getAllContacts.fillInternal:" + errorMessage;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in ContactCollection.getAllContacts:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method getAllContactsForCustomer
         * fetch all Contacts from DB
         * using the supplied CustomerId
         * Sort by CreateDate (newest first)
         */
        public bool getAllContactsForCustomer(int pCustomerId)
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Contact WHERE CustomerID = " + pCustomerId.ToString() + " ORDER BY CreateDate DESC";

                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in ContactCollection.getAllContactsForCustomer:" + sqlsvr.errorMessage;
                    return false;
                }

                if (!this.fillInternal(ds.Tables[0].Rows))
                {
                    errorMessage = "Error in ContactCollection.getAllContactsForCustomer.fillInternal:" + errorMessage;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in ContactCollection.getAllContactsForCustomer:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method getAllContactsToday
         * get all contacts with a date 
         * matching today and 
         * add them to the list
         */
        public bool getAllContactsToday()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;
            DateTime theNow = DateTime.Now;

            clearInternal();

            try
            {
                sql = "SELECT id FROM Contact WHERE DATEPART(yy,CreateDate) = " + theNow.Year;
                sql += " AND DATEPART(mm,CreateDate) = " + theNow.Month;
                sql += " AND DATEPART(dd,CreateDate) = " + theNow.Day;

                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in ContactCollection.getAllContactsToday:" + sqlsvr.errorMessage;
                    return false;
                }

                if (!this.fillInternal(ds.Tables[0].Rows))
                {
                    errorMessage = "Error in ContactCollection.getAllContactsToday.fillInternal:" + errorMessage;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in ContactCollection.getAllContactsToday:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        /**
         * method fillInternal
         * fills internal List
         * with Rows from SQL query
         * 
         * NOTE: requires the "id" field to of been selected from the DB
         */
        private bool fillInternal(DataRowCollection rows)
        {
            foreach (DataRow row in rows)
            {
                Contact co = new Contact();

                if (! co.initContact(int.Parse(row["id"].ToString())))
                {
                    errorMessage = "Error initing Contact:" + co.errorMessage;
                    return false;
                }

                this.Add(co);
            }
            return true;
        }

        /**
         * method clearInternal
         * clears the internal List of all Contacts
         */
        private void clearInternal()
        {
            this.Clear();
        }
    }
}