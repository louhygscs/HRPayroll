<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="SetEmployeeSchedule.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeSchedule.SetEmployeeSchedule" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Set Employee Schedule</title>
</asp:Content>

<asp:Content ID="cBoday" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Set Employee Schedule</h3>

            <div class="addingbtn">

                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSchedule/SetEmployeeSchedule.aspx") %>">Form View</a>
                    </span>                    
                </div>

                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeSchedule/SetEmployeeSchedule.aspx") %>">Grid View</a>
                    </span>                    
                </div>

            </div>
        </div>
    </div>
    <!-- End Breadcrumbs line -->

    <!-- Employee Schedule Form -->
    <div class="row">

        <div class="col-md-12">

            <div class="widget box employeesForms">

                <!-- widget header -->
                <div class="widget-header">
                    <div class="headingOftabel">
                        <%--<h2>Schedule <span>View</span></h2>--%>
                    </div>
                </div> <!-- end widget header -->

                <!-- widget container -->
                <div class="widget-content">

                    <!-- container -->
                    <div class="container">
                        
                        <!-- form -->
                        <form id="frmMain" runat="server">
                            
                            <!-- Worklocation & Cutoff -->
                            <div class="row gutters">

                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">

								    <div class="form-group">
                                        <asp:Button ID="btnSelect" runat="server" Text="Select All" CssClass="btn btn-primary" OnClick="btnSelect_Click"/>
                                        <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn" OnClick="btnClear_Click"/> <br /><br />
									    <label for="drpWorkLocation">Employee's List <span class="lblrequired">*</span></label>
                                        <asp:CheckBoxList ID="chkEmpList" runat="server"></asp:CheckBoxList>
								    </div>

							    </div>

							    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">

                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">

								        <div class="form-group">
									        <label for="drpWorkLocation">Work Location <span class="lblrequired">*</span></label>
									        <asp:DropDownList ID="drpWorkLocation" name="drpWorkLocation" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpWorkLocation_SelectedIndexChanged"></asp:DropDownList>
								        </div>

							        </div>

                                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">

								    <div class="form-group">
									    <label for="drpCutOffPeriod">CutOff Period <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpCutOffPeriod" name="drpCutOffPeriod" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>

							    </div>

							    </div>
                            </div>
                            <!-- End Worklocation & Cutoff -->

                            <!-- Schedule Form -->
                            
                            <!-- grid -->
                            <div class="row gutters">

                            </div>
                            <!-- grid -->

                            <!-- Weekends -->
                            <div class="row gutters">

                                <h4>Weekends</h4>
    
                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12">
								    <div class="form-group">
									    <label for="drpSunday">Sunday <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpSunday" name="drpSunday" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>
							    </div>

                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12">
								    <div class="form-group">
									    <label for="drpSaturday">Saturday <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpSaturday" name="drpSaturday" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>
							    </div>

                            </div>
                            <!-- End Weekends -->

                            <!-- Weekdays -->
                            <div class="row gutters">

                                <h4>Weekdays</h4>
    
                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12">
								    <div class="form-group">
									    <label for="drpMonday">Monday <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpMonday" name="drpMonday" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>
							    </div>

                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12">
								    <div class="form-group">
									    <label for="drpTuesday">Tuesday <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpTuesday" name="drpTuesday" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>
							    </div>

                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12">
								    <div class="form-group">
									    <label for="drpWednesday">Wednesday <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpWednesday" name="drpWednesday" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>
							    </div>

                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12">
								    <div class="form-group">
									    <label for="drpThursday">Thursday <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpThursday" name="drpThursday" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>
							    </div>

                                <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 col-12">
								    <div class="form-group">
									    <label for="drpFriday">Firday <span class="lblrequired">*</span></label>
									    <asp:DropDownList ID="drpFriday" name="drpFriday" runat="server" CssClass="form-control"></asp:DropDownList>
								    </div>
							    </div>
                            </div>
                            <!-- End Weekdays -->

                            <div class="row gutters">
                                <asp:Button ID="btnSetScheduke" runat="server" Text="Select All" CssClass="btn btn-primary" OnClick="btnSetScheduke_Click"/>
                            </div>
                            <!-- End Schedule Form -->

                        </form><!-- end form -->
                    </div> <!-- end container -->
                </div> <!-- end widget container -->
            </div>
        </div>

    </div>
    <!-- End Employee Schedule Form -->

</asp:Content>
