<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucProducts.ascx.cs" Inherits="WhiteElephantWebsite.ucProducts" %>
<asp:Label ID="lblHeading" EnableViewState="false" runat="server" Text="" CssClass="large"></asp:Label>
<br />

<asp:Repeater ID="rptProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
    <ItemTemplate>
        <div class="flexcontainer">
            <div class="flex-cols">
                <asp:Image
                    CssClass="productsinrepeater"
                    ID="Image1"
                    runat="server"
                    AlternateText='<%# Eval("altText") %>'
                    ImageUrl='<%# Eval("ImageName") %>' />
            </div>
            <%
                string productId = Request.QueryString["id"];
                if (string.IsNullOrEmpty(productId))
                {%>
            <div class="flex-cols">
                <a href='Products.aspx?id=<%# Eval("id") %>'><%# Eval("id") %></a>
            </div>
            <%}%>
            <div class="flex-cols">
                <asp:Label ID="lblName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
            </div>
            <div class="flex-cols">
                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("price", "{0:c}") %>'></asp:Label>
            </div>
        </div>

        <%                
            if (!string.IsNullOrEmpty(productId))
            {
        %>
        <!--Details -->
        <strong>Product Featured:</strong><br />
        <asp:CheckBox Enabled="false" ID="chkFeatured" Checked='<%# Eval("featured") %>' runat="server" /><br />
        <strong>Brief Description:</strong><asp:Label ID="lblBriefDesc" runat="server" Text='<%# Eval("briefDescription") %>'></asp:Label><br />
        <strong>Full Description:</strong><asp:Label ID="lblFullDescription" runat="server" Text='<%# Eval("fullDescription") %>'></asp:Label><br /><br />
        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" style="cursor:pointer;" CommandArgument='<%# Eval("Id") %>' CommandName="Add"/>
        <%}%>
        
    </ItemTemplate>
    <SeparatorTemplate>
        <hr />
    </SeparatorTemplate>
</asp:Repeater><br />
<asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" EnableViewState="false"></asp:Label>
