<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true"
    CodeBehind="EmployeeTypeSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeTypeSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Type</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Add Employee Type</h3>
		
	
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Save <span>Employee Type</span>
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
										Employee Type <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtEmployeeType" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvEmployeeType" SetFocusOnError="true" ControlToValidate="txtEmployeeType" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Employee Type." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										Leave / Month <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:DropDownList ID="ddlLeave" runat="server" CssClass="form-control input-width-xlarge">
											<asp:ListItem Value="0.5">0.5</asp:ListItem>
											<asp:ListItem Value="1.0">1.0</asp:ListItem>
											<asp:ListItem Value="1.5">1.5</asp:ListItem>
											<asp:ListItem Value="2.0">2.0</asp:ListItem>
											<asp:ListItem Value="2.5">2.5</asp:ListItem>
											<asp:ListItem Value="3.0">3.0</asp:ListItem>
											<asp:ListItem Value="3.5">3.5</asp:ListItem>
											<asp:ListItem Value="4.0">4.0</asp:ListItem>
											<asp:ListItem Value="4.5">4.5</asp:ListItem>
											<asp:ListItem Value="5.0">5.0</asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
							</div>
						</div>
						
                        
                        
                        <div class="form-actions formActionbtn">
                            
                            <asp:Button CssClass="btn btn-success" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							<a href="/Modules/HRAndPayRoll/Masters/EmployeeTypeList.aspx" class="btn ">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
