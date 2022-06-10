<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DeviceAttendance.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Reports.DeviceAttendance" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Device Attendance</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Device Attendance</h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box employeeTable">
                <!--<div class="widget-header">
                    <h4>
                        <i class="fa fa-list"></i>
                       All List <span>Device Attendance </span>
                    </h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> -->
                <div class="widget-content noFiltering">
                    <div class="headingOftabel">
                        <h4>All List <span>Device Attendance </span></h4>
                    </div>
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-group efirst">
                                            <label class="col-md-12 control-label">
                                                Month :
                                            </label>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control input-width-xlarge">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvMonth" runat="server" SetFocusOnError="true" InitialValue=""
                                                    ControlToValidate="ddlMonth" ErrorMessage="Please select Month." Display="Dynamic" CssClass="required"
                                                    Text="Please select Month."> 
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-md-12 control-label">
                                                Employee :
                                            </label>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control input-width-xlarge">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" SetFocusOnError="true" InitialValue=""
                                                    ControlToValidate="ddlEmployee" ErrorMessage="Please select Employee." Display="Dynamic" CssClass="required"
                                                    Text="Please select Employee."> 
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-success" OnClick="btnReport_Click" Text="Generate Report" />
                                    </div>
                                </div>

                                <div class="widget box" runat="server" id="divcolor" visible="false">
                                    <div class="widget-content">
                                        <div class="greenFilled margin5 sizeFilled"></div>
                                        <div class="margin5">Present</div>
                                        <div class="redFilled margin5 sizeFilled"></div>
                                        <div class="margin5">Leave</div>
                                        <div class="cyanFilled margin5 sizeFilled"></div>
                                        <div class="margin5">Holiday</div>
                                        <div class="blueFilled margin5 sizeFilled"></div>
                                        <div class="margin5">Weekly Off</div>
                                        <br />
                                    </div>
                                </div>

                                <asp:GridView ID="gvDeviceAttendance" runat="server" AutoGenerateColumns="False" CssClass="table dataTable table-bordered gvDeviceAttendance">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttendanceDate" CssClass='<%#setClass(Convert.ToInt32(Eval("AttendanceType")))%>' runat="server" Text='<%# Eval("AttendanceDateValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Attendances" DataField="Attendances" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Punch Method" DataField="PunchMethod" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Working Hours" DataField="WorkingHours" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Overtime Hours" DataField="OvertimeHours" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttendanceType" CssClass='<%#setClass(Convert.ToInt32(Eval("AttendanceType")))%>' runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <EmptyDataRowStyle BackColor="#F9F9F9" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
