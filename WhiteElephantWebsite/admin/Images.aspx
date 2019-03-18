<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="Images.aspx.cs" Inherits="WhiteElephantWebsite.admin.ImageApproval" %>
<%@ Register TagPrefix="uc1" TagName="ucadminnav" Src="~/ucAdminNav.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <uc1:ucadminnav runat="server" id="ucAdminNav" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="padding-bottom: 20px;">
        <h4 style="text-align:center">Image Moving and File IO</h4>
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
        <div>
            <asp:FileUpload ID="uplImage" runat="server" /><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"   /><br />
            <asp:Image ID="imgImageUpload" runat="server" Visible="false"/>
            <br />
            <asp:DropDownList ID="ddlImages" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlImages_SelectedIndexChanged" ></asp:DropDownList> <asp:Button ID="btnMove" runat="server" Text="Move to Images" OnClick="btnMove_Click" />
        </div>
       
</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
