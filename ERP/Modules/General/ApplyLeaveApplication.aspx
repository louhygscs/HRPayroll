<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="ApplyLeaveApplication.aspx.cs" Inherits="ERP.Modules.General.ApplyLeaveApplication" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Leave Application</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/General/ApplyLeaveApplication.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        
		<div class="pageHeading">
			<h3>Apply Leave Application</h3>
		
		</div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Apply <span>Leave Application</span>
						</h4>
					</div>
               
                    <!--<div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div> -->
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:HiddenField ID="hfStatus" runat="server" Value="" />
                         <asp:HiddenField ID="hfReply" runat="server" Value="" />
						<div class="row"> 
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group efirst">
										<label class="col-md-12 control-label">
											Employee Name :
										</label>
										<div class="col-md-12 control-label textLeft">
											<label runat="server" id="lblEmployeeName"></label>
										</div>
									</div>
								</div>
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label">
											Leaves :
										</label>
										<div class="col-md-12 control-label textLeft">
											<label runat="server" id="lblLeaves" >0</label>
										</div>
									</div>
								</div>
							</div>
							<div class="row"> 
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label">
											Leave Category <span class="required">*</span>
										</label>
										<div class="col-md-12">
											<asp:DropDownList ID="ddlLeaveCategory" runat="server" CssClass="form-control input-width-xlarge">
											</asp:DropDownList>
											<asp:RequiredFieldValidator ID="rfvLeaveCategory" SetFocusOnError="true" ControlToValidate="ddlLeaveCategory" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Leave Category." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label">
											Start Date - End Date
										</label>
										<div class="col-md-12 date-select">
											<input type="text" runat="server" readonly="" id="txtDateRange" class="form-control input-width-xlarge txtDateRange" />
											<%--<i class="glyphicon glyphicon-calendar fa fa-calendar"></i>--%>
										</div>
									</div>
								</div>
							</div>
							<div class="row"> 
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label">
											Consider Half Leave
										</label>
										<div class="col-md-12">
											<label class="checkbox-inline">
												<asp:CheckBox ID="chkStartDateHalfLeave" runat="server" Text="Start Date Half Leave" />
											</label>
											<label class="checkbox-inline">
												<asp:CheckBox ID="chkEndDateHalfLeave" runat="server" Text="End Date Half Leave" />
											</label>
										</div>
									</div>
								</div>
								<div class="col-md-6 col-sm-6 col-xs-12">
									<div class="form-group">
										<label class="col-md-12 control-label">
											Total Leave <span class="required">*</span>
										</label>
										<div class="col-md-12">
											<asp:TextBox ID="txtTotalLeave" MaxLength="6" runat="server" CssClass="form-control input-width-xlarge txtBasic" onkeypress="return Common.isNumericKey(event,this)"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvTotalLeave" SetFocusOnError="true" ControlToValidate="txtTotalLeave" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Total Leave." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
							</div>
							<div class="row"> 
								<div class="col-md-6">
									<div class="form-group">
										<label class="col-md-12 control-label">
											Reason <span class="required">*</span>
										</label>
										<div class="col-md-12">
											<asp:TextBox ID="txtReason" MaxLength="1000" TextMode="MultiLine" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
											<asp:RequiredFieldValidator ID="rfvReason" SetFocusOnError="true" ControlToValidate="txtReason" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Reason." runat="server"></asp:RequiredFieldValidator>
										</div>
									</div>
								</div>
						
							</div>
						<div class="form-actions formActionbtn">
                           
                            <asp:Button CssClass="btn btn-success" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
							 <a href="/Modules/General/LeaveApplicationList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
