<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeLoanSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeLoanSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Loan</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeLoanSave.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Add Employee Loan</h3>
		
			
		</div>
       
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Save <span>Employee Loan</span>
						</h4>
					</div>
                    
                   <!-- <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div> -->
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:HiddenField ID="hfId" runat="server" />
                       
                            <asp:UpdatePanel ID="upMain" runat="server">
                                <ContentTemplate>
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
											<label class="col-md-12 control-label paddingTop0">
												Employee<span class="required">*</span>
											</label>
											<div class="col-md-12">
												<asp:DropDownList ID="ddlEmployee" CssClass="form-control input-width-xlarge" runat="server"></asp:DropDownList>
												<asp:RequiredFieldValidator ID="rfvEmployee" InitialValue="" SetFocusOnError="true" ControlToValidate="ddlEmployee" CssClass="required" Display="Dynamic" ErrorMessage="Please select Employee." runat="server"></asp:RequiredFieldValidator>
											</div>
										</div>
									</div>
								</div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
							
							<div class="row">
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label paddingTop0"">
											Title <span class="required">*</span>
										</label>
										<div class="col-md-12">
											<asp:TextBox ID="txtTitle" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvTitle" SetFocusOnError="true" ControlToValidate="txtTitle" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Title." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label paddingTop0">
											Loan Date<span class="required">*</span>
										</label>
										<div class="col-md-12 date-select">
											 <input type="text" runat="server" readonly="" id="txtLoanDate" class="form-control input-width-xlarge txtLoanDate" />
											<asp:RequiredFieldValidator ID="rfvLoanDate" SetFocusOnError="true" ControlToValidate="txtLoanDate" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Loan Date." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label paddingTop0">
											Amount<span class="required">*</span>
										</label>
										<div class="col-md-12">
											<asp:TextBox ID="txtAmount" runat="server" MaxLength="12" onkeyup="EmployeeLoanSave.CalculateInstallment()" onkeypress="return Common.isNumericKey(event,this);" CssClass="form-control input-width-xlarge txtAmount"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvAmount" SetFocusOnError="true" ControlToValidate="txtAmount" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Amount." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label paddingTop0">
											Total Months<span class="required">*</span>
										</label>
										<div class="col-md-12">
											<asp:TextBox ID="txtTotalMonths" MaxLength="3" onkeyup="EmployeeLoanSave.CalculateInstallment()" onkeypress="return Common.isNumberKey(event)" runat="server" CssClass="form-control input-width-xlarge txtTotalMonths"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvTotalMonths" SetFocusOnError="true" ControlToValidate="txtTotalMonths" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Total Months." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label paddingTop0">
											Installment / Month
										</label>
										<div class="col-md-12">
											<div id="lblInstallment" runat="server" class="lblInstallment"></div>
										</div>
									</div>
								</div>
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label paddingTop0">
											Approved By<span class="required">*</span>
										</label>
										<div class="col-md-12">
											<asp:TextBox ID="txtApprovedBy" MaxLength="150" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvApprovedBy" SetFocusOnError="true" ControlToValidate="txtApprovedBy" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Approved By." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
							</div>
							<div class="form-group">
								<label class="col-md-12 control-label paddingTop0">
									Description
								</label>
								<div class="col-md-12">
									<asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
								</div>
							</div>

                        <div class="form-actions formActionbtn">
                            
                            <asp:Button CssClass="btn btn-success" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							<a href="/Modules/HRAndPayRoll/Transactions/EmployeeLoanList.aspx" class="btn ">Cancel
                            </a>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>

</asp:Content>


