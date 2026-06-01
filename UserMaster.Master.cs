using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiometricRationDistribution
{
    public partial class UserMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["uname"] != null)
            {
                Label1.Text = "Welcome " + Application["uname"]+" "+ Application["mobile"];
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}