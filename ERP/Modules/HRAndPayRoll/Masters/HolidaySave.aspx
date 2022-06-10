<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="HolidaySave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.HolidaySave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Holiday</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/HolidaySave.js")%>"></script>

</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			 <h3>Add Holiday</h3>
		
			
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Save <span> Holiday</span>
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
                        <asp:HiddenField ID="hfId" runat="server" />
						
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group efirst">
									<label class="col-md-12 control-label">
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
									<label class="col-md-12 control-label">
										From Date - To Date
									</label>
									<div class="col-md-12 date-select">
										<input type="text" runat="server" readonly="" id="txtDateRange" class="form-control input-width-xlarge txtDateRange" />
										<%--<i class="glyphicon glyphicon-calendar fa fa-calendar"></i>--%>
									</div>
								</div>
							</div>
						</div>
                        <div class="row">
							<div class="col-md-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Description 
									</label>
									<div class="col-md-12">
										<asp:TextBox TextMode="MultiLine" ID="txtDescription" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
									</div>
								</div>
							</div>
						</div>
                        
                        


                        <div class="form-actions formActionbtn">
                           
                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							 <a href="/Modules/HRAndPayRoll/Masters/HolidayList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>

</asp:Content>

