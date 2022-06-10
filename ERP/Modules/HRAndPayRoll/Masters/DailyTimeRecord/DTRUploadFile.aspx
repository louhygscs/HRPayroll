<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DTRUploadFile.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.DailyTimeRecord.DTRUploadFile" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Upload Employee's Time Logs</title>
    <%--<script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeDTR.js")%>"></script>--%>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- breadcrumb -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Upload Employee's Time Logs</h3>
		</div>
    </div>
	<!-- end breadcrumb -->

    <!-- content row -->
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
                    
                    <div class="container">
                        
                        <!-- row -->
                        <div class="row gutters">

                            <!-- form -->
                            <form id="frmMain" runat="server">

                                <!-- control form -->
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
                                            <label>Choose a file <span class="lblrequired">*</span></label>
                                            <div class="col-md-12" style="margin-left:-15px !important; margin-top:-5px !important;">
				                                <div id="divUploadPhoto" runat="server" class="divUploadPhoto" style="display: block;">
					                                <asp:FileUpload ID="fuPhoto" runat="server" />
					                                <asp:RegularExpressionValidator ID="revPhoto"
						                                runat="server" ControlToValidate="fuPhoto"
						                                ErrorMessage="Please select correct Excel or Tab Delimeted extension file."
						                                ValidationExpression="(.*?)\.(xlsx|XLSX|txt|TXT)$"
						                                Display="Dynamic" CssClass="required"></asp:RegularExpressionValidator>
				                                </div>
				                                <div id="divViewPhoto" runat="server" class="divViewPhoto" style="display: none;">
					                                <asp:HiddenField ID="hfPhoto" runat="server" />
					                                <a href="javascript:;" id="btnDeletePhoto" class="btn btn-sm btnDeletePhoto"><i class="fa fa-trash" title="Click to delete photo."></i>&nbsp;Delete</a>
				                                </div>

			                                </div>
                                        </div>
                                    </div>

                                </div><!-- end control form -->

                                <!-- button -->
                                <div class="row gutters">
                                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 col-12">
										<div class="form-group">
											<asp:Button id="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
										</div>
									</div>
                                </div><!-- end button -->

                                <!-- Grid -->
                                <div class="row gutters">

                                </div><!-- end grid -->

                            </form>
                            <!-- end form -->

                        </div><!-- end row -->

                    </div><!-- end container -->

                </div><!-- end widget content -->

            </div><!-- end widget box -->

        </div> <!-- end col-md-12 -->

    </div><!-- end content row -->

</asp:Content>
