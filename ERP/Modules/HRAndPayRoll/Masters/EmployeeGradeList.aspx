<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeGradeList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeGradeList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Grade</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeGradeList.js")%>"></script>
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
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeGradeList.aspx") %>" title="Department">Employee Grade</a>
            </li>
        </ul>

    </div> -->
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee Classification</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeGradeSave.aspx") %>">Add Classification</a>
					</span>
					
				</div>
			</div>
		</div>
        
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
				
				
                <!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Employee Grade</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                           <span class="btn btn-xs addButtons">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeGradeSave.aspx") %>">Add Employee Grade</a>
                            </span>
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Employee Classification</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvEmployeeGrade" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gvEmployeeGrade" OnPreRender="gvEmployeeGrade_PreRender">
                            <Columns>
                                <asp:BoundField HeaderText="Employee Grade" DataField="EmployeeGradeName" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"EmployeeGradeSave.aspx?id="+Eval("EmployeeGradeID") %>">
											<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign"/>

										</a>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("EmployeeGradeID") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
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
