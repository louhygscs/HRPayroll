<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="InterviewList.aspx.cs" Inherits="ERP.Modules.General.InterviewList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Interview</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/General/InterviewList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Interview</h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <%--<span class="btn filterButton">
                        <span class="filtericon">
                            <img src="<%=ResolveUrl("~/Images/filter.svg") %>" class="svg" alt="filter sign" />
                        </span>
                        <a href="#">Filter</a>
                    </span>--%>
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/General/InterviewSave.aspx") %>">Add Interview</a>
                    </span>

                </div>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content">
                    <div class="headingOftabel">
                        <h4>All List <span>Interview</span></h4>
                    </div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvInterview" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered gvInterview" OnPreRender="gvInterview_PreRender">
                            <Columns>
                                <asp:BoundField HeaderText="Interview ID" DataField="InterviewNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Contact No" DataField="Mobile" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Education" DataField="Education" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Current Salary" DataField="CurrentSalary" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Expected Salary" DataField="ExpectedSalary" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Experience" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("ExperienceYear") %> Years <%# Eval("ExperienceMonth") %> Months
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Join After/Notice Period" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Eval("JoinAfterDaysOrMonth") %>  <%# Convert.ToBoolean(Eval("IsJoinDays"))?"Days":"Months" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# ERP.Common.GlobalHelper.GetEnumDescription((ERP.Common.InterviewStatus)Eval("InterviewStatusId")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"InterviewSave.aspx?id="+Eval("InterviewID") %>">
                                            <img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

                                        </a>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("InterviewID") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
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
