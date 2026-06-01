<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ARation.aspx.cs" Inherits="BiometricRationDistribution.ARation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .grid { width:100%; border-collapse:collapse; }
        .grid th, .grid td { border:1px solid #ccc; padding:8px; text-align:center; }
        .grid th { background:#2c3e50; color:white; }
        .form-section { margin-top: 20px; }

        .form-section h3 {
            margin-bottom: 15px;
            color: #2c3e50;
            font-size: 18px;
            border-bottom: 2px solid #2c3e50;
            padding-bottom: 5px;
        }

        .form-group { margin-bottom: 12px; }

        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 14px;
            box-sizing: border-box;
            transition: border-color 0.3s;
        }

        .form-control:focus {
            border-color: #2c3e50;
            outline: none;
        }

        .btn-submit {
            width: 100%;
            padding: 12px;
            background: #2c3e50;
            color: white;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            cursor: pointer;
            transition: background 0.3s;
        }

        .btn-submit:hover {
            background: #1a242f;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 style="text-align:center; color:#2c3e50;">Ration Items Management</h2>

    <!-- GridView -->
    <asp:GridView ID="gvRationItems" runat="server" AutoGenerateColumns="False" CssClass="grid"
        DataKeyNames="item_id" OnRowEditing="gvRationItems_RowEditing"
        OnRowUpdating="gvRationItems_RowUpdating" OnRowCancelingEdit="gvRationItems_RowCancelingEdit"
        OnRowDeleting="gvRationItems_RowDeleting">

        <Columns>
            <asp:BoundField DataField="item_id" HeaderText="Item ID" ReadOnly="True" />
            <asp:BoundField DataField="item_name" HeaderText="Item Name" />
            <asp:BoundField DataField="quota" HeaderText="Quota (kg/litre)" />
            <asp:BoundField DataField="price" HeaderText="Price (₹)" />

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <hr />

   <!-- Add New Ration Item -->
<div class="form-section">
    <h3>Add New Ration Item</h3>

    <div class="form-group">
        <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" Placeholder="Item Name"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtQuota" runat="server" CssClass="form-control" Placeholder="Quota (kg/litre)"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" Placeholder="Price (₹)"></asp:TextBox>
    </div>

    <asp:Button ID="btnAddRationItem" runat="server" Text="Add Item" CssClass="btn-submit" OnClick="btnAddRationItem_Click" />
</div>

</asp:Content>

