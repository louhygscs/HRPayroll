<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeProfileList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeProfileList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Profile</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeProfileList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

	<!-- breadcrumb -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee(s) 201 File</h3>
		
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeProfile.aspx") %>">Add Employees</a>
					</span>
					
				</div>
			</div>
		</div>
    </div>
	<!-- end breadcrumb -->

	<!-- content row -->
	<div class="row">
		<div class="col-md-12">
			<div class="widget box employeeTable">

				<div class="widget-content">
					<div class="headingOftabel">
						<h4>List All <span> Record(s)</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvEmployee" OnPreRender="gvEmployee_PreRender">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Full Name" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsLeave"))?"redFont":"" %>'><%# Eval("FullName") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                
								<%--<asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />--%>

								<asp:TemplateField HeaderText="Emp. No" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left">
									<ItemTemplate>
										<a class="" href="<%#"EmployeeProfile.aspx?id="+Eval("EmployeeID") %>">
											<%# Eval("EmployeeNo") %>
										</a>
									</ItemTemplate>
								</asp:TemplateField>

								<asp:BoundField HeaderText="Date Hired" DataField="DateHired" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Full Name" DataField="FullName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Work Location" DataField="WorkLocation" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
								<asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
                                <%--<asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Designation" DataField="Designation" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />--%>
                                <asp:BoundField HeaderText="Job Position" DataField="JobTitle" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
								<asp:BoundField HeaderText="Employee Status" DataField="EmploymentStatus" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                       <%--<a class='<%# Convert.ToBoolean(Eval("IsLeave"))?"displayNone":"" %> ' href="<%#"EmployeeSave.aspx?id="+Eval("EmployeeID") %>">
										<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>
									   </a>--%>
                                        <%--<a class="" href="<%#"EmployeeProfile.aspx?id="+Eval("EmployeeID") %>">
										<img  src="<%=ResolveUrl("~/Images/resign.svg") %>" class="svg" alt="View Employee Profile"/>
									   </a>--%>
									   <a class="" href="<%#"EmployeeResignSave.aspx?id="+Eval("EmployeeID") %>">
										<img  src="<%=ResolveUrl("~/Images/resign.svg") %>" class="svg" alt="resign sign"/>
									   </a>
									   <%--<asp:LinkButton   ID="btnPermenentDelete" runat="server" CommandArgument='<%#Eval("EmployeeID") %>' CssClass='<%# Convert.ToBoolean(Eval("IsLeave"))?"btnPermenentDelete ":"btnPermenentDelete displayNone" %> ' OnClick="btnPermenentDelete_Click">
										<i class="fa fa-ban" title="remove employee in database permanently"></i>
									   </asp:LinkButton>--%>
									   <%--<asp:LinkButton   ID="btnDelete" runat="server" CommandArgument='<%#Eval("EmployeeID") %>' CssClass='<%# Convert.ToBoolean(Eval("IsLeave"))?"btnDelete":"btnDelete displayNone" %>' OnClick="btnDelete_Click">
										<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove employee"/>
									   </asp:LinkButton>--%>
										
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
	<!-- end content row -->

</asp:Content>
