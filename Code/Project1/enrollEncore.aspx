<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="enroll.aspx.vb" Inherits="BluegreenOnline.enroll"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Enrollment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rev="stylesheet" href="main.css" rel="stylesheet">
		<script language="javascript" src="scripts/rollover.js"></script>
	</HEAD>
	<body leftMargin="0" background="images/enrollBkgd2.gif" topMargin="0" onload="MM_preloadImages('images/logout_btn_o.gif','images/ownerhome_o.gif','images/resortspecials_o.gif','images/newprop_o.gif','images/vacationclub_o.gif','images/vacationrent_o.gif')"
		marginheight="0" marginwidth="0">
		<!-- begin HBX code -->
		<script language="javascript1.1" src="js/hbx_page_code_standard.js"></script>
		<!-- end HBX code -->
		<form id="enrollForm" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td vAlign="top" width="50%" background="images/enrollBkgd2_lft.gif"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
						<td vAlign="top" width="740">
							<!-- header bar -->
							<table cellSpacing="0" cellPadding="0" width="740" border="0">
								<tr>
									<td vAlign="top" width="740"><IMG height="17" alt="" src="images/headerBar1.gif" width="740" border="0"></td>
								</tr>
								<tr>
									<td vAlign="top" bgColor="#666666" colSpan="2"><IMG height="1" src="images/blank.gif" width="100%" border="0"></td>
								</tr>
								<tr>
									<td vAlign="top" background="images/headerBkgd1.gif" colSpan="2">
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td width="129"><A href="default.aspx"><IMG height="63" alt="Bluegreen Vacation Club" src="images/bgvc_logo2.gif" width="129"
															border="0"></A></td>
												<td width="371"><IMG height="63" src="images/blank.gif" width="371" border="0"></td>
												<td width="240"><A onmouseover="MM_swapImage('hdrTag_','','images/hdrTag_o.gif',1)" onmouseout="MM_swapImgRestore()"
														href="#"><IMG height="63" alt="Phone us at 800.456.CLUB (2582)" src="images/hdrTag_x.gif" width="240"
															border="0" name="hdrTag_"></A></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<!-- end header bar -->
							<table cellSpacing="0" cellPadding="0" width="740" border="0">
								<TBODY>
									<tr>
										<td colSpan="3">
											<table cellSpacing="0" cellPadding="0" width="740" border="0">
												<tr>
													<td width="736" bgColor="#666666"><IMG height="19" alt="" src="images/blank.gif" width="736" border="0"></td>
													<td width="4" bgColor="#ff9900"><IMG height="19" alt="" src="images/blank.gif" width="4" border="0"></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td vAlign="top" width="193">
											<table class="text" cellSpacing="0" cellPadding="0" width="193" bgColor="#efefef" border="0">
												<tr>
													<td colSpan="3"><IMG height="59" src="encoreRewards/images/enroll_img.gif" width="193" border="0"><br>
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
													<td vAlign="top" width="173"><IMG height="24" src="encorerewards/images/Enroll_titleSm.gif" width="163" border="0"><br>
														Refer a friend to Bluegreen and you’ll receive thousands of Encore Rewards™ you can redeem for:
														<ul class="text">
															<li>
															Great vacation stays
															<li>
															Payment of RCI® or Interval International® exchange fees
															<LI>
															 MasterCard® gift cards
														</ul>
														So go ahead, share the fun of a Bluegreen vacation with your friends and family and you’ll earn Encore Rewards™ the fastest way possible!
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
										<td vAlign="top" width="497">
											<IMG height="10" src="images/blank.gif" width="497" border="0"><br>
											<IMG height="39" src="encoreRewards/images/enroll_title.gif" width="340" border="0"><br>
											<br>
											<asp:Image id="imgAlert" runat="server" Visible="False" ImageUrl="images/alert.gif"></asp:Image>&nbsp;&nbsp;
											<asp:label id="lblErrorSignIn" runat="server" Visible="False" CssClass="textRed"></asp:label><br>
											
											
											<table class="reservData" cellSpacing="0" cellPadding="0" width="100%" border="0">
															<tr>
														<td>As a new Bluegreen owner, you can enroll in Encore Rewards and immediately begin earning Encore Rewards™ for referring your friends and family! Once you receive your owner number in approximately 30 days, you can then enroll in Bluegreen Online and access all the exciting benefits of the web site.<br><br></td>
															</tr>
															<tr>
															<tr>
													<td align="center" width="497" background="images/reservDotRule.gif"><IMG height="1" src="images/blank.gif" width="497" border="0"></td>
												</tr>
												<tr>
													<td align="center" colSpan="497">
														<table class="reservData" cellSpacing="1" cellPadding="0" width="100%" bgColor="#efefef"
															border="0">
															
																<td class="reservDataLabel" align="center" width="120" bgColor="#ff9900" height="17">Sign 
																	In Information</td>
																
																
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
											<strong class="resortName">Please Provide:</strong>
											<IMG height="6" src="images/blank.gif" width="497" border="0"><br>
											<table class="text" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td vAlign="top" width="497">
												<tr>
											<STRONG><SPAN class="textRed">*</SPAN> </STRONG><strong>Last ten digits of the <span class="hl">home phone</span> number on file 
												with Bluegreen:</strong>&nbsp;<asp:textbox id="txtAcctPhone" CssClass="text" Runat="server" Width="70px" MaxLength="10"></asp:textbox><br><br>
																		
											<strong>Please note your email address will be your sign in name.</strong>
												</tr></td>
														<td vAlign="top" width="497">
														<p><STRONG><SPAN class="textRed">*</SPAN> </STRONG>Email Address:
															<asp:textbox id="txtAcctEmail" CssClass="text" Runat="server" Width="120px"></asp:textbox><br>
															<br>
															<STRONG><SPAN class="textRed">*</SPAN> </STRONG>Re-enter Email Address:
															<asp:textbox id="txtAcctEmail2" CssClass="text" Runat="server" Width="120px"></asp:textbox></p>
														<p><STRONG><SPAN class="textRed">*</SPAN> </STRONG>Enter Password:
															<asp:textbox id="txtAcctPassword" CssClass="text" Runat="server" Width="120px" TextMode="Password"></asp:textbox><br>
															Must be a combination of six to ten&nbsp;letters <STRONG>AND</STRONG>&nbsp;numbers</SPAN></p>
														<p><STRONG><SPAN class="textRed">*</SPAN> </STRONG>Re-enter Password:
															<asp:textbox id="txtAcctPassword2" CssClass="text" Runat="server" Width="120px" TextMode="Password"></asp:textbox></SPAN></p>
													</td>
													<td width="116"><IMG height="10" src="images/blank.gif" width="12" border="0"></td>
													<td width="1" background="images/vDotRuleBkgd.gif"><IMG height="1" src="images/blank.gif" width="1" border="0"></td>
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
			<center>
				<p><img src="images/blank.gif" width="740" height="20" border="0"><br>
					<img src="images/rule999.gif" width="740" height="1" border="0"></p>
				<P class="foottext"><A class="foottextlink" href="default.aspx">Owner Home</A> &nbsp;&nbsp;|&nbsp;&nbsp;
					<A class="foottextlink" href="corporate/index.aspx" target="_blank">Corporate</A>
					&nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" href="sitemap.aspx">Site Map</A>
					&nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('privacy.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
						href="#">Privacy</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('terms.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
						href="#">Terms</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('legal.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
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
				</p>
			</center>
			</TD>
			<td width="50%" valign="top"><img src="images/blank.gif" width="10" height="10" border="0"></td>
			</TR></TBODY></TABLE>
		</form>
		<!-- begin HBX code -->
		<script language="javascript1.1" defer src="js/hbx.js"></script>
		<!-- end HBX code -->
	</body>
</HTML>
