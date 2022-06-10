<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeLoanList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeLoanList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Loan</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeLoanList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee Loan</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Transactions/EmployeeLoanSave.aspx") %>">Add Employee Loan</a>
					</span>
					
				</div>
			</div>
			
		</div>
        
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box employeTable">
                
				<!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Employee Loan</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                            <span class="btn btn-xs">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Transactions/EmployeeLoanSave.aspx") %>">Add Employee Loan</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> -->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Employee Loan</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvEmployeeLoan_PreRender" ID="gvEmployeeLoan" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gvEmployeeLoan">
                            <Columns>
                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsComplete"))?"greenFont":"" %>'><%# Eval("EmployeeName") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Title" DataField="LoanTitle" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Loan" DataField="Amount" HeaderStyle-Width="9%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Paid" DataField="PaidLoan" HeaderStyle-Width="9%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Pending" DataField="PendingLoan" HeaderStyle-Width="9%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Approved By" DataField="ApprovedBy" HeaderStyle-Width="13%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Total Months" DataField="TotalMonths" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Loan Date" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("LoanDate", "{0:MM/dd/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a class='<%# Convert.ToBoolean(Eval("PaidLoan"))?"displayNone":"" %>' href="<%#"EmployeeLoanSave.aspx?id="+Eval("EmployeeLoanID") %>">
											<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>

										</a>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("EmployeeLoanID") %>' OnClick="btnDelete_Click" CssClass='<%# Convert.ToBoolean(Eval("PaidLoan"))?"btnDelete displayNone":"btnDelete" %>'>
										<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove sign"/>

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
