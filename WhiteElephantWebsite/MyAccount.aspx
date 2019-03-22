﻿<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="WhiteElephantWebsite.MyAccount" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>Account Details</h2>
    <br />
     <div id="notLoggedIn" runat="server" visible="false">
        You are not logged in. <a href="Login.aspx?returnurl=MyAccount.aspx">Login</a> or <a href="AccountCreation.aspx?returnurl=MyAccount.aspx">create a new account</a> to view and manage your account.
    </div>
    <asp:Label ID="lblConfirmationEmailSent" runat="server" Text=""></asp:Label>
    <asp:DetailsView ID="detUser" runat="server" Height="50px" Width="483px" AutoGenerateRows="False" CellPadding="4" ForeColor="#333333" GridLines="None" 
        OnModeChanging="detUser_ModeChanging" OnItemUpdating="detUser_ItemUpdating">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
    <EditRowStyle BackColor="#999999" />
    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
    <Fields>
        <asp:BoundField DataField="street" HeaderText="Street:" />
        <asp:BoundField DataField="city" HeaderText="City:" />
        <asp:BoundField DataField="province" HeaderText="Province:" />
        <asp:BoundField DataField="postalCode" HeaderText="Postal Code:" />
        <asp:BoundField DataField="phone" HeaderText="Phone:" />
        <asp:CommandField ButtonType="Button" ShowEditButton="true" />
    </Fields>
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
</asp:DetailsView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    
    </asp:Content>
