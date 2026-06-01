<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="UHome.aspx.cs" Inherits="BiometricRationDistribution.UHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f7f9fc;
            margin: 20px;
        }
        h1 {
            text-align: center;
            color: #2c3e50;
            font-size: 28px;
            margin-bottom: 20px;
        }
        .gridview {
            width: 80%;
            margin: auto;
            border-collapse: collapse;
            box-shadow: 0px 3px 8px rgba(0,0,0,0.1);
        }
        .gridview th {
            background-color: #2c3e50;
            color: #ffffff;
            padding: 10px;
            text-align: center;
            font-size: 15px;
        }
        .gridview td {
            padding: 8px;
            text-align: center;
            font-size: 14px;
            color: #333;
        }
        .gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        .gridview tr:hover {
            background-color: #dff0ff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Your Ration Transaction for 
            <%= DateTime.Now.ToString("MMMM") %> <%= DateTime.Now.Year %>
        </h1>

        <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="true" 
            CssClass="gridview"
            BorderStyle="None"
            GridLines="Horizontal"
            CellPadding="5">
        </asp:GridView>
</asp:Content>
