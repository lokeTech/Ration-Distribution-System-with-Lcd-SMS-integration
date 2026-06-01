<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AHome.aspx.cs" Inherits="BiometricRationDistribution.AHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .grid { width:100%; border-collapse:collapse; }
        .grid th, .grid td { border:1px solid #ccc; padding:8px; text-align:center; }
        .grid th { background:#2c3e50; color:white; }
        .form-section {
    margin-top: 20px;
}

.form-section h3 {
    margin-bottom: 15px;
    color: #2c3e50;
    font-size: 18px;
    border-bottom: 2px solid #2c3e50;
    padding-bottom: 5px;
}

.form-group {
    margin-bottom: 12px;
}

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

    <h2 style="text-align:center; color:#2c3e50;">Users Management</h2>

    <!-- GridView -->
    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CssClass="grid"
        DataKeyNames="user_id" OnRowEditing="gvUsers_RowEditing"
        OnRowUpdating="gvUsers_RowUpdating" OnRowCancelingEdit="gvUsers_RowCancelingEdit"
        OnRowDeleting="gvUsers_RowDeleting">

        <Columns>
            <asp:BoundField DataField="user_id" HeaderText="User ID" ReadOnly="True" />
            <asp:BoundField DataField="ration_card_no" HeaderText="Ration Card No" />
            <asp:BoundField DataField="name" HeaderText="Name" />
            <asp:BoundField DataField="phone_no" HeaderText="Phone" />
            <asp:BoundField DataField="family_size" HeaderText="Family Size" />

            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <hr />

   <!-- Add New User -->
<div class="form-section">
    <h3>Add New User</h3>

    <div class="form-group">
        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" Placeholder="User ID"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtRation" runat="server" CssClass="form-control" Placeholder="Ration Card No"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Name"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Phone"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control" Placeholder="Family Size"></asp:TextBox>
    </div>

    <div class="form-group">
        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Password"></asp:TextBox>
    </div>

    <asp:Button ID="btnAdd" runat="server" Text="Add User" CssClass="btn-submit" OnClick="btnAdd_Click" />
</div>


</asp:Content>
