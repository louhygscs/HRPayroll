<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="ERP.Modules.BioMetricDevice.Employee.EmployeeList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Employee</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/BioMetricDevice/Employee/EmployeeList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    
    <!-- /Breadcrumbs line -->

    <div class="page-header">
		<div class="pageHeading">
			<h3>Employee</h3>
	
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <!--<div class="widget-header">
                    <h4><i class="fa fa-list"></i>All List <span>Employee</span></h4>
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div> -->
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>All List <span>Employee</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvEmployee_PreRender" ID="gvEmployee"
                            runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvEmployee">
                            <Columns>
                                <asp:BoundField HeaderText="Full Name" DataField="FullName" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="Mobile No" DataField="Mobile" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                 <asp:BoundField HeaderText="Have Finger" DataField="IsFinger" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                 <asp:BoundField HeaderText="Have Face" DataField="IsFace" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="Join Date" DataField="JointDate"  DataFormatString = "{0:MM/dd/yyyy}" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="Count" DataField="Count" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="#F9F9F9" />
                        </asp:GridView>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
