<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="GenerateCutOff.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.GenerateCutOff" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Generate CutOff Period</title>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    
    <!-- Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Generate CutOff Period</h3>
        </div>
    </div>
    <!-- end BreadCrumbs line -->

    <!-- Save Form -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <!-- widget header -->
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>CutOff Period</span></h4>
                    </div>
                </div>
                <!-- end widget header -->

                <!-- widget content -->
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">

                        <div class="form-group efirst">
                            
                            <div class="col-md-4">
                                <label class="col-md-12 control-label">
                                    Start Date <span class="required">*</span>
                                </label>
                                <asp:TextBox ID="txtStartDate" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge" TextMode="Date"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvStartDate" SetFocusOnError="true" ControlToValidate="txtStartDate" CssClass="required" Display="Dynamic" ErrorMessage="Please enter valid Date." runat="server"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4">
                                <label class="col-md-12 control-label">
                                    End Date <span class="required">*</span>
                                </label>
                                <asp:TextBox ID="txtEndDate" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge" TextMode="Date"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEndDate" SetFocusOnError="true" ControlToValidate="txtEndDate" CssClass="required" Display="Dynamic" ErrorMessage="Please enter valid Date" runat="server"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="form-group efirst">
                            <div class="col-md-4">
                                <label class="col-md-12 control-label">
                                    CutOff First Day<span class="required">*</span>
                                </label>
                                <asp:DropDownList ID="drpCutOffFirstDay" runat="server" CssClass="form-control input-width-xlarge"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvCutOffFirstDay" SetFocusOnError="true" ControlToValidate="drpCutOffFirstDay" CssClass="required" Display="Dynamic" ErrorMessage="Please select a Day" runat="server"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-md-4">
                                <label class="col-md-12 control-label">
                                    CutOff Second Day <span class="required">*</span>
                                </label>
                                <asp:DropDownList ID="drpCutOffSecondDay" runat="server" CssClass="form-control input-width-xlarge"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvCutOffSecondDay" SetFocusOnError="true" ControlToValidate="drpCutOffSecondDay" CssClass="required" Display="Dynamic" ErrorMessage="Please select a Day" runat="server"></asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="form-group efirst">
                            <label class="col-md-12 control-label">
                                Remarks <span class="required">*</span>
                            </label>
                            <div class="col-md-12">
                                <asp:TextBox ID="txtRemarks" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-actions formActionbtn">
                            <asp:Button CssClass="btn btn-success " ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                            <a href="/Modules/HRAndPayRoll/Masters/CutOff/CutOffList.aspx" class="btn">Cancel</a>
                        </div>

                    </form>
                </div>
                <!-- end widget content -->
            </div>
        </div>
    </div>
    <!-- End Save Form -->


</asp:Content>
