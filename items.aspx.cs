using Sysmgr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
namespace BiometricRationDistribution
{
    public partial class items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRationItems();
            }
            if(Application["wt"]==null)
            {
                Application["wt"] = 0;
            }
        }
        private void LoadRationItems()
        {
            if (Validity.Check())
            {
                DataSet ds = new DataSet();
                string xmlFilePath = Server.MapPath("~/App_Data/RationItems.xml");

                try
                {
                    ds.ReadXml(xmlFilePath);

                    if (ds.Tables["Item"] != null)
                    {
                        ddlRationItems.DataSource = ds.Tables["Item"];
                        ddlRationItems.DataTextField = "item_name";   // What the user sees
                        ddlRationItems.DataValueField = "item_id";    // Underlying value
                        ddlRationItems.DataBind();

                        ddlRationItems.Items.Insert(0, new ListItem("-- Select Item --", "0"));
                    }
                }
                catch (Exception ex)
                {
                    // Handle error (e.g., log or display)
                    ddlRationItems.Items.Clear();
                    ddlRationItems.Items.Add(new ListItem("Error loading items", "0"));
                }
            }
        }
        protected void ddlRationItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rationFilePath = Server.MapPath("~/App_Data/RationItems.xml");
            string txnFilePath = Server.MapPath("~/App_Data/Transactions.xml");

            string selectedItemId = ddlRationItems.SelectedValue;
            string currentUserId = Session["uid"]?.ToString() ?? "1"; // default to 1 if not logged in

            try
            {
                // 🔹 Step 1: Read RationItems.xml to get full quota
                DataSet dsRation = new DataSet();
                dsRation.ReadXml(rationFilePath);

                DataTable rationTable = dsRation.Tables["Item"];
                DataRow[] rationRows = rationTable.Select($"item_id = '{selectedItemId}'");

                if (rationRows.Length == 0)
                {
                    Literal1.Text = "Selected item not found in ration list.";
                    return;
                }

                string itemName = rationRows[0]["item_name"].ToString();
                decimal totalQuota = Convert.ToDecimal(rationRows[0]["quota"]);

                // 🔹 Step 2: Read Transactions.xml
                DataSet dsTxn = new DataSet();
                dsTxn.ReadXml(txnFilePath);

                DataTable txnTable = dsTxn.Tables["Transaction"];
                decimal consumedQty = 0;

                if (txnTable != null)
                {
                    DataRow[] userTxns = txnTable.Select($"user_id = '{currentUserId}' AND item_id = '{selectedItemId}'");

                    if (userTxns.Length > 0)
                    {
                        // Sum all previous transactions (instead of taking last one)
                        consumedQty = userTxns.Sum(r => Convert.ToDecimal(r["qty"]));
                    }
                }

                decimal availableQuota = totalQuota - consumedQty;
                if (availableQuota < 0) availableQuota = 0;

                // Store quota in session for later use (e.g., validation or insert)
                Session["quota"] = availableQuota.ToString("F2");

                // 🔹 Step 3: Display data
                Literal1.Text = $"<b>Item:</b> {itemName}<br/>" +
                                $"<b>Total Quota:</b> {totalQuota} KG<br/>" +
                                $"<b>Consumed Qty:</b> {consumedQty} KG<br/>" +
                                $"<b>Available Quota:</b>";

                Literal2.Text = availableQuota.ToString();
            }
            catch (Exception ex)
            {
                Literal1.Text = "Error reading data: " + ex.Message;
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            int a = int.Parse(TextBox1.Text);
            int b = (int)double.Parse(Literal2.Text);
            if (a < b && a>0)
            {
                Application["wt"] = a;
                Literal3.Text = "Processing... Please wait for the transaction to finish.";
                Timer1.Enabled = true;
            }
            else
            {
                Literal3.Text = "You cannot request more than your available quota or 0 quantity.";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if(Application["wt"].ToString().Contains ("0"))
            {
                int userId = Convert.ToInt32(Session["uid"]);     
                int itemId = Convert.ToInt32(ddlRationItems.SelectedValue);
                decimal quota = Convert.ToDecimal(Session["quota"]);  
                decimal qty = Convert.ToDecimal(TextBox1.Text);
                InsertTransaction(userId, itemId, quota, qty);
                Timer1.Enabled=false;
                Literal3.Text = "Thank you! The transaction has been processed and saved.";
            }
            Literal4.Text = DateTime.Now.ToLongTimeString();
        }
        private void InsertTransaction(int userId, int itemId, decimal quota, decimal qty)
        {
            string xmlPath = Server.MapPath("~/App_Data/Transactions.xml");

            XmlDocument doc = new XmlDocument();

            try
            {
                // Load existing XML
                doc.Load(xmlPath);

                XmlNode root = doc.DocumentElement;

                // Auto-increment txn_id
                int newTxnId = 1;

                XmlNodeList existingTxns = doc.GetElementsByTagName("Transaction");
                if (existingTxns.Count > 0)
                {
                    var lastTxn = existingTxns[existingTxns.Count - 1];
                    int lastId = int.Parse(lastTxn["txn_id"].InnerText);
                    newTxnId = lastId + 1;
                }

                // Calculate available quota
                decimal availableQuota = quota - qty;
                if (availableQuota < 0) availableQuota = 0;

                // Create new Transaction node
                XmlElement txn = doc.CreateElement("Transaction");

                txn.AppendChild(CreateNode(doc, "txn_id", newTxnId.ToString()));
                txn.AppendChild(CreateNode(doc, "user_id", userId.ToString()));
                txn.AppendChild(CreateNode(doc, "item_id", itemId.ToString()));
                txn.AppendChild(CreateNode(doc, "quota", quota.ToString("F2")));
                txn.AppendChild(CreateNode(doc, "qty", qty.ToString("F2")));
                txn.AppendChild(CreateNode(doc, "available_quota", availableQuota.ToString("F2")));
                txn.AppendChild(CreateNode(doc, "txn_date", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")));

                // Append to root
                root.AppendChild(txn);

                // Save back to XML
                doc.Save(xmlPath);

                Literal1.Text = "Transaction inserted successfully.";
            }
            catch (Exception ex)
            {
                Literal1.Text = "Error inserting transaction: " + ex.Message;
            }
        }

        // 🔧 Helper function to create nodes
        private XmlElement CreateNode(XmlDocument doc, string name, string value)
        {
            XmlElement element = doc.CreateElement(name);
            element.InnerText = value;
            return element;
        }
    }
}
