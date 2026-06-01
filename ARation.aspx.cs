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
    public partial class ARation : System.Web.UI.Page
    {
        string filePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            filePath = Server.MapPath("~/App_Data/rationitems.xml");

            // Create XML file if not exists
            if (!System.IO.File.Exists(filePath))
            {
                new XDocument(new XElement("RationItems")).Save(filePath);
            }

            if (!IsPostBack)
                LoadRationItems();
        }

        private void LoadRationItems()
        {
            XDocument doc = XDocument.Load(filePath);
            DataTable dt = new DataTable();
            dt.Columns.Add("item_id");
            dt.Columns.Add("item_name");
            dt.Columns.Add("quota");
            dt.Columns.Add("price");

            foreach (var item in doc.Descendants("Item"))
            {
                dt.Rows.Add(
                    item.Element("item_id")?.Value,
                    item.Element("item_name")?.Value,
                    item.Element("quota")?.Value,
                    item.Element("price")?.Value
                );
            }
            
            gvRationItems.DataSource = dt;
            gvRationItems.DataBind();
        }

        // ADD Ration Item
        protected void btnAddRationItem_Click(object sender, EventArgs e)
        {
            XDocument doc = XDocument.Load(filePath);
            XElement root = doc.Element("RationItems");

            // Generate new ID
            int newId = 1;
            var lastItem = doc.Descendants("Item").LastOrDefault();
            if (lastItem != null)
                newId = int.Parse(lastItem.Element("item_id").Value) + 1;

            root.Add(new XElement("Item",
                new XElement("item_id", newId),
                new XElement("item_name", txtItemName.Text.Trim()),
                new XElement("quota", txtQuota.Text.Trim()),
                new XElement("price", txtPrice.Text.Trim())
            ));

            doc.Save(filePath);
            LoadRationItems();

            txtItemName.Text = txtQuota.Text = txtPrice.Text = "";
        }

        // EDIT Mode
        protected void gvRationItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRationItems.EditIndex = e.NewEditIndex;
            LoadRationItems();
        }

        // CANCEL Edit
        protected void gvRationItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRationItems.EditIndex = -1;
            LoadRationItems();
        }

        // UPDATE Item
        protected void gvRationItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string itemId = gvRationItems.DataKeys[e.RowIndex].Value.ToString();
            XDocument doc = XDocument.Load(filePath);
            var item = doc.Descendants("Item").FirstOrDefault(x => x.Element("item_id").Value == itemId);

            if (item != null)
            {
                item.Element("item_name").Value = ((TextBox)gvRationItems.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
                item.Element("quota").Value = ((TextBox)gvRationItems.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
                item.Element("price").Value = ((TextBox)gvRationItems.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            }

            doc.Save(filePath);
            gvRationItems.EditIndex = -1;
            LoadRationItems();
        }

        // DELETE Item
        protected void gvRationItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string itemId = gvRationItems.DataKeys[e.RowIndex].Value.ToString();
            XDocument doc = XDocument.Load(filePath);
            var item = doc.Descendants("Item").FirstOrDefault(x => x.Element("item_id").Value == itemId);

            if (item != null)
                item.Remove();

            doc.Save(filePath);
            LoadRationItems();
        }
    }
}