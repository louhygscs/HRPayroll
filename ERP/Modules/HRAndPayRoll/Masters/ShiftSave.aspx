<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Main.Master" AutoEventWireup="true"
    CodeBehind="ShiftSave.aspx.cs" Inherits="ERP.Modules.HRAndPayRoll.Masters.ShiftSave" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <title>Shift</title>
  
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <!-- /Breadcrumbs line -->
    <div class="page-header">
		<div class="pageHeading">
			<h3>Add Shift</h3>
		
		</div>
		
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="widget box">
                <div class="widget-header">
					<div class="headingOftabel">
						<h4>
							Save <span>Shift</span>
						</h4>
					</div>
                    
                   <!-- <div class="toolbar no-padding">
                        <div class="btn-group">
                            <span class="btn btn-xs widget-collapse"><i class="fa fa-angle-down"></i></span>
                        </div>
                    </div> -->
                </div>
                <div class="widget-content shiftSelect">
                    <form id="frmMain" runat="server" class="form-horizontal row-border">
                        <asp:HiddenField ID="hfId" runat="server" />
						<div class="row">
							<div class="col-md-12">
								<div class="form-group efirst">
									<label class="col-md-12 control-label">
										Shift <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<asp:TextBox ID="txtShift" MaxLength="50" runat="server" CssClass="form-control input-width-xlarge"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfvShift" SetFocusOnError="true" ControlToValidate="txtShift" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Shift." runat="server"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										From Time <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<div class="row">
											<div class="col-md-4">
												<%-- <asp:TextBox ID="txtFromTime" runat="server" CssClass="form-control input-width-xlarge txtFromTime"></asp:TextBox>--%>
												<asp:DropDownList ID="ddlFromHour" runat="server" CssClass="form-control input-width-xlarge">
													<asp:ListItem Value="01">01</asp:ListItem>
													<asp:ListItem Value="02">02</asp:ListItem>
													<asp:ListItem Value="03">03</asp:ListItem>
													<asp:ListItem Value="04">04</asp:ListItem>
													<asp:ListItem Value="05">05</asp:ListItem>
													<asp:ListItem Value="06">06</asp:ListItem>
													<asp:ListItem Value="07">07</asp:ListItem>
													<asp:ListItem Value="08">08</asp:ListItem>
													<asp:ListItem Value="09">09</asp:ListItem>
													<asp:ListItem Value="10">10</asp:ListItem>
													<asp:ListItem Value="11">11</asp:ListItem>
													<asp:ListItem Value="12">12</asp:ListItem>
												</asp:DropDownList>
											</div>
											<div class="col-md-4">
												<asp:DropDownList ID="ddlFromMinute" runat="server" CssClass="form-control input-width-xlarge">
													<asp:ListItem Value="00">00</asp:ListItem>
													<asp:ListItem Value="01">01</asp:ListItem>
													<asp:ListItem Value="02">02</asp:ListItem>
													<asp:ListItem Value="03">03</asp:ListItem>
													<asp:ListItem Value="04">04</asp:ListItem>
													<asp:ListItem Value="05">05</asp:ListItem>
													<asp:ListItem Value="06">06</asp:ListItem>
													<asp:ListItem Value="07">07</asp:ListItem>
													<asp:ListItem Value="08">08</asp:ListItem>
													<asp:ListItem Value="09">09</asp:ListItem>
													<asp:ListItem Value="10">10</asp:ListItem>
													<asp:ListItem Value="11">11</asp:ListItem>
													<asp:ListItem Value="12">12</asp:ListItem>
													<asp:ListItem Value="13">13</asp:ListItem>
													<asp:ListItem Value="14">14</asp:ListItem>
													<asp:ListItem Value="15">15</asp:ListItem>
													<asp:ListItem Value="16">16</asp:ListItem>
													<asp:ListItem Value="17">17</asp:ListItem>
													<asp:ListItem Value="18">18</asp:ListItem>
													<asp:ListItem Value="19">19</asp:ListItem>
													<asp:ListItem Value="20">20</asp:ListItem>
													<asp:ListItem Value="21">21</asp:ListItem>
													<asp:ListItem Value="22">22</asp:ListItem>
													<asp:ListItem Value="23">23</asp:ListItem>
													<asp:ListItem Value="24">24</asp:ListItem>
													<asp:ListItem Value="25">25</asp:ListItem>
													<asp:ListItem Value="26">26</asp:ListItem>
													<asp:ListItem Value="27">27</asp:ListItem>
													<asp:ListItem Value="28">28</asp:ListItem>
													<asp:ListItem Value="29">29</asp:ListItem>
													<asp:ListItem Value="30">30</asp:ListItem>
													<asp:ListItem Value="31">31</asp:ListItem>
													<asp:ListItem Value="32">32</asp:ListItem>
													<asp:ListItem Value="33">33</asp:ListItem>
													<asp:ListItem Value="34">34</asp:ListItem>
													<asp:ListItem Value="35">35</asp:ListItem>
													<asp:ListItem Value="36">36</asp:ListItem>
													<asp:ListItem Value="37">37</asp:ListItem>
													<asp:ListItem Value="38">38</asp:ListItem>
													<asp:ListItem Value="39">39</asp:ListItem>
													<asp:ListItem Value="40">40</asp:ListItem>
													<asp:ListItem Value="41">41</asp:ListItem>
													<asp:ListItem Value="42">42</asp:ListItem>
													<asp:ListItem Value="43">43</asp:ListItem>
													<asp:ListItem Value="44">44</asp:ListItem>
													<asp:ListItem Value="45">45</asp:ListItem>
													<asp:ListItem Value="46">46</asp:ListItem>
													<asp:ListItem Value="47">47</asp:ListItem>
													<asp:ListItem Value="48">48</asp:ListItem>
													<asp:ListItem Value="49">49</asp:ListItem>
													<asp:ListItem Value="50">50</asp:ListItem>
													<asp:ListItem Value="51">51</asp:ListItem>
													<asp:ListItem Value="52">52</asp:ListItem>
													<asp:ListItem Value="53">53</asp:ListItem>
													<asp:ListItem Value="54">54</asp:ListItem>
													<asp:ListItem Value="55">55</asp:ListItem>
													<asp:ListItem Value="56">56</asp:ListItem>
													<asp:ListItem Value="57">57</asp:ListItem>
													<asp:ListItem Value="58">58</asp:ListItem>
													<asp:ListItem Value="59">59</asp:ListItem>
												</asp:DropDownList>
											</div>
											<div class="col-md-4">
												<asp:DropDownList ID="ddlFromMeridiem" runat="server" CssClass="form-control input-width-xlarge">
													<asp:ListItem Value="AM">AM</asp:ListItem>
													<asp:ListItem Value="PM">PM</asp:ListItem>

												</asp:DropDownList>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="col-md-6 col-sm-6 col-xs-12">
								<div class="form-group">
									<label class="col-md-12 control-label">
										To Time <span class="required">*</span>
									</label>
									<div class="col-md-12">
										<div class="row">
											<div class="col-md-4">
												<%--<asp:TextBox ID="txtToTime" runat="server" CssClass="form-control input-width-xlarge txtToTime"></asp:TextBox>--%>
												<asp:DropDownList ID="ddlToHour" runat="server" CssClass="form-control input-width-xlarge">
													<asp:ListItem Value="01">01</asp:ListItem>
													<asp:ListItem Value="02">02</asp:ListItem>
													<asp:ListItem Value="03">03</asp:ListItem>
													<asp:ListItem Value="04">04</asp:ListItem>
													<asp:ListItem Value="05">05</asp:ListItem>
													<asp:ListItem Value="06">06</asp:ListItem>
													<asp:ListItem Value="07">07</asp:ListItem>
													<asp:ListItem Value="08">08</asp:ListItem>
													<asp:ListItem Value="09">09</asp:ListItem>
													<asp:ListItem Value="10">10</asp:ListItem>
													<asp:ListItem Value="11">11</asp:ListItem>
													<asp:ListItem Value="12">12</asp:ListItem>
												</asp:DropDownList>
											</div>
											<div class="col-md-4">
												<asp:DropDownList ID="ddlToMinute" runat="server" CssClass="form-control input-width-xlarge">
													<asp:ListItem Value="00">00</asp:ListItem>
													<asp:ListItem Value="01">01</asp:ListItem>
													<asp:ListItem Value="02">02</asp:ListItem>
													<asp:ListItem Value="03">03</asp:ListItem>
													<asp:ListItem Value="04">04</asp:ListItem>
													<asp:ListItem Value="05">05</asp:ListItem>
													<asp:ListItem Value="06">06</asp:ListItem>
													<asp:ListItem Value="07">07</asp:ListItem>
													<asp:ListItem Value="08">08</asp:ListItem>
													<asp:ListItem Value="09">09</asp:ListItem>
													<asp:ListItem Value="10">10</asp:ListItem>
													<asp:ListItem Value="11">11</asp:ListItem>
													<asp:ListItem Value="12">12</asp:ListItem>
													<asp:ListItem Value="13">13</asp:ListItem>
													<asp:ListItem Value="14">14</asp:ListItem>
													<asp:ListItem Value="15">15</asp:ListItem>
													<asp:ListItem Value="16">16</asp:ListItem>
													<asp:ListItem Value="17">17</asp:ListItem>
													<asp:ListItem Value="18">18</asp:ListItem>
													<asp:ListItem Value="19">19</asp:ListItem>
													<asp:ListItem Value="20">20</asp:ListItem>
													<asp:ListItem Value="21">21</asp:ListItem>
													<asp:ListItem Value="22">22</asp:ListItem>
													<asp:ListItem Value="23">23</asp:ListItem>
													<asp:ListItem Value="24">24</asp:ListItem>
													<asp:ListItem Value="25">25</asp:ListItem>
													<asp:ListItem Value="26">26</asp:ListItem>
													<asp:ListItem Value="27">27</asp:ListItem>
													<asp:ListItem Value="28">28</asp:ListItem>
													<asp:ListItem Value="29">29</asp:ListItem>
													<asp:ListItem Value="30">30</asp:ListItem>
													<asp:ListItem Value="31">31</asp:ListItem>
													<asp:ListItem Value="32">32</asp:ListItem>
													<asp:ListItem Value="33">33</asp:ListItem>
													<asp:ListItem Value="34">34</asp:ListItem>
													<asp:ListItem Value="35">35</asp:ListItem>
													<asp:ListItem Value="36">36</asp:ListItem>
													<asp:ListItem Value="37">37</asp:ListItem>
													<asp:ListItem Value="38">38</asp:ListItem>
													<asp:ListItem Value="39">39</asp:ListItem>
													<asp:ListItem Value="40">40</asp:ListItem>
													<asp:ListItem Value="41">41</asp:ListItem>
													<asp:ListItem Value="42">42</asp:ListItem>
													<asp:ListItem Value="43">43</asp:ListItem>
													<asp:ListItem Value="44">44</asp:ListItem>
													<asp:ListItem Value="45">45</asp:ListItem>
													<asp:ListItem Value="46">46</asp:ListItem>
													<asp:ListItem Value="47">47</asp:ListItem>
													<asp:ListItem Value="48">48</asp:ListItem>
													<asp:ListItem Value="49">49</asp:ListItem>
													<asp:ListItem Value="50">50</asp:ListItem>
													<asp:ListItem Value="51">51</asp:ListItem>
													<asp:ListItem Value="52">52</asp:ListItem>
													<asp:ListItem Value="53">53</asp:ListItem>
													<asp:ListItem Value="54">54</asp:ListItem>
													<asp:ListItem Value="55">55</asp:ListItem>
													<asp:ListItem Value="56">56</asp:ListItem>
													<asp:ListItem Value="57">57</asp:ListItem>
													<asp:ListItem Value="58">58</asp:ListItem>
													<asp:ListItem Value="59">59</asp:ListItem>
												</asp:DropDownList>
											</div>
											<div class="col-md-4">
												<asp:DropDownList ID="ddlToMeridiem" runat="server" CssClass="form-control input-width-xlarge">
													<asp:ListItem Value="AM">AM</asp:ListItem>
													<asp:ListItem Value="PM">PM</asp:ListItem>

												</asp:DropDownList>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
                        
                        
                        <div class="form-actions formActionbtn">
                            
                            <asp:Button CssClass="btn btn-success" ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" />
							<a href="/Modules/HRAndPayRoll/Masters/ShiftList.aspx" class="btn ">Cancel
                            </a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
