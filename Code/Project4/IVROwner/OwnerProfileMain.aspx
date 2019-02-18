<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OwnerProfileMain.aspx.vb" Inherits="VSSA.OwnerProfileMain" %>

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

    <script type="text/javascript">
        function fixHeader() {
            var tblMtg = document.getElementById("tblMtg");
            if (tblMtg != null) {
                divMtg.style["visibility"] = "visible";
                var tblMtgthead = tblMtg.getElementsByTagName("thead")[0];
                var tblMtg1 = tblMtg.cloneNode(false);
                tblMtg1.appendChild(tblMtgthead);
                tblMtgHeader.appendChild(tblMtg1);
            }

            var tblOwnerType = document.getElementById("tblOwnerType");
            if (tblOwnerType != null) {
                divOwnerType.style["visibility"] = "visible";
                var tblOwnerTypethead = tblOwnerType.getElementsByTagName("thead")[0];
                var tblOwnerType1 = tblOwnerType.cloneNode(false);
                tblOwnerType1.appendChild(tblOwnerTypethead);
                tblOwnerTypeHeader.appendChild(tblOwnerType1);
            }

            var tblAccounts = document.getElementById("tblAccounts");
            if (tblAccounts != null) {
                divtblAccounts.style["visibility"] = "visible";
                var tblAccountsthead = tblAccounts.getElementsByTagName("thead")[0];
                var tblAccounts1 = tblAccounts.cloneNode(false);
                tblAccounts1.appendChild(tblAccountsthead);
                tblAccountsHeader.appendChild(tblAccounts1);
            }

            var tblOwnerPoints = document.getElementById("tblOwnerPoints");
            if (tblOwnerPoints != null) {
                divOwnerPoints.style["visibility"] = "visible";
                var tblOwnerPointsthead = tblOwnerPoints.getElementsByTagName("thead")[0];
                var tblOwnerPoints1 = tblOwnerPoints.cloneNode(false);
                tblOwnerPoints1.appendChild(tblOwnerPointsthead);
                tblOwnerPointsHeader.appendChild(tblOwnerPoints1);
            }

            var tblBilling = document.getElementById("tblBilling");
            if (tblBilling != null) {
                divBilling.style["visibility"] = "visible";
                var tblBillingthead = tblBilling.getElementsByTagName("thead")[0];
                var tblBilling1 = tblBilling.cloneNode(false);
                tblBilling1.appendChild(tblBillingthead);
                tblBillingHeader.appendChild(tblBilling1);
            }

        }
        window.onload = fixHeader;
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

        <table style="width: 100%; margin: auto">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div style="width: 100%; font-size: 30px; text-align: center; padding-bottom: 15px; padding-top: 10px">Bluegreen Owner Info</div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div style="width: 100%; font-size: 15px;">
                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <asp:Label ID="ProcessTime" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 45%; vertical-align: top">

                    <table style="border: 1px solid; width: 95%; margin-bottom: 20px">
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
                                        <td class="Font12pxNormal" style="">
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
                                    <tr>
                                        <td class="Font12pxBOLD">Transfer Point</td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblTransferTxt" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
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

                    <table style="border: 1px solid; width: 95%; margin-bottom: 20px">
                        <tr>
                            <td>
                                <div class="Font14pxBOLD">
                                    Owner Information
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 40%">Primary </td>
                                        <td class="Font12pxNormal">
                                            <asp:Repeater ID="rptprimaryNames" runat="server">
                                                <HeaderTemplate>
                                                    <table style="width:100%">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                                        <td>
                                                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("OwnerName")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label Visible='<%# IIf(rptprimaryNames.Items.Count = 0, True, False)%>' runat="server" ID="Label2" Text="N/A"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 40%">Secondary</td>
                                        <td class="Font12pxNormal">
                                            <asp:Repeater ID="rptsecNames" runat="server" >
                                                <HeaderTemplate>
                                                    <table style="width:100%">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                                        <td>
                                                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("OwnerName")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label Visible='<%# IIf(rptsecNames.Items.Count = 0, True, False)%>' runat="server" ID="Label2" Text="N/A"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 40%">Third </td>
                                        <td class="Font12pxNormal">
                                            <asp:Repeater ID="rptthirdNames" runat="server">
                                                <HeaderTemplate>
                                                    <table style="width:100%">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                                        <td>
                                                            <asp:Label runat="server" ID="Label1" Text='<%# Eval("OwnerName")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label Visible='<%# IIf(rptthirdNames.Items.Count = 0, True, False)%>' runat="server" ID="Label2" Text="N/A"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr class="Font12pxBOLD">
                                        <td style="width: 30%">ARVACT</td>
                                        <td style="width: 40%">Ownership Level</td>
                                        <td style="width: 30%">BGO Reg.</td>
                                    </tr>

                                    <tr class="Font12pxNormal">
                                        <td>
                                            <asp:Label ID="lblArvact" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOwnerLevel" runat="server" Text="N/A"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblRegister" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="Font12pxBOLD">Ownership Type </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="tblOwnerTypeHeader" style="width: 620px;"></div>
                                            <div id="divOwnerType" style="width: 100%; max-height: 200px; overflow-y: auto; overflow-x: hidden; visibility: hidden">
                                                <asp:Repeater ID="rpOwnerTypes" runat="server">
                                                    <HeaderTemplate>
                                                        <table id="tblOwnerType" style="width: 620px;">
                                                            <thead>
                                                                <tr class="gridhdr">
                                                                    <td style="width: 150px;">Project Number</td>
                                                                    <td style="width: 150px;">Account Number</td>
                                                                    <td style="width: 320px;">Description</td>
                                                                </tr>
                                                            </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                                            <td style="width: 150px;">
                                                                <asp:Label runat="server" ID="Label1" Text='<%# IIf(Eval("Legacy.ProjectNumber") = Nothing, "None", Eval("Legacy.ProjectNumber"))%>' /></td>
                                                            <td style="width: 150px;">
                                                                <asp:Label runat="server" ID="Label2" Text='<%# IIf(Eval("AccountNumber") = Nothing, "None", Eval("AccountNumber"))%>' /></td>
                                                            <td style="width: 320px;">
                                                                <asp:Label runat="server" ID="Label3" Text='<%# IIf(Eval("Legacy.ProjectName") = Nothing, "None", Eval("Legacy.ProjectName"))%>' /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr class="Font12pxBOLD">
                                        <td style="width: 60%">Address </td>
                                        <td style="width: 40%">City, State, Zip </td>
                                    </tr>

                                    <tr class="Font12pxNormal">
                                        <td>
                                            <asp:Label ID="lblAddress" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCity" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr class="Font12pxBOLD">
                                        <td style="width: 30%">Primary Phone  </td>
                                        <td style="width: 30%">Business Phone   </td>
                                        <td style="width: 40%">Email Address  </td>
                                    </tr>
                                    <tr class="Font12pxNormal">

                                        <td style="width: 25%">
                                            <asp:Label ID="lblHomePhone" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:Label ID="lblBusinessPhone" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblEmail" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr class="Font12pxBOLD">
                                        <td>TP Level </td>
                                        <td>TP Expiration </td>
                                        <td>TP Auto Renewal </td>
                                        <td>Encore Dividend Balance </td>
                                    </tr>
                                    <tr class="Font12pxNormal">
                                        <td>
                                            <asp:Label ID="lblTpLevel" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTpExpDate" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTpRenewal" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEncDividend" runat="server" Text="N/A"></asp:Label>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr class="Font12pxBOLD">
                                        <td>Last Purchase Date </td>
                                        <td>Upgrade/Reload Process </td>
                                        <td>Account Status  </td>
                                        <td>Account - Security Block </td>
                                    </tr>
                                    <tr class="Font12pxNormal">
                                        <td>
                                            <asp:Label ID="lblLastPurchase" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblUpgrade" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAcctStatus" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSecBlock" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table style="border: 1px solid; width: 95%;">

                        <tr>
                            <td>
                                <div class="Font14pxBOLD">
                                    Mortgage Information
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="tblMtgHeader" style="width: 620px"></div>
                                <div id="divMtg" style="width: 100%; max-height: 200px; overflow-y: auto; overflow-x: hidden; visibility: hidden;">
                                    <asp:Repeater ID="grdMortgage" runat="server" OnItemDataBound="grdMortgage_ItemDataBound">
                                        <HeaderTemplate>
                                            <table id="tblMtg" style="width: 620px;">
                                                <thead>
                                                    <tr class="gridhdr">
                                                        <td style="width: 150px">Loan Number</td>
                                                        <td style="width: 170px">Number of Days Delinquent</td>
                                                        <td style="width: 150px">Delinquent Amount</td>
                                                        <td style="width: 150px">Mortgage Status</td>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                                <td style="width: 150px">
                                                    <asp:Label runat="server" ID="lblLoan" Text='<%# Eval("LoanNumber")%>' /></td>
                                                <td style="width: 170px">
                                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("DelinquentDays")%>' /></td>
                                                <td style="width: 150px">
                                                    <asp:Label runat="server" ID="lblDelqAmt" Text='<%# String.Format("{0:N}", Eval("DelinquentAmount"))%>' /></td>
                                                <td style="width: 150px">
                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("LoanStatus")%>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                    </table>

                </td>

                <td style="width: 45%; vertical-align: top">
                    <table style="border: 1px solid; width: 95%; margin-bottom: 20px">
                        <tr>
                            <td>
                                <div class="Font14pxBOLD">
                                    Billing Information
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 40%">Payment Plan </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblPmtPlan" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Installment Plan </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblInstallPlan" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Next Bill Date </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblBillDate" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Paperless Billing </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblPaperless" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div id="tblBillingHeader" style="width: 630px;"></div>
                                <div id="divBilling" style="width: 100%; max-height: 200px; overflow-y: auto; overflow-x: hidden; visibility: hidden; margin-bottom: 10px;">
                                    <asp:Repeater ID="rpBilling" runat="server" OnItemDataBound="rpBilling_ItemDataBound">
                                        <HeaderTemplate>
                                            <table id="tblBilling" style="width: 630px;">
                                                <thead>
                                                    <tr class="gridhdr">
                                                        <td style="width: 150px;">Account Number</td>
                                                        <td style="width: 101px;">Past Due</td>
                                                        <td style="width: 103px;">Current Balance</td>
                                                        <td style="width: 101px;">Amount</td>
                                                        <td style="width: 175px;">Collection Status</td>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                                <td style="width: 150px">
                                                    <asp:Label runat="server" ID="Label1" Text='<%# IIf(Not IsDBNull(Eval("acct")), Eval("acct"), "None")%>' /></td>
                                                <td style="width: 101px;">
                                                    <asp:Label runat="server" ID="Label2" Text='<%# String.Format("{0:n}", Container.DataItem("OverDueAmount"))%>' /></td>
                                                <td style="width: 103px;">
                                                    <asp:Label runat="server" ID="Label3" Text='<%# String.Format("{0:n}", Container.DataItem("balcu"))%>' /></td>
                                                <td style="width: 101px;">
                                                    <asp:Label runat="server" ID="lblAmtDue" Text='<%# String.Format("{0:n}",Container.DataItem("totalDue"))%>' /></td>
                                                <td style="width: 175px;">
                                                    <asp:Label runat="server" ID="Label15" Text='<%# IIf(Not IsDBNull(Eval("collectionCode")), Eval("collectionCode"), "N/A")%>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <tr class="gridfooter">
                                                <td>Total</td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblTotalDue" Text="" ForeColor="Red" /></td>
                                                <td></td>
                                            </tr>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <table style="border: 1px solid; width: 95%; margin-bottom: 20px">
                        <tr>
                            <td>
                                <div class="Font14pxBOLD">
                                    Points Information
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 40%">Total Billable Points </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblTotalBillablePts" runat="server" Text="N/A"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Available Points </td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblTotalAvailPts" runat="server" Text="N/A"></asp:Label>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <div id="tblAccountsHeader" style="width: 630px"></div>
                                <div id="divtblAccounts" style="width: 100%; max-height: 200px; overflow-y: auto; overflow-x: hidden; visibility: hidden; margin-bottom: 10px;">
                                    <asp:Repeater ID="grdContracts" runat="server">
                                        <HeaderTemplate>
                                            <table id="tblAccounts" style="width: 630px;">
                                                <thead>
                                                    <tr class="gridhdr">
                                                        <td style="width: 110px;">Account Number</td>
                                                        <td style="width: 300px;">Description</td>
                                                        <td style="width: 110px;">Next Earn Date</td>
                                                        <td style="width: 110px;">Next Earn Amount</td>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                               <td style="width: 110px;">
                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("AccountNumber")%>' /></td>
                                                <td style="width: 300px;">
                                                    <asp:Label runat="server" ID="Label2" Text='<%# Eval("Legacy.ProjectName")%>' /></td>
                                                <td style="width: 110px;">
                                                  <asp:Label runat="server" ID="Label3" Text='<%# Eval("Legacy.Expiration")%>' /></td>
                                                <td style="width: 110px;">
                                                  <asp:Label runat="server" ID="Label4" Text='<%# Eval("NextEarnAmount")%>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td>

                                <div id="tblOwnerPointsHeader" style="width: 630px"></div>
                                <div id="divOwnerPoints" style="width: 100%; max-height: 200px; overflow-y: auto; overflow-x: hidden; visibility: hidden; margin-bottom: 10px">
                                    <asp:Repeater ID="grdOwnerPoints" runat="server">
                                        <HeaderTemplate>
                                            <table id="tblOwnerPoints" style="width: 630px">
                                                <thead>
                                                    <tr class="gridhdr">
                                                        <td style="width: 105px;">Account Number</td>
                                                        <td style="width: 100px;">Available Points</td>
                                                        <td style="width: 110px;">Expire Date</td>
                                                        <td style="width: 115px;">Points Type</td>
                                                        <td style="width: 100px;">Sec Mkt</td>
                                                        <td style="width: 100px;">Action Needed</td>
                                                    </tr>
                                                </thead>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                                <td style="width: 105px;">
                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("AcctNum")%>' /></td>
                                                <td style="width: 100px;">
                                                    <asp:Label runat="server" ID="Label2" Text='<%# IIf(Eval("pointBal") = -1, "N/A", String.Format("{0:N0}", Eval("pointBal")))%>' /></td>
                                                <td style="width: 110px;">
                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("expireDate")%>' /></td>
                                                <td style="width: 115px;">
                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("pointTypeDesc")%>' /></td>
                                                <td style="width: 100px;">
                                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("SecMkt")%>' /></td>
                                                <td style="width: 100px;">
                                                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("Action")%>' /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%;">
                    <table style="border: 1px solid; width: 100%;">
                        <tr>
                            <td>
                                <div class="Font14pxBOLD" style="width: 100%">
                                    Reservation Information
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 47%;">
                                    <tr>
                                        <td class="Font12pxBOLD" style="width: 60%">Number of Reservations in past 12 months</td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblTotalReservations" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Number of Remaining Cancel Waivers</td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblWaivers" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Font12pxBOLD">Number of Remaining Free Blue/White Stays</td>
                                        <td class="Font12pxNormal">
                                            <asp:Label ID="lblBHStays" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="rptPendingReservations" runat="server">
                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr class="gridhdr">
                                                <td>Reservation No.</td>
                                                <td>Reservation Type</td>
                                                <td>Reservation Name</td>
                                                <td>Source</td>
                                                <td>Resort</td>
                                                <td>Check In</td>
                                                <td>Unit Type</td>
                                                <td>No. Of Nights</td>
                                                <td>Points</td>
                                                <td>Amount (B/T)</td>
                                                <td>PPP</td>
                                                <td>Source Code/Promo</td>
                                                <td>Exchange (Y/N)</td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="<%# IIf(Container.ItemIndex Mod 2 = 0, "gridrow", "gridrowalt")%>">
                                            <td>
                                                <asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container, "DataItem._ReservationNumber")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# ReservationType(DataBinder.Eval(Container, "DataItem._ReservationType"), DataBinder.Eval(Container, "DataItem._ExchangeCode"))%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem._GuestFullName")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem._TakenBy")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text='<%# getResortName(DataBinder.Eval(Container, "DataItem._ProjectStay"), DataBinder.Eval(Container, "DataItem._ReservationType"))%>'> </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text='<%# NumericToDate(DataBinder.Eval(Container, "DataItem._CheckInDate"), "yyMMdd")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text='<%# getVillaDescription(DataBinder.Eval(Container, "DataItem._ProjectStay"), DataBinder.Eval(Container, "DataItem._AS400UnitType"))%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem._NumberOfNights")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text='<%# convertPoints(DataBinder.Eval(Container, "DataItem._Points"), DataBinder.Eval(Container, "DataItem._ReservationType"))%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem._AmountDue")%>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="Label11" runat="server" Text='<%# PolicyStatus(DataBinder.Eval(Container, "DataItem._PolicyStatus"), DataBinder.Eval(Container, "DataItem._EligibleDate"), "Future", DataBinder.Eval(Container, "DataItem._ReservationNumber"), DataBinder.Eval(Container, "DataItem._ExchangeCode"), DataBinder.Eval(Container, "DataItem._ReservationType"))%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label12" runat="server" Text="N/A"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem._ExchangeCode")%>'></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>