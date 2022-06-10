<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true"
    CodeBehind="EmployeeSalarySave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeSalarySave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Salary</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeSalarySave.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Add Employee Salary</h3>

        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>Employee Salary</span>
                        </h4>
                    </div>

                    <!--  <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>-->
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
                                        Salary Type 
                                    </label>
                                    <div class="col-md-12">
                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbtnMonthSalary" runat="server" Text="Month-Wise Salary" GroupName="Salary" Checked="true" AutoPostBack="true" OnCheckedChanged="rbtnMonthSalary_CheckedChanged" />
                                        </label>
                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbtnWeeklySalary" runat="server" Text="Weekly-Wise Salary" GroupName="Salary" AutoPostBack="true" OnCheckedChanged="rbtnMonthSalary_CheckedChanged" />
                                        </label>
                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbtnHourSalary" runat="server" Text="Hour-Wise Salary" GroupName="Salary" AutoPostBack="true" OnCheckedChanged="rbtnMonthSalary_CheckedChanged" />
                                        </label>

                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rbtnDailySalary" runat="server" Text="Daily-Wise Salary" GroupName="Salary" AutoPostBack="true" OnCheckedChanged="rbtnMonthSalary_CheckedChanged" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-group efirst">
                                            <label class="col-md-12 control-label">
                                                Department <span class="required">*</span>
                                            </label>
                                            <div class="col-md-12 control-label textLeft">
                                                <asp:Label ID="lblDepartment" Visible="false" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control input-width-xlarge">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Department." runat="server"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-md-12 control-label">
                                                Employee <span class="required">*</span>
                                            </label>
                                            <div class="col-md-12 control-label textLeft">
                                                <asp:Label ID="lblEmployee" Visible="false" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control input-width-xlarge">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Employee." runat="server"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-md-12 control-label" id="lblBasic" runat="server">
                                        Basic<span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtBasic" MaxLength="10" runat="server" CssClass="form-control input-width-xlarge txtBasic" onkeypress="return Common.isNumericKey(event,this)" onkeyup="EmployeeSalarySave.CalculateTotal()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBasic" SetFocusOnError="true" ControlToValidate="txtBasic" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Basic." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">
                                        Total Salary :
                                    </label>
                                    <div class="col-md-9 control-label textLeft">
                                        <asp:HiddenField ID="hfTotalSalary" runat="server" Value="0" ClientIDMode="Static" />
                                        <asp:Label ID="lblTotalSalary" runat="server" CssClass="lblTotalSalary" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12" runat="server" id="divAllowance">
                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Allowance </h4>
                                        </div>

                                    </div>
                                    <div class="widget-content">
                                        <asp:Repeater ID="rptAllowance" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            <%#Eval("AllowanceName") %> (<%#Eval("Percentage") %>%)
																
																<span class="redFont"><%#Convert.ToBoolean(Eval("IsConsider"))?"Consider":"" %></span>
                                                        </label>
                                                        <div class="col-md-12">
                                                            <asp:HiddenField ID="hfAllowanceId" runat="server" Value='<%#Eval("AllowanceID") %>' />
                                                            <asp:TextBox ID="txtAllowance" MaxLength="10" runat="server" Text='<%#Eval("Amount") %>' CssClass="form-control input-width-xlarge txtAllowance" onkeypress="return Common.isNumericKey(event,this)" onkeyup="EmployeeSalarySave.CalculateTotal()"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="row">
                                            <div class="col-md-6">

                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Total Allowance :
                                                    </label>
                                                    <div class="col-md-12 control-label textLeft">
                                                        <asp:HiddenField ID="hfTotalAllowance" runat="server" Value="0" ClientIDMode="Static" />
                                                        <asp:Label ID="lblTotalAllowance" runat="server" CssClass="lblTotalAllowance" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Deduction </h4>
                                        </div>
                                    </div>
                                    <div class="widget-content">
                                        <asp:Repeater ID="rptDeduction" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            <%#Eval("DeductionName") %>
                                                            <span class="redFont"><%#Convert.ToBoolean( Eval("IsConsider"))?"Consider":"" %></span>
                                                        </label>
                                                        <div class="col-md-12">
                                                            <asp:HiddenField ID="hfDeductionId" runat="server" Value='<%#Eval("DeductionID") %>' />
                                                            <asp:TextBox ID="txtDeduction" MaxLength="10" runat="server" Text='<%#Eval("Amount") %>' CssClass="form-control input-width-xlarge txtDeduction" onkeypress="return Common.isNumericKey(event,this)" onkeyup="EmployeeSalarySave.CalculateTotal()"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Total Deduction :
                                                    </label>
                                                    <div class="col-md-12 control-label textLeft">
                                                        <asp:HiddenField ID="hfTotalDeduction" runat="server" Value="0" ClientIDMode="Static" />
                                                        <asp:Label ID="lblTotalDeduction" runat="server" CssClass="lblTotalDeduction" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <div class="form-actions formActionbtn">
                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                            <a href="/Modules/HRAndPayRoll/Masters/EmployeeSalaryList.aspx" class="btn">Cancel
                            </a>


                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
