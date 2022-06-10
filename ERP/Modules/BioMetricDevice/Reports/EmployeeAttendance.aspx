<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendance.aspx.cs" Inherits="ERP.Modules.BioMetricDevice.Reports.EmployeeAttendance" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Attendance</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/BioMetricDevice/Reports/EmployeeAttendance.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
  

    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee Attendance</h3>
		
		</div>
        
    </div>
	<div class="row">
        <div class="col-md-12">
            <div class="widget box">
				<form id="frmMain" runat="server" class="form-horizontal">
					<asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
					<div class="reportContent">
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group efirst">
									<label class="col-md-12 control-label ">
										Employee Name
									</label>
									<div class="col-md-12">
										<asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control input-width-xlarge">
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Device Name :
									</label>
									<div class="col-md-12">
										<asp:DropDownList ID="ddlDevice" runat="server" CssClass="form-control input-width-xlarge">
										</asp:DropDownList>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<!--<div class="col-md-2"></div>-->
									<div class="col-md-12">
										<label class="radio-inline">
											<asp:RadioButton ID="rbtnDaily" runat="server" Text="Daily" GroupName="Date" Checked="true" AutoPostBack="true" OnCheckedChanged="rdDate_SelectedIndexChanged" />
										</label>
										<label class="radio-inline">
											<asp:RadioButton ID="rbtnWeekly" runat="server" Text="Weekly" GroupName="Date" AutoPostBack="true" OnCheckedChanged="rdDate_SelectedIndexChanged" />
										</label>
										<label class="radio-inline">
											<asp:RadioButton ID="rbtnMonthly" runat="server" Text="Monthly" GroupName="Date" AutoPostBack="true" OnCheckedChanged="rdDate_SelectedIndexChanged" />
										</label>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<!--<div class="col-md-2"></div>-->
									<div class="col-md-12">
										<input type="text" runat="server" readonly="" id="txtDate" class="form-control input-width-xlarge txtDate" />
										<input type="text" runat="server" readonly="" id="txtFromToDate" class="form-control input-width-xlarge txtFromToDate" />
										<span>
											<asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control input-width-xlarge">
											</asp:DropDownList></span>
										<span>
											<asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control input-width-xlarge">
											</asp:DropDownList></span>
									</div>
								</div>
							</div>
						</div>
					
						<div class="row">
							<div class="col-md-12">
								<asp:UpdatePanel ID="upMain" runat="server">
									<ContentTemplate>
										<div class="form-group">
											<!--<label class="col-md-2 control-label">
											</label> -->
											<div class="col-md-12">
												<asp:Button CssClass="btn btn-success" ID="btnGenerateReport" runat="server" Text="Generate Report" OnClick="btnGenerateReport_Click" />
											</div>
										</div>
										<div class="col-md-12">
											<rsweb:ReportViewer ID="rvReportDetail" Visible="true" runat="server" Width="100%" Height="600px"></rsweb:ReportViewer>
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
