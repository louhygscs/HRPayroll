<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="LeaveDetails.aspx.cs" Inherits="ERP.Modules.General.LeaveDetails" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Leave Details</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
 
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        
		 <div class="pageHeading">
			<h3>Leave Details</h3>
		
		</div>
		
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Leave <span>Details</span>
						</h4>
					</div>
                   
                   <!-- <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>-->
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:GridView ID="gvLeaveDetails" runat="server" AutoGenerateColumns="False" CssClass="table dataTable table-bordered gvLeaveDetails">
                            <Columns>
                                <asp:TemplateField HeaderText="Month" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("Month")+" - "+ Eval("Year") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Allowed Leave" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("AllowLeave", "{0:0.#}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Used Leave" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("TotalUseLeave", "{0:0.#}") %>
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
