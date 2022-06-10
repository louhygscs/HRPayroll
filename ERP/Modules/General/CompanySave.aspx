<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CompanySave.aspx.cs" Inherits="ERP.Modules.General.CompanySave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Company Details</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/General/CompanySave.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
  
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Save Company</h3>
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Company <span>Information</span>
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
										Company Name <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtCompanyName" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvCompanyName" SetFocusOnError="true" ControlToValidate="txtCompanyName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Company Name." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
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
						</div>
						<div class="row">
							<div class="col-md-12">
								<asp:UpdatePanel ID="upMain" runat="server">
									<ContentTemplate>
									<div class="row">
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Country <span class="required">*</span>
												</label>
												<div class="col-md-12">
													<asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control input-width-xlarge">
													</asp:DropDownList>
													<asp:RequiredFieldValidator ID="rfvCountry" SetFocusOnError="true" ControlToValidate="ddlCountry" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Country." runat="server"></asp:RequiredFieldValidator>
												</div>
											</div>
										</div>
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													State <span class="required">*</span>
												</label>
												<div class="col-md-12">
													<asp:DropDownList ID="ddlState" runat="server" CssClass="form-control input-width-xlarge">
													</asp:DropDownList>
													<asp:RequiredFieldValidator ID="rfvState" SetFocusOnError="true" ControlToValidate="ddlState" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select State." runat="server"></asp:RequiredFieldValidator>
												</div>
											</div>
										</div>
										
									</div>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
							
							
						</div>
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										City <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtCity" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvCity" SetFocusOnError="true" ControlToValidate="txtCity" CssClass="required" Display="Dynamic" ErrorMessage="Please enter City." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Mobile <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtMobile" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvMobile" SetFocusOnError="true" ControlToValidate="txtMobile" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Mobile." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							
						</div>
						<div class="row">
							
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Phone <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtPhone" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvPhone" SetFocusOnError="true" ControlToValidate="txtPhone" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Phone Number." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Hotline No. <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtHotlineNo" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvHotlineNo" SetFocusOnError="true" ControlToValidate="txtHotlineNo" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Hotline Number." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							
						</div>
						<div class="row">
							
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Fax No. <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtFaxNo" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvFaxNo" SetFocusOnError="true" ControlToValidate="txtFaxNo" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Fax Number." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Website <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtWebSite" MaxLength="500" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvWebSite" SetFocusOnError="true" ControlToValidate="txtWebSite" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Website." runat="server"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revWebSite" runat="server" ErrorMessage="Please enter valid Website." ControlToValidate="txtWebSite" SetFocusOnError="true" Display="Dynamic" CssClass="required"
											ValidationExpression="(https?:\/\/(?:www\.|(?!www))[^\s\.]+\.[^\s]{2,}|www\.[^\s]+\.[^\s]{2,})">
										</asp:RegularExpressionValidator>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Logo <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<div id="divUploadLogo" runat="server" class="divUploadLogo" style="display: block;">
											<asp:FileUpload ID="fuLogo" runat="server" />
											<asp:RegularExpressionValidator ID="revLogo"
												runat="server" ControlToValidate="fuLogo"
												ErrorMessage="Please select correct logo extension file."
												ValidationExpression="^([0-9a-zA-Z :\\-_!@$%^&*()])+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.JPG|.jpg|.bitmap|.BITMAP)$"
												Display="Dynamic" CssClass="required"></asp:RegularExpressionValidator>
										</div>
										<div id="divViewLogo" runat="server" class="divViewLogo" style="display: none;">
											<asp:HiddenField ID="hfLogo" runat="server" Value="" />
											<img id="imgLogo" runat="server" alt="Logo" class="viewImage" />
											<!--<a href="javascript:;" id="btnDeleteLogo" class="btn btn-sm btnDeleteLogo"><i class="fa fa-trash" title="Click to delete logo."></i>&nbsp;Delete</a> -->
											<a href="javascript:;" id="btnDeleteLogo" class="btn btn-sm btnDeleteLogo"><i class="fa fa-trash" title="Click to delete logo."></i></a>
										</div>
									</div>
								</div>
							</div>
                            <%--<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 col-sm-12 col-xs-12 control-label">
										License Generate Key
									</label>
									
									<div class="col-md-12 col-sm-12 col-xs-12">
										<asp:TextBox ID="txtKey" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
									</div>
									 <div class="col-md-12 col-sm-12 col-xs-12">
										 <asp:Button runat="server" ID="btnGetKey" Text="Get License Key" CssClass="btn btn-success getKeyBtn" OnClick="btnGetKey_Click" />
										 <asp:Button runat="server" ID="btnActivateKey" Text="Activate Key" CssClass="btn btn-success activeKey" OnClick="btnActivateKey_Click" />
									 </div>
								</div>
							</div>--%>
						</div>
						<div class="row">
                                <div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Address <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox TextMode="MultiLine" ID="txtAddress" MaxLength="1000" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvAddress" SetFocusOnError="true" ControlToValidate="txtAddress" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Address." runat="server"></asp:RequiredFieldValidator>
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
