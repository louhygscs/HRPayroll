<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="LicenseGenerateSave.aspx.cs" Inherits="ERP.Modules.General.LicenseGenerateSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>License Key Generate</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
 
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Save License Generate Key</h3>
		
			
		</div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Save <span>License Generate Key</span>
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
                        <div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group efirst">
									<label class="col-md-12 control-label">
										Email <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtEmail" MaxLength="200" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvEmail" SetFocusOnError="true" ControlToValidate="txtEmail" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Email." runat="server"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please enter valid Email." ControlToValidate="txtEmail" SetFocusOnError="true" Display="Dynamic" CssClass="required" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
										</asp:RegularExpressionValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group ">
									<label class="col-md-12 control-label">
										License Key
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtKey" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge" ReadOnly="true"></asp:TextBox>
									</div>
								</div>
							</div>
						</div>
						
                        
                        <div class="clearfix"></div>
                        <div class="form-actions formActionbtn">
							<asp:Button CssClass="btn btn-success" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							<a href="/Modules/General/LicenseGenerateList.aspx" class="btn ">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
