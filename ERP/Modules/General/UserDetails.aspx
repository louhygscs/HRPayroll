<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="ERP.Modules.General.UserDetails" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>User Details</title>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
  
    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>User Details</h3>
			
		</div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="widget box employessForms">
                <!--<div class="widget-header">
					<div class="headingOftabel">
						<h4>
							User <span>Details</span>
						</h4>
					</div>
                  
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <div class="col-md-12">
                            <div class="widget box">
                                <div class="widget-header">
									<div class="headingOftabel">
										<h4>
											Personal <span>Details</span>
										</h4>
									</div>
                                   
                                </div>
                                <div class="widget-content">
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group efirst">
												<label class="col-md-12 control-label">
													First Name 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblFirstName" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Middle Name 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblMiddleName" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Last Name 
												</label>
												<div class="col-md-12 control-label textLeft ">
													<asp:Label ID="lblLastName" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Father's Name 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblFatherName" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Date of Birth 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Gender 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblGender" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Marital Status 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Cast
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblCast" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Photo 
												</label>
												<div class="col-md-12">

													<img id="imgPhoto" runat="server" alt="Photo" class="viewImage" />

												</div>
											</div>
										</div>
										
									</div>
								</div>
                            </div>

                            <div class="widget box">
                                <div class="widget-header">
									<div class="headingOftabel">
										<h4>
											Official <span>Details</span>
										</h4>
									</div>
                                    
                                </div>
                                <div class="widget-content">
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group efirst">
												<label class="col-md-12 control-label">
													Employee Type 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblEmployeeType" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Department 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblDepartment" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Designation 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblDesignation" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Employee Grade
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblEmployeeGrade" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Join Date 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblJoinDate" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													PF Number
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblPFNumber" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Shift 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblShift" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-8 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Working Days 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblWorkingDays" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										
									</div>
								</div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="widget box">
                                <div class="widget-header">
									<div class="headingOftabel">
										<h4>
											Contact <span>Details</span>
										</h4>
									</div>
                                   
                                </div>
                                <div class="widget-content">
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group efirst">
												<label class="col-md-12 control-label">
													Country 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblCountry" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													State 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblState" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													City 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblCity" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Address 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblAddress" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Pin Code 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblPinCode" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Mobile 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblMobile" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Phone
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblPhone" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Email 
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblEmail" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										
									</div>
								</div>
                            </div>


                            <div class="widget box">
                                <div class="widget-header">
									<div class="headingOftabel">
										<h4>
											Bank <span>Information</span>
										</h4>
									</div>
                                    
                                </div>
                                <div class="widget-content">
									<div class="row">
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group efirst">
												<label class="col-md-12 control-label">
													Bank Name
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblBankName" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Branch Name
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblBranchName" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Account Name
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblAccountName" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Account Number
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
                                </div>
                            </div>
                            <div class="widget box">
                                <div class="widget-header">
									<div class="headingOftabel">
										<h4>
											Employee <span>Documents</span>
										</h4>
									</div>
                                    
                                </div>
                                <div class="widget-content">
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group efirst">
												<label class="col-md-12 control-label">
													Resume
												</label>
												<div class="col-md-12">
													<div id="divViewResume" runat="server" class="divViewResume" style="display: none;">

														<a id="btnViewResume" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
													</div>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Offer Letter
												</label>
												<div class="col-md-12">
													<div id="divViewOfferLetter" runat="server" class="divViewOfferLetter" style="display: none;">

														<a id="btnViewOfferLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
													</div>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Joining Letter
												</label>
												<div class="col-md-12">
													<div id="divViewJoiningLetter" runat="server" class="divViewJoiningLetter" style="display: none;">

														<a id="btnViewJoiningLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
													</div>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Contract Paper
												</label>
												<div class="col-md-12">
													<div id="divViewContractPaper" runat="server" class="divViewContractPaper" style="display: none;">

														<a id="btnViewContractPaper" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
													</div>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													ID Proof
												</label>
												<div class="col-md-12">
													<div id="divViewIDProff" runat="server" class="divViewIDProff" style="display: none;">

														<a id="btnViewIDProff" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
													</div>
												</div>
											</div>
										</div>
										<div class="col-md-4 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Other Document
												</label>
												<div class="col-md-12">
													<div id="divViewOtherDocument" runat="server" class="divViewOtherDocument" style="display: none;">

														<a id="btnViewOtherDocument" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
													</div>
												</div>
											</div>
										</div>
									</div>
                                </div>
                            </div>


                            <div id="divResignationInformation" runat="server" class="widget box">
                                <div class="widget-header">
                                    <h4><i class="fas fa-info-circle"></i>Resignation Information </h4>
                                </div>
                                <div class="widget-content">
									<div class="row">
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group efirst">
												<label class="col-md-12 control-label">
													Resign Letter
												</label>
												<div class="col-md-12">
													<div id="divResignLetter" runat="server" class="divResignLetter" style="display: none;">
														<a id="btnViewResignLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
													</div>
												</div>
											</div>
										</div>
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group">
												<label class="col-md-12 control-label">
													Branch Name
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="Label2" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
									<div class="row">
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group ">
												<label class="col-md-12 control-label">
													Leave Date
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblLeaveDate" runat="server"></asp:Label>
												</div>
											</div>
										</div>
										<div class="col-md-6 col-sm-6 col-xs-12">
											<div class="form-group ">
												<label class="col-md-12 control-label">
													Leave Description
												</label>
												<div class="col-md-12 control-label textLeft">
													<asp:Label ID="lblLeaveDescription" runat="server"></asp:Label>
												</div>
											</div>
										</div>
									</div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
