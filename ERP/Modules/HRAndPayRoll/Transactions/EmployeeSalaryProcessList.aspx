<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeSalaryProcessList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeSalaryProcessList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Salary Process</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
  
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee Salary Process</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Transactions/EmployeeSalaryProcessSave.aspx") %>">Add Employee Salary Process</a>
						
					</span>
					
				</div>
			</div>
		</div>
        
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box employeTable salaryProcessTable">
                <!--<div class="widget-header">
                    <h4>
                        <i class="fa fa-list"></i>
                        All List <span>Employee Salary Process</span>
                    </h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content noFiltering removeScroll">
					<div class="headingOftabel">
						<h4>All List <span>Employee Salary Process</span></h4>
					</div>
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
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control input-width-xlarge ddlMonth" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divSalaryProcess" visible="false" class="form-group" runat="server">
                                    <div class="col-md-12">
                                        <div class="tabbable tabbable-custom">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tabCompleted" data-toggle="tab">Completed</a></li>
                                                <li><a href="#tabPending" data-toggle="tab">Pending</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tabCompleted">
                                                    <asp:GridView ID="gvEmployeeCompletedSalaryProcess" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvEmployeeCompletedSalaryProcess">
                                                        <Columns>

                                                            <asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Employee Name" DataField="FullName" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Paid Basic" DataField="PaidBasic" HeaderStyle-Width="9%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Paid Allowance" DataField="PaidTotalEarning" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Paid Deduction" DataField="PaidTotalDeduction" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Total Paid" DataField="PaidTotalSalary" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    
                                                                    <a href="javascript:;" id="btnView" onclick="EmployeeSalaryProcessList.RediectSalarySave('<%#Eval("EmployeeId")%>')" class="btnView"><i class="fas fa-eye" title="Click to View Employee Salary Process"></i></a>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle BackColor="#F9F9F9" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="tab-pane" id="tabPending">
                                                    <asp:GridView ID="gvEmployeePendingSalaryProcess" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvEmployeePendingSalaryProcess">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Employee Name" DataField="FullName" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Basic" DataField="PaidBasic" HeaderStyle-Width="9%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Total Allowance" DataField="PaidTotalEarning" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Total Deduction" DataField="PaidTotalDeduction" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:BoundField HeaderText="Total Salary" DataField="PaidTotalSalary" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>

                                                                    <a href="javascript:;" id="btnEdit" onclick="EmployeeSalaryProcessList.RediectSalarySave('<%#Eval("EmployeeId")%>')" class="btnEdit" >
																		<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>

																	</a>

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

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
