<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ERP.Modules.Main1" EnableViewState="true" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Arity Infoway</title>
    <script>
        window.onload = function () {
            var ctx = document.getElementById('chart-area').getContext('2d');
            window.myDoughnut = new Chart(ctx, config);
        };
    </script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <form id="frmMain" runat="server">

        <div id="divAdmin" visible="false" runat="server" class="dashboards">
            <div class="page-header">
                <h3>Overview</h3>
                <!--<h3>Welcome back,
				<asp:Label ID="lblEmployeeName" ></asp:Label></h3>-->
            </div>
            <!--<span class="todaysDate">Today is Wednesday, 5 August 2020</span>-->
            <div class="row overviewData">
                <%--<div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <div class="widget-content">
							<div class="title">Total Employee</div>
                            <div class="value">
                                <asp:Label ID="lblTotalEmployee" runat="server" Text="60"></asp:Label>
								<span class="updownvalue">
									<i class="fas fa-long-arrow-alt-up"></i>
									<i class="fas fa-long-arrow-alt-down hidden"></i>
									2.00
								</span>
                            </div>
							 <div class="visual">
                                <img  src="<%=ResolveUrl("~/Images/employess.svg") %>" class="svg" alt="employess"/>

                            </div>
                        </div>
                    </div>
                </div>--%>

                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <div class="widget-content">
                            <div class="title">Active Employee</div>
                            <div class="value">
                                <asp:Label ID="lblActiveEmployee" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="visual">
                                <img src="<%=ResolveUrl("~/Images/employess.svg") %>" class="svg" alt="Active Employee" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/PresentEmployeeList.aspx") %>">
                            <div class="widget-content">
                                <div class="title">Present Employee</div>
                                <div class="value">
                                    <asp:Label ID="lblPresentEmployee" runat="server" Text="0"></asp:Label>
                                </div>
                                <div class="visual">
                                    <img src="<%=ResolveUrl("~/Images/employess.svg") %>" class="svg" alt="Present Employee" />
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/AbsentEmployeeList.aspx") %>">
                            <div class="widget-content">
                                <div class="title">Absent Employee</div>
                                <div class="value">
                                    <asp:Label ID="lblAbsentEmployee" runat="server" Text="0"></asp:Label>
                                </div>
                                <div class="visual">
                                    <img src="<%=ResolveUrl("~/Images/employess.svg") %>" class="svg" alt="Absent Employee" />
                                </div>
                            </div>
                        </a>

                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <div class="widget-content">
                            <div class="title">Today Employee on Leave</div>
                            <div class="value">
                                <asp:Label ID="lblLeaveEmployee" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="visual">
                                <img src="<%=ResolveUrl("~/Images/employess.svg") %>" class="svg" alt="Leave Employee" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <div class="widget-content">
                            <div class="title">Today Interview</div>
                            <div class="value">
                                <asp:Label ID="lblInterview" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="visual">
                                <img src="<%=ResolveUrl("~/Images/interview.svg") %>" class="svg" alt="Today Interview" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <div class="widget-content">
                            <div class="title">Birthday Update</div>
                            <div class="value">
                                <asp:Label ID="lblBirthdayCount" runat="server" Text="0"></asp:Label>
                            </div>
                            <div class="visual">
                                <img src="<%=ResolveUrl("~/Images/revenue.svg") %>" class="svg" alt="Today Birthday" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <a href="<%=ResolveUrl("~/Modules/BioMetricDevice/Device/DeviceList.aspx") %>">
                            <div class="widget-content">
                                <div class="title">BioMetric Devices</div>
                                <div class="value">
                                    <asp:Label ID="lblBioMetricDevice" runat="server" Text="0"></asp:Label>
                                </div>
                                <div class="visual">
                                    <img src="<%=ResolveUrl("~/Images/Biometric.svg") %>" class="svg" alt="BioMetric Devices" />
                                </div>
                            </div>
                        </a>
                    </div>
                </div>

                <div class="col-sm-6 col-md-3">
                    <div class="statbox widget box box-hidden">
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Transactions/EmployeeAttendanceEntry.aspx") %>">
                            <div class="widget-content">
                                <div class="title">Employee Attendance</div>
                                <div class="visual">
                                    <img src="<%=ResolveUrl("~/Images/attendance.svg") %>" class="svg" alt="Employee Attendance Entry" />
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Salary Chart</h4>
                            <div class="toolbar no-padding">
                                <div class="form-group selectValues">
                                    <select class="form-control">
                                        <option>Last 6 Months</option>
                                        <option>Last 1 Months</option>
                                    </select>
                                </div>
                                <!-- <div class="btn-group">
                                    <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                                </div>-->
                            </div>
                        </div>
                        <div class="widget-content">

                            <asp:Chart ID="ChartMonthSalaryAdmin" runat="server" Width="1000px">
                                <Series>
                                    <asp:Series Name="SeriesTotalAdmin" IsValueShownAsLabel="true"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartAreaSalaryAdmin">
                                        <AxisX Title="Months">
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                        <AxisY Title="Salary">
                                            <MajorGrid Enabled="false" />
                                        </AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Department Chart</h4>
                        </div>
                        <div class="widget-content">
                            <br />
                            <div class="margin5">Total Employee</div>
                            <%--    <div class="blueFilled margin5 sizeFilled"></div>
                            <div class="margin5">Weekly Off</div>
                            <div class="cyanFilled margin5 sizeFilled"></div>
                            <div class="margin5">Holiday</div>
                            <div class="redFilled margin5 sizeFilled"></div>
                            <div class="margin5">Leave</div>--%>

                            <div class="clearfix"></div>

                            <asp:Chart ID="ChartDepartmentAdmin" runat="server" Width="1000px">
                                <Series>
                                    <asp:Series Name="SeriesTotalEmployeeAdmin" IsValueShownAsLabel="true"></asp:Series>
                                    <%-- <asp:Series Name="SeriesWeekluOffAdmin" Color="Blue" IsValueShownAsLabel="true"></asp:Series>
                                    <asp:Series Name="SeriesHolidaysAdmin" Color="Cyan" IsValueShownAsLabel="true"></asp:Series>
                                  <asp:Series Name="SeriesTotalUseLeaveAdmin" Color="Red" IsValueShownAsLabel="true"></asp:Series>--%>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartAreaDepartmentAdmin">
                                        <AxisX Title="Department Name">
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                        <AxisY Title="Employee">
                                            <MajorGrid Enabled="false" />
                                        </AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row endsCharts">
                <div class="col-md-4">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Upcoming Holidays</h4>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="gvUpcomingHoliday" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvUpcomingHoliday">
                                <Columns>
                                    <asp:BoundField HeaderText="Name" DataField="Title" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("StartDate", "{0:MM/dd/yyyy}") %> -  <%# Eval("EndDate", "{0:MM/dd/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BackColor="#F9F9F9" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Employee By Department</h4>
                        </div>
                        <div class="widget-content">
                            <div class="pieChart">
                                <div id="canvas-holder">
                                    <canvas id="chart-area"></canvas>
                                </div>
                            </div>
                            <div class="pieChartInfo" runat="server" id="divDepartment">
                                <ul>
                                    <li>Developer</li>
                                    <li>Designer</li>
                                    <li>Sales</li>
                                    <li>Others</li>
                                </ul>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="col-md-4" style="padding-left: 0">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Upcoming BirthDays</h4>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="gvUpcomingBirthday" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvUpcomingBirthday">
                                <Columns>
                                    <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("BirthDate", "{0:dd/MM/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BackColor="#F9F9F9" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- Employee Part Start --%>

        <div id="divEmployee" visible="false" runat="server" class="dashboards">
            <div class="page-header">
                <div class="userProfile">
                    <div class="userPhoto">
                        <img id="imgUserProfile" runat="server" alt="User Icon" />
                    </div>
                    <div class="userInfo">
                        <h4 class="userTitle">
                            <asp:Label ID="lblusername" runat="server"></asp:Label>
                            <span class="text-muted" runat="server" id="spnDesignation"></span>
                        </h4>
                        <div class="text-muted userdetail">
                            ID:
                            <label runat="server" id="employeeId"></label>
                        </div>
                        <div class="text-muted userdetail">
                            My Office Shift:
                            <label runat="server" id="employeeshift"></label>
                        </div>
                        <div class="userbtns">
                            <asp:Button CssClass="btn btn-success clockin" runat="server" ID="btnClockIn" Text="Clock In" OnClick="btnClockIn_Click" />
                            <asp:Button CssClass="btn clockout" runat="server" ID="btnClockOut" Text="Clock Out" OnClick="btnClockOut_Click" Enabled="false" />
                            <a href="<%=ResolveUrl("~/Modules/Profile.aspx") %>" class="btn userSetting" data-toggle="tooltip" data-placement="top" title="My Profile"><i class="fas fa-user-cog"></i></a>
                        </div>

                    </div>
                </div>
            </div>

            <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
            <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
            <div class="row row-bg overviewData">
                <div class="col-sm-6 col-md-4">
                    <div class="statbox widget box box-shadow">
                        <div class="widget-content">
                            <div class="visual cyan">
                                <i class="fa fa-list-alt marginRight0"></i>
                            </div>
                            <div class="title">Total Leave</div>
                            <div class="value">
                                <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-4">
                    <div class="statbox widget box box-shadow">
                        <div class="widget-content">
                            <div class="visual green">
                                <i class="fa fa-calendar-check marginRight0"></i>
                            </div>
                            <div class="title">Used Leave</div>
                            <div class="value">
                                <asp:Label ID="lblUsedLeave" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-6 col-md-4 hidden-xs">
                    <div class="statbox widget box box-shadow">
                        <div class="widget-content">
                            <div class="visual yellow">
                                <i class="fa fa-user-clock marginRight0"></i>
                            </div>
                            <div class="title">Remaining Leave</div>
                            <div class="value">
                                <asp:Label ID="lblRemainingLeave" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <asp:UpdatePanel ID="upCalender" runat="server">
                    <ContentTemplate>
                        <div class="col-md-4">
                            <div class="widget box calendarChart">
                                <div class="widget-header">

                                    <h4>Calender</h4>
                                    <!--<div class="toolbar no-padding">
                                        <div class="btn-group">
                                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                                        </div>
                                    </div> -->
                                </div>
                                <div class="widget-content">
                                    <div id="chart_simple" class="chart">
                                        <div class="widget-content">
                                            <div class="redFilled margin5 sizeFilled"></div>
                                            <div class="control-label margin5">Leave</div>
                                            <div class="cyanFilled margin5 sizeFilled"></div>
                                            <div class="margin5">Holiday</div>
                                            <div class="blueFilled margin5 sizeFilled"></div>
                                            <div class="margin5">Weekly Off</div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <asp:Calendar ID="Calendar" TitleStyle-BackColor="#F9F9F9" BorderColor="White" Width="100%" runat="server" OnDayRender="Calendar_DayRender"></asp:Calendar>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-md-4">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Upcoming Holidays</h4>
                            <!--<div class="toolbar no-padding">
                                <div class="btn-group">
                                    <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                                </div>
                            </div> -->
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="gvHoliday" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvHoliday">
                                <Columns>
                                    <asp:BoundField HeaderText="Name" DataField="Title" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("StartDate", "{0:MM/dd/yyyy}") %> -  <%# Eval("EndDate", "{0:MM/dd/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BackColor="#F9F9F9" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Upcoming Birthdays</h4>
                        </div>
                        <div class="widget-content">
                            <asp:GridView ID="gvBirthday" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvBirthday">
                                <Columns>
                                    <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Eval("BirthDate", "{0:dd/MM/yyyy}") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BackColor="#F9F9F9" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Salary Chart</h4>
                            <!--<div class="toolbar no-padding">
                                <div class="btn-group">
                                    <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                                </div>
                            </div> -->
                        </div>
                        <div class="widget-content">
                            <asp:Chart ID="ChartSalary" runat="server">
                                <Series>
                                    <asp:Series Name="SeriesTotal" IsValueShownAsLabel="true"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartAreaSalary">
                                        <AxisX Title="Months">
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                        <AxisY Title="Salary">
                                            <MajorGrid Enabled="false" />
                                        </AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="widget box">
                        <div class="widget-header">
                            <h4>Attendance Chart</h4>
                            <!--<div class="toolbar no-padding">
                                <div class="btn-group">
                                    <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                                </div>
                            </div> -->
                        </div>
                        <div class="widget-content">
                            <br />
                            <div class="pinkFilled margin5 sizeFilled"></div>
                            <div class="margin5">Total Days</div>
                            <div class="silverFilled margin5 sizeFilled"></div>
                            <div class="margin5">Present Days</div>
                            <div class="blueFilled margin5 sizeFilled"></div>
                            <div class="margin5">Weekly Off</div>
                            <div class="cyanFilled margin5 sizeFilled"></div>
                            <div class="margin5">Holiday</div>
                            <div class="redFilled margin5 sizeFilled"></div>
                            <div class="margin5">Leave</div>

                            <div class="clearfix"></div>
                            <asp:Chart ID="ChartAttendance" runat="server" BackGradientStyle="None">
                                <Series>
                                    <asp:Series Name="SeriesTotalDays" Color="Pink" IsValueShownAsLabel="true"></asp:Series>
                                    <asp:Series Name="SeriesPresentDays" Color="Silver" IsValueShownAsLabel="true"></asp:Series>
                                    <asp:Series Name="SeriesWeekluOff" Color="Blue" IsValueShownAsLabel="true"></asp:Series>
                                    <asp:Series Name="SeriesHolidays" Color="Cyan" IsValueShownAsLabel="true"></asp:Series>
                                    <asp:Series Name="SeriesTotalUseLeave" Color="Red" IsValueShownAsLabel="true"></asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartAreaAttendance">
                                        <AxisX Title="Months">
                                            <MajorGrid Enabled="false" />
                                        </AxisX>
                                        <AxisY Title="Days">
                                            <MajorGrid Enabled="false" />
                                        </AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>
