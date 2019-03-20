<%@ Page Title="" Language="C#" MasterPageFile="~/My.Master" AutoEventWireup="true" CodeBehind="Images.aspx.cs" Inherits="WhiteElephantWebsite.admin.ImageApproval" %>
<%@ Register TagPrefix="uc1" TagName="ucadminnav" Src="~/ucAdminNav.ascx" %>

<!--
     
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
     NBCC Academic Integrity Policy (policy 1111)
     -->

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
            <br />
            <asp:DropDownList ID="ddlImages" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlImages_SelectedIndexChanged" >
                
            </asp:DropDownList> <asp:Button ID="btnMove" runat="server" Text="Move to Images" OnClick="btnMove_Click" />
                
            <asp:Image ID="imgImageUpload" runat="server"/>
        </div>
       
</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
