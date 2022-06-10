<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DTRSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.DailyTimeRecord.DTRSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee's Daily Time Record</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeDTR.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- breadcrumb -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee's Daily Time Record</h3>
		
			<%--<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeProfile.aspx") %>">Add Employees</a>
					</span>
					
				</div>
			</div>--%>

		</div>
    </div>
	<!-- end breadcrumb -->

	<!-- Profile -->
	<div class="row">
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
					<!-- form -->
					<form id="frmMain" runat="server">
						<asp:HiddenField ID="hfId" runat="server" />

						<!-- Profile Picture -->
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
						<!-- End Profile Picture -->

						<div>
							<div class="row gutters">
															
								<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
									<div class="form-group">
										<label for="txtEmployeeNo">Employee No <span class="lblrequired">*</span></label>
										<asp:TextBox ID="txtEmployeeNo" name="txtEmployeeNo" runat="server" CssClass="form-control" placeholder="Enter Employee No"></asp:TextBox>
									</div>
								</div>
							</div>
						</div>

					</form>
					<!-- end form -->
				</div>
			</div>
		</div>

		<!-- end widget content -->

	</div>
	<!-- end Profile -->

</asp:Content>
