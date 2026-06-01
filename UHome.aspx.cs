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
    public partial class UHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        private void LoadData()
        {
            if (Validity.Check())
            {
                if (Session["uid"] == null)
                {
                    Response.Write("User not logged in!");
                    return;
                }

                string currentUserId = Session["uid"].ToString();

                // ✅ Step 2: Load RationItems.xml
                DataSet dsRation = new DataSet();
                dsRation.ReadXml(Server.MapPath("~/App_Data/RationItems.xml"));

                if (dsRation.Tables["Item"] == null)
                {
                    Response.Write("Ration items not found.");
                    return;
                }

                DataTable rationTable = dsRation.Tables["Item"];

                // ✅ Step 3: Load Transactions.xml
                DataSet dsTxn = new DataSet();
                dsTxn.ReadXml(Server.MapPath("~/App_Data/Transactions.xml"));

                DataTable txnTable = dsTxn.Tables["Transaction"];
                if (txnTable == null)
                {
                    // No transactions yet, initialize empty table
                    txnTable = new DataTable();
                    txnTable.Columns.Add("txn_id", typeof(int));
                    txnTable.Columns.Add("user_id", typeof(string));
                    txnTable.Columns.Add("item_id", typeof(string));
                    txnTable.Columns.Add("quota", typeof(decimal));
                    txnTable.Columns.Add("qty", typeof(decimal));
                    txnTable.Columns.Add("available_quota", typeof(decimal));
                    txnTable.Columns.Add("txn_date", typeof(DateTime));
                }

                // ✅ Step 4: Prepare Result Table
                DataTable resultTable = new DataTable();
                resultTable.Columns.Add("Item ID", typeof(int));
                resultTable.Columns.Add("Item Name", typeof(string));
                resultTable.Columns.Add("Total Quota (KG)", typeof(decimal));
                resultTable.Columns.Add("Consumed Qty (KG)", typeof(decimal));
                resultTable.Columns.Add("Available Quota (KG)", typeof(decimal));

                // ✅ Step 5: Process each item
                foreach (DataRow rationRow in rationTable.Rows)
                {
                    int itemId = Convert.ToInt32(rationRow["item_id"]);
                    string itemName = rationRow["item_name"].ToString();
                    decimal quota = Convert.ToDecimal(rationRow["quota"]);

                    // Get total quantity consumed by current user for this item
                    decimal consumed = 0;

                    if (txnTable.Rows.Count > 0)
                    {
                        var userTransactions = txnTable.AsEnumerable()
                            .Where(r =>
                                r.Field<string>("user_id") == currentUserId &&
                                r.Field<string>("item_id") == itemId.ToString());

                        if (userTransactions.Any())
                        {
                            consumed = userTransactions.Sum(r => Convert.ToDecimal(r["qty"]));
                        }
                    }

                    decimal available = quota - consumed;
                    if (available < 0) available = 0;

                    // Add row to result
                    resultTable.Rows.Add(itemId, itemName, quota, consumed, available);
                }

                // ✅ Step 6: Bind to GridView
                GridView1.DataSource = resultTable;
                GridView1.DataBind();
            }
        }
    }
}