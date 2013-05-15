using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomentumTest.lib
{
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