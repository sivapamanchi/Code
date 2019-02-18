<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.registerSuccess" Codebehind="registerSuccess.aspx.vb" %>

<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
<title>Register with Bluegreen Online - Success</title>
<link type="text/css" rel="stylesheet" href="css/bgOnline.css" />
 <link rel="stylesheet" rev="stylesheet" href="Owner/owner.css" />
</head>
<body>
<a href="<%=System.Configuration.ConfigurationManager.AppSettings("bxgwebSecureURL")%>" target="_blank" title="Bluegreen Resorts"><img src="images/ResortsLogo.gif" alt="Bluegreen Resorts" width="271" height="20" border="0"  /></a>
<div class="headerInfo">

     <img src="images/phone.gif" alt="Phone us at 800.456.2582"  border="0" />
</div>

<img src="images/blank.gif" width="740" height="1" />

<div class="mainContent">
	<div style="display:block; padding: 0 20px 10px; border:1px solid #ccc; text-align:center;">
		<p style="color:#393; font-size:20px;"><img src="images/successIcon.gif" width="24" height="20" alt="" style="position:relative; top:2px; margin-right:10px;" /><strong>Congratulations!</strong></p>
		<p style="color:#393; font-size:16px;">You have successfully registered on Bluegreen Online.</p>
		<p>Now schedule your very own <span style="color:#393;">Ownership Experience Kick-off</span> with a dedicated Vacation Specialist who will give you a personalized, guided tour of your Bluegreen Vacations ownership and, most importantly, get you on your first vacation! Select a date and time that is most convenient for you.</p>
		<p><a href="http://www.secure-booker.com/bluegreen/BookOnlineStart.aspx" target="_blank">
			<img src="images/btn-create-appointment.gif" width="240" height="28" alt="Create an Appointment" /></a></p>
	</div>
	<p align="center">The next time you sign in, all you will need is your sign-in name (email address) and password.</p>
	<p align="center" style="padding:10px; background:#efefef;">Please confirm or update your contact information below and select <strong>CONFIRM MY ACCOUNT DETAILS</strong>.</p>


	<form id="registerSuccessForm" method="post" runat="server">
		<table class="reservData" cellspacing="1" cellpadding="3" width="650" border="0">
			<tr>
				<td align="center" colspan="2" width="497" height="1" background="images/reservDotRule.gif"><img src="images/blank.gif" width="497" height="1" border="0" alt="" /></td>
			</tr>
			<tr>
				<td width="200" height="17" align="left" bgcolor="#ff9900" class="reservDataLabel">Account Information</td>
				<td align="right" background="images/reservBarBkgd.gif">(<font color="#cc0000">*</font> Required fields)</td>
			</tr>
			<tr>
				<td align="center" colspan="2" width="497" height="1" background="images/reservDotRule.gif"><img src="images/blank.gif" width="497" height="1" border="0" alt="" /></td>
			</tr>
		</table>


		<table cellspacing="0" cellpadding="0" width="650" border="0">
			<tr>
				<td>
					<asp:Image ID="imgAlert" runat="server" Visible="False" ImageUrl="images/alert.gif"></asp:Image>
					&nbsp;&nbsp;
					<asp:Label ID="lblAddVerifyErr" runat="server" Visible="False" ForeColor="Red"></asp:Label>
					<p>If you need to update a military address or need assistance, please call 800.456.CLUB (2582) within the U.S.; if you are calling from the U.K., you may call toll-free  00.800.4707.4707; all others outside the U.S. and U.K. dial 317.572.2065</p>
				</td>
			</tr>
		</table>


		<table class="reservData" cellspacing="0" cellpadding="3" width="610" border="0" style="margin:0 auto;">
			<tr>
				<td>Primary Account Contact</td>
			</tr>
			<tr>
				<td>Account Number:</td>
				<td><asp:Label ID="lblAcctNo" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><font color="#cc0000">**</font>First Name:</td>
				<td><asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><font color="#cc0000">**</font>Last Name:</td>
				<td><asp:Label ID="lblLastName" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><font color="#cc0000">*</font>Address1:</td>
				<td><asp:TextBox ID="txtAddress1" runat="server" Width="345px" MaxLength="30"></asp:TextBox>
					<%If Session("msg") = True Then%><div class="reservData" style="vertical-align:top;" id="msg1">Enter only your Street Address</div><%End If%>
				</td>
			</tr>
			<tr>
				<td><asp:Label ID="lblAdd2Req" runat="server" Visible="False"><font color="#cc0000">*</font></asp:Label>Address2:</td>
				<td><asp:TextBox ID="txtAddress2" runat="server" Width="345px" MaxLength="30"></asp:TextBox>
					<%If Session("msg") = True Then%><div class="reservData" style="vertical-align:top;" id="msg2">Enter your apartment, unit and/or building number (if applicable)</div><%End If%>
				</td>
			</tr>
		<asp:Panel ID="pnlCountryUS" runat="server">
			<tr>
				<td><font color="#cc0000">*</font>City:</td>
				<td><asp:TextBox ID="txtCity" runat="server" MaxLength="50" Width="345px"></asp:TextBox></td>
			</tr>
			<tr>
				<td><font color="#cc0000">*</font>State/Province:</td>
				<td><asp:DropDownList ID="ddlState" runat="server" Width="345px"></asp:DropDownList></td>
			</tr>
			<tr>
				<td><asp:Label ID="lblPostal" runat="server"><font color="#cc0000">*</font>Zip/Postal Code:</asp:Label></td>
				<td><asp:TextBox ID="txtZip" runat="server" MaxLength="10" Width="345px"></asp:TextBox></td>
			</tr>
		</asp:Panel>
		<asp:Panel ID="pnlCountryNotUS" runat="server" Visible="false">
			<tr>
				<td>Address3:</td>
				<td><asp:TextBox ID="txtAddress3" runat="server" MaxLength="300" Width="345px"></asp:TextBox></td>
			</tr>
		</asp:Panel>
			<tr>
				<td><font color="#cc0000">*</font>Country:</td>
				<td><asp:DropDownList ID="ddlCountry" runat="server" Width="345px" AutoPostBack="True"></asp:DropDownList></td>
			</tr>
			<tr>
				<td><font color="#cc0000">*</font>Home Phone:</td>
				<td width="371"><asp:TextBox ID="txtHomePhone" runat="server" Width="345px" MaxLength="15"></asp:TextBox></td>
			</tr>
			<tr>
				<td><font color="#cc0000">*</font>Alternate Phone:</td>
				<td><asp:TextBox ID="txtDayPhone" runat="server" Width="345px" MaxLength="15"></asp:TextBox></td>
			</tr>
			<tr>
				<td><font color="#cc0000">*</font>Sign In Name/Email:</td>
				<td><asp:TextBox ID="txtEmail" runat="server" Width="345px" MaxLength="60"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="center" colspan="2">
					<br />
					<asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="images/Btn_ConfirmAccountDetails.gif"></asp:ImageButton>
					<br /><br /></td>
			</tr>
			<tr>
				<td colspan="2"><a href="https://bluegreentransfers.wufoo.com/forms/bluegreen-vacations-title-transfer-request/" class="textlink" style="color:#cc0000;">**Click here for information about making name changes to your title.</a>
					<br /><br /></td>
			</tr>
		</table>

		<table cellspacing="1" cellpadding="3" width="650" border="0">
			<tr>
				<td colspan="3" align="center" class="reservDataLabel" style="background-color: #ff9900;">Contract Information</td>
			</tr>
		<asp:Repeater ID="rptContracts" runat="server">
			<HeaderTemplate>
				<tr>
					<td class="resortDataw" align="center" style="width: 16; background-color: #333333">No.</td>
					<td class="resortDataw" align="center" style="width: 96; background-color: #333333">Contract No.</td>
					<td class="resortDataw" align="center" style="width: 374; background-color: #333333">Description</td>
				</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td class="resortData" align="center" style="width: 16; background-color: #ffffff"><%# DataBinder.Eval(Container, "DataItem").PRORDER %></td>
					<td class="resortData" align="center" style="width: 96; background-color: #ffffff"><%# DataBinder.Eval(Container, "DataItem").ACCT %></td>
					<td class="resortData" align="center" style="width: 374; background-color: #ffffff"><%# DataBinder.Eval(Container, "DataItem").PRONAME %></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
		</table>
	</form>
</div><!-- .mainContent --><br />
<includeControlFooter:footer ID="footer" runat="server"></includeControlFooter:footer>
</body>
</html>