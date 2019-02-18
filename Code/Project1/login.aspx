<%@ Page Language="vb" AutoEventWireup="false" Inherits="BGO.BluegreenOnline.LoginChoice" Codebehind="login.aspx.vb" %>

<%@ Register TagPrefix="includeControlFooter" TagName="footer" Src="includes/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Choose Account</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link rev="stylesheet" href="main.css" rel="stylesheet" />
    <script type="text/javascript" src="scripts/rollover.js"></script>
    <link rel="stylesheet" rev="stylesheet" href="Owner/owner.css" />
</head>
<body>
    <table align="center" cellspacing="0" cellpadding="0" width="740" border="0">
        <form id="Form1" method="post" runat="server">
        <tr>
            <td valign="top" width="50%" style="background-image: url(images/enrollBkgd2_lft.gif)">
                <img height="10" src="images/blank.gif" width="10" border="0" alt="" />
            </td>
        </tr>
        <tr>
            <td valign="top" width="740">
                <!-- header bar -->
                <table cellspacing="0" cellpadding="0" width="740" border="0">
                    <tr>
                        <td valign="top" width="740">
                            <img height="17" alt="" src="images/headerBar1.gif" width="740" border="0"/>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" bgcolor="#666666" colspan="2">
                            <img height="1" src="images/blank.gif" width="100%" border="0" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" background="images/headerBkgd1.gif" colspan="2">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td width="271">
                                        <a href="default.aspx">
                                            <img src="images/ResortsLogo.gif" alt="Bluegreen Resorts" width="271" height="20"  />
                                        </a>
                                    </td>
                                    <td width="371">
                                        <img height="63" src="images/blank.gif" width="331" border="0" alt="" />
                                    </td>
                                    <td width="240">
                                        <%--<a onmouseover="MM_swapImage('hdrTag_','','images/hdrTag_o.gif',1)" onmouseout="MM_swapImgRestore()"
                                            href="#">--%>
                                            <img alt="Phone us at 800.456.CLUB (2582)" src="images/phone.gif"
                                                border="0" name="hdrTag_" alt="" />
                                        </a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- end header bar -->
                <table cellspacing="0" cellpadding="0" width="740" border="0">
                    <tr>
                        <td colspan="3">
                            <table cellspacing="0" cellpadding="0" width="740" border="0">
                                <tr>
                                    <td width="736" bgcolor="#666666">
                                        <img height="19" alt="" src="images/blank.gif" width="736" border="0" alt="" />
                                    </td>
                                    <td width="4" bgcolor="#ff9900">
                                        <img height="19" alt="" src="images/blank.gif" width="4" border="0" alt="" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="193">
                            <table class="text" cellspacing="0" cellpadding="0" width="193" bgcolor="#efefef"
                                border="0">
                                <tr>
                                    <td colspan="3">
                                        <img height="59" src="images/enroll_img.gif" width="193" border="0" alt="" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10">
                                        <img height="10" src="images/blank.gif" width="10" border="0" alt="" />
                                        <br />
                                    </td>
                                    <td width="173">
                                        <img height="10" src="images/blank.gif" width="173" border="0" alt="" />
                                        <br />
                                    </td>
                                    <td width="10">
                                        <img height="10" src="images/blank.gif" width="10" border="0" alt="" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10">
                                        <img height="18" src="images/blank.gif" width="10" border="0" alt="" />
                                        <br />
                                    </td>
                                    <td valign="top" width="173">
                                        <img height="24" src="images/yourVacaMemb_title.gif" width="163" border="0" alt="" />
                                        <br />
                                        Your vacation&nbsp;ownership includes online account access at no additional charge.
                                        Please choose which account you would like to manage.
                                    </td>
                                    <td width="10">
                                        <img height="18" src="images/blank.gif" width="10" border="0" alt="" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <img height="15" src="images/blank.gif" width="193" border="0" alt="" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background-image: url(images/shadow.gif)" colspan="3">
                                        <img height="30" src="images/blank.gif" width="193" border="0" alt="" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                        <!-- divider -->
                        <td valign="top" width="50" bgcolor="#ffffff">
                            <img height="67" src="images/dividerDots4.gif" width="50" border="0" alt="" />
                            <br />
                        </td>
                        <!-- end divider -->
                        <!-- enroll content -->
                        <td valign="top" width="497">
                            <p>
                                <img height="10" src="images/blank.gif" width="497" border="0" alt="" />
                                <br />
                                <img height="39" src="images/selectAccount.gif" width="340" border="0" alt="" />
                                <br />
                            </p>
                            <p>
                                <br />
                                We’ve noticed you have more than one type of Bluegreen Resorts ownership account.
                                Please select below the account you would like to access first. In order to access
                                your other account, you will need to sign out and log back in.
                                <br />
                                <br />
                                Select the account you would like to access first:
                                <asp:RadioButtonList ID="rdoAccountList" Width="456px" AutoPostBack="True" runat="server"
                                    Height="120px">
                                </asp:RadioButtonList>
                            </p>
                            <!-- personal information -->
                        </td>
                    </tr>
                </table>
    </td> </tr>
    <tr>
        <td colspan="4">
            <!-- footer -->
            <includeControlFooter:footer ID="footer" runat="server"></includeControlFooter:footer>
            <!-- end footer -->
        </td>
    </tr>
    </form> </table>
</body>
</html>
