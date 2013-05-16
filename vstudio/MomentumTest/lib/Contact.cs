using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        private int _id;
        private int _customerId;
        private DateTime _createDate;
        private string _note;

        public Contact()
        {
            _id = 0;
        }

        public bool initContact(int contactId)
        {
            return true;
        }

        public int createContact(int pCustomerId, DateTime pCreateDate, string pNote)
        {
            return 1;
        }

        //***************** PROPERTIES **********************/

        public int id
        {
            get { return _id; }
        }

        public int customerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public DateTime createDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public string note
        {
            get { return _note; }
            set { _note = value; }
        }
    }
}