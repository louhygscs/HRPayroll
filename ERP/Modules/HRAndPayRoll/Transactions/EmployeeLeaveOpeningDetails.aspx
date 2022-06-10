<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeLeaveOpeningDetails.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeLeaveOpeningDetails" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Leave Opening Details</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeLeaveOpeningDetails.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Leave Opening Details</h3>
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Leave Opening Details</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> -->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Leave Opening Details</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvEmployeeLeaveOpeningDetails_PreRender" ID="gvEmployeeLeaveOpeningDetails" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvEmployeeLeaveOpeningDetails">
                            <Columns>
                                <asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Employee Name" DataField="FullName" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField HeaderText="Allowed Leave" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("TotalAllowLeave", "{0:0.#}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Used Leave" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("TotalUseLeave", "{0:0.#}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculate Leave" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("RemainingLeave", "{0:0.#}") %>
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

