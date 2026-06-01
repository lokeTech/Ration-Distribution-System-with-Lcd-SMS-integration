using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiometricRationDistribution
{
    public partial class sms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Application["sms"] != null)
            {
                Response.Write(Application["sms"].ToString());
                Application["sms"] = null;
            }
            else
            {
                Response.Write("0");
            }
        }
    }
}