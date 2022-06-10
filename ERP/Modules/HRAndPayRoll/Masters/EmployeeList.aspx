<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeList.js")%>"></script>
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
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeList.aspx") %>" title="Employee">Employee</a>
            </li>
        </ul>

    </div> -->

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee(s) 201 File</h3>
		
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSave.aspx") %>">Add Employees</a>
					</span>
					
				</div>
			</div>
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box employeTable">
				<!--<div class="widget-header">
                    <h4>List All <span>Employees</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                            <span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSave.aspx") %>">Add Employee</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> -->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>List All <span> Record(s)</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvEmployee_PreRender" ID="gvEmployee" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvEmployee">
                            <Columns>
                                <asp:TemplateField HeaderText="Full Name" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
									
                                        <span class='<%# Convert.ToBoolean(Eval("IsLeave"))?"redFont":"" %>'><%# Eval("FullName") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Employee Type" DataField="EmployeeType" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Employee Grade" DataField="EmployeeGrade" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Designation" DataField="Designation" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Email/Username" DataField="Email" HeaderStyle-Width="17%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                       <a class='<%# Convert.ToBoolean(Eval("IsLeave"))?"displayNone":"" %> ' href="<%#"EmployeeSave.aspx?id="+Eval("EmployeeID") %>">
										<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>
									   </a>
                                        <a class="" href="<%#"EmployeeProfile.aspx?id="+Eval("EmployeeID") %>">
										<img  src="<%=ResolveUrl("~/Images/resign.svg") %>" class="svg" alt="View Employee Profile"/>
									   </a>
									   <a class="" href="<%#"EmployeeResignSave.aspx?id="+Eval("EmployeeID") %>">
										<img  src="<%=ResolveUrl("~/Images/resign.svg") %>" class="svg" alt="resign sign"/>
									   </a>
									   <asp:LinkButton   ID="btnPermenentDelete" runat="server" CommandArgument='<%#Eval("EmployeeID") %>' OnClick="btnPermenentDelete_Click" CssClass='<%# Convert.ToBoolean(Eval("IsLeave"))?"btnPermenentDelete ":"btnPermenentDelete displayNone" %> ' >
										<i class="fa fa-ban" title="remove employee in database permanently"></i>
									   </asp:LinkButton>
									   <asp:LinkButton   ID="btnDelete" runat="server" CommandArgument='<%#Eval("EmployeeID") %>' OnClick="btnDelete_Click" CssClass='<%# Convert.ToBoolean(Eval("IsLeave"))?"btnDelete":"btnDelete displayNone" %>'>
										<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove employee"/>
									   </asp:LinkButton>
										
                                      
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
