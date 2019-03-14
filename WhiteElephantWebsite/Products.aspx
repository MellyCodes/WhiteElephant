<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WhiteElephantWebsite.Products" %>
<%@ Register Src="~/ucProducts.ascx" TagPrefix="uc1" TagName="ucProducts" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <uc1:ucProducts runat="server" id="ucProducts" />
</asp:Content>
