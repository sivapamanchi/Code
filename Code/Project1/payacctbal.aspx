<%@ Page Language="VB" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.payAcctBal" Codebehind="PayAcctBal.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="~/includes/ucmenu.ascx" %>
<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="~/includes/footer.ascx" %>
<%@ Register TagPrefix="includeOwnerPalette" TagName="OwnerPalette" Src="~/includes/OwnerPalette.ascx" %>
<%@ Register TagPrefix="includeFeaturedResorts" TagName="NewResorts" Src="~/includes/FeaturedResorts.ascx" %>
<%@ Register TagPrefix="includeControlDynamicSpecials" TagName="DynamicSpecials" Src="~/includes/dynamicSpecials.ascx" %>
<%@ Register TagPrefix="includeMOLNav" TagName="MOLNav" Src="~/includes/mortgageonlinenav.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" 
        "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Payment Balances</title>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="EXPIRES" content="0" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" rev="stylesheet" href="/css/owner.css" />
    <link href="/css/ucmenu.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" rev="stylesheet" href="/css/ownerPalette.css" />
    <!-- JAVASCRIPT FILE(S) -->
    <script src="<%= jQuery %>" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jQscripts.js"></script>
    <script type="text/javascript">
        function NavigateToTheLink(quicklink) {

            //    alert(quicklink);
            window.location = quicklink;

        }
    </script>
    


</head>
<body style="margin: 0px">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" align="center" width="100%">
                <uc1:ucMenu ID="UcMenu1" runat="server"></uc1:ucMenu>
            </td>
        </tr>
    </table>
    <table align="center" cellspacing="0" cellpadding="0" width="740" border="0">
        <tr>
             <td valign="top" width="183">
                 <table align="left" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <!-- owners palette -->
                        <includeOwnerPalette:OwnerPalette ID="OwnerPalette" runat="server"></includeOwnerPalette:OwnerPalette>
                        <!-- end owners palette -->
                        <!-- dynamic palette -->
                    </tr>
                    <tr class="text">
                        <!-- mortgage menu -->
                        <includeMOLNav:MOLNav ID="MOLNav" runat="server" />
                        <!-- end mortgage menu -->
                    </tr>
                    <tr>
                        <td bgcolor="#ffffff">
                            <img height="20" src="images/blank.gif" width="183" border="0">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="183">
                            <!-- top 3 resorts subnav -->
                            <includeFeaturedResorts:NewResorts ID="NewResorts" runat="server"></includeFeaturedResorts:NewResorts>
                            <!-- end top 3 resorts subnav -->
                        </td>
                    </tr>
                    <!-- end dynamic palette -->
                    <tr>
                        <td bgcolor="#ffffff">
                            <!-- dynamic specials -->
                            <includeControlDynamicSpecials:DynamicSpecials ID="DynamicSpecials1" runat="server">
                            </includeControlDynamicSpecials:DynamicSpecials>
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
                                <table id="page-title" class="bcrumb" cellspacing="0" cellpadding="0" width="497"
                                    border="0">
                                    <tr>
                                        <td valign="top">
                                            <a class="bcrumblink" href='<%= Session("UnsecuredURL") %>default.aspx'>Bluegreen</a>
                                            / <a class="bcrumblink" href="home.aspx">Owners</a> / <a class="bcrumblink" href="payAcctBal.aspx">
                                                Payments</a> / <a class="bcrumblink" href="payAcctBal.aspx">Maintenance Fees & Club
                                                    Dues</a> / Make a Payment
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <h2 id="makePayment">
                                                Make a Payment</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <br>
                                            <!-- <img src="images/pg-title-maint-vegas.gif" width="497" height="42"> -->
                                        </td>
                                    </tr>
                                </table>
                                <table border="0">
                                    <asp:Panel ID="PanelError" runat="server" Visible="false">
                                        <tr>
                                            <td width="10%">
                                                <img src="images/alert.gif" alt="" />
                                            </td>
                                            <td class="op_label">
                                                <br />
                                                <p class="textRed">
                                                    This section of our site is currently down for maintenance. We apologize for the
                                                    inconvenience.
                                                    <br />
                                                    <br />
                                                </p>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDoesNotQualify" runat="server" Visible="false">
                                        <tr>
                                            <td width="10%">
                                                <img src="images/alert.gif" alt="" />
                                            </td>
                                            <td class="op_label">
                                                <br />
                                                <p class="textRed">
                                                    We apologize for the inconvenience, but your account does not qualify for online
                                                    payments at this time.<br />
                                                    <br />
                                                    Please contact us at 800.456.CLUB (2582) to learn how to enable your account.<br />
                                                    <br />
                                                </p>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPaidinFull" runat="server" Visible="false">
                                        <tr>
                                            <td width="10%">
                                            </td>
                                            <td class="op_label">
                                                <span class="text">Your account is currently paid in full. When your payments come due,
                                                    you will be able to pay them right here online, at your convenience, from any computer.
                                                    You will be able to pay with a major credit card or from your checking or savings
                                                    account.</span> <br /><br />
                                                   
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                
                                                
                                      
                                    <asp:Panel ID="pnlInstallment" runat="server" Visible="false">
                                        <tr>
                                            <td width="10%">
                                            </td>
                                            <td class="op_label">
                                                <span class="text">
                                                    <asp:Literal ID="ltlInstallment" runat="server"></asp:Literal></span>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPayments" runat="server" Visible="false">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <p class="text">
                                                    Enter the payment(s) you would like to make and choose your payment method. You
                                                    will need to repeat this process for each association you are paying. Please note:
                                                    <strong>Late charges may apply to any balance not paid in full by the due date.</strong><br>
                                                    <br>
                                                    View <a href="faq.aspx?C=37" target="_blank" style="text-decoration: underline" class="textlink">
                                                        Maintenance Fee</a>
                                                    <%
                                                        If Session("OwnerContractType") = "Vacation Club" Then
                                                    %>
                                                    and <a href="faq.aspx?C=35" target="_blank" style="text-decoration: underline" class="textlink">
                                                        Club Dues Frequently Asked Questions</a></p>
                                                <% End If%>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlPending" runat="server" Visible="false">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <br />
                                                <p class="text">
                                                    Your account is currently pending processing of payments. Please allow several business
                                                    days for your payment to post to your account. We thank you for your patience.
                                                    <br />
                                                    <br />
                                                    <br />
                                                    If you still have outstanding payments you would like to make in the meantime, you
                                                    can call 800.456.CLUB(2582) or mail payment to:
                                                    <br />
                                                    <br />
                                                    Bluegreen Resorts Management<br />
                                                    P.O.Box 810758<br />
                                                    Boca Raton, FL 33481-0758<br />
                                                    <br />
                                                    Please use the preprinted remittance stub and return envelope sent with your statement,
                                                    if unavailable be sure to include your account number on your check.
                                                    <br />
                                                    <br />
                                                    If you have any questions please call 800.456.CLUB(2582).
                                                </p>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlErrorAccessing500" Visible="false">
                                        <tr>
                                            <td width="10%">
                                                <img src="images/alert.gif" alt="" />
                                            </td>
                                            <td>
                                                <br />
                                                <br />
                                                <p class="textRed">
                                                    We apologize for the inconvenience, but we are currently experiencing technical
                                                    difficulties.<br />
                                                    <br />
                                                    <br />
                                                    Please call 800.456.CLUB(2582) for assistance with your account.</p>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                                <form id="frmAcct50" runat="server">
                                 <center><asp:Button ID="btnPrepayment" runat="server" Text="Make a Pre-Payment" Font-Size="11px"  Visible="false"  align="center"/></center>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="0px" />
                                <br />
                                <asp:Label ID="lblErrors" runat="server" Visible="false" />
                                <asp:GridView ID="gvOUTER" runat="server" AutoGenerateColumns="False" 
                                    Width="500px" ShowHeader="False" RowStyle-Height="0%"
                                    RowStyle-BorderStyle="solid" RowStyle-BorderColor="green" SelectedRowStyle-Height="0%"
                                    HeaderStyle-Height="0%" Height="0%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <table id="headertable" runat="server" width="100%" cellpadding="0" cellspacing="0">
                                                    <tr style="background-color: #ff9900;">
                                                        <td>
                                                            <asp:Label runat="server" ID="lblPRONAME" Font-Bold="true" Font-Underline="True"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hfPROJNUM" />
                                                        </td>
                                                        <td align="right">
                                                            <asp:HyperLink runat="server" ID="viewst" Font-Bold="true" ForeColor="black" Font-Underline="true" Visible="false"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:GridView BorderStyle="solid" BorderColor="white" BorderWidth="1" ID="gvDETAIL"
                                                    runat="server" AutoGenerateColumns="False" ShowFooter="true" GridLines="both"
                                                    AlternatingRowStyle-BackColor="#cfcfcf" RowStyle-BackColor="#efefef">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Acct. No." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                                            HeaderStyle-BackColor="#333333" HeaderStyle-CssClass="resortDataw">
                                                            <ItemTemplate>
                                                                <asp:HyperLink runat="server" ID="hlAcctNo" CssClass="resortData" Text='<%# Container.DataItem("ACCT") %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PASTDUE" DataFormatString="{0:c}" HtmlEncode="False" HeaderText="Past Due">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle BackColor="#333333" CssClass="resortDataw" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BALCU" DataFormatString="{0:c}" HtmlEncode="False" HeaderText="Current">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle BackColor="#333333" CssClass="resortDataw" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAYAMOUNT" HtmlEncode="False" HeaderText="Payamount" Visible="False" />
                                                        <asp:TemplateField HeaderText="ARDA*">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hfARDA" Value='<%# Container.DataItem("ARDAFL") %>' />
                                                                <asp:HiddenField runat="server" ID="hfARDAMT1" Value='<%# Container.DataItem("ARDAMT1") %>' />
                                                                <asp:HiddenField runat="server" ID="hfColStat" Value='<%# Container.DataItem("COLSTAS") %>' />
                                                                <asp:HiddenField runat="server" ID="hfARSLTY" Value='<%# Container.DataItem("ARSLTY") %>' />
                                                                <asp:CheckBox runat="server" ID="cbARDA" AutoPostBack="True" OnCheckedChanged="cbARDA_CheckedChanged" CausesValidation="true" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle BackColor="#333333" CssClass="resortDataw" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Amount">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="MerchID" Value='<%# Container.DAtaItem("mrchid") %>' />
                                                                <asp:HiddenField ID="hfACCT" runat="server" Value='<%# Container.DataItem("ACCT") %>' />
                                                                <asp:HiddenField ID="hfProjNum" runat="server" Value='<%# Container.DataItem("ProjNum") %>' />
                                                                <asp:HiddenField ID="hfProjName" runat="server" Value='<%# Container.DataItem("PRONAME") %>' />
                                                                <asp:HiddenField ID="hfPayment" runat="server" Value='<%# Container.DataItem("PayAmount") %>' />
                                                                <asp:HiddenField ID="hfBilledAmount" runat="server" Value="<%# totalDue %>" />
                                                                $ <asp:TextBox ID="PayAmount" runat="server" Text='<%# String.Format("{0:n}",Container.DataItem("PayAmount")) %>'
                                                                    AutoPostBack="True" CausesValidation="True"  OnTextChanged="PayAmount_TextChanged" onchange="javascript:checkAmt();"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ID="fv1" ControlToValidate="PayAmount" EnableClientScript="true"
                                                                    ErrorMessage="<font color=red>Please enter a payment amount. If none enter 0.</font>"
                                                                    Text="*" />
                                                               <%-- <asp:CustomValidator runat="server" ID="cv1" ControlToValidate="PayAmount" ErrorMessage="<font color=red>The pay amount cannot be zero.</font>"
                                                                    Text="*" OnServerValidate="CheckPaymentNotZero" />
                                                                <asp:CustomValidator runat="server" ID="cv2" ControlToValidate="PayAmount" Text="*"
                                                                    OnServerValidate="CheckPaymentNotOver" />--%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle BackColor="#333333" CssClass="resortDataw" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle BackColor="#EFEFEF" />
                                                    <AlternatingRowStyle BackColor="#CFCFCF" />
                                                </asp:GridView>
                                                <asp:Panel ID="pnlPaymentTotal" runat="server" Wrap="false" Visible="true">
                                                    <table border="0" width="100%">
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <asp:Label runat="server" ID="lblTotalDue" Font-Bold="true" Text="Total Payment:" />
                                                                <asp:TextBox runat="server" ID="TotalDue" Text='<%# String.Format("{0:c}",TotalDue) %>'
                                                                    BorderStyle="None" BorderWidth="0px" ReadOnly="True"></asp:TextBox>
                                                                </strong><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label runat="server" ID="lblPaymentMethod" Text="Payment Method: " Font-Bold="true" />
                                                            </td>
                                                            <td align="center">
                                                                <asp:RadioButtonList ID="rblPAYMENTMETHOD" runat="server" RepeatDirection="Horizontal"  CausesValidation="true">
                                                                    <asp:ListItem Value="in">Installments<a class="textlink" onclick="newWin=window.open('helpWinInstall.aspx','newWin1','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=230,height=450,left=50,top=50'); return false;" href="#" style="text-decoration:underline"><img src="/includes/images/help_icon.gif" width="10" height="10" border="0"></a></asp:ListItem>
                                                                    <asp:ListItem Value="cs">Checking/Savings</asp:ListItem>
                                                                    <asp:ListItem Value="cc">Credit/Debit Card</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RadioButtonList ID="rblPAYMENTMETHOD2" runat="server" RepeatDirection="Horizontal"
                                                                    Visible="false">
                                                                    <asp:ListItem Value="cc">Credit Card</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="submitPayment" ImageUrl="./images/BtnPayNow.gif"
                                                                    CausesValidation="true" CommandName="getAccount" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="left">
                                                                <asp:HyperLink runat="server" Target="_blank" ID="hlAcctMsg" Font-Size="Small" Font-Underline="true"
                                                                    CssClass="textlink" NavigateUrl="~/Owner/projectY_Z.aspx" Text="Please click here for an explanation of your current fees" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </ItemTemplate>
                                            <ControlStyle Width="500px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <table style="width: 100%">
                                    <tr style="background-image: url(./images/dottedRule_w.gif); background-repeat: repeat-x;">
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel runat="server" ID="noOUterROws" Visible="false">
                                    <table>
                                        <tr>
                                            <td width="10%">
                                                <img src="images/alert.gif" alt="" />
                                            </td>
                                            <td>
                                                <br />
                                                <br />
                                                <p class="textRed">
                                                    We apologize for the inconvenience, but we are currently experiencing technical
                                                    difficulties.
                                                    <br />
                                                    <br />
                                                    <br />
                                                    Please call 800.456.CLUB(2582) for assistance with your account.</p>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlPayInfo" runat="server" Visible="false">
                                    <% If Session("ProjNum") = "50" Then%>
                                    <%Else%>
                                    <table>
                                        <tr>
                                            <td>
                                                <p class="text">
                                                    You have three ways to pay:</p>
                                                <ol>
                                                    <li>Online: Pay from your credit card.</li>
                                                    <li>By phone: 800.456.CLUB (2582), option 2.</li>
                                                    <li>By mail: Use the preprinted remittance stub and return envelope sent with your statement.
                                                        If unavailable, mail payments to the address below. Please be sure to include your
                                                        account number.</li>
                                                </ol>
                                                Bluegreen Resorts Management<br />
                                                Attn: Financial Services AR Dept.<br />
                                                P.O. Box 810758<br />
                                                Boca Raton, FL 33481-0758<br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <% End If%>
                                </asp:Panel>
                                <asp:Panel ID="pnlARDA" runat="server" Visible="False">
                                    <table>
                                        <tr>
                                            <td>
                                                <br />
                                                <% If Session("ProjNum") = "50" Then%>
                                                Don't want to pay online? <a class="textlink" href="#" onclick="newWin=window.open('helpWinpaymethod.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=230,height=410,left=100,top=10'); return false;">
                                                    View other options</a>.
                                                <% End If%>
                                                <br>
                                                <br>
                                                *ARDA contributions ($5 per account) are completely voluntary. If you do not wish
                                                to contribute, or only wish to contribute towards a single account, please uncheck
                                                the necessary box(es) in the ARDA column. <a class="textlink" href="#" onclick="newWin=window.open('helpwinarda.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=230,height=450,left=100,top=10'); return false;">
                                                    Click here</a> to learn more about ARDA.<br />
                                            </td>
                                        </tr>
                                        <asp:Panel ID="vegasPromo" runat="server" Visible="false">
                                            <!-- START VEGAS PROMO --->
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td colspan="2" valign="top">
                                                                <img src="images/d_rule.gif" width="497" height="15">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <p class="text">
                                                                    <img src="images/maint.gif" width="354" height="60"><br>
                                                                    <br>
                                                                    When you pay your Maintenance Fee online, on time and in full**,<br>
                                                                    you&rsquo;ll be automatically entered in our prize drawing.<br>
                                                                    <a class="textlink" href="#" onclick="newWin=window.open('vegasDOP.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=650,height=450,left=100,top=10'); return false;">
                                                                        Click here</a> for details of participation.</p>
                                                            </td>
                                                            <td width="152" valign="top">
                                                                <img src="images/maint-02.gif" width="152" height="117">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" valign="top">
                                                                <img src="images/d_rule.gif" width="497" height="15">
                                                                <span class="foottext">**Less voluntary ARDA contributions you may opt not to pay ($5
                                                                    per account). All past due balances (including Club Dues) and current Maintenance
                                                                    Fee balances must be paid in full. Any payments made past the due date will not
                                                                    be eligible for automatic entry into the prize drawing.</span>
                                                                <p class="foottext">
                                                                    ***Bluegreen Club 36 is not available for sale and is not part of the Bluegreen
                                                                    Vacation Club. Availability for sale and entry into the Bluegreen Vacation Club
                                                                    is contingent upon meeting applicable registration and/or licensing requirements
                                                                    which may or may not occur. This property and the layout is subject to change. This
                                                                    resort is under construction and, if delivered, is anticipated to be completed in
                                                                    the summer of 2008.</p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <!-- END VEGAS PROMO --->
                                        </asp:Panel>
                                    </table>
                                </asp:Panel>
                                </form>
                                <!-- end page content -->
                                <!-- footer -->
                                <includeControlFooter:footer ID="footer" runat="server"></includeControlFooter:footer>
                                <!-- end footer -->
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
<!--Start AliveChat Button Code-->
 <div style="z-index: 100000; border-bottom: red 0px solid; position: fixed; border-left: red 0px solid;
        width: 209px; bottom: 0px; height: 85px; border-top: red 0px solid; cursor: pointer;
        right: 0px; border-right: red 0px solid" id="Div1" class="wsa_box">
       <img src='https://a1.websitealive.com/5980/Visitor/vButton_v3.asp?groupid=5980&departmentid=5575&w=400&h=400&icon_online=https%3A%2F%2Fimages%2Ewebsitealive%2Ecom%2Fimages%2Fhosted%2Fupload%2F52043%2EGIF&icon_offline=https%3A%2F%2F'
        border='0' onclick="window.open('https://a1.websitealive.com/5980/rRouter.asp?groupid=5980&websiteid=0&departmentid=5575&dl='+escape(document.location.href),'','width=400,height=400');"
        style='cursor: pointer;'>
       
    </div>
<!--End AliveChat Button Code-->
</body>
</html>
<script type="text/javascript">
    function checkAmt() {

        var errorLbl = document.getElementById('lblErrors');

        if (Page_IsValid)
        { }
        else {
            if (errorLbl != null) {
                errorLbl.style.display = 'none';
            }
        }
    } 
    </script>
