using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiometricRationDistribution
{
    public partial class getdata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "application/json";

            if (Application["wt"] != null)
            {
                //Response.Write("{\"quota\":" + Application["wt"] + "}");
                Response.Write("{\"name\":\""+ Application["uname"] + "\",\"quota\":" + Application["wt"] + "}");

            }
            else
            {
                //Response.Write("{\"quota\":"+0+"}");
                Response.Write("{\"name\":\"No\",\"quota\":" + 0 + "}");
            }
        }
    }
}