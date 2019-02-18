<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.AccountNumberLookup" Codebehind="AccountNumberLookup.aspx.vb" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="includes/ucMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Acct Number Lookup</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link rel="stylesheet" rev="stylesheet" href="main.css">		
   		<link href="css/ucmenu.css" rel="stylesheet" type="text/css" />
		<link rel="stylesheet" rev="stylesheet" href="owner/owner.css">	
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
						<td vAlign="top" width="50%"><IMG height="10" src="images/blank.gif" width="10" border="0">	</td>
						<td vAlign="top" width="740">
							<table cellSpacing="0" cellPadding="0" width="740" border="0">
								<tbody>
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
														online.
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
										<td id="page-title" vAlign="top" width="497"><IMG height="10" src="images/blank.gif" width="497" border="0"><br>
											<h2 id="enroll">Enroll In Bluegreenonline</h2>
											<asp:image id="imgAlert" runat="server" ImageUrl="images/alert.gif" Visible="False"></asp:image>&nbsp;
											<asp:label id="lblErrorPersonal" runat="server" Visible="False" CssClass="textRed"></asp:label><br>
											<span class="text">To look up your owner number, please complete the following 
												information:</span><br>
											<br>
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
											<asp:panel id="pnlEntry" runat="server" Visible="True"><IMG height="6" src="images/blank.gif" width="497" border="0"><BR><B>
													<I>Please provide&nbsp;the two items below.</I></B><BR><STRONG>1. Last four 
													digits of the <SPAN class="hl">primary</SPAN> account holder's <SPAN class="hl">social 
														security</SPAN> number:</STRONG>&nbsp; 
                                            <asp:textbox id="txtAcctSocial" CssClass="text" Width="40px" Runat="server" MaxLength="4"></asp:textbox><BR><BR><STRONG>
                                            <FONT size="4">AND</FONT></STRONG> <BR/><BR/><STRONG>2. Last ten digits 
													                                            of the <SPAN class="hl">home phone</SPAN> number on file with Bluegreen:</STRONG>&nbsp; 
                                            <asp:textbox id="txtAcctPhone" CssClass="text" Width="70px" Runat="server" MaxLength="10"></asp:textbox><BR><BR>At 
                                                        Bluegreen, your security and privacy are important to us. We will 
                                                        never sell or rent your personal information. See our <A class="textlink" onClick="newWin=window.open('privacy.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=600,height=600,left=50,top=20'); return false;"
													                                            href="#">Privacy Policy</A> for details.<BR><BR><IMG height="6" src="images/blank.gif" width="497" border="0"><BR>
                                            <asp:imagebutton id="submitBtn" ImageUrl="Owner/images/submitBtn.gif" Runat="server"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp; 
                                            <asp:imagebutton id="cancelBtn" ImageUrl="images/cancelBtn.gif" Runat="server"></asp:imagebutton></asp:panel><asp:panel id="pnlResult" runat="server" Visible="False"><BR>Your account number is: <B>
													                                            <asp:Label id="lblAcctID" Runat="server"></asp:Label></B><BR><BR>
                                            <asp:imagebutton id="btnGoToEnroll" ImageUrl="images/enrollBtn.gif" Runat="server"></asp:imagebutton></asp:panel></td>
									</tr>
								
								
						
					
				</TBODY>
			                </table>
			
				        </TD>
						<td vAlign="top" width="50%">   <IMG height="10" src="images/blank.gif" width="10" border="0">
			</td>
			</TR>
		
			    </TBODY>
		    </TABLE>
		</form>		
		<includecontrolfooter:footer id="footer" runat="server"></includecontrolfooter:footer>
	</body>
</HTML>
