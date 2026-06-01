using Sysmgr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
namespace BiometricRationDistribution
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Simple demo validation (replace with DB check)
            if (username == "admin" && password == "123")
            {
                Session["uname"] = "admin";
                Response.Redirect("AHome.aspx");
            }
            else
            {
                try
                {
                    if (Validity.Check())
                    {
                        // Adjust path: put Users.xml in App_Data for security
                        string xmlPath = Server.MapPath("~/App_Data/Users.xml");
                        if (!File.Exists(xmlPath))
                        {
                            lblMessage.Text = "User database not found.";
                            return;
                        }

                        XDocument doc = XDocument.Load(xmlPath);

                        // Find a user with matching ration_card_no (case-insensitive) and password
                        var user = doc.Descendants("User")
                                      .Where(u =>
                                          (string)u.Element("ration_card_no") != null &&
                                          string.Equals(((string)u.Element("ration_card_no")).Trim(), username, StringComparison.OrdinalIgnoreCase) &&
                                          (string)u.Element("password") != null &&
                                          ((string)u.Element("password")).Trim() == password
                                      )
                                      .Select(u => new
                                      {
                                          UserId = (string)u.Element("user_id"),
                                          Name = (string)u.Element("name"),
                                          RationCard = (string)u.Element("ration_card_no"),
                                          mobile = (string)u.Element("phone_no")
                                      })
                                      .FirstOrDefault();

                        if (user != null)
                        {
                            // Set session and redirect to ration page
                            Session["uid"] = user.UserId;
                            Application["uname"] = user.Name;
                            Application["mobile"] = user.mobile;
                            Session["ration_card_no"] = user.RationCard;
                            Response.Redirect("UHome.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid ration card number or password.";
                        }
                    }
                    else 
                    {
                        Validity.Error(Server.MapPath("~/App_Data"));
                    }
                }
                catch (Exception ex)
                {
                    // Log exception in real app
                    lblMessage.Text = "An error occurred. Please try again later.";
                }
                //Response.Write("<script>alert('Invalid Username or Password');</script>");
            }
        }
    }
}