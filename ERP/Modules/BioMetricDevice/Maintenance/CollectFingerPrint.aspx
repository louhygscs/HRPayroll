<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CollectFingerPrint.aspx.cs" Inherits="ERP.Modules.BioMetricDevice.Maintenance.CollectFingerPrint" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Collect Fingerprint</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">


    <!-- /Breadcrumbs line -->

    <div class="page-header">
        <div class="pageHeading">
            <h3>Collect Fingerprint</h3>


        </div>

    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content noFiltering">
                    <div class="headingOftabel">
                        <h4>Collect <span>Fingerprint</span>
                        </h4>
                    </div>

                    <form id="frmMain" runat="server">
                        <div class="form-actions">
                            <asp:Button ID="btnConfrim"  runat="server" CssClass="btn btn-success" Text="Apply" OnClientClick="return confirm('Are you sure, you want to collect fingerprint from device(s)?');" OnClick="btnConfrim_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
</asp:Content>
