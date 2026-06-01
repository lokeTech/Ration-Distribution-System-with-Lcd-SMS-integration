using Sysmgr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace BiometricRationDistribution
{
    public partial class AHome : System.Web.UI.Page
    {
        string filePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            filePath = Server.MapPath("~/App_Data/users.xml");

            if (!IsPostBack)
                LoadUsers();
        }

        private void LoadUsers()
        {
            if (Validity.Check())
            {
                XDocument doc = XDocument.Load(filePath);
                DataTable dt = new DataTable();

                dt.Columns.Add("user_id");
                dt.Columns.Add("ration_card_no");
                dt.Columns.Add("name");
                dt.Columns.Add("phone_no");
                dt.Columns.Add("family_size");

                foreach (var u in doc.Descendants("User"))
                {
                    dt.Rows.Add(
                        u.Element("user_id")?.Value,
                        u.Element("ration_card_no")?.Value,
                        u.Element("name")?.Value,
                        u.Element("phone_no")?.Value,
                        u.Element("family_size")?.Value
                    );
                }

                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        // ADD User
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Validity.Check())
            {
                XDocument doc = XDocument.Load(filePath);
                XElement root = doc.Element("Users");

                root.Add(new XElement("User",
                    new XElement("user_id", txtUserId.Text),
                    new XElement("ration_card_no", txtRation.Text),
                    new XElement("name", txtName.Text),
                    new XElement("phone_no", txtPhone.Text),
                    new XElement("family_size", txtFamily.Text),
                    new XElement("password", txtPass.Text)
                ));

                doc.Save(filePath);
                LoadUsers();

                txtUserId.Text = txtRation.Text = txtName.Text = txtPhone.Text = txtFamily.Text = txtPass.Text = "";
            }
        }

        // EDIT Mode
        protected void gvUsers_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            LoadUsers();
        }

        // CANCEL Edit
        protected void gvUsers_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            LoadUsers();
        }

        // UPDATE User
        protected void gvUsers_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            if (Validity.Check())
            {
                string userId = gvUsers.DataKeys[e.RowIndex].Value.ToString();

                XDocument doc = XDocument.Load(filePath);
                var user = doc.Descendants("User").FirstOrDefault(x => x.Element("user_id").Value == userId);

                if (user != null)
                {
                    user.Element("ration_card_no").Value = ((System.Web.UI.WebControls.TextBox)gvUsers.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
                    user.Element("name").Value = ((System.Web.UI.WebControls.TextBox)gvUsers.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
                    user.Element("phone_no").Value = ((System.Web.UI.WebControls.TextBox)gvUsers.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
                    user.Element("family_size").Value = ((System.Web.UI.WebControls.TextBox)gvUsers.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
                }

                doc.Save(filePath);
                gvUsers.EditIndex = -1;
                LoadUsers();
            }
        }

        // DELETE User
        protected void gvUsers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            if (Validity.Check())
            {
                string userId = gvUsers.DataKeys[e.RowIndex].Value.ToString();

                XDocument doc = XDocument.Load(filePath);
                var user = doc.Descendants("User").FirstOrDefault(x => x.Element("user_id").Value == userId);

                if (user != null)
                    user.Remove();

                doc.Save(filePath);
                LoadUsers();
            }
        }
    }
}