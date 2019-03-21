<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="ProductMaintenance.aspx.cs" Inherits="WhiteElephantWebsite.admin.ProductMaintenance" %>
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
     NBCC Academic Integrity Policy (policy 1111)--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <uc1:ucAdminNav runat="server" ID="ucAdminNav" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div style="padding-bottom: 20px;">
            <h3>Product Maintenance</h3>
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
        <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False"
            DataKeyNames="id"
            OnRowCommand="grdProducts_RowCommand"
            OnRowDeleting="grdProducts_RowDeleting"
            OnRowUpdating="grdProducts_RowUpdating"
            OnRowEditing="grdProducts_RowEditing"
            OnRowCancelingEdit="grdProducts_RowCancelingEdit"
            AllowPaging="True"
            OnPageIndexChanging="grdProducts_PageIndexChanging"
            PageSize="5"
            ShowFooter="True"
            AllowSorting="True"
            >
            <FooterStyle BackColor="#3399ff" />
            <Columns>
                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                        <asp:Label ID="lblProductId" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProductName" runat="server" Text='<%# Eval("name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtProductNameNew" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Brief Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBriefDesc" runat="server" Text='<%# Eval("briefDescription") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBriefDesc" runat="server" Text='<%# Eval("briefDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtBriefDescNew" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Full Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFullDesc" runat="server" Text='<%# Eval("fullDescription") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFullDesc" runat="server" Text='<%# Eval("fullDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtFullDescNew" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Active">
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkStatus" runat="server" Checked='<%# Bind("statusCode") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:CheckBox ID="chkStatusNew" runat="server" />
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStatusDisplay" runat="server" Checked='<%# Bind("statusCode") %>' Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPrice" runat="server" Text='<%# Eval("price", "{0:0.00}") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:c}") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPriceNew" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Featured">
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkFeatured" runat="server" Checked='<%# Bind("featured") %>' />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:CheckBox ID="chkFeaturedNew" runat="server" />
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkFeaturedDisplay" runat="server" Checked='<%# Bind("featured") %>' Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlCategoriesNew" runat="server"></asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# "~/" + Eval("ImageName") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:FileUpload ID="uplUpdateImage" runat="server" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:FileUpload ID="uplNewImage" runat="server" />
                    </FooterTemplate>
                    <ControlStyle Width="200px" />
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
                        <asp:Button ID="btnNew" runat="server" CommandName="btnNew" Text="Insert New Product" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
