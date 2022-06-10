<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="AllowanceSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.AllowanceSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Allowance</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Add Allowance</h3>
		
		</div>
      
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Save <span>Allowance</span>
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
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group efirst">
									<label class="col-md-12 control-label">
										Allowance <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtAllowance" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>

										<asp:RequiredFieldValidator ID="rfvAllowance" SetFocusOnError="true" ControlToValidate="txtAllowance" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Allowance." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										IsConsider
									</label>
									<div class="col-md-12">
										<label class="checkbox">
										<asp:CheckBox ID="chkIsConsider" runat="server"/>  <span class="InfoLabel">(This allowance value will be use Leave or OT Calculation)</span> 
										</label>
									</div>
								</div>
							</div>
						</div> 
                        <div class="form-actions formActionbtn">
                            
                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							<a href="/Modules/HRAndPayRoll/Masters/AllowanceList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>

</asp:Content>

