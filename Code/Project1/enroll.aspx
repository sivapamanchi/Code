<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.enroll" Codebehind="enroll.aspx.vb" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="includes/ucMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<HTML>
	<HEAD>
		<title>Enrollment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<link rel="stylesheet" rev="stylesheet" href="main.css">	
	<link rel="stylesheet" rev="stylesheet" href="owner/owner.css">	
    <link href="css/ucmenu.css" rel="stylesheet" type="text/css" />
	</HEAD>
		<body style="margin:0px">
        <table width="100%"  border="0" cellspacing="0" cellpadding="0">
        	<tr>
        		<td align="center"><table width="740" border="0" cellspacing="0" cellpadding="0">
                	<tr>
                		<td align="center"><table width="730" height="70" border="0" cellpadding="0" cellspacing="0">
                				<tr>
                					<td align="left" valign="middle"><a href="default.aspx"><img src="images/ResortsLogo.gif" alt="Bluegreen Resorts" width="121" height="50" border="0" /></a></td>
                					<td align="right"><table border="0" cellspacing="6" cellpadding="0">
                							<tr>
                								<td align="right"><table border="0" cellspacing="0" cellpadding="0">
                										<tr>
                											<td width="36" align="left"><a href="corporate/index.aspx"><img src="images/go.gif" width="28" height="28" border="0"></a></td>
                											<td width="36" align="left"><a href="explore/home.aspx"><img src="images/see.gif" width="28" height="28" border="0"></a></td>
                											<td><a href="owner/home.aspx"><img src="images/do.gif" width="28" height="28" border="0"></a></td>
           											</tr>
                										</table></td>
           								</tr>
                							<tr>
                								<td align="right"><img src="Owner/images/phone.gif" alt="Phone us at 800.456.2582" border="0"></td>
           								</tr>
                							</table></td>
           					</tr>
                				</table>
                    			<img src="images/no_nav.gif" width="740" height="25"></td>
               		</tr>
                	</table></td>
       		</tr>
        	</table>
        <form id="enrollForm" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
						<td vAlign="top" width="740">
							<table cellSpacing="0" cellPadding="0" width="740" border="0">
								<TBODY>
									<tr>
										<td colSpan="3">
										</td>
									</tr>
									<tr>
										<td vAlign="top" width="193">
											<table class="text" cellSpacing="0" cellPadding="0" width="193" bgColor="#efefef" border="0">
												<tr>
													<td colSpan="3"><IMG height="59" src="images/enroll_img.gif" width="193" border="0"><br>
													</td>
												<tr>
													<td width="10"><IMG height="10" src="images/blank.gif" width="10" border="0"><br>
													</td>
													<td width="173"><IMG height="10" src="images/blank.gif" width="173" border="0"><br>
													</td>
													<td width="10"><IMG height="10" src="images/blank.gif" width="10" border="0"><br>
													</td>
												</tr>
												<tr>
													<td width="10"><IMG height="18" src="images/blank.gif" width="10" border="0"><br>
													</td>
													<td vAlign="top" width="173"><IMG height="24" src="images/yourVacaMemb_title.gif" width="163" border="0"><br>
														Your vacation ownership includes online account access at no additional charge. 
														By enrolling in Bluegreen Online, you can manage your vacation account 
														online:
														<ul class="text">
															<li>
															Pay your Maintenance Fees and Club Dues
															<li>
															Make Bonus Time reservations
															<LI>
															Check your Vacation Point balances
															<li>
															Discover last minute deals and resort specials
															<li>
															Find answers to Frequently Asked Questions (FAQs)
															<li>
																Edit your contact information
															</li>
															<li>
																Access the Encore Rewards&reg; and Bluegreen Traveler Plus&trade; web sites
															</li>
														</ul>
													</td>
													<td width="10"><IMG height="18" src="images/blank.gif" width="10" border="0"><br>
													</td>
												</tr>
												<tr>
													<td colSpan="3"><IMG height="15" src="images/blank.gif" width="193" border="0"><br>
													</td>
												</tr>
												<tr>
													<td background="images/shadow.gif" colSpan="3"><IMG height="30" src="images/blank.gif" width="193" border="0"><br>
													</td>
												</tr>
											</table>
											<br>
										</td>
										<!-- divider -->
										<td vAlign="top" width="50" bgColor="#ffffff"><IMG height="67" src="images/dividerDots4.gif" width="50" border="0"><br>
										</td>
										<!-- end divider -->
										<!-- enroll content -->
										<td id="page-title" vAlign="top" width="497">
											<asp:Image id="imgAlert" runat="server" Visible="False" ImageUrl="images/alert.gif"></asp:Image>&nbsp;&nbsp;
											<asp:label id="lblErrorSignIn" runat="server" Visible="False" CssClass="textRed"></asp:label><br>
											<h2 id="enroll">Enroll In Bluegreen Online</h2>
											<p class="text">To enroll, please complete the following information:</p>
											<!-- personal information -->
											<table class="reservData" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td align="center" width="497" background="images/reservDotRule.gif"><IMG height="1" src="images/blank.gif" width="497" border="0"></td>
												</tr>
												<tr>
													<td align="center" colSpan="497">
														<table class="reservData" cellSpacing="1" cellPadding="0" width="100%" bgColor="#efefef"
															border="0">
															<tr>
																<td class="reservDataLabel" align="center" width="120" bgColor="#ff9900" height="17">Personal 
																	Information</td>
																<td align="right" width="374" background="images/reservBarBkgd.gif"></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td align="center" width="497" background="images/reservDotRule.gif"><IMG height="1" src="images/blank.gif" width="497" border="0"></td>
												</tr>
											</table>
											<br>
											<SPAN class="small">(<SPAN class="textRed">*</SPAN> Required fields)</SPAN> <IMG height="6" src="images/blank.gif" width="497" border="0">
											<strong><SPAN class="textRed">*</SPAN> Owner number:</strong>&nbsp;<asp:textbox id="txtAcctNo" CssClass="text" Runat="server" Width="45px" MaxLength="15"></asp:textbox>
											&nbsp;(<A href="AccountNumberLookup.aspx" class="textlink">Look it up</A>)<br>
											<a href="encorerewards/enroll.aspx" class="textlink">I have not yet received my ownership card with owner number.</a>
											<br><br>
											<IMG height="6" src="images/blank.gif" width="497" border="0"><br>
											<i><STRONG><SPAN class="textRed">*</SPAN> Please provide one of 
													the&nbsp;two items below:</strong></i><br>
											<strong>Last ten digits of the <span class="hl">home phone</span> number on file 
												with Bluegreen:</strong>&nbsp;<asp:textbox id="txtAcctPhone" CssClass="text" Runat="server" Width="70px" MaxLength="10"></asp:textbox><br>
											<strong>
												<BR>
												<span class="prevNext"><FONT size="4">OR</FONT></span></strong><br>
											<BR>
											<strong>Last four digits of the <span class="hl">primary</span> account holder's <span class="hl">
													social security</span> number:</strong>&nbsp;<asp:textbox id="txtAcctSocial" CssClass="text" Runat="server" Width="40px" MaxLength="4"></asp:textbox><br>
											<br>
											<table class="reservData" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td align="center" width="497"><IMG height="1" src="images/blank.gif" width="497" border="0"></td>
												</tr>
												<tr>
													<td align="center" colSpan="497">
														<table class="reservData" cellSpacing="1" cellPadding="0" width="100%" bgColor="#efefef"
															border="0">
															<tr>
																<td class="reservDataLabel" align="center" width="120" bgColor="#ff9900" height="17">Sign 
																	In Information</td>
																<td align="right" width="374" background="images/reservBarBkgd.gif"></td>
															</tr>
														</table>
													</td>
												</tr>
												<tr>
													<td align="center" width="497"><IMG height="1" src="images/blank.gif" width="497" border="0"></td>
												</tr>
											</table><br><br>
											<strong>Please note your email address will be your sign in name.</strong>
											<br>
											<IMG height="6" src="images/blank.gif" width="497" border="0"><br>
											<table class="text" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td vAlign="top" width="363">
														<p><STRONG><SPAN class="textRed">*</SPAN> </STRONG>Email Address:
															<asp:textbox id="txtAcctEmail" CssClass="text" Runat="server" Width="180px" MaxLength="60"></asp:textbox><br>
															<br>
															<STRONG><SPAN class="textRed">*</SPAN> </STRONG>Re-enter Email Address:
															<asp:textbox id="txtAcctEmail2" CssClass="text" Runat="server" Width="180px" MaxLength="60"></asp:textbox></p>
														<p><STRONG><SPAN class="textRed">*</SPAN> </STRONG>Enter Password:
															<asp:textbox id="txtAcctPassword" CssClass="text" Runat="server" Width="120px" TextMode="Password" MaxLength="10"></asp:textbox><br>
															Must be a combination of six to ten&nbsp;letters <STRONG>AND</STRONG>&nbsp;numbers</SPAN></p>
														<p><STRONG><SPAN class="textRed">*</SPAN> </STRONG>Re-enter Password:
															<asp:textbox id="txtAcctPassword2" CssClass="text" Runat="server" Width="120px" TextMode="Password" MaxLength="10"></asp:textbox></SPAN></p>
													</td>
													<td width="116"><IMG height="10" src="images/blank.gif" width="12" border="0"></td>
													<td width="1"><IMG height="1" src="images/blank.gif" width="1" border="0"></td>
													<td width="12"><IMG height="10" src="images/blank.gif" width="12" border="0"></td>
													<td class="text9" vAlign="top" width="5">&nbsp;<br>
														&nbsp;<br>
													</td>
												<tr>
												</tr>
											</table>
											<BR>
											At Bluegreen, your security and privacy are important to us. See our <a href="#" onClick="newWin=window.open('privacy.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
												class="textlink">Privacy Policy</a> for details.<br>
											<br>
											<asp:label id=LblError runat="server" Visible="False" ForeColor="Red"></asp:label><br />
											<IMG height="6" src="images/blank.gif" width="497" border="0"><br>
											<asp:imagebutton id="enrollBtn" Runat="server" ImageUrl="images/enrollBtn.gif"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:imagebutton id="cancelBtn" Runat="server" ImageUrl="images/cancelBtn.gif"></asp:imagebutton>
											<P></P>
										</td>
									</tr>
						</td>
					</tr>
					<tr>
						</TD></tr>
				</TBODY>
			</table>								
			<!-- <center>
				<p><img src="images/blank.gif" width="740" height="20" border="0"><br>
					<img src="images/rule999.gif" width="740" height="1" border="0"></p>
				<P class="foottext"><A class="foottextlink" href="default.aspx">Owner Home</A> &nbsp;&nbsp;|&nbsp;&nbsp;
					<A class="foottextlink" href="corporate/index.aspx" target="_blank">Corporate</A>
					&nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="sitemap.aspx">Site Map</A>
					&nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onClick="newWin=window.open('privacy.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
						href="#">Privacy</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onClick="newWin=window.open('terms.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
						href="#">Terms</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onClick="newWin=window.open('legal.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
						href="#">Legal</A></P>
				<p><table class="copytext" cellSpacing="5" cellPadding="0" border="0" style="WIDTH: 647px; HEIGHT: 54px">
						<tr>
							<td><A href="corporate/index.aspx"><IMG height="21" alt="" src="images/bxg_sml_999.gif" width="65" border="0"></A></td>
							<td align="center" class="copytext">
								<P align="center">ENTIRE SITE COPYRIGHT ©
									<%= year(today()) %>
									BLUEGREEN CORPORATION.<br>
									ALL RIGHTS RESERVED.</P>
							</td>
							<td><IMG height="28" alt="" src="images/equalhousing.gif" width="29" border="0"></td>
						</tr>
					</table>
				</p>-->
			</center>
			</TD>
			<td width="50%" valign="top"><img src="images/blank.gif" width="10" height="10" border="0"></td>
			</TR></TBODY></TABLE> 
		</form>
<includeControlFooter:footer id="footer" runat="server"></includeControlFooter:footer>
	</body>
</HTML>
