<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DepartmentSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.DepartmentSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Department</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->

    <div class="page-header">
        <div class="pageHeading">
            <h3>Add Department</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>Department</span>
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
                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Department <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtDepartment" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="txtDepartment" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Department." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-actions formActionbtn">

                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                            <a href="/Modules/HRAndPayRoll/Masters/DepartmentList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
