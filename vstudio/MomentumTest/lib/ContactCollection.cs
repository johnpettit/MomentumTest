using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace MomentumTest.lib
{
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