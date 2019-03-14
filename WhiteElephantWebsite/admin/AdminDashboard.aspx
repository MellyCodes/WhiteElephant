<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="WhiteElephantWebsite.admin.AdminDashboard" %>
<%@ Register Src="~/ucAdminNav.ascx" TagPrefix="uc1" TagName="ucAdminNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <uc1:ucAdminNav runat="server" id="ucAdminNav" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    Welcome to the White Elephant Administration Portal Dashboard
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
