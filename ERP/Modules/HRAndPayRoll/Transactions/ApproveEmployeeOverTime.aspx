<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="ApproveEmployeeOverTime.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.ApproveEmployeeOverTime" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Approve Employee OverTime</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Approve Employee OverTime</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content noFiltering">
                    <div class="headingOftabel">
                        <h4>All List <span>OverTime Employee</span></h4>
                    </div>
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager runat="server" ID="smMain"></asp:ScriptManager>
                        <asp:UpdatePanel ID="upMain" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-group efirst">
                                            <label class="col-md-12 control-label">
                                                <label class="col-md-12 control-label">
                                                    Month :
                                                </label>
                                                <div class="col-md-12">
                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control input-width-xlarge">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMonth" runat="server" SetFocusOnError="true" InitialValue=""
                                                        ControlToValidate="ddlMonth" ErrorMessage="Please select Month." Display="Dynamic" CssClass="required"
                                                        Text="Please select Month."> 
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label class="col-md-12 control-label">
                                                Employee :
                                            </label>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control input-width-xlarge">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" SetFocusOnError="true" InitialValue=""
                                                    ControlToValidate="ddlEmployee" ErrorMessage="Please select Employee." Display="Dynamic" CssClass="required"
                                                    Text="Please select Employee."> 
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-success" OnClick="btnReport_Click" Text="Generate Report" />
                                    </div>
                                </div>
                                <asp:GridView ID="gvDeviceAttendance" runat="server" AutoGenerateColumns="False" CssClass="table dataTable table-bordered gvDeviceAttendance">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttendanceDate" runat="server" Text='<%# Eval("AttendanceDateValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Attendances" DataField="Attendances" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Punch Method" DataField="PunchMethod" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Working Hours" DataField="WorkingHours" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Overtime Hours" DataField="OvertimeHours" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttendanceType"  runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Approve" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" CssClass='<%# Convert.ToBoolean(Eval("IsApproved"))?"displayNone":"" %>' ID="chkApprove" Checked='<%# Convert.ToBoolean(Eval("IsApproved"))?true:false %>' OnCheckedChanged="chkApprove_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
                                    </Columns>
                                    <EmptyDataRowStyle BackColor="#F9F9F9" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
