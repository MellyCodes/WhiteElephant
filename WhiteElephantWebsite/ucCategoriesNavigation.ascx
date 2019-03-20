<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCategoriesNavigation.ascx.cs" Inherits="WhiteElephantWebsite.ucCategoriesNavigation" %>
<!--
     
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
     NBCC Academic Integrity Policy (policy 1111)
     -->

<h5>Product Categories</h5>
<asp:DataList ID="dtvCategories" runat="server" CellPadding="4" ForeColor="#333333" Height="241px" Width="217px" >
    <AlternatingItemStyle BackColor="White" />
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
    <ItemTemplate>
        <asp:HyperLink
            title='<%# Eval("name") %>'
            CssClass="categories-datalist"
            ID="hypCategories"
            runat="server"
            NavigateUrl='<%# Eval("id","Products.aspx?categoryId={0}") %>'
            Text='<%# Eval("name") %>'></asp:HyperLink>
    </ItemTemplate>
    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
</asp:DataList>
<br />