<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeSalaryList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeSalaryList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Salary</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeSalaryList.js")%>"></script>




</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
   <!-- <div class="crumbs">
        <ul id="breadcrumbs" class="breadcrumb">
            <li>
                <a href="<%=ResolveUrl("~/Modules/Main.aspx") %>">
                    <i class="fa fa-home"></i>
                    Dashboard
                </a>
            </li>
            <li class="current">
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSalaryList.aspx") %>" title="Employee Salary">Employee Salary</a>
            </li>
        </ul>

    </div> -->
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee Salary</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSalarySave.aspx") %>">Add Employee Salary</a>
					</span>
				</div>
			</div>
		</div> 
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
				<!--<div class="widget-header">
                    <h4>All List <span>Employee Salary</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                           <span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSalarySave.aspx") %>">Add Employee Salary</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> -->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Employee Salary</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvEmployeeSalary_PreRender" ID="gvEmployeeSalary" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvEmployeeSalary">
                            <Columns>
                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsLeave"))?"redFont":"" %>'><%# Eval("FullName") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Basic" DataField="Basic" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Allowance" DataField="TotalEarning" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Deduction" DataField="TotalDeduction" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Total" DataField="TotalSalary" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"EmployeeSalarySave.aspx?id="+Eval("EmployeeSalaryID") %>">
											<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>

										</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="#F9F9F9" />
                        </asp:GridView>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
