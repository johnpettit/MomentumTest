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
     * WebForm for dealing with Contacts
     * Displays a list of them
     * Allows for creation of them
     * Takes a customerid as an input
     */
    public partial class Contact : System.Web.UI.Page
    {
        /**
         * method Page_Load
         * On load, default the new Contact DateTime to now
         * init the Customer for this page
         * Fill in the Customer name
         * Display the Contact List for this Customer
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            txtAddContactDateTime.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            Customer cu = new Customer();
            if (! cu.initCustomer(int.Parse(Request.QueryString["customerid"].ToString())))
            {
                //error
                lblError.Text = cu.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                lblCustomerName.Text = cu.firstName + " " + cu.lastName;
                Repeater1.DataSource = cu.contacts;
                Repeater1.DataBind();
            }
        }

        /**
         * method btnDoAddContact_Click
         * insert a new Contact when clicked
         * On success redirect to home page
         */
        protected void btnDoAddContact_Click(object sender, EventArgs e)
        {
            MomentumTest.lib.Contact co = new MomentumTest.lib.Contact();
            int res = co.insertContact(int.Parse(Request.QueryString["customerid"].ToString()), DateTime.Parse(txtAddContactDateTime.Text), txtAddContactNotes.Text);
            if (res == 0)
            {
                //error
                lblError.Text = co.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        /**
         * method btnContactCancel_Click
         * go back to home page if no new Contact
         * is to be added
         */
        protected void btnContactCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}