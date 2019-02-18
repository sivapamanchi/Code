<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerProfileMain.aspx.vb" Inherits="VSSA.CustomerProfileMain" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/vssa.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        td {
            padding: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>


        <table style="width: 100%; margin: auto">
            <tr>
                <td style="width: 100%;">
                    <div style="width: 100%; font-size: 30px; text-align: center; padding-bottom: 15px; padding-top: 10px">Concierge Customer Info</div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <div style="width: 100%; font-size: 15px;">
                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table style="width: 80%;">
                        <tr>
                            <td style="width: 35%; vertical-align: top">
                                <table style="width: 100%; border: 1px solid;">
                                    <tr>
                                        <td>
                                            <div class="Font14pxBOLD">
                                                Incoming Call Information
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="Font12pxBOLD" style="width: 40%">Captured </td>
                                                    <td class="Font12pxNormal">
                                                        <asp:Label ID="lblCaptured" runat="server" Text="Unavailable"></asp:Label>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td class="Font12pxBOLD">Searched </td>
                                                    <td class="Font12pxNormal">
                                                        <asp:Label ID="lblSearched" runat="server" Text="Unavailable"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="Font12pxBOLD" style="width: 40%">Disposition </td>
                                                    <td class="Font12pxNormal">
                                                        <asp:Label ID="lblDisposition" runat="server" Text=""></asp:Label>
                                                    </td>

                                                </tr>
                                                <asp:Panel ID="pnlTransferPt" runat="server">
                                                    <tr>
                                                        <td class="Font12pxBOLD">Transfer Point</td>
                                                        <td class="Font12pxNormal">
                                                            <asp:Label ID="lblTransferTxt" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnlMenu" runat="server">

                                        <tr>
                                            <td>
                                                <div class="Font14pxBOLD">
                                                    Requested Menu Options
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="Font12pxNormal">
                                                            <asp:Label ID="lblMenuOption" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                </table>
                            </td>
                            <td style="width: 45%; vertical-align: top">
                                <table style="width: 100%; border: 1px solid;">
                                    <tr>
                                        <td>
                                            <div class="Font14pxBOLD">
                                                Customer Information
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr class="Font12pxBOLD">
                                                    <td style="width: 15%">Customer Number</td>
                                                    <td style="width: 15%">First Name</td>
                                                    <td style="width: 15%">Last Name </td>
                                                </tr>
                                                <tr class="Font12pxNormal">
                                                    <td>
                                                        <asp:Label ID="lblCustNbr" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                    <td style="background-color:#FFFAF0">
                                                        <asp:Label ID="lblFName" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                    <td style="background-color:#FFFAF0">
                                                        <asp:Label ID="lblLName" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="Font12pxNormal">
                                                    <td>&nbsp;</td>
                                                    <td style="background-color:#F0F8FF">
                                                        <asp:Label ID="lblSpFName" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                    <td style="background-color:#F0F8FF">
                                                        <asp:Label ID="lblSpLName" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">&nbsp;</td>
                                                </tr>
                                                <tr class="Font12pxBOLD">
                                                    <td>Address1</td>
                                                    <td>Address2</td>
                                                    <td>City,State,Zip</td>
                                                </tr>
                                                <tr class="Font12pxNormal">
                                                    <td>
                                                        <asp:Label ID="lblAddress1" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAddress2" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCity" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">&nbsp;</td>
                                                </tr>
                                                <tr class="Font12pxBOLD">
                                                    <td>Primary Phone</td>
                                                    <td>Alt Phone</td>
                                                    <td>Email</td>
                                                </tr>
                                                <tr class="Font12pxNormal">
                                                    <td>
                                                        <asp:Label ID="lblHome" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAlt" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEmail" runat="server" Text="N/A"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <asp:DataList ID="dtAccounts" runat="server" Width="80%" GridLines="None" CellSpacing="1" OnItemDataBound="dtAccounts_ItemDataBound">
                        <ItemTemplate>
                            <table style="width: 70%; margin-bottom: 10px; border: 1px solid">
                                <tr class="Font14pxBOLD">
                                    <td>Account:
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACCOUNT_NBR")%>'></asp:Label>
                                    </td>
                                </tr>
                                <tr class="Font12pxBOLDWht">
                                    <td>Package:</td> <td>&nbsp;</td>
                                   
                                </tr>
                                 <tr class="Font12pxBOLDWht">
                                    
                                    <td>Recommendation:&nbsp;&nbsp;<asp:Label ID="lblRecommendations" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RECOMMENDATION")%>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr class="gridhdrLight">
                                                <td>Sell Vendor No.</td>
                                                <td>Sell Vendor</td>
                                                <td>Package Type</td>
                                                <td>Sale Date</td>
                                            </tr>
                                            <tr class="gridrow">
                                                <td>
                                                    <asp:Label ID="lblVendNbr" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VEND_NBR")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblVendName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VEND_NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPkgType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PACKAGE_TYPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSaleDt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SALE_DT")%>'> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr class="Font12pxBOLDWht">
                                    <td>Offer Details:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr class="gridhdrLight">
                                                <td>Offer ID</td>
                                                <td>Offer Description</td>
                                            </tr>
                                            <tr class="gridrow">
                                                <td>
                                                    <asp:Label ID="lblOfferID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OFFER_ID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOfferDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OFFER_DESC")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr class="Font12pxBOLDWht">
                                    <td>Reservations:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr class="gridhdrLight">
                                                <td>Created</td>
                                                <td>Check-In</td>
                                                <td>Departure</td>
                                                <td>Destination</td>
                                            </tr>
                                            <tr class="gridrow">
                                                <td>
                                                    <asp:Label ID="lblResCrDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RES_CREATE_DT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblResvArrDt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ARRIVAL_DT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblResvDepDt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEPARTURE_DT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDest" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESTINATION")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr class="gridhdrLight">
                                                <td>Last Comment</td>
                                            </tr>
                                            <tr class="gridrow">
                                                <td>
                                                    <asp:Label ID="lblComment" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LAST_COMMENT")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr class="gridhdrLight">
                                                <td>Active Promo Codes</td>
                                                <td>Promo Description</td>
                                            </tr>
                                            <tr class="gridrow">
                                                <td>
                                                    <asp:Label ID="lblPromo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PROMOTION_CODE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPromoDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PROMOTION_DESCRIPTION")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

