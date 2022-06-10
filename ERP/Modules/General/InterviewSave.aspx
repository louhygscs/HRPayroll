<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="InterviewSave.aspx.cs" Inherits="ERP.Modules.General.InterviewSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Interview</title>
     <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/General/InterviewSave.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Add Interview</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <div class="headingOftabel">
                        <h4>Save <span>Interview</span>
                        </h4>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
                                        Name <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtName" MaxLength="100" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvName" SetFocusOnError="true" ControlToValidate="txtName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Name." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Email <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtEmail" MaxLength="200" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmail" SetFocusOnError="true" ControlToValidate="txtEmail" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Email." runat="server"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please enter valid Email." ControlToValidate="txtEmail" SetFocusOnError="true" Display="Dynamic" CssClass="required" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Mobile <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtMobile" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge txtNumber"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvMobile" SetFocusOnError="true" ControlToValidate="txtMobile" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Mobile." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Education <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlEducation" runat="server" CssClass="form-control input-width-xlarge">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEducation" SetFocusOnError="true" ControlToValidate="ddlEducation" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Education." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Department <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control input-width-xlarge">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Department." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Designation <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control input-width-xlarge">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" SetFocusOnError="true" ControlToValidate="ddlDesignation" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Designation." runat="server"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Resume
                                    </label>
                                    <div class="col-md-12">
                                        <div id="divUploadResume" runat="server" class="divUploadResume" style="display: block;">
                                            <asp:FileUpload ID="fuResume" runat="server" />
                                        </div>
                                        <asp:RegularExpressionValidator
                                            ID="rvfuResume" runat="server" CssClass="required"
                                            ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                            ControlToValidate="fuResume" Display="Dynamic"></asp:RegularExpressionValidator>

                                        <div id="divViewResume" runat="server" class="divViewResume" style="display: none;">
                                            <asp:HiddenField ID="hfResume" runat="server" />
                                            <a id="btnViewResume" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                            <a href="javascript:;" class="btn btn-sm" onclick="InterviewSave.ShowHideDocument('.divViewResume','.divUploadResume');"><i class="fa fa-trash" title="Click to delete resume."></i>&nbsp;Delete</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Certificate
                                    </label>
                                    <div class="col-md-12">
                                        <div id="divUploadCertificate" runat="server" class="divUploadCertificate" style="display: block;">
                                            <asp:FileUpload ID="fuCertificate" runat="server" />
                                            <asp:RegularExpressionValidator
                                                ID="rvfuCertificate" runat="server" CssClass="required"
                                                ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                                ControlToValidate="fuCertificate" Display="Dynamic"></asp:RegularExpressionValidator>
                                        </div>
                                        <div id="divViewCertificate" runat="server" class="divViewCertificate" style="display: none;">
                                            <asp:HiddenField ID="hfCertificate" runat="server" />
                                            <a id="btnViewCertificate" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                            <a href="javascript:;" class="btn btn-sm" onclick="InterviewSave.ShowHideDocument('.divViewCertificate','.divUploadCertificate');"><i class="fa fa-trash" title="Click to delete Certificate."></i>&nbsp;Delete</a>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
                                        Current Salary
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtCurrentSalary" MaxLength="10" runat="server" onkeypress="return Common.isNumericKey(event,this)" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Expected Salary
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtExpectedSalary" MaxLength="10" runat="server" onkeypress="return Common.isNumericKey(event,this)" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Experience Year
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtExperienceYear" MaxLength="5" runat="server" onkeypress="return Common.isNumberKey(event,this)" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">
                                        Experience Month
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtExperienceMonth" MaxLength="5" runat="server" onkeypress="return Common.isNumberKey(event,this)" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="chkIsJoinDays" runat="server" Text="Joining by Days?" AutoPostBack="true" OnCheckedChanged="chkIsJoinDays_CheckedChanged" />
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label CssClass="col-md-12 control-label" runat="server" ID="lblMonthOrDays">
                                        Join After/Notice Period(Months)
                                    </asp:Label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtJoinAfterDaysOrMonth" MaxLength="5" runat="server" onkeypress="return Common.isNumberKey(event,this)" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-12 control-label">Personal Detail</label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtPersonalDetail" MaxLength="1000" runat="server" TextMode="MultiLine" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="widget-header">
                                <div class="headingOftabel">
                                    <h4><span>Status</span>
                                    </h4>
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group efirst">
                                    <label class="col-md-12 control-label">
                                        Status <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control input-width-xlarge" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Interview Scheduled</asp:ListItem>
                                            <asp:ListItem Value="3">Will Join</asp:ListItem>
                                            <asp:ListItem Value="4">Rejected</asp:ListItem>
                                            <asp:ListItem Value="5">Lack of Knowledge</asp:ListItem>
                                            <asp:ListItem Value="6">In Queue</asp:ListItem>
                                            <asp:ListItem Value="7">Salary Unexpected</asp:ListItem>
                                            <asp:ListItem Value="8">Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="divReason">
                                    <label class="col-md-12 control-label" runat="server" id="lblReason">
                                        Text <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <asp:TextBox ID="txtReason" MaxLength="1000" runat="server"  TextMode="MultiLine" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvtxtReason" SetFocusOnError="true" ControlToValidate="txtReason" CssClass="required" Display="Dynamic" ErrorMessage="Please Fill this Field." runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="divScheduleDate">
                                    <label class="col-md-12 control-label">
                                        Schedule Date <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <input type="text" runat="server" readonly="" id="txtInterviewDate" class="form-control input-width-xlarge txtInterviewDate" />
                                    </div>
                                </div>

                                <div class="form-group" runat="server" id="divScheduleTime">
                                    <label class="col-md-12 control-label">
                                        Schedule Time <span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <input type="text" runat="server" readonly="" id="txtInterviewTime" class="form-control input-width-xlarge txtInterviewTime" />
                                    </div>
                                </div>

                                <div class="form-group" runat="server" id="divJoinDate">
                                    <label class="col-md-12 control-label">
                                        Join Date<span class="required">*</span>
                                    </label>
                                    <div class="col-md-12">
                                        <input type="text" runat="server" readonly="" id="txtJoinDate" class="form-control input-width-xlarge txtJoinDate" />
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12"></div>
                        </div>
                        <div class="form-actions formActionbtn">
                            <asp:Button CssClass="btn btn-success " ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                            <a href="/Modules/General/InterviewList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
