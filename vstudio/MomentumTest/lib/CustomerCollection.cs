using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace MomentumTest.lib
{
    public class CustomerCollection : CollectionBase
    {
        public string errorMessage;

        public CustomerCollection()
        {
        }

        public bool getAllCustomers()
        {
            return true;
        }

        //****************************************PROPERTIES******************

        public Customer this[int index]
        {
            get { return ((Customer)List[index]); }
            set { List[index] = value; }
        }
    }
}