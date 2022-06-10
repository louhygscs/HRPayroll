<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="ERP.Modules.ChangePassword" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Change Password</title>
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
            <li class="current">
                <a href="<%=ResolveUrl("~/Modules/ChangePassword.aspx") %>" title="Change Password">Change Password</a>
            </li>
        </ul>

    </div> -->
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Change Password</h3>

            <div class="signoutBtn">
                <button class="btn">
                    <i class="ion ion-md-power text-danger"></i>
                    Sign out
                </button>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">

                <div class="widget-content noFiltering">
                    <div class="headingOftabel">
                        <h4>
                            <span>Change Password</span>
                        </h4>
                    </div>
                    <form id="frmChangePassword" runat="server" class="form-horizontal">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
                                        Old Password <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtOldPassword" MaxLength="20" TextMode="Password" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOldPassword" SetFocusOnError="true" ControlToValidate="txtOldPassword" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Old Password." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        New Password <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtNewPassword" MaxLength="20" TextMode="Password" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNewPassword" SetFocusOnError="true" ControlToValidate="txtNewPassword" CssClass="required" Display="Dynamic" ErrorMessage="Please enter New Password." runat="server"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revNewPassword" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{8,20}$" ControlToValidate="txtNewPassword" CssClass="required" Display="Dynamic" ErrorMessage="New Password should be alphanumeric characters with minimum of 8 characters and maximum of 20 characters." runat="server"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Confirm Password <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtConfirmPassword" MaxLength="20" TextMode="Password" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        <asp:CompareValidator ID="cvConfirmPassword" SetFocusOnError="true" ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" CssClass="required" Display="Dynamic" ErrorMessage="New Password and confirm password do not match." runat="server"></asp:CompareValidator>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="form-actions">
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
