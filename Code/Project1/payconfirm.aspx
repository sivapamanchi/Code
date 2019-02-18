<%@ Reference Control="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="includeControlDynamicSpecials" TagName="DynamicSpecials"
    Src="~/includes/dynamicSpecials.ascx" %>
<%@ Register TagPrefix="includeVacNewsLetter" TagName="Signup" Src="~/includes/corpVacationNewsletter.ascx" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="~/includes/footer.ascx" %>
<%@ Register TagPrefix="includeFeaturedResorts" TagName="NewResorts" Src="~/includes/FeaturedResorts.ascx" %>
<%@ Register TagPrefix="includeOwnerPalette" TagName="OwnerPalette" Src="~/includes/OwnerPalette.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="~/includes/ucMenu.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.payConfirm" Codebehind="payConfirm.aspx.vb" %>

<%@ Register TagPrefix="includeMOLNav" TagName="MOLNav" Src="~/includes/mortgageonlinenav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
    <title>Payments: Payment Approved</title>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="EXPIRES" content="0" />
    <!-- wut <link rev="stylesheet" href="owner.css" rel="stylesheet" /> -->
    <link rel="stylesheet" rev="stylesheet" href="/css/owner.css" />
    <link href="/css/ucmenu.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" rev="stylesheet" href="/css/ownerPalette.css" />
    <!-- JAVASCRIPT FILE(S) -->
    <script type="text/javascript" language="javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="/js/jQscripts.js"></script>
    <script type="text/javascript">
        function NavigateToTheLink(quicklink) {

            //    alert(quicklink);
            window.location = quicklink;

        }
    </script>
</head>
<body leftmargin="0" topmargin="0">
    <uc1:ucMenu ID="UcMenu1" runat="server"></uc1:ucMenu>
    <table align="center" cellspacing="0" cellpadding="0" width="740" border="0">
        <tr>
            <td valign="top" width="183">
                <table align="left" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <!-- owners palette -->
                        <includeOwnerPalette:OwnerPalette ID="OwnerPalette" runat="server"></includeOwnerPalette:OwnerPalette>
                        <!-- end owners palette -->
                    </tr>
                    <tr>
                        <td valign="top" width="183">
                            <!-- top 3 resorts subnav -->
                            <includeFeaturedResorts:NewResorts ID="NewResorts" runat="server"></includeFeaturedResorts:NewResorts>
                            <!-- end top 3 resorts subnav -->
                        </td>
                        <td valign="top" width="10" bgcolor="#ffffff">
                            <img height="10" src="images/blank.gif" width="10" border="0" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#ffffff" colspan="2">
                            <img height="20" src="images/blank.gif" width="193" border="0" alt="" />
                        </td>
                    </tr>
                    <!-- end dynamic palette -->
                    <tr>
                        <td bgcolor="#ffffff" colspan="2">
                            <!-- dynamic specials -->
                            <includeControlDynamicSpecials:DynamicSpecials ID="DynamicSpecials1" runat="server"
                                Visible="false"></includeControlDynamicSpecials:DynamicSpecials>
                            <!-- end dynamic specials -->
                        </td>
                    </tr>
                </table>
                <!-- divider -->
            </td>
            <td valign="top" width="547">
                <table class="text" cellspacing="0" cellpadding="0" align="left" border="0">
                    <tbody>
                        <tr>
                            <td valign="top" width="49" bgcolor="#ffffff">
                                <img height="88" src="images/banners/dividerDots2.gif" width="49" border="0" alt="" />
                            </td>
                            <!-- end divider -->
                            <!-- top navigation -->
                            <td valign="top" width="497">
                                <!-- end top navigation -->
                                <!-- page content -->
                                <!-- page title -->
                                <img src="images/blank.gif" width="15" height="15" alt="" />
                                <table id="page-title" width="497" cellpadding="0" cellspacing="0" border="0" class="bcrumb">
                                    <tr>
                                        <td valign="top" style="height: 11px">
                                            <a class="bcrumblink" href='<%= Session("UnsecuredURL") %>default.aspx'>Bluegreen</a>
                                            / <a class="bcrumblink" href="home.aspx">Owners</a> /&nbsp; <a class="bcrumblink"
                                                href="payAcctBal.aspx">Payments</a> / Payment Approved
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="paymentsubmitted" valign="top" style="height: 35px">
                                        </td>
                                    </tr>
                                </table>
                                <!--
								<br />
                                <div id="last-chance-banner">
                                <a href="https://bluegreenowner.com/owner/FeatureDetail.aspx?ID=736" style="border:none">
                                <img src="images/last-chance-banner.jpg" style="margin:0 0 15px 0;" width="550" height="193" border="0" alt="Last Chance to Win" />
                                </a> 
                                </div>
								-->
                                <div runat="server" id="paperless" visible="true">
                                    <img src="/TravelerPlus/owner/images/div_rule.gif" width="493" height="10" alt="" />
                                   <img height="12" src="images/blank.gif" width="497" border="0" alt="" />
                                    <a href="ownerpaperless.aspx" style="border:none">
                                    <img src="/TravelerPlus/owner/images/gogreenOther.gif" alt="" style="border-color:White" />
                                    </a>
                                     <img height="12" src="images/blank.gif" width="497" border="0" alt="" />
                                </div>
                                <img src="/TravelerPlus/owner/images/div_rule.gif" width="493" height="10" alt="" />
                                <!-- end page title -->
                                <a href="javascript:self.print()">
                                    <h3 style="color: #cc0000">
                                        PLEASE PRINT THIS PAGE FOR YOUR RECORDS</h3>
                                </a>
                                <!-- IT, please kindly check the code for this number -->
                                <asp:Panel ID="pnlCredit" runat="server" Visible="False">
                                    <p>
                                        Thank you for your payment in the amount of <b>
                                            <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label></b>.
                                        Your authorization number is
                                        <asp:Label ID="lblConfirmation" runat="server" Font-Bold="True"></asp:Label>. It
                                        may take up to 3 business days for this payment to be reflected on your Bluegreen
                                        account. You will soon receive an email confirming this transaction.
                                    </p>
                                </asp:Panel>
                                <asp:Panel ID="pnlChecking" runat="server" Visible="false">
                                    <p>
                                        Thank you for your payment in the amount of <b>
                                            <asp:Label ID="achlblAmount" runat="server" Font-Bold="true"></asp:Label></b>.
                                        Your payment will be processed in the next 3-5 business days. <strong>Please note: You
                                            will not receive an email confirmation of this transaction. </strong>Be sure
                                        to check with your financial institution to confirm the funds were withdrawn from
                                        your account. Please remember to update your account register to avoid an overdraft
                                        on your account.
                                    </p>
                                </asp:Panel>
                                <table id="btmbuttons" width="100%">
                                    <tr>
                                        <td align="center">
                                         <a href='vacationProfile.aspx'>Update My Vacation Profile Now</a>
                                                <br />
                                              
                                                <center>or</center>
                                                
                                            <%
										        
                                                If Session("OwnerContractType") <> "Vacation Club" Then
                                            %>
                                            <a href="homeFixed.aspx">
                                                <img src="/TravelerPlus/owner/images/BtnReturnHome.gif" width="75" height="20" border="0" alt="" /></a>&nbsp;&nbsp;&nbsp;
                                            <%
                                            Else
                                            %>
                                            <a href="home.aspx">
                                                <img src="/TravelerPlus/owner/images/BtnReturnHome.gif" width="75" height="20" border="0" alt="" /></a>&nbsp;&nbsp;&nbsp;
                                            <%
                                            End If
										        
                                            %>
                                        </td>
                                    </tr>
                                </table>
                                <!--CRUISE OFFER STARTS HERE-->
                                <asp:Panel ID="pnlPromotion" runat="server" Visible="False">
                                    <p>
                                        <img src="/TravelerPlus/owner/images/title-congrats.gif" width="131" height="28" align="left" alt="" />
                                        </p>
                                    <br />
                                    <br />
                                    <p>
                                        If you are current and paid on time and in full, you will be automatically entered
                                        into our Las Vegas Sweepstakes for a chance to win one of 1,000 vacation stays.
                                        Drawing to be held by on or about February 25th, 2008. Winners will be notified
                                        by mail and email. Odds of winning depend on number of entries received. 1,000 First
                                        Prizes are each for a 4-day, 3-night stay at Bluegreen Club 36&trade;** in Las Vegas.
                                        Prize includes accommodations only and does not include airfare, transfers, taxes,
                                        meals or incidentals.</p>
                                </asp:Panel>
                                <asp:Panel ID="pnlPromotion_2" runat="server" Visible="False">
                                    <table width="497" height="367" border="0" cellpadding="15" cellspacing="0" background="images/Cruise_promo07.jpg">
                                        <tr>
                                            <td valign="top">
                                                <p>
                                                    <font color="#FFFFFF">
                                                        <img src="images/blank.gif" width="440" height="70"><br>
                                                        Upon activation, you will have one year to redeem your voucher towards the purchase
                                                        of a cruise booked through Ownership Rewards* &ndash; without having to use your
                                                        Vacation Points! This may be the only opportunity you will have to activate your
                                                        cruise voucher.* <a class="textlink" href="#" onclick="newWin=window.open('cruiseTC.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=650,height=450,left=100,top=10'); return false;">
                                                            <font color="#FFFFFF">Click here</font></a> for terms and conditions of this
                                                        offer. </font>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
                                                <p>
                                                    <br>
                                                </p>
                                            </td>
                                        </tr>
                                    </table>
                                    <p class="foottext">
                                        *International Cruise &amp; Excursion Gallery, Inc. (ICE), dba ICE, Ownership Rewards
                                        is independently owner and operated.
                                    </p>
                                    <p class="foottext">
                                        **Bluegreen Club 36 is not available for sale and is not part of the Bluegreen Vacation
                                        Club. Availability for sale and entry into the Bluegreen Vacation Club is contingent
                                        upon meeting applicable registration and/or licensing requirements which may or
                                        may not occur. This property and the layout is subject to change. This resort is
                                        under construction and, if delivered, is anticipated to be completed in the summer
                                        of 2008.</p>
                                </asp:Panel>
                                <p>
                                </p>
                                <!-- end page content -->
                                <!-- footer -->
                                <includeControlFooter:footer ID="footer" runat="server"></includeControlFooter:footer>
                                <!-- end footer -->
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
