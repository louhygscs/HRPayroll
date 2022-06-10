<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="ReplyEmployeeLeave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Transactions.ReplyEmployeeLeave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee Leave</title>
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
                <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Transactions/EmployeeLeaveList.aspx") %>" title="Employee Leave">Employee Leave</a>
            </li>

            <li class="current">Reply Employee Leave
            </li>
        </ul>

    </div>
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <h3>Reply Employee Leave</h3>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
                    <h4>
                        <i class="fa fa-reorder"></i>
                        <span>Reply Employee Leave</span>
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
                                Employee Name :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                <label runat="server" id="lblEmployeeName"></label>
                            </div>
                        </div>

                       <div class="form-group">
                            <label class="col-md-2 control-label">
                                Employee No :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                <label runat="server" id="lblEmployeeNo"></label>
                            </div>
                        </div>

                       <div class="form-group">
                            <label class="col-md-2 control-label">
                                Leaves :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                <label runat="server" id="lblLeaves" >0</label>
                                
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Leave Category :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                               <label runat="server" id="lblLeaveCategory"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Start Date - End Date :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                               <label runat="server" id="lblDateRange"></label>
                            </div>
                        </div>
                         <div class="form-group">
                            <label class="col-md-2 control-label">
                                Consider Half Leave :
                            </label>
                            <div class="col-md-10">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkStartDateHalfLeave" runat="server" Text="Start Date Half Leave" Enabled="false" />
                                </label>
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkEndDateHalfLeave" runat="server" Text="End Date Half Leave" Enabled="false"/>
                                </label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Total Leave :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                 <label runat="server" id="lblTotalLeave"></label>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-md-2 control-label">
                                Apply Date :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                 <label runat="server" id="lblApplyDate"></label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">
                                Reason :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                               <label runat="server" id="lblReason"></label>
                            </div>
                        </div>
                       <div class="form-group">
                            <label class="col-md-2 control-label">
                                Status :
                            </label>
                            <div class="col-md-10 control-label textLeft">
                               <label runat="server" id="lblStatus"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">
                               Is Approve :
                            </label>
                            <div class="col-md-10">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsApprove" runat="server"  />
                                </label>
                            </div>
                        </div>

                       <div class="form-group">
                            <label class="col-md-2 control-label">
                                Response :<span class="red">*</span>
                            </label>
                            <div class="col-md-10 control-label textLeft">
                                <asp:TextBox ID="txtReply" MaxLength="1000" runat="server" TextMode="MultiLine" CssClass="form-control input-width-xlarge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReply"
                                            ControlToValidate="txtReply" SetFocusOnError="true" runat="server" ErrorMessage="Please enter Response." CssClass="required" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-actions">
                            <a href="/Modules/HRAndPayRoll/Transactions/EmployeeLeaveList.aspx" class="btn pull-right">Cancel
                            </a>
                            <asp:Button CssClass="btn btn-success pull-right" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />

                        </div>
                    </form>

                </div>

            </div>
        </div>
    </div>

</asp:Content>


