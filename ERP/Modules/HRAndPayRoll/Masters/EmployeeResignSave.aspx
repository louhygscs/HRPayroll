<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeResignSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EmployeeResignSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Resign</title>
    <link href="<%=ResolveUrl("~/Styles/daterangepicker.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/Scripts/moment.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/daterangepicker.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EmployeeResignSave.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <div class="crumbs">
        <ul id="breadcrumbs" class="breadcrumb">
            <li>
                <a href="<%=ResolveUrl("~/Modules/Main.aspx") %>">
                    <i class="fa fa-home"></i>
                    Dashboard
                </a>
            </li>
            <li>
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EmployeeList.aspx") %>" title="Employee">Employee</a>
            </li>

            <li class="current">Save Employee Resign
            </li>
        </ul>

    </div>
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <h3>Save Employee Resign</h3>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <h4>
                        <i class="fa fa-reorder"></i>
                        <span>Save Employee Resign</span>
                    </h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>
                <div class="widget-content">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />
                        <div class="form-group efirst">
                            <label class="col-md-2 control-label">
                                Employee Name 
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                <label id="lblFullName" runat="server"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Resign Date <span class="required">*</span>
                            </label>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtResignDate" runat="server" ReadOnly="true" CssClass="form-control input-width-xlarge txtResignDate"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvResignDate" SetFocusOnError="true" ControlToValidate="txtResignDate" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Resign Date." runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Resign Letter
                            </label>
                            <div class="col-md-10">

                                <div id="divUploadResignLetter" runat="server" class="divUploadResignLetter" style="display: block;">
                                    <asp:FileUpload ID="fuResignLetter" runat="server" />
                                     <asp:RegularExpressionValidator
                    id="RegularExpressionValidator1" runat="server"
                    ErrorMessage="Only .pdf, .doc, .docx, .jpg, .png, .jpeg files are allowed!"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.doc|.docx|.jpg|.png|.jpeg)$"
                    ControlToValidate="fuResignLetter" CssClass="text-red"></asp:RegularExpressionValidator>
                                </div>
                                <div id="divViewResignLetter" runat="server" class="divViewResignLetter" style="display: none;">
                                    <asp:HiddenField ID="hfResignLetter" runat="server" />
                                    <a id="btnViewResignLetter" runat="server" class="btn btn-sm" target="_blank"><i class="fa fa-download"></i>&nbsp;Download</a>
                                    <a href="javascript:;" class="btn btn-sm" onclick="EmployeeResignSave.ShowHideDocument('.divViewResignLetter','.divUploadResignLetter');"><i class="fa fa-trash" title="Click to delete resign letter."></i>&nbsp;Delete</a>
                                </div>

                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Description 
                            </label>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="form-control input-width-xlarge txtDescription"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-actions">
                            <a href="/Modules/HRAndPayRoll/Masters/EmployeeList.aspx" class="btn pull-right">Cancel
                            </a>
                            <asp:Button CssClass="btn btn-success pull-right btnSave"  ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />

                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
