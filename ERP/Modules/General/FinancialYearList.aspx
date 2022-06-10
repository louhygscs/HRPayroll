<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="FinancialYearList.aspx.cs" Inherits="ERP.Modules.General.FinancialYearList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Accounting Period</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/General/FinancialYearList.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Accounting Period</h3>
			<div class="addingbtn">
				<div class="btn-group">
					
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/General/FinancialYearSave.aspx") %>">Add Date Period</a>
					</span>
					
				</div>
			</div>
			
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
				
                <!--<div class="widget-header">
                    <h4>List All <span>Financial Year</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

							<span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/General/FinancialYearSave.aspx") %>">Add Financial Year</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>List All <span>Accounting Period</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvFinancialYear_PreRender" ID="gvFinancialYear"
                            runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvFinancialYear">
                            <Columns>
                                <asp:BoundField HeaderText="Accounting Period" DataField="FinancialYearText" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Year" DataField="Year"  HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("FinancialYearId") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
										<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove"/>
	
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
