<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ERP.Modules.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>

    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <link rel="shortcut icon" type="image/ico" href="~/Images/favicon.ico" />
    <link rel="shortcut icon" href="~/Images/favicon.png" type="image/png" />

    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />
    <link href="<%=ResolveUrl("~/Styles/bootstrap.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Styles/all.min.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Styles/main.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Styles/responsive.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Styles/Login.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Styles/toastr.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Styles/Common.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/Scripts/jquery-1.10.2.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/bootstrap.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/toastr.js")%>"></script>
    <script src="<%=ResolveUrl("~/Scripts/Common.js")%>"></script>

    <!--[if lt IE 9]>
    <script src="<%=ResolveUrl("~/Scripts/html5shiv.js")%>"></script>
    <![endif]-->
</head>
<body class="login">
    <div class="logo">
        <strong>Arity Infoway</strong>
    </div>

    <div class="box">
        <div class="content">
            <form id="frmResetPassword" runat="server">
                <asp:HiddenField ID="hfId" runat="server" />
                <h3 class="form-title">Reset Password</h3>

                <div class="form-group">
                    <label>
                        Password
                    </label>
                    <div>
                        <asp:TextBox ID="txtPassword" MaxLength="20" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" SetFocusOnError="true" ControlToValidate="txtPassword" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Password." runat="server"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPassword" SetFocusOnError="true" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{8,20}$" ControlToValidate="txtPassword" CssClass="required" Display="Dynamic" ErrorMessage="Password should be alphanumeric characters with minimum of 8 characters and maximum of 20 characters." runat="server"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="form-group">
                    <label>
                        Confirm Password
                    </label>
                    <div>
                        <asp:TextBox ID="txtConfirmPassword" MaxLength="20" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CompareValidator ID="cvConfirmPassword" SetFocusOnError="true" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" CssClass="required" Display="Dynamic" ErrorMessage="Password and confirm password do not match." runat="server"></asp:CompareValidator>
                    </div>
                </div>

                <div class="form-btn-actions">
                    <asp:LinkButton CssClass="submit btn btn-primary pull-left" ID="btnResetPassword" runat="server" OnClick="btnResetPassword_Click">Reset Password <i class="fa fa-angle-right"></i></asp:LinkButton>
                    <a href="/Modules/Login.aspx" class="btn pull-right">Cancel
                    </a>
                </div>
                <div class="clearfix"></div>
            </form>
        </div>
    </div>
</body>
</html>
