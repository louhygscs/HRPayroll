<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="SendEmployee.aspx.cs" Inherits="ERP.Modules.BioMetricDevice.Maintenance.SendEmployee" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Send Employee To Device(s)</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->

    <div class="page-header">
        <div class="pageHeading">
            <h3>Send Employee To Device(s)</h3>


        </div>

    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content noFiltering">
                    <div class="headingOftabel">
                        <h4>Send <span>Employee To Device(s)</span>
                        </h4>
                    </div>
                    <form id="frmMain" runat="server">

                        <div class="form-actions">
                            <asp:Button ID="btnConfrim" runat="server" CssClass="btn btn-success" Text="Apply" OnClientClick="return confirm('Are you sure, you want to send Employee to Device(s)?');" OnClick="btnConfrim_Click" />
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
