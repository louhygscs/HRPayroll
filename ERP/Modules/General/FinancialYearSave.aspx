<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="FinancialYearSave.aspx.cs" Inherits="ERP.Modules.General.FinancialYearSave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Financial Year</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
  
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Save Financial Year</h3>
	
		</div>

    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							 Save <span>Financial Year</span>
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
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                         <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>
								<div class="row">
									<div class="col-md-6 col-sm-6 col-xs-12">
										<div class="form-group efirst">
											<label class="col-md-12 control-label">
												Year <span class="required">*</span>
											</label>
											<div class="col-md-12">
												<asp:DropDownList ID="ddlYear" AutoPostBack="true" CssClass="form-control input-width-xlarge" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
												<asp:RequiredFieldValidator ID="rfvYear" SetFocusOnError="true" ControlToValidate="ddlYear" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Year." runat="server"></asp:RequiredFieldValidator>
											</div>
										</div>
									</div>
									<div class="col-md-6 col-sm-6 col-xs-12">
										<div class="form-group">
											<label class="col-md-12 control-label">
												Financial Year :
											</label>
											<div class="col-md-12 control-label textLeft">
												<asp:Label ID="lblFinancialYear" runat="server" CssClass="lblFinancialYear"></asp:Label>
											</div>
										</div>
									</div>
								</div>
                                
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="form-actions formActionbtn">
                            
                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							<a href="/Modules/General/FinancialYearList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
