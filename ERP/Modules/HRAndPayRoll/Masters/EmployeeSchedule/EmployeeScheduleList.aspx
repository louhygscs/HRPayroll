<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeScheduleList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeSchedule.EmployeeScheduleList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Schedule</title>
    <%--<script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeScheduleList.js")%>"></script>--%>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Employee Schedule</h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSchedule/SetEmployeeSchedule.aspx") %>">Set Employee Schedule</a>
                    </span>

                </div>
            </div>
        </div>
    </div>
    <!-- End Breadcrumbs line -->

    <!-- Employee Schedule -->
    <div class="row">
        <div class="col-md-12">
            <div class="widget box employessForms">

                <!-- widget header -->
                <div class="widget-header">
                    <div class="headingOftabel">
                        <%--<h4>Employee <span>Daily Time Summary</span></h4>--%>
                    </div>
                </div>
                <!-- end widget header -->

                <div class="widget-content">
                    
                    <!-- Container -->
                    <div class="container">

                        <!-- row -->
                        <div class="row gutters">
                            <form id="frmMain" runat="server">

                                <asp:HiddenField ID="hfId" runat="server" />

                                <!-- Filter -->
						        <div class="row gutters">

							        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
								        <div class="form-group">
									        <label for="drpWorkLocation">Work Location <span class="lblrequired">*</span></label>
									        <asp:DropDownList ID="drpWorkLocation" name="drpWorkLocation" runat="server" CssClass="form-control"></asp:DropDownList>
								        </div>
							        </div>

							        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
								        <div class="form-group">
									        <label for="drpCutOffPeriod">Cut Off Period <span class="lblrequired">*</span></label>
									        <asp:DropDownList ID="drpCutOffPeriod" name="drpCutOffPeriod" runat="server" CssClass="form-control"></asp:DropDownList>
								        </div>
							        </div>

							        <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
								        <div class="form-group">
									        <asp:Button id="btnEmpSchedGenerate" runat="server" Text="Generate" CssClass="btn btn-primary" OnClick="btnEmpSchedGenerate_Click"/>
                                            <asp:Button ID="btnDisplaySchedule" runat="server" Text="Load Schedule" CssClass="btn btn-primary" OnClick="btnDisplaySchedule_Click"/>
								        </div>
							        </div>

						        </div>

						        <!-- End Filter -->

                                <asp:GridView ID="gvEmpSchedule" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvEmpSchedule"
                                    OnRowDataBound="gvEmpSchedule_RowDataBound" 
                                    OnPreRender="gvEmpSchedule_PreRender">
                                    <Columns>
                                        <asp:BoundField HeaderText="EmpShiftId" DataField="EmpShiftId" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" Visible="false" />
                                        <asp:BoundField HeaderText="EmployeeId" DataField="CutOffId" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" Visible="false" />
                                        <asp:BoundField HeaderText="CutOffId" DataField="EmployeeNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" Visible="false"/>
                                        <asp:BoundField HeaderText="Employee No" DataField="EmployeeNo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Last Name" DataField="LastName" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="First Name" DataField="FirstName" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Middle Name" DataField="MiddleName" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:BoundField HeaderText="Designation" DataField="Designation" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />

                                        <asp:TemplateField HeaderText="Day 01" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay1Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay1Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 02" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList Id="ddlDay2Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay2Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 03" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay3Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay3Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 04" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay4Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay4Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 05" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay5Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay5Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 06" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay6Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay6Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 07" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay7Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay7Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 08" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay8Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay8Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 09" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay9Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay9Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 10" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay10Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay10Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 11" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay11Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay11Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 12" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay12Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay12Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 13" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay13Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay13Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day 14" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay14Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay14Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                
                                        <asp:TemplateField HeaderText="Day 15" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDay15Schedule" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDay15Schedule_SelectedIndexChanged"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle BackColor="#F9F9F9" />
                                </asp:GridView>
                            </form>
                        </div>
                        <!-- end row -->
                    </div>

                    

                </div>
            </div>
        </div>
    </div>

    <!-- end Employee Schedule -->

</asp:Content>
