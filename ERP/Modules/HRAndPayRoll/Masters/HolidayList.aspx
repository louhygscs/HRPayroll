<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="HolidayList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.HolidayList" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Holiday</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/HolidayList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Holiday</h3>
		
            <div class="addingbtn">        
               
			</div>

			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">    
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/HolidaySave.aspx") %>">Add Holiday</a>
					</span>
				</div>
			</div>
		</div>
        
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
				
				
                <%--<div class="widget-header">
                    <h4><i class="fa fa-list"></i> All List <span>Holiday</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                            <span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/HolidaySave.aspx") %>">Add Holiday</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> --%>
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Holiday</span></h4>
					</div>
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
										Financial Year
									</label>
                                    <asp:DropDownList ID="drpFinancialYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:Button id="btnGenerateHoliday" runat="server" Text="Generate" OnClick="btnGenerateHoliday_Click"/>
                                </div>
                            </div>
                        </div>

                        <asp:GridView OnPreRender="gvHoliday_PreRender" ID="gvHoliday" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gvHoliday">
                            <Columns>
                                <asp:BoundField HeaderText="Title" DataField="Title" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Left" />
                               <asp:TemplateField HeaderText="Date Range" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("StartDate", "{0:MM/dd/yyyy}") %> - <%# Eval("EndDate", "{0:MM/dd/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"HolidaySave.aspx?id="+Eval("HolidayID") %>">
											<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>

										</a>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("HolidayID") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
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

