<%@ Page Language="VB" AutoEventWireup="false" Inherits="VSSA.TravelerPlus_owner_rentAdditionalPoints"
    CodeBehind="rentAdditionalPoints.aspx.vb" SmartNavigation="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html dir="ltr">
<head>

    <title>Rent Additional Points</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <%-- <link href="Styles/vssa.css" rel="stylesheet" type="text/css" />--%>
    <link rev="stylesheet" href="../css/owner.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.4.1-vsdoc.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/boxover.js"></script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 350px;
        }
    </style>
    <style type="text/css">
        /* travelerplus/owner/rentadditionalpoints.aspx */
        #rent-points p {
            font-size: 12px;
            line-height: 1.5em;
            margin: 0;
            padding: 0 0 11px 0;
        }

        #rent-points a {
            color: #3366cc;
            text-decoration: underline;
            font-size: 12px;
        }

        #rent-points ul#nav {
            background: #999;
            height: 26px;
            margin: 0;
            padding: 0;
        }

            #rent-points ul#nav li {
                list-style: none;
                float: left;
            }

                #rent-points ul#nav li a {
                }

                #rent-points ul#nav li#start {
                    display: block;
                    text-indent: -5000px;
                    background: url(../images/rent-points-navigation.gif) 0 0 no-repeat;
                    width: 210px;
                    height: 26px;
                }

                #rent-points ul#nav li#work a {
                    display: block;
                    text-indent: -5000px;
                    background: url(../images/rent-points-navigation.gif) -210px 0 no-repeat;
                    width: 128px;
                    height: 26px;
                }

                #rent-points ul#nav li#cost a {
                    display: block;
                    text-indent: -5000px;
                    background: url(../images/rent-points-navigation.gif) -340px 0 no-repeat;
                    width: 155px;
                    height: 26px;
                }

        #rent-points fieldset {
            margin-top: 10px;
            background: #efefef;
            border: 1px solid #ccc;
            padding: 0;
        }

        #rent-points div.title {
            border-bottom: 1px solid #ccc;
            height: 24px;
            padding: 12px 0 0 0;
        }

            #rent-points div.title span {
                float: right;
                padding: 0 15px 0 0;
            }

        #rent-points h3 {
            margin: 0 0 0 50px;
            padding: 0;
            float: left;
        }

        #rent-points fieldset ol {
            padding: 15px 0 0 50px;
            margin: 0 0 15px 0;
            border-top: 1px solid white;
        }

        #rent-points fieldset select {
            padding: 0;
            margin: 0;
            font-size: 12px;
        }

        #rent-points fieldset label {
            text-align: right;
            display: block;
            width: 100px;
            float: left;
            padding: 0 10px 0 0;
            line-height: 20px;
        }

        #rent-points fieldset#points label {
            text-align: left;
            display: block;
            width: 444px;
            float: left;
            padding: 0 10px 0 0;
            line-height: 15px;
        }

        #rent-points fieldset#points ol {
            padding: 10px 0 0 50px;
            margin: 0;
            height: 26px;
            border-top: none;
        }

        #rent-points fieldset#points select {
            padding: 0;
            margin: -3px 0 0 0;
            font-size: 12px;
        }

        #rent-points fieldset option, #rent-points fieldset select {
            font-size: 12px;
            font-weight: normal;
        }

        #rent-points fieldset li {
            height: 22px;
            padding: 0;
            margin: 0 0 10px 0;
            list-style: none;
        }

            #rent-points fieldset li.required {
                background: url(../images/rent-points-required.gif) 260px 0 no-repeat;
            }

        #rent-points fieldset#points {
            padding: 0;
            background: #efefef url(../images/rent-points-one.gif) 0 0 no-repeat;
        }

        #rent-points fieldset#billing {
            padding: 0;
            background: #efefef url(../images/rent-points-two.gif) 0 0 no-repeat;
        }

        #rent-points fieldset#credit {
            padding: 0;
            background: #efefef url(../images/rent-points-three.gif) 0 0 no-repeat;
        }

        #rent-points .submit {
            margin: 0 0 0 110px;
            padding: 5px 0 15px 0;
        }

            #rent-points .submit input {
                float: left;
            }

            #rent-points .submit a {
                margin: 0 0 0 15px;
                float: left;
                line-height: 18px;
            }

        #rent-points div.desc {
            color: Red;
            font-weight: bold;
        }

        #rent-points .buttonimg input {
            color: White;
        }

        #rent-points .confirmation fieldset#points_2 {
            padding: 0;
            background: white url(../images/rent-points-one.gif) 0 0 no-repeat;
        }

        #rent-points .confirmation fieldset#billing {
            padding: 0;
            background: white url(../images/rent-points-two.gif) 0 0 no-repeat;
        }

        #rent-points .confirmation fieldset#credit {
            padding: 0;
            background: white url(../images/rent-points-three.gif) 0 0 no-repeat;
        }

        #rent-points .confirmation fieldset#points label {
            text-align: left;
            display: block;
            float: left;
            padding: 0 10px 0 0;
            line-height: 15px;
            font-size: 12px;
        }

        #rent-points .confirmation fieldset#points ol {
            border-bottom: 1px solid #ccc;
            padding: 15px 0 0 0;
            margin: 0;
        }

        #rent-points .confirmation h3 {
            margin: 0 0 0 50px;
            padding: 0;
            float: left;
        }

        #rent-points .confirmation ol {
            padding: 10px 0 0 0;
        }

        #rent-points .confirmation fieldset label {
            font-size: 12px;
            text-align: left;
            display: block;
            width: 100%;
            float: left;
            padding: 0 10px 0 0;
            line-height: 20px;
        }

        #rent-points .success p {
            background: url(../images/rent-points-success.gif) 0 0 no-repeat;
            padding: 0 0 20px 40px;
        }

        #rent-points .success h2 {
            text-indent: -5000px;
            background: url(../images/rent-points-use.gif) 0 0 no-repeat;
            height: 26px;
            border-bottom: 1px solid #ccc;
        }

        #rent-points .success ol {
            margin: 0 0 0 15px;
            padding: 0;
        }

        #rent-points .success li {
            list-style-type: square !important;
            margin: 0 0 7px 0;
            font-size: 12px;
        }

        #rent-points .success a {
            font-weight: bold;
        }

        #rent-points .orange {
            color: orange;
            font-size: 12px;
        }

        input.imagebutton1 {
            background-image: url(images/submitBtn.gif);
            padding-left: 0px;
            padding-right: 0px;
            border-bottom-style: none;
            border-top-style: none;
            border-right-style: none;
            border-left-style: none;
            background-color: Transparent;
            background-repeat: no-repeat;
            width: 43px;
            height: 20px;
            cursor: hand;
        }



        input.imagebuttonedit {
            background-image: url(images/edit_btn.gif);
            padding-left: 0px;
            padding-right: 0px;
            border-bottom-style: none;
            border-top-style: none;
            border-right-style: none;
            border-left-style: none;
            background-color: Transparent;
            background-repeat: no-repeat;
            width: 27px;
            height: 20px;
            cursor: hand;
        }
    </style>

</head>
<body leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
    <form id="rentPointsForm" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>


        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true">
            <ProgressTemplate>
                <div class="overlay">
                    <div class="overlayContent">
                        <asp:Image ID="Image1" runat="server" ImageUrl="images/icon-search-loading.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdPnladditionalpts" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
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
                                Font-Underline="true" ForeColor="blue" PostBackUrl="~/Default.aspx" />
                            |
                        <asp:HyperLink NavigateUrl="http://boss" Font-Underline="true" ForeColor="blue" ID="HyperLink2"
                            runat="server" Text="BOSS" CssClass="text"></asp:HyperLink>


                        </td>
                    </tr>
                </table>
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="center">

                            <table cellspacing="0" cellpadding="0" width="600" border="0">
                                <tr>

                                    <td valign="top">




                                        <table cellspacing="0" cellpadding="0" width="600" border="0">
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>

                                                <td valign="top" align="left" width="493">

                                                    <br>




                                                    <br />


                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:Image ID="imgAlert" runat="server" ImageUrl="images/alert.gif" Visible="false" />&nbsp;
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div id="rent-points">
                                                        <a style="color: #ff9900 !important;" href="javascript:window.history.go(<%=Session("HistoryCount")%>)">
                                                            [Go Back]</a>
                                                        <img src="images/rent-additional-header.gif" width="100%" height="35" /><br />


                                                        <ul id="nav" runat="server" class="clear" style="display: none;">
                                                            <li id="start">Start Now</li>
                                                            <li id="work" title="fade=[on] fadespeed=[0.1] cssbody=[tooltip] cssheader=[tooltip-header] header=[HOW DOES IT WORK?] hideselects=[on] body=[<p>Points must be rented in increments of 1,000 and are valid for use up to 12 months from the date of rental. Best of all, as long as your Traveler Plus membership is active, you can rent Points as often as necessary and Points are available for use immediately following rental.</p><p><strong>You can use rented Points to:</strong></p><ol><li>Make reservations at any Bluegreen resort</li><li>Book Hotel PointStays in more than 70 countries</li><li>Redeem for Owner Adventures or cruise vacations</li><li>Reserve Hot Weeks Getaways</li><li>Camp at a Coast to Coast affiliated RV resort</li></ol>]">
                                                                <a href="#">How Does It Work? <span>tooltip</span></a></li>
                                                            <li id="cost" title="fade=[on] fadespeed=[0.1] cssbody=[tooltip] cssheader=[tooltip-header] header=[HOW MUCH DOES IT COST?] hideselects=[on] body=[<p>At a cost of only <strong>$150 per 1,000 Points</strong>, you can afford to extend your vacation or upgrade accommodations! There’s never been a better time to be a Bluegreen Traveler Plus™ member!</p>]">
                                                                <a href="#">How Much Does It Cost? <span>tooltip</span></a></li>
                                                        </ul>



                                                        <fieldset runat="server" id="points" title="Points">
                                                            <ol>
                                                                <li class="clear">
                                                                    <label for="points">
                                                                        <strong>How many Points would you like to rent?&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList
                                                                            AutoPostBack="true" ID="ddlPtsValues" runat="server">
                                                                            <asp:ListItem Selected="true" Value="none">Select One</asp:ListItem>
                                                                            <asp:ListItem Value="150">1000 at $150</asp:ListItem>
                                                                            <asp:ListItem Value="300">2000 at $300</asp:ListItem>
                                                                            <asp:ListItem Value="450">3000 at $450</asp:ListItem>
                                                                            <asp:ListItem Value="600">4000 at $600</asp:ListItem>
                                                                            <asp:ListItem Value="750">5000 at $750</asp:ListItem>
                                                                            <asp:ListItem Value="900">6000 at $900</asp:ListItem>
                                                                            <asp:ListItem Value="1050">7000 at $1050</asp:ListItem>
                                                                            <asp:ListItem Value="1200">8000 at $1200</asp:ListItem>
                                                                            <asp:ListItem Value="1350">9000 at $1350</asp:ListItem>
                                                                            <asp:ListItem Value="1500">10000 at $1500</asp:ListItem>
                                                                        </asp:DropDownList></strong></label></li>
                                                            </ol>
                                                        </fieldset>




                                                        <fieldset id="billing" visible="true" runat="server" title="Billing Information">
                                                            <div class="title">
                                                                <span><font color="orange"><b>*</b></font> Indicates required field</span><h3>Billing
                                                                    Information</h3>
                                                            </div>




                                                            <ol>
                                                                <li class="clear">
                                                                    <label for="firstname"><strong><span class="orange">*</span>First Name:</strong></label>
                                                                    <asp:TextBox ID="txtBxFirstName" runat="server" size="30"></asp:TextBox>
                                                                </li>
                                                                <li class="clear">
                                                                    <label for="lastname"><strong><span class="orange">*</span>Last Name:</strong></label>
                                                                    <asp:TextBox ID="txtBxLastName" runat="server" size="30"></asp:TextBox>
                                                                </li>


                                                                <li class="clear">
                                                                    <label for="city"><strong><span class="orange">*</span>City:</strong></label>
                                                                    <asp:TextBox ID="txtBxCity" runat="server" size="30"></asp:TextBox>
                                                                </li>
                                                                <li class="clear">
                                                                    <label for="state"><strong><span class="orange">*</span>State:</strong></label>
                                                                    <asp:DropDownList ID="ddlState" runat="server">
                                                                    </asp:DropDownList>
                                                                </li>

                                                                <li class="clear">
                                                                    <label for="zip"><strong><span class="orange">*</span>Zip:</strong></label>
                                                                    <asp:TextBox ID="txtBxZip" runat="server" size="30"></asp:TextBox>
                                                                </li>

                                                                <li class="clear">
                                                                    <label for="phone"><strong><span class="orange">*</span>Phone:</strong></label>
                                                                    <asp:TextBox ID="txtBxPhone" runat="server" size="30"></asp:TextBox>
                                                                </li>

                                                                <li class="clear">
                                                                    <label for="email"><strong><span class="orange">*</span>Email:</strong></label>
                                                                    <asp:TextBox ID="txtBxEmail" runat="server" size="30"></asp:TextBox>
                                                                </li>

                                                                <li class="clear">
                                                                    <label for="ownernumber"><strong><span class="orange">*</span>Owner#:</strong></label>
                                                                    <asp:TextBox ID="txtBxBVC" runat="server" ForeColor="Black" size="30"></asp:TextBox>
                                                                </li>
                                                            </ol>
                                                        </fieldset>




                                                        <fieldset visible="true" runat="server" id="credit" title="Credit Card Information">
                                                            <div class="title">
                                                                <h3>Credit Card Information</h3>
                                                                <span>
                                                                    <img src="/images/rent-points-lock.gif"></span>
                                                            </div>
                                                            <ol>
                                                                <li class="clear">
                                                                    <label for="payment"><span class="orange">*</span><strong>Payment: $</strong></label>
                                                                    <asp:TextBox ID="txtBxPayment" runat="server" size="30" ForeColor="Black"></asp:TextBox>
                                                                </li>
                                                                <li class="clear">
                                                                    <label for="state"><span class="orange">*</span><strong>Card Type:</strong></label>
                                                                    <asp:DropDownList runat="server" ID="ddlCCtype">
                                                                        <asp:ListItem Value="V" Selected="True" Text="Visa"></asp:ListItem>
                                                                        <asp:ListItem Value="M" Text="MasterCard"></asp:ListItem>
                                                                        <asp:ListItem Value="A" Text="AMEX"></asp:ListItem>
                                                                        <asp:ListItem Value="D" Text="Discover"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </li>
                                                                <li class="clear">
                                                                    <label for="cardnumber"><span class="orange">*</span><strong>Card Number: </strong>
                                                                    </label>
                                                                    <asp:TextBox ID="txtBxCCNumber" runat="server" size="30" MaxLength="16"></asp:TextBox>

                                                                </li>
                                                                <li class="clear">
                                                                    <label for="expiredate"><span class="orange">*</span><strong>Exp Date:</strong></label>
                                                                    <asp:DropDownList ID="ddlExpMonth" runat="server">
                                                                        <asp:ListItem Value="01">1</asp:ListItem>
                                                                        <asp:ListItem Value="02">2</asp:ListItem>
                                                                        <asp:ListItem Value="03">3</asp:ListItem>
                                                                        <asp:ListItem Value="04">4</asp:ListItem>
                                                                        <asp:ListItem Value="05">5</asp:ListItem>
                                                                        <asp:ListItem Value="06">6</asp:ListItem>
                                                                        <asp:ListItem Value="07">7</asp:ListItem>
                                                                        <asp:ListItem Value="08">8</asp:ListItem>
                                                                        <asp:ListItem Value="09">9</asp:ListItem>
                                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlExpYear" runat="server"></asp:DropDownList>
                                                                </li>
                                                            </ol>
                                                            <div style="width: 100%; height: 30px;">
                                                                <span style="margin: 0 0 25px 170px; padding: 10px; float: left">
                                                                    <asp:ImageButton runat="server" ID="btnCCsubmit" ImageUrl="images/submitBtn.gif" />&nbsp;&nbsp;&nbsp;
                                                                    <asp:ImageButton Visible="false" runat="server" ID="btnCCCancel" ImageUrl="images/edit_btn.gif" /></span>
                                                            </div>
                                                            <br />
                                                            <span style="margin-left: -200px;"><strong>This is a final transaction and cannot be
                                                                cancelled or reversed. </strong></span>
                                                        </fieldset>

                                                        <!-- START FORM CONFIRMATION -->


                                                        <div id="pnlConfirm" runat="server" style="display: none;" visible="false" class="confirmation">
                                                            <p>Please review and confirm the information below. Click <strong>SUBMIT</strong> if
                                                                correct or <strong>EDIT</strong> to make changes.</p>
                                                            <fieldset runat="server" id="points_2" title="Points">
                                                                <asp:Label ID="lblPoints" runat="server"><div class="title"><h3>Points for rent:&nbsp;&nbsp;</h3> <%=Me.ddlPtsValues.SelectedItem.Text%></div></asp:Label>
                                                            </fieldset>
                                                            <fieldset id="billing" title="Billing Information">
                                                                <center>
                                                                    <div class="title">
                                                                        <h3>Billing Information</h3>
                                                                    </div>
                                                                </center>

                                                                <ol span style="margin: 15px 0 0 50px; padding: 0; float: left">
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblFirstName" runat="server"><strong>First Name:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblLastName" runat="server"><strong>Last Name:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblCity" runat="server"><strong>City:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblState" runat="server"><strong>State:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblZip" runat="server"><strong>Zip:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblPhone" runat="server"><strong>Phone:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblEmail" runat="server"><strong>Email:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblOwnerNumber" runat="server"><strong>Owner Number:</strong></asp:Label>
                                                                    </li>
                                                                </ol>
                                                            </fieldset>

                                                            <fieldset id="credit" title="Credit Card Information">

                                                                <div class="title">
                                                                    <h3>Credit Card Information</h3>
                                                                    <span>
                                                                        <img src="images/rent-points-lock.gif"></span>
                                                                </div>
                                                                <ol span style="margin: 15px 0 0 50px; padding: 0; float: left">
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblPayment" runat="server"><strong>Payment:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblCCtype" runat="server"><strong>Card Type:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblCardNumber" runat="server"><strong>Card Number:</strong></asp:Label>
                                                                    </li>
                                                                    <li class="clear">
                                                                        <asp:Label ID="lblExpirationDate" runat="server"><strong>Expiration Date:</strong></asp:Label>
                                                                    </li>
                                                                </ol>


                                                                <div style="margin-top: 150px; margin-left: 100px; margin-right: 30px; color: Red;">
                                                                    Online Rental of Points is a final transaction. Only select "Submit" one time,
                                            do not refresh or press the back button at any point to avoid multiple charges.
                                                                </div>

                                                                <br />


                                                                <span style="margin: 0 0 25px 170px; padding: 10px; float: left">
                                                                    <asp:Button runat="server" ID="btnSubmit" CssClass="imagebutton1" UseSubmitBehavior="False"
                                                                        OnClientClick="this.disabled=true;document.getElementById('btnCancel').style.display='none';" />&nbsp;&nbsp;&nbsp;<asp:Button
                                                                            runat="server" ID="btnCancel" CssClass="imagebuttonedit" />
                                                                </span>


                                                            </fieldset>


                                                            <p class="text">Please review and confirm the information above. Click <strong>SUBMIT</strong>
                                                                if correct or <strong>EDIT</strong> to make changes.</p>
                                                        </div>

                                                        <!-- End Confirmation -->
                                                        <cc1:ModalPopupExtender runat="server" ID="ModalPopupExtender1" PopupControlID="pnlOverlay"
                                                            BackgroundCssClass="modalBackground" OkControlID="btnCloseWindow" OnOkScript="__doPostBack('btnCloseWindow','')"
                                                            TargetControlID="pnlOverlay">
                                                        </cc1:ModalPopupExtender>

                                                        <asp:Panel ID="pnlOverlay" Style="display: none;" runat="server" CssClass="modalPopup">
                                                            <div runat="server" id="pnlSuccess" visible="false">
                                                                <br />
                                                                <br />
                                                                <p>
                                                                    Thank you! You have successfully rented additional points.<br />
                                                                    Your Payment was approved.<br />
                                                                    <strong>Bluegreen owner ID: </strong>
                                                                    <asp:Label ID="lblOwnerId" runat="server" Text=""></asp:Label><br />
                                                                    <strong>Owner: </strong>
                                                                    <asp:Label ID="lblOwnerName" runat="server" Text=""></asp:Label><br />
                                                                    <strong>Rented Points: </strong><%=Session("pointspurchased")%><br />
                                                                    <strong>Payment Amount: $</strong><asp:Label ID="lblPaymentAmount" runat="server"
                                                                        Text=""></asp:Label><br />
                                                                    <strong>Authorization Number: </strong>
                                                                    <asp:Label ID="lblAuthorization" runat="server" Text=""></asp:Label><br />
                                                                    <br />

                                                                </p>
                                                                <p align="center" valign="bottom">
                                                                    <asp:Button ID="btnCloseWindow" runat="server" Text="OK" OnClick="btnCloseWindow_Click" />
                                                                </p>
                                                            </div>

                                                        </asp:Panel>

                                                        <!-- End Success Message -->
                                                    </div>
                                                    <!-- End Rent Points -->


                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCCsubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCloseWindow" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>


    </form>
</body>
</html>