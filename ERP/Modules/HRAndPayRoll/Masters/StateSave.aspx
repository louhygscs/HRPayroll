<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="StateSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.StateSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>State</title>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Add State</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>State</span>
                        </h4>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Country <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" CssClass="form-control input-width-xlarge">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCountry" SetFocusOnError="true" ControlToValidate="ddlCountry" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Country." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-12 control-label">
                                State <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtState" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvState" SetFocusOnError="true" ControlToValidate="txtState" CssClass="required" Display="Dynamic" ErrorMessage="Please enter State." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-actions formActionbtn">

                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                            <a href="/Modules/HRAndPayRoll/Masters/StateList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
