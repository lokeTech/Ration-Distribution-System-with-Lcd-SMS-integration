using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace BiometricRationDistribution
{
    public partial class putdata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["q"].Contains("done"))
            {
                Application["sms"] = "9665302573#Dear "+ Application["uname"] + "\r\nYour grain transaction of " + Application["wt"] + " kg has been completed.";
                Application["sms"] += "@"+ Application["mobile"] + "#Dear " + Application["uname"] + "\r\nYour grain transaction of " + Application["wt"] + " kg has been completed.";
                Application["wt"] = "0";
                
            }
        }
        

    }
}