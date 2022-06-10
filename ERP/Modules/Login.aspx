<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ERP.Modules.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Login</title>

        <meta charset="UTF-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
        <link rel="shortcut icon" type="image/ico" href="~/Images/favicon.ico" />
        <link rel="shortcut icon" href="~/Images/favicon.png" type="image/png" />

        <%--<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css' />--%>
        <link href="<%=ResolveUrl("~/Styles/yhlfont.css")%>" rel="stylesheet" type="text/css"/>
        <link href="<%=ResolveUrl("~/Styles/bootstrap.css")%>" rel="stylesheet" />
        <link href="<%=ResolveUrl("~/Styles/font-awesome.css")%>" rel="stylesheet" /> 
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
            <!-- Logo -->
            <div class="logo">
                <img  src="<%=ResolveUrl("~/Images/companylogo.png") %>" class="svg" alt="HRM logo"/>
                <h3 class="form-title">Log In to your Account</h3>
            </div>

            <div class="content">
                <form id="frmLogin" runat="server">  
                    <div class="form-group"> 
                            <label class="control-label">Username</label>
                            <asp:TextBox ID="txtUserName" MaxLength="200" runat="server" placeholder="Username" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" SetFocusOnError="true" ControlToValidate="txtUserName" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Username." runat="server"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group"> 
                        <label class="control-label">Password</label>
                            <asp:TextBox ID="txtPassword" MaxLength="20" TextMode="Password" runat="server" placeholder="Password" CssClass="form-control txtSpaceRemoveOnly"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" SetFocusOnError="true" ControlToValidate="txtPassword" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Password." runat="server"></asp:RequiredFieldValidator>
                    
                    </div>
                    <div class="form-group">
                        <label class="control-label">Accounting Period</label>
                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvFinancialYear"
                            ControlToValidate="ddlFinancialYear" SetFocusOnError="true" InitialValue="" runat="server" ErrorMessage="Please select Financial Year." CssClass="required" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="clearfix">
                         <div class="pull-left">
                            <div class="form-group">
                                <label class="checkbox">
                                    <asp:CheckBox runat="server" ID="chkRememberMe" Text="Remember Me" />
                                </label>
                            </div>
                        </div>
                        <div class="pull-right">
                            <div class="form_group">
                                <a href="/Modules/ForgotPassword.aspx" class="forgot-password-link">Forgot Password?</a>
                            </div>
                        </div>
                    </div>
                    <div class"form-btn">
                         <asp:LinkButton CssClass="submit btn btn-success btn-block" ID="btnLogin" runat="server" OnClick="btnLogin_Click">Log In <i class="fa fa-angle-right"></i></asp:LinkButton>
                    </div> 
                </form>
            </div> 

            <!-- Below Logo -->
            <div class="below-logo">
                <h5 class="form-title">Developed and Design By <a href="https://www.yhlsoftware.com" class="copyright">YHL Software</a></h5>
                <img  src="<%=ResolveUrl("~/Images/Logo2.png") %>" class="yhl-logo" alt="HRM logo"/>
            </div>
        </div> 
    </body>
</html>