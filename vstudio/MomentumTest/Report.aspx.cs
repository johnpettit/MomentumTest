using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MomentumTest.lib;

namespace MomentumTest
{
    /**
     * WebForm for dealing with the system reporting
     * Displays a list of Customers below based
     * on the report selected
     */
    public partial class Report : System.Web.UI.Page
    {
        /**
         * method Page_Load
         * no data loaded on start
         */
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /**
         * method btnContactCancel_Click
         * gets the list of rare Customers (havent been Contacted 
         * in the last 6 months )
         * and fills the repeater
         */
        protected void btnNoContact_Click(object sender, EventArgs e)
        {
            CustomerCollection cc = new CustomerCollection();
            if (!cc.getRareCustomer())
            {
                //error
                lblError.Text = cc.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                refreshCustomerList(cc);
            }
        }

        /**
         * method btnLastWeek_Click
         * gets the list of Customers contacted within the
         * last week
         * and fills the repeater
         */
        protected void btnLastWeek_Click(object sender, EventArgs e)
        {
            CustomerCollection cc = new CustomerCollection();
            if (!cc.getRecentCustomer())
            {
                //error
                lblError.Text = cc.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                refreshCustomerList(cc);
            }
        }

        /**
         * method btnManyContact_Click
         * gets the list of Customers contacted over
         * 2 times within the last week
         * and fills the repeater
         */
        protected void btnManyContact_Click(object sender, EventArgs e)
        {
            CustomerCollection cc = new CustomerCollection();
            if (!cc.getRecentActiveCustomer())
            {
                //error
                lblError.Text = cc.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                refreshCustomerList(cc);
            }
        }

        /**
         * method refreshCustomerList
         * accepts a CustomerCollection and
         * refills the repeater
         */
        protected void refreshCustomerList(CustomerCollection cc)
        {
            Repeater1.DataSource = cc;
            Repeater1.DataBind();
        }
    }
}