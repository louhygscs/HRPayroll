<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="ERP.Modules.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
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
    <div class="box">
             <div class="logo">
        <img  src="<%=ResolveUrl("~/Images/Logo.svg") %>" class="svg" alt="HRM logo"/>

                  <h3 class="form-title">Forgot Password</h3>
                 </div> 
        <div class="content">
            <form id="frmForgotPassword" runat="server"> 

                <div class="form-group"> 
                        <label class="control-label">Username</label>
                        <asp:TextBox ID="txtUserName" MaxLength="200" runat="server" placeholder="Username" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUserName" SetFocusOnError="true" ControlToValidate="txtUserName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Username." runat="server"></asp:RequiredFieldValidator>
                    
                </div>

                <div class="form-btn-actions form-group">
                    <asp:LinkButton CssClass="submit btn btn-success btn-block" ID="btnSendLink" runat="server" OnClick="btnSendLink_Click">Send Link <i class="fa fa-angle-right"></i></asp:LinkButton>
                   
                </div>
        <div class="text-center">
             <a href="/Modules/Login.aspx" class="btn btn-link link-success">Cancel
                    </a>
        </div>
                
            </form>
        </div>
    </div>

</body>
</html>
