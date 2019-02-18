<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProxyAdmin.aspx.vb" Inherits="VSSA.ProxyAdmin" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proxy Admin</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
<%--    <script type="text/javascript">
        function dateselect(ev) {

            var calendarBehavior1 = $find("Calendar1");
            var d = calendarBehavior1._selectedDate;
            var now = new Date();
            calendarBehavior1.get_element().value = d.format("MM/dd/yyyy") + " " + now.format("HH:mm")

        }
    </script>--%>

    <link href="Styles/vssa.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ajax__tab_tab {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
            color: black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- TOP BAR START -->
         <asp:Label ID="lblStatus" runat="server" Visible="false" CssClass="Font10px" ></asp:Label>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="vertical-align: top; width: 214">
                    <img height="30" alt="" src="images/topBarGreyLinesShadow.gif" width="214" style="border: none" />
                </td>
                <td style="vertical-align: top; width: 176">
                    <img height="30" alt="" src="images/topBrGryLinesShdowBckrnd.gif" width="176" style="border: none" />
                </td>
                <td style="vertical-align: top; width: 199">
                    <img height="30" alt="" src="images/topBrGryDots.gif" width="199" style="border: none" />
                </td>
                <td style="vertical-align: top; width: 171">
                    <img height="30" alt="" src="images/topBrGrnDots.gif" width="168" style="border: none" />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" width="214"></td>
                <td align="center" valign="top" width="176"></td>
                <td colspan="2" align="right" valign="top">
                    <asp:LinkButton ID="lbAddressUpload" runat="server" Text="Address Upload" CssClass="text"
                        Font-Underline="true" ForeColor="Blue" Visible="false" />
                    <asp:Label ID="lblAddressUploadDivider" runat="server" Text="|" Visible="false" />
                        <asp:LinkButton runat="server" ID="lbNewSearch" Text="New Search" CssClass="text"
                            Font-Underline="true" ForeColor="blue"  PostBackUrl="~/Default.aspx"/>
                    |
                        <asp:HyperLink NavigateUrl="http://boss" Font-Underline="true" ForeColor="blue" ID="HyperLink2"
                            runat="server" Text="BOSS" CssClass="text"></asp:HyperLink>

                </td>
            </tr>
        </table>
        <!-- TOP BAR END -->
        <table class="text" border="0" width="6%" cellpadding="0">
            <tr>
                <td id="banner">
                    <img alt="" src="images/oss_hdr.gif" />
                </td>
            </tr>
            <asp:Label runat="server" ID="LabelErrorMsg" CssClass="Font10px" Visible="false"></asp:Label>
            <asp:Panel ID="pnlGeneralMessage" runat="server" Visible="false">
                <asp:Label runat="server" ID="lblGeneralMessage" CssClass="Font10px"></asp:Label>
            </asp:Panel>

        </table>
        <br /><br />
         
        <cc1:toolkitscriptmanager ID="scriptManager1" runat="server"></cc1:toolkitscriptmanager>
        <cc1:TabContainer ID="tabs" runat="server">
            <cc1:TabPanel ID="Vote" runat="server" HeaderText="Start/End Date">
                <ContentTemplate>
                    <div>
                         <asp:Label ID="lblselectDates" runat="server" Visible="false" CssClass="Font10px" ></asp:Label>
                        <br /><br />
                        &nbsp;&nbsp;Select Voting Year: <br />
                        &nbsp;&nbsp;<asp:Label ID="lblVotingYear" runat="server" Font-Size="8" Font-Italic="true">* All times are Eastern</asp:Label>
                        <br />
                        &nbsp;&nbsp;<asp:DropDownList ID="ddlYear2" Width="180" runat="server" EnableViewState="true" AutoPostBack="true" CausesValidation = "false" ></asp:DropDownList>
                        <br /><br />
                        &nbsp;&nbsp;Enter Start Date & Time: <br />
                        &nbsp;&nbsp;<asp:TextBox ID="txtStartDate" runat="server" Width="180"></asp:TextBox> 
                        &nbsp;&nbsp;<asp:DropDownList ID="ddlStartTime" runat="server" />
                        <cc1:CalendarExtender ID="ceStartDate" runat="server" TargetControlID="txtStartDate" />
                        <br /><br />
                        &nbsp;&nbsp;Enter End Date & Time: <br />
                        &nbsp;&nbsp;<asp:TextBox ID="txtEndDate" runat="server" Width="180"></asp:TextBox>
                        &nbsp;&nbsp;<asp:DropDownList ID="ddlEndTime" runat="server" />
                        <cc1:CalendarExtender ID="ceEndDate" runat="server" TargetControlID="txtEndDate"  />
                        <br /><br /><br />
                        &nbsp;&nbsp;<asp:button ID="btnSaveDates" runat="server" Text="Save" CausesValidation = "false" />

                    </div><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                </ContentTemplate>
            </cc1:TabPanel>
           
            <cc1:TabPanel ID="tbManageCandidates" runat="server"  HeaderText="Manage Candidates">
                <ContentTemplate>
                 
                    <div>
                        <asp:Panel ID="pnlCandidateMessage" runat="server" Visible="false">
                            &nbsp;&nbsp;<asp:Label ID="lblGvCandidateMessage" runat="server" /><br /><br />
                        </asp:Panel>
                        &nbsp;&nbsp;
                        <asp:GridView ID="gvCandidates" runat="server" AllowPaging="true" PageSize="20" BackColor="White" BorderColor="#999999" 
                            AutoGenerateColumns="false" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" >
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                            <Columns>
                                <asp:BoundField DataField="CandidateID" HeaderText="Candidate ID" SortExpression="CandidateID" visible="false" />
                                <asp:BoundField DataField="CandidateName" HeaderText="Candidate Name"  SortExpression="ResortName"/>
                                <asp:BoundField DataField="OwnerID" HeaderText="Owner ID"  SortExpression="OwnerID"/>
                                <asp:BoundField DataField="VotingYear" HeaderText="Voting Year"  SortExpression="VotingYear"/>
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By"  SortExpression="ModifiedBy"/>
                                <asp:BoundField DataField="ModifiedDate" HeaderText="Modified Date"  SortExpression="ModifiedDate"/>
                                <asp:CommandField ShowSelectButton="true" SelectText="Edit" HeaderText=""  />
                                <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" ShowEditButton="false" HeaderText=""  />
                            </Columns>
                        </asp:GridView>
                       
                        <br /><br />
                        &nbsp;&nbsp;<asp:LinkButton id="lbAddNew" runat="server" Text="Add New Candidate"></asp:LinkButton>
                        <br /><br />
                        <asp:Panel ID="pnlAddCandidate" runat="server" BorderWidth="1" BorderStyle="Ridge" BorderColor="LightSlateGray" Width="400">
                            <div>
                                <br /><br />
                                <asp:HiddenField ID="hdnCandidateID" Visible="false" runat="server" EnableViewState="true" value="" />
                                &nbsp;&nbsp;Select Voting Year: <br />
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlYear" Width="120" runat="server"></asp:DropDownList>
                                <br /><br />
                                &nbsp;&nbsp;Enter Candidate Name: <br />
                                &nbsp;&nbsp;<asp:TextBox ID="txtCandidateName" MaxLength="25" runat="server" Width="180"></asp:TextBox>
                                <br /><br />
                                &nbsp;&nbsp;Enter Candidate Owner ID: <br />
                                &nbsp;&nbsp;<asp:TextBox ID="txtCandidateOwnerID" MaxLength="15" runat="server" Width="180"></asp:TextBox>
                                <br /><br />
                                &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CausesValidation = "false"  />
                            </div>
                            <br /><br />
                        </asp:Panel>

                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="tbEligibleVoters" runat="server"  HeaderText="Eligible Voters">
                <ContentTemplate>
                    <br /><br />
                    &nbsp;&nbsp;Select Voting Year: <br />
                    &nbsp;&nbsp;<asp:DropDownList ID="ddlYear3" Width="120" runat="server" EnableViewState="true" AutoPostBack="true" CausesValidation = "false" ></asp:DropDownList>
                    <br /><br />
                    &nbsp;&nbsp;Enter number of eligible owners to vote: <br />
                    &nbsp;&nbsp;<asp:TextBox ID="txtEligibleVoters" runat="server" Width="120"></asp:TextBox>
                    <br /><br />
                    &nbsp;&nbsp;<asp:Button ID="btnSave2" runat="server" Text="Save" CausesValidation = "false"  />
                    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </form>
</body>
</html>
