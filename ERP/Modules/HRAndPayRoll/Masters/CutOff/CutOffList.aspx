<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CutOffList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.CutOffList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>CutOff Period</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/CutOffList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>CutOff Period</h3>

            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/CutOff/CutOffSave.aspx") %>">Add CutOff Period</a>
                    </span>
                </div>
            </div>
            &nbsp;&nbsp;
            <div class="addingbtn2">
                <div class="btn-group" style="margin-top:11px;">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/CutOff/GenerateCutOff.aspx") %>">Generate CutOff Periods</a>
                    </span>
                </div>
            </div>
        </div>
    </div>
    <!-- End Breadcrumbs line -->

    <!-- CutOff Period -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box employeeTable">
                <div class="widget-content">
                    <div class="headingOftabel">
                        <h4>List All <span>CutOff Period</span></h4>
                    </div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvList" OnPreRender="gvList_PreRender">
                                <Columns>
                                    <%--<asp:BoundField HeaderText="CutOff Code" DataField="CutOffCode" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />--%>
                                    <asp:TemplateField HeaderText="CutOff Code" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
									    <ItemTemplate>
										    <a class="" href="<%#"CutOffSave.aspx?id="+Eval("PayrollCutOffId") %>">
											    <%# Eval("CutOffCode") %>
										    </a>
									    </ItemTemplate>
								    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Actual Date" DataField="ActualDate" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Start Date" DataField="StartDate" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="End Date" DataField="EndDate" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a href="<%#"CutOffSave.aspx?id="+Eval("PayrollCutOffId") %>">
                                                <img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />
                                            </a>
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("PayrollCutOffId") %>' CssClass="btnDelete" OnClick="btnDelete_Click">
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
    <!-- End CutOff Period -->

</asp:Content>
