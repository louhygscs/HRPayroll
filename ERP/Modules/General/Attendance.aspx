<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="ERP.Modules.General.Attendance" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Attendance</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
 
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
			<h3>Attendance</h3>			
		</div>	
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box employessForms">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Attendance <span>Information</span>
						</h4>
					</div>
                   
                    <!--<div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div> -->
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
                                        Month :
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control input-width-xlarge" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="widget box " runat="server" id="divAttandanceInformation" visible="false">
                                  
                                    <div class="widget-content">
										<div class="row">
											<div class="col-md-4 col-sm-4 col-xs-12">
												<div class="form-group efirst">
													<label class="col-md-12 control-label">
														Total Days :
													</label>
													<div class="col-md-12 control-label textLeft">
														<asp:Label ID="lblTotalDays" runat="server" Text="0"></asp:Label>
													</div>
												</div>
											</div>
											<div class="col-md-4 col-sm-4 col-xs-12">
												<div class="form-group ">
													<label class="col-md-12 control-label">
														Total Present :
													</label>
													<div class="col-md-12 control-label textLeft">
														<asp:Label ID="lblTotalPresent" runat="server" Text="0"></asp:Label>
													</div>
												</div>	
											</div>
											<div class="col-md-4 col-sm-4 col-xs-12">
												<div class="form-group ">
													<label class="col-md-12 control-label">
														Total Leave :
													</label>
													<div class="col-md-12 control-label textLeft">
														<asp:Label ID="lblTotalLeave" runat="server" Text="0"></asp:Label>
													</div>
												</div>
											</div>
										</div>
										<div class="row">
											<div class="col-md-4 col-sm-4 col-xs-12">
												<div class="form-group">
													<label class="col-md-12 control-label">
														Total Weekly Off :
													</label>
													<div class="col-md-12 control-label textLeft">
														<asp:Label ID="lblTotalWeeklyOff" runat="server" Text="0"></asp:Label>
													</div>
												</div>
											</div>
											<div class="col-md-4 col-sm-4 col-xs-12">
												<div class="form-group">
													<label class="col-md-12 control-label">
														Total Holiday :
													</label>
													<div class="col-md-12 control-label textLeft">
														<asp:Label ID="lblTotalHoliday" runat="server" Text="0"></asp:Label>
													</div>
												</div>
											</div>
											<div class="col-md-4 col-sm-4 col-xs-12">
												<div class="form-group">
													<label class="col-md-12 control-label">
														Total Working Hours :
													</label>
													<div class="col-md-12 control-label textLeft">
														<asp:Label ID="lblTotalWorkingHours" runat="server" Text="0"></asp:Label>
													</div>
												</div>
											</div>
										</div>
										<div class="row">
											<div class="col-md-4 col-sm-4 col-xs-12">
												<div class="form-group">
													<label class="col-md-12 control-label">
														Total Overtime Hours :
													</label>
													<div class="col-md-12 control-label textLeft">
														<asp:Label ID="lblTotalOvertimeHours" runat="server" Text="0"></asp:Label>
													</div>
												</div>
											</div>
										</div>
										
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

                                <asp:GridView ID="gvEmployeeAttendance" runat="server" AutoGenerateColumns="False" CssClass="table dataTable trHeader table-bordered gvEmployeeAttendance">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttendanceDate" CssClass='<%#setClass(Convert.ToInt32(Eval("AttendanceType")))%>' runat="server" Text='<%# Eval("AttendanceDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="In Time" DataField="TimeIn" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Out Time" DataField="TimeOut" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Working Hours" DataField="WorkingHours" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Overtime Hours" DataField="OverTimeHours" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Type" DataField="AttendanceText" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-Width="28%" HeaderStyle-HorizontalAlign="Left" />
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
