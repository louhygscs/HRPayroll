<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DeviceSave.aspx.cs" Inherits="ERP.Modules.BioMetricDevice.Device.DeviceSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Device</title>
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
            <li>
                <a href="<%=ResolveUrl("~/Modules/BioMetricDevice/Device/DeviceList.aspx") %>" title="Device">Device</a>
            </li>

            <li class="current">Save Device
            </li>
        </ul>

    </div> -->
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			 <h3>Save Device</h3>
		
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
						<span class="wizardList-icon fas fa-mobile"></span> 
						<span class="wizardTitle">
							Device
							<div class="text-muted small">Device</div>
						</span>
					</a>
				</li>
				<li>
					<a href="#" class="activeWizard">
						<span class="wizardList-icon fas fa-save"></span> 
						<span class="wizardTitle">
							Save Device
							<div class="text-muted small">Save Device</div>
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
                        <i class="fa fa-save"></i>
						Save <span>Device</span>
                    </h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group efirst">
									<label class="col-md-2 control-label">
										Device Name <span class="required">*</span>
									</label>
									<div class="col-md-10">
										<asp:TextBox ID="txtDeviceName" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvDeviceName" SetFocusOnError="true" ControlToValidate="txtDeviceName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Device Name." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-2 control-label">
										Device Code <span class="required">*</span>
									</label>
									<div class="col-md-10">
										<asp:TextBox ID="txtDeviceCode" MaxLength="20" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvDeviceCode" SetFocusOnError="true" ControlToValidate="txtDeviceCode" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Device Code." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-2 control-label">
										IP Address<span class="required">*</span>
									</label>
									<div class="col-md-10">
										<asp:TextBox ID="txtIPAddress" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvIPAddress" SetFocusOnError="true" ControlToValidate="txtIPAddress" CssClass="required" Display="Dynamic" ErrorMessage="Please enter IP Address." runat="server"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revIPAddress"
											ControlToValidate="txtIPAddress" runat="server"
											CssClass="required"
											ErrorMessage="Enter Valid IP Address."
											ValidationExpression="\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b">
										</asp:RegularExpressionValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-2 control-label">
										Port<span class="required">*</span>
									</label>
									<div class="col-md-10">
										<asp:TextBox ID="txtPort" MaxLength="10" runat="server"  CssClass="form-control input-width-xlarge" onkeypress="return Common.isNumericKey(event,this)"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvPort" SetFocusOnError="true" ControlToValidate="txtPort" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Port." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-2 control-label">
										Phone No
									</label>
									<div class="col-md-10">
										<asp:TextBox ID="txtPhone" MaxLength="10" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Address<span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtAddress" MaxLength="500" TextMode="MultiLine" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvAddress" SetFocusOnError="true" ControlToValidate="txtAddress" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Address." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
						</div>
                        <div class="form-actions">
                            <a href="/Modules/BioMetricDevice/Device/DeviceList.aspx" class="btn pull-right">Cancel
                            </a>
                            <asp:Button CssClass="btn btn-success pull-right" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />

                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
