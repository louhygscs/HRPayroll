<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeProfile.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeProfile" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Profile</title>

	<script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeProfileList.js")%>"></script>

</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->
    <div class="page-header">
        <%--<div class="pageHeading">
            <h3>Employee Profile</h3>
        </div>--%>
    </div>
    <!-- end BreadCrumbs line -->

    <!-- Profile  -->
    <div class="row">
        <div class="col-md-12">
            <!-- widget box -->
            <div class="widget box employessForms">
                <!-- widget header -->
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Employee <span>Profile</span></h4>
                    </div>
                </div>
                <!-- end widget header -->

                <!-- widget content -->
                <div class="widget-content">

				   <div class="container">
                       <div class="row gutters">

						   <form id="frmMain" runat="server">
						   <asp:HiddenField ID="hfId" runat="server" />

							<div class="col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12">
								<div class="card h-100">
									<div class="card-body">
										<div class="account-settings">
											<div class="user-profile">
												<div class="user-avatar">
													<%--<img src="https://bootdey.com/img/Content/avatar/avatar7.png" alt="Employee Picture" style="border:1px solid #000;padding:10px 5px 0px 5px;" />--%>
													<img id="imgPhoto" runat="server" alt="Employee Picture" style="border:1px solid #000;padding:10px 5px 0px 5px;" />
													<div class="row" style="margin-top:10px;">
														<div class="col-md-12 col-sm-12 col-xs-12">
															<div class="form-group">
																<div class="col-md-12">
																	<div id="divUploadPhoto" runat="server" class="divUploadPhoto" style="display: block;">
																		<asp:FileUpload ID="fuPhoto" runat="server" />
																		<asp:RegularExpressionValidator ID="revPhoto"
																			runat="server" ControlToValidate="fuPhoto"
																			ErrorMessage="Please select correct photo extension file."
																			ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$"
																			Display="Dynamic" CssClass="required"></asp:RegularExpressionValidator>
																	</div>
																	<div id="divViewPhoto" runat="server" class="divViewPhoto" style="display: none;">
																		<asp:HiddenField ID="hfPhoto" runat="server" />
																		
																		<a href="javascript:;" id="btnDeletePhoto" class="btn btn-sm btnDeletePhoto"><i class="fa fa-trash" title="Click to delete photo."></i>&nbsp;Delete</a>
																	</div>

																</div>
															</div>
														</div>
													</div>
												</div>
												<h5 class="user-name">
													<asp:Label ID="lblFullName" name="lblFullName" runat="server" Text="Full Name"></asp:Label>
												</h5>
												<h6 class="user-email">
													<asp:HyperLink ID="lnkEmail" name="lnkEmail" Text="employee@email.com" runat="server" OnPreRender="lnkEmail_PreRender"></asp:HyperLink>
												</h6>
											</div>
											<div class="about">
												<h5>About</h5>

												<asp:TextBox ID="txtAbout" name="txtAbout" runat="server" TextMode="MultiLine" CssClass="txtAbout"></asp:TextBox>
											</div>
										</div>
									</div>
								</div>
							</div>

							<div class="col-xl-9 col-lg-9 col-md-12 col-sm-12 col-12">
								<div class="card h-100">
									<div class="card-body">

										<ul class="nav nav-tabs">
											<li class="active"><a data-toggle="tab" href="#basic">Basic Information</a></li>
											<li><a data-toggle="tab" href="#schedule">Schedule</a></li>
											<li><a data-toggle="tab" href="#time">Timelogs</a></li>
											<li><a data-toggle="tab" href="#salary">Payslip Summary</a></li>
											<li><a data-toggle="tab" href="#history">History</a></li>
											<li><a data-toggle="tab" href="#document">Documents</a></li>
										</ul>

										<div class="tab-content">

											<div id="basic" class="tab-pane fade active in">
												
												<div class="accordion" id="accordionExample">

													<div class="editmode_container">
													
													</div>

													<!-- Personal -->
													<div class="card">
														<div class="card-header" id="Personal">
															<h2 class="mb-0">
																<button class="btn btn-link employeeAccor" type="button" data-toggle="collapse" data-target="#accOne" aria-expanded="true">
																	Personal <span>Information</span> <i class="arrow fa fa-angle-down"></i>
																</button>
															</h2>															
														</div>

													<div id="accOne" class="in" aria-expanded="true">
														<div class="card-body">
														
															<div class="row gutters">
															
																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtEmployeeNo">Employee No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtEmployeeNo" name="txtEmployeeNo" runat="server" CssClass="form-control" placeholder="Enter Employee No"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtStaffCode">Biometric No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtStaffCode" name="txtStaffCode" runat="server" CssClass="form-control" placeholder="Enter Biometric No"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtDateHired">Date Hired (mm/dd/yyyy) <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtDateHired" name="txtDateHired" runat="server" CssClass="form-control" placeholder="Enter Date Hired" TextMode="Date"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
															<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtFirstName">First Name <span class="lblrequired">*</span></label>
																	<asp:TextBox ID="txtFirstName" name="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter First Name"></asp:TextBox>
																</div>
															</div>

															<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtMiddleName">Middle Name</label>
																	<asp:TextBox ID="txtMiddleName" name="txtMiddleName" runat="server" CssClass="form-control" placeholder="Enter Middle Name"></asp:TextBox>
																</div>
															</div>

															<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtLastName">Last Name <span class="lblrequired">*</span></label>
																	<asp:TextBox ID="txtLastName" name="txtLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name"></asp:TextBox>
																</div>
															</div>

															</div>

															<div class="row gutters">

																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtGender">Gender <span class="lblrequired">*</span></label>
																	
																	<div class="col-md-12">
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rbtnMale" runat="server" Text="Male" GroupName="Gender" Checked="true" />
                                                        </label>
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rbtnFeMale" runat="server" Text="Female" GroupName="Gender" />
                                                        </label>
                                                    </div>

																</div>
															</div>

																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																  	<div class="form-group">
																  		<label for="drpMartialStatus">Martial Status <span class="lblrequired">*</span></label>
																  		<asp:DropDownList ID="drpMartialStatus" name="drpMartialStatus" runat="server" CssClass="form-control" placeholder="Enter Martial Status"></asp:DropDownList>
																  	</div>
																	</div>

																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																	  	<label for="txtDateMarriage">Date of Marriage (If Married)</label>
																	  	<asp:TextBox ID="txtMartialDate" name="txtMartialDate" runat="server" CssClass="form-control" placeholder="Enter Martial Date" TextMode="Date"></asp:TextBox>
																		
																	</div>
																	</div>

															</div>

															<div class="row gutters">

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtDateOfBirth">Date of Birth (mm/dd/yyyy) <span class="lblrequired">*</span></label>
																	<asp:TextBox ID="txtDateOfBirth" name="txtDateOfBirth" runat="server" CssClass="form-control" placeholder="Enter Date of Birth" TextMode="Date"></asp:TextBox>
																</div>
															</div>

															<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtBirthPlace">Birthplace (City)</label>
																	<asp:TextBox ID="txtBirthPlace" name="txtBirthPlace" runat="server" CssClass="form-control" placeholder="Enter Birth Place"></asp:TextBox>
																</div>
															</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtNoOfChildren">No of Children</label>
																	<asp:DropDownList ID="drpNoOfChildren" name="drpNoOfChildren" runat="server" CssClass="form-control" placeholder="Enter No of Children"></asp:DropDownList>
																</div>
															</div>

															</div>

															<div class="row gutters">

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtBasicPay">Basic Pay <span class="lblrequired">*</span></label>
																	<asp:TextBox ID="txtBasicPay" name="txtBasicPay" runat="server" CssClass="form-control" placeholder="Enter Basic Pay"></asp:TextBox>
																</div>
															</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtPaymentTerms">Payment Terms <span class="lblrequired">*</span></label>
																	<asp:DropDownList ID="drpPaymentTerms" name="drpPaymentTerms" runat="server" CssClass="form-control" placeholder="Enter Payment Terms"></asp:DropDownList>
																</div>
															</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																<div class="form-group">
																	<label for="txtEmployeeStatus">Employment Status <span class="lblrequired">*</span></label>
																	<asp:DropDownList ID="drpEmployeeStatus" name="txtEmployeeStatus" runat="server" CssClass="form-control" placeholder="Enter Employee Status"></asp:DropDownList>
																</div>
															</div>
														</div>

														</div>
													</div>

													</div>

													<!-- Address & Contact -->
													<div class="card">
													<div class="card-header" id="Address">
														<h2 class="mb-0">
														<button class="btn btn-link employeeAccor collapsed" type="button" data-toggle="collapse" data-target="#accTwo" aria-expanded="false" aria-controls="collapseTwo">
															Address &amp; <span>Contact</span> <i class="arrow fa fa-angle-down"></i>
														</button>
														</h2>
													</div>
													<div id="accTwo" class="in" aria-expanded="true">
														<div class="card-body">
															
															<div class="row gutters">
															
																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtHouseStreetNo">House &amp; Street No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtHouseStreetNo" name="txtHouseStreetNo" runat="server" CssClass="form-control" placeholder="Enter House Street No"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtBarangayTown">Barangay/Town/Village <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtBarangayTown" name="txtBarangayTown" runat="server" CssClass="form-control" placeholder="Enter Barangay/Town/Village"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtCityMunicipality">City/Municipality/Province</label>
																		<asp:TextBox ID="txtCityMunicipality" name="txtCityMunicipality" runat="server" CssClass="form-control" placeholder="Enter City/Municipality/Province"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="drpCountry">Country <span class="lblrequired">*</span></label>
																		<asp:DropDownList ID="drpCountry" name="drpCountry" runat="server" CssClass="form-control" placeholder="Enter Country" OnTextChanged="drpCountry_TextChanged"></asp:DropDownList>
																	</div>
																</div>

																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="drpRegion">Region <span class="lblrequired">*</span></label>
																		<asp:DropDownList ID="drpRegion" name="drpRegion" runat="server" CssClass="form-control" placeholder="Enter Region"></asp:DropDownList>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtPostalCode">Postal Code</label>
																		<asp:TextBox ID="txtPostalCode" name="txtPostalCode" runat="server" CssClass="form-control" placeholder="Enter Postal Code"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtHomeTelephoneNo">Home Telephone No</label>
																		<asp:TextBox ID="txtHomeTelephoneNo" name="txtHomeTelephoneNo" runat="server" CssClass="form-control" placeholder="Enter Home Telephone"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtMobileNo">Mobile No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtMobileNo" name="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Mobile No"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtEmailAddress">Email Address</label>
																		<asp:TextBox ID="txtEmailAddress" name="txtEmailAddress" runat="server" CssClass="form-control" placeholder="Enter Email Address"></asp:TextBox>
																	</div>
																</div>

															</div>

														</div>
													</div>
													</div>

													<!-- Education & Contract -->
													<div class="card">
													<div class="card-header" id="Contact">
														<h2 class="mb-0">
														<button class="btn btn-link employeeAccor collapsed" type="button" data-toggle="collapse" data-target="#accThree" aria-expanded="false" aria-controls="collapseTwo">
															Education &amp; <span>Contract</span> <i class="arrow fa fa-angle-down"></i>
														</button>
														</h2>
													</div>
													<div id="accThree" class="in" aria-expanded="true">
														<div class="card-body">
														<div class="row gutters">
															
																<div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
																	<div class="form-group">
																		<label for="txtHighEducAtt">Highest Educational Attainment</label>
																		<asp:TextBox ID="txtHighEducAtt" name="txtHighEducAtt" runat="server" CssClass="form-control" placeholder="Enter Highest Educational Attainment"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtSchool">School</label>
																		<asp:TextBox ID="txtSchool" name="txtSchool" runat="server" CssClass="form-control" placeholder="Enter School"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtYearCompleted">Years Completed</label>
																		<%--<asp:DropDownList ID="txtYearCompleted" name="txtYearCompleted" runat="server" CssClass="form-control" placeholder="Enter Year Completed"></asp:DropDownList>--%>
																		<asp:TextBox ID="txtYearCompleted" name="txtYearCompleted" runat="server" CssClass="form-control" placeholder="Enter Year Completed"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtDateCompleted">Date Completed</label>
																		<asp:TextBox ID="txtDateCompleted" name="txtDateCompleted" runat="server" CssClass="form-control" placeholder="Enter Date Completed" TextMode="Date"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtContractType">Contract Type <span class="lblrequired">*</span></label>
																		<asp:DropDownList ID="drpContractType" name="drpContractType" runat="server" CssClass="form-control" placeholder="Contract Type"></asp:DropDownList>
																	</div>
																</div>

																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtContractStartDate">Contract Start Date <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtContractStartDate" name="txtContractStartDate" runat="server" CssClass="form-control" placeholder="Enter Contract Start" TextMode="Date"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtContractEndDate">Contract End Date <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtContractEndDate" name="txtContractEndDate" runat="server" CssClass="form-control" placeholder="Enter Contract End" TextMode="Date"></asp:TextBox>
																	</div>
																</div>

															</div>

														</div>
													</div>
													</div>

													<!-- Organization -->
													<div class="card">
													<div class="card-header" id="Organization">
														<h2 class="mb-0">
														<button class="btn btn-link employeeAccor collapsed" type="button" data-toggle="collapse" data-target="#accFour" aria-expanded="false" aria-controls="collapseThree">
															Organization &amp; <span>Department</span><i class="arrow fa fa-angle-down"></i>
														</button>
														</h2>
													</div>
													<div id="accFour" class="in" aria-expanded="true">
														<div class="card-body">
															
															<div class="row gutters">
															
																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtPositionTitle">Designation/Job Title <span class="lblrequired">*</span></label>
																		<asp:DropDownList ID="drpPositionTitle" name="drpPositionTitle" runat="server" CssClass="form-control" placeholder="Enter Job Title"></asp:DropDownList>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtWorkLocation">Work Location <span class="lblrequired">*</span></label>
																		<asp:DropDownList ID="drpWorkLocation" name="drpWorkLocation" runat="server" CssClass="form-control" placeholder="Enter WorkL Location"></asp:DropDownList>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtFuncDepartment">Department/Function</label>
																		<asp:DropDownList ID="drpDepartment" name="drpDepartment" runat="server" CssClass="form-control" placeholder="Enter Department"></asp:DropDownList>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtCostCenter">Cost Center</label>
																		<asp:TextBox ID="txtCostCenter" name="txtCostCenter" runat="server" CssClass="form-control" placeholder="Enter Cost Center"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtTypeOfNCR">Type of NCR</label>
																		<asp:TextBox ID="txtTypeOfNCR" name="txtTypeOfNCR" runat="server" CssClass="form-control" placeholder="Enter Type of NCR"></asp:TextBox>
																	</div>
																</div>

															</div>

														</div>
													</div>
													</div>

													<!-- Government -->
													<div class="card">
													<div class="card-header" id="Government">
														<h2 class="mb-0">
														<button class="btn btn-link employeeAccor collapsed" type="button" data-toggle="collapse" data-target="#accFive" aria-expanded="false" aria-controls="collapseThree">
															Government <span>IDs</span> <i class="arrow fa fa-angle-down"></i>
														</button>
														</h2>
													</div>
													<div id="accFive" class="in" aria-expanded="true">
														<div class="card-body">

															<div class="row gutters">
															
																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtTINNo">TIN No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtTINNo" name="txtTINNo" runat="server" CssClass="form-control" placeholder="Enter TIN No"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtTaxExemption">Tax Exemption</label>
																		<asp:TextBox ID="txtTaxExemption" name="txtTaxExemption" runat="server" CssClass="form-control" placeholder="Enter Tax Exemption"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtSSSNo">SSS No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtSSSNo" name="txtSSSNo" runat="server" CssClass="form-control" placeholder="Enter SSS No"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
																	<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtPagIbigNo">Pag Ibig No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtPagIbigNo" name="txtPagIbigNo" runat="server" CssClass="form-control" placeholder="Enter Pag Ibig No"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtPhilHealthNo">Philhealth No</label>
																		<asp:TextBox ID="txtPhilHealthNo" name="txtPhilHealthNo" runat="server" CssClass="form-control" placeholder="Enter Philhealth No"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtDriverLicenseNo">Driver License No</label>
																		<asp:TextBox ID="txtDriverLicenseNo" name="txtDriverLicenseNo" runat="server" CssClass="form-control" placeholder="Enter Driver License No"></asp:TextBox>
																	</div>
																</div>

															</div>

														</div>
													</div>
													</div>

												</div>

												<div class="row gutters">
													<div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
														<div class="text-right">
															<a href="/Modules/HRAndPayRoll/Masters/EmployeeProfileList.aspx" class="btn">Cancel</a>
															<%--<asp:Button ID="BtnGenerate2" name="BtnGenerate2" runat="server" CssClass="btn btn-primary" Text="Generate" OnClick="BtnGenerate2_Click"/>--%>
															<asp:Button ID="BtnSave" name="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click"/>
														</div>
													</div>
												</div>

											</div>

											<div id="schedule" class="tab-pane fade">

												<div class="row gutters" style="margin-top:10px;">

													<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
														<div class="form-group">
															<label for="drpSchedCutOff">CutOff Period <span class="lblrequired">*</span></label>
															<asp:DropDownList ID="drpSchedCutOff" name="drpSchedCutOff" runat="server" CssClass="form-control"></asp:DropDownList>
														</div>
													</div>

													<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
														<div class="form-group" style="margin-top:15px;">
															<asp:Button id="btnGenerateSched" runat="server" Text="Load Schedule" CssClass="btn btn-primary" OnClick="btnGenerateSched_Click"/>
														</div>
													</div>

												</div>

												<div class="row gutters">
													<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
														<div class="form-group">
															<asp:Button id="btnSaveSched" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveSched_Click"/>
															
														</div>
													</div>
												</div>

												<div class="row gutters">

													<asp:GridView ID="grdEmpSched" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered grdEmpSched" OnRowDataBound="grdEmpSched_RowDataBound">
														<Columns>
															<asp:BoundField HeaderText="EmpShiftId" DataField="EmpShiftId" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" Visible="false" />
															<asp:BoundField HeaderText="Actual Date" DataField="ActualDate" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />
															<asp:BoundField HeaderText="Day Name" DataField="ActualDateDayName" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />
															
															<asp:TemplateField HeaderText="Shift" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
																<ItemTemplate>
																	<asp:HiddenField ID="hdDayShiftId" runat="server" Value='<%#Eval("ShiftId")%>' />
																	<asp:DropDownList ID="ddlDayShift" runat="server" CssClass="form-control"></asp:DropDownList>
																</ItemTemplate>
															</asp:TemplateField>

															<%--<asp:TemplateField HeaderText="Action" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
																<ItemTemplate>
																	<a href="<%#"DTRSave.aspx?id="+Eval("EmpShiftId") %>">
																	<img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

																	</a>
																	<asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("EmpShiftId") %>' CssClass="btnDelete">
																	<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove sign"/>

																	</asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>--%>

														</Columns>
														<EmptyDataRowStyle BackColor="#F9F9F9" />
													</asp:GridView>

												</div>

											</div>

											<div id="time" class="tab-pane fade">

												<div class="row gutters" style="margin-top:10px;">
													
													<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
														<div class="form-group">
															<label for="drpCutOffPeriod">CutOff Period <span class="lblrequired">*</span></label>
															<asp:DropDownList ID="drpCutOffPeriod" name="drpCutOffPeriod" runat="server" CssClass="form-control"></asp:DropDownList>
														</div>
													</div>

													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group" style="margin-top:15px;">
															<asp:Button id="btnGenerate" runat="server" Text="Load Timelogs" CssClass="btn btn-primary" OnClick="btnGenerate_Click"/>
														</div>
													</div>

												</div>

												<div class="row gutters">
													<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
														<div class="form-group">
															<asp:Button id="BtnSaveTimelogs" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="BtnSaveTimelogs_Click" />
															
														</div>
													</div>
												</div>

												<div class="row gutters">
													<asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvList" OnRowDataBound="gvList_RowDataBound">
														<Columns>
															<asp:BoundField HeaderText="Actual Date" DataField="ActualDate" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" />
															<asp:BoundField HeaderText="Day" DataField="ActualDateDayName" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" />
															<asp:TemplateField HeaderText="Schedule" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
																<ItemTemplate>
																	<asp:HiddenField ID="hdDayTimeLogShiftId" runat="server" Value='<%#Eval("ScheduleId")%>' />
																	<asp:DropDownList ID="ddlTimeLogDay1Schedule" runat="server" AutoPostBack="true" Enabled="false"></asp:DropDownList>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:BoundField HeaderText="TimeType" DataField="TimeType" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />

															<asp:TemplateField HeaderText="ActualTime" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
																<ItemTemplate>
																	<asp:TextBox id="txtActualTime" name="txtActualTime" runat="server" CssClass="timepicker"></asp:TextBox>
																	<%--<input type="text" class="timepicker" name="time"/>--%>
																</ItemTemplate>
															</asp:TemplateField>

															<asp:TemplateField HeaderText="Status" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
																<ItemTemplate>
																	<asp:DropDownList ID="ddlTimeLogStatus" runat="server" AutoPostBack="true">
																		<asp:ListItem Text="Absent" Value="Absent"></asp:ListItem>
																		<asp:ListItem Text="Present" Value="Present"></asp:ListItem>
																	</asp:DropDownList>
																</ItemTemplate>
															</asp:TemplateField>

															<%--<asp:BoundField HeaderText="ActualTime" DataField="ActualTime" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" />--%>

															<%--<asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
																<ItemTemplate>
																	<a class="lnkEditPopup">
																		<img  src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="resign sign"/>
																	</a>
																</ItemTemplate>
															</asp:TemplateField>--%>

														</Columns>
														<EmptyDataRowStyle BackColor="#F9F9F9" />
													</asp:GridView>
												</div>

												<div class="row gutters" style="text-align:right;">
													<asp:Label ID="lblTotalDays" runat="server" Text="Total Days :0" CssClass="totalLabel ttlday"></asp:Label><br />
													<asp:Label ID="lblTotalWrks" runat="server" Text="Total Working Hrs :0" CssClass="totalLabel ttlwrk"></asp:Label><br />
													<asp:Label ID="lblTotalLate" runat="server" Text="Total Late Hrs :0" CssClass="totalLabel ttllate"></asp:Label><br />
													<asp:Label ID="lblTotalOvertime" runat="server" Text="Total Overtime Hrs :0" CssClass="totalLabel ttlovertime"></asp:Label><br />
													<asp:Label ID="lblTotalAdjust" runat="server" Text="Total Adjustment Hrs :0" CssClass="totalLabel ttladjust"></asp:Label><br />
												</div>

											</div>

											<div id="salary" class="tab-pane fade">
												
												<div class="row gutters" style="margin-top:10px;">
													<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
														<div class="form-group">
															<label for="drpCutOffPeriod">CutOff Period <span class="lblrequired">*</span></label>
															<asp:DropDownList ID="drpSalary" name="drpSalary" runat="server" CssClass="form-control"></asp:DropDownList>
														</div>
													</div>
												
													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group" style="margin-top:16px;">
															<asp:Button id="btnGenSalary" runat="server" Text="Load Payslip" CssClass="btn btn-primary" OnClick="btnGenSalary_Click"/>
														</div>
													</div>
												</div>
												
												<div class="row gutters">
													<div class="col-md-12">
														<%--<asp:UpdatePanel ID="upMain" runat="server">
															<ContentTemplate>

																<div class="col-md-12">
																	<rsweb:ReportViewer ID="rvReportSalary" runat="server" Width="100%" Height="600px"></rsweb:ReportViewer>
																</div>

															</ContentTemplate>
														</asp:UpdatePanel> --%>
													</div>
												</div>

												<div class="row gutters">
													<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvList">
														<Columns>
															<asp:BoundField HeaderText="CutOff Code" DataField="CutOffCode" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:BoundField HeaderText="Start Date" DataField="StartDate" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:BoundField HeaderText="End Date" DataField="EndDate" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
																<ItemTemplate>
																	<a href="<%#"CutOffSave.aspx?id="+Eval("PayrollCutOffId") %>">
																	<img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

																	</a>
																	<asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("PayrollCutOffId") %>' CssClass="btnDelete">
																	<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove sign"/>

																	</asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
														<EmptyDataRowStyle BackColor="#F9F9F9" />
													</asp:GridView>
												</div>

											</div>

											<div id="history" class="tab-pane fade">
												<div class="row gutters" style="margin-top:10px;">
													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group">
															<asp:Button id="btnAddHistory" runat="server" Text="Add History(s)" CssClass="btn btn-primary"/>
														</div>
													</div>
												</div>

												<div class="row gutters">
													<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvList">
														<Columns>
															<asp:BoundField HeaderText="Log Date" DataField="LogDate" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:BoundField HeaderText="Note/Remarks" DataField="Remarks" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
																<ItemTemplate>
																	<a href="<%#"CutOffSave.aspx?id="+Eval("PayrollCutOffId") %>">
																	<img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

																	</a>
																	<asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("PayrollCutOffId") %>' CssClass="btnDelete">
																	<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove sign"/>

																	</asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
														<EmptyDataRowStyle BackColor="#F9F9F9" />
													</asp:GridView>
												</div>
											</div>

											<div id="document" class="tab-pane fade">
												
												<div class="row gutters" style="margin-top:10px;">
													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group">
															<asp:Button id="btnAddDocument" runat="server" Text="Add Document(s)" CssClass="btn btn-primary"/>
														</div>
													</div>
												</div>

												<div class="row gutters">
													<asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvDocuments">
														<Columns>
															<asp:BoundField HeaderText="Type" DataField="DocType" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:BoundField HeaderText="Title" DataField="DocTitle" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:BoundField HeaderText="Url" DataField="DocUrl" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />
															<asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
																<ItemTemplate>
																	<a href="<%#"CutOffSave.aspx?id="+Eval("PayrollCutOffId") %>">
																	<img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

																	</a>
																	<asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("PayrollCutOffId") %>' CssClass="btnDelete">
																	<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove sign"/>

																	</asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
														<EmptyDataRowStyle BackColor="#F9F9F9" />
													</asp:GridView>
												</div>
											</div>

										</div>

									</div>
								</div>
							</div>

							</form>
						</div>
				   </div>

                </div>
                <!-- end widget content -->

            </div>
        </div>
        <!-- end widget box -->
    </div>
    <!-- End Profile -->

</asp:Content>
