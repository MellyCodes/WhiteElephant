﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAdminNav.ascx.cs" Inherits="WhiteElephantWebsite.ucAdminNav" %>
<%--
    
    @project White Elephant E-Commerce Website
     @authors Courtney Diotte
     @authors Melanie Roy-Plommer
     @version 1.0
    
     @section DESCRIPTION
     <   >
    
     @section LICENSE
     Copyright 2018 - 2019
     Permission to use, copy, modify, and/or distribute this software for
     any purpose with or without fee is hereby granted, provided that the
     above copyright notice and this permission notice appear in all copies.
    
     THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
     WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
     MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
     ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
     WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
     ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
     OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
    
    
     @section Academic Integrity
     I certify that this work is solely my own and complies with
     NBCC Academic Integrity Policy (policy 1111)--%>

<div id="header">
    <asp:Image ID="logo" runat="server" ImageUrl="~/images/WhiteElephantLogo.png" /><br />   
    <asp:Label ID="lblHeading" EnableViewState="false" runat="server" Text="" CssClass="content-heading large">Administration Portal</asp:Label>
    <nav>
        <a href="ProductMaintenance.aspx">Product Maintenance</a> &middot 
        <a href="Images.aspx">Image Maintenance</a> &middot 
        <a href="CategoryMaintenance.aspx">Category Maintenance</a> &middot
        <a href="ManageCustomerAccount.aspx">Manage Customer Accounts</a> &middot
        <asp:HyperLink ID="hypLogin" runat="server" NavigateUrl="~/Logout.aspx">Logout</asp:HyperLink>
    </nav>
    <hr />
    <asp:Label ID="lblMasterMessage" runat="server" EnableViewState="False" SkinID="msgLabel"></asp:Label>
</div>