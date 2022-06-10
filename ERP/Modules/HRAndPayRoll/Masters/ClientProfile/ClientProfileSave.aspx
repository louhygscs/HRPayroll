<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="ClientProfileSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.ClientProfile.ClientProfileSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Client Profile</title>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- Breadcrumbs line -->
    <div class="page-header">
        <%--<div class="pageHeading">
            <h3>Employee Profile</h3>
        </div>--%>
    </div>
    <!-- end BreadCrumbs line -->

    <!-- Profle -->
    <div class="row">
        <div class="col-md-12">
            <!-- widget box -->
            <div class="widget box employessForms">
                <!-- widget header -->
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Client <span>Profile</span></h4>
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
													<img src="https://bootdey.com/img/Content/avatar/avatar7.png" alt="Employee Picture" style="border:1px solid #000;padding:10px 5px 0px 5px;" />
													
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
																		<img id="imgPhoto" runat="server" alt="Photo" class="viewImage" />
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
													<asp:HyperLink ID="lnkEmail" name="lnkEmail" Text="employee@email.com" runat="server"></asp:HyperLink>
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
											<li><a data-toggle="tab" href="#time">Invoice Summary</a></li>
											<li><a data-toggle="tab" href="#salary">Account Receivable</a></li>
											<li><a data-toggle="tab" href="#history">History</a></li>
											<li><a data-toggle="tab" href="#document">Documents</a></li>
										</ul>

										<div class="tab-content">

											<div id="basic" class="tab-pane fade in active">
												
												<div class="accordion" id="accordionExample">

													<div class="editmode_container">
													
													</div>

													<!-- Personal -->
													<div class="card">
														<div class="card-header" id="Personal">
															<h2 class="mb-0">
																<button class="btn btn-link employeeAccor" type="button" data-toggle="collapse" data-target="#accOne" aria-expanded="true" aria-controls="collapseOne">
																	Company <span>Information</span> <i class="arrow fa fa-angle-down"></i>
																</button>
															</h2>															
														</div>

													<div id="accOne" class="in" aria-labelledby="headingOne" data-parent="#accordionExample">
														<div class="card-body">
														
															<div class="row gutters">
															
																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtCompanyName">Company Name <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtCompanyName" name="txtCompanyName" runat="server" CssClass="form-control" placeholder="Enter Company Name"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="drpCategory">Category <span class="lblrequired">*</span></label>
																		<asp:DropDownList ID="drpCategory" name="drpCategory" runat="server" CssClass="form-control" placeholder="Enter First Name"></asp:DropDownList>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtEmailAddress">Email Address<span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtEmailAddress" name="txtEmailAddress" runat="server" CssClass="form-control" placeholder="Enter Email Address" TextMode="Email"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
															
																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="drpCountry">Country</label>
																		<asp:DropDownList ID="drpCountry" name="drpCountry" runat="server" CssClass="form-control"></asp:DropDownList>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="drpState">Region <span class="lblrequired">*</span></label>
																		<asp:DropDownList ID="drpState" name="drpState" runat="server" CssClass="form-control"></asp:DropDownList>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																	  	<label for="txtCity">City</label>
																	  	<asp:TextBox ID="txtCity" name="txtCity" runat="server" CssClass="form-control" placeholder="Enter City"></asp:TextBox>																		
																	</div>
																</div>
															</div>

															<div class="row gutters">
																<div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
																	<div class="form-group">
																		<label for="txtAddress">Address</label>
																		<asp:TextBox ID="txtAddress" name="txtMartialDate" runat="server" CssClass="form-control" placeholder="Enter Address"></asp:TextBox>																		
																	</div>
																</div>
															</div>

															<div class="row gutters">

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtMobileNo">Mobile No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtMobileNo" name="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Date of Birth"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtPhoneNo">Phone No</label>
																		<asp:TextBox ID="txtPhoneNo" name="txtPhoneNo" runat="server" CssClass="form-control" placeholder="Enter Birth Place"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtHotlineNo">Hotline No</label>
																		<asp:TextBox ID="txtHotlineNo" name="txtHotlineNo" runat="server" CssClass="form-control" placeholder="Enter No of Children"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtFaxNo">Fax No <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtFaxNo" name="txtFaxNo" runat="server" CssClass="form-control" placeholder="Enter Basic Pay" TextMode="Number"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtWebsite">Website <span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtWebsite" name="txtWebsite" runat="server" CssClass="form-control" placeholder="Enter Payment Terms"></asp:TextBox>
																	</div>
																</div>

																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtTINNo">TIN No<span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtTINNo" name="txtTINNo" runat="server" CssClass="form-control" placeholder="Enter Employee Status"></asp:TextBox>
																	</div>
																</div>

															</div>

															<div class="row gutters">
																<div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
																	<div class="form-group">
																		<label for="txtBusinessPermitNo">Business Permit No<span class="lblrequired">*</span></label>
																		<asp:TextBox ID="txtBusinessPermitNo" name="txtBusinessPermitNo" runat="server" CssClass="form-control" placeholder="Enter Employee Status"></asp:TextBox>
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
															<button type="button" id="submit" name="submit" class="btn btn-secondary">Cancel</button>
															<button type="button" id="submit" name="submit" class="btn btn-primary">Update</button>
															<asp:Button ID="BtnSave" name="BtnSave" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnSave_Click"/>
														</div>
													</div>
												</div>

											</div>

											<div id="time" class="tab-pane fade">

												<div class="row gutters" style="margin-top:10px;">
													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group">
															<label for="drpCutOffPeriod">CutOff Period <span class="lblrequired">*</span></label>
															<asp:DropDownList ID="drpCutOffPeriod" name="drpCutOffPeriod" runat="server" CssClass="form-control">
																<asp:ListItem Text="Jan10-Jan25-2021"></asp:ListItem>
																<asp:ListItem Text="Jan26-Feb09-2021"></asp:ListItem>
																<asp:ListItem Text="Feb10-Feb25-2021"></asp:ListItem>
															</asp:DropDownList>
														</div>
													</div>
												</div>

												<div class="row gutters">
													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group">
															<asp:Button id="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-primary"/>
														</div>
													</div>
												</div>

												<div class="row gutters">
													<asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvList">
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

											<div id="salary" class="tab-pane fade">
												
												<div class="row gutters" style="margin-top:10px;">
													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group">
															<label for="drpCutOffPeriod">CutOff Period <span class="lblrequired">*</span></label>
															<asp:DropDownList ID="drpSalary" name="drpSalary" runat="server" CssClass="form-control">
																<asp:ListItem Text="Jan10-Jan25-2021"></asp:ListItem>
																<asp:ListItem Text="Jan26-Feb09-2021"></asp:ListItem>
																<asp:ListItem Text="Feb10-Feb25-2021"></asp:ListItem>
															</asp:DropDownList>
														</div>
													</div>
												</div>

												<div class="row gutters">
													<div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
														<div class="form-group">
															<asp:Button id="btnGenSalary" runat="server" Text="Generate" CssClass="btn btn-primary"/>
														</div>
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
    <!-- end Profile -->

</asp:Content>
