<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="ShiftList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.ShiftList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Shift</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/ShiftList.js")%>"></script>
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
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/ShiftList.aspx") %>" title="Shift">Shift</a>
            </li>
        </ul>

    </div> -->
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Shift</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/ShiftSave.aspx") %>">Add Shift</a>
					</span>
					
				</div>
			</div>
		</div>
        
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
				<!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Shift</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                           <span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/ShiftSave.aspx") %>">Add Shift</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Shift</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvShift_PreRender" ID="gvShift" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gvShift">
                            <Columns>
                                <asp:BoundField HeaderText="Shift" DataField="ShiftName" HeaderStyle-Width="50%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="From Time" DataField="FromTime" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                                 <asp:BoundField HeaderText="To Time" DataField="ToTime" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"ShiftSave.aspx?id="+Eval("ShiftID") %>">
											<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>
										</a>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("ShiftID") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
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
