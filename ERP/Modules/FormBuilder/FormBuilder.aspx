<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="FormBuilder.aspx.cs" Inherits="ERP.Modules.FormBuilder.FormBuilder" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Form Builder</title>
    <%--<script src="<%=ResolveUrl("~/Scripts/Modules/FormBuilder/Formbuilder.js")%>"></script>--%>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <div id="app"></div>
    <script src="<%=ResolveUrl("~/Scripts/Modules/FormBuilder/formbuilder/static/bundle.js")%>"></script>
    <%--<script src="../formbuilder/static/bundle.js"></script>--%>
</asp:Content>
