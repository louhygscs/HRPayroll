<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="UserSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.User.UserSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>User</title>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Add User</h3>
        </div>
    </div>
    <!-- end BreadCrumbs line -->

    <!-- form -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>User</span>
                        </h4>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />
                        
                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Work Location <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:DropDownList id="drpWorkLocation" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLocation" SetFocusOnError="true" ControlToValidate="drpWorkLocation" CssClass="required" Display="Dynamic" ErrorMessage="Please Select Work Location." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Employee Name <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:DropDownList id="drpEmployee" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="drpEmployee" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Employee." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Role <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:DropDownList id="drpRole" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRole" SetFocusOnError="true" ControlToValidate="drpRole" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Role." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Username <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtUsername" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUsername" SetFocusOnError="true" ControlToValidate="txtUsername" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Country." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Password <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtPassword" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge" type="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" SetFocusOnError="true" ControlToValidate="txtPassword" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Country." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-actions formActionbtn">

                            <asp:Button CssClass="btn btn-success " ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                            <a href="/Modules/HRAndPayRoll/Masters/User/UserList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- end form -->

</asp:Content>
