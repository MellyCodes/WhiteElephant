<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="WhiteElephantWebsite.Checkout" %>
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
    <div id="notLoggedIn" runat="server" visible="false">
        You are not logged in. <a href="Login.aspx?returnurl=checkout.aspx">Login</a> or <a href="AccountCreation.aspx?returnurl=checkout.aspx">create a new account</a> to complete your order
    </div>
    <!-- Order Summary -->
    <!-- Get User Info -->
    <h5 class="candy-border" style="text-align: center;" runat="server" id="ship">Billing Address</h5>
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
    <div id="shippingCheckbox" runat="server" visible="false">
        <asp:CheckBox ID="chkDifferentShipping" runat="server" AutoPostBack="true" OnCheckedChanged="chkDifferentShipping_CheckedChanged" Text="Different Shipping Address" />
    </div>
    <br />
    <br />
    <div id="shippingAddress" runat="server" visible="false">
        <asp:Label ID="lblShippingStreet" runat="server" Text="Street: "></asp:Label>
        <br />
        <asp:TextBox ID="txtShippingStreet" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblShippingCity" runat="server" Text="City: "></asp:Label>
        <br />
        <asp:TextBox ID="txtShippingCity" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblShippingProvince" runat="server" Text="Province: "></asp:Label>
        <br />
        <asp:TextBox ID="txtShippingProvince" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code: "></asp:Label>
        <br />
        <asp:TextBox ID="txtShippingPostalCode" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regPostalCode" runat="server"
                        ErrorMessage="Postal code is not a valid format"
                        ForeColor="Red"
                        ToolTip="Postal code is not a valid format"
                        ControlToValidate="txtShippingPostalCode"
                        ValidationExpression="^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$"
                        ValidationGroup="Card1"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
    </div>
    <br />
    <div id="creditCardInfo" runat="server" visible="false">
        <asp:Label ID="lblCreditCardNumber" runat="server" Text="Credit Card Number: "></asp:Label>
        <br />
        <asp:TextBox ID="txtCreditCardNumber" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqCreditCardNumber" ValidationGroup="Card1"
            runat="server"
            ErrorMessage="You must provide a credit card number"
            Display="Dynamic"
            ControlToValidate="txtCreditCardNumber"
            ToolTip="You must provide a credit card number"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regCreditCardNumber" runat="server"
                        ErrorMessage="Credit card number must be 15 digits"
                        ForeColor="Red"
                        ToolTip="Credit card number must be 15 digits"
                        ControlToValidate="txtCreditCardNumber"
                        ValidationExpression="[1-9][0-9]{14}"
                        ValidationGroup="Card1"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
        
        <br />
        <asp:Label ID="lblCardType" runat="server" Text="Card Type:"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlCardType" AppendDataBoundItems="true" runat="server">
            <asp:ListItem Text="Visa" Value="0" />
            <asp:ListItem Text="Mastercard" Value="0" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="reqCardType"
            ValidationGroup="Card1"
            runat="server"
            ErrorMessage="You must select a credit card type"
            ControlToValidate="ddlCardType"
            Display="Dynamic"
            ToolTip="You must select a credit card type."
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblCVV" runat="server" Text="CVV:"></asp:Label><br />
        <asp:TextBox ID="txtCVV" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqCVV"
            ValidationGroup="Card1"
            runat="server"
            ErrorMessage="You must provide your 3 digit CVV number"
            ControlToValidate="txtCVV"
            Display="Dynamic"
            ToolTip="You must provide your 3 digit CVV number"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regCVV" runat="server"
                        ErrorMessage="CVV must be 3 digits"
                        ForeColor="Red"
                        ToolTip="CVV must be 3 digits"
                        ControlToValidate="txtCVV"
                        ValidationExpression="[0-9]{3}"
                        ValidationGroup="Card1"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="lblExpiry" runat="server" Text="Expiry Date: "></asp:Label>
        <br />
        <asp:Calendar ID="cldExpiryDate" runat="server" OnSelectionChanged="cldExpiryDate_SelectionChanged"></asp:Calendar>
        <asp:TextBox ID="txtMyCal" runat="server"></asp:TextBox>
        <asp:RangeValidator ID="rngMydate"
                            runat="server"
                            ErrorMessage="Invalid date."
                            ToolTip="Invalid date"
                            ValidationGroup="Card1"
                            ControlToValidate="txtMyCal"
                            Type="Date">*</asp:RangeValidator>
    </div>
    <br />
    <h5 class="candy-border" style="text-align: center;" runat="server" id="order">Order Details</h5>
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
    <div style="text-align: center">
        <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="red"></asp:Label>
        <asp:Label ID="lblTotal" runat="server" Text="Total:" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblCartTotal" runat="server" Text="" Font-Bold="true" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <asp:Button ID="btnSubmitOrder" runat="server" Text="Confirm Order" ValidationGroup="Card1" OnClick="btnSubmitOrder_Click" CssClass="crs" />
    <asp:Button ID="btnUpdateMyCart" runat="server" Text="Make changes to my order" OnClick="btnUpdateMyCart_Click" CssClass="crs" />
    <asp:Button ID="btnContinueShopping" runat="server" Text="Continue Shopping" Width="151px" OnClick="btnContinueShopping_Click" CssClass="crs" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
