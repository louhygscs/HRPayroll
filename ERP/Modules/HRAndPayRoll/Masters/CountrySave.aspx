<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CountrySave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.CountrySave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Country</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->

    <div class="page-header">
        <div class="pageHeading">
            <h3>Add Education</h3>
        </div>
    </div>
    <!-- end BreadCrumbs line -->

    <!-- form -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>Country</span>
                        </h4>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />
                        
                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Code <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtCode" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCode" SetFocusOnError="true" ControlToValidate="txtCode" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Code." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Country <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtCountryName" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCountry" SetFocusOnError="true" ControlToValidate="txtCountryName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Country." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-actions formActionbtn">

                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                            <a href="/Modules/HRAndPayRoll/Masters/CountryList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- end form -->

</asp:Content>
