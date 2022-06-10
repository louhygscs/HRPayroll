<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="SalarySlip.aspx.cs" Inherits="ERP.Modules.General.SalarySlip" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Salary Slip</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
   
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        
		<div class="pageHeading">
			<h3>Employee Salary Slip</h3>
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Employee <span>Salary Slip</span>
						</h4>
					</div>
                    
                    <!--<div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>-->
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>

                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
                                        Month<span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control input-width-xlarge" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMonth" InitialValue="" SetFocusOnError="true" ControlToValidate="ddlMonth" CssClass="required" Display="Dynamic" ErrorMessage="Please select Month." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                     <rsweb:ReportViewer ID="rvReportDetail" runat="server" Width="100%" Height="600px"></rsweb:ReportViewer>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
