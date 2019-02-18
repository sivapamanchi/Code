<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.enrollUnsuccess" Codebehind="enrollUnsuccess.aspx.vb" %>
<%@ Reference Control="~/includes/ucmenu.ascx" %>

<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="includes/ucMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<HTML>
	<HEAD>
		<title>Unsuccessful Enroll</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link rel="stylesheet" rev="stylesheet" href="main.css">		
  		<link href="css/ucmenu.css" rel="stylesheet" type="text/css" />
	</HEAD>
		<body>
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
        <form id="frmLogin" name="frmLogin" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
					<td vAlign="top" width="740">
						<table cellSpacing="0" cellPadding="0" width="740" border="0">

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
												Your vacation&nbsp;ownership includes online account access at no additional 
												charge. By enrolling in Bluegreen Online, you can manage your vacation 
												account online.
												<P></P>
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
								<!-- page content -->
								<td vAlign="top" width="497"><IMG height="10" src="images/blank.gif" width="497" border="0"><br>
									<IMG height="39" src="images/enrollUnsuccess_title2a.gif" width="340" border="0"><br>
									<br>
									<br>
									<table class="resortText" cellSpacing="0" cellPadding="0" width="497" border="0">
										<tr>
											<td vAlign="top" width="24"><IMG height="20" src="images/alert.gif" width="24" border="0"></td>
											<td width="5"><IMG height="20" src="images/blank.gif" width="5" border="0"></td>
											<td><asp:panel id="pnlNoMatch" Visible="False" runat="server">
													<!--<TABLE class="text" style="WIDTH: 464px; HEIGHT: 106px" height="106" cellSpacing="1" cellPadding="3"
														width="464" bgColor="#cccccc" border="0">
														<TR>
															<TD bgColor="white">
																<P><STRONG>SAMPLER OWNERS:</STRONG> Because Sampler ownership follows different 
																	guidelines than full ownership, we are not able to accommodate your online 
																	account at this time. Please be assured that we WILL be adding access to the 
																	Bluegreen Online owner site as soon as it is modified to follow Sampler 
																	policies. We will notify you by email when this process is complete. In the 
																	meantime, you may explore Bluegreen resorts by clicking on the "Explore 
																	Bluegreen Vacation Club" link on the home page. For questions, or to submit 
																	your email address, please call <SPAN style="FONT-WEIGHT: bold; COLOR: #ff9900">800.456.1597</SPAN>, 
																	option 1, or <A class="textlink" href="explore/contact.aspx">email us</A>.</P>
															</TD>
														</TR>
													</TABLE>-->
													<P>The information you provided does not match our records. Please use the <B>BACK</B>
														button to check your owner number, social security number and home phone number 
														to see that you entered them correctly. To look up your owner (account) number, <A class="textlink" href="accountnumberlookup.aspx">
															click here</A>.
														<BR>
														<BR>
														If you are a new Bluegreen owner, it takes approximately 30-45 days for your 
														contract information to become effective in our computer system. Once your 
														information is processed, you will be able to enroll in Bluegreen Online 
														Services. Please check back periodically or call <SPAN style="FONT-WEIGHT: bold; COLOR: #ff9900">
															800.456.CLUB (2582)</SPAN> to inquire about your status. In the meantime, <A class="textlink" href="explore/Home.aspx">
															click here</A> to explore the Bluegreen Vacation Club.
														<BR>
														<BR>
														If you need further assistance, contact us at <SPAN style="FONT-WEIGHT: bold; COLOR: #ff9900">
															800.456.CLUB (2582)</SPAN> or send us an <A class="textlink" href="explore/contact.aspx">
															email now</A>.
														<BR>
														<BR>
													</P>
												</asp:panel>
												<asp:panel id="pnlEnrolled" runat="server" visible="False">According 
                  to our records you have already enrolled in Bluegreen Online 
                  Services. If you would like to update your account 
                  information, sign in and go to <STRONG>My Account</STRONG>. 
                  Use the <B>BACK</B> button below to return to the sign in page.
                  
                  </asp:panel><br>
												<br>
												&nbsp;&nbsp;&nbsp;
												<A HREF="javascript: history.go(-1)"><IMG BORDER="0" SRC="Owner/images/gobackBtn.gif"></A>
												<asp:ImageButton id="imgGoBack" runat="server" ImageUrl="Owner/images/gobackBtn.gif" Visible=false></asp:ImageButton></td>
										</tr>
									</table>
									<br>
									<br>
								</td>
							</tr>
							<!-- end page content -->
							<tr>
								<td colSpan="3"><center>
									<center>
								</center>
									</center>
								</td>
							</tr>
						</table>
					</td>
					<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0"></td>
				</tr>
			</table>
		</form>
<includeControlFooter:footer id="footer" runat="server"></includeControlFooter:footer>
	</body>
</HTML>
