<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="items.aspx.cs" Inherits="BiometricRationDistribution.items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Select Ration Item <br />
    <asp:DropDownList ID="ddlRationItems" runat="server" AutoPostBack="true" Height="30px" OnSelectedIndexChanged="ddlRationItems_SelectedIndexChanged" Width="50%" />
    <br /> <br />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
    <br />
    <br />
    Enter your Quantity :<br /> <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br /><br />
    <asp:Button ID="Button1" runat="server" Text="Submit" Height="26px" OnClick="Button1_Click" Width="89px" />
    <br />
    <br />
    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
<br />
<asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="2000" OnTick="Timer1_Tick">
</asp:Timer>
    <br />
    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
<br />
</asp:Content>
