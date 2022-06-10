<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeBehind="EmployeeAttendanceEntry.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeAttendanceEntry" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Attendance</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeAttendanceEntry.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Employee Attendance Entry</h3>
        </div>

    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <!-- <div class="widget-header">
                    <h4>
                        <i class="fa fa-list"></i>
                        All List <span>Employee Attendance Entry</span>
                    </h4>	
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> -->
                <div class="widget-content noFiltering attendaceTable">
                    <div class="headingOftabel">
                        <h4>All List <span>Employee Attendance Entry</span></h4>
                    </div>
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>

                        <%--  <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        
                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label ">
                                        Department<span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control input-width-xlarge" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" InitialValue="" SetFocusOnError="true" ControlToValidate="ddlDepartment" CssClass="required" Display="Dynamic" ErrorMessage="Please select Department." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Employee<span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlEmployee" CssClass="form-control input-width-xlarge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEmployee" InitialValue="" SetFocusOnError="true" ControlToValidate="ddlEmployee" CssClass="required" Display="Dynamic" ErrorMessage="Please select Employee." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Month<span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control input-width-xlarge" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvMonth" InitialValue="" SetFocusOnError="true" ControlToValidate="ddlMonth" CssClass="required" Display="Dynamic" ErrorMessage="Please select Month." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <asp:GridView ID="gvEmployeeAttendance" runat="server" AutoGenerateColumns="False" CssClass="table dataTable trHeader table-bordered gvEmployeeAttendance">
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hfEmployeeAttendanceId" Value='<%# Eval("EmployeeAttendanceID") %>' />
                                        <asp:Label ID="lblAttendanceDate" runat="server" Text='<%# Eval("AttendanceDate","{0:MM/dd/yyyy}") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Time" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <cc1:TimeSelector ID="tsTimeIn" Hour='<%# Eval("TimeInHours") %>' Minute='<%# Eval("TimeInMinutes") %>' SelectedTimeFormat="Twelve" AmPm='<%# Eval("TimeInAMPM").ToString()=="AM"?MKB.TimePicker.TimeSelector.AmPmSpec.AM:MKB.TimePicker.TimeSelector.AmPmSpec.PM %>' runat="server" DisplaySeconds="false" CssClass="Timer">
                                        </cc1:TimeSelector>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Out Time" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <cc1:TimeSelector ID="tsTimeOut" Hour='<%# Eval("TimeOutHours") %>' Minute='<%# Eval("TimeOutMinutes") %>' SelectedTimeFormat="Twelve" AmPm='<%# Eval("TimeOutAMPM").ToString()=="AM"?MKB.TimePicker.TimeSelector.AmPmSpec.AM:MKB.TimePicker.TimeSelector.AmPmSpec.PM %>' runat="server" DisplaySeconds="false" CssClass="Timer">
                                        </cc1:TimeSelector>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Working Hours" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWorkingHours" runat="server" onKeyUp="EmployeeAttendanceEntry.checkDecimal(this);" Text='<%# Eval("WorkingHours") %>' Enabled='<%# ((Convert.ToInt32(Eval("AttendanceType"))== 2 || (Convert.ToInt32(Eval("AttendanceType"))== 3 ))? false:true) %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overtime Hours" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOvertimeHours" runat="server" onKeyUp="EmployeeAttendanceEntry.checkDecimal(this);" Text='<%# Eval("OvertimeHours") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlAttendanceType" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAttendanceType_SelectedIndexChanged" Enabled='<%# ((Convert.ToInt32(Eval("AttendanceType"))== 2 || (Convert.ToInt32(Eval("AttendanceType"))== 3 ))? false:true) %>' SelectedValue='<%# Eval("AttendanceType") %>' runat="server">
                                            <asp:ListItem Value="4">Default</asp:ListItem>
                                            <asp:ListItem Value="3">Holiday</asp:ListItem>
                                            <asp:ListItem Value="1">Leave</asp:ListItem>
                                            <asp:ListItem Value="2">WeeklyOff</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attendance" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttendance" Text='<%# ((Convert.ToInt32(Eval("AttendanceType"))== 4)? "Present":"") %>' Visible='<%# ((Convert.ToInt32(Eval("AttendanceType"))== 4)? true:false) %>' runat="server" />
                                        <asp:DropDownList ID="ddlAttendance" CssClass="form-control" Visible='<%# ((Convert.ToInt32(Eval("AttendanceType"))== 1)? true:false) %>' SelectedValue='<%# Eval("Attendance") %>' runat="server">
                                            <asp:ListItem Value="1.00">1</asp:ListItem>
                                            <asp:ListItem Value="0.50">0.5</asp:ListItem>
                                            <asp:ListItem Value="0.00">0</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <button type="button" class="btn modelbox-open <%# ((Convert.ToInt32(Eval("AttendanceType"))== 2 || (Convert.ToInt32(Eval("AttendanceType"))== 3 ))? "hide":"") %>" onclick="EmployeeAttendanceEntry.AddAttendance(this);" data-toggle="modal" data-target="#attendance-modal" data-date="<%# Eval("AttendanceDate") %>" data-employeeattendanceid="<%# Eval("EmployeeAttendanceID") %>">Add</button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="#F9F9F9" />
                        </asp:GridView>

                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>

                        <div class="form-actions formActionbtn">
                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                            <a href="/Modules/Main.aspx" class="btn ">Cancel
                            </a>
                        </div>

                        <div id="attendance-modal" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Attendance Time</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <input type="hidden" runat="server" id="hfDate" value="" class="hfDate" />
                                                    <input type="hidden" runat="server" id="hfEmployeeAttendanceID" value="" class="hfEmployeeAttendanceID" />
                                                    <label class="control-label">In Time</label>
                                                    <div>
                                                        <cc1:TimeSelector ID="atsTimeIn" SelectedTimeFormat="Twelve" AmPm='<%# MKB.TimePicker.TimeSelector.AmPmSpec.AM %>' runat="server" DisplaySeconds="false" CssClass="Timer">
                                                        </cc1:TimeSelector>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label">In Out</label>
                                                    <div>
                                                        <cc1:TimeSelector ID="atsTimeOut" SelectedTimeFormat="Twelve" AmPm='<%# MKB.TimePicker.TimeSelector.AmPmSpec.PM %>' runat="server" DisplaySeconds="false" CssClass="Timer">
                                                        </cc1:TimeSelector>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label">Description</label>
                                                    <asp:TextBox ID="atxtDescription" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnAttendanceAdd" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnAttendanceAdd_Click" />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </form>
                </div>

            </div>
        </div>
    </div>

    <!-- Modal -->


</asp:Content>
