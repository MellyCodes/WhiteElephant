<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCategoriesNavigation.ascx.cs" Inherits="WhiteElephantWebsite.ucCategoriesNavigation" %>
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