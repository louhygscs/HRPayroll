<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="GlobalSetting.aspx.cs" Inherits="ERP.Modules.General.GlobalSetting" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Global Setting</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/General/CompanySave.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
  
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Save Global Setting</h3>
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Global <span>Setting</span>
						</h4>
					</div>
                </div>
                <div class="widget-content companyFomrs">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:HiddenField ID="hfId" runat="server" />
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group efirst">
									<label class="col-md-12 control-label">
										LateTime(Minutes) <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtLateTime" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
									</div>
								</div>
							</div>
						</div>
                        <div class="form-actions formActionbtn">
                            <asp:Button CssClass="btn btn-success" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							 <a href="/Modules/Main.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
