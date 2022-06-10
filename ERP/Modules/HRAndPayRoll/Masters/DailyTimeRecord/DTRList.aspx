<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DTRList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.DailyTimeRecord.DTRList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>DTR Summary</title>
	<script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/DTRList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- breadcrumb -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee's Daily Time Summary</h3>
		
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/DailyTimeRecord/DTRUploadFile.aspx") %>">Upload Timelogs</a>
					</span>
					
				</div>
			</div>
		</div>
    </div>
	<!-- end breadcrumb -->

	<!-- Content Row -->
	<div class="row">
		<div class="col-md-12">
            <!-- widget box -->
            <div class="widget box employessForms">
                
				<!-- widget header -->
                <div class="widget-header">
                    <div class="headingOftabel">
                        <%--<h4>Employee <span>Daily Time Summary</span></h4>--%>
                    </div>
                </div>
                <!-- end widget header -->

                <!-- widget content -->
                <div class="widget-content">

				   <!-- Container -->
				   <div class="container">
                       
					   <!-- row -->
					   <div class="row gutters">

						   <!-- Form -->
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
										<div class="form-group" style="margin-top:15px;">
											<asp:Button id="btnGenerate" runat="server" Text="Load Timelogs" CssClass="btn btn-primary" OnClick="btnGenerate_Click"/>
											<asp:Button id="btnGeneratePayrollDTR" runat="server" Text="Generate DTR" CssClass="btn btn-primary" OnClick="btnGeneratePayrollDTR_Click"/>
										</div>
									</div>

							   </div>
							   <!-- End Filter -->
							   
							   <!-- Grid Daily Time Logs -->
							   <div class="grdVW">
								   <asp:GridView ID="gvDTRDailyTimelogs" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvDTRDailyTimelogs">
										<Columns>
											<asp:BoundField HeaderText="Employee No"	DataField="EmployeeNo" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Employee Name"  DataField="FullName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day1TimeIn"  DataField="Day1TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day1TimeOut" DataField="Day1TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day1TtlHrs"  DataField="Day1TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day2TimeIn"  DataField="Day2TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day2TimeOut" DataField="Day2TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day2TtlHrs"  DataField="Day2TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day3TimeIn"  DataField="Day3TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day3TimeOut" DataField="Day3TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day3TtlHrs"  DataField="Day3TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day4TimeIn"  DataField="Day4TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day4TimeOut" DataField="Day4TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day4TtlHrs"  DataField="Day4TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day5TimeIn"  DataField="Day5TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day5TimeOut" DataField="Day5TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day5TtlHrs"  DataField="Day5TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day6TimeIn"  DataField="Day6TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day6TimeOut" DataField="Day6TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day6TtlHrs"  DataField="Day6TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day7TimeIn"  DataField="Day7TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day7TimeOut" DataField="Day7TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day7TtlHrs"  DataField="Day7TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day8TimeIn"  DataField="Day8TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day8TimeOut" DataField="Day8TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day8TtlHrs"  DataField="Day8TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day9TimeIn"  DataField="Day9TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day9TimeOut" DataField="Day9TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day9TtlHrs"  DataField="Day9TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day10TimeIn"  DataField="Day10TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day10TimeOut" DataField="Day10TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day10TtlHrs"  DataField="Day10TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day11TimeIn"  DataField="Day11TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day11TimeOut" DataField="Day11TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day11TtlHrs"  DataField="Day11TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day12TimeIn"  DataField="Day12TimeIn" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day12TimeOut" DataField="Day12TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day12TtlHrs"  DataField="Day12TtlHrs" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day13TimeIn"  DataField="Day13TimeIn"  HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day13TimeOut" DataField="Day13TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day13TtlHrs"  DataField="Day13TtlHrs"  HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day14TimeIn"  DataField="Day14TimeIn"  HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day14TimeOut" DataField="Day14TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day14TtlHrs"  DataField="Day14TtlHrs"  HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

											<asp:BoundField HeaderText="Day15TimeIn"  DataField="Day15TimeIn"  HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day15TimeOut" DataField="Day15TimeOut" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
											<asp:BoundField HeaderText="Day15TtlHrs"  DataField="Day15TtlHrs"  HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

										</Columns>
								   </asp:GridView>
							   </div>
							   <!-- End Grid Daily Time Logs -->

							   <!-- Grid Computed Hrs -->
							   <asp:GridView ID="gvDTRDaily" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvDTRDaily">
									<Columns>
										
										<asp:TemplateField HeaderText="Emp. No" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<a class="" href="<%#"EmployeeProfile.aspx?id="+Eval("EmployeeId") %>">
													<%# Eval("EmployeeNo") %>
												</a>
											</ItemTemplate>
										</asp:TemplateField>

										<%--<asp:BoundField HeaderText="Date Hired" DataField="DateHired" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />--%>
										<asp:BoundField HeaderText="Employee Name" DataField="FullName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />

										<asp:BoundField HeaderText="Actual Date" DataField="ActualDate" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
										<%--<asp:BoundField HeaderText="TimeIn" DataField="EmployeeId" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="TimeOut" DataField="EmployeeId" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Status" DataField="EmployeeId" HeaderStyle-Width="6%" HeaderStyle-HorizontalAlign="Left" />--%>
										<asp:BoundField HeaderText="Working Hrs" DataField="WorkingHrs" HeaderStyle-Width="14%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Late Hrs" DataField="LateHrs" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Overtime Hrs" DataField="OvertimeHrs" HeaderStyle-Width="14%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Adjust Hrs" DataField="AdjustHrs" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />

										<asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
											<ItemTemplate>
											   <a class="" href="<%#"EmployeeResignSave.aspx?id="+Eval("EmployeeId") %>">
												<img  src="<%=ResolveUrl("~/Images/resign.svg") %>" class="svg" alt="resign sign"/>
											   </a>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
									<EmptyDataRowStyle BackColor="#F9F9F9" />
							   </asp:GridView>
							   <!-- End Grid -->

						   </form>
						   <!-- End Form -->

						</div>
					   <!-- end row -->

				   </div>
				   <!-- End Container -->

                </div>
                <!-- end widget content -->

            </div>
        </div>
        <!-- end widget box -->
	</div>
	<!-- End Content Row -->

</asp:Content>
