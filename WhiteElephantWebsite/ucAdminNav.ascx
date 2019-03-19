<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAdminNav.ascx.cs" Inherits="WhiteElephantWebsite.ucAdminNav" %>
<div id="header">
    <asp:Image ID="logo" runat="server" ImageUrl="~/images/WhiteElephantLogo.png" /><br />   
    <asp:Label ID="lblHeading" EnableViewState="false" runat="server" Text="" CssClass="content-heading large">Administration Portal</asp:Label>
    <nav>
        <a href="ProductMaintenance.aspx">Product Maintenance</a> &middot 
        <a href="Images.aspx">Image Maintenance</a> &middot 
        <a href="CategoryMaintenance.aspx">Category Maintenance</a> &middot
        <asp:HyperLink ID="hypLogin" runat="server" NavigateUrl="~/Logout.aspx">Logout</asp:HyperLink>
    </nav>
    <hr />
    <asp:Label ID="lblMasterMessage" runat="server" EnableViewState="False" SkinID="msgLabel"></asp:Label>
</div>