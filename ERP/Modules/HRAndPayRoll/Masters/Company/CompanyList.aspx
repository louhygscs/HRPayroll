<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CompanyList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.Company.CompanyList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Company</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/CompanyList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Companies</h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/ClientProfile/ClientProfileSave.aspx") %>">Add Company</a>
                    </span>

                </div>
            </div>
        </div>
    </div>

    <!-- End Breadcrumbs line -->

    <!-- Company List -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content">
                    <div class="headingOftabel">
                        <h4>List All <span>Companies</span></h4>
                    </div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvCompany" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvCompany" OnPreRender="gvCompany_PreRender">
                            <Columns>
                                <asp:BoundField HeaderText="Company Name" DataField="CompanyName" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Email Address" DataField="EmailAddress" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="City" DataField="City" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Address" DataField="Address" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Phone No" DataField="PhoneNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Website" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <a href="<%#Eval("Website") %>" target="_blank">
                                            <%#Eval("Website") %>
                                        </a>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"CompanySave.aspx?id="+Eval("CompanyId") %>">
                                            <img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

                                        </a>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("CompanyId") %>' CssClass="btnDelete" OnClick="btnDelete_Click">
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
    <!-- End Company List -->

</asp:Content>
