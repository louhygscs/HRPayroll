<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="PSList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.PayrollSummary.PSList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Payroll Summary</title>

    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeProfileList.js")%>"></script>

</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- breadcrumb -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Payroll Summary</h3>
		
			<%--<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeProfile.aspx") %>">Generate Daily Time Record</a>
					</span>
					
				</div>
			</div>--%>

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

								   <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3 col-12">
										<div class="form-group">
											<label for="drpWorkLocation">Work Location <span class="lblrequired">*</span></label>
											<asp:DropDownList ID="drpWorkLocation" name="drpWorkLocation" runat="server" CssClass="form-control"></asp:DropDownList>
										</div>
									</div>

								   <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3 col-12">
										<div class="form-group">
											<label for="drpCutOffPeriod">Cut Off Period <span class="lblrequired">*</span></label>
											<asp:DropDownList ID="drpCutOffPeriod" name="drpCutOffPeriod" runat="server" CssClass="form-control"></asp:DropDownList>
										</div>
									</div>

								   <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3 col-12">
										<div class="form-group">
											<label for="drpPaymentTerms">Payment Terms <span class="lblrequired">*</span></label>
											<asp:DropDownList ID="drpPaymentTerms" name="drpPaymentTerms" runat="server" CssClass="form-control"></asp:DropDownList>
										</div>
									</div>

								    <div class="col-xl-3 col-lg-3 col-md-3 col-sm-4 col-12">
										<div class="form-group">
											<asp:Button id="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary" OnClick="btnGenerate_Click"/>
										</div>
									</div>

							   </div>
							   <!-- End Filter -->

							   <!-- Grid -->
							   <asp:GridView ID="gvPayrollSummaries" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvPayrollSummaries" OnPreRender="gvPayrollSummaries_PreRender">
									<Columns>
										
										<%--<asp:TemplateField HeaderText="Emp. No" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Left">
											<ItemTemplate>
												<a class="" href="<%#"PSDetail.aspx?id="+Eval("PayrollTimeId") %>">
													<%# Eval("EmployeeId") %>
												</a>
											</ItemTemplate>
										</asp:TemplateField>--%>

										<%--<asp:BoundField HeaderText="Date Hired" DataField="EmployeeId" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" />--%>
										<asp:BoundField HeaderText="Full Name" DataField="FullName" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Monthly Rate" DataField="gMonthlyRate" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Half Month Earnings" DataField="gHalfMonthEarning" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										
										<asp:BoundField HeaderText="RT OT Hrs" DataField="gRTOTHrs" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="RT OT Amt" DataField="gRTOTAmt" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										
										<asp:BoundField HeaderText="RD Hrs" DataField="gRDHrs" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="RD Amt" DataField="gRDAmt" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<%--<asp:BoundField HeaderText="RD OT Hrs" DataField="EmployeeId" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="RD OT Amt" DataField="EmployeeId" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />--%>
										
										<asp:BoundField HeaderText="SH Hrs" DataField="gSHHrs" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="SH Amt" DataField="gSHAmt" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<%--<asp:BoundField HeaderText="SH OT Hrs" DataField="EmployeeId" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="SH OT Amt" DataField="EmployeeId" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />--%>
										
										<asp:BoundField HeaderText="Position Allowance" DataField="gPosAllowance" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="SIL" DataField="gSIL" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />

										<asp:BoundField HeaderText="Payroll Adj" DataField="gAdjAmt" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="30% OVERTIME" DataField="g30PerOT" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />

										<%--<asp:BoundField HeaderText="Total Gross" DataField="EmployeeId" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />--%>

										<asp:BoundField HeaderText="CIGNA" DataField="dCigna" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="PHIC Prem" DataField="dPhicPrem" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="HDMF" DataField="dHDMF" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="HDMF Loan" DataField="dHDMFLoan" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<%--<asp:BoundField HeaderText="Payroll Adj/Absent" DataField="EmployeeId" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />--%>
										<asp:BoundField HeaderText="Motor Loan" DataField="dMotorLoan" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="CashAdvance" DataField="dCashAdv" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Paluwagan funds" DataField="dSplFunds" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />

										<asp:BoundField HeaderText="Total Deduction" DataField="TtlDeduction" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />
										<asp:BoundField HeaderText="Total Net Salary" DataField="TtlNetSalary" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" />

										<asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
											<ItemTemplate>
											   <a class="" href="<%#"PSDetail.aspx?id="+Eval("PayrollTimeId") %>">
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
