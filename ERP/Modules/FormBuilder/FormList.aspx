<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="FormList.aspx.cs" Inherits="ERP.Modules.FormBuilder.FormList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Form List</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/FormBuilder/FormList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Form</h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/FormBuilder/FormBuilder.aspx") %>">Form Builder</a>
                    </span>

                </div>
            </div>
        </div>
    </div>
    <!-- end Breadcrumbs Line -->

</asp:Content>
