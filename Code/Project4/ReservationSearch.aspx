<%@ Page Language="VB" AutoEventWireup="false"
    Inherits="VSSA.ReservationSearch" Codebehind="ReservationSearch.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reservation Search</title>
      <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="Styles/vssa.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AjaxCalendar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CloseWindow() {
            window.close();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 615px;
        }
        .btnVSSA {
        color: rgba(41, 38, 40, 1);
        FONT-SIZE: 9px; 
        FONT-FAMILY: Verdana; 
        font-weight:500;
        background-color:rgba(253, 254, 252, 1);
        border: 1px solid rgba(252, 169, 28, 1);
        padding: 4px 4px 3px 3px;
        position: relative;
        margin: 0 auto;
        box-shadow:   
        inset 0 0 0 1px #fff,    
        inset 0 0 0 2px rgba(151, 153, 155, 1);
        cursor:pointer;
        text-decoration:none;
    }
    </style>
</head>
<body>
    <form id="frmSearch" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <!-- BEGIN HEADER -->
    <table style="margin-left: auto; margin-right: auto;" id="" cellspacing="0" cellpadding="0"
        width="" border="0">
        <tr>
            <td align="center" valign="top" width="214">
                <img height="30" alt="" src="images/topBarGreyLinesShadow.gif" width="214" border="0" />
            </td>
            <td align="center" valign="top" width="176">
                <img height="30" alt="" src="images/topBrGryLinesShdowBckrnd.gif" width="176" border="0" />
            </td>
            <td align="center" valign="top" width="199">
                <img height="30" alt="" src="images/topBrGryDots.gif" width="199" border="0" />
            </td>
            <td align="center" valign="top" width="171">
                <img height="30" alt="" src="images/topBrGrnDots.gif" width="168" border="0" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px;">
            </td>
        </tr>
    </table>
    <!--END HEADER -->
    <!--Begin Body-->
    <asp:Panel ID="pnlSearch" Width="100%" Height="300" runat="server">
        <table id="tblSearchPanel" cellpadding="0" cellspacing="0" style="margin-left: auto;
            margin-right: auto;">
            <tr>
                <td id="tblMainColMarginLeft" style="width: 140px;">
                </td>
                <!--BEGIN Search Panel -->
                <td valign="top" background="images/reservBoxBkgd2.gif" height="300" style="width: 495px">
                    <table id="tblInnerControlContainer" cellspacing="0" cellpadding="0">
                        <tr id="rowError" runat="server" visible="false">
                            <td>
                            </td>
                            <td valign="top" align="right">
                                <asp:Image ID="imgError" runat="server" ImageUrl="images/alert.gif" />
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Label ID="lstErrors" runat="server" CssClass="text" ForeColor="red"></asp:Label>
                            </td>
                        </tr>
                        <tr id="tblInnerMarginTop">
                            <td id="tblInnerMarginLeft1" style="width: 100px;">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td align="center" style="height: 50px;">
                                <br />
                                <asp:Label Font-Size="Small" ID="lblTitle" runat="server" CssClass="Font10pxBOLD"
                                    Text="Reservation Confirmation"></asp:Label>
                            </td>
                            <caption>
                                <br />
                            </caption>
                        </tr>
                        <tr>
                            <td id="tblInnerMarginLeft2" style="width: 100px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="top">
                                <asp:Label CssClass="text" ID="lblArvact" Text="Arvact:" runat="server"></asp:Label>
                                &nbsp;
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtArvact" CssClass="textBlack" runat="server" Width="160"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td3" style="width: 100px; height: 22px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="top" style="height: 22px">
                                <asp:Label ID="lblFirstName" runat="server" CssClass="text" Text="First Name:" ></asp:Label>
                                &nbsp;
                            </td>
                            <td valign="top" style="height: 22px">
                                <asp:TextBox CssClass="textBlack" ID="txtFirstName" runat="server" Width="160" ReadOnly="true" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="tblInnerMarginLeft3" style="width: 100px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="top">
                                <asp:Label ID="lblLastName" runat="server" CssClass="text" Text="Last Name:"></asp:Label>
                                &nbsp;
                            </td>
                            <td valign="top">
                                <asp:TextBox CssClass="textBlack" ID="txtLastName" runat="server" Width="160" ReadOnly="true" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="tblInnerMarginLeft4" style="width: 100px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="top">
                                <asp:Label ID="lblReservationNumber" runat="server" CssClass="text" Text="Reservation No:"></asp:Label>
                                &nbsp;
                            </td>
                            <td valign="top">
                                <asp:TextBox Width="160" CssClass="textBlack" ID="txtReservationNumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="tblInnerMarginLeft5" style="width: 100px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="top">
                                <asp:Label ID="lblFrom" runat="server" CssClass="text" Text="From:"></asp:Label>
                                &nbsp;
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtstartdate" runat="server" SkinID="textbox1"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="txtstartdate"
                                    CssClass="ajax__calendar" EnabledOnClient="true" Format="MM/dd/yyyy" />
                            </td>
                        </tr>
                        <tr>
                            <td id="tblInnerMarginLeft6" style="width: 100px;">
                                &nbsp;
                            </td>
                            <td align="right" valign="top">
                                <asp:Label ID="lblTo" runat="server" CssClass="text" Text="To:"></asp:Label>
                                &nbsp;
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtenddate" runat="server" SkinID="textbox1"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtenddate"
                                    CssClass="ajax__calendar" EnabledOnClient="true" Format="MM/dd/yyyy" />
                            </td>
                        </tr>
                        <tr>
                            <td id="tblInnerMarginLeft7" style="width: 100px; height: 39px;">
                                &nbsp;
                            </td>
                            <td valign="top" style="height: 39px">
                                &nbsp;
                            </td>
                            <td align="center" style="height: 39px">
                                <br />
                                <asp:ImageButton AlternateText="Search owner reservations" ID="btnSubmit" runat="server"
                                    ImageUrl="images/search_btn.gif" />&nbsp;
                               
                            </td>
                        </tr>
                    </table>
                </td>
                <td id="tblMaincolmarginRight" style="width: 140px;">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!--End Search Panel-->
    <!-- BEGIN REPEATER -->
    <table id="tblRepeater" style="margin-left: auto; margin-right: auto;">
        <tr id="rowAS400Validation" runat="server" visible="false">
            <td>
            </td>
            <td valign="top" align="right">
                <asp:Image ID="imgAS400validate" runat="server" ImageUrl="images/alert.gif" />
            </td>
            <td align="left" valign="bottom" class="style1">
                <asp:Label ID="lblAS400Validate" Font-Size="Medium" Font-Bold="true" runat="server"
                    CssClass="text" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr id="rowCloseWindow" runat="server" visible="false">
            <td colspan="3" align="right" valign="top">
                <a href="#" style="color: Blue;" id="lnkClosePage" onclick="CloseWindow()" class="Font10pxWhtB">
                    X Close</a>
                <br />
            </td>
        </tr>
        <tr id="rowGoHome" runat="server" visible="false">
            <td colspan="3" align="right" valign="top">                               
                <asp:Button ID="bntBackTop" CssClass="btnVSSA" runat="server" Text="BACK" />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Repeater ID="rptReservations" runat="server">
                    <HeaderTemplate>
                        <table border="1" style="border-width: thin;" cellpadding="0" visible="true" cellspacing="0"
                            width="695px">
                            <tr>
                                <td colspan="9" align="left" style="background-color: Black;">
                                    <asp:Label ID="lblRepeaterTitle" ForeColor="white" runat="server" CssClass="Font10pxBOLD"
                                        Text="Email or print reservation"></asp:Label>
                                </td>
                            </tr>
                            <tr class="orange">
                                <td>
                                    <span class="textBlack">Confirmation #</span>
                                </td>
                                <td>
                                    <span class="textBlack">Resort Name</span>
                                </td>
                                <td>
                                    <span class="textBlack">Check In Date</span>
                                </td>
                                <td>
                                    <span class="textBlack">Check Out Date</span>
                                </td>
                                <td>
                                    <span class="textBlack">Reservation Type</span>
                                </td>
                                <td>
                                    <span class="textBlack">Exchange Deposit</span>
                                </td>
                                <td>
                                    <span class="textBlack">Email Address</span>
                                </td>
                                <td>
                                    <span class="textBlack">Send Email Confirmation</span>
                                </td>
                                <td>
                                    <span class="textBlack">Print Confirmation</span>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblConfirmationNumber" Text="N/A" runat="server" CssClass="text"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblResortName" runat="server" Text="N/A" CssClass="text"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCheckinDate" runat="server" Text="N/A" CssClass="text"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCheckoutDate" runat="server" CssClass="text" Text="N/A"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblResortType" runat="server" CssClass="text" Text="N/A"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox Enabled="false" ID="chkExchange" runat="server"></asp:CheckBox>
                            </td>
                            <td>
                                <asp:Label ID="lblEmailAddress" runat="server" CssClass="text" Text="N/A"></asp:Label>
                                &nbsp;
                                <asp:LinkButton ID="lnkExpand" CommandName="expand" ForeColor="blue" runat="server"
                                    CssClass="text" ToolTip="Expand" Text="+"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkSendEmail" CommandName="sendEmail" runat="server" CssClass="Font10pxWhtB"
                                    ForeColor="blue" Text="SEND"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkPrintEmail" CommandName="printConfirmation" runat="server"
                                    CssClass="Font10pxWhtB" ForeColor="blue" Text="Print"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="rowChild" runat="server" visible="false">
                            <td colspan="9" style="background-color: #FFCC66">
                                <span class="text">One time use Email </span>
                                <asp:TextBox ID="txtUpdateEmail" runat="server" CssClass="text"></asp:TextBox>
                                <asp:LinkButton ID="LnkUpdateEmail" runat="server" CommandName="updateEmail" CssClass="Font10pxWhtB"
                                    ForeColor="blue" Text="Save"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="orange">
                            <td>
                                <asp:Label ID="lblConfirmationNumber" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblResortName" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCheckinDate" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCheckoutDate" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblResortType" runat="server" CssClass="textBlack" Text="N/A"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkExchange" Enabled="false" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblEmailAddress" runat="server" CssClass="text" Text="N/A"></asp:Label>
                                &nbsp;
                                <asp:LinkButton ID="lnkExpand" CommandName="expand" ForeColor="blue" runat="server"
                                    CssClass="text" ToolTip="Expand" Text="+"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkSendEmail" CommandName="sendEmail" runat="server" CssClass="Font10pxWhtB"
                                    ForeColor="blue" Text="SEND"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkPrintEmail" CommandName="printConfirmation" runat="server"
                                    CssClass="Font10pxWhtB" ForeColor="blue" Text="Print"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="rowChild" runat="server" visible="false">
                            <td colspan="9" style="background-color: #FFCC66">
                                <span class="text">One time use Email </span>
                                <asp:TextBox ID="txtUpdateEmail" runat="server" CssClass="text"></asp:TextBox>
                                <asp:LinkButton ID="LnkUpdateEmail" runat="server" CssClass="Font10pxWhtB" ForeColor="blue"
                                    Text="Save" CommandName="updateEmail"></asp:LinkButton>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
    </table>
    <!--END REPEATER-->
    <!--END Body-->
    </form>
</body>
</html>
