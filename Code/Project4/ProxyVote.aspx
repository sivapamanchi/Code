<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProxyVote.aspx.vb" Inherits="VSSA.ProxyVote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proxy Vote</title>
    <style type="text/css">
        .ajax__tab_outer .ajax__tab_tab .ajax__tab_inner .ajax__tab_body .ajax__tab_active {
            border: 0;
            border-top: 0;
            border-top-color: White;
        }
        .CustomTabStyle .ajax__tab_header .ajax__tab_active {
            visibility: hidden;
        }

        #tabs_Vote_btnSubmit {

            font-family:verdana,tahoma,helvetica;
            font-size:10pt;
            color:black;

        }

    </style>
    <link href="Styles/vssa.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ChoiceStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AjaxCalendar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <cc1:toolkitscriptmanager ID="scriptManager1" runat="server"></cc1:toolkitscriptmanager>
    <!-- BEGIN HEADER -->   
     <div>
            <!-- TOP BAR START -->
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
                    <td align="center" valign="top" width="214">
                    </td>
                    <td align="center" valign="top" width="176">
                    </td>
                    <td colspan="2" align="right" valign="top">
                        <asp:LinkButton runat="server" ID="lbNewSearch" Text="New Search" CssClass="text"
                            Font-Underline="true" ForeColor="blue"  PostBackUrl="~/Default.aspx"/>
                        |
                        <asp:HyperLink NavigateUrl="http://boss" Font-Underline="true" ForeColor="blue" ID="HyperLink2"
                            runat="server" Text="BOSS" CssClass="text"></asp:HyperLink>
                        
                    </td>
                </tr>
            </table>
    </div>
    <!--END BEGIN HEADER -->
    <br /><br />
    <!-- ERROR MESSAGE--> 
     <div>
        <asp:Label ID="lblOwnerAccountError" runat="server" Text="" Font-Bold="True" ForeColor="Maroon"></asp:Label>    
    </div>
    <!-- END ERROR MESSAGE --> 
     <div class="clearfix">
            <div id="divSearchResults" runat="server">
                <asp:GridView runat="server" ID="gvSearchResults" AutoGenerateColumns="false" HeaderStyle-BackColor="darkGray"
                    HeaderStyle-CssClass="Font10px" EmptyDataText="Could not find any owner with information entered."
                    EmptyDataRowStyle-CssClass="Font10px" BorderStyle="none" Width="760PX">
                    <Columns>                       
                        <asp:BoundField DataField="ARVACT" HeaderText="ARVACT" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Reg" HeaderText="Reg." ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="NameFirst" HeaderText="First Name" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="NameLast" HeaderText="Last Name" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Email" HeaderText="Email Address" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="PhoneHome" HeaderText="Primary Phone" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Address1" HeaderText="Address" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="City" HeaderText="City" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="State" HeaderText="State" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="PostalCode" HeaderText="ZIP" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Last4SSN" HeaderText="SSN" ItemStyle-CssClass="Font10px" />
                    </Columns>
                </asp:GridView>
                
            </div>
    </div>
     <br /><br />
     <div id="moreInfo" runat="server">
            <br />
            <asp:Label ID="Reg" runat="server" Text="Registered: " CssClass="Font10pxBOLD" Visible="False" />
            <asp:Label ID="lblRegistered" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
            <asp:Label ID="Label1" runat="server" Text="Traveler Plus Expiration: " CssClass="Font10pxBOLD"
                Visible="False" />
            <asp:Label ID="lblTPExp" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
            <asp:Label ID="label2" runat="server" Text="Collection Code: " CssClass="Font10pxBOLD"
                Visible="false" />
            <asp:Label ID="lblCollCode" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
         <br />
         <br />
            <a href="javascript:history.go(-1)">[Go Back]</a>
        </div>
        <br />
        <br />
        <cc1:TabContainer ID="tabs" runat="server">
            <cc1:TabPanel ID="Vote" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:Panel ID="pnlVotingMessage" runat="server" Visible="false"> 
                            <asp:Label ID="lblvoting" runat="server" CssClass="Font12px"></asp:Label>
                            <br /><br />
                        </asp:Panel>
                        <asp:Label ID="lblHeader" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label><br />
                        <br />
                        <asp:GridView ID="gvVotes" AutoGenerateColumns="false" runat="server" BackColor="White" ShowHeaderWhenEmpty="true" 
                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" >
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
                                <asp:BoundField DataField="Candidate" HeaderText="Candidate" ItemStyle-CssClass="Font10px" />
                                <asp:BoundField DataField="VotingYear" HeaderText="Voting Year" ItemStyle-CssClass="Font10px" />
                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ItemStyle-CssClass="Font10px" />
                                <asp:BoundField DataField="ModifiedDate" HeaderText="Modified Date" ItemStyle-CssClass="Font10px" />
                            </Columns>
                        </asp:GridView>
                        <br /><br />
                        Select Candidate: <br />
                        <asp:DropDownList ID="ddlCandidate" runat="server" Width="120" AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                        <br /><br />
                        <asp:Panel ID="pnlWriteInCandidate" runat="server" Visible="false">
                            Write-in Candidate:
                            <br />
                            <asp:TextBox ID="txtCandidate" runat="server" Width="120"></asp:TextBox>
                        </asp:Panel>
                        <br /><br />
                        <asp:panel ID="pnlDesignatedVoter" runat="server">
                            <div>
                                Is there a desginated voter for this account? <br />
                                <asp:label id="lblDesignatedVoter" runat="server" Font-Size="8" Font-Italic="true">By selecting a designated voter this account will no longer be able to vote online</asp:label>  <br />
                                <asp:DropDownList ID="ddlDesignatedVoter" runat="server" Width="120" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                &nbsp;<asp:Button runat="server" ID="btnRemove" BorderStyle="Inset" Text="Remove" BackColor="Crimson" visible="false"/>
                            </div>
                        </asp:panel>
                        <br /><br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" UseSubmitBehavior="true" />
    
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </form>
</body>
</html>
