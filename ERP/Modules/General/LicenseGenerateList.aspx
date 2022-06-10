<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="LicenseGenerateList.aspx.cs" Inherits="ERP.Modules.General.LicenseGenerateList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>License Generate</h3>
			<div class="addingbtn">
				<div class="btn-group">
					<span class="btn addButtons">
						<span class="plushicon">
							<img  src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign"/>
						</span>
						<a href="<%=ResolveUrl("~/Modules/General/LicenseGenerateSave.aspx") %>">Add License Key</a>
					</span>
					
				</div>
			</div>
		</div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
				<!--<div class="widget-header">
					
                    
                    <div class="toolbar no-padding">
                        <div class="btn-group">
                             <span class="btn btn-xs addButtons">
                                <i class="ion ion-md-add"></i>
                                <a href="<%=ResolveUrl("~/Modules/General/LicenseGenerateSave.aspx") %>">Add License Key</a>
                            </span>  
                          <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div>
                </div>-->
				
                <div class="widget-content noFiltering">
					<div class="headingOftabel">
						<h4>List All <span>License Generate</span></h4>
					</div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvLicense" runat="server" OnPreRender="gvLicense_PreRender" AutoGenerateColumns="False" CssClass="table dataTable table-bordered gvLicense">
                            <Columns>
                                <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Key" DataField="Key" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Used" DataField="IsUsed" HeaderStyle-Width="17%" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnUsedKey" runat="server" CommandArgument='<%#Eval("LicenseKeyID") %>' CssClass='<%# Convert.ToBoolean(Eval("IsUsed"))?"displayNone":"" %>' OnClick="btnUsedKey_Click"><i class="fa fa-reply replaySquare" title="Click to License Key Used"></i></asp:LinkButton>
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
