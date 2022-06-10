<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DeductionList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.DeductionList" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Deduction</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/DeductionList.js")%>"></script>
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
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/DeductionList.aspx") %>" title="Deduction">Deduction</a>
            </li>
        </ul>
    </div> -->
     <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Deduction</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/DeductionSave.aspx") %>">Add Deduction</a>
					</span>
					
				</div>
			</div>
		</div>
    </div>
      <div class="row">
        <div class="col-md-12">
            <div class="widget box">
							
                <!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All New <span>Deduction</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                            <span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/DeductionSave.aspx") %>">Add Deduction</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Deduction</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView  ID="gvDeduction" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvDeduction" OnPreRender="gvDeduction_PreRender">
                            <Columns>
                                <asp:BoundField HeaderText="Deduction" DataField="DeductionName" HeaderStyle-Width="60%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="IsConsider" DataField="IsConsider" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"DeductionSave.aspx?id="+Eval("DeductionID") %>">
											<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>
										</a>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("DeductionID") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
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

