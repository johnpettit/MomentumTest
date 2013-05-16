using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

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
    public class ContactCollection : CollectionBase
    {
        public string errorMessage;

        public ContactCollection()
        {
        }

        public bool getAllContacts()
        {
            return true;
        }

        public bool getAllContactsForCustomer(int pCustomerId)
        {
            return true;
        }

        //****************************************PROPERTIES******************

        public Contact this[int index]
        {
            get { return ((Contact)List[index]); }
            set { List[index] = value; }
        }
    }
}