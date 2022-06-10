<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="DeviceList.aspx.cs" Inherits="ERP.Modules.BioMetricDevice.Device.DeviceList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Device</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/BioMetricDevice/Device/DeviceList.js")%>"></script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
  
    <!-- /Breadcrumbs line -->

    <div class="page-header">
		<div class="pageHeading">
			 <h3>Device</h3>
			<div class="addingbtn">
				<div class="btn-group">
					
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/BioMetricDevice/Device/DeviceSave.aspx") %>">Add Device</a>
					</span>
					
				</div>
			</div>
		</div>
       
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <!-- <div class="widget-header firstHeader">
					<div class="headingOftabel">
						<h4>Add New <span>Device</span></h4>
					</div>
                    
                   <div class="toolbar no-padding">
                        <div class="btn-group">

                            <span class="btn btn-xs">
                                <i class="ion ion-md-add"></i>
                                <a href="<%=ResolveUrl("~/Modules/BioMetricDevice/Device/DeviceSave.aspx") %>">Add Device</a>
                            </span>
                           
                        </div>
                    </div> 
                </div>-->
				
				
                <div class="widget-content">
					<div class="headingOftabel">
						<h4>Add New <span>Device</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView OnPreRender="gvDevice_PreRender" ID="gvDevice"
                            runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered gvDevice">
                            <Columns>
                                <asp:BoundField HeaderText="Device Name" DataField="DeviceName" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="Device Code" DataField="DeviceCode" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="Connection Status" DataField="ConnectionStatus" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="IP Address" DataField="IPAddress" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField HeaderText="Port" DataField="Port" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnConnectDevice" runat="server" CausesValidation="false" CommandArgument='<%#Eval("DeviceID") %>' OnClick="btnConnectDevice_Click"><i class="<%# Convert.ToString(Eval("ConnectionStatus"))=="DisConnected"?"fa fa-play":"fa fa-stop"%>" title="<%# Convert.ToString(Eval("ConnectionStatus"))=="DisConnected"?"Click to Connect Device":"Click to Stop Device"%>"></i></asp:LinkButton>&nbsp;
                                        <a href="<%#"DeviceSave.aspx?id="+Eval("DeviceID") %>"><i class="fa fa-edit editSquare" title="Click to edit Device"></i></a>&nbsp; 
                                        <asp:LinkButton ID="btnDelete" CssClass="btnDelete" runat="server" CommandArgument='<%#Eval("DeviceID") %>' OnClick="btnDelete_Click"><i class="fa fa-times-circle resignSquare" title="Click to delete Device"></i></asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="#F9F9F9" />
                        </asp:GridView>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
