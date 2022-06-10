<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeDetail.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Reports.EmployeeDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Detail Report</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Reports/EmployeeDetail.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <!--<div class="crumbs">
        <ul id="breadcrumbs" class="breadcrumb">
            <li>
                <a href="<%=ResolveUrl("~/Modules/Main.aspx") %>">
                    <i class="fa fa-home"></i>
                    Dashboard
                </a>
            </li>
            <li class="current">
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Reports/EmployeeDetail.aspx") %>" title="Employee Detail Report">Employee Detail Report</a>
            </li>
        </ul>
    </div> -->
      <div class="page-header">
		<div class="pageHeading">
			<h3>Employee Detail Report</h3>
		
			
		</div>
        
    </div>
	<div class="row">
        <div class="col-md-12">
            <div class="widget box">
				
				<form id="frmMain" runat="server" class="form-horizontal row-border">
					<script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
					<asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
					<div class="reportContent">
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group efirst">
									<label class="col-md-12 control-label">
										Employee Type :
									</label>
									<div class="col-md-12">
										<asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control input-width-xlarge">
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Status :
									</label>
									<div class="col-md-12">
										<label class="radio-inline">
											<asp:RadioButton ID="rbtnAll" runat="server" Text="All" GroupName="Status" Checked="true" />
										</label>
										<label class="radio-inline">
											<asp:RadioButton ID="rbtnPresent" runat="server" Text="Present" GroupName="Status" />
										</label>
										<label class="radio-inline">
											<asp:RadioButton ID="rbtnResign" runat="server" Text="Resign" GroupName="Status" />
										</label>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Join Date :
									</label>
									<div class="col-md-12 date-select">
										<input type="text" runat="server" readonly="" id="txtDateRange" class="form-control input-width-xlarge txtDateRange" />
									</div>
								</div>
							</div>
							
						</div>
						<div class="row">
							<div class="col-md-12">
								<asp:UpdatePanel ID="upMain" runat="server">
									<ContentTemplate>
										<div class="form-group">
											<!--<label class="col-md-12 control-label">
											</label>-->
											<div class="col-md-12">
												<asp:Button CssClass="btn btn-success" ID="btnGenerateReport" OnClick="btnGenerateReport_Click" runat="server" Text="Generate Report" />

											</div>
										</div>

										<div class="col-md-12">
											<rsweb:ReportViewer ID="rvReportDetail" runat="server" Width="100%" Height="600px"></rsweb:ReportViewer>
										</div>

									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
						</div>
					</div>
					

				</form>
			</div>
		</div>
	</div>
</asp:Content>
