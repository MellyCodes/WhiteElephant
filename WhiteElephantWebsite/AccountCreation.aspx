<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="AccountCreation.aspx.cs" Inherits="WhiteElephantWebsite.AccountCreation" %>
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
    <h2>Create Account</h2>
    <div>
        <label>
            Email Address:
        </label>
        <br />
        <asp:TextBox ID="txtEmailAddress" runat="server" ValidationGroup="Group1"></asp:TextBox>
        <asp:RequiredFieldValidator
            CssClass="cursor"
            ID="reqEmailAdress" runat="server"
            ErrorMessage="Email address required"
            ToolTip="Email address is required"
            data-toggle="tooltip"
            data-placement="right"
            ControlToValidate="txtEmailAddress"
            ValidationGroup="Group1"
            Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regEmailAddress" runat="server"
            ErrorMessage="Email is not in valid format" ForeColor="red"
            ToolTip="Email is not in valid format"
            ControlToValidate="txtEmailAddress"
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ValidationGroup="Group1"
            Display="Dynamic">*</asp:RegularExpressionValidator>
        <br />
        <br />
        <label>First Name:</label><br />
        <asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="Group1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqFirstName" runat="server"
            ErrorMessage="First name is required"
            ToolTip="First name is required"
            ControlToValidate="txtFirstName"
            ValidationGroup="Group1"
            Display="Dynamic"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <br />
        <label>Last Name:</label><br />
        <asp:TextBox ID="txtLastName" runat="server" ValidationGroup="Group1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqLastName" runat="server"
            ErrorMessage="Last name is required"
            ToolTip="Last name is required"
            ControlToValidate="txtLastName"
            ValidationGroup="Group1"
            Display="Dynamic"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <br />
        <label>Street:</label><br />
        <asp:TextBox ID="txtStreet" runat="server" ValidationGroup="Group1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqStreet" runat="server"
            ErrorMessage="Street is required"
            ToolTip="Street is required"
            ControlToValidate="txtStreet"
            ValidationGroup="Group1"
            Display="Dynamic"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <br />
        <label>City:</label><br />
        <asp:TextBox ID="txtCity" runat="server" ValidationGroup="Group1"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqCity" runat="server"
            ErrorMessage="City is required"
            ToolTip="City is required"
            ControlToValidate="txtCity"
            ValidationGroup="Group1"
            Display="Dynamic"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <br />
        <br />
        <label>Province:</label><br />
        <asp:TextBox ID="txtProvince" runat="server" ValidationGroup="Group1" palceholder="NB"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqProvince" runat="server"
            ErrorMessage="Province is required"
            ToolTip="Province is required"
            ControlToValidate="txtProvince"
            ValidationGroup="Group1"
            Display="Dynamic"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator 
            Display="Dynamic"                                         
            ID="regProvince" 
            ValidationExpression="^[\s\S]{2,2}$" 
            runat="server" 
            ErrorMessage="Province must be two characters"
            ToolTip="Province must be two characters"
            ControlToValidate="txtProvince"
            ValidationGroup="Group1"
            ForeColor="Red"
            >*</asp:RegularExpressionValidator>
        <br />
        <br />
        <label>Postal Code:</label><br />
        <asp:TextBox ID="txtPostalCode" runat="server" ValidationGroup="Group1" placeholder="H0H0H0"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqPostalCode" runat="server"
            ErrorMessage="Postal code is required"
            ToolTip="Postal code is required"
            ControlToValidate="txtPostalCode"
            ValidationGroup="Group1"
            Display="Dynamic"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regPostalCode" runat="server"
            ErrorMessage="Postal code is not in valid format" ForeColor="red"
            ToolTip="Postal code is not in valid format"
            ControlToValidate="txtPostalCode"
            ValidationExpression="[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"
            ValidationGroup="Group1"
            Display="Dynamic">*</asp:RegularExpressionValidator>
        <br />
        <br />
        <label>Phone:</label><br />
        <asp:TextBox ID="txtPhone" runat="server" ValidationGroup="Group1" placeholder="5065551212"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqPhone" runat="server"
            ErrorMessage="Phone number is required"
            ToolTip="Phone number is required"
            ControlToValidate="txtPhone"
            ValidationGroup="Group1"
            Display="Dynamic"
            ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regPhone" runat="server"
            ErrorMessage="Phone number is not in valid format" ForeColor="red"
            ToolTip="Phone number is not in valid format"
            ControlToValidate="txtPhone"
            ValidationExpression="^([0-9\(\)\/\+ \-]*)$"
            ValidationGroup="Group1"
            Display="Dynamic">*</asp:RegularExpressionValidator>
        <br />
        <br />
        <label>
            Password:
        </label>
        <br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="Group1"></asp:TextBox>
        <asp:RequiredFieldValidator
            CssClass="cursor"
            ID="reqPassword"
            runat="server"
            ErrorMessage="Password is required"
            ToolTip="Password is required"
            data-toggle="tooltip"
            data-placement="right"
            ControlToValidate="txtPassword"
            ValidationGroup="Group1"
            Display="Dynamic" ForeColor="Red">
                            *
        </asp:RequiredFieldValidator>
        <asp:CompareValidator
            CssClass="cursor"
            ID="compPassword"
            runat="server"
            ErrorMessage="Password does not match"
            ToolTip="Passwords do not match"
            data-toggle="tooltip"
            data-placement="right"
            ControlToCompare="txtConfirmPassword"
            ControlToValidate="txtPassword"
            ValidationGroup="Group1"
            Display="Dynamic" ForeColor="Red">*</asp:CompareValidator>
        <br />
        <br />
        <label>
            Confirm Password:
        </label>
        <br />
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" ValidationGroup="Group1"></asp:TextBox>
        <asp:RequiredFieldValidator
            CssClass="cursor"
            ID="reqConfirmPassword" runat="server"
            ErrorMessage="Confirm password is required"
            ToolTip="Confirm password is required"
            data-toggle="tooltip"
            data-placement="right"
            ControlToValidate="txtConfirmPassword"
            ValidationGroup="Group1"
            Display="Dynamic" ForeColor="Red">*</asp:RequiredFieldValidator>
        <asp:CompareValidator
            CssClass="cursor"
            ID="compConfirmPassword"
            runat="server"
            ErrorMessage=""
            ToolTip="Passwords does not match"
            data-toggle="tooltip"
            data-placement="right"
            ControlToValidate="txtConfirmPassword"
            ControlToCompare="txtPassword"
            ValidationGroup="Group1"
            Display="Dynamic" ForeColor="Red">*</asp:CompareValidator>
        <br />
        <br />
    </div>
    <div>
        <asp:Button ID="btnCreateAccount" ValidationGroup="Group1" 
            runat="server" 
            Text="Create Account" 
            OnClick="btnCreateAccount_Click" />
    </div>
    <div>
        <asp:Label ID="lblError" runat="server" EnableViewState="False" class="red"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            ValidationGroup="Group1" ForeColor="Red" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
