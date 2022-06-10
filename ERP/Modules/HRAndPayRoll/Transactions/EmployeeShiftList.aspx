<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeShiftList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.EmployeeShiftList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Shift</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Transactions/EmployeeShiftList.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Employee Shift</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Employee Shift</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
                    <div class="headingOftabel">
                        <h4>All List <span>Employee Shift</span></h4>
                    </div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvEmployeeShift_PreRender" ID="gvEmployeeShift" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvEmployeeShift">
                            <Columns>
                                <asp:TemplateField HeaderText="Full Name" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span class='<%# Convert.ToBoolean(Eval("IsLeave"))?"redFont":"" %>'><%# Eval("FullName") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Emp. No" DataField="EmployeeNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />

                                <asp:BoundField HeaderText="Department" DataField="Department" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Shift" DataField="Shift" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="From Time" DataField="FromTime" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="To Time" DataField="ToTime" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a class='<%# Convert.ToBoolean(Eval("IsLeave"))?"displayNone":"" %>' href="<%#"EmployeeShiftSave.aspx?id="+Eval("EmployeeID") %>">
                                            <img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

                                        </a>
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
