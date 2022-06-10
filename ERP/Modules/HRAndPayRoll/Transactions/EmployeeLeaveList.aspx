<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeLeaveList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeLeaveList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Leave</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeLeaveList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee Leave</h3>
		</div>
        
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box employeTable employeLeaveTable">
               
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Employee Leave</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvEmployeeLeave_PreRender" ID="gvEmployeeLeave" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gvEmployeeLeave">
                            <Columns>
                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsApprove"))?"greenFont":"" %>'><%# Eval("EmployeeName") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Leave Category" DataField="LeaveCategory" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Date Range" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("StartDate", "{0:MM/dd/yyyy}") %> - <%# Eval("EndDate", "{0:MM/dd/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("TotalDay", "{0:0.#}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apply Date" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("ApplyDate", "{0:yyyy-MM-dd}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reason" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Response" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResponse" runat="server" Text='<%#Eval("Comments") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#  string.IsNullOrEmpty(Convert.ToString(Eval("Comments")))?Eval("Status"): Eval("Status")+" By "+Eval("ApprovedBy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a class="<%# Convert.ToString(Eval("Status"))=="Approve"?"displayNone":""%>" href="<%#"ReplyEmployeeLeave.aspx?id="+Eval("EmployeeLeaveCategoryMapID") %>">
											<i class="fa fa-reply replaySquare" title="Click to reply leave application"></i>
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
