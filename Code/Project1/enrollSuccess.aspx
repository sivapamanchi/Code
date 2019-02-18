<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.enrollSuccess" Codebehind="enrollSuccess.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="includes/ucMenu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title>Successful Enroll</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link rel="stylesheet" rev="stylesheet" href="main.css" />
    <link href="css/ucmenu.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <table width="740" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">
                            <table width="730" height="70" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" valign="middle">
                                        <a href="default.aspx">
                                            <img src="images/ResortsLogo.gif" alt="Bluegreen Resorts" width="121" height="50"
                                                border="0" /></a></td>
                                    <td align="right">
                                        <table border="0" cellspacing="6" cellpadding="0">
                                            <tr>
                                                <td align="right">
                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width:36" align="left">
                                                                <a href="corporate/index.aspx">
                                                                    <img src="images/go.gif" width="28" height="28" border="0" alt="" /></a></td>
                                                            <td style="width:36" align="left">
                                                                <a href="explore/home.aspx">
                                                                    <img src="images/see.gif" width="28" height="28" border="0" alt="" /></a></td>
                                                            <td>
                                                                <a href="owner/home.aspx">
                                                                    <img src="images/do.gif" width="28" height="28" border="0" alt="" /></a></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right"><img src="Owner/images/phone.gif" alt="Phone us at 800.456.2582" border="0"></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <img src="images/no_nav.gif" width="740" height="25"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <form id="enrollSuccessForm" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tbody>
                <tr>
                    <td id="noRepeat" valign="top" style="width:50%">
                        <img height="10" src="images/blank.gif" width="10" border="0" alt="" /></td>
                    <td valign="top" style="width:740">
                        <table cellspacing="0" cellpadding="0" width="740" border="0">
                            <tbody>
                                <tr>
                                    <td valign="top" style="width:193">
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
                                    <td valign="top" style="width:50; background-color:#ffffff">
                                        <img height="67" src="images/dividerDots4.gif" width="50" style="border:0" alt="" /><br />
                                    </td>
                                    <!-- end divider -->
                                    <!-- enroll content -->
                                    <td valign="top" style="width:497">
                                        <table class="resortText" cellspacing="0" cellpadding="0" width="497" border="0">
                                            <tr>
                                                <td>
													<p><font color="#24a61a" size="+2"><img src="images/successIcon.gif" width="24" height="20" hspace="5">Congratulations!</font></p>
													<p><font color="#24a61a" size="3">You have successfully enrolled in Bluegreen Online</font><font size="3">.</font></p>
													<p>The next time you sign in, all you will need is your sign-in name (email address) and password.</p>
												
									<p style="padding: 10px; background: #f6f2d1;"> Please confirm or update your contact information and travel profile below and select
                                                      <strong>CONFIRM MY ACCOUNT DETAILS</strong>.</p>	
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Image ID="imgAlert" runat="server" Visible="False" ImageUrl="images/alert.gif">
                                        </asp:Image>&nbsp;&nbsp;
                                        <asp:Label ID="lblAddVerifyErr" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                                        <!-- contact information -->
                                        <table class="reservData" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td align="center" width="497" background="images/reservDotRule.gif">
                                                    <img height="1" src="images/blank.gif" width="497" border="0"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="497">
                                                    <table class="reservData" cellspacing="1" cellpadding="3" width="100%" bgcolor="#efefef"
                                                        border="0">
                                                        <tr>
                                                            <td width="200" height="17" align="left" bgcolor="#ff9900" class="reservDataLabel">
                                                                &nbsp;Account Information &amp; Travel Profile </td>
                                                            <td align="right" background="images/reservBarBkgd.gif"> (<font color="#ff0000">*</font> Required fields)
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="497" background="images/reservDotRule.gif">
                                                    <img height="1" src="images/blank.gif" width="497" border="0"></td>
                                            </tr>
                                        </table>
										<p>If you need to update a military address or need assistance, please call 800.456.CLUB (2582) within the U.S.; if you are calling from the U.K., you may call toll-free 00.800.4707.4707; all others outside the U.S. and U.K. dial 317.572.2065 </p>

										
										
                                        <table class="reservData" cellspacing="0" cellpadding="3" width="100%" border="0">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">
                                                        Primary Account Contact</td>
                                                </tr>
                                                <tr>
                                                    <td width="118">
                                                        Account Number:</td>
                                                    <td width="371">
                                                        <asp:Label ID="lblAcctNo" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td width="118">
                                                        <font color="#cc0000">**</font>First Name:</td>
                                                    <td width="371">
                                                        <asp:Label ID="lblFirstName" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 20px" width="118">
                                                        <font color="#cc0000">**</font>Last Name:</td>
                                                    <td style="height: 20px" width="371">
                                                        <asp:Label ID="lblLastName" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td width="118">
                                                        <font color="#cc0000">*</font>Address1:</td>
                                                    <td width="371">
                                                        <asp:TextBox ID="txtAddress1" runat="server" Width="345px" MaxLength="30"></asp:TextBox>
                                                        <%If Session("msg") = True Then%>
                                                        <div class="reservData" style="vertical-align: top; color: black" id="msg1">
                                                            Enter only your Street Address</div>
                                                        <%End If%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="118">
                                                        <asp:Label ID="lblAdd2Req" runat="server" Visible="False">
																<font color="#cc0000">*</font></asp:Label>Address2:</td>
                                                    <td width="371">
                                                        <asp:TextBox ID="txtAddress2" runat="server" Width="345px" MaxLength="30"></asp:TextBox>
                                                        <%If Session("msg") = True Then%>
                                                        <div class="reservData" style="vertical-align: top; color: black;" id="msg2">
                                                            Enter your apartment, unit and/or building number (if applicable)</div>
                                                        <%End If%>
                                                    </td>
                                </tr>
                                <asp:Panel ID="pnlCountryUS" runat="server">
                                    <tr>
                                        <td width="118">
                                            <font color="#cc0000">*</font>City:</td>
                                        <td width="371">
                                            <asp:TextBox ID="txtCity" runat="server" MaxLength="50" Width="345px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td width="118">
                                            <font color="#cc0000">*</font>State/Province:</td>
                                        <td width="371">
                                            <asp:DropDownList ID="ddlState" runat="server" Width="345px">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td width="118">
                                            <asp:Label ID="lblPostal" runat="server"><font color="#cc0000">*</font>Zip/Postal Code:</asp:Label></td>
                                        <td width="371">
                                            <asp:TextBox ID="txtZip" runat="server" MaxLength="10" Width="345px"></asp:TextBox></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pnlCountryNotUS" runat="server" Visible="false">
                                    <tr>
                                        <td width="118">
                                            Address3:</td>
                                        <td width="371">
                                            <asp:TextBox ID="txtAddress3" runat="server" MaxLength="300" Width="345px"></asp:TextBox></td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td width="118">
                                        <font color="#cc0000">*</font>Country:</td>
                                    <td width="371">
                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="345px" AutoPostBack="True">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td width="118">
                                        <font color="#cc0000">*</font>Home Phone:</td>
                                    <td width="371">
                                        <asp:TextBox ID="txtHomePhone" runat="server" Width="345px" MaxLength="15"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td width="118">
                                        <font color="#cc0000">*</font>Alternate Phone:</td>
                                    <td width="371">
                                        <asp:TextBox ID="txtDayPhone" runat="server" Width="345px" MaxLength="15"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td width="118">
                                        <font color="#cc0000">*</font>Sign In Name/Email:</td>
                                    <td width="371">
                                        <asp:TextBox ID="txtEmail" runat="server" Width="345px" MaxLength="60"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td width="118">
                                    </td>
                                    <td width="371">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <!-- travel profile -->
                        <table class="reservData" cellspacing="0" cellpadding="3" width="100%" border="0">
                            <tr>
                                <td colspan="2">
                                    By letting us know what your interests are, we can keep you updated when we add
                                    new resorts or activities at our current resorts that match your preferred vacation
                                    experience.</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    Preferred vacation experience (check all that apply):<br />
                                    <table class="reservData" cellspacing="0" cellpadding="3" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:CheckBoxList ID="chklistVacExp" runat="server" RepeatColumns="3">
                                                    <asp:ListItem Value="Adventure">Adventure</asp:ListItem>
                                                    <asp:ListItem Value="Beach">Beach</asp:ListItem>
                                                    <asp:ListItem Value="Bicycling">Bicycling</asp:ListItem>
                                                    <asp:ListItem Value="Boating/Sailing">Boating/Sailing</asp:ListItem>
                                                    <asp:ListItem Value="Camping">Camping</asp:ListItem>
                                                    <asp:ListItem Value="Casinos">Casinos</asp:ListItem>
                                                    <asp:ListItem Value="Collectibles &amp; Antiques">Collectibles &amp; Antiques</asp:ListItem>
                                                    <asp:ListItem Value="Cruises">Cruises</asp:ListItem>
                                                    <asp:ListItem Value="Dieting/Weight Control">Dieting/Weight Control</asp:ListItem>
                                                    <asp:ListItem Value="Fishing">Fishing</asp:ListItem>
                                                    <asp:ListItem Value="Fitness/Health">Fitness/Health</asp:ListItem>
                                                    <asp:ListItem Value="Golf">Golf</asp:ListItem>
                                                    <asp:ListItem Value="Gourmet Cooking">Gourmet Cooking</asp:ListItem>
                                                    <asp:ListItem Value="Hiking">Hiking</asp:ListItem>
                                                    <asp:ListItem Value="Historical">Historical</asp:ListItem>
                                                    <asp:ListItem Value="Horseback Riding">Horseback Riding</asp:ListItem>
                                                    <asp:ListItem Value="Hunting">Hunting</asp:ListItem>
                                                    <asp:ListItem Value="Motorcycling">Motorcycling</asp:ListItem>
                                                    <asp:ListItem Value="Mountains">Mountains</asp:ListItem>
                                                    <asp:ListItem Value="Sightseeing">Sightseeing</asp:ListItem>
                                                    <asp:ListItem Value="Snow Skiing">Snow Skiing</asp:ListItem>
                                                    <asp:ListItem Value="Theme Parks">Theme Parks</asp:ListItem>
                                                    <asp:ListItem Value="Water Skiing">Water Skiing</asp:ListItem>
                                                    <asp:ListItem Value="Wine Tasting">Wine Tasting</asp:ListItem>
                                                </asp:CheckBoxList></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr> <br />
                            <tr>
                                <td>
                                    Preferred vacation destination: 
                                    <asp:DropDownList ID="ddlDestination" runat="server">
                                        <asp:ListItem Value="" Selected="True">Select One</asp:ListItem>
                                        <asp:ListItem Value="NE U.S.">NE U.S.</asp:ListItem>
                                        <asp:ListItem Value="SE U.S.">SE U.S.</asp:ListItem>
                                        <asp:ListItem Value="Central U.S.">Central U.S.</asp:ListItem>
                                        <asp:ListItem Value="SW U.S.">SW U.S.</asp:ListItem>
                                        <asp:ListItem Value="NW U.S.">NW U.S.</asp:ListItem>
                                        <asp:ListItem Value="Caribbean">Caribbean</asp:ListItem>
                                        <asp:ListItem Value="Canada">Canada</asp:ListItem>
                                        <asp:ListItem Value="International">International</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                </td>
                            </tr>

                        </table>
                        <!-- end travel profile -->
                        
						
						<!-- update/confirm buttons -->
                        <img height="16" src="images/blank.gif" width="497" border="0">
                        &nbsp;&nbsp;&nbsp;
                       <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                           <tr>
                               <td align="center">
                                   <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="images/Btn_ConfirmAccountDetails.gif"></asp:ImageButton>
                               </td>
                           </tr>
						</table>

					   <img height="16" src="images/blank.gif" width="497" border="0"><br />
                        &nbsp;&nbsp;&nbsp;
                       <!-- end update/confirm buttons -->						

                        <span class="text9">
                                           <a class="textlink" href="https://bluegreentransfers.wufoo.com/forms/bluegreen-vacations-title-transfer-request/"><font color="#cc0000"><strong>**</strong>Click here for information about making name changes to your title.</font></a></span><br />
                        <br />
                        <!-- end contact information -->
                        <!-- vacation contract info -->
                        <table class="reservData" cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td align="center" style="width:497; background:images/reservDotRule.gif">
                                    <img height="1" src="images/blank.gif" width="497" border="0" alt="" /></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="497">
                                    <table class="reservData" cellspacing="1" cellpadding="0" width="100%" bgcolor="#efefef"
                                        border="0">
                                        <tr>
                                            <td class="reservDataLabel" align="center" style="width:120; background-color:#ff9900; height:17">                                               Contract Information</td>
                                            <td align="right" style="width:374; background:images/reservBarBkgd.gif">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width:497; background:images/reservDotRule.gif">
                                    <img height="1" src="images/blank.gif" width="497" border="0" alt="" /></td>
                            </tr>
                        </table>
                        <img height="6" src="images/blank.gif" width="497" border="0"><br />
                        <table cellspacing="1" cellpadding="3" width="100%" bgcolor="#cccccc" border="0">
                            <asp:Repeater ID="rptContracts" runat="server">
                                <HeaderTemplate>
                                    <tr>
                                        <td class="resortDataw" align="center" style="width:16; background-color:#333333">
                                            No.</td>
                                        <td class="resortDataw" align="center" style="width:96; background-color:#333333">
                                            Contract No.</td>
                                        <td class="resortDataw" align="center" style="width:374; background-color:#333333">
                                            Description</td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="resortData" align="center" style="width:16; background-color:#ffffff">
                                            <%# DataBinder.Eval(Container, "DataItem").PRORDER %>
                                        </td>
                                        <td class="resortData" align="center" style="width:96; background-color:#ffffff">
                                            <%# DataBinder.Eval(Container, "DataItem").ACCT %>
                                        </td>
                                        <td class="resortData" align="center" style="width:374; background-color:#ffffff">
                                            <%# DataBinder.Eval(Container, "DataItem").PRONAME %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <!-- end vacation contract info -->
                        <br />

  </td>
                </tr>
            </tbody>
        </table>
        <center>
        </center>
        </td>
        <td valign="top" style="width:50%">
            <img height="10" src="images/blank.gif" width="10" border="0" alt="" /></td>
        </tr> </TBODY> </table>
    </form>
    <includeControlFooter:footer id="footer" runat="server">
    </includeControlFooter:footer>
    
    
</body>
</html>
