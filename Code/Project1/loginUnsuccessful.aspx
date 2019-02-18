<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.loginUnsuccessful" CodeBehind="loginUnsuccessful.aspx.vb" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Register TagPrefix="includeOwnerLogin" TagName="OwnerLogin" Src="includes/OwnerLogin.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Unsuccessful Login</title>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
		<link rev="stylesheet" href="owner/owner.css" rel="stylesheet" />
	    <script language="javascript" src="scripts/rollover.js"></script>
	</head>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">

			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top" style="width: 50%"><img height="10" src="images/blank.gif" width="10" border="0"></td>
					<td vAlign="top" width="740">
						<!-- header bar -->
						<table width="740" border="0" cellspacing="0" cellpadding="0">
                        	<tr>
                        		<td align="center"><table width="730" height="70" border="0" cellpadding="0" cellspacing="0">
                        				<tr>
                        					<td align="left" valign="middle"><a href="default.aspx"><img src="images/ResortsLogo.gif" alt="Bluegreen Resorts" width="271" height="20" border="0" /></a></td>
                        					<td align="right"><table border="0" cellspacing="6" cellpadding="0">
                        							<%--<tr>
                        								<td align="right"><table border="0" cellspacing="0" cellpadding="0">
                        										<tr>
                        											<td width="36" align="left"><a href="corporate/index.aspx"><img src="images/go.gif" width="28" height="28" border="0"></a></td>
                        											<td width="36" align="left"><a href="explore/home.aspx"><img src="images/see.gif" width="28" height="28" border="0"></a></td>
                        											<td><a href="owner/home.aspx"><img src="images/do.gif" width="28" height="28" border="0"></a></td>
                   											</tr>
                        										</table></td>
                   								</tr>--%>
                        							<tr>
                        								<td align="right"><img src="Owner/images/phone.gif" alt="Phone us at 800.456.2582" border="0"></td>
                   								</tr>
                        							</table></td>
                   					</tr>
                        				</table>
                            			<img src="images/no_nav.gif" width="740" height="25"></td>
                       		</tr>
                        	</table>
						<!-- end header bar -->
						<table cellSpacing="0" cellPadding="0" width="740" border="0">
							<TBODY>

								<tr>
									<td vAlign="top" width="193">
										<table class="text" cellSpacing="0" cellPadding="0" width="193" bgColor="white" border="0">
											<tr>
												<td width="173" colSpan="3">&nbsp;												</td>
											
											<tr>
												
												<td vAlign="top" width="173">
												
												    <!-- owner login -->
								                    <includeOwnerLogin:OwnerLogin id="OwnerLogin" runat="server"></includeOwnerLogin:OwnerLogin>
								                    <!-- end owner login -->
								                    
								                    <br />
								                    <br />
								                    
												    
													<P></P>
													
													
								                    
												</td>
												
											</tr>
											<tr>
												<td colSpan="3"><img alt="_blank" height="15" src="images/blank.gif" width="193" border="0" /><br />
												</td>
											</tr>
											<tr>
												<td colSpan="3" style="height: 14px">
												</td>
											</tr>
										</table>
										<br />
									</td>
									<!-- divider -->
									<td vAlign="top" width="50" bgColor="#ffffff"><img height="67" src="images/dividerDots4.gif" width="50" border="0"><br />
									</td>
									<!-- end divider -->
									<!-- page content -->
									<td vAlign="top" width="497"><img height="10" src="images/blank.gif" width="497" border="0"><br />
										<img src="images/pg-title-help.gif" width="497" height="42" border="0"><br />
										<br />
										<br />
										<table class="resortText" cellSpacing="0" cellPadding="0" width="497" border="0">
											<TBODY>
												<tr>
													<td vAlign="top" width="24"><img height="20" src="images/alert.gif" width="24" border="0"></td>
													<td width="5"><img height="20" src="images/blank.gif" width="5" border="0"></td>
													<td><asp:panel id="pnlNonSampler" Runat="server">
															<p>&nbsp;<strong class="hl">We're sorry, we were unable to verify your information</STRONG>.&nbsp; 
																Please ensure that the email and password you provided were entered correctly. 
																Use the sign in fields on the menu to the left to enter your email and password again.
																<br />
																<br />
																<a class="textlink" href="#1">Sign in help</a><br />
																<a class="textlink" href="#2">Change your email address or password</a><br />
																<a class="textlink" href="#3">Remember me</a><br />
																<a class="textlink" href="#4">Passwords</a></p>
															<p><span class="searchTitle"><a name="1"></a>How do I sign in?</span><br />
																<br />
																If you have already enrolled in Bluegreen Online, enter your email address and 
																password in the input fields provided. You should use the email address and 
																password on file with Bluegreen.
															</p>
															<p>If you have not enrolled in Bluegreen Online, you can <a class="textlink" href="enroll.aspx">
																	register now</a> in a few easy steps.<br />
																<br />
																
											            <%--<p><strong>SAMPLER Owners:</strong> Because Sampler package ownership follows different guidelines than full Bluegreen Resorts ownership, we are not able to accommodate Sampler 
													            accounts online at this time. We apologize for the inconvenience. <br /><br />However, you can still <u><a class="textlink" href="http://www.bluegreenonline.com/explore/home.aspx" title="Explore Bluegreen">Explore Bluegreen Resorts online</a></u> to view resort descriptions, 
													            amenities, photos, floor plans, virtual tours, maps and directions, weather, area information and important resort notes. <u><a class="textlink" href="http://www.bluegreenonline.com/explore/home.aspx" title="Explore Bluegreen">Click here</a></u> or the “Explore Bluegreen Resorts” 
													            link on the home page. To make a reservation, please call <strong>800.459.1597</strong> to speak with a Bluegreen Vacation Specialist, Monday -Friday 8 a.m. - 9 p.m.</p>
													          --%>  
																<strong>AOL users: </strong>Please be sure to enter the email address 
																associated with your online account and not your AOL screen name.<p>
                                                                </p>
                                                                <p>
                                                                    <strong>Have you recently upgraded your Vacation Points?</strong><br /> If you 
                                                                    have, you may have been assigned a new owner number and will need to
                                                                    <a class="textlink" href="enroll.aspx">enroll the new account</a>. You should 
                                                                    use your email address on file with Bluegreen and a password DIFFERENT from the 
                                                                    one you used when you first enrolled. If you cannot sign in and you don't know 
                                                                    your new owner number, please call <strong>800.456.CLUB (2582)</strong> for 
                                                                    assistance.</p>
                                                                <p>
                                                                    <strong>Has your email address changed since you last signed in?</strong><br /> 
                                                                    If it has, and it has not been updated in our system, you should use your old 
                                                                    email address to sign in, then go to <strong>My Account</strong> to update it 
                                                                    (see below).</p>
                                                                <p class="searchTitle">
                                                                    <a id="2" name="2"></a>Change your email address or password
                                                                </p>
                                                                <p>
                                                                    You can change your email address or password at any time. Once you have signed 
                                                                    in, go to the <strong>My Account</strong> page. Scroll to the last line in the 
                                                                    Contact Information section, enter your new email address then <strong>UPDATE</strong>
                                                                    your changes. The link to change your password is directly below the email 
                                                                    field.
                                                                    <br />
                                                                    <br />
                                                                    <img height="69" src="images/changePassword.gif" width="379"></img></p>
                                                                <p>
                                                                    <span class="searchTitle"><a id="3" name="3"></a>Remember me</span><br />
                                                                    <br />
                                                                    Check this box if you want bluegreenonline.com to remember your email address 
                                                                    for future visits. For security purposes, you will still need to enter your 
                                                                    password.
                                                                </p>
                                                                <p class="searchTitle">
                                                                    <a id="4" name="4"></a>Passwords</p>
                                                                <p>
                                                                    To ensure your privacy, passwords must be a combination of six to ten letters 
                                                                    AND numbers. Also, passwords are case-sensitive. Be sure to enter your password 
                                                                    exactly as you created it. Be careful when typing upper and lower case letters, 
                                                                    and make sure your Caps Lock key is not on.<br />
                                                                    <br />
                                                                    If you need further assistance, contact us at
                                                                    <span style="FONT-WEIGHT: bold; COLOR: #ff9900">800.456.CLUB (2582)</span> or 
                                                                    send us an <a class="textlink" href="explore/contact.aspx">email now</a>.
                                                                    
                                                                </p>
                                                               
                                                                <p>
                                                                </p>
                                                               
                                                            </p>
														</asp:panel><asp:panel id="pnlSampler" runat="server"><br />
															<table class="text" cellSpacing="1" cellPadding="3" width="456" bgColor="#cccccc" border="0">
																<tr>
																	<td bgColor="white">
																		<p><span style="FONT-WEIGHT: bold; COLOR: #ff9900">VALUE SAMPLER MEMBERS:</span>
																			Because Value Sampler membership follows different guidelines than full ownership, we are
                                                                            not able to accommodate your online account. One of the 
																			benefits of Bluegreen ownership is full access to the many features we offer 
																			online. Until you become an owner, you may explore Bluegreen resorts by <A class="textlink" href="explore/home.aspx">
																				clicking here.</A>
																		</P>
																	</td>
																</TR>
															</TABLE>
															<br />
															<br />
														</asp:panel>														
														<asp:panel id="PnlPlayer" runat="server" Visible="False"><br />
															<TABLE class="text" cellSpacing="1" cellPadding="3" width="456" bgColor="#cccccc" border="0">
																<TR>
																	<TD bgColor="white">
																		<P><span style="FONT-WEIGHT: bold; COLOR: #ff9900">PLAYER'S CLUB MEMBERS:Bluegreen no longer services these accounts. </span>
																			If you have any questions please contact at 800.456.2582.
																		</P>
																	</td>
																</TR>
															</TABLE>
															<br />
															<br />
														</asp:panel>
                                                           <%---- Added for Pono Kai Owners ----%>
														<asp:panel id="PnlPono" runat="server" Visible="False"><br />
															<table class="text" cellSpacing="1" cellPadding="3" width="456" bgColor="#cccccc" border="0">
																<TR>
																	<TD bgColor="white">
																		<P><span style="FONT-WEIGHT: bold; COLOR: #ff9900">PONO KAI MEMBERS:Bluegreen no longer services these accounts. </span>
																			If you have any questions please contact at 800.456.2582.
																		</P>
																	</td>
																</TR>
															</TABLE>
															<br />
															<br />
														</asp:panel>
														
													</td>
												</tr>
											</TBODY></table>
										<br />
										<br />
									</td>
								</tr>
								<!-- end page content -->
								<!-- footer -->
								<tr>
									<td colSpan="3">
										<center>
											<p><img height="20" src="images/blank.gif" width="740" border="0"><br />
												<img height="1" src="images/rule999.gif" width="740" border="0"></p>
											<P class="foottext"><A class="foottextlink" href="default.aspx">Owner Home</A> &nbsp;&nbsp;|&nbsp;&nbsp;
												<A class="foottextlink" href="corporate/index.aspx">Corporate</A> &nbsp;&nbsp;|&nbsp;&nbsp;
												<A class="foottextlink" href="sitemap.aspx?">Site Map</A> &nbsp;&nbsp;|&nbsp;&nbsp;
												<A class="foottextlink" onclick="newWin=window.open('./privacy.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;" href="#" > 
												Privacy</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('./terms.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;" href="#" > 
												Terms</A> &nbsp;&nbsp;|&nbsp;&nbsp; <A class="foottextlink" onclick="newWin=window.open('./legal.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;" href="#" >
													Legal</A></P> 
											<p>
											<table class="copytext" cellSpacing="5" cellPadding="0" border="0">
													<tr>
														<td><A href="../corporate/index.aspx"><img height="21" alt="" src="images/bxg_sml_999.gif" width="65" border="0"></A></td>
														<td class="copytext" align="center" width="100%">ENTIRE SITE COPYRIGHT © <script type="text/javascript">
                var currentTime = new Date()
                var year = currentTime.getFullYear()
                document.write(year);
</script> 
															BLUEGREEN CORPORATION.<br />
															ALL RIGHTS RESERVED.</td>
														<td><img height="28" alt="" src="images/equalhousing.gif" width="29" border="0"></td>
													</tr>
											</table>
											</p>
											<center></center>
										</center>
									</td>
								</tr>
							</TBODY></table>
					</td>
					<td vAlign="top" width="50%"><img height="10" src="images/blank.gif" width="10" border="0"></td>
				</tr>
			</table>
			<!-- end footer --> 
	</body>
</HTML>
