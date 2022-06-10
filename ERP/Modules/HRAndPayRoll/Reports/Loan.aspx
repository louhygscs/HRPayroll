<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="Loan.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Reports.Loan" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Loan Report</title>
    <link href="<%=ResolveUrl("~/Styles/fSelect.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/fSelect.js")%>"></script>
    <link href="<%=ResolveUrl("~/Styles/multiselect.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/multiselect.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Reports/Loan.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    
    <div class="page-header">
		<div class="pageHeading">
			<h3>Loan Report</h3>
		
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
									<label class="col-md-12 control-label ">
										Department<span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control input-width-xlarge" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
										<asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="" SetFocusOnError="true" ControlToValidate="ddlDepartment" CssClass="required" Display="Dynamic" ErrorMessage="Please select Department." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label ">
										Employee Name<span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:ListBox SelectionMode="Multiple" ID="lbEmployees" runat="server" CssClass="lbEmployees"></asp:ListBox>
										<asp:RequiredFieldValidator ID="rfvEmployees" runat="server" SetFocusOnError="true" InitialValue=""
											ControlToValidate="lbEmployees" ErrorMessage="Please select atleast one employee." Display="Dynamic" CssClass="required"
											Text="Please select atleast one employee."> 
										</asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
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
											<asp:RadioButton ID="rbtnCompleted" runat="server" Text="Completed" GroupName="Status" />
										</label>
										<label class="radio-inline">
											<asp:RadioButton ID="rbtnPending" runat="server" Text="Pending" GroupName="Status" />
										</label>
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
												<asp:Button CssClass="btn btn-success" ID="btnGenerateReport" OnClick="btnGenerateReport_Click" runat="server" Text="Generate Report" />

											</div>
										</div>
										<div class="col-md-12">
											<rsweb:reportviewer id="rvReportDetail" visible="true" runat="server" width="100%" height="600px"></rsweb:reportviewer>
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
