<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CatergorySave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.Dimension.CatergorySave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Category</title>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Add Category</h3>
        </div>
    </div>
    <!-- end BreadCrumbs line -->

    <!-- Save Form -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>Category</span>
                        </h4>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />
                        
                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Entry Table <span class="required">*</span>
                            </label>
                            <div class="col-md-12">                                                               
                                <asp:DropDownList ID="drpEntryTable" name="drpEntryTable" runat="server" CssClass="form-control input-width-xlarge"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Category <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtCategoryName" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSymbol" SetFocusOnError="true" ControlToValidate="txtCategoryName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Category Name." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-actions formActionbtn">
                            <asp:Button CssClass="btn btn-success " ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                            <a href="/Modules/HRAndPayRoll/Masters/Dimension/CategoryList.aspx" class="btn">Cancel</a>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>
    <!-- End Save Form -->

</asp:Content>
