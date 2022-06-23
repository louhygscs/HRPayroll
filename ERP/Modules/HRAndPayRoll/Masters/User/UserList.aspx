<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.User.UserList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>User</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/User/UserList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>User</h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/User/UserSave.aspx") %>">Add User</a>
                    </span>

                </div>
            </div>
        </div>
    </div>
    <!-- End Breadcrumbs -->

    <!-- User Table -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content">
                    <div class="headingOftabel">
                        <h4>List All <span>User</span></h4>
                    </div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvUser" OnPreRender="gvUser_PreRender">
                            <Columns>
                                <asp:BoundField HeaderText="Employee No" DataField="EmployeeNo" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Role" DataField="RoleName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Username" DataField="Username" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="First Name" DataField="FirstName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Middle Name" DataField="MiddleName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Last Name" DataField="LastName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"UserSave.aspx?id="+Eval("UserID") %>">
                                            <img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

                                        </a>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("UserID") %>' CssClass="btnDelete" OnClick="btnDelete_Click">
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
    <!-- End User Table -->

</asp:Content>
