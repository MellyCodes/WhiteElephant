<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="ManageCustomerAccount.aspx.cs" Inherits="WhiteElephantWebsite.admin.ManageCustomerAccount" %>
<%@ Register TagPrefix="uc1" TagName="ucAdminNav" Src="~/ucAdminNav.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <uc1:ucAdminNav runat="server" ID="ucAdminNav" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:GridView ID="grdCustomers" runat="server" AutoGenerateColumns="False"
            DataKeyNames="id"
            OnRowDeleting="UpdateArchive"
            AllowPaging="True"
            OnPageIndexChanging="grdCustomer_PageIndexChanging"
            PageSize="5"
            ShowFooter="True"
            AllowSorting="True" 
            >
            <FooterStyle BackColor="#3399ff" />
            <Columns>
                <asp:TemplateField HeaderText="Customer ID">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerID" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name">
                    
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerFirstName" runat="server" Text='<%# Eval("firstName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Last Name">
                    
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerLastName" runat="server" Text='<%# Eval("lastName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
                <asp:TemplateField HeaderText="Archived">
                    
                    <ItemTemplate>
                        <asp:CheckBox ID="chkArchivedDisplay" runat="server" Checked='<%# Bind("IsArchived") %>' Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>
                
               
                <asp:TemplateField>
                    
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Delete"
                            Text="Archive" />
                    </ItemTemplate>
                    
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
