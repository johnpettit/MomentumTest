using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MomentumTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MomentumTest.lib.CustomerCollection cc = new lib.CustomerCollection();

            if (!cc.getAllCustomers())
            {
                Response.Write(cc.errorMessage);
            }

            Repeater1.DataSource = cc;
            Repeater1.DataBind();

        }
    }
}
