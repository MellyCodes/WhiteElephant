<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WhiteElephantWebsite.admin.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:Label ID="lblHeading" EnableViewState="false" runat="server" Text="" CssClass="content-heading large">Administration Login</asp:Label>
     <div>
        <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" 
            OnAuthenticate="Login1_Authenticate" 
            OnLoginError="Login1_LoginError">
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Verdana" 
               Font-Size="1.5em" ForeColor="#284775"/>
            <TextBoxStyle Font-Size="1.5em" BorderColor="Black"/>
            <LabelStyle Font-Size="1.5em" />
            <TitleTextStyle Font-Size="1.5em" />
            <CheckBoxStyle Font-Size="1.5em" />
            <FailureTextStyle Font-Size="1.5em" />
            <ValidatorTextStyle Font-Size="1.5em" />
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
        </asp:Login>        
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
