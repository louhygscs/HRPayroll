<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true" CodeBehind="EducationList.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.EducationList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Education</title>
    <script src="<%=ResolveUrl("~/Scripts/Modules/HRAndPayRoll/Masters/EducationList.js")%>"></script>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    
    <!-- /Breadcrumbs line -->
    <div class="page-header">
        <div class="pageHeading">
            <h3>Education</h3>
            <div class="addingbtn">
                <div class="btn-group">
                    <span class="btn addButtons">
                        <span class="plushicon">
                            <img src="<%=ResolveUrl("~/Images/plush sign.svg") %>" class="svg" alt="plush sign" />
                        </span>
                        <a href="<%=ResolveUrl("~/Modules/HRAndPayRoll/Masters/EducationSave.aspx") %>">Add Education</a>
                    </span>

                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-content">
                    <div class="headingOftabel">
                        <h4>List All <span>Education</span></h4>
                    </div>
                    <form id="frmMain" runat="server">
                        <asp:GridView ID="gvEducation" runat="server" AutoGenerateColumns="False" CssClass="table  table-bordered gvEducation" OnPreRender="gvEducation_PreRender">
                            <Columns>
                                <asp:BoundField HeaderText="Education" DataField="EducationName" HeaderStyle-Width="90%" HeaderStyle-HorizontalAlign="Left" />

                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <a href="<%#"EducationSave.aspx?id="+Eval("EducationID") %>">
                                            <img src="<%=ResolveUrl("~/Images/edit.svg") %>" class="svg" alt="edit sign" />

                                        </a>
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("EducationID") %>' OnClick="btnDelete_Click" CssClass="btnDelete">
										<img  src="<%=ResolveUrl("~/Images/remove.svg") %>" class="svg" alt="remove sign"/>

                                        </asp:LinkButton>
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
