<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true"
    CodeBehind="EmployeeSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee</title>
    <link href="<%=ResolveUrl("~/Styles/bootstrap-datepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/bootstrap-datepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeSave.js")%>"></script>

    <script type="text/javascript">
        function ValidateWorkingDays(sender, args) {
            var checkBoxList = document.getElementById("<%=chkListWorkingDays.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>

</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Add New Employee
                <!--<span>- 65</span>-->
            </h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="#">Add Company</a>
                    </span>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box employessForms">
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <script src="<%=ResolveUrl("~/Scripts/Loader.js")%>"></script>
                        <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                        <asp:HiddenField ID="hfId" runat="server" />
                        <!--<div class="form-group efirst">
                            <label class="col-md-2 control-label" style="width:auto;">
                                Employee No :
                            </label>
                            <div class="col-md-10 control-label textLeft" style="width:auto;">
                                <label runat="server" id="lblEmployeeNo">0</label>
                            </div>
                        </div> -->
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Personal <span>Information</span>
                                            </h4>
                                        </div>

                                    </div>
                                    <div class="widget-content">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        First Name <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtFirstName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvFirstName" SetFocusOnError="true" ControlToValidate="txtFirstName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter First Name." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Middle Name 
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtMiddleName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Last Name <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtLastName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvLastName" SetFocusOnError="true" ControlToValidate="txtLastName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Last Name." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Gender <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rbtnMale" runat="server" Text="Male" GroupName="Gender" Checked="true" />
                                                        </label>
                                                        <label class="radio-inline">
                                                            <asp:RadioButton ID="rbtnFeMale" runat="server" Text="FeMale" GroupName="Gender" />
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Date of Birth <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12 date-select">
                                                        <input type="text" runat="server" readonly="" id="txtBirthDate" class="form-control input-width-xlarge txtBirthDate" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Maratial Status <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList ID="ddlMaratialStatus" runat="server" CssClass="form-control input-width-xlarge">
                                                            <asp:ListItem Value="">-- Select --</asp:ListItem>
                                                            <asp:ListItem Value="Single">SINGLE</asp:ListItem>
                                                            <asp:ListItem Value="Married">MARRIED</asp:ListItem>
                                                            <asp:ListItem Value="Widowed">WIDOWED</asp:ListItem>
                                                            <asp:ListItem Value="Divorced">DIVORCED</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvMaratialStatus" SetFocusOnError="true" ControlToValidate="ddlMaratialStatus" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Maratial Status." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Photo 
                                                    </label>
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
                                </div>

                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Official <span>Information</span>
                                            </h4>
                                        </div>

                                    </div>
                                    <div class="widget-content">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        Employee Type <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control input-width-xlarge">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvEmployeeType" SetFocusOnError="true" ControlToValidate="ddlEmployeeType" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Employee Type." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
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
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
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
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Employee Grade <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList ID="ddlEmployeeGrade" runat="server" CssClass="form-control input-width-xlarge">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvEmployeeGrade" SetFocusOnError="true" ControlToValidate="ddlEmployeeGrade" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Employee Grade." runat="server"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Join Date <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12 date-select">
                                                        <input type="text" runat="server" readonly="" id="txtJoinDate" class="form-control input-width-xlarge txtJoinDate" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Shift <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control input-width-xlarge">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvShift" SetFocusOnError="true" ControlToValidate="ddlShift" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Shift." runat="server"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Working Days <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">

                                                        <asp:CheckBoxList runat="server" ID="chkListWorkingDays" RepeatColumns="3" RepeatDirection="Horizontal" CssClass="checkboxlist">
                                                            <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                                            <asp:ListItem Value="Monday" Selected="True">Monday</asp:ListItem>
                                                            <asp:ListItem Value="Tuesday" Selected="True">Tuesday</asp:ListItem>
                                                            <asp:ListItem Value="Wednesday" Selected="True">Wednesday</asp:ListItem>
                                                            <asp:ListItem Value="Thursday" Selected="True">Thursday</asp:ListItem>
                                                            <asp:ListItem Value="Friday" Selected="True">Friday</asp:ListItem>
                                                            <asp:ListItem Value="Saturday" Selected="True">Saturday</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                        <asp:CustomValidator ID="cvWorkingDays" ErrorMessage="Please select at least one working day."
                                                            ClientValidationFunction="ValidateWorkingDays" runat="server" Display="Dynamic" CssClass="required" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Contact <span>Information</span>
                                            </h4>
                                        </div>

                                    </div>
                                    <div class="widget-content">
                                        <asp:UpdatePanel ID="upMain" runat="server">
                                            <ContentTemplate>

                                                <div class="row">
                                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group efirst">
                                                            <label class="col-md-12 control-label">
                                                                Country <span class="required">*</span>
                                                            </label>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control input-width-xlarge">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCountry" SetFocusOnError="true" ControlToValidate="ddlCountry" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select Country." runat="server"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                                        <div class="form-group">
                                                            <label class="col-md-12 control-label">
                                                                State <span class="required">*</span>
                                                            </label>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control input-width-xlarge">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvState" SetFocusOnError="true" ControlToValidate="ddlState" CssClass="required" Display="Dynamic" InitialValue="" ErrorMessage="Please select State." runat="server"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        City <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtCity" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCity" SetFocusOnError="true" ControlToValidate="txtCity" CssClass="required" Display="Dynamic" ErrorMessage="Please enter City." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Address <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox TextMode="MultiLine" ID="txtAddress" MaxLength="1000" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAddress" SetFocusOnError="true" ControlToValidate="txtAddress" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Address." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Pin Code <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtPinCode" MaxLength="10" runat="server" CssClass="form-control input-width-xlarge txtNumber"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPinCode" SetFocusOnError="true" ControlToValidate="txtPinCode" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Pin Code." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Mobile <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtMobile" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge txtNumber"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvMobile" SetFocusOnError="true" ControlToValidate="txtMobile" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Mobile." runat="server"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Phone
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtPhone" MaxLength="15" runat="server" CssClass="form-control input-width-xlarge txtNumber"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Email <span class="required">*</span>
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtEmail" MaxLength="200" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvEmail" SetFocusOnError="true" ControlToValidate="txtEmail" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Email." runat="server"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please enter valid Email." ControlToValidate="txtEmail" SetFocusOnError="true" Display="Dynamic" CssClass="required" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                        </asp:RegularExpressionValidator>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Bank <span>Information</span>
                                            </h4>
                                        </div>

                                    </div>
                                    <div class="widget-content">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        Bank Name
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtBankName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Branch Name
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtBranchName" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Account Holder Name
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtAccountName" MaxLength="150" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Account Number
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtAccountNumber" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge txtNumber"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Leave <span>Details</span>
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="widget-content">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        Others
                                                    </label>
                                                    <div class="col-md-12">
                                                        <input type="text" class="form-control" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        Emergency Leave
                                                    </label>
                                                    <div class="col-md-12">
                                                        <input type="text" class="form-control" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        Sick Leave
                                                    </label>
                                                    <div class="col-md-12">
                                                        <input type="text" class="form-control" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        Causal Leave
                                                    </label>
                                                    <div class="col-md-12">
                                                        <input type="text" class="form-control" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Employee <span>Documents</span>
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="widget-content">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                        Resume
                                                    </label>
                                                    <div class="col-md-12">
                                                        <div id="divUploadResume" runat="server" class="divUploadResume" style="display: block;">
                                                            <asp:FileUpload ID="fuResume" runat="server" />
                                                        </div>
                                                        <asp:RegularExpressionValidator
                                                            ID="RVfuResume" runat="server"
                                                            ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                                            ControlToValidate="fuResume" CssClass="text-red" Display="Dynamic"></asp:RegularExpressionValidator>

                                                        <div id="divViewResume" runat="server" class="divViewResume" style="display: none;">
                                                            <asp:HiddenField ID="hfResume" runat="server" />
                                                            <a id="btnViewResume" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                            <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewResume','.divUploadResume');"><i class="fa fa-trash" title="Click to delete resume."></i>&nbsp;Delete</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Offer Letter
                                                    </label>
                                                    <div class="col-md-12">

                                                        <div id="divUploadOfferLetter" runat="server" class="divUploadOfferLetter" style="display: block;">
                                                            <asp:FileUpload ID="fuOfferLetter" runat="server" />
                                                            <asp:RegularExpressionValidator
                                                                ID="RfuOfferLetter" runat="server"
                                                                ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                                                ControlToValidate="fuOfferLetter" CssClass="text-red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div id="divViewOfferLetter" runat="server" class="divViewOfferLetter" style="display: none;">
                                                            <asp:HiddenField ID="hfOfferLetter" runat="server" />
                                                            <a id="btnViewOfferLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                            <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewOfferLetter','.divUploadOfferLetter');"><i class="fa fa-trash" title="Click to delete offer letter."></i>&nbsp;Delete</a>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Joining Letter
                                                    </label>
                                                    <div class="col-md-12">


                                                        <div id="divUploadJoiningLetter" runat="server" class="divUploadJoiningLetter" style="display: block;">
                                                            <asp:FileUpload ID="fuJoiningLetter" runat="server" />
                                                            <asp:RegularExpressionValidator
                                                                ID="RfuJoiningLetter1" runat="server"
                                                                ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                                                ControlToValidate="fuJoiningLetter" CssClass="text-red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div id="divViewJoiningLetter" runat="server" class="divViewJoiningLetter" style="display: none;">
                                                            <asp:HiddenField ID="hfJoiningLetter" runat="server" />
                                                            <a id="btnViewJoiningLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                            <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewJoiningLetter','.divUploadJoiningLetter');"><i class="fa fa-trash" title="Click to delete joining letter."></i>&nbsp;Delete</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Contract Paper
                                                    </label>
                                                    <div class="col-md-12">

                                                        <div id="divUploadContractPaper" runat="server" class="divUploadContractPaper" style="display: block;">
                                                            <asp:FileUpload ID="fuContractPaper" runat="server" />
                                                            <asp:RegularExpressionValidator
                                                                ID="RfuContractPaper" runat="server"
                                                                ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                                                ControlToValidate="fuContractPaper" CssClass="text-red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div id="divViewContractPaper" runat="server" class="divViewContractPaper" style="display: none;">
                                                            <asp:HiddenField ID="hfContractPaper" runat="server" />
                                                            <a id="btnViewContractPaper" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                            <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewContractPaper','.divUploadContractPaper');"><i class="fa fa-trash" title="Click to delete contract paper."></i>&nbsp;Delete</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        ID Proff
                                                    </label>
                                                    <div class="col-md-12">
                                                        <div id="divUploadIDProff" runat="server" class="divUploadIDProff" style="display: block;">
                                                            <asp:FileUpload ID="fuIDProff" runat="server" />
                                                            <asp:RegularExpressionValidator
                                                                ID="RfuIDProff" runat="server"
                                                                ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                                                ControlToValidate="fuIDProff" CssClass="text-red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div id="divViewIDProff" runat="server" class="divViewIDProff" style="display: none;">
                                                            <asp:HiddenField ID="hfIDProff" runat="server" />
                                                            <a id="btnViewIDProff" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                            <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewIDProff','.divUploadIDProff');"><i class="fa fa-trash" title="Click to delete id proff."></i>&nbsp;Delete</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group">
                                                    <label class="col-md-12 control-label">
                                                        Other Document
                                                    </label>
                                                    <div class="col-md-12">

                                                        <div id="divUploadOtherDocument" runat="server" class="divUploadOtherDocument" style="display: block;">
                                                            <asp:FileUpload ID="fuOtherDocument" runat="server" />
                                                            <asp:RegularExpressionValidator
                                                                ID="RfuOtherDocument" runat="server"
                                                                ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                                                                ControlToValidate="fuOtherDocument" CssClass="text-red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div id="divViewOtherDocument" runat="server" class="divViewOtherDocument" style="display: none;">
                                                            <asp:HiddenField ID="hfOtherDocument" runat="server" />
                                                            <a id="btnViewOtherDocument" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                                            <a href="javascript:;" class="btn btn-sm" onclick="EmployeeSave.ShowHideDocument('.divViewOtherDocument','.divUploadOtherDocument');"><i class="fa fa-trash" title="Click to delete other document."></i>&nbsp;Delete</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="widget box">
                                    <div class="widget-header">
                                        <div class="headingOftabel">
                                            <h4>Employee Overtime Rate
                                            </h4>
                                        </div>
                                    </div>
                                    <div class="widget-content">
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6 co-xs-12">
                                                <div class="form-group efirst">
                                                    <label class="col-md-12 control-label">
                                                       OverTime Amount
                                                    </label>
                                                    <div class="col-md-12">
                                                        <asp:TextBox ID="txtOvertimeAmount" MaxLength="5" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <div class="form-actions formActionbtn">
                            <asp:Button CssClass="btn btn-success" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
                            <a href="/Modules/HRAndPayRoll/Masters/EmployeeList.aspx" class="btn">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
