<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationList.aspx.cs" Inherits="ERP.Modules.General.LeaveApplicationList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Leave Application</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/General/LeaveApplicationList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        
		<div class="pageHeading">
			<h3>Add Leave Application</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<svg xmlns="http://www.w3.org/2000/svg" width="10.224" height="10.225" viewBox="0 0 10.224 10.225">
							  <g id="Group_288" data-name="Group 288" transform="translate(-250.888 -250.811)">
								<g id="Group_287" data-name="Group 287">
								  <path id="Path_420" data-name="Path 420" d="M260.26,255.071h-3.408v-3.408a.852.852,0,0,0-1.7,0v3.408H251.74a.852.852,0,1,0,0,1.7h3.408v3.409a.852.852,0,1,0,1.7,0v-3.409h3.408a.852.852,0,0,0,0-1.7Z" fill="#fff"/>
								</g>
							  </g>
							</svg>
						</span>
						<a href="<%=ResolveUrl("~/Modules/General/ApplyLeaveApplication.aspx") %>">Apply Leave Application</a>
					</span>
					
				</div>
			</div>
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box employeTable">
                				
				 <!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Leave Application<span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">

                           <span class="btn btn-xs">
                                <i class="fa fa-plus"></i>
                                <a href="<%=ResolveUrl("~/Modules/General/ApplyLeaveApplication.aspx") %>">Apply Leave Application</a>
                            </span> 
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>
							Leave <span>Application</span>
						</h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvLeaveApplication_PreRender" ID="gvLeaveApplication"
                            OnRowDataBound="gvLeaveApplication_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvLeaveApplication">
                            <Columns>
                                <asp:TemplateField HeaderText="Leave Category" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsApprove"))?"greenFont":"" %>'><%# Eval("LeaveCategory") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date Range" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("StartDate", "{0:MM/dd/yyyy}") %> - <%# Eval("EndDate", "{0:MM/dd/yyyy}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Leave" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("TotalDay", "{0:0.#}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Apply Date" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("ApplyDate", "{0:yyyy-MM-dd}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reply Date" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Convert.ToString(Eval("Comments")))?"": Eval("ApproveDate", "{0:yyyy-MM-dd}") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Reason" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReason" runat="server" Text='<%#Eval("Reason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Response" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResponse" runat="server" Text='<%#Eval("Comments") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#  string.IsNullOrEmpty(Convert.ToString(Eval("Comments")))?Eval("Status"): Eval("Status")+" By "+Eval("ApprovedBy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a class="<%# Convert.ToString(Eval("Status"))=="Approve"?"displayNone":""%>" href="<%#"ApplyLeaveApplication.aspx?id="+Eval("EmployeeLeaveCategoryMapID") %>">
											<svg xmlns="http://www.w3.org/2000/svg" width="12.647" height="13.797" viewBox="0 0 12.647 13.797">
											  <g id="Group_295" data-name="Group 295" transform="translate(0)">
												<g id="Group_294" data-name="Group 294">
												  <path id="Path_429" data-name="Path 429" d="M259.124,255.226a.538.538,0,0,0-.539-.538h-5.551a.539.539,0,1,0,0,1.077h5.551A.539.539,0,0,0,259.124,255.226Z" transform="translate(-250.237 -249.298)" fill="#37c12f"/>
												  <path id="Path_430" data-name="Path 430" d="M253.034,256.563a.539.539,0,1,0,0,1.077h3.371a.539.539,0,1,0,0-1.077Z" transform="translate(-250.266 -249.017)" fill="#37c12f"/>
												  <path id="Path_431" data-name="Path 431" d="M254.684,262.719h-1.849a1.094,1.094,0,0,1-1.106-1.078v-9.485a1.093,1.093,0,0,1,1.106-1.077h6.8a1.092,1.092,0,0,1,1.105,1.077v3.315a.553.553,0,0,0,1.106,0v-3.315A2.186,2.186,0,0,0,259.631,250h-6.8a2.186,2.186,0,0,0-2.211,2.156v9.485a2.186,2.186,0,0,0,2.211,2.156h1.849a.539.539,0,1,0,0-1.077Z" transform="translate(-250.623 -250)" fill="#37c12f"/>
												  <path id="Path_432" data-name="Path 432" d="M261.753,256.849a1.619,1.619,0,0,0-2.286,0l-2.959,2.952a.544.544,0,0,0-.135.225l-.645,2.121a.538.538,0,0,0,.516.7.555.555,0,0,0,.144-.02l2.175-.6a.543.543,0,0,0,.237-.138l2.952-2.947A1.618,1.618,0,0,0,261.753,256.849Zm-3.614,4.37-1.094.3.321-1.054,2-1.992.762.762Zm2.852-2.847-.1.1-.762-.762.1-.1a.539.539,0,0,1,.763.761Z" transform="translate(-249.58 -249.045)" fill="#37c12f"/>
												  <path id="Path_433" data-name="Path 433" d="M258.585,252.813h-5.551a.539.539,0,1,0,0,1.077h5.551a.539.539,0,1,0,0-1.077Z" transform="translate(-250.237 -249.579)" fill="#37c12f"/>
												</g>
											  </g>
											</svg>

										</a> 
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("EmployeeLeaveCategoryMapID") %>' OnClick="btnDelete_Click" CssClass='<%# string.IsNullOrEmpty(Convert.ToString(Eval("Comments")))?"btnDelete":"displayNone"%>'>
										<svg id="remove" xmlns="http://www.w3.org/2000/svg" width="12.072" height="13.797" viewBox="0 0 12.072 13.797">
										  <path id="Path_425" data-name="Path 425" d="M251.5,261.622a1.727,1.727,0,0,0,1.725,1.725h6.9a1.726,1.726,0,0,0,1.725-1.725V253H251.5Z" transform="translate(-250.637 -249.55)" fill="#ff4e00"/>
										  <path id="Path_426" data-name="Path 426" d="M258.51,250.861V250H255.06v.862h-4.311v1.725h12.072v-1.725Z" transform="translate(-250.749 -249.999)" fill="#ff4e00"/>
										</svg>

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
