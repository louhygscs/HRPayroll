<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeHourlySalaryProcessSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeHourlySalaryProcessSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Salary Process</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessSave.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <div class="crumbs">
        <ul id="breadcrumbs" class="breadcrumb">
            <li>
                <a href="<%=ResolveUrl("~/Modules/Main.aspx") %>">
                    <i class="fa fa-home"></i>
                    Dashboard
                </a>
            </li>
            <li>
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessList.aspx") %>" title="Employee Salary Process">Employee Salary Process</a>
            </li>

            <li class="current">Save Employee Salary Process
            </li>
        </ul>

    </div>
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <h3>Save Employee Salary Process</h3>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <h4>
                        <i class="fa fa-reorder"></i>
                        <span>Save Employee Salary Process</span>
                    </h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <asp:HiddenField ID="hfMonth" runat="server" />
                        <asp:HiddenField ID="hfEmployeeId" runat="server" />

                        <div class="form-group efirst">
                            <label class="col-md-2 control-label">
                                Department :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label">
                                Employee Name :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label">
                                Employee No:
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblEmployeeNo" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Month :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblMonth" runat="server"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label">
                                Year :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblYear" runat="server"></asp:Label>
                            </div>

                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Total Days :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblTotalDays" runat="server" CssClass="lblTotalDays" Text="0"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label">
                                Total Present Days :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblTotalPresentDays" runat="server" CssClass="lblTotalPresentDays" Text="0"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label">
                                Total Holidays :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblTotalHolidays" runat="server" CssClass="lblTotalHolidays" Text="0"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Leave :
                            </label>
                            <div class="col-md-6 control-label textLeft">
                                <asp:HiddenField ID="hfAllowLeave" runat="server" Value="0"></asp:HiddenField>
                                <asp:HiddenField ID="hfTotalUsedLeave" runat="server" Value="0"></asp:HiddenField>
                                <asp:HiddenField ID="hfCalculateLeave" runat="server" Value="0"></asp:HiddenField>
                                <asp:Label ID="lblLeave" runat="server" Text="0" />
                            </div>
                            <div id="divPaidLeaveAmount" runat="server">
                                <label class="col-md-2 control-label">
                                    Leave Salary :
                                </label>
                                <div class="col-md-2 control-label textLeft">
                                    <asp:Label ID="lblTotalPaidLeaveSalary" runat="server" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Total Hours :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblTotalHours" runat="server" Text="0"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label ">
                                Total Hours Salary :
                            </label>
                            <div class="col-md-6 control-label textLeft">
                                <asp:Label ID="lblTotalHourSalary" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Total Over Time Hours :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblTotalOverTimeHours" runat="server" Text="0"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label ">
                                Over Time Salary :
                            </label>
                            <div class="col-md-6 control-label textLeft">
                                <asp:Label ID="lblTotalOverTimeSalary" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Basic :
                            </label>
                            <div class="col-md-10  control-label textLeft">
                                <asp:Label ID="lblBasic" Text="0" CssClass="lblBasic" runat="server"></asp:Label>
                                /
                                <asp:Label ID="lblPaidBasic" Text="0" CssClass="lblPaidBasic" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Net Salary :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblNetSalary" runat="server" CssClass="lblNetSalary" Text="0"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label">
                                Professional Tax :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:Label ID="lblProfessionalTax" runat="server" CssClass="lblProfessionalTax" Text="0"></asp:Label>
                            </div>
                            <label class="col-md-2 control-label">
                                On Hand Salary :
                            </label>
                            <div class="col-md-2 control-label textLeft">
                                <asp:HiddenField ID="hfCalculateSalary" Value="0" runat="server" ClientIDMode="Static" />
                                <asp:Label ID="lblOnHandSalary" runat="server" CssClass="lblOnHandSalary" Text="0"></asp:Label>
                                <asp:TextBox ID="txtOnHandSalary" runat="server" CssClass="displayNone txtOnHandSalary"></asp:TextBox>
                                <asp:CompareValidator ID="cvSalary" runat="server" ValueToCompare="0" ControlToValidate="txtOnHandSalary" Display="Dynamic"
                                    ErrorMessage="Must set On Hand Salary grater than 0." CssClass="required" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Paid Date <span class="required">*</span>
                            </label>
                            <div class="col-md-10 date-select">
                                <input type="text" runat="server" readonly="" id="txtPaidDate" class="form-control input-width-xlarge txtPaidDate" />
                                <asp:RequiredFieldValidator ID="rfvPaidDate" SetFocusOnError="true" ControlToValidate="txtPaidDate" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Paid Date." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Is Paid 
                            </label>
                            <div class="col-md-10">
							<label class="checkbox-inline">
                                    <asp:CheckBox ID="chkbxIsPaid" runat="server"  />
                                    <label for="cphBody_chkbxIsPaid">Is Paid</label>
                                </label> 
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <div class="form-actions">
                            <a href="/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessList.aspx" class="btn pull-right">Cancel</a>
                            <asp:Button CssClass="btn btn-success pull-right" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
