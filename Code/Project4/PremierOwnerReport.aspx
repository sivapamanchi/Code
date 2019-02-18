<%@ Page Language="VB" AutoEventWireup="false" Inherits="VSSA.PremierOwnerReport" CodeBehind="PremierOwnerReport.aspx.vb" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VSSA Account Manager</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="Styles/vssa.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .clearfix {
            display: block;
            width: 881px;
        }

            .clearfix:after {
                display: block;
                visibility: hidden;
                clear: both;
                content: ".";
                height: 0;
                font-size: 0;
            }

        .required {
            color: red;
        }
        .btnExcel {
            /*background-image: url('');
            background-size: 21px 21px;*/
            width : 70px;
            height : 70px;
            background: url('images/microsoft-excel-icon.png') #f2f2f2;
            background-repeat: no-repeat;
            background-size: 40px 40px !important;
            color: #000;
        }
    </style>
   
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

     <link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
        <link href="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
      <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
     <style type="text/css">
         
        #gridSearchResults td {
            cursor :pointer;
        }
        .selected_row {
            background-color: #ff9900;
           font-size: 9px;   
        }
        fieldset.scheduler-border {
             border: 2px groove #ddd !important;
             padding: 0 1.4em 1.4em 1.4em !important;
             margin: 0 0 1.5em 0 !important;
             -webkit-box-shadow: 0px 0px 0px 0px #000;
             box-shadow: 0px 0px 0px 0px #000;
             border-color: darkgray !important;
            border-style: solid !important;
            width: 735px !important;
            margin-left : 10px !important;
            display :inline-block;
         }

         legend.scheduler-border {
             font-size: 1.2em !important;
             font-weight: bold !important;
             text-align: left !important;
             width: auto;
             padding: 0 10px;
             border-bottom: none;
         }
        .resortDataw {
            font-size: 9px;
            background-color: #333333;
            text-align: center;
            color: #FFFFFF;
           
        }
         .resortDataw th {
             text-align :center;
         }
        .resDetail {
            font-size: 9px;
            background-color: #cfcfcf;
            text-align: center;
            color: Black;
        }
         tr.resDetail td:not(:first-child) {
             text-align :left;
         }
        .resAltDetail {
            font-size: 9px;
            background-color: #FFFFFF;
            text-align: center;
            color: Black;
        }
        tr.resAltDetail td:not(:first-child) {
             text-align :left;
         }
        .pager a {
            border: 1px solid #EDF5FF;
            color: #0067A5;
            text-decoration: underline;
            padding: 2px 5px;
        }

        .pager span {
            background-color: #0067A5;
            border: 1px solid #DBEAFF;
            color: #FFFFFF;
            padding: 2px 5px;
        }

        .pager a:hover {
            color: #1E90FF;
        }

        .blur {
            background-color: White;
            filter: alpha(opacity=80);
            opacity: 80;
            z-index: 99999;
            top: 220px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $(document).ready(function () {
                $('[id*=cboRequestStatus]').multiselect({
                    includeSelectAllOption: true,
                    buttonWidth: '200px',
                    nonSelectedText :'Please Select'

                });
                $('[id*=cboResortID]').multiselect({
                    includeSelectAllOption: true,
                    buttonWidth: '350px',
                    nonSelectedText: 'Please Select'
                });
                $('[id*=ddlUpdateStatus]').multiselect({
                    includeSelectAllOption: false,
                    buttonWidth: '200px'

                });
                
            });
        });
        
        $(function () {
            $("[id*=gridSearchResults] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=gridSearchResults] tr").each(function () {
                    if ($(this)[0] != row[0]) {
                        $("td", this).removeClass("selected_row");
                    }
                });
                $("td", row).each(function () {
                    if (!$(this).hasClass("selected_row")) {
                       // $(this).addClass("selected_row");
                    } else {
                        $(this).removeClass("selected_row");
                    }
                });
            });
        });

        function limitText(limitField, limitNum) {
            if (limitField.value.length >= limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
                $('#divCss').addClass("alert-info");
                if (limitField.id == "txtInternalNotes") { $("#divCss span").text("Maximum of 100 characters exceeded."); }
                else if (limitField.id == "txtSpecialReq") { $("#divCss span").text("Maximum of 100 characters exceeded."); }
            } else {
                $('#divCss').removeClass("alert-info");
                $("#divCss span").text("");
                //limitCount.value = limitNum - limitField.value.length;
            }
        }
    </script>
</head>
<body>
    <div class="mainBody">
        <form id="frmPremierOwner" runat="server">
           <%--  <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>--%>
           <cc1:ToolkitScriptManager ID="ScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
           <%-- <script type="text/javascript">
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
            </script>--%>
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
                        <td align="center" valign="top" width="214"></td>
                        <td align="center" valign="top" width="176"></td>
                        <td colspan="2" align="right" valign="top">
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

                </table>
                <div>
                    <fieldset id="filterFieldSet" runat="server" class="scheduler-border">
                        <legend class="scheduler-border">
                            <asp:Label ID="lblSearchFieldsetLegend" runat="server"><span style="color:#33bbff !important;">Search</span></asp:Label></legend>
                        <div class="control-group">
                            <table >
                                <tr>
                                    <td valign="middle" colspan="2" class="Font10pxBOLD" align="justify">&nbsp;Resort Name :
                                         <br /><br />
                                        <div style="display: block; width: 100%; float: left;text-align:left;">
                                            <asp:ListBox ID="cboResortID" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </td>
                                    <td valign="middle" colspan="2" class="Font10pxBOLD" align="justify">&nbsp;Request Status :
                                        <br /><br />
                                         <div style="display: block; width: 100%;float:right;text-align:left;">
                                            <asp:ListBox ID="cboRequestStatus" runat="server" SelectionMode="Multiple">
                                                <%--<asp:ListItem Selected="True" Text="All" Value="0" />--%>
                                                <asp:ListItem Text="Reservation Confirmed" Value="Reservation Confirmed" />
                                                <asp:ListItem Text="Request Pending" Value="Request Pending" />
                                                <asp:ListItem Text="Not Available" Value="Not Available" />
                                                <asp:ListItem Text="Not Eligible" Value="Not Eligible" />
                                                <asp:ListItem Text="Request Cancelled" Value="Request Cancelled" />
                                                <asp:ListItem Text="Limited Availability–Max Requests Received" value="Limited Availability–Max Requests Received"></asp:ListItem>
                                            </asp:ListBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr><td><br /></td><td><br /></td></tr>
                                <tr>
                                    <td valign="middle" colspan="2" class="Font10pxBOLD" align="center">Date Of Arrival :<br />
                                    </td>
                                    <td valign="middle" colspan="2" class="Font10pxBOLD" align="center">Date Entered :<br />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">&nbsp;From :<br />
                                        <asp:TextBox runat="server" ID="FromCheckInDate"  class="form-control"/>
                                        <cc1:CalendarExtender ID="FromCheckIn" runat="server" CssClass="ajax__calendar"
                                            TargetControlID="FromCheckInDate" Format="MM/dd/yyyy" />
                                    </td>
                                    <td valign="middle" class="Font10pxBOLD" align="left">&nbsp;To :<br />
                                        <asp:TextBox runat="server" ID="ToCheckInDate" class="form-control"/>
                                        <cc1:CalendarExtender ID="ToCheckIn" runat="server" CssClass="ajax__calendar"
                                            TargetControlID="ToCheckInDate" Format="MM/dd/yyyy" />
                                    </td>
                                    <td valign="middle" class="Font10pxBOLD" align="left">&nbsp;From :<br />
                                        <asp:TextBox runat="server" ID="FromEnteredDate" class="form-control"/>
                                        <cc1:CalendarExtender ID="FromEnterDate" runat="server" CssClass="ajax__calendar"
                                            TargetControlID="FromEnteredDate" Format="MM/dd/yyyy" />
                                    </td>
                                    <td valign="middle" class="Font10pxBOLD" align="left">&nbsp;To :<br />
                                        <asp:TextBox runat="server" ID="ToEnteredDate" class="form-control"/>
                                        <cc1:CalendarExtender ID="ToEnterDate" runat="server" CssClass="ajax__calendar"
                                            TargetControlID="ToEnteredDate" Format="MM/dd/yyyy" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="right" valign="middle">
                                        <br />
                                        <asp:Button ID="imgCheckAvailability" runat="server" Text="Search" class="btn btn-primary"/>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" valign="middle">
                                        <asp:Label runat='server' ID="lblDateValError" CssClass="Font10pxBOLD required" Text="Error" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                    <br />
                    <br />
                    <div id="divSearchResults" runat="server" visible="false">
                        <table style="width: 723px;">
                            <tr>
                                <td>Click Any Column to sort by Field. Select Request ID to change status and add confirmation number.</td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle">
                                    <%--<asp:ListItem Selected="True" Text="All" Value="0" />--%>
                                    <asp:GridView ID="gridSearchResults" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                        OnRowCommand="gridSearchResults_RowCommand" Width="750px" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                       OnPageIndexChanging="gridSearchResults_PageIndexChanging" OnSorting="SortRecords" AllowSorting="True" >
                                        <Columns>
                                            <%--<asp:HyperLinkField ItemStyle-Width="60px" DataTextField="RequestID" DataNavigateUrlFields="RequestID" HeaderText="Request ID"  />--%>
                                            <%-- <asp:BoundField ItemStyle-Width="80px" DataField="RequestID" HeaderText="Request ID" />--%>
                                            <asp:TemplateField HeaderText="RequestID" SortExpression="RequestID" ItemStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblRequestID" runat="server"
                                                        Text='<%# Eval("RequestID")%>'
                                                        CommandName="RequestIdUpdate"
                                                        CommandArgument='<%#Bind("RequestID")%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField ItemStyle-Width="180px" DataField="ResortName" HeaderText="Resort"  SortExpression="ResortName"/>
                                            <asp:BoundField ItemStyle-Width="50px" DataField="ARVACT" HeaderText="ARVACT" SortExpression="ARVACT"/>
                                            <asp:BoundField ItemStyle-Width="50px" DataField="OwnerName" HeaderText="Owner Name" SortExpression="OwnerName"/>
                                            <asp:BoundField ItemStyle-Width="50px" DataField="NoOfGuests" HeaderText="Guest" SortExpression="NoOfGuests"/>
                                            <asp:BoundField ItemStyle-Width="50px" DataField="CheckInDate" HeaderText="Check-In Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="CheckInDate"/>
                                            <asp:BoundField ItemStyle-Width="50px" DataField="CheckOutDate" HeaderText="Check-Out Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="CheckOutDate"/>
                                             <asp:BoundField ItemStyle-Width="50px" DataField="NoofNights" HeaderText="Number of Nights" SortExpression="NoofNights"/>
                                            <asp:BoundField ItemStyle-Width="100px" DataField="VillaSize" HeaderText="Unit/Estimated Points" SortExpression="VillaSize"/>                                            
                                            <asp:BoundField ItemStyle-Width="50px" DataField="DateOfRequest" HeaderText="Date Entered" DataFormatString="{0:MM/dd/yyyy}" SortExpression="DateOfRequest"/>
                                            <asp:BoundField ItemStyle-Width="120px" DataField="Status" HeaderText="Status" SortExpression="Status"/>
                                        </Columns>
                                        <%--<HeaderStyle CssClass="resortDataw" />
                                        <AlternatingRowStyle CssClass="resDetail" />
                                        <RowStyle HorizontalAlign="Center" CssClass="resAltDetail" />
                                        <PagerStyle CssClass="pager" />--%>        
                                        <HeaderStyle CssClass="resortDataw" />  
                                        <AlternatingRowStyle CssClass="resDetail" />
                                        <RowStyle HorizontalAlign="Center" CssClass="resAltDetail " />                              
                                        <PagerStyle CssClass="pagination-ys" />
                                        <SelectedRowStyle CssClass="selected_row" />
                                    </asp:GridView>
                                    
                                    <%--<asp:ListItem Selected="True" Text="All" Value="0" />--%>
                                    <br />
                                    <div style="text-align:right;vertical-align:bottom;">
                                        
                                        <asp:LinkButton ID="btnExport" CssClass="" runat="server" OnClick="ExportToExcel">
                                            <img src="images/microsoft-excel-icon.png" alt="Excel Image" width="40px" height="40px" />
                                            Export To Excel</asp:LinkButton>
                                        <%--<asp:HyperLinkField ItemStyle-Width="60px" DataTextField="RequestID" DataNavigateUrlFields="RequestID" HeaderText="Request ID"  />--%>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%-- <asp:BoundField ItemStyle-Width="80px" DataField="RequestID" HeaderText="Request ID" />--%>
                    <asp:Label runat='server' ID="lblError" CssClass="Font10pxBOLD required" Text="Error" Visible="false" /><br />
                    <div id="divCss" runat="server" width="723px" style="width: 723px; text-align: center;margin-left:10px;">
                        <asp:Label runat="server" ID="lblUpdateStatusMsg" CssClass=""></asp:Label>
                    </div>
                    <fieldset id="rowDetailsFieldSet" runat="server" visible="false" class="scheduler-border">
                        <legend class="scheduler-border">
                            <asp:Label ID="lblrowDetailsLegend" runat="server"><span style="color:#33bbff !important;">Update</span></asp:Label></legend>
                        <div class="control-group">
                            <table style="width: 740px">
                                <tr>
                                    <td  align="left" style="font-size:14px;" class="selected_row">Request #: 
                                        <asp:Label ID="lblRequest" Width="150px" runat="server"  disabled="disabled"></asp:Label>
                                    </td>
                                    <td> </td>
                                    <td> </td>
                                    <td> </td>                                    
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Resort Name 
                                     <br />
                                        <asp:TextBox runat="server" ID="textResortName" Width="100%" class="form-control" ReadOnly=true />
                                    </td>
                                    <td valign="middle" class="Font10pxBOLD" align="left"  colspan="1">Request Status 
                                      <br />
                                        <asp:DropDownList ID="ddlUpdateStatus" runat="server" Width="100%" AutoPostBack="true"  >                                            
                                            <asp:ListItem Text="Reservation Confirmed" Value="Reservation Confirmed" />
                                            <asp:ListItem Text="Request Pending" Value="Request Pending" />
                                            <asp:ListItem Text="Not Available" Value="Not Available" />
                                            <asp:ListItem Text="Not Eligible" Value="Not Eligible" />
                                            <asp:ListItem Text="Request Cancelled" Value="Request Cancelled" />
                                            <asp:ListItem Text="Limited Availability–Max Requests Received" value="Limited Availability–Max Requests Received"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                     <td  valign="middle" runat="server" id="confirmationNumber" class="Font10pxBOLD" align="left" colspan="1">Confirmation Number <span class="required">*</span>
                                      <br />
                                        <asp:TextBox runat="server" ID="txtConfirmationNumber"  class="form-control" Width="150px" MaxLength="5" />
                                      
                                    </td>
                                </tr>
                                <tr><td><br /></td></tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left" width="50%">Internal Notes :
                                     <br />
                                        <textarea id="txtInternalNotes" name="txtInternalNotes" rows="5" cols="10" runat="server" class="form-control" style="min-width: 100%" 
                                            onKeyDown="limitText(this.form.txtInternalNotes,100);"
                                            onKeyUp="limitText(this.form.txtInternalNotes,100);" maxlength="100"
                                            ></textarea>
                                       
                                    </td>
                                    <td valign="middle" runat="server" class="Font10pxBOLD" align="left" colspan="2">Special Requests:
                                      <br />
                                        <textarea id="txtSpecialReq" name="txtSpecialReq" rows="5" cols="10" runat="server" class="form-control" style="min-width: 100%"
                                            onKeyDown="limitText(this,100);"
                                            onKeyUp="limitText(this,100);" maxlength="100"
                                            ></textarea>
                                    </td>                                   
                                </tr>
                                
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Owner Level :

                                    </td>
                                    <td valign="middle" align="left" colspan="2">
                                        <asp:Label ID="lblOwnerLevel" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Arvact # : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblArvactNum" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Guest Name : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblGuestName" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Guest Email : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblGuestEmail" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Guest Phone : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblGuestPhone" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Check-In Date : </td>
                                    <td valign="middle" align="left" colspan="2">
                                        <asp:Label ID="lblCheckInDate" Width="150px" runat="server" disabled="disabled" Text="Label"></asp:Label>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Check-Out Date : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblCheckOutDate" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Villa Size : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblUnitType" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Number of Nights : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblNumberOfNights" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>        
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Date Completed : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblDateCompleted" Width="150px" runat="server" 
                                             disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Number Of Guests : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblNumOfGuests" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle" class="Font10pxBOLD" align="left">Last Modified By : </td>
                                    <td valign="middle" align="left" colspan="2">
                                         <asp:Label ID="lblLastModifiedBy" Width="150px" runat="server" disabled="disabled"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right" valign="middle">
                                        <br />
                                        <asp:Button ID="lblUpdate" runat="server" Text="Update" class="btn btn-primary"/>
                                        <br />
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </fieldset>
                    

                    <%--<HeaderStyle CssClass="resortDataw" />
                                        <AlternatingRowStyle CssClass="resDetail" />
                                        <RowStyle HorizontalAlign="Center" CssClass="resAltDetail" />
                                        <PagerStyle CssClass="pager" />--%>
                </div>
            </div>
            <div class="clearfix">
                <asp:Label runat="server" ID="lblAdminName" CssClass="Font10px" Visible="false"/>
                <asp:Label runat="server" ID="lblUsername" />
            </div>
        </form>
    </div>
</body>
</html>
