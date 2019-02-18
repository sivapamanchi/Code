<%@ Page Language="VB" AutoEventWireup="false" Inherits="VSSA._Default" Codebehind="Default.aspx.vb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VSSA Account Manager</title>
     <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="Styles/vssa.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        div.mybox {
    border: 1px solid black;
    width:400px;
    height:250px;
}
    </style>
     <style type="text/css">
        .full-width {
  width: 100vw;
}
    </style>
    <style type="text/css">
        .Suggesst {
            border-collapse: collapse;
            border: 1px solid black;
        }
        .tall-row td {   padding: 80px 10px; }
</style>
     <script language="javascript" type="text/javascript">
       function trim(el) {
           el.value = el.value.replace(/(^\s*)|(\s*$)/gi, "")
           return;
       }
    </script>
    <script language="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
     <style type="text/css">

    .modalBackground {
        background-color:Gray;
        filter:alpha(opacity=70);
        opacity:0.7;
    }

    .modalPopup {
        background-color:#ffffdd;
        border-width:3px;
        border-style:solid;
        border-color:Gray;
        padding:3px;
        width:350px;
    }
    .MStatusmodalPopup {
        background-color:#ffffdd;
        border-width:3px;
        border-style:solid;
        border-color:Gray;
        padding:3px;
        width:400px;
    }


    </style>
   
     <script language="javascript" type="text/javascript">
         function check(e) {
             var keynum
             var keychar
             var numcheck
             // For Internet Explorer
             if (window.event) {
                 keynum = e.keyCode
             }
                 // For Netscape/Firefox/Opera
             else if (e.which) {
                 keynum = e.which
             }
             keychar = String.fromCharCode(keynum)
             //List of special characters you want to restrict
             if (keychar == "*" ) 
             {

                 return false;
             }
             else {
                 return true;
             }   
         }
</script>
    <style type="text/css">   
        .clearfix
        {
            display: block;
            width: 881px;
        }
        .clearfix:after
        {
            display: block;
            visibility: hidden;
            clear: both;
            content: ".";
            height: 0;
            font-size: 0;
        }
        #homeTab_form
        {
            float: left;
            list-style: none;
            margin: 0px;
            padding: 0px;
        }
        #homeTab_form li
        {
            float: left;
            width: 192px;
            list-style: none;
            margin: 0px 10px 20px 0px;
            padding: 0px;
        }
        #homeTab_form li label
        {
            float: left;
            width: 83%;
        }
        #homeTab_form li input, #homeTab_form li textarea
        {
            clear: both;
            margin: 0px;
            padding: 0px;
            font-size: 11px;
            font-weight: normal;
        }
        #homeTab_form li input[type="checkbox"]
        {
            float: left;
            width: auto !important;
            margin-right: 5px;
        }
        #homeTab_form li select
        {
            clear: both;
            width: 100%;
            height: 20px;
            margin: 0px;
            padding: 0px;
            font-size: 11px;
            font-weight: normal;
        }
        #homeTab_form li option
        {
            font-size: 11px;
            font-weight: normal;
        }
        #homeTab_form .required
        {
            color: #900;
        }
    </style>
    
    <style type="text/css">
    
    .blur
    {
 
    background-color: White;
 
   filter: alpha(opacity=80);
    opacity: 80;

    z-index: 99999;
 top:220px;
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
    <div class="mainBody">
        <form id="frmVSSA" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <%--    this client-side code is to disable another request during the postback. 
    The Javascript disables the element during the beginRequest event of the PageRequestManager
    and enables it when control has been returned to the browser in the endRequest event. 
    The control causing the postback is returned from the get_postBackElement() method of the 
    BeginRequestEventArgs object which is passed to the function handling the beginRequest event.
        --%>
        <script type="text/javascript">
            var pbControl = null;
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                pbControl = args.get_postBackElement();  //the control causing the postback
                pbControl.disabled = true;
            }
            function EndRequestHandler(sender, args) {
                pbControl.disabled = false;
                pbControl = null;
            }
        </script>
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
                        <%--<asp:LinkButton ID="lbPremierOwner" runat="server" Text="Premier Waitlist" CssClass="text" 
                            Font-Underline="true" ForeColor="Blue" Visible="true" />
                        <asp:Label ID="lbPremierOwnerDivider" runat="server" Text="|" Visible="true" />--%>
                        <asp:LinkButton ID="lbResetGroupsForAccess" runat="server" Text="Reset Access" CssClass="text" 
                            Font-Underline="true" ForeColor="Blue" Visible="false" />
                        <asp:Label ID="lblResetGroupsDivider" runat="server" Text="|" Visible="false" />
                        <asp:LinkButton ID="lbProxyAdmin" runat="server" Text="Proxy Admin" CssClass="text" 
                            Font-Underline="true" ForeColor="Blue" Visible="false" />
                        <asp:Label ID="lblProxyAdminDivider" runat="server" Text="|" Visible="false" />
                        <asp:LinkButton ID="lbAddressUpload" runat="server" Text="Address Upload" CssClass="text" 
                            Font-Underline="true" ForeColor="Blue" Visible="false" />
                        <asp:Label ID="lblAddressUploadDivider" runat="server" Text="|" Visible="false" />
                        <asp:LinkButton runat="server" ID="lbNewSearch" Text="New Search" CssClass="text"
                            Font-Underline="true" ForeColor="blue" />
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
                <asp:Panel ID="pnlSearch" runat="server" Visible="true">
                    <tr>
                        <td valign="middle" class="Font10pxBOLD">
                            Owner Number (ARVACT):<br />
                            <asp:TextBox runat="server" ID="txtARVACT" MaxLength="30" Columns="30" CssClass="Font10px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                        <td valign="middle" class="Font10pxBOLD">
                            First Name:<br />
                            <asp:TextBox runat="server" ID="txtFName" CssClass="Font10px"></asp:TextBox>
                        </td>
                        <td valign="middle" class="Font10pxBOLD">
                            Last Name:<br />
                            <asp:TextBox runat="server" ID="txtLName" CssClass="Font10px"></asp:TextBox>
                        </td>
                        <td valign="middle" class="Font10pxBOLD">
                            Email Address:<br />
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="Font10px"></asp:TextBox>
                        </td>
                        <td valign="middle" class="Font10pxBOLD">
                            Phone Number:<br />
                            <asp:TextBox runat="server" ID="txtPhone" MaxLength="30" Columns="30" CssClass="Font10px" onkeypress="return isNumberKey(event)" ></asp:TextBox>
                        </td>
                         <td valign="middle" runat="server" class="Font10pxBOLD" id="hideTSW" visible="false">
                            TSW Person ID:<br />
                            <asp:TextBox runat="server" ID="tbSrchTSW" MaxLength="10" Columns="30" CssClass="Font10px" onkeypress="return isNumberKey(event)"></asp:TextBox>
                        </td>
                        <td valign="bottom" class="Font10pxBOLD">
                            <asp:Button CssClass="btnVSSA" runat="server" ID="btnSearch" Text="SEARCH"></asp:Button>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlHelpText" runat="server" Visible="true">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblINFO1" CssClass="Font10px" Text="You must enter the data in its entirety when entering an <b>Owner Number (ARVACT).</b>" />
                        </td>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblINFO2" CssClass="Font10px" Text="<b>You can enter all OR part of the First Name OR Last Name</b> (e.g., first three letters). This is helpful when you are not sure of the spelling." />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblINFO3" CssClass="Font10px" Text="You must enter the data in its entirety when entering an <b>Email Address.</b>" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblINFO4" CssClass="Font10px" Text="You must enter the data in its entirety. <b>Do not use spaces, dashes or any other characters</b> when entering the Phone Number." />
                        </td>
                        <td id="hideTSWhint" runat="server" visible="false" >
                            <asp:Label runat="server" ID="lblINFO5" CssClass="Font10px" Text="You must enter the TSW Person ID in its entirety. <b>TSW Person ID</b> is a 3 to 10 digit number not beginning with zero." />
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="clearfix">
            <div id="divSearchResults" runat="server">
                <asp:GridView runat="server" ID="gvSearchResults" AutoGenerateColumns="false" HeaderStyle-BackColor="darkGray"
                    HeaderStyle-CssClass="Font10px" EmptyDataText="Could not find any owner with information entered."
                    EmptyDataRowStyle-CssClass="Font10px" BorderStyle="none" AllowPaging="true" AllowSorting="true" OnSorting="gvSearchResults_Sorting" 
                    OnPageIndexChanging="gvSearchResults_PageIndexChanging" PageSize="40" OnDataBound="gvSearchResults_DataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="ARVACT" ItemStyle-CssClass="Font10px" SortExpression="ARVACT">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbARVACT" Text='<%# DataBinder.Eval(Container, "DataItem.ARVACT") %>'
                                    CausesValidation="false" CommandName="GetDetail"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Reg" HeaderText="Reg." ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="NameFirst" HeaderText="First Name" ItemStyle-CssClass="Font10px" SortExpression="NameFirst" />
                        <asp:BoundField DataField="NameLast" HeaderText="Last Name" ItemStyle-CssClass="Font10px" SortExpression="NameLast" />
                        <asp:BoundField DataField="Email" HeaderText="Email Address" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="PhoneHome" HeaderText="Primary Phone" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Address1" HeaderText="Address" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="City" HeaderText="City" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="State" HeaderText="State" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="PostalCode" HeaderText="ZIP" ItemStyle-CssClass="Font10px" />
                        <asp:BoundField DataField="Last4SSN" HeaderText="SSN" ItemStyle-CssClass="Font10px" />
                    </Columns>
                </asp:GridView>
                <%-- BEGIN OWNER ACCOUNT VIEW ########################################################### --%>
                <asp:Label runat='server' ID="lblError" CssClass="Font10px" />
                <div id="div_ownerInfo" runat="server" style="margin-left: auto; margin-right: auto">
                    <div style="float: left; position: relative;">
                        <asp:GridView ID="gvARVACT" runat="server" AutoGenerateColumns="false" BorderStyle="none"
                            BorderWidth="1" HeaderStyle-CssClass="Font10pxHeader" EmptyDataText="No Data Found."
                            HorizontalAlign="Justify">
                            <Columns>
                                <asp:BoundField DataField="!" HeaderText="Owner#" ItemStyle-CssClass="Font10px" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="float: left; position: relative;">
                        <asp:GridView ID="gvAccountHolders" runat="server" AutoGenerateColumns="false" BorderStyle="none"
                            BorderWidth="1" HeaderStyle-CssClass="Font10pxHeader" EmptyDataText="No Data Found."
                            HorizontalAlign="Justify">
                            <Columns>
                                <asp:BoundField DataField="OwnerName" HeaderText="Account Holders" ItemStyle-CssClass="Font10px" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="float: left; position: relative;">
                        <asp:GridView ID="gvContractInfo" runat="server" HeaderStyle-CssClass="Font10pxHeader"
                            AutoGenerateColumns="false" BorderStyle="none" BorderWidth="1" EmptyDataText="No Data Found.">
                            <AlternatingRowStyle BackColor="#FFCCCC" />
                            <Columns>
                                <asp:BoundField DataField="proj" HeaderText="Proj." ItemStyle-CssClass="Font10px" />
                                <asp:BoundField DataField="AcctNum" HeaderText="Account #" ItemStyle-CssClass="Font10px" />
                                <asp:BoundField DataField="ProjNM" HeaderText="Description" ItemStyle-CssClass="Font10px" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="float: left; position: relative;">
                        <asp:GridView ID="gvCollectionCode" runat="server" HeaderStyle-CssClass="Font10pxHeader"
                            AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1" EmptyDataText="No Data Found.">
                            <Columns>
                                <asp:BoundField HeaderText="Collection Code" DataField="!" ItemStyle-CssClass="Font10px"
                                    ItemStyle-Wrap="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="float: left; position: relative;">
                        <asp:GridView ID="gvFirstLogin" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="Font10pxHeader"
                            BorderStyle="None" BorderWidth="1" EmptyDataText="NA" Width="140px">
                            <Columns>
                                <asp:BoundField HeaderText="First Login" DataField="!" ItemStyle-CssClass="Font10px"
                                    ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="float: left; position: relative;">
                        <asp:GridView ID="gvLastLogin" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="Font10pxHeader"
                            BorderStyle="None" BorderWidth="1" EmptyDataText="NA" Width="140px">
                            <Columns>
                                <asp:BoundField HeaderText="Last Login" DataField="!" ItemStyle-CssClass="Font10px"
                                    ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="float: left; position: relative;">
                        <asp:GridView ID="gvLoginCount" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="Font10pxHeader"
                            BorderStyle="None" BorderWidth="1" EmptyDataText="NA" Width="70px">
                            <Columns>
                                <asp:BoundField HeaderText="Total" DataField="!" ItemStyle-CssClass="Font10px" ItemStyle-Width="3"
                                    ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div id="moreInfo" runat="server">
            <br />
         <asp:Panel ID="PnlEditTSW" runat="server" Width="547" >
          <div class="full-width">
             <asp:Label ID="lblTwsID" runat="server" Text="TSW Person ID: " CssClass="Font10pxBOLD" Visible="False" />
             <asp:Label ID="lbltswPersonID" runat="server" Text='' CssClass="Font10px" Visible="true" />
             <asp:TextBox ID="tbEditable" runat="server" MaxLength="25" CssClass="Font10px" Visible="False"  onkeypress="return isNumberKey(event)"></asp:TextBox>
             <asp:Button ID="btnEdit" CssClass="btnVSSA" runat="server" Text="EDIT" Visible="false"  OnClick="btnEdit_Click" ></asp:Button >
             <asp:Button ID="btnUpdateTSW" CssClass="btnVSSA" runat="server" Text="UPDATE" Visible="false" OnClick="btnUpdateTSW_Click"  ></asp:Button >
             <asp:Button ID="btnCancel" CssClass="btnVSSA" runat="server" Text="CANCEL" Visible="false" OnClick="btnCancel_Click"></asp:Button >
             <asp:Label ID="lblTSWMessage" runat="server" CssClass="Font10pxBOLD" Visible="False" /><br />
            <asp:Label ID="Reg" runat="server" Text="Registered: " CssClass="Font10pxBOLD" Visible="False" />
            <asp:Label ID="lblRegistered" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
            <asp:Label ID="Label1" runat="server" Text="Traveler Plus Expiration: " CssClass="Font10pxBOLD" Visible="False" />
            <asp:Label ID="lblTPExp" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
            <asp:Label ID="label2" runat="server" Text="Collection Code: " CssClass="Font10pxBOLD" Visible="false" />
            <asp:Label ID="lblCollCode" runat="server" Text='' CssClass="Font10px" Visible="true" /><br />
        </div>
      </asp:Panel>
                        <asp:Panel ID="pnlOverlay" style="display:none;" Visible="false" runat="server" CssClass="modalPopup">
                                    <p style="text-align: center;"> TSW Person id already exists</p>
                            <div style ="height:200px; width:350px; overflow:auto;">
                             <asp:GridView runat="server" ID="gvTSW" AutoGenerateColumns="false" HeaderStyle-BackColor="darkGray"
                    HeaderStyle-CssClass="Font10px" EmptyDataText="Could not find any owner with information entered."
                    EmptyDataRowStyle-CssClass="Font10px" BorderStyle="none" Width="350PX" style="text-align: left" >
                    <Columns>   
                        <asp:BoundField DataField="ARVACT" HeaderText="ARVACT" ItemStyle-CssClass="Font10px"  />
                        <asp:BoundField DataField="NameFirst" HeaderText="First Name" ItemStyle-CssClass="Font10px"   />
                        <asp:BoundField DataField="NameLast" HeaderText="Last Name" ItemStyle-CssClass="Font10px"   />
                        <asp:BoundField DataField="TSWPersonID" HeaderText="TSW PersonID" ItemStyle-CssClass="Font10px"  />
                    </Columns>
                </asp:GridView>
                                </div>
                                    <p align="center" valign="bottom">  <asp:Button ID="btnCloseWindow" runat="server" Text="OK" onclick="btnCloseWindow_Click"  />&nbsp;&nbsp;<asp:Button ID="btnCancelWindow" runat="server" OnClick="btnCancelWindow_Click" Text="Cancel" /></p>
                        </asp:Panel>
                      </div>  
        <cc1:ModalPopupExtender runat="server" ID="ModalPopupExtender1"  PopupControlID="pnlOverlay" 
                                BackgroundCssClass="modalBackground" CancelControlID="btnCancelWindow"  OnCancelScript="__doPostBack('btnCancelWindow','')"  OkControlID="btnCloseWindow" OnOkScript="__doPostBack('btnCloseWindow','')"
                                TargetControlID="pnlOverlay" >
                        </cc1:ModalPopupExtender>
       <div style="display: none;"></div>
        <div class="clearfix">
            <div style="display: inline-block; border: 0px solid; float: left;">
                        <%-- BEGIN OWNER CONTACT VIEW ########################################################### --%>
                        <asp:FormView runat="server" ID="fvOwner" Width="351px" >
                            <ItemTemplate>
                                <asp:Panel runat="server" ID="pnlUserInfo" BackColor="LightGray" Width="440px">
                                    <table id="Table1" border="0" cellpadding="9" runat="server" width="440px">
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblUpdateMessage" CssClass="Font12px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label15" runat="server" Text="**First Name:" CssClass="Font10pxBOLD" />
                                                <asp:Label ID="lblfirst" runat="server" Text='<%# Eval("nameFirst")  %>' CssClass="Font10px"
                                                    Width="75px" />
                                                <asp:Label ID="Label17" runat="server" Text="*Last Name:" CssClass="Font10pxBOLD" />
                                                <asp:Label ID="lbllast" runat="server" Text='<%# Eval("nameLast") %>' CssClass="Font10px"
                                                    Width="75px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 0%">
                                                <table width="50%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label12" runat="server" Text="*Country:" CssClass="Font10pxBOLD" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:DropDownList runat="server" ID="ddlCountry" Width="308" CssClass="Font10px"
                                                                CausesValidation="false" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Value="NA" Text="None Entered"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label13" runat="server" Text="*Address1" CssClass="Font10pxBOLD" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:TextBox runat="server" Text='<%# Eval("Address1") %>' CssClass="Font10px" Width="300"
                                                                ID="txtAddress1" MaxLength="30" onkeypress="return check(event)" onchange="return trim(this)"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label14" runat="server" Text="Address2" CssClass="Font10pxBOLD" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:TextBox runat="server" Text='<%# Eval("Address2") %>' CssClass="Font10px" Width="300"
                                                                ID="txtAddress2" ValidationGroup="address" MaxLength="30" onkeypress="return check(event)" onchange="return trim(this)"  />
                                                        </td>
                                                        </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblAddress3" runat="server" Text="Address3" CssClass="Font10pxBOLD"
                                                                Visible="false"  />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:TextBox runat="server" Text='<%# Eval("Address3") %>' CssClass="Font10px" Width="300"
                                                                ID="txtAddress3" Visible="false" MaxLength="30" onkeypress="return check(event)" onchange="return trim(this)" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label runat="server" Text="*City: " CssClass="Font10pxBOLD" ID="lblCity" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:TextBox runat="server" Text='<%# Eval("City") %>' CssClass="Font10px" Width="300"
                                                                ID="txtCity" Visible="true" ValidationGroup="address" MaxLength="30" onkeypress="return check(event)" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" Text="*State/Province: " CssClass="Font10pxBOLD" Width="30"
                                                                ID="lblState" Visible="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" Text="*Zip/Postal Code: " CssClass="Font10pxBOLD" ID="lblZip"
                                                                Visible="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlState" Width="185" CssClass="Font10px" Visible="true" />
                                                        </td>
                                                        <td>
                                                       <asp:TextBox runat="server" ID="txtZip" Text='<%# Eval("postalCode") %>' CssClass="Font10px"
                                                                Visible="true" ValidationGroup="address" AutoPostBack="True" onkeypress="return isNumberKey(event)" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblHomePhone" runat="server" Text="*Primary Phone: " CssClass="Font10pxBOLD" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAltPhone" runat="server" Text="Alternate Phone: " CssClass="Font10pxBOLD" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtHomePhone" Width="140" CssClass="Font10px" Text='<%# Eval("phoneHome") %>'
                                                                ValidationGroup="address" MaxLength="10" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtAltPhone" Text='<%# Eval("phoneCell") %>' Width="140"
                                                                CssClass="Font10px" MaxLength="10" />
                                                        </td>
                                                    </tr>
                                                    <br />
                                                    <tr>
                                                        <td colspan="2" align="right">
                                                             <asp:Button ID="btnUpdate" CommandName="UpdateContactInfo" UseSubmitBehavior="false"  CssClass="btnVSSA" ValidationGroup="address"  runat="server" Text="UPDATE" OnClick="btnUpdate_Click" ></asp:Button >
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <br />
                                                            <asp:Label runat="server" ID="lblRequired1" ForeColor="black" CssClass="Font10px"
                                                                Text="*Required Fields" /><br />
                                                            <asp:Label runat="server" ID="lblNameChanges" CssClass="Font10px" Text="**Name changes must be completed by the Maintenance Fee Department and can only be made once proper documentation is received." />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:FormView>
            </div>
             <asp:Panel ID="SubmitnosuggPanel" style="display:none;" Visible="false" runat="server" CssClass="MStatusmodalPopup"  >
                  <div id="divmelisastatus" runat="server" style="border:groove;">
                             <asp:Label ID="lblMStatus" runat="server" Text="" CssClass="Font12px"></asp:Label>
                </div>
                 <table>
                    <tr>
                        <td><b>Select an address</b></td>
                    </tr>
                    <tr>
                        <td>Please confirm this is the correct address and click submit.
                            <hr>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" align="left" cellpadding="4" cellspacing="3">
                                <tr align="left">
                                    <th>Address Entered</th>
                                </tr>
                                <tr align="left">
                                    <td>
                                        <asp:RadioButton AutoPostBack="true" ID="rbconfirm" runat="server" Checked="True" OnCheckedChanged="rbconfirm_CheckedChanged" />
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="confirmaddr1" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="confirmaddr2" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="confirmcity" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="confirmstate" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="confirmpostal" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                 <asp:Label ID="confirmationlbl" runat="server" Text=""></asp:Label>
                  <asp:LinkButton ID="lbconfirm" runat="server" CausesValidation="False"></asp:LinkButton>
              <p align="center" valign="bottom"><asp:Button ID="btnconfirmSubmit" runat="server" CssClass="btnVSSA" Text="Submit selected address" onclick="btnconfirmSubmit_Click"/>&nbsp;&nbsp;<asp:Button ID="btnconfirmCancel" CssClass="btnVSSA" runat="server" OnClick="btnconfirmCancel_Click" Text="Cancel" /></p>
            </asp:Panel>
                 <cc1:ModalPopupExtender runat="server" ID="ModalPopupExtender3"  PopupControlID="SubmitnosuggPanel" DropShadow="true" 
                                BackgroundCssClass="modalBackground" CancelControlID="btnconfirmCancel"  OnCancelScript="__doPostBack('btnconfirmCancel','')"  OkControlID="btnconfirmSubmit" OnOkScript="__doPostBack('btnconfirmSubmit','')"
                                TargetControlID="lbconfirm" >
                        </cc1:ModalPopupExtender>
            <asp:Panel ID="SuggPanel" style="display:none;" Visible="false" runat="server" CssClass="MStatusmodalPopup"  >
                <div id="dvMeliStatus" runat="server" style="border:groove ;">
                             <asp:Label ID="lblMeliStatus" runat="server" Text="" CssClass="Font12px"></asp:Label>
                </div>
                <table>
                     
                    <tr>
                        <td><b>Select an address</b></td>
                    </tr>
                    <tr>
                        <td>We found a more precise version of the address you entered. If it looks right, please
                            use the suggested address.
                            <hr>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" align="left" cellpadding="4" cellspacing="3">
                                <tr align="left">
                                    <th>Address Entered</th>
                                    <th>Suggessted Address</th>
                                </tr>
                                <tr align="left">
                                    <td>
                                        <asp:RadioButton AutoPostBack="true"  ID="rbentered" OnCheckedChanged="rbentered_CheckedChanged" runat="server" />
                                    </td>
                                     <td>
                                        <asp:RadioButton AutoPostBack="true" ID="rbsuggested" OnCheckedChanged="rbsuggested_CheckedChanged" Checked="True" runat="server" />
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="enterAddr1" runat="server" Text="" CssClass="Font10px" /></td>
                                    <td><asp:Label ID="suggAddr1" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="enterAddr2" runat="server" Text="" CssClass="Font10px" /></td>
                                    <td><asp:Label ID="suggAddr2" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="entercity" runat="server" Text="" CssClass="Font10px" /></td>
                                    <td><asp:Label ID="suggcity" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="enterstate" runat="server" Text="" CssClass="Font10px" /></td>
                                    <td><asp:Label ID="suggstate" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                                <tr align="left">
                                    <td><asp:Label ID="enterpostal" runat="server" Text="" CssClass="Font10px" /></td>
                                    <td><asp:Label ID="suggpostal" runat="server" Text="" CssClass="Font10px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:LinkButton ID="btnhidden" runat="server" CausesValidation="False"></asp:LinkButton>
          <p align="center" valign="bottom">  <asp:Button ID="Suggok" runat="server" CssClass="btnVSSA" Text="Submit selected address" onclick="Suggok_Click" />&nbsp;&nbsp;<asp:Button ID="SuggCancel" CssClass="btnVSSA" runat="server" OnClick="SuggCancel_Click" Text="Cancel" /></p>
            </asp:Panel>
                 <cc1:ModalPopupExtender runat="server" ID="ModalPopupExtender2"  PopupControlID="SuggPanel" DropShadow="true" 
                                BackgroundCssClass="modalBackground" CancelControlID="SuggCancel"  OnCancelScript="__doPostBack('SuggCancel','')"  OkControlID="Suggok" OnOkScript="__doPostBack('Suggok','')"
                                TargetControlID="btnhidden" >
                        </cc1:ModalPopupExtender>
            <div runat="server" id="ownerMoreDetails" visible="false" style="display: inline-block;
                border: 0px solid;">
                <table id="Table2" border="0" width="9px" cellpadding="9" runat="server" style="background-color: lightgrey">
                    <tr>
                        <td valign="top">
                            <table style="background-color: white; width: 400px">
                                <tr>
                                    <td>
                                        <asp:Label runat="Server" ID="lblfeedback" CssClass="Font12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p class="Font10pxBOLD">
                                            Email Address:</p>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbConfirmEmail" Visible="false" Text="*Confirm Name/Email:"
                                            CssClass="Font10px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtcurrentEmail" CssClass="Font10px" Width="200"
                                            Enabled="true" />
                                        <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtcurrentEmail"
                                            ValidationExpression=".*@.*\..*" ErrorMessage="Your entry is not a valid e-mail address."
                                            Display="none" ValidationGroup="email" Enabled="true" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" nowrap align="left">
                                        <asp:Button ID="btnUpdateEmail"  CssClass="btnVSSA" runat="server" Text="UPDATE" ></asp:Button >
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>

                                </tr>
                                <tr style="padding-top: 30">
                                    <td colspan="3" nowrap align="right">
                                           <asp:Button ID="btnresetUserPassword" CssClass="btnVSSA" runat="server" Text="RESET USER PASSWORD" ></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" nowrap align="right" >                                      
                                        <asp:Button ID="btnLoginasOwner" CssClass="btnVSSA" runat="server" Text="LOGIN AS USER" ></asp:Button>
                                    </td>
                                </tr>
                                <tr style="padding-top:30px;">
                                    <td colspan="3" nowrap align="right" style="padding-top: 10px;">                                         
                                        <a  style="text-decoration: none;"  href="ReservationSearch.aspx?ownerNumber=<%= owner.ARVACT%>">
                                             <asp:Label CssClass="btnVSSA" runat="server" ID="lblOwnerReservation" Text="VIEW OWNER RESERVATION"></asp:Label></a>
                                    </td>
                                </tr>

                                  <tr style="padding-top:30px;">
                                    <td colspan="3" nowrap align="right" style="padding-top: 10px;">                                     
                                      <a style="text-decoration: none;"  href="cpaccounts.aspx">
                                            <asp:Label CssClass="btnVSSA" runat="server" ID="lblChoicePrivileges"  Text="CHOICE PRIVILEGES"></asp:Label></a>
                                    </td>
                                </tr>
                                <tr style="padding-top:30px;">
                                    <td colspan="3" nowrap align="right"  style="padding-top: 10px; padding-bottom: 10px;">
                                      
                                      <a href="rentAdditionalPoints.aspx" style="text-decoration: none;">
                                          <asp:Label CssClass="btnVSSA" runat="server" ID="lblRentAdditionalPoints" Visible="false" Text="RENT ADDITIONAL POINTS"></asp:Label>
                                        

                                      </a>
                                            
                                    </td>
                                </tr>
                                <tr style="padding-top: 30px;">
                                    <td colspan="3" nowrap align="right" style="padding-top: 10px;">
                                        <asp:LinkButton runat="server" ID="lbProxyVote" style="text-decoration: none;">
                                            <asp:Label Visible="false" CssClass="btnVSSA" runat="server" ID="btnProxyVote" Text="PROXY VOTE"></asp:Label></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr visible="false">
                                    <td colspan="3" nowrap align="right">
                                        <asp:ImageButton Visible="false" ID="btnPrintReservations" CommandName="printReservations"
                                            runat="server" ImageUrl="" AlternateText="Print Owner Reservations" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
                


            </div>
        </div>
        <%-- END OWNER DATA ################################################################################### --%>
        <p align="Left">
        </p>
           
            
        <div class="clearfix">
            <asp:Label runat="server" ID="lblAdminName" CssClass="Font10px" />
            <asp:GridView runat="server" ID="gvDebug" />
            <asp:Label runat="server" ID="lblUsername" />
        </div>
          
        </form>
    </div>
</body>
</html>
