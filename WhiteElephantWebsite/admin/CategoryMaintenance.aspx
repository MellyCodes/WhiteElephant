<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="CategoryMaintenance.aspx.cs" Inherits="WhiteElephantWebsite.admin.CategoryMaintenance" %>
<%@ Register TagPrefix="uc1" TagName="ucAdminNav" Src="~/ucAdminNav.ascx" %>

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
     NBCC Academic Integrity Policy (policy 1111)--%><asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <uc1:ucAdminNav runat="server" ID="ucAdminNav" />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="padding-bottom: 20px;">
        <h3>Category Maintenance</h3>
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
        <asp:GridView ID="grdCategories" runat="server" AutoGenerateColumns="False"
            DataKeyNames="id"
            OnRowCommand="grdCategories_RowCommand"
            OnRowDeleting="grdCategories_RowDeleting"
            OnRowUpdating="grdCategories_RowUpdating"
            OnRowEditing="grdCategories_RowEditing"
            OnRowCancelingEdit="grdCategories_RowCancelingEdit"
            AllowPaging="True"
            OnPageIndexChanging="grdCategories_PageIndexChanging"
            PageSize="5"
            ShowFooter="True"
            AllowSorting="True"
            >
            <FooterStyle BackColor="#3399ff" />
            <Columns>
                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCategoryName" runat="server" Text='<%# Eval("name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtCategoryNameNew" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDesc" runat="server" Text='<%# Eval("description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDescNew" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update"
                            Text="Update" />&nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text="Cancel" />
                    
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit" />&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False"
                                CommandName="Delete" Text="Delete" />
                    
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="btnNew" runat="server" CommandName="btnNew" Text="Insert New Category" />
                    
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>
    

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
