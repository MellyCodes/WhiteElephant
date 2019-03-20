<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WhiteElephantWebsite.Login" %>
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
    <div>
        <asp:Login ID="Login1" runat="server" BackColor="coral" BorderColor="black" BorderPadding="4" 
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="1em" ForeColor="black" 
            OnAuthenticate="Login1_Authenticate"             
            OnLoginError="Login1_LoginError" TextLayout="TextOnTop" Width="339px">
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <LoginButtonStyle BackColor="Coral" BorderColor="Black" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Verdana" 
              />
            <TextBoxStyle Font-Size="0.8em" BorderColor="Black" Width="20em"/>
            <LabelStyle Font-Size="1em" />
            <TitleTextStyle Font-Size="1em" BackColor="Coral" />
            <CheckBoxStyle Font-Size=".8em" />
            <FailureTextStyle Font-Size="1.5em" />
            <ValidatorTextStyle Font-Size="1.5em" />
            <TitleTextStyle BackColor="coral" Font-Bold="True" Font-Size="0.9em" ForeColor="black" />
        </asp:Login>        
        <br />
    </div>
    <div>
        <a href="AccountCreation.aspx">Create Account</a>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>