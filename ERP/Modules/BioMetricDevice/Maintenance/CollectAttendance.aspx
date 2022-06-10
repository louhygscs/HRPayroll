<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CollectAttendance.aspx.cs" Inherits="ERP.Modules.BioMetricDevice.Maintenance.CollectAttendance" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Collect Attendance from Device(s)</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/BioMetricDevice/Maintenance/CollectAttendance.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">


    <!-- /Breadcrumbs line -->

    <div class="page-header">
		<div class="pageHeading">
			<h3>Collect Attendance from Device(s)</h3>
		
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box"> 
                <div class="widget-content noFiltering">
                    <div class="headingOftabel">
                        <h4>
							Collect <span>Attendance from Device(s)</span>
						</h4>
                    </div>
                    <form id="frmMain" runat="server" class="form-horizontal">
                        <div class="form-group efirst">
							<div class="row">
								<div class="col-md-6 col-sm-6 col-xs-12">
									<label class="col-md-12 control-label" style="text-align: left;">
										Date <span class="required">*</span>
									</label>
									<div class="col-md-12 date-select">
										<input type="text" runat="server" readonly="" id="txtDate" class="form-control input-width-xlarge txtDate" />
									</div>
								</div> 
							</div> 
                        </div> 
                        <div class="form-actions">
                        <asp:Button ID="btnCollect" runat="server" CssClass="btnCollect btn btn-success" Text="Collect" OnClientClick="return confirm('Are you sure, you want to Collect Attendance from Device(s)?');" OnClick="btnCollect_Click" />
                            </div>

                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
