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
     * Represents a Collection of Contacts
     * generally the collection of Contacts 
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
     * Contacts, so this object is rarely needed.
     */
    public class ContactCollection : List<Contact>
    {
        /**
         * Properties
         */
        public string errorMessage { get; set; }

        /**
         * Constructor
         * Not much to do
         */
        public ContactCollection()
        {
        }

        public bool getAllContacts()
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;
            Contact co;

            try
            {
                sql = "SELECT id FROM Contact";

                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in ContactCollection.getAllContacts:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    co = new Contact();

                    if (!co.initContact(int.Parse(row["id"].ToString())))
                    {
                        errorMessage = "Error initing Contact:" + co.errorMessage;
                        return false;
                    }

                    this.Add(co);
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in ContactCollection.getAllContacts:" + ex.Message + ex.StackTrace;
                return false;
            }
        }

        public bool getAllContactsForCustomer(int pCustomerId)
        {
            SqlServer sqlsvr = new SqlServer();
            string sql;
            DataSet ds;
            Contact co;

            try
            {
                sql = "SELECT id FROM Contact WHERE CustomerID = " + pCustomerId;

                ds = sqlsvr.runSqlReturnDataSet(sql);

                if (ds == null)
                {
                    errorMessage = "Sql Server error in ContactCollection.getAllContacts:" + sqlsvr.errorMessage;
                    return false;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    co = new Contact();

                    if (!co.initContact(int.Parse(row["id"].ToString())))
                    {
                        errorMessage = "Error initing Contact:" + co.errorMessage;
                        return false;
                    }

                    this.Add(co);
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Exception in ContactCollection.getAllContacts:" + ex.Message + ex.StackTrace;
                return false;
            }
        }
    }
}