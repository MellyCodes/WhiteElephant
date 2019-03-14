<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="WhiteElephantWebsite.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="notLoggedIn" runat="server" visible="false">
        You are not logged in. <a href="Login.aspx?returnurl=checkout.aspx">Login</a> or <a href="AccountCreation.aspx?returnurl=checkout.aspx">create a new account</a> to complete your order
    </div>
    <!-- Order Summary -->
    <!-- Get User Info -->
    <h5 class="candy-border" style="text-align:center;" runat="server" id="ship">Ship To</h5>
    <asp:DetailsView ID="detUser" runat="server" Height="50px" Width="483px" AutoGenerateRows="False" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>
            <asp:BoundField DataField="firstName" HeaderText="First Name:" />
            <asp:BoundField DataField="lastName" HeaderText="Last Name:" />
            <asp:BoundField DataField="street" HeaderText="Street:" />
            <asp:BoundField DataField="city" HeaderText="City:" />
            <asp:BoundField DataField="province" HeaderText="Province:" />
            <asp:BoundField DataField="postalCode" HeaderText="Postal Code:" />
            <asp:BoundField DataField="phone" HeaderText="Phone:" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>
    <h5 class="candy-border" style="text-align:center;" runat="server" id="order">Order Details</h5>
    <!--Order Total-->
    <asp:GridView ID="grdCart" runat="server" 
        AutoGenerateColumns="False">
        <Columns>            
            <asp:BoundField HeaderText="Product Id" DataField="ProductId" />
            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
            <asp:BoundField HeaderText="Quantity" DataField="Qty" />
            <asp:BoundField DataFormatString="{0:c}" HeaderText="Price" DataField="Price" />
            <asp:BoundField HeaderText="Subtotal" DataField="LineTotal" DataFormatString="{0:c}" />            
        </Columns>
    </asp:GridView>
     <div style="text-align:center">
        <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="red"></asp:Label>
        <asp:Label ID="lblTotal" runat="server" Text="Total:" EnableViewState="false"></asp:Label> <asp:Label ID="lblCartTotal" runat="server" Text="" Font-Bold="true" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
     </div>
    <asp:Button ID="btnSubmitOrder" runat="server" Text="Confirm Order" OnClick="btnSubmitOrder_Click" CssClass="crs" /> 
    <asp:Button ID="btnUpdateMyCart" runat="server" Text="Make changes to my order" OnClick="btnUpdateMyCart_Click" CssClass="crs" /> 
    <asp:Button ID="btnContinueShopping" runat="server" Text="Continue Shopping" Width="151px" OnClick="btnContinueShopping_Click" CssClass="crs" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
