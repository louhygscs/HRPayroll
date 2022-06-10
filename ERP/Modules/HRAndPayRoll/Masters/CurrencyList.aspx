<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="CurrencyList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.CurrencyList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Currencies</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/CurrencyList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Currencies</h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/CurrencySave.aspx") %>">Add Currencies</a>
                    </span>

                </div>
            </div>
        </div>
    </div>

    <!-- Country Table -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content">
                    <div class="headingOftabel">
                        <h4>List All <span>Currencies</span></h4>
                    </div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvCurrency" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvCurrency" OnPreRender="gvCurrency_PreRender">
                            <Columns>
                                <asp:BoundField HeaderText="Code" DataField="CurrencyCode" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Symbol" DataField="CurrencySymbol" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"CurrencySave.aspx?id="+Eval("CurrencyID") %>">
                                            <img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

                                        </a>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("CurrencyID") %>' CssClass="btnDelete">
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
    <!-- End Country Table -->

</asp:Content>
