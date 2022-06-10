<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ERP.Modules.Profile" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Profile</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/Profile.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   <!-- <div class="crumbs">
        <ul id="breadcrumbs" class="breadcrumb">
            <li>
                <a href="<%=ResolveUrl("~/Modules/Main.aspx") %>">
                    <i class="fa fa-home"></i>
                    Dashboard
                </a>
            </li>

            <li class="current">Profile
            </li>
        </ul>

    </div> -->
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			 <h3>Profile</h3>
		
			<div class="signoutBtn">
				<button class="btn">
					<i class="ion ion-md-power text-danger"></i>
					Sign out
				</button>
			</div>
		</div>
		<div class="wizardList">
			<ul>
				<li>
					<a href="#" class="">
						<span class="wizardList-icon ion ion-md-speedometer"></span> 
						<span class="wizardTitle">
							Dashboard
							<div class="text-muted small">Dashboard</div>
						</span>
					</a>
				</li>
				<li>
					<a href="#" class="activeWizard">
						<span class="wizardList-icon fa fa-user-circle"></span> 
						<span class="wizardTitle">
							Profile
							<div class="text-muted small">Profile</div>
						</span>
					</a>
				</li>
				
			</ul>
		</div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <h4>
                        <i class="fa fa-list"></i>
                        <span>Profile</span>
                    </h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <div class="col-md-6">
                            <div class="widget box">
                                <div class="widget-header">
                                    <h4><i class="fas fa-user-edit"></i>Personal Details </h4>
                                </div>
                                <div class="widget-content">
                                    <div class="form-group efirst">
                                        <label class="col-md-12 control-label">
                                            First Name <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtFirstName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFirstName" SetFocusOnError="true" ControlToValidate="txtFirstName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter First Name." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Middle Name 
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtMiddleName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Last Name <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtLastName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastName" SetFocusOnError="true" ControlToValidate="txtLastName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Last Name." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Father's Name <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtFatherName" MaxLength="150" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFatherName" SetFocusOnError="true" ControlToValidate="txtFatherName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Father's Name." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Date of Birth <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12 date-select">
                                            <input type="text" runat="server" readonly="" id="txtBirthDate" class="form-control input-width-xlarge txtBirthDate" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Gender <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtnMale" runat="server" Text="Male" GroupName="Gender" Checked="true" />
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rbtnFeMale" runat="server" Text="FeMale" GroupName="Gender" />
                                            </label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Maratial Status <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlMaratialStatus" runat="server" CssClass="form-control input-width-xlarge">
                                                <asp:ListItem Value="">-- Select --</asp:ListItem>
                                                <asp:ListItem Value="Married">Married</asp:ListItem>
                                                <asp:ListItem Value="Un-Married">Un-Married</asp:ListItem>
                                                <asp:ListItem Value="Widowed">Widowed</asp:ListItem>
                                                <asp:ListItem Value="Divorced">Divorced</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMaratialStatus" SetFocusOnError="true" ControlToValidate="ddlMaratialStatus" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Maratial Status." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Cast
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtCast" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Photo <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <div id="divUploadPhoto" runat="server" class="divUploadPhoto" style="display: block;">
                                                <asp:FileUpload ID="fuPhoto" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvPhoto"
                                                    ControlToValidate="fuPhoto" SetFocusOnError="true" runat="server" ErrorMessage="Please select Photo." Display="Dynamic" CssClass="required rfvPhoto"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revPhoto"
                                                    runat="server" ControlToValidate="fuPhoto"
                                                    ErrorMessage="Please select correct photo extension file."
                                                    ValidationExpression="^([0-9a-zA-Z :\\-_!@$%^&*()])+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.JPG|.jpg|.bitmap|.BITMAP)$"
                                                    Display="Dynamic" CssClass="required"></asp:RegularExpressionValidator>
                                            </div>
                                            <div id="divViewPhoto" runat="server" class="divViewPhoto" style="display: none;">
                                                <asp:HiddenField ID="hfPhoto" runat="server" />
                                                <img id="imgPhoto" runat="server" alt="Photo" class="viewImage" />
                                                <a href="javascript:;" id="btnDeletePhoto" class="btn btn-sm btnDeletePhoto"><i class="fa fa-trash" title="Click to delete photo."></i>&nbsp;Delete</a>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="widget box">
                                <div class="widget-header">
                                    <h4><i class="fas fa-info-circle"></i>Contact Details </h4>
                                </div>
                                <div class="widget-content">
                                    <asp:UpdatePanel ID="upMain" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group efirst">
                                                <label class="col-md-12 control-label">
                                                    Country <span class="required">*</span>
                                                </label>
                                                <div class="col-md-12">
                                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control input-width-xlarge">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCountry" SetFocusOnError="true" ControlToValidate="ddlCountry" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Country." runat="server"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            City <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtCity" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCity" SetFocusOnError="true" ControlToValidate="txtCity" CssClass="required" Display="Dynamic" ErrorMessage="Please enter City." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Address <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox TextMode="MultiLine" ID="txtAddress" MaxLength="1000" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress" SetFocusOnError="true" ControlToValidate="txtAddress" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Address." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Pin Code <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtPinCode" MaxLength="10" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPinCode" SetFocusOnError="true" ControlToValidate="txtPinCode" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Pin Code." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Mobile <span class="required">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtMobile" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMobile" SetFocusOnError="true" ControlToValidate="txtMobile" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Mobile." runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-12 control-label">
                                            Phone
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtPhone" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="form-actions">
                            <a href="/Modules/Main.aspx" class="btn pull-right">Cancel
                            </a>
                            <asp:Button CssClass="btn btn-success pull-right" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
