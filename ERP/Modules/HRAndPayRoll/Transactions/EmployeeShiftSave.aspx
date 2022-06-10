<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeShiftSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeShiftSave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Shift</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="crumbs">
        <ul id="breadcrumbs" class="breadcrumb">
            <li>
                <a href="<%=ResolveUrl("~/Modules/Main.aspx") %>">
                    <i class="fa fa-home"></i>
                    Dashboard
                </a>
            </li>
            <li>
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Transactions/EmployeeShiftList.aspx") %>" title="Employee Shift">Employee Shift</a>
            </li>

            <li class="current">Update Employee Shift
            </li>
        </ul>

    </div>
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <h3>Update Employee Shift</h3>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <h4>
                        <i class="fa fa-reorder"></i>
                        <span>Update Employee Shift</span>
                    </h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfEmployeeId" runat="server" />

                        <div class="form-group efirst">
                            <label class="col-md-2 control-label">
                                Department :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Employee Name :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Shift <span class="required">*</span>
                            </label>
                            <div class="col-md-10">
                                <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control input-width-xlarge">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvShift" SetFocusOnError="true" ControlToValidate="ddlShift" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Shift." runat="server"></asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="form-actions">
                            <a href="/Modules/HRAndPayRoll/Transactions/EmployeeShiftList.aspx" class="btn pull-right">Cancel</a>
                            <asp:Button CssClass="btn btn-success pull-right" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
