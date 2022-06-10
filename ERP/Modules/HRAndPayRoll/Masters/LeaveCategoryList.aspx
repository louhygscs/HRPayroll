<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="LeaveCategoryList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.LeaveCategoryList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Leave Category</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/LeaveCategoryList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Leave Category</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/LeaveCategorySave.aspx") %>">Add Leave Category</a>
					</span>
					
				</div>
			</div>
		</div>	
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
				
				
                <!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Leave Category</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                            <span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/LeaveCategorySave.aspx") %>">Add Leave Category</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Leave Category</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvLeaveCategory_PreRender" ID="gvLeaveCategory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gvLeaveCategory">
                            <Columns>
                                <asp:BoundField HeaderText="Leave Category" DataField="LeaveCategoryName" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"LeaveCategorySave.aspx?id="+Eval("LeaveCategoryID") %>">
											<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>

										</a>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("LeaveCategoryID") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
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
