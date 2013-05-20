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
     * WebForm that acts as the primary dashboard
     * Displays Customer list
     * Allows for searches
     * Allows for the creation of new Customers
     * Shows Dashboard statistics
     */
    public partial class _Default : System.Web.UI.Page
    {
        /**
         * method Page_Load
         * get all Customers and
         * fill the repeater
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerCollection cc = new CustomerCollection();
            if (!cc.getAllCustomers())
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
         * method btnDoSearch_Click
         * executes a search against the Customer 
         * list with the supplied parameters
         * The result is then refreshed into the repeater
         * NOTE: The search is an AND, not an OR search
         */
        protected void btnDoSearch_Click(object sender, EventArgs e)
        {
            CustomerCollection cc = new CustomerCollection();
            if (!cc.searchCustomer(txtSearchFirstName.Text, txtSearchLastName.Text, txtSearchPhoneNumber.Text, txtSearchEmail.Text))
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
         * method btnDoCreate_Click
         * inserts a new Customer with
         * the supplied parameters.
         * Little to no validation is being done.
         * NOTE: a new Customer always get a new Contact
         * timestamped with NOW
         * and a note saying "Created"
         */
        protected void btnDoCreate_Click(object sender, EventArgs e)
        {
            Customer cu = new Customer();
            int res = cu.insertCustomer(txtCreateFirstName.Text, txtCreateLastName.Text, txtCreatePhoneNumber.Text, txtCreateEmail.Text);
            if (res == 0)
            {
                //error
                lblError.Text = cu.errorMessage;
                lblError.Visible = true;
            }
            else
            {            
                CustomerCollection cc = new CustomerCollection();
                if (!cc.getAllCustomers())
                {
                    //error
                    lblError.Text = cc.errorMessage;
                    lblError.Visible = true;
                }
                else
                {
                    //clear fields after success
                    txtCreateFirstName.Text = "";
                    txtCreateLastName.Text = "";
                    txtCreatePhoneNumber.Text = "";
                    txtCreateEmail.Text = "";
                    txtSearchFirstName.Text = "";
                    txtSearchLastName.Text = "";
                    txtSearchPhoneNumber.Text = "";
                    txtSearchEmail.Text = "";
                    refreshCustomerList(cc);
                }
            }
        }

        /**
         * method refreshCustomerList
         * accepts a CustomerCollection and
         * fill the repeater with it.
         * Also refresh the Dashboard stats
         * NOTE: Not very efficient
         */
        protected void refreshCustomerList(CustomerCollection cc)
        {
            Repeater1.DataSource = cc;
            Repeater1.DataBind();

            //fill Dashboard values
            CustomerCollection cc2 = new CustomerCollection();

            //Odd
            if (!cc2.getCustomersOdd())
            {
                //error
                lblError.Text = cc.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                lblDBOdd.Text = cc2.Count.ToString();
            }

            //Even
            if (!cc2.getCustomerEven())
            {
                //error
                lblError.Text = cc.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                lblDBEven.Text = cc2.Count.ToString();
            }

            //Family
            int famResult = cc2.getCustomerFamily();
            if (famResult == -1)
            {
                //error
                lblError.Text = cc.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                lblDBFamily.Text = famResult.ToString();
            }

            //Contacts
            ContactCollection concol = new ContactCollection();
            if (! concol.getAllContactsToday())
            {
                //error
                lblError.Text = concol.errorMessage;
                lblError.Visible = true;
            }
            else
            {
                lblDBToday.Text = concol.Count.ToString();
            }
        }
    }
}
